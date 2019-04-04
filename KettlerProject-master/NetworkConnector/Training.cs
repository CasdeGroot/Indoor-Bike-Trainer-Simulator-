using System;
using System.Collections.Generic;
using KettlerReader;

namespace NetworkConnector
{
    [Serializable]
    public class Training
    {
        [Serializable]
        //  DELEGATE FOR NOTIFICATION WHEN ACTION HAPPEND
        //  TODO IS THE STRING HAPPENING NECESSAIRY OR CAN BEEN REPLACED WITH BETTER VALUE LIKE TRAININGCHECKRESPOND?? 
        public delegate void ActionHappened(string happening);
        
        /// <summary>
        /// Actionlistener for client/doctor to get notified when a sertain action has occured. 
        /// WARNING: NOT IMPLEMENTED!
        /// </summary>
        public ActionHappened actionListener;
        
        /// <summary>
        /// List for saving calculated averages
        /// TODO add values when calculation is needed bijv: (TrainingCheckValue) when heartbeat is > 130 calc average
        /// WARNING: NOT INITIALIZED BECAUSE NEVER USED! SHOULD BEEN INTIALIZED WITH SIZE OF EVERY STATNAMES (heartbeat,rpm,time,etc)
        /// </summary>
        public int[] averages;
       
        /// <summary>
        /// last power that has been setted
        /// (for comparing values that no double values are send but once)
        /// </summary>
        private double lastPower;

        /// <summary>
        /// power offset (the amount of power the program checks want to add or subtract)
        /// used for checking (like if rpm is lower then 30 then power + 1 (offset +1)
        /// can be changed by TrainingCheckValue with HIGHER POWER or STANDARD POWER as parameter
        /// </summary>
        public int powerOffset;

        /// <summary>
        /// List of power values that have been planned (for every second in the training)
        /// when the 4th second in the training the power will been setted as powervalues[4]
        /// so the power changes every second (when values arent the same)
        /// the length of the powervalues are setted in the constructor and is standard 60 (1 minute)
        /// </summary>
        public int[] powerValues;

        /// <summary>
        /// Constructor for creating training, generates powervalues with empty values 0 (will be rounded off to 10% in bike)
        /// WARNING: THE TRAINING LENGTH IS LIMITED SO WHEN TRAINING HAS ENDED NOTHING WILL HAPPEN AND HE WILL KEEP CHECKING EVEN IF 
        /// THE END OF THE ARRAY HAS BEEN REACHED
        /// </summary>
        /// <param name="trainingLength"> is the totallength of the training in SECONDS. after training the values will not change</param>
        public Training(int trainingLength = 60)
        {
            powerValues = new int[trainingLength];
        }

        /// <summary>
        /// List of functions that are checks
        /// A check will been executed every second and returns a bikestatevent. 
        /// may return NULL but nothing will happen in bike
        /// functions get a bikestatEvent with all the latest data from the bike
        /// This data must been checked and returns trainingcheckvalue with what the trainingprogram 
        /// must do!
        /// </summary>
        public List<Func<BikeStatEvent, TrainingCheckValue>> checks { get; set; } =
            new List<Func<BikeStatEvent, TrainingCheckValue>>();

        /// <summary>
        /// returns power at corresponding time. with offset and checks time
        /// WARNING: returns -1 when inputted time is not correct! (training ended or time is negative)
        /// </summary>
        /// <param name="time">the current time in seconds!</param>
        /// <returns></returns>
        public double getPower(int time)
        {
            if ((time >= powerValues.Length) || (time < 0))
                return -1;
            return powerValues[time] + powerOffset;
        }


        /// <summary>
        ///     first check the bikevent with the predefined checks (for calculating averages or setting power)
        ///     Then return the programmed wattage with offset (when below or above threshold)
        /// </summary>
        /// <param name="bikeEvent">Parameter is the latest bikeevent, if bikeevent == null then returns -1 (errorvalue)</param>
        /// <returns></returns>
        public double registerBikeEvent(BikeStatEvent bikeEvent)
        {
            if (bikeEvent == null) return -1;
            foreach (var check in checks)
            {
                var value = check.Invoke(bikeEvent);
                if (value != null)
                    switch (value.value)
                    {
                        case TrainingCheckValue.TypeTrainingValue.DOCTOR_MSG:
                            actionListener.Invoke(value.data + "");
                            break;
                        case TrainingCheckValue.TypeTrainingValue.AVERAGE_CALC:
                            averages[(int) value.averageIndex] += (int) value.data;
                            break;
                        case TrainingCheckValue.TypeTrainingValue.DEFAULT_POWER:
                            if (powerOffset != 0)
                                powerOffset -= 1;
                            break;
                        case TrainingCheckValue.TypeTrainingValue.HIGHER_POWER:
                            powerOffset += (int) value.data;
                            break;
                    }
            }
            var power = getPower((int) bikeEvent.values[(int) Stats.StatName.TIME]);
            if (power != lastPower)
            {
                lastPower = power;
                return power;
            }
            return -1;
        }

