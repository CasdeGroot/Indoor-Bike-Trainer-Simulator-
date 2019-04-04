using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using KettlerReader;
using VRController;

//using Pluralsight.Crypto.UI;

namespace NetworkConnector
{
    internal class Program
    {
        public int index = 0;

        /// <summary>
        ///     the program startup
        /// </summary>
        public Program()
        {
            Starter.startServer();
            new Thread(() => Starter.createAdmin("admin", "admin")).Start();
            new Thread(() => Starter.createClient("Patient0", "1234")).Start();
            new Thread(() => Starter.createDoctor("Doctor0", "1234")).Start();
        }


        /// <summary>
        ///     The startup
        /// </summary>
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            new Program();
        }
    }

    public class Starter
    {
        public static string hosts = "127.0.0.1";
        public static int ports = 160;


        public static void startServer()
        {
            if (!CheckForInternetConnection())
            {
                MessageBox.Show("Geen internetToegang!", "Verbindingsfout!");
                return;
            }
            try
            {
                var serverConnector = new ServerConnector(160);
                new Thread(serverConnector.searchForServers).Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Kan server niet opzetten! poort al bezet?", "Fout bij opzetten");
            }
        }


        public static void createAuto(string username = null, string password = null)
        {
            var tcpclient = connectSavely();
            if (tcpclient == null) return;


            var client = new Client(tcpclient);

            new Thread(client.checkForResponse).Start();
            var values = loginWithGet(client);
            Console.WriteLine(string.Join(",", values));
            client.stopChecking();

            var serverGranted = client.serverGranted;
            client.sendData(Commands.BREAKCONNECTION);

            if (serverGranted == Authentication.Rights.PATIENT)
                createClient(values[0], values[1], signed: true);
            if (serverGranted == Authentication.Rights.ADMIN)
                createAdmin(values[0], values[1], signed: true);
            if (serverGranted == Authentication.Rights.DOCTOR)
                createDoctor(values[0], values[1], signed: true);
        }

        public static void createAdmin(string username = null, string password = null, TcpClient tcpclient = null,
            bool signed = false)
        {
            if (tcpclient == null)
                tcpclient = connectSavely();
            if (tcpclient == null) return;

            var client = new ServerClient(tcpclient);
            new Thread(client.checkForResponse).Start();

            if (signed || ((username != null) && (password != null)) || login(client))
            {
                if ((username != null) && (password != null))
                    client.sendData(new Authentication(username, password, Authentication.Rights.ADMIN));

                var GUI = new ServerClientGUI(client);
                Application.Run(GUI);
            }
        }


        /// <summary>
        ///     creata a new client with the specfied unique ID
        /// </summary>
        /// <param name="id"></param>
        public static void createClient(string username = null, string password = null, TcpClient tcpclient = null,
            bool signed = false)
        {
            if (tcpclient == null)
                tcpclient = connectSavely();

            if (tcpclient == null) return;


            var vr = connectSavely("145.48.6.10", 6666);
            if (vr == null) return;
            var connector = new VRConnector(vr);

            var bikem = new BikeManager(false, true, false);
            new Thread(bikem.construct).Start();
            while (bikem.bike == null)
            {
            }


            var client = new PatientClient(tcpclient, bikem.bike, connector);
            new Thread(client.checkForResponse).Start();

            if (signed || ((username != null) && (password != null)) || login(client))
            {
                if ((username != null) && (password != null))
                    client.sendData(new Authentication(username, password, Authentication.Rights.PATIENT));
                var GUI = new Patient_GUI(client);
                Application.Run(GUI);
            }
        }

        /// <summary>
        ///     create a new doctor with his unique index
        /// </summary>
        /// <param name="index"></param>
        public static void createDoctor(string username = null, string password = null, TcpClient tcpclient = null,
            bool signed = false)
        {
            if (tcpclient == null)
                tcpclient = connectSavely();

            if (tcpclient == null) return;

            var client = new DoctorClient(tcpclient);
            new Thread(client.checkForResponse).Start();

            if (signed || ((username != null) && (password != null)) || login(client))
            {
                if ((username != null) && (password != null))
                    client.sendData(new Authentication(username, password, Authentication.Rights.DOCTOR));

                var GUI = new Doctor_GUI(client);
                Application.Run(GUI);
            }
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static TcpClient connectSavely(string host = null, int? port = null)
        {
            if (host == null)
                host = hosts;

            if (port == null)
                port = ports;
            if (!CheckForInternetConnection())
            {
                MessageBox.Show("Geen internet toegang! ben je wel verbonden?", "verbindingsfout");
                return null;
            }

            try
            {
                return new TcpClient(host, (int) port);
            }
            catch (Exception e)
            {
                if (e is SocketException)
                    MessageBox.Show("Server is niet bereikbaar! neem contact op met de beheerder!", "Verbindingsfout");
                return null;
            }
        }

        /// <summary>
        ///     login on a client
        /// </summary>
        /// <param name="client">Client client</param>
        /// <returns>returns true if the login was succesfull</returns>
        public static bool login(Client client)
        {
            var login = new log_in(client);

            if (login.ShowDialog() != DialogResult.OK) return false;
            return true;
        }

        public static string[] loginWithGet(Client client)
        {
            var login = new log_in(client);

            if (login.ShowDialog() != DialogResult.OK) return null;
            return login.authentication.getEncrypted();
        }
    }
}