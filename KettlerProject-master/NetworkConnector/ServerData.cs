using System;
using System.Collections.Generic;
using System.Speech.Synthesis;
using KettlerReader;

namespace NetworkConnector
{
    /// <summary>
    ///     All possible commands the server knows
    /// </summary>
    [Serializable]
    public enum Commands
    {
        GETAUTHENTICATIONS, // (ADMIN)    RETURNS MESSAGE WITH LIST OF ALL CLIENTIDENTFIERS

        DISCONNECT, // (ADMIN)            SEND AS MESSAGE WITH CLIENTIDENTIFIER AS PARAMETER

        BANUSER,
        // (ADMIN)               Bans a user from connecting & disconnects them (sets rights as BANNED) send as Message with authentication

        GRANDRIGHTS,
        // (ADMIN)           gives a user new rights (when unbanning a user) must send as message with authentication as parameter with new rights

        ADDUSER,
        // (ADMIN)             Adds a user to the registered authentication (give username and password) send as message with Authentication

        RESETPASS,
        // (ADMIN)            Resets the password from the user to a specified one send as message with authentication with new encrypted password!


        EMERGENCYBREAK,


        ALLBIKES,

        ALLCONNECTED,

        GETSTATS,

        STARTTRAINING,

        STOPTRAINING,

        BIKEREQUESTSTART,

        BIKEREQUESTSTOP,

        MANUALCONTROL,

        AUTOCONTROL,

        NOTIFYONNEWCONNECTED,

        GETAVERAGE,


        GETLATEST,

        BIKECOMMAND,

        FOLLOW,

        HISTORYSESSIONS,

        LOADHISTORYSESSION,

        BREAKCONNECTION,

        SAVESESSION,

        UNFOLLOW,

        WRONGDESTINATION,

        AUTHENTICATIONDENIED,

        AUTHENTICATIONINUSE,

        AUTHENTICATIONCORRECT,

        AUTHENTICATIONRIGHTS,

        AUTHENTICATIONRIGHTSINCORRECT,

        MESSAGE,

        VR_RUNNING,

        VR_STOP,

        VR_START,

        VR_CONNECT,

        VR_AVAILABLE,

        VR_NOTRUNNING,

        ERROR,

        TRAINING
    }

    public class AccesRights
    {
        public static Authentication.Rights[] rightsNeeded =
        {
            Authentication.Rights.ADMIN,
            Authentication.Rights.ADMIN,
            Authentication.Rights.ADMIN,
            Authentication.Rights.ADMIN,
            Authentication.Rights.ADMIN,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.PATIENT,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.PATIENT,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.DOCTOR,
            Authentication.Rights.BANNED,
            Authentication.Rights.BANNED,
            Authentication.Rights.BANNED,
            Authentication.Rights.BANNED,
            Authentication.Rights.BANNED,
            Authentication.Rights.BANNED,
            Authentication.Rights.BANNED,
            Authentication.Rights.BANNED,
            Authentication.Rights.BANNED,
            Authentication.Rights.PATIENT,
            Authentication.Rights.PATIENT,
            Authentication.Rights.PATIENT,
            Authentication.Rights.PATIENT,
            Authentication.Rights.PATIENT,
            Authentication.Rights.PATIENT,
            Authentication.Rights.PATIENT,
            Authentication.Rights.BANNED,
            Authentication.Rights.PATIENT,
            Authentication.Rights.PATIENT
        };


        public static bool checkRights(Commands toExecute, Authentication.Rights rights)
        {
            return rights <= rightsNeeded[(int) toExecute];
        }
    }

    [Serializable]
    public class Forward
    {
        /// <summary>
        ///     forward a #BlamCode
        /// </summary>
        /// <param name="destination">int destination</param>
        /// <param name="command">Commands command</param>
        /// <param name="forwarding">object forwarding</param>
        /// <param name="source">int source</param>
        /// <param name="sourceName">string sourceName</param>
        public Forward(int destination, Commands command, object forwarding, int source = -1, string sourceName = null)
        {
            this.forwarding = forwarding;
            this.destination = destination;
            this.command = command;
            this.source = source;
            this.sourceName = sourceName;
        }

        public Commands command { get; set; }

        public int destination { get; set; }

        public object forwarding { get; set; }

        public int source { get; set; }

