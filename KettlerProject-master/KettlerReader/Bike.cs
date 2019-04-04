using System;
using System.Collections.Generic;
using System.Linq;

namespace KettlerReader
{
    /// <summary>
    ///     Object which stores the data of a connected bike.
    /// </summary>
    public class Bike
    {
        public delegate void NewStat(BikeStatEvent stat);


        /// <summary>
        ///     Delegate that is used for BikeStatEvents so you can get the data in other classes.
        /// </summary>
        public NewStat newStats;

        public Stats stats = new Stats();


        /// <summary>
        ///     Generates a connection with a connected bike
        /// </summary>
        /// <param name="connector"></param>
        public Bike(Connector connector)
        {
            this.connector = connector;
            connector.receivedHandler += addStat;
        }

        public Connector connector { get; set; }

        /// <summary>
        ///     Generates a dictionary for the stats and their values.
        ///     Adds them from the ST returned string
        /// </summary>
        /// <param name="data">String containing the data that needs to be added</param>
        /// <returns>dictionary of values and stats</returns>
        public static Dictionary<Stats.StatName, double> getValuesFromInput(string data)
        {
            var dict = new Dictionary<Stats.StatName, double>();
            var statList = data.Split('\t');
            var i = 0;
            foreach (var stat in statList)
            {
                var statName = (Stats.StatName) Enum.GetValues(typeof(Stats.StatName)).GetValue(i);
                if (statName == Stats.StatName.TIME)
                {
                    double time = int.Parse(stat.Split(':')[0])*60 + int.Parse(stat.Split(':')[1]);
                    dict.Add(statName, time);
                }
                else
                {
                    dict.Add(statName, int.Parse(stat));
                }

                i++;
            }

            return dict;
        }

        // returns current stats
        public Stats getStats()
        {
            return stats;
        }

        /// <summary>
        ///     Stores data from the bike in the stats dictionary
        /// </summary>
        /// <param name="sender"> object that send the data</param>
        /// <param name="e">command that was send</param>
        private void addStat(string data)
        {
            // switch to process return statements.
            if (data.Contains("ACK") || data.Contains("RUN")
                || data.Contains("ERROR")) return;

            var statE = new BikeStatEvent(BikeStatEvent.StatTypes.CURRENT_STATISTICS);
            var dict = getValuesFromInput(data);
            foreach (var name in dict.Keys)
            {
                double value = 0;
                dict.TryGetValue(name, out value);
                stats.addResult(name, value);

                statE.values[(int) name] = value;
            }

            newStats.Invoke(statE);
        }
    }

    /// <summary>
    ///     Stats containing essential data from the bike
    /// </summary>
    [Serializable]
    public class Stats
    {
        /// <summary>
        ///     Enum for different values stats that can be set for a bike
        /// </summary>
        [Serializable]
        public enum StatName
        {
            HEARTBEAT,

            RPM,

            SPEED,

            DISTANCE,

            PROGRAMWATTAGE,

            ENERGY,

            TIME,

            WATTAGE
        }

        public static bool[] calculateAverage = {true, true, true, false, false, false, false, true};

        //public Dictionary<StatName, List<double>> dictionary = new Dictionary<StatName, List<double>>();

        public List<double>[] dictionary = new List<double>[Enum.GetValues(typeof(StatName)).Length];

        public bool[] ignoreZero = {true, false, false, false, true, true, true, true};

        public int source;


        /// <summary>
        ///     Return the prefix
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string getPrefix(BikeStatEvent.StatTypes type, int index)
        {
            if (type != BikeStatEvent.StatTypes.COMBINED) return "";
            if (calculateAverage[index])
                return " (avg)";
            return " (total)";
        }

        /// <summary>
        ///     Adds a result of BikeStats
        /// </summary>
        /// <param name="bikeEvent"></param>
        public void addResult(BikeStatEvent bikeEvent)
        {
            foreach (StatName name in Enum.GetValues(typeof(StatName))) addResult(name, bikeEvent.values[(int) name]);
        }

        /// <summary>
        ///     Adds a result of BikeStats
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void addResult(StatName name, double value)
        {
            if (dictionary[(int) name] == null)
                dictionary[(int) name] = new List<double>();

            var list = dictionary[(int) name];


            /*if (this.dictionary.ContainsKey(name))
            {
                this.dictionary.TryGetValue(name, out list);
                this.dictionary.Remove(name);
            }*/

            list.Add(value);
            dictionary[(int) name] = list;
        }

        /// <summary>
        ///     get for every stat the average value
        /// </summary>
        /// <returns> Returns a list<double> with every double being the avrage of a stat</returns>
        public List<double> allAverage()
        {
            Enum.GetValues(typeof(StatName));

            var list = new List<double>();

            foreach (StatName naam in Enum.GetValues(typeof(StatName))) list.Add(average(naam));

            return list;
        }

