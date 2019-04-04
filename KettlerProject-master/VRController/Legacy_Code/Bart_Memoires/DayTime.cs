/*namespace VRController
{
    class DayTime : VRConnector
    {
        public System.Timers.Timer daytimer = new System.Timers.Timer();
        public double timeOfDay =12.0;
        public DayTime() : base()
        {

            daytimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            daytimer.Interval = 5000;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private  void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            setTime(timeOfDay);
            timeOfDay += 0.01;
        }
        public void startDay()
        {
            daytimer.Enabled = true;

        }
        public void stopDay()
        {
            daytimer.Enabled = false;

        }
        public void setTime(double setTime)
        {
            Dictionary<string, object> time = new Dictionary<string, object>();
            time.Add("id", "scene/skybox/settime");
            Dictionary<string, object> timeDict = new Dictionary<string, object>();
            timeDict.Add("time", setTime);
            time.Add("data", timeDict);
            sendTunnelData(time);
            write("Time SEND");

        }
        public void updateSkyBox()
        {
            Dictionary<string, object> time = new Dictionary<string, object>();
            time.Add("id", "scene/skybox/update");
            time.Add("data", null);
            sendTunnelData(time);
            write("update SEND");
        }
    }
}*/

