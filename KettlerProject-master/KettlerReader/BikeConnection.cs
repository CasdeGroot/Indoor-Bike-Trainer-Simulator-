using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace KettlerReader
{
    /// <summary>
    ///     The connector class it can choose between a physical bike or a simulator
    /// </summary>
    public abstract class Connector
    {
        public delegate void DataReceived(string data);

        public enum BikeModes
        {
            CD,
            CM,
            NONE
        }

        public BikeModes mode = BikeModes.NONE;

        public string[] modeNeeded = {"PW", "PD", "PE", "PT"};

        public DataReceived receivedHandler;


        /// <summary>
        ///     Closes this instance.
        /// </summary>
        public abstract void close();

        /// <summary>
        ///     Forwards data when data is set.
        /// </summary>
        /// <param name="data">The data.</param>
        public void receivedData(string data)
        {
            receivedHandler.Invoke(data);
        }

        /// <summary>
        ///     Sends data
        ///     possible values:
        ///     ST : status request
        ///     CD : computer modes countdown
        ///     CM : computer modes countup
        ///     RS : reset
        /// </summary>
        /// <param name="data">The data.</param>
        public virtual void sendData(string data)
        {
            switch (data.Substring(0, 2))
            {
                case "CD":
                    if (mode == BikeModes.CM)
                        sendData("RS");
                    mode = BikeModes.CD;
                    break;
                case "CM":
                    if (mode == BikeModes.CD)
                        sendData("RS");
                    mode = BikeModes.CM;
                    break;
                case "RS":
                    mode = BikeModes.NONE;
                    break;

                case "ST":

                    break;
                default:
                    var commandNeeded = false;
                    foreach (var needed in modeNeeded)
                        if (commandNeeded)
                            break;
                        else
                            commandNeeded = data.Contains(needed);

                    if (commandNeeded && (mode == BikeModes.NONE))
                        sendData("CM");
                    break;
            }
        }

        /// <summary>
        /// </summary>
        /// <seealso cref="System.EventArgs" />
        public class StatEvent : EventArgs
        {
            /// <summary>
            ///     Gets or sets the data.
            /// </summary>
            /// <value>
            ///     The data.
            /// </value>
            public string data { get; set; }
        }
    }

    /// <summary>
    ///     Creates a connection with a physical bike
    /// </summary>
    /// <seealso cref="KettlerReader.Connector" />
    public class BikeConnector : Connector
    {
        /// <summary>
        ///     Connects to occupied port
        /// </summary>
        /// <param name="port">string containing occupied com port</param>
        public BikeConnector(string port)
        {
            serialPort = new SerialPort(port, 9600);
            serialPort.DataReceived += DataReceivedHandler;
            serialPort.Open();
        }

        /// <summary>
        ///     checks to see if SerialPort is in use
        /// </summary>
        /// <value>
        ///     The serial port.
        /// </value>
        private SerialPort serialPort { get; }

        /// <summary>
        ///     Returns list of connected ports
        /// </summary>
        /// <returns></returns>
        public static string[] getPortNames()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        ///     Closes all connections
        /// </summary>
        public override void close()
        {
            serialPort.Close();
        }

        /// <summary>
        ///     Sends data to the serialport
        /// </summary>
        /// <param name="data">String in data which needs to be compatible with the bike</param>
        public override void sendData(string data)
        {
            base.sendData(data);
            try
            {
                serialPort.WriteLine(data);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///     Event handler which starts the event in connector when data is received.
        ///     NOT FINISHED
        /// </summary>
        /// <param name="sender">sending bike</param>
        /// <param name="e">data</param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort) sender;
            var indata = sp.ReadLine();

            // Console.Write(indata); 
            // TODO DO SOMETHING WITH THE RECEIVED DATA
            receivedData(indata);
        }
    }

    /// <summary>
    ///     Connects to virtual bike
    ///     SHOULD ONLY BE STARTED WHEN DEBUGGING [DEBUG]
    /// </summary>
    /// <seealso cref="KettlerReader.Connector" />
    public class Simulator : Connector
    {
        /// <summary>
        ///     The dictionary
        /// </summary>
        private readonly Dictionary<Stats.StatName, double> dictionary = new Dictionary<Stats.StatName, double>();

        // time in ms
        /// <summary>
        ///     The update interval
        /// </summary>
        private readonly double updateInterval = 1000;

        /// <summary>
        ///     The count
        /// </summary>
        private int count;

        /// <summary>
        ///     The focus
        /// </summary>
        private Stats.StatName focus;

        /// <summary>
        ///     The previous program update
        /// </summary>
        private double previousProgramUpdate = -1;

        /// <summary>
        ///     The RPM target
        /// </summary>
        private double rpmTarget = 40;

        /// <summary>
        ///     The target
        /// </summary>
        private double target;

        /// <summary>
        ///     Gets or sets the programmed.
        /// </summary>
        /// <value>
        ///     The programmed.
        /// </value>
        public List<Stats.StatName> programmed { get; set; } = new List<Stats.StatName>();

        /// <summary>
        ///     Check to see if bike program is finished.
        /// </summary>
        public void checkPrograms()
        {
            var current = getStat(Stats.StatName.TIME);
            if (previousProgramUpdate == -1)
            {
                if (current != 0) previousProgramUpdate = current - 1;
            }
            else if (current >= previousProgramUpdate)
            {
                var diff = current - previousProgramUpdate;
                previousProgramUpdate = current;

                foreach (Stats.StatName chosen in Enum.GetValues(typeof(Stats.StatName)))
                    if (programmed.Contains(chosen)) updateProgramData(chosen, diff);
            }
        }

        /// <summary>
        ///     Closes Connection and Simulator
        /// </summary>
        public override void close()
        {
            close();
            Environment.Exit(0);
        }

        /// <summary>
        ///     Returns current stat data as string
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string getData(Stats.StatName name)
        {
            if (name == Stats.StatName.TIME)
            {
                var min = (int) (getStat(name)%60) + string.Empty;
                if (min.Length != 2) min = "0" + min;
                return (int) (getStat(name)/60) + ":" + min;
            }

            if ((name == Stats.StatName.PROGRAMWATTAGE) || (name == Stats.StatName.WATTAGE))
                return (int) getStat(name)*4 + string.Empty;
            return (int) getStat(name) + string.Empty;
        }

        /// <summary>
        ///     Gets the most recent value from stat
        /// </summary>
        /// <param name="name">ENUM of the Stat</param>
        /// <returns>
        ///     returns the value of the stat (most recent)
        /// </returns>
        public double getStat(Stats.StatName name)
        {
            double value = 0;
            dictionary.TryGetValue(name, out value);
            return value;
        }

        /// <summary>
        ///     Sends data
        ///     possible values:
        ///     ST : status request
        ///     CD : computer modes countdown
        ///     CM : computer modes countup
        ///     RS : reset
        ///     PD : get disctance data
        ///     PE : get energy data
        ///     PW : get programwattage data
        ///     VS : get speed data
        /// </summary>
        /// <param name="data">The data.</param>
        public override void sendData(string data)
        {
            base.sendData(data);
            switch (data.Substring(0, 2))
            {
                case "ST":

                    // checkPrograms();
                    receivedData(
                        string.Format(
                            $"{getData(Stats.StatName.HEARTBEAT)}\t" + $"{getData(Stats.StatName.RPM)}\t"
                            + $"{getData(Stats.StatName.SPEED)}\t" + $"{getData(Stats.StatName.DISTANCE)}\t"
                            + $"{getData(Stats.StatName.PROGRAMWATTAGE)}\t"
                            + $"{getData(Stats.StatName.ENERGY)}\t" + $"{getData(Stats.StatName.TIME)}\t"
                            + $"{getData(Stats.StatName.WATTAGE)}"));

                    break;

                case "CD":
                    if (mode != BikeModes.CD)
                    {
                        if (count != 0) receivedData("RUN\r");
                        else count = -1;

                        mode = BikeModes.CD;
                    }
                    break;

                case "CM":
                    if (mode != BikeModes.CM)
                    {
                        if (count != 0) receivedData("RUN\r");
                        else count = 1;

                        mode = BikeModes.CM;
                    }
                    break;

                case "RS":
                    count = 0;
                    dictionary.Clear();

                    // focus = null;
                    target = 0.0;
                    receivedData("ACK\r");
                    mode = BikeModes.NONE;
                    break;

                case "ES1": // EXERCISE STATE 1
                    break;

                case "ID": // SEND ID #CANCELD#

                    // See labels
                    break;

                case "KI": // RETURN KETTLER ID #CANCELD#

                    // See labels
                    break;

                case "PD":
                    focus = Stats.StatName.DISTANCE;
                    setTarget(data);
                    receivedData("ACK\r");
                    break;

                case "PE":
                    focus = Stats.StatName.ENERGY;
                    setTarget(data);
                    receivedData("ACK\r");
                    break;

                case "PW":
                    focus = Stats.StatName.PROGRAMWATTAGE;
                    //this.setTarget(data);
                    var value = 5;
                    int.TryParse(data.Substring(3), out value);
                    setData(Stats.StatName.PROGRAMWATTAGE, value);
                    receivedData("ACK\r");
                    break;

                case "RP": // PROGRAM DATA
                    break;

                case "SP": // SET PERSON
                    break;

                case "VE": // return edition (Firmware)
                    break;

                case "VS":
                    receivedData(getData(Stats.StatName.SPEED));
                    break;
            }
        }

        /// <summary>
        ///     Write data to simulator
        /// </summary>
        /// <param name="name">stat name in ENUM</param>
        /// <param name="value">value of the stat</param>
        public void setData(Stats.StatName name, double value)
        {
            if (dictionary.ContainsKey(name)) dictionary.Remove(name);
            dictionary.Add(name, value);
        }

        /// <summary>
        ///     Sets data from string
        /// </summary>
        /// <param name="data">data</param>
        public void setTarget(string data)
        {
            var splitted = data.Substring(3);
            var value = 0;
            int.TryParse(splitted, out value);
            target = value;
            if (count == -1) setData(focus, target); // SET TARGET AS START!
        }

        /// <summary>
        ///     Changes the data on the bike according to algorithm
        /// </summary>
        /// <param name="name">Stat ENUM</param>
        /// <param name="diff">target data</param>
        private void updateProgramData(Stats.StatName name, double diff)
        {
            if (name.Equals(focus))
                if (((getStat(name) <= 0) && (count == -1))
                    || ((getStat(name) == target) && (count == 0)))
                    if (count == -1)
                    {
                        count = 1;
                        setData(name, target);
                    }

            switch (name)
            {
                case Stats.StatName.HEARTBEAT:
                    var rpm = getStat(Stats.StatName.RPM);
                    var power = getStat(Stats.StatName.WATTAGE)/400.0;
                    var calcHeart = 60
                                    + (rpm*(0.50 + new Random().NextDouble()/4.0)
                                       + 10*power*(0.5 + new Random().NextDouble()/2.0)
                                       + new Random().NextDouble()*10);
                    setData(name, calcHeart);
                    break;
                case Stats.StatName.DISTANCE:
                    if (count == 0)
                        setData(
                            Stats.StatName.DISTANCE,
                            getStat(Stats.StatName.DISTANCE)
                            + getStat(Stats.StatName.SPEED)/3.6*diff/100);
                    else
                        setData(
                            Stats.StatName.DISTANCE,
                            getStat(Stats.StatName.DISTANCE)
                            + count*(getStat(Stats.StatName.SPEED)/3.6*diff)/100);
                    break;
                case Stats.StatName.SPEED:
                    setData(Stats.StatName.SPEED, getStat(Stats.StatName.RPM)/10*3.55);
                    break;
                case Stats.StatName.RPM:
                    if (getStat(Stats.StatName.RPM) > rpmTarget + 10)
                    {
                        setData(
                            Stats.StatName.RPM,
                            getStat(Stats.StatName.RPM) - 1*(getStat(Stats.StatName.PROGRAMWATTAGE)/40));
                    }
                    else if (getStat(Stats.StatName.RPM) < rpmTarget - 10)
                    {
                        setData(
                            Stats.StatName.RPM,
                            getStat(Stats.StatName.RPM) + 1*(getStat(Stats.StatName.PROGRAMWATTAGE)/40));
                    }
                    else
                    {
                        rpmTarget = 1/getStat(Stats.StatName.PROGRAMWATTAGE)
                                    *(new Random().NextDouble()*100)*100;
                        if (rpmTarget > 160) rpmTarget = new Random().NextDouble()*150;
                    }

                    break;
                case Stats.StatName.WATTAGE:
                    var RPM = getStat(Stats.StatName.RPM);
                    var ProgramWatt = getStat(Stats.StatName.PROGRAMWATTAGE);
                    if (RPM*ProgramWatt/4/100 < 100) setData(Stats.StatName.WATTAGE, RPM*ProgramWatt/100);
                    else setData(Stats.StatName.WATTAGE, ProgramWatt);
                    break;
                case Stats.StatName.ENERGY:
                    var cwt = getStat(Stats.StatName.WATTAGE)*4;
                    if ((cwt > 0) && (cwt < 30))
                        setData(
                            Stats.StatName.ENERGY,
                            getStat(Stats.StatName.ENERGY) + updateInterval/60000*2*4.184);
                    else if ((cwt > 30) && (cwt < 50))
                        setData(
                            Stats.StatName.ENERGY,
                            getStat(Stats.StatName.ENERGY) + updateInterval/60000*5*4.184);
                    else if ((cwt > 50) && (cwt < 90))
                        setData(
                            Stats.StatName.ENERGY,
                            getStat(Stats.StatName.ENERGY) + updateInterval/60000*7*4.184);
                    else if ((cwt > 90) && (cwt < 100))
                        setData(
                            Stats.StatName.ENERGY,
                            getStat(Stats.StatName.ENERGY) + updateInterval/60000*10*4.184);
                    else if ((cwt > 100) && (cwt < 160))
                        setData(
                            Stats.StatName.ENERGY,
                            getStat(Stats.StatName.ENERGY) + updateInterval/60000*13*4.184);
                    else if ((cwt > 160) && (cwt < 200))
                        setData(
                            Stats.StatName.ENERGY,
                            getStat(Stats.StatName.ENERGY) + updateInterval/60000*16*4.184);
                    else if ((cwt > 200) && (cwt < 270))
                        setData(
                            Stats.StatName.ENERGY,
                            getStat(Stats.StatName.ENERGY) + updateInterval/60000*21*4.184);
                    else if ((cwt > 270) && (cwt < 360))
                        setData(
                            Stats.StatName.ENERGY,
                            getStat(Stats.StatName.ENERGY) + updateInterval/60000*26*4.184);
                    else if (cwt > 360)
                        setData(
                            Stats.StatName.ENERGY,
                            getStat(Stats.StatName.ENERGY) + updateInterval/60000*30*4.184);
                    break;
            }
        }
    }
}