        /// <summary>
        ///     Returns the average of the questioned stat
        /// </summary>
        /// <param name="name">ENUM for statname</param>
        /// <returns>double average</returns>
        public double average(StatName name)
        {
            if (!calculateAverage[(int) name]) return double.NaN;
            if ((dictionary.Length <= (int) name) || (dictionary[(int) name] == null)) return double.NaN;
            var value = dictionary[(int) name];

            if (!ignoreZero[(int) name]) return value.Average();
            var size = 0;
            double total = 0;
            foreach (var doub in value)
                if (doub != 0.0)
                {
                    total += doub;
                    size++;
                }

            return total/size;
        }

        /// <summary>
        ///     get all data from a specific stat type
        /// </summary>
        /// <param name="name"> ENUM for statname</param>
        /// <returns> returns a list<double> of all data of the specified statistic</returns>
        public List<double> getAll(StatName name)
        {
            var list = new List<double>();
            if (dictionary[(int) name] == null)
                dictionary[(int) name] = list;
            list = dictionary[(int) name];
            return list;
        }

        /// <summary>
        ///     get a mix of getAvarge() and getLatest() to get a logical to get all the right data for the right statistic. \n For
        ///     example ""heartbeat" will give an average value while "distance" will give the latast value
        /// </summary>
        /// <returns>returns a BikeStatEvent with if possible the avrage value and else the latest value</returns>
        public BikeStatEvent getCombined()
        {
            var bikeStatEvent = getAverage();
            bikeStatEvent.type = BikeStatEvent.StatTypes.COMBINED;
            var latest = getLatest().getValues();
            for (var i = 0; i < bikeStatEvent.getValues().Length; i++)
                if (double.IsNaN(bikeStatEvent.values[i]))
                    bikeStatEvent.values[i] = latest[i];

            bikeStatEvent.source = source;
            return bikeStatEvent;
        }

        /// <summary>
        ///     get all avrages for every statistic
        /// </summary>
        /// <returns>
        ///     returns a BikeStatEvent with all the avrages for every statistic. If there is no avrage available for a
        ///     statistic "NaN" will be returned for that specific statistic
        /// </returns>
        public BikeStatEvent getAverage()
        {
            var bikeStatEvent = new BikeStatEvent(BikeStatEvent.StatTypes.AVERAGE_STATISTICS);
            foreach (StatName naam in Enum.GetValues(typeof(StatName)))
                bikeStatEvent.values[(int) naam] = Math.Floor(average(naam));
            bikeStatEvent.source = source;
            return bikeStatEvent;
        }

        /// <summary>
        ///     get all the latest data for every stat
        /// </summary>
        /// <returns> returns a BikeStatEvent with the latest know data for every statistic</returns>
        public BikeStatEvent getLatest()
        {
            var bikeStatEvent = new BikeStatEvent(BikeStatEvent.StatTypes.CURRENT_STATISTICS);
            foreach (StatName naam in Enum.GetValues(typeof(StatName)))
            {
                double last = 0;

                if ((getAll(naam) != null) && (getAll(naam).Count > 0)) last = getAll(naam).Last();
                bikeStatEvent.values[(int) naam] = last;
            }

            return bikeStatEvent;
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var total = string.Empty;
            foreach (var values in Enum.GetValues(typeof(StatName)))
                if (dictionary[(int) values] != null)
                {
                    foreach (var value in dictionary[(int) values])
                        total += value + " ";
                    total += "\n";
                }

            return total;
        }
    }

    [Serializable]
    public class BikeStatEvent
    {
        /// <summary>
        ///     CURRENT_STATISTICS : all the latest recieved data
        ///     AVERAGE_STATISTICS : all avrage statistic. When no avrage value could or should be shown, returns NAN
        ///     COMBINED           : a combination of the above to possibilty's. when with AVERAGE_STATISTICS NAN would have been
        ///     displayed now displays the CURRENT_STATISTICS of that statistic.
        /// </summary>
        public enum StatTypes
        {
            CURRENT_STATISTICS,

            AVERAGE_STATISTICS,

            COMBINED
        }

        /// <summary>
        ///     a class with
        /// </summary>
        /// <param name="type"></param>
        public BikeStatEvent(StatTypes type)
        {
            this.type = type;
            values = new double[Enum.GetValues(typeof(Stats.StatName)).Length];
        }

        public StatTypes type { get; set; }

        public int source { get; set; }

        public double[] values { get; set; }


        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var data = string.Empty;
            foreach (var dou in values) data += dou + " ";
            return type + "|" + data;
        }

        /// <summary>
        ///     get all known values
        /// </summary>
        /// <returns> a double[] with this class know values</returns>
        public double[] getValues()
        {
            return values;
        }
    }
}