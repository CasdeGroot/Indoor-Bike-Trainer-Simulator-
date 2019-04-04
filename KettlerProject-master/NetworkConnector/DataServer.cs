using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using KettlerReader;

namespace NetworkConnector
{
    public class DataServer
    {
        public List<Authentication> Authentications = new List<Authentication>();
        public List<int> notifyNew = new List<int>();

        public List<Server> servers = new List<Server>();

        /// <summary>
        ///     a data server
        /// </summary>
        public DataServer()
        {
            currentConnections = -1;
            //Authentications = readFromFile();
            Authentications = createTestUsers();
            //writeToFile(Authentications);   //  TODO REPLACE WHEN FINISHED
            //WriteSessions(new List<HistoryStats> {new HistoryStats {username = "Patient1" }});
        }

        private int currentConnections { get; set; }
        /**
         *Adds default users to the list 
         */

        private static List<Authentication> createTestUsers()
        {
            var users = new List<Authentication>();
            users.Add(new Authentication("Doctor0", "1234", Authentication.Rights.DOCTOR));
            users.Add(new Authentication("Doctor1", "1234", Authentication.Rights.DOCTOR));
            users.Add(new Authentication("Patient0", "1234", Authentication.Rights.PATIENT));
            users.Add(new Authentication("Patient1", "1234", Authentication.Rights.PATIENT));
            users.Add(new Authentication("Patient2", "1234", Authentication.Rights.PATIENT));
            users.Add(new Authentication("admin", "admin", Authentication.Rights.ADMIN));
            users.Add(new Authentication("Banned0", "1234", Authentication.Rights.BANNED));

            users.Add(new Authentication("Sascha Worms", "1234", Authentication.Rights.PATIENT));
            users.Add(new Authentication("Pieter", "1234", Authentication.Rights.PATIENT));
            users.Add(new Authentication("Bart Machielsen", "1234", Authentication.Rights.DOCTOR));

            return users;
        }


        public void exception(ClientIdentifier sender, string exception)
        {
            Console.WriteLine(exception);
            foreach (var server in servers)
                if (server.Authentication.rights == Authentication.Rights.ADMIN)
                    if (sender == null)
                        server.sendData(new Message(Commands.ERROR, exception));
                    else
                        server.sendData(new Message(Commands.ERROR, sender + ":" + exception));
        }

        /**
        * Writes param list of data to file users.bin
        */

        private void writeToFile(List<Authentication> users)
        {
            try
            {
                using (Stream stream = File.Open(Environment.CurrentDirectory + "users.bin", FileMode.OpenOrCreate))
                {
                    var bin = new BinaryFormatter();
                    bin.Serialize(stream, users);
                }
            }
            catch (IOException e)
            {
                exception(null, e.Message);
            }
        }

        /**
        *Returns Authentication List from Data\users.bin
        */

        private List<Authentication> readFromFile()
        {
            var users = new List<Authentication>();
            if (!File.Exists(Environment.CurrentDirectory + "users.bin"))
                return users;
            try
            {
                using (Stream stream = File.Open(Environment.CurrentDirectory + "users.bin", FileMode.Open))
                {
                    var bin = new BinaryFormatter();

                    users = (List<Authentication>) bin.Deserialize(stream);
                }
            }
            catch (IOException e)
            {
                exception(null, e.ToString());
            }
            return users;
        }


        /// <summary>
        ///     Check weather the spcified authentication is already being used somewhere else
        /// </summary>
        /// <param name="auth">Athentication auth : the authentication for wich is it needs to check</param>
        /// <returns>
        ///     returns true when the authentication is already used
        ///     returns false when the authentication is not being used
        /// </returns>
        public bool inUse(Authentication auth)
        {
            lock (servers)
            {
                foreach (var server in servers) if (server.Authentication.Equals(auth)) return true;
                return false;
            }
        }

        /// <summary>
        ///     make a new connection
        /// </summary>
        /// <returns>return the amount of current connections</returns>
        public int newConnection()
        {
            currentConnections++;
            return currentConnections;
        }