        /// <summary>
        /// Method for generatin points within a range with a certain stapsize
        /// you can generate points within 0-100 for every 10 steps (5 points for countdown and count up)
        /// response will be a array of points
        /// end -1 means the size of the list 
        /// </summary>
        /// <param name="start">the start int (in seconds) default 0</param>
        /// <param name="end">the end int (in seconds) default total size</param>
        /// <param name="stepsize">the amount of steps between the points (*2 because count up and down)</param>
        /// <returns></returns>
        public int[] generatePoints(int start = 0, int end = -1, int stepsize = 10)
        {
            if (end == -1)
                end = powerValues.Length;
            var curr = start;
            var points = new int[(int) ((end - start)/(double) (stepsize*2))];
            for (var i = 0; i < points.Length; i++)
            {
                curr += stepsize*2;
                points[i] = curr;
            }
            return points;
        }
        /// <summary>
        /// Method for generatin the list of power for the bike, the powerpeeks list has points that say where the power has
        /// to be the max of power. the power switches slowly to this value between the points 
        /// </summary>
        /// <param name="powerPeeks">a list with points within the range of the powerwattages. this points are the maxPower</param>
        /// <param name="maxPower">the maxpower that the simulation has (in %)</param>
        /// <param name="startvalue">the startvalue which all points get (default every point 10% of power)</param>
        /// <param name="prev">the generate start where to start generating (when first 10 (bijv) arent included in generation</param>
        public void generatePowerPeeks(int[] powerPeeks, int maxPower = 100, int startvalue = 10, int prev = 0)
        {
            var prevPeek = prev;
            for (var i = 0; i < powerValues.Length; i++)
                powerValues[i] = startvalue;


            foreach (var peek in powerPeeks)
            {
                if (powerValues.Length > peek)
                    powerValues[peek] = maxPower;
                var amount = (int) ((peek - prevPeek)/2.0);
                var amountAddition = startvalue;
                var amountToAdd = 0;
                while (amountToAdd == 0)
                {
                    amountToAdd = (int) ((maxPower - startvalue)/(double) amount);
                    if (amountToAdd == 0)
                        amount -= 1;
                }
                for (var i = peek - amount; i < peek; i++)
                {
                    powerValues[i] = amountAddition;
                    amountAddition += amountToAdd;
                }
                if (peek + amount > powerValues.Length)
                    amount = powerValues.Length - peek;
                for (var i = peek; i < peek + amount; i++)
                {
                    powerValues[i] = amountAddition;
                    amountAddition -= amountToAdd;
                }
                prevPeek = peek;
            }
        }
    }


    [Serializable]
    public class TrainingCheckValue
    {
        /// <summary>
        /// the different types of responces a trainingcheckvalue has. 
        /// identifies what the object does and is. like higher power highers the bike value with a certain offset
        /// 
        /// </summary>
        [Serializable]
        public enum TypeTrainingValue
        {
            AVERAGE_CALC,   //calculating average value : parameter --> double and (statname?)
            DOCTOR_MSG,     //for notifieing doctor : parameter --> message TODO not implemented
            HIGHER_POWER,   // lower/higher the offset : parameter --> offset (int range(-:+))
            DEFAULT_POWER   // lowers the offset with -1 (only if offset > 0) : parameter not used
        }
        /// <summary>
        /// TODO delete and replace with StatName index in TRaining (because is nicer)
        /// </summary>
        public int? averageIndex;

        /// <summary>
        /// Constructor for creating a trainingcheckvalue
        /// </summary>
        /// <param name="value">typetrainingvalue for identifieng what the message means</param>
        /// <param name="data">the used data (can be null in some cases => check typetrainingvalue is possible)</param>
        /// <param name="averageIndex"> replace?</param>
        public TrainingCheckValue(TypeTrainingValue value, object data = null, int? averageIndex = null)
        {
            this.value = value;
            this.data = data;
            this.averageIndex = averageIndex;
        }

        public TypeTrainingValue value { get; set; }

      
        public object data { get; set; }
    }
}