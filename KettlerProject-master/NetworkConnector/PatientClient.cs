using System;
using System.Net.Sockets;
using System.Windows.Forms;
using KettlerReader;
using VRController;

namespace NetworkConnector
{
    public class PatientClient : Client
    {
        public delegate void EmergencyBreak();

        public BikeStatEvent last;
        public EmergencyBreak notifyAlert;
        public Timer timer;
        public Training training;

        /// <summary>
        ///     a PatientClient
        /// </summary>
        /// <param name="client">TcpClient client</param>
        /// <param name="bike">Bike bike</param>
        /// <param name="vr">VRConnector vr</param>
        /// <
        /// >
        /// #BlameCode
        public PatientClient(TcpClient client, Bike bike, VRConnector vr = null)
            : base(client)
        {
            this.bike = bike;
            bike.newStats += updateStat;
            this.vr = vr;
            timer = new Timer
            {
                Interval = 500
            };
            timer.Tick += requestDataBike;

            timer.Enabled = true;
            timer.Start();
        }

        public bool autoControl { get; set; }
        public Bike bike { get; set; }

        public VRConnector vr { get; set; }


        /// <summary>
        ///     request the bike data by sending the command "ST"
        /// </summary>
        /// <param name="sender" object sender></param>
        /// <param name="args">EventArgs args</param>
        public void requestDataBike(object sender, EventArgs args)
        {
            if (bike != null) bike.connector.sendData("ST");
        }

        /// <summary>
        ///     Writes the recieved data to the console. depending on what kind of data it is. Does it do differnt things
        /// </summary>
        /// <param name="data">object data</param>
        public override void dataReceived(object data)
        {
            base.dataReceived(data);
            bool mode;
            if (data is Commands)
                switch ((Commands) data)
                {
                    case Commands.AUTOCONTROL:
                        VRSceneHandler.ModeChanged(true);
                        break;
                    case Commands.MANUALCONTROL:
                        mode = false;
                        VRSceneHandler.ModeChanged(false);
                        break;
                }

            if (data is Forward)
            {
                var forward = (Forward) data;
                switch (forward.command)
                {
                    case Commands.BIKECOMMAND:
                        Console.WriteLine("COMMAND " + forward.forwarding);
                        bike.connector.sendData((string) forward.forwarding);
                        break;
                    case Commands.VR_AVAILABLE:
                        if (vr == null) sendData(new Forward(forward.source, forward.command, Commands.VR_NOTRUNNING));
                        else
                            sendData(
                                new Forward(forward.source, forward.command, vr.sessionsOfCurrentMachine()));
                        break;
                    case Commands.STARTTRAINING:
                        Console.WriteLine("TRAINING STARTED]");
                        sendData(new TextMessage("[TRAINING STARTED[" + forward.destination + "]", null,
                            new SystemIdentifier()));
                        training = (Training) forward.forwarding;
                        //  STARTTRAINING
                        break;
                    case Commands.STOPTRAINING:
                        Console.WriteLine("TRAINING STOPPED");
                        sendData(new TextMessage("[TRAINING STOPPED][" + forward.destination + "]", null,
                            new SystemIdentifier()));
                        //  STOPTRAINING
                        training = null;
                        break;

                    case Commands.BIKEREQUESTSTART:
                        timer.Start();
                        break;
                    case Commands.BIKEREQUESTSTOP:
                        timer.Stop();
                        break;

                    case Commands.EMERGENCYBREAK:
                        bike.connector.sendData("PW 40");
                        notifyAlert.Invoke();
                        break;
                }
            }
        }

        public override bool sendData(object data)
        {
            return base.sendData(data);
        }


        /// <summary>
        ///     update the BikeStatEvent
        /// </summary>
        /// <param name="e"></param>
        public void updateStat(BikeStatEvent e)
        {
            if ((last == null) ||
                (e.getValues()[(int) Stats.StatName.TIME] != last.getValues()[(int) Stats.StatName.TIME]))
            {
                sendData(e);
                last = e;
                if (training != null)
                {
                    var power = training.registerBikeEvent(e);
                    if (power != -1)
                        bike.connector.sendData("PW " + power);
                }
            }
        }
    }
}