        /// <summary>
        ///     parse data
        /// </summary>
        /// <param name="sender">Server sender : the server who is asking to parse data</param>
        /// <param name="data">object data : the data to be parsed</param>
        public void parseData(Server sender, object data)
        {
            if (data == null)
            {
                exception(sender.identifier, "Trying to parse NULL data");
                return;
            }
            if (sender.Authentication.rights == Authentication.Rights.UNKNOWN)
            {
                sender.sendData(Commands.AUTHENTICATIONRIGHTS);
                exception(sender.identifier, "RIGHTS ARE UNKNOWN");
                return;
            }

            if (data is BikeStatEvent)
            {
                var bikeEvent = (BikeStatEvent) data;
                bikeEvent.source = sender.identifier.serverID;
                sender.Statistics.addResult(bikeEvent);
                var notifiers = sender.notifiers;
                foreach (var target in servers)
                    if ((target != null) && (target.Authentication.rights == Authentication.Rights.DOCTOR))
                    {
                        if (target.following.Count(arg => arg.serverID == sender.identifier.serverID) < 1)
                            return;
                        if (target.StatType == BikeStatEvent.StatTypes.AVERAGE_STATISTICS)
                        {
                            var average = sender.Statistics.getAverage();
                            target.sendData(
                                new Message(Commands.FOLLOW, average));
                        }
                        if (target.StatType == BikeStatEvent.StatTypes.COMBINED)
                        {
                            var combined = sender.Statistics.getCombined();
                            target.sendData(new Message(Commands.FOLLOW, combined));
                        }
                        target.sendData(new Message(Commands.FOLLOW, bikeEvent));
                    }
            }

            if (data is Forward)
            {
                var forward = (Forward) data;
                if (!AccesRights.checkRights(forward.command, sender.Authentication.rights))
                {
                    sender.sendData(Commands.AUTHENTICATIONRIGHTS);
                    return;
                }
                if (string.IsNullOrEmpty(forward.sourceName))
                    forward.sourceName = sender.Authentication.getEncrypted()[0];
                if (forward.source == -1) forward.source = sender.identifier.serverID;
                if ((servers.Count() < forward.destination) || (forward.destination < 0))
                {
                    exception(sender.identifier, "Wrong destination from Forward to " + forward.destination);
                    sender.sendData(new Message(forward.command, Commands.WRONGDESTINATION));
                }
                else getServerByID(forward.destination).sendData(forward);
            }

            if (data is TextMessage)
            {
                var message = (TextMessage) data;
                var targetType = Authentication.Rights.DOCTOR;
                if (sender.Authentication.rights == Authentication.Rights.DOCTOR)
                    targetType = Authentication.Rights.PATIENT;
                if (message.source == null) message.source = sender.identifier;
                if (message.target == null)
                {
                    foreach (var server in servers)
                        if ((server.identifier.serverID != sender.identifier.serverID) &&
                            (server.Authentication.rights == targetType))
                            server.sendData(message);
                }
                else
                {
                    var target = getServerByID(message.target.serverID);
                    target.sendData(message);
                }
            }

            if (data is Message)
            {
                var message = (Message) data;
                if (!AccesRights.checkRights(message.parameter, sender.Authentication.rights))
                {
                    sender.sendData(Commands.AUTHENTICATIONRIGHTS);
                    return;
                }
                switch (message.parameter)
                {
                    case Commands.GETAVERAGE:
                        sender.sendData(new Message(message.parameter, getAverage((int) message.sendObject)));
                        break;
                    case Commands.FOLLOW:
                        var server = ((ClientIdentifier) message.sendObject).serverID;
                        if ((servers.Count() < server) || (server < 0))
                            sender.sendData(new Message(message.parameter, Commands.WRONGDESTINATION));
                        else getServerByID(server).notifiers.Add(sender.identifier.serverID);
                        break;
                    case Commands.GETLATEST:
                        if ((servers.Count() < (int) message.sendObject) || ((int) message.sendObject < 0))
                        {
                            sender.sendData(new Message(message.parameter, Commands.WRONGDESTINATION));
                        }
                        else
                        {
                            if (getServerByID((int) message.sendObject).Statistics != null)
                                sender.sendData(
                                    new Message(
                                        Commands.FOLLOW,
                                        getServerByID((int) message.sendObject).Statistics.getLatest()));
                        }

                        break;
                    case Commands.UNFOLLOW:
                        var serverID = ((ClientIdentifier) message.sendObject).serverID;
                        if ((servers.Count() < serverID) || (serverID < 0))
                        {
                            sender.sendData(new Message(message.parameter, Commands.WRONGDESTINATION));
                        }
                        else
                        {
                            var index = serverID;
                            if (index >= 0) getServerByID(index).notifiers.Remove(sender.identifier.serverID);
                        }

                        break;

                    case Commands.GETSTATS:
                        var client = (ClientIdentifier) message.sendObject;
                        var target = getServerByID(client.serverID);
                        if (target == null)
                            return;
                        target.Statistics.source = client.serverID;
                        sender.sendData(new Message(Commands.GETSTATS, getServerByID(client.serverID).Statistics));
                        break;
                    case Commands.SAVESESSION:
                        var clientserver = (ClientIdentifier) message.sendObject;
                        var targetserver = getServerByID(clientserver.serverID);
                        targetserver.writeHistory();

                        break;
                    case Commands.HISTORYSESSIONS:
                        var target3 = (ClientIdentifier) message.sendObject;
                        sender.sendData(new Message(Commands.HISTORYSESSIONS, getServerByID(target3.serverID).history));
                        break;

                    case Commands.RESETPASS:
                        var auth = findAuthentication((Authentication) message.sendObject);
                        auth.setEncrypted(auth.getEncrypted()[0],
                            ((Authentication) message.sendObject).getEncrypted()[1]);
                        break;

                    case Commands.ADDUSER:
                        var authen = (Authentication) message.sendObject;
                        Authentications.Add(authen);

                        writeToFile(Authentications);

                        break;


                    case Commands.BANUSER:
                        var authentication = findAuthentication((Authentication) message.sendObject);

                        foreach (var serv in servers)
                            if (serv.Authentication.Equals(authentication))
                            {
                                serv.sendData(Commands.BREAKCONNECTION);
                                break;
                            }
                        authentication.rights = Authentication.Rights.BANNED;

                        writeToFile(Authentications);

                        break;

                    case Commands.GRANDRIGHTS:
                        var newRights = ((Authentication) message.sendObject).rights;
                        var authentication2 = findAuthentication((Authentication) message.sendObject);

                        authentication2.rights = newRights;

                        writeToFile(Authentications);

                        break;

                    case Commands.DISCONNECT:
                        var targetident = (ClientIdentifier) message.sendObject;
                        var servertarget = getServerByID(targetident.serverID);
                        servertarget.sendData(Commands.BREAKCONNECTION);
                        break;
                }
            }

            if (data is Commands)
            {
                if (!AccesRights.checkRights((Commands) data, sender.Authentication.rights))
                {
                    sender.sendData(Commands.AUTHENTICATIONRIGHTS);
                    return;
                }
                switch ((Commands) data)
                {
                    case Commands.ALLCONNECTED:
                        updateServers(sender);
                        break;


                    case Commands.BREAKCONNECTION:
                        servers.RemoveAll(arg2 => arg2.identifier.serverID == sender.identifier.serverID);
                        updateNotifyWanted();

                        break;
                    case Commands.NOTIFYONNEWCONNECTED:
                        if (notifyNew.Contains(sender.identifier.serverID))
                            notifyNew.Remove(sender.identifier.serverID);
                        else
                            notifyNew.Add(sender.identifier.serverID);
                        break;

                    case Commands.GETAUTHENTICATIONS:
                        var secured = new List<Authentication>();
                        foreach (var insecure in secured)
                            insecure.setEncrypted(insecure.getEncrypted()[0], "NOPASSWORD");

                        sender.sendData(new Message((Commands) data, Authentications));
                        break;
                }
            }
        }


