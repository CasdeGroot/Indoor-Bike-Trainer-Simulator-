using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using KettlerReader;
using NetwerkConnector.NetwerkConnector;

namespace NetworkConnector
{
    public class Client : TCPController
    {
        public delegate void ConnectionClosed();

        //public event EventHandler messageReceived;

        public delegate void ConnectionsUpdated(List<ClientIdentifier> connections);

        public delegate void HistoryUpdated(List<HistoryStats> stats);

        public delegate void MessagesUpdated(List<TextMessage> messages);

        public enum LoginResponse
        {
            Unknown,
            Accepted,
            Denied,
            AccountAlreadyInUse,
            AcountBanned
        }

        public bool connected = true;

        public ConnectionClosed connectionCloseNotifier;

        public ConnectionsUpdated ConnectionsUpdate;
        public ClientIdentifier focus;

        public ClientIdentifier follow;

        public HistoryUpdated historyNotifier;

        public MessagesUpdated MessageNotifier;

        public List<TextMessage> messages = new List<TextMessage>(); //  new Textmessage class ?

        public Authentication.Rights serverGranted = Authentication.Rights.BANNED;

        /// <summary>
        ///     a Client
        /// </summary>
        /// <param name="client">TcpClient client</param>
        public Client(TcpClient client)
            : base(client, false)
        {
            while (!created)
            {
            }
        }

        public string name { get; set; }

        public LoginResponse loginResponse { get; set; } = LoginResponse.Unknown;

        /// <summary>
        ///     refres the messages of a client
        /// </summary>
        /// <param name="focus">ClientIndentifier focus : the client</param>
        public void refreshMessages(ClientIdentifier focus)
        {
            this.focus = focus;
            MessageNotifier.Invoke(filter());
        }

        /// <summary>
        ///     messages
        ///     Writes the recieved data to the console. depending on what kind of data it is. Does it do differnt things
        /// </summary>
        /// <param name="data">object data</param>
        public override void dataReceived(object data)
        {
            if (data is Commands)
                switch ((Commands) data)
                {
                    case Commands.AUTHENTICATIONDENIED:
                        loginResponse = LoginResponse.Denied;
                        break;
                    case Commands.AUTHENTICATIONINUSE:
                        loginResponse = LoginResponse.AccountAlreadyInUse;
                        break;
                    case Commands.AUTHENTICATIONCORRECT:
                        loginResponse = LoginResponse.Accepted;
                        break;
                    case Commands.AUTHENTICATIONRIGHTSINCORRECT:
                        loginResponse = LoginResponse.AcountBanned;
                        break;


                    case Commands.BREAKCONNECTION:
                        connectionCloseNotifier.Invoke();
                        sendData(Commands.BREAKCONNECTION);
                        break;
                }


            if (data is Message)
            {
                var message = (Message) data;
                switch (message.parameter)
                {
                    case Commands.ALLCONNECTED:
                        ConnectionsUpdate.Invoke((List<ClientIdentifier>) message.sendObject);
                        break;
                    case Commands.HISTORYSESSIONS:
                        historyNotifier.Invoke((List<HistoryStats>) message.sendObject);
                        break;
                    case Commands.AUTHENTICATIONRIGHTSINCORRECT:
                        serverGranted = ((Authentication) message.sendObject).rights;
                        break;
                }
            }


            if (data is TextMessage)
            {
                var message = (TextMessage) data;
                messages.Add(message);
                MessageNotifier.Invoke(filter());
            }
        }

        /// <summary>
        ///     filter textmessages
        /// </summary>
        /// <returns>returns a List<TextMessage> with messages of a the focuses client</returns>
        private List<TextMessage> filter()
        {
            if (focus == null) return messages;

            var filtered = new List<TextMessage>();
            foreach (var message in messages)
            {
                var add = (message.source == null) || (message.target == focus);
                if ((message.source != null) && (message.source.serverID == focus.serverID)) add = true;
                if (message.target == null) add = true;
                if (message.source is SystemIdentifier) add = true;

                if ((message.source != null) && (message.target != null))
                {
                    var targetType = focus.GetType();

                    if ((message.source.GetType() == targetType) && (message.target.GetType() == targetType) && add)
                        add = true;
                    else
                        add = false;
                }


                if (add) filtered.Add(message);
            }

            return filtered;
        }

