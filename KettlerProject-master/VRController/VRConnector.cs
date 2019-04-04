using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VRController
{
    public class VRConnector : NetworkConnector
    {
        public delegate void Connetected(bool value);

        public string tunnelID; // TUNNEL ID WHEN CONNECTED TO SIM

        /// <summary>
        ///     CONSTRUCTOR FOR CREATING A VRConnector WITH THE DEFAULT SERVERSETTINGS
        /// </summary>
        /// oude ip: 84.24.41.72
        public VRConnector(string host, int port)
            : base(host, port)
        {
        }


        public VRConnector(TcpClient client) : base(client)
        {
        }

        public Connetected conHandler { get; set; }
        public string key { get; set; } = "BlameBart"; // PASSWORD OF THE SIMULATOR

        public string session { get; set; } // ID OF SIM

        /// <summary>
        ///     METHOD FOR CREATING A TUNNEL, METHOD SETS TUNNEL ID WITH CREATED TUNNEL ID
        /// </summary>
        /// <param name="session"> PARAMETER SESSION IS SESSION ID OF TARGETED SESSION</param>
        /// <param name="key"> PARAMTER KEY IS THE SECRET PASSWORD OF THE SESSIONTARGET</param>
        public bool createTunnel(string session, string key)
        {
            dynamic packet = new {id = "tunnel/create", data = new {session, key}};

            string packetString = JsonConvert.SerializeObject(packet);
            sendData(packetString);

            var response = dataChecker();
            var keys = new List<string>();
            keys.Add("data.status");
            keys.Add("data.id");

            var dict3 = parseDataDict(response, keys);

            string status;
            dict3.TryGetValue(keys.ElementAt(0), out status);
            //write("TUNNEL CREATE STATUS : " + status);
            //Console.WriteLine("TUNNEL CREATE STATUS : " + status);
            dict3.TryGetValue(keys.ElementAt(1), out tunnelID);
            if (status.ToLower() == "error") return false;
            if (status.ToLower() == "ok") return true;
            return false;
        }

        /// <summary>
        ///     METHOD THAT IS EXECUTED WHEN THE VRCONNECTOR IS CONNECTED TO A SIMULATOR (AND NO ERROS OCCURED)
        /// </summary>
        public virtual void executeWhenConnected()
        {
        }

        /// <summary>
        ///     METHOD FOR RETURNING ALL AVAILABLE SESSIONS WITH ITS DATA
        ///     DATA: HOSTNAME, USERNAME AND ID (FOR CONNECTING TO SIMULATOR)
        /// </summary>
        /// <returns> RETURNS ITS CONTENT IN A STRING ARRAY INSIDE A ARRAY FOR RETRIEVING ALL AVAILABLE SESSIONS</returns>
        public string[][] getSessions()
        {
            dynamic packet = new {id = "session/list"};

            string packetString = JsonConvert.SerializeObject(packet);
            sendData(packetString);
            var response = dataChecker();
            var list = new List<string> {"clientinfo.host", "clientinfo.user", "id"};
            return getAll(response, "data.?", list);
        }

        /// <summary>
        ///     METHOD FOR PARSING DATA FROM THE SERVER, IT SEARCHES THE RESPONSE FOR CERTAIN KEYS FROM THE TOGETLIST
        /// </summary>
        /// <param name="response">TE FULL JSONQUERY THAT THE SERVER HAS SEND</param>
        /// <param name="toGet">THE LIST OF KEYS THAT MUST BE SEARCHED FOR</param>
        /// <returns>A LIST OF VALUES THAT CORRESPOND THE ASKED KEYS</returns>
        public List<string> parseData(string response, List<string> toGet)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(response);
            var jsonStuff = new List<string>();
            foreach (var varia in toGet) jsonStuff.Add(getObject(varia, jsonData).ToString());

            return jsonStuff;
        }

        /// <summary>
        ///     METHOD PARSES DATA FROM A RESPONSE FROM THE SERVER (JSONQUERY) AND SEARCHES FOR KEYS IN THE KEYSLIST
        ///     AND RETURNS THIS VALUES INTO A DICTIONAIRY
        /// </summary>
        /// <param name="response">THE FULL JSONQUERY THAT THE SERVER HAS SEND</param>
        /// <param name="keys">THE KEYS THAT MUST BEEN SEARCHED FOR</param>
        /// <returns>RETURNS DICTIONAIRY WITH ALL KEYS THAT AND VALUES THAT HAS BEEN FOUND</returns>
        public Dictionary<string, string> parseDataDict(string response, List<string> keys)
        {
            var dict = new Dictionary<string, string>();
            if (response == null) return new Dictionary<string, string>();
            dynamic jsonData = JsonConvert.DeserializeObject(response);
            if (jsonData == null) return new Dictionary<string, string>();
            foreach (var key in keys)
                try
                {
                    var addObject = getObject(key, jsonData);
                    if (addObject != null)
                        dict.Add(key, addObject.ToString());
                }
                catch (Exception e)
                {
                    write(e.StackTrace);
                }

            return dict;
        }

        /// <summary>
        ///     checks to see if the sessions is being runned of the current machine
        /// </summary>
        /// <returns>retuns a string[]</returns>
        public string[] sessionsOfCurrentMachine()
        {
            var sessions = new List<string>();
            foreach (var stri in getSessions())
                if ((stri[0].ToLower() == Environment.MachineName.ToLower())
                    && (stri[1].ToLower() == Environment.UserName.ToLower())) sessions.Add(stri[2]);
            return sessions.ToArray();
        }

        /// <summary>
        ///     METHOD FOR CREATING A TUNNEL, METHOD SETS TUNNEL ID WITH CREATED TUNNEL ID
        /// </summary>
        /// <param name="session"> PARAMETER SESSION IS SESSION ID OF SESSION</param>
        /// <returns>RETURNS A BOOLEAN IF TUNNEL HAS BEEN CREATED OR ERROR OCCURED</returns>
        public bool testTunnel(string session, string key = "value")
        {
            try
            {
                return createTunnel(session, key);
            }
            catch (Exception e)
            {
                write(e.ToString());
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        ///     GETS ALL THE VALUES OF A JSON QUERY
        /// </summary>
        /// <param name="response"> THE FULL JSON QUERY FROM THE SERVER</param>
        /// <param name="location"> THE LOCATION FROM WHERE THE VALUES MUST BE OBTAINED --> ? MEANS START OF ARRAY</param>
        /// <param name="keys"> THE KEYS THAT MUST BE SEARCHED FOR</param>
        /// <returns>
        ///     THE VALUE OF EACH KEY WILL BE RETURNED IN A ARRAY IN A ARRAY OF ALL TE JSONOBJECTS IN THE ARRAY
        ///     THAT HAS BEEN MARKED WITH ?
        /// </returns>
        public string[][] getAll(string response, string location, List<string> keys)
        {
            if (response == null)
            {
                Console.WriteLine("FUCKED UP");
                return new string[0][];
            }
            dynamic jsonData = JsonConvert.DeserializeObject(response);
            JArray array = getObject(location, jsonData);

            var strings = new string[array.Count][];
            for (var i = 0; i < array.Count; i++)
            {
                strings[i] = new string[keys.Count()];
                for (var z = 0; z < keys.Count(); z++)
                    strings[i][z] = getObject(keys.ElementAt(z), (dynamic) array.ElementAt(i)).ToString();
            }

            return strings;
        }


        /// <summary>
        ///     METHOD SEARCHES FOR A OBJECT,IN A JSON RESPONSE FROM THE SERVER, RETURNS THIS OBJECT
        ///     OBJECT COULD BE, JARRAY, JOBJECT,JVALUE ...
        ///     WARNING! MUST BE CATCHED WHEN PATH IS NOT TESTED
        ///     WARNING! JVALUE CANT GET VALUE BECAUSE IT IS THE END VALUE AND DOES NOT CONTAIN ELEMTENTS (BUT VALUE)
        /// </summary>
        /// <param name="location"> FULL TEXT LOCATION WHERE TO SEARCH</param>
        /// <param name="obj"> OBJECT TO START WITH, LIKE FULL QUERY</param>
        /// <returns> RETURNS OBJECT THAT ENDED WITH (LAST LOCATION) OR WHEN OBJECT IS JVALUE</returns>
        private dynamic getObject(string location, dynamic obj)
        {
            if (obj != null)
                try
                {
                    var next = location.Split('.');
                    if (next[0] == "?") return obj;
                    if (obj is JValue || (next.Length <= 0)) return obj;
                    if (obj is JArray)
                    {
                        if (next.Length > 1)
                        {
                            var forw = location.Substring(location.IndexOf(".") + 1);
                            var forw2 = forw.Substring(forw.IndexOf(".") + 1);
                            var number = forw.Split('.')[0];
                            try
                            {
                                var num = int.Parse(number);
                                return getObject(forw2, ((JArray) obj)[num]);
                            }
                            catch (Exception e)
                            {
                            }

                            return getObject(forw2, ((JArray) obj)[0]);
                        }

                        return obj;
                    }

                    if (next.Length > 1)
                    {
                        var forw = location.Substring(location.IndexOf(".") + 1);

                        // write(forw);
                        try
                        {
                            return getObject(forw, obj.GetValue(next[0]));
                        }
                        catch (Exception e)
                        {
                            write("Node doesnt exist");
                            return null;
                        }
                    }

                    return obj.GetValue(next[0]);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"ERROR WITH PARSING DATA FROM VR RESPONSE!" +
                                            $"\n current location = {location}" +
                                            $"\n current dynamic = {obj}");
                    return null;
                }
            return null;
        }

        /// <summary>
        ///     Get sessionID (ID FOR CREATING A TUNNEL) THAT CORRESPONDS WITH A CERTAIN HOSTNAME
        ///     WARNING: FIRST ID WHEN MULTIPLE HOSTNAMES OCCURS
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private string getSessionID(string host)
        {
            dynamic packet = new {id = "session/list"};

            string packetString = JsonConvert.SerializeObject(packet);
            sendData(packetString);

            var response = dataChecker();

            try
            {
                var id = searchData(response, "data.?.clientinfo", "host:" + host, "id");
                return id;
            }
            catch (Exception e)
            {
                write(e.ToString());
            }

            return null;
        }

        /// <summary>
        ///     METHOD FOR GETTING THE SESSION ID FOR A SIMULATOR THAT HAS THE SAME HOSTNAME AS YOUR LOCAL PC.
        ///     WARNING!  FIRST ID WHEN MULTIPLE HOSTNAMES OCCURS
        /// </summary>
        /// <returns></returns>
        private string getSessionID()
        {
            return getSessionID(Environment.MachineName);
        }

        /// <summary>
        ///     METHOD FOR SEARCHING FOR A STRING IN A RESPONSE QUERY, TRANSFORSM QUERY INTO JSON OBJECT
        ///     AND LOOPS UNTIL END REACHED OF WHERE (OR ?) AND CHECKS IF CHECK IS TRUE, THE GET VALUE WILL BEEN RETURNED
        /// </summary>
        /// <param name="response"> THE FULL RESPONSE QUERY RETURNED FROM THE SERVER</param>
        /// <param name="where"> THE LOCATION OF THE CHECKAREA, ? --> MEANS JARRAY WITH MULTIPLE VALUES AND CHECK</param>
        /// <param name="check">
        ///     CHECKQUERY FOR CHECKING IF CORRECT OBJECT HAS BEEN FOUND  {
        ///     <VALUENAME:CORRECTVALUE>}</CORRECTVALUE>
        /// </param>
        /// <param name="get"> THE VALUE YOU WANT RETURNED FROM THE FOUND OBJECT RETURNED AS STRING</param>
        /// <returns>RETURNS THE FOUND VALUE WITH CHECK AS STRING, (GET VALUE YOU REQUESTED)</returns>
        private string searchData(string response, string where, string check, string get)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(response);
            dynamic ob = getObject(where, jsonData);

            return searchObject(where.Substring(where.IndexOf("?") + 2), ob, check, get);
        }

        /// <summary>
        ///     METHOD SEARCHES FOR A OBJECT IN A ARRAY
        /// </summary>
        /// <param name="location"> THE LOCATION WHERE TO SEARCH</param>
        /// <param name="array"> THE ARRAY THAT MUST BE SEARCHED</param>
        /// <param name="check"> THE CHECK WHICH MUST BE TRUE TO RETURN OBJECT</param>
        /// <param name="get">THE VALUE THAT MUST BE RETURNED FROM THE FOUND AND CHECKED OBJECT</param>
        /// <returns>THE VALUE THAT HAS BEEN FOUND OF THE OBJECT THAT IS CHECKED, IT IS FOUND BY THE GET STRING</returns>
        private string searchObject(string location, JArray array, string check, string get)
        {
            foreach (JObject obj in array)
            {
                dynamic obj2 = getObject(location, obj);

                // write(obj2);
                try
                {
                    // write(obj2.GetValue(check.Split(':')[0]).ToString().ToUpper());
                    if (obj2.GetValue(check.Split(':')[0]).ToString().ToUpper() == check.Split(':')[1].ToUpper())
                        return obj.GetValue(get).ToString();
                }
                catch (Exception e)
                {
                    write(e.ToString());
                }
            }

            return null;
        }
    }
}