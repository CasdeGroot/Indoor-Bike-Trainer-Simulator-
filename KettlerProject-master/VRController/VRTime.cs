using Newtonsoft.Json;

namespace VRController
{
    public class VRTime
    {
        private readonly VRConnector vr;

        public double currentTime;

        /// <summary>
        ///     Initializes a new instance of the <see cref="VRTime" /> class.
        /// </summary>
        /// <param name="vr">The vr</param>
        public VRTime(VRConnector vr)
        {
            this.vr = vr;
        }

        /// <summary>
        ///     Set the time in the VR world
        /// </summary>
        /// <param name="setTime">double setTime : value[0-24] > exmple 12.5 = 12:30</param>
        public void setTime(double setTime)
        {
            currentTime = setTime;
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data =
                    new
                    {
                        dest = vr.tunnelID,
                        data = new {id = "scene/skybox/settime", data = new {time = setTime}}
                    }
                };

            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }
    }
}