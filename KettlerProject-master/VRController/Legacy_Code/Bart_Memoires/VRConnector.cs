/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VRController
{
    public class VRConnector : NetworkConnector
    {
        public string session { get; set; } // ID OF SIM

        public string tunnelID;   //  TUNNEL ID WHEN CONNECTED TO SIM
        public string key { get; set; } = "BlameBart"; //  PASSWORD OF THE SIMULATOR




        /// <summary>
        /// CONSTRUCTOR FOR CREATING A VRConnector WITH THE DEFAULT SERVERSETTINGS
        /// </summary>
        public VRConnector() : base("84.24.41.72", 6666)
        {

        }


        /// <summary>
        /// Get sessionID (ID FOR CREATING A TUNNEL) THAT CORRESPONDS WITH A CERTAIN HOSTNAME
        /// 
        /// WARNING: FIRST ID WHEN MULTIPLE HOSTNAMES OCCURS
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private string getSessionID(string host)
        {
            sendData(ParseToJSon(new Dictionary<string, object> {{"id", "session/list"}}));
            var response = dataChecker();

            try
            {

                var id = searchData(response, "data.?.clientinfo", "host:" + host, "id");
                return id;
            }
            catch (Exception e)
            {
                
            }
            return null;

        }

        /// <summary>
        /// METHOD THAT IS EXECUTED WHEN THE VRCONNECTOR IS CONNECTED TO A SIMULATOR (AND NO ERROS OCCURED)
        /// </summary>
        public virtual void executeWhenConnected()
        {
        }


        /// <summary>
        /// METHOD FOR GETTING THE SESSION ID FOR A SIMULATOR THAT HAS THE SAME HOSTNAME AS YOUR LOCAL PC. 
        /// WARNING!  FIRST ID WHEN MULTIPLE HOSTNAMES OCCURS
        /// </summary>
        /// <returns></returns>
        private string getSessionID()
        {
            return getSessionID(Environment.MachineName);
        }


        /// <summary>
        /// METHOD FOR RETURNING ALL AVAILABLE SESSIONS WITH ITS DATA
        /// DATA: HOSTNAME, USERNAME AND ID (FOR CONNECTING TO SIMULATOR)
        /// </summary>
        /// <returns> RETURNS ITS CONTENT IN A STRING ARRAY INSIDE A ARRAY FOR RETRIEVING ALL AVAILABLE SESSIONS</returns>
        public string[][] getSessions()
        {
            sendData(ParseToJSon(new Dictionary<string, object> {{"id", "session/list"}}));
            var response = dataChecker();
            var list = new List<string> {"clientinfo.host", "clientinfo.user", "id"};
            return getAll(response, "data.?", list);
        }



        /// <summary>
        /// METHOD FOR CREATING A TUNNEL, METHOD SETS TUNNEL ID WITH CREATED TUNNEL ID
        /// </summary>
        /// <param name="session"> PARAMETER SESSION IS SESSION ID OF SESSION</param>
        /// <returns>RETURNS A BOOLEAN IF TUNNEL HAS BEEN CREATED OR ERROR OCCURED</returns>
        public bool testTunnel(string session, string key = "value")
        {
            try
            {
                createTunnel(session, key);
                return true;
            }
            catch (Exception e)
            {

                //write(e);
                return false;
            }
        }



        /// <summary>
        /// METHOD FOR CREATING A TUNNEL, METHOD SETS TUNNEL ID WITH CREATED TUNNEL ID
        /// </summary>
        /// <param name="session"> PARAMETER SESSION IS SESSION ID OF TARGETED SESSION</param>
        /// <param name="key"> PARAMTER KEY IS THE SECRET PASSWORD OF THE SESSIONTARGET</param>
        public void createTunnel(string session, string key)
        {
            var dict = new Dictionary<string, object>();
            dict.Add("id", "tunnel/create");

            var dict2 = new Dictionary<string, object>();
            dict2.Add("session", session);
            dict2.Add("key", key);

            dict.Add("data", dict2);
            sendData(ParseToJSon(dict));

            var response = dataChecker();
            var keys = new List<string>();
            keys.Add("data.status");
            keys.Add("data.id");

            var dict3 = parseDataDict(response, keys);


            string status;
            dict3.TryGetValue(keys.ElementAt(0), out status);
            write("TUNNEL CREATE STATUS : " + status );
            dict3.TryGetValue(keys.ElementAt(1), out tunnelID);
        }


        
        /// <summary>
        /// METHOD SEARCHES FOR A OBJECT,IN A JSON RESPONSE FROM THE SERVER, RETURNS THIS OBJECT
        /// OBJECT COULD BE, JARRAY, JOBJECT,JVALUE ...
        /// WARNING! MUST BE CATCHED WHEN PATH IS NOT TESTED
        /// WARNING! JVALUE CANT GET VALUE BECAUSE IT IS THE END VALUE AND DOES NOT CONTAIN ELEMTENTS (BUT VALUE)
        /// </summary>
        /// <param name="location"> FULL TEXT LOCATION WHERE TO SEARCH</param>
        /// <param name="obj"> OBJECT TO START WITH, LIKE FULL QUERY</param>
        /// <returns> RETURNS OBJECT THAT ENDED WITH (LAST LOCATION) OR WHEN OBJECT IS JVALUE</returns>
        private dynamic getObject(string location, dynamic obj)
        {
            var next = location.Split('.');
            if (next[0] == "?")
                return obj;
            if (obj is JValue || (next.Length <= 0))
                return obj;
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
                //write(forw);
                return getObject(forw, obj.GetValue(next[0]));
            }
            return obj.GetValue(next[0]);
        }



        /// <summary>
        /// METHOD FOR SEARCHING FOR A STRING IN A RESPONSE QUERY, TRANSFORSM QUERY INTO JSON OBJECT
        /// AND LOOPS UNTIL END REACHED OF WHERE (OR ?) AND CHECKS IF CHECK IS TRUE, THE GET VALUE WILL BEEN RETURNED
        /// </summary>
        /// <param name="response"> THE FULL RESPONSE QUERY RETURNED FROM THE SERVER</param>
        /// <param name="where"> THE LOCATION OF THE CHECKAREA, ? --> MEANS JARRAY WITH MULTIPLE VALUES AND CHECK</param>
        /// <param name="check"> CHECKQUERY FOR CHECKING IF CORRECT OBJECT HAS BEEN FOUND  {<VALUENAME:CORRECTVALUE>}</CORRECTVALUE></param>
        /// <param name="get"> THE VALUE YOU WANT RETURNED FROM THE FOUND OBJECT RETURNED AS STRING</param>
        /// <returns>RETURNS THE FOUND VALUE WITH CHECK AS STRING, (GET VALUE YOU REQUESTED)</returns>
        private string searchData(string response, string where, string check, string get)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(response);
            dynamic ob = getObject(where, jsonData);

            return searchObject(where.Substring(where.IndexOf("?") + 2), ob, check, get);
        }




        /// <summary>
        /// GETS ALL THE VALUES OF A JSON QUERY
        /// </summary>
        /// <param name="response"> THE FULL JSON QUERY FROM THE SERVER</param>
        /// <param name="location"> THE LOCATION FROM WHERE THE VALUES MUST BE OBTAINED --> ? MEANS START OF ARRAY</param>
        /// <param name="keys"> THE KEYS THAT MUST BE SEARCHED FOR</param>
        /// <returns>THE VALUE OF EACH KEY WILL BE RETURNED IN A ARRAY IN A ARRAY OF ALL TE JSONOBJECTS IN THE ARRAY 
        /// THAT HAS BEEN MARKED WITH ?</returns>
        private string[][] getAll(string response, string location, List<string> keys)
        {
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
        /// METHOD SEARCHES FOR A OBJECT IN A ARRAY
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
                //write(obj2);
                try
                {
                    //write(obj2.GetValue(check.Split(':')[0]).ToString().ToUpper());
                    if (obj2.GetValue(check.Split(':')[0]).ToString().ToUpper() == check.Split(':')[1].ToUpper())
                        return obj.GetValue(get).ToString();
                }
                catch (Exception e)
                {
                }
            }
            return null;
        }




        /// <summary>
        /// METHOD PARSES DATA FROM A RESPONSE FROM THE SERVER (JSONQUERY) AND SEARCHES FOR KEYS IN THE KEYSLIST
        /// AND RETURNS THIS VALUES INTO A DICTIONAIRY
        /// </summary>
        /// <param name="response">THE FULL JSONQUERY THAT THE SERVER HAS SEND</param>
        /// <param name="keys">THE KEYS THAT MUST BEEN SEARCHED FOR</param>
        /// <returns>RETURNS DICTIONAIRY WITH ALL KEYS THAT AND VALUES THAT HAS BEEN FOUND</returns>
        public Dictionary<string, string> parseDataDict(string response, List<string> keys)
        {
            var dict = new Dictionary<string, string>();
            dynamic jsonData = JsonConvert.DeserializeObject(response);
            foreach (var key in keys)
            {
                try
                {
                    dict.Add(key, getObject(key, jsonData).ToString());
                }
                catch (Exception e) { write( e.StackTrace); }
            }
            return dict;
        }



        /// <summary>
        /// METHOD FOR PARSING DATA FROM THE SERVER, IT SEARCHES THE RESPONSE FOR CERTAIN KEYS FROM THE TOGETLIST
        /// </summary>
        /// <param name="response">TE FULL JSONQUERY THAT THE SERVER HAS SEND</param>
        /// <param name="toGet">THE LIST OF KEYS THAT MUST BE SEARCHED FOR</param>
        /// <returns>A LIST OF VALUES THAT CORRESPOND THE ASKED KEYS</returns>
        public List<string> parseData(string response, List<string> toGet)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(response);
            var jsonStuff = new List<string>();
            foreach (var varia in toGet)
                jsonStuff.Add(getObject(varia, jsonData).ToString());

            return jsonStuff;
        }



        /// <summary>
        /// METHOD FOR SENDING TUNNEL DATA, AUTO ADDS DATA LIKE TUNNEL ID AND DATA FOR OPTIMALIZATION
        /// WAITS FOR ANSWER FROM THE SERVER
        /// </summary>
        /// <param name="data">DATA THAT MUST BEEN PARSED TO JSON AND SEND TO SERVER</param>
        /// <returns>RETURNS THE ANSWER FROM THE SERVER (JSON)</returns>
        public string sendTunnelData(Dictionary<string, object> data)
        {
            var toSend = new Dictionary<string, object>();
            toSend.Add("id", "tunnel/send");

            var toSend2 = new Dictionary<string, object>();
            toSend.Add("data", toSend2);
            toSend2.Add("dest", tunnelID);

            toSend2.Add("data", data);
            sendData(ParseToJSon(toSend));
            return dataChecker();
        }



        /// <summary>
        /// METHOD FOR SENDING TUNNEL DATA, AUTO ADDS DATA LIKE TUNNEL ID AND DATA FOR OPTIMALIZATION
        /// WAITS FOR ANSWER FROM THE SERVER
        /// Difference with sendTunnelData is this can send dynamic "pakkets" instead of Dictionary's
        /// </summary>
        /// <param name="pakket">dynamic pakket data that is json and can be send to the server</param>
        /// <returns>RETURNS THE ANSWER FROM THE SERVER (JSON)</returns>
        public string sendTunnelDataNotDic(object data)
        {
            dynamic totalPakkat = new
            {
                id = "tunnel/send",
                data =  new
                {
                    dest = tunnelID,
                    data 
                }  
            };
            write(JsonConvert.SerializeObject(totalPakkat));
            sendData(JsonConvert.SerializeObject(totalPakkat));
            return dataChecker();
        }

        


        /// <summary>
        /// PARSES A DICTIONARY TO A JSON QUERY
        ///
        /// </summary>
        /// <param name="keys">DICTIONAIRY THAT CONTAINS A KEY AND A OBJECT LIKE:
        /// ACCEPTED: !!!!
        /// ANOTHER DICTIONAIRY WITH A STRING AND A OBJECT
        /// A STRING,INT OR DOUBLE
        /// A LIST WITH DICTIONAIRYS, INTS OR STRINGS
        /// </param>
        /// <returns>RETURNS CREATED JSON QUERY</returns>
        public string ParseToJSon(Dictionary<string, object> keys)
        {
            var stringBuilder = new StringBuilder();
            var stringWriter = new StringWriter(stringBuilder);
            JsonWriter jsonWriter = new JsonTextWriter(stringWriter);
            jsonWriter.Formatting = Formatting.Indented;
            var index = 0;
            foreach (var key in keys.Keys)
            {
                write(key);
                jsonWriter.WritePropertyName(key);

                object value = string.Empty;
                keys.TryGetValue(key, out value);
                if (value is string)
                {
                    jsonWriter.WriteValue(value);
                }
                else if (value is int)
                {
                    jsonWriter.WriteRawValue(value.ToString());
                }
                else if (value is double)
                {
                    jsonWriter.WriteRawValue(value.ToString().Replace(',', '.'));
                }else if (value is Path)
                {
                    jsonWriter.WriteRawValue("{ " + ((Path)value).path + " }");
                }
                else if (value is Dictionary<string, object>)
                {
                    var keys2 = (Dictionary<string, object>) value;
                    var value2 = ParseToJSon(keys2);
                    jsonWriter.WriteRawValue(value2);
                }
                else if (value is List<Dictionary<string, object>>)
                {
                    jsonWriter.WriteStartArray();
                    var list = (List<Dictionary<string, object>>) value;
                    for (var i = 0; i < list.Count(); i++)
                    {
                        var dict = list.ElementAt(i);
                        jsonWriter.WriteRawValue(ParseToJSon(dict));
                    }
                    jsonWriter.WriteEndArray();
                }
                else if (value is List<string>)
                {
                    jsonWriter.WriteStartArray();
                    foreach (var val in (List<string>) value)
                        jsonWriter.WriteRawValue(val);

                    jsonWriter.WriteEndArray();
                }
                else if (value is List<int>)
                {
                    jsonWriter.WriteStartArray();
                    foreach (var val in (List<int>) value)
                        jsonWriter.WriteRawValue(val.ToString());

                    jsonWriter.WriteEndArray();
                    if (index < keys.Count() - 1)
                        jsonWriter.WriteRawValue(",");
                }
                else if (value is Array)
                {
                    jsonWriter.WriteStartArray();
                    foreach (int val in (Array) value)
                        jsonWriter.WriteRawValue(val.ToString());
                }
                index++;
            }


            var original = "{ " + stringBuilder + " }";
            return original;
        }

   
    }
}*/

