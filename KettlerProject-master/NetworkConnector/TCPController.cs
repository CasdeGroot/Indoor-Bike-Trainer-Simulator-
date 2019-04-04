#undef CONSOLE
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace NetwerkConnector
{
    namespace NetwerkConnector
    {
        public class TCPController
        {
            public bool check = true;

            /// <summary>
            ///     The secure
            /// </summary>
            public bool created;

            public bool sending;

            /// <summary>
            ///     The stream
            /// </summary>
            private Stream stream;

            /// <summary>
            ///     Initializes a new instance of the <see cref="TCPController" /> class.
            /// </summary>
            /// <param name="client">The client.</param>
            public TCPController(TcpClient client, bool server)
            {
                this.client = client;
                stream = new SslStream(client.GetStream(), false, VerifyClientCertificate, null);
                if (server)
                    setServer();
                else
                    setClient();
            }

            public bool closed { get; set; }

            public TcpClient client { get; set; }

            /// <summary>
            ///     write a string to the console
            /// </summary>
            /// <param name="s">string s</param>
            [Conditional("CONSOLE")]
            public static void write(string s)
            {
                Console.WriteLine(s);
            }

            /// <summary>
            ///     set the server
            /// </summary>
            public void setServer()
            {
                if (!new FileInfo(Environment.CurrentDirectory + "\\cert.p12").Exists)
                {
                    stream = client.GetStream();
                    created = true;
                    return;
                }
                try
                {
                    var cert = new X509Certificate2(Environment.CurrentDirectory + "\\cert.p12", "password");
                    ((SslStream) stream).AuthenticateAsServer(
                        cert,
                        false,
                        SslProtocols.Tls,
                        false);
                }
                catch (Exception e)
                {
                    write(e.ToString());
                }
                created = true;
            }

            /// <summary>
            ///     set the client
            /// </summary>
            public void setClient()
            {
                if (!new FileInfo(Environment.CurrentDirectory + "\\cert.p12").Exists)
                {
                    stream = client.GetStream();
                    created = true;
                }
                else
                {
                    ((SslStream) stream).AuthenticateAsClient("localhost");
                    created = true;
                }
            }

            public void close()
            {
                client.Close();
                closed = true;
            }

            public void stopChecking()
            {
                check = false;
            }

            /// <summary>
            ///     Checks for response.
            /// </summary>
            public void checkForResponse()
            {
                var errorCount = 0;
                while (!created)
                {
                }
                try
                {
                    while (client.Connected && !closed && check)
                    {
                        var o = receiveData();
                        if (o != null)
                        {
                            dataReceived(o);
                            errorCount = 0;
                        }
                        else
                        {
                            if (errorCount > 3)
                                return;
                            errorCount++;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            private static bool VerifyClientCertificate(object sender, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

            /// <summary>
            ///     The received data.
            /// </summary>
            /// <param name="data">The data.</param>
            public virtual void dataReceived(object data)
            {
            }


            /// <summary>
            ///     Receives the data.
            /// </summary>
            /// <returns></returns>
            public object receiveData()
            {
                var formatter = new BinaryFormatter();
                try
                {
                    return formatter.Deserialize(stream);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    return null;
                }
            }

            /// <summary>
            ///     Sends the data.
            /// </summary>
            /// <param name="data">The data.</param>
            /// <returns></returns>
            public virtual bool sendData(object data)
            {
                while (sending)
                {
                }
                if (!data.GetType().IsSerializable) return false;
                sending = true;
                var formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(stream, data);
                    sending = false;
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
                sending = false;
            }
        }
    }
}