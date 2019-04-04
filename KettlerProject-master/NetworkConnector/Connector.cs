using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using KettlerReader;
using NetwerkConnector.NetwerkConnector;

namespace NetworkConnector
{
    internal class ServerConnector
    {
        private readonly TcpListener listener;

        /// <summary>
        ///     make a connection with {#BlameCode}
        /// </summary>
        /// <param name="port">int port</param>
        public ServerConnector(int port)
        {
            listener = new TcpListener(port);
            dataServer = new DataServer();
        }

        public bool connect { get; set; } = true;

        public DataServer dataServer { get; set; }

        /// <summary>
        ///     Try's to connect with a new client
        /// </summary>
        /// <param name="client">TCPClient client :  the client with who it will try to connect</param>
        public virtual void newClientconnected(TcpClient client)
        {
            Console.WriteLine("NEW CONNECTION ON SERVER");
            var server = new Server(client, dataServer);
            new Thread(server.checkForResponse).Start();
        }

        /// <summary>
        ///     Search for available servers
        /// </summary>
        public void searchForServers()
        {
            listener.Start();
            while (connect)
            {
                TcpClient client;
                if ((client = listener.AcceptTcpClient()) != null) newClientconnected(client);
            }
        }
    }

    public class Server : TCPController
    {
        public HistoryStats alreadySaved;
        public bool connected = true;
        public List<ClientIdentifier> following = new List<ClientIdentifier>();

        public List<TextMessage> messages = new List<TextMessage>();
        public bool wantsNotify;

        /// <summary>
        ///     a server
        /// </summary>
        /// <param name="client">TCPClient client</param>
        /// <param name="dataServer">DataServer dataServer</param>
        public Server(TcpClient client, DataServer dataServer)
            : base(client, true)
        {
            server = dataServer;
            notifiers = new List<int>();
            identifier = new ClientIdentifier(-1, "UNKNOWN", Authentication.Rights.UNKNOWN);
        }

        public List<HistoryStats> history { get; set; }
        public Authentication Authentication { get; set; }

        public Stats Statistics { get; set; }

        public ClientIdentifier identifier { get; set; }


        public List<int> notifiers { get; set; }

        public DataServer server { get; set; }

        public BikeStatEvent.StatTypes StatType { get; set; } = BikeStatEvent.StatTypes.CURRENT_STATISTICS;


        /// <summary>
        ///     writes all knwo data form a session to the server
        /// </summary>
        public void writeHistory()
        {
            if (Authentication.rights != Authentication.Rights.PATIENT)
                return;
            var stat = new HistoryStats(Statistics, identifier.name, messages);
            if (alreadySaved != null)
            {
                server.exception(identifier, "Overwriting history [session already saved]");
                history.Remove(alreadySaved);
            }
            alreadySaved = stat;
            history.Add(stat);
            stat.historyID = history.Count - 1;
            server.WriteSessions(history);
        }


        /// <summary>
        ///     Send data to the server
        /// </summary>
        /// <param name="data">object data</param>
        /// <returns>returns a boolean weather is was succesfull or not</returns>
        public override bool sendData(object data)
        {
            if (!connected)
            {
                server.exception(identifier, "NOT CONNECTED TO CLIENT [cant send data]");
                return false;
            }
            if (data is TextMessage)
            {
                var message = (TextMessage) data;
                messages.Add(message);
                if (message.target == null) message.target = identifier;
            }
            return base.sendData(data);
        }

        /// <summary>
        ///     Writes the recieved data to the console. depending on what kind of data it is. Does it do differnt things
        /// </summary>
        /// <param name="data">object data</param>
        public override void dataReceived(object data)
        {
            Console.WriteLine("SERVER RECEIVED " + data);
            if (data is Commands)
            {
                var command = (Commands) data;

                if (command == Commands.BREAKCONNECTION)
                {
                    server.exception(identifier, "Client wants to break connection [connection closed]");
                    connected = false;
                    writeHistory();
                    server.parseData(this, data);
                    close();
                    return;
                }
                if (command == Commands.NOTIFYONNEWCONNECTED)
                {
                    wantsNotify = !wantsNotify;
                    return;
                }
            }

            if (data is BikeStatEvent.StatTypes)
            {
                StatType = (BikeStatEvent.StatTypes) data;
                return;
            }

            if (data is TextMessage)
                messages.Add((TextMessage) data);

            if (data is Message)
            {
                var message = (Message) data;
                switch (message.parameter)
                {
                    case Commands.FOLLOW:
                        following.Add((ClientIdentifier) message.sendObject);
                        return;
                    case Commands.UNFOLLOW:
                        var client = (ClientIdentifier) message.sendObject;
                        following.RemoveAll(arg => client.serverID == arg.serverID);
                        return;
                }
            }

            if (data is Authentication)
            {
                Authentication = (Authentication) data;
                if (!server.validUser(Authentication))
                {
                    server.exception(identifier, "Client has given wrong username/password");
                    sendData(Commands.AUTHENTICATIONDENIED);

                    // breakConnection();
                    return;
                }

                if (server.inUse(Authentication))
                {
                    server.exception(identifier, "login details are already in use");
                    sendData(Commands.AUTHENTICATIONINUSE);

                    // breakConnection();
                    return;
                }

                if (Authentication.rights == Authentication.Rights.UNKNOWN)
                {
                    Authentication.rights = server.getCorrespondingRights(Authentication);
                    server.exception(identifier,
                        "Client rights don't correspond with server details: UKNOWN vs " + Authentication.rights +
                        " [savely replaced]");
                    sendData(new Message(Commands.AUTHENTICATIONRIGHTSINCORRECT, Authentication));
                }

                if ((Authentication.rights != server.getCorrespondingRights(Authentication)) ||
                    (server.getCorrespondingRights(Authentication) == Authentication.Rights.BANNED))
                {
                    server.exception(identifier,
                        "Client rights don't correspond with server details:" + Authentication.rights);
                    sendData(Commands.AUTHENTICATIONRIGHTSINCORRECT);
                    return;
                }

                identifier.rights = Authentication.rights;
                sendData(Commands.AUTHENTICATIONCORRECT);
                server.exception(identifier, "Client registered!");
                server.registerServer(this);
                return;
            }

            if (identifier.serverID != -1)
                server.parseData(this, data);
        }
    }
}