        /// <summary>
        ///     get a server by it unique ID
        /// </summary>
        /// <param name="id">int ID : a server unique ID</param>
        /// <returns>returns the specified server if found else returns null</returns>
        public Server getServerByID(int id)
        {
            foreach (var server in servers)
                if (server.identifier.serverID == id)
                    return server;


            return null;
        }

        public Authentication findAuthentication(Authentication authentication)
        {
            foreach (var auth in Authentications)
                if (auth.getEncrypted()[0] == authentication.getEncrypted()[0])
                    return auth;
            return null;
        }

        /// <summary>
        ///     Update all servers
        /// </summary>
        public void updateNotifyWanted()
        {
            foreach (var server in servers)
                updateServers(server);
        }

        /// <summary>
        ///     updata a specific server
        /// </summary>
        /// <param name="sender">Server sender : the specific server to be updated</param>
        public void updateServers(Server sender)
        {
            if (!sender.wantsNotify)
                return;


            var conn = new List<ClientIdentifier>();
            foreach (var server in servers)
            {
                if (server == null) continue;
                if (server.identifier.serverID != sender.identifier.serverID)
                {
                    var id = server.identifier;
                    id.self = false;
                    conn.Add(id);
                }
                else
                {
                    if (server.identifier != null)
                    {
                        var id = server.identifier;
                        id.self = true;
                        conn.Add(id);
                    }
                }
            }
            sender.sendData(new Message(Commands.ALLCONNECTED, conn));
        }

