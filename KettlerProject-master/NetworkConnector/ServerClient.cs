using System.Net.Sockets;

namespace NetworkConnector
{
    public class ServerClient : Client
    {
        public delegate void Error(string error);

        public Error errorOccured;

        public ServerClient(TcpClient client) : base(client)
        {
        }

        public override void dataReceived(object data)
        {
            base.dataReceived(data);

            if (data is Message)
            {
                var message = (Message) data;
                switch (message.parameter)
                {
                    case Commands.ERROR:
                        errorOccured.Invoke(message.sendObject.ToString());
                        break;
                }
            }
        }

        public override bool sendData(object data)
        {
            return base.sendData(data);
        }
    }
}