        /// <summary>
        ///     Send data to the server
        /// </summary>
        /// <param name="data">object data</param>
        /// <returns>returns a boolean weather is was succesfull or not</returns>
        public override bool sendData(object data)
        {
            if (!connected) return false;
            base.sendData(data);
            if (data is Commands)
            {
                var command = (Commands) data;
                if (command == Commands.BREAKCONNECTION)
                {
                    connected = false;
                    close();
                }
            }
            if (data is TextMessage)
            {
                messages.Add((TextMessage) data);
                if (MessageNotifier != null)
                    MessageNotifier.Invoke(filter());
            }
            if (data is Authentication)
                name = ((Authentication) data).getEncrypted()[0];
            return true;
        }


        /// <summary>
        ///     follow a client
        /// </summary>
        /// <param name="identifier">ClientIdentifier identifier : the client</param>
        public void followIdentifieer(ClientIdentifier identifier)
        {
            if (follow == identifier) return;
            if (follow != null) unfollowIdentifieer();
            sendData(new Message(Commands.FOLLOW, identifier));
            follow = identifier;
        }

        /// <summary>
        ///     unfollow the current client being followed
        /// </summary>
        public void unfollowIdentifieer()
        {
            if (follow == null) return;
            sendData(new Message(Commands.UNFOLLOW, follow));
            follow = null;
        }
    }

    public class DoctorClient : Client
    {
        public delegate void BikeAllData(Stats stats);


        public delegate void BikeData(BikeStatEvent bikeData);

        public delegate void VRData(string[] data);

        private readonly List<ClientIdentifier> listeningTo = new List<ClientIdentifier>();


        private int notifyp = -1;
        public BikeAllData receivedAllBikeData;

        public BikeData receivedBikeData;
        public VRData receivedVRData;

        /// <summary>
        ///     a DocterClient #BlameCode
        /// </summary>
        /// <param name="client">TcpClient client</param>
        public DoctorClient(TcpClient client)
            : base(client)
        {
        }

        public int registerListener(ClientIdentifier identifier)
        {
            var amount = listeningTo.Count(arg => arg.serverID == identifier.serverID);
            if (amount == 0)
                sendData(new Message(Commands.FOLLOW, identifier));


            listeningTo.Add(identifier);
            return listeningTo.Count - 1;
        }

        public void removeListener(int index, ClientIdentifier identifier)
        {
            if ((index >= 0) && (index < listeningTo.Count))
                listeningTo.RemoveAt(index);

            var amount = listeningTo.Count(arg => arg.serverID == identifier.serverID);
            if (amount == 0)
                sendData(new Message(Commands.UNFOLLOW, identifier));
        }


        /// <summary>
        ///     Writes the recieved data to the console. depending on what kind of data it is. Does it do differnt things
        /// </summary>
        /// <param name="data">object data</param>
        public override void dataReceived(object data)
        {
            base.dataReceived(data);
            if (data is Message)
            {
                var message = (Message) data;
                switch (message.parameter)
                {
                    case Commands.GETAVERAGE:

                        break;
                    case Commands.FOLLOW:
                        lock (receivedBikeData)
                        {
                            if (message.sendObject is BikeStatEvent && (receivedBikeData != null))
                                receivedBikeData.Invoke((BikeStatEvent) message.sendObject);
                        }
                        break;
                    case Commands.GETLATEST:
                        if (message.sendObject is BikeStatEvent && (receivedBikeData != null))
                            receivedBikeData.Invoke((BikeStatEvent) message.sendObject);
                        break;

                    case Commands.GETSTATS:
                        lock (receivedAllBikeData)
                        {
                            receivedAllBikeData.Invoke((Stats) message.sendObject);
                        }
                        break;
                }
            }

            if (data is Forward)
            {
                var forward = (Forward) data;
                switch (forward.command)
                {
                    case Commands.VR_AVAILABLE:
                        receivedVRData.Invoke((string[]) forward.forwarding);
                        break;
                }
            }
        }


        /// <summary>
        ///     if already following someone unfollow that client and follow the new client specified by 'the index
        /// </summary>
        /// <param name="index">int index : the client to be followed</param>
        public void notify(int index)
        {
            if (notifyp != -1) sendData(new Message(Commands.UNFOLLOW, notifyp));

            sendData(new Message(Commands.FOLLOW, index));
            notifyp = index;
        }
    }

    public class VRevent : EventArgs
    {
    }
}