        public string sourceName { get; set; }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return string.Format($"from {source} to {destination} | {command} --> {forwarding}");
        }
    }

    [Serializable]
    public class Message
    {
        public Message(Commands parameter, object sendObject)
        {
            this.parameter = parameter;
            this.sendObject = sendObject;
        }

        public Commands parameter { get; set; }

        public object sendObject { get; set; }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "MESSAGE: " + parameter + " | " + sendObject;
        }
    }

    [Serializable]
    public class Authentication
    {
        public enum Rights
        {
            SYSTEM,
            ADMIN,
            DOCTOR,
            PATIENT,
            UNKNOWN,
            BANNED
        }

        private string name;

        private string password;


        public Authentication(string name, string password, Rights type)
        {
            setEncrypted(name, password);
            rights = type;
        }

        public Rights rights { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Authentication)) return false;
            var a = (Authentication) obj;
            return (a.getEncrypted()[0] == getEncrypted()[0]) && (a.getEncrypted()[1] == getEncrypted()[1]);
        }

        /// <summary>
        ///     get an encrypted aythentication
        /// </summary>
        /// <returns>returns the encrypted authentication</returns>
        public string[] getEncrypted()
        {
            return new[] {StringCipher.Decrypt(name), StringCipher.Decrypt(password)};
        }

        /// <summary>
        ///     set an encrypted authentication
        /// </summary>
        /// <param name="name">string name</param>
        /// <param name="password">string password</param>
        public void setEncrypted(string name, string password)
        {
            this.name = StringCipher.Encrypt(name);
            this.password = StringCipher.Encrypt(password);
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "AUTHENTICATION:" + getEncrypted()[0] + "," + getEncrypted()[1];
        }
    }

    [Serializable]
    public class ClientIdentifier
    {
        public ClientIdentifier(int severId, string name, Authentication.Rights rights)
        {
            serverID = severId;
            this.name = name;
            this.rights = rights;
        }

        public int serverID { get; set; }
        public string name { get; set; }

        public Authentication.Rights rights { get; set; }


        public bool self { get; set; } = false;

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            if (serverID == -1) return name;
            return serverID + ":" + name;
        }
    }

    [Serializable]
    public class SystemIdentifier : ClientIdentifier
    {
        public SystemIdentifier() : base(-1, null, Authentication.Rights.SYSTEM)
        {
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "System";
        }
    }

    [Serializable]
    public class TextMessage
    {
        public DateTime messageTime;

        /// <summary>
        ///     a textmessage
        /// </summary>
        /// <param name="text">string text : the real message the Textmessage contains</param>
        /// <param name="target">ClientIndentifier target : the desetination of the TextMessage</param>
        /// <param name="source">ClientIndentifier source : the object that created the TextMessage</param>
        public TextMessage(string text, ClientIdentifier target, ClientIdentifier source)
        {
            this.text = text;
            this.target = target;
            this.source = source;
            messageTime = DateTime.Now;
        }

        public string text { get; set; }
        public ClientIdentifier target { get; set; }
        public ClientIdentifier source { get; set; }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            if (source == null) return $"{messageTime.ToLongTimeString()} you: {text}";
            return $"{messageTime.ToLongTimeString()} {source.name}: {text}";
        }


        public void toSpeech()
        {
            var speaker = new SpeechSynthesizer();
            speaker.SpeakAsync(text);
        }
    }

    [Serializable]
    public class HistoryStats : Stats
    {
        private Stats statistics;

        /// <summary>
        ///     Statistics of an already happends session
        /// </summary>
        /// <param name="statistics">Stats statistics</param>
        /// <param name="username">string username</param>
        /// <param name="messages">List<TextMessage> messages</param>
        public HistoryStats(Stats statistics, string username, List<TextMessage> messages)
        {
            sessionDate = DateTime.Now;
            dictionary = statistics.dictionary;
            this.username = username;
            this.messages =
                messages.FindAll(message => (message.target.name == username) || (message.source.name == username));
        }

        public DateTime sessionDate { get; set; }
        public string username { get; set; }

        public List<TextMessage> messages { get; set; } = new List<TextMessage>();

        public int historyID { get; set; }

        /// <summary>
        ///     Returns a string that represents the current object.
        ///     this toString() returns a historyID and sessionData
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "(" + historyID + ") " + sessionDate;
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        ///     this toString() returns a username and historyID
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public string ToString2()
        {
            return $"[{username}({historyID})]";
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public string ToString3()
        {
            return $"[{username} <history{historyID}>]";
        }
    }

    public class HistoryIdentifier : ClientIdentifier
    {
        public HistoryStats HistoryStats;

        public HistoryIdentifier(int severId, string name, Authentication.Rights type) : base(severId, name, type)
        {
        }
    }
}