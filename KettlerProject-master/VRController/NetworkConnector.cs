#undef CONSOLE
using System;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace VRController
{
    public class NetworkConnector
    {
        private readonly NetworkStream stream; // NETWORKSTREAM FOR SENDING DATA
        private readonly TcpClient tcpClient; // TCPCLIENT FOR SENDING DATA
        private bool connected;

        private bool connecting;
        private string set;
        public bool started = false;
        private readonly Timer timeoutTimer = new Timer();
        private bool waitingForReceive;


        /// <summary>
        ///     CONSTRUCTOR FOR MAKING A CONNECTION WITH THE SERVER AT A HOST AND PORT
        /// </summary>
        /// <param name="addres"> HOSTADDRES (IP) OF THE SERVER</param>
        /// <param name="port"> PORTNUMBER OF THE SERVER</param>
        public NetworkConnector(string addres, int port)
        {
            //          this.timeoutTimer.Elapsed += this.onTimedEvent;
            timeoutTimer.Interval = 10000;
            tcpClient = new TcpClient(addres, port);
            stream = tcpClient.GetStream();
            Application.EnableVisualStyles();
        }

        public NetworkConnector(TcpClient client)
        {
            //      this.timeoutTimer.Elapsed += this.onTimedEvent;
            timeoutTimer.Interval = 10000;
            tcpClient = client;
            stream = tcpClient.GetStream();
            Application.EnableVisualStyles();
        }

        //[Conditional("CONSOLE")]
        public void write(string s)
        {
            Console.WriteLine(s);
        }


        /// <summary>
        ///     Timer that closes the connection when the VR Server sends no response whitin 10 seconds.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void onTimedEvent(object source, ElapsedEventArgs e)
        {
            timeoutTimer.Close();
            timeoutTimer.Dispose();
            timeoutTimer.Enabled = false;
            if ((set == null) && (connected == false))
            {
                stream.Close();
                tcpClient.Close();
                MessageBox.Show(
                    "Couldn't connect to Virtual Reality Server. Please check your internet connection and restart the program.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                connected = true;
            }
        }

        /// <summary>
        ///     METHOD FOR WAITING FOR DATA AND RETURNS THE DATA
        /// </summary>
        /// <returns>RETURNS JSONDATA FROM THE SERVER</returns>
        public string dataChecker()
        {
            var lengtToRead = 0;
            set = null;
            if (!connecting)
                connecting = true;

            while (true)
            {
                try
                {
                    while (lengtToRead <= 0)
                    {
                        var size1 = new byte[4];
                        var size2 = stream.Read(size1, 0, 4);
                        set = size2.ToString();

                        lengtToRead = BitConverter.ToInt32(size1, 0);
                        Console.WriteLine(BitConverter.ToString(size1) + " " + lengtToRead);
                    }

                    var responseString = string.Empty;

                    var response = 0;
                    var bytes = new byte[lengtToRead];
                    while (response < lengtToRead)
                    {
                        var data = new byte[lengtToRead];
                        var currentsize = stream.Read(data, 0, lengtToRead - response);
                        //Console.WriteLine("RECEIVED PART " + (currentsize+response) + "/" + lengtToRead);
                        for (var i = 0; i < currentsize; i++)
                            bytes[i + response] = data[i];
                        response += currentsize;
                    }

                    responseString = Encoding.Default.GetString(bytes, 0, response);
                    //Console.WriteLine("RECEIVED " + responseString);

                    //Console.WriteLine("REceIVED " + responseString);
                    waitingForReceive = false;

                    if (!(responseString.Contains("button") || responseString.Contains("mouse")))
                        return responseString;
                    write("FAULTY RESPONSE");
                }
                catch (Exception e)
                {
                    if (e is OutOfMemoryException)
                        Console.WriteLine("SYSTEM IS OUT OF MEMORY!!! ERROR WITH MEMORY " + lengtToRead + " BYTES");
                }

                return null;
            }
        }

        /// <summary>
        ///     SEND DATA TO THE SERVER
        /// </summary>
        /// <param name="data"> SEND DATA TO CONNECTED SERVER DATA AS STRING (JSONQUERY)</param>
        public void sendData(string data)
        {
            while (waitingForReceive)
            {
            }
            waitingForReceive = true;
            try
            {
                var dataBytes = Encoding.Default.GetBytes(data);
                var tosend = new byte[dataBytes.Length + 4];
                var dataLength = BitConverter.GetBytes(dataBytes.Length);
                Array.Copy(dataLength, tosend, 4);
                Array.Copy(dataBytes, 0, tosend, 4, dataBytes.Length);
                stream.Write(tosend, 0, tosend.Length);
                //Console.WriteLine("Sending vr: " + data.Substring(0,10)+"...");
                //Console.WriteLine("SENDING "+ data);
            }
            catch (Exception e)
            {
                // write(e);
            }
        }
    }
}