        /// <summary>
        ///     register a new server
        /// </summary>
        /// <param name="sender"> Server sender : the server who want to be registerd</param>
        /// <returns>returns a ClientIdentifier</returns>
        public ClientIdentifier registerServer(Server sender)
        {
            if (sender.identifier.serverID == -1)
            {
                if (sender.Authentication == null) return null;
                sender.identifier.serverID = newConnection();

                var stats = new Stats();


                if (sender.Authentication.rights == Authentication.Rights.PATIENT) sender.Statistics = new Stats();
                servers.Add(sender);

                var ident = new ClientIdentifier(sender.identifier.serverID, sender.Authentication.getEncrypted()[0],
                    sender.Authentication.rights);
                sender.identifier = ident;
                updateNotifyWanted();
                loadHistory(sender);
            }
            return null;
        }

        /// <summary>
        ///     load a history into a server
        /// </summary>
        /// <param name="sender">Server sender : the server to wich the history shall be loaded into</param>
        public void loadHistory(Server sender)
        {
            sender.history = GetHistory(sender.identifier.name);
        }

        public Authentication.Rights getCorrespondingRights(Authentication auth)
        {
            foreach (var authentication in Authentications)
                if (auth.Equals(authentication))
                    return authentication.rights;
            return Authentication.Rights.UNKNOWN;
        }

        public bool validUser(Authentication auth)
        {
            foreach (var authentication in Authentications)
                if (auth.Equals(authentication))
                    return true;

            return false;
        }

        /// <summary>
        ///     Write a sesions data to a file
        /// </summary>
        /// <param name="stats">List<HistoryStats> stats :  the stats that need to be written to a file</param>
        public void WriteSessions(List<HistoryStats> stats)
        {
            if (stats.Count <= 0) return;
            var fileName = stats.ElementAt(0).username + "-sessions.statistics";
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var fileStream = File.Open(path, FileMode.OpenOrCreate);
            var serializer = new BinaryFormatter();
            serializer.Serialize(fileStream, stats);
            fileStream.Close();
        }

        /// <summary>
        ///     Get the histroy of a specified persen by his username
        /// </summary>
        /// <param name="username">string username</param>
        /// <returns>returns a list<HistoryStats> with the stats form an early sesion with the specified username</returns>
        public List<HistoryStats> GetHistory(string username)
        {
            var fileName = username + "-sessions.statistics";
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (!File.Exists(path)) return new List<HistoryStats>();
            var fileStream = File.Open(path, FileMode.Open);
            var serializer = new BinaryFormatter();
            var stats = (List<HistoryStats>) serializer.Deserialize(fileStream);
            for (var i = 0; i < stats.Count; i++)
                stats.ElementAt(i).historyID = i;
            fileStream.Close();
            return stats;
        }


        /// <summary>
        /// </summary>
        /// <param name="ID">int ID : a server unique ID</param>
        /// <returns>retuns all avrages of the specifed server. if server not found returns a Commands.WRONGDESTINATION</returns>
        private object getAverage(int ID)
        {
            if ((servers.Count() < ID) || (ID >= 0)) return getServerByID(ID).Statistics.allAverage();
            return Commands.WRONGDESTINATION;
        }
    }
}