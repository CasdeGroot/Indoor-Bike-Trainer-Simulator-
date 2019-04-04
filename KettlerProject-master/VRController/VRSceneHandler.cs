using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using KettlerReader;

namespace VRController
{
    public class VRSceneHandler
    {
        public delegate void countChanged(int newCount);

        private static bool powerBasedOnHeight = true;
        public double ap;

        public Bike bike;
        private bool bikeSet;
        private string bikeUUID;
        private double bpm;
        private string cameraUUID;
        public bool changeDaytime;

        public int currentCount;
        public double distance;
        public double energy;
        public int lh;
        private string loadingPanelUUID;
        public string message1 = " ";
        public string message2 = " ";
        public string message3 = " ";
        public VRNode node;

        public countChanged notifyOnCountChanged;

        public bool notStarted;
        public double oldRpm;
        public VRPanel panel;
        private string panelUUID;
        public int percentage;
        private string phase;
        public int[] position;
        public double power;
        public int powerA = 200;
        public double rpm;
        private bool sceneLoaded;
        public double speed;

        public bool threadStop;
        public double timeD;
        private readonly VRConnector vr;
        public VRRoute VRr;
        public VRTerrain vrt;
        public VRTime VRtime;


        public VRSceneHandler(VRConnector vr, Bike bike)
        {
            sceneLoaded = false;
            this.bike = bike;
            this.vr = vr;


            newMessage = " ";
            VRr = new VRRoute(vr);
            node = new VRNode(vr);
            panel = new VRPanel(vr);
            VRtime = new VRTime(vr);
        }

        public string newMessage { get; set; }


        public void countAdded(int addition = 1)
        {
            currentCount += addition;
            notifyOnCountChanged.Invoke(currentCount);
        }

        /// <summary>
        ///     On every interval of the timer send new power data to the bike and change the bike data on the panels
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void tickingTimer()
        {
            threadStop = false;
            while (!threadStop)
            {
                Console.WriteLine("SCENE LOADED " + sceneLoaded);
                if (sceneLoaded)
                {
                    Console.WriteLine("TIMER TICK " + Process.GetCurrentProcess().Threads.Count);
                    if (changeDaytime)
                        VRtime.setTime(VRtime.currentTime + 0.01);
                    Thread.Sleep(30);

                    if (bikeSet)
                    {
                        if (false)
                        {
                            var h = 40;
                            try
                            {
                                position = node.getPosition("Bike");
                                if (position != null)
                                    h = vrt.getHeight(position[0], position[1]);

                                Thread.Sleep(30);
                            }
                            catch (Exception exp)
                            {
                                Console.WriteLine("ERROR POSITION GETTING " + exp);
                            }
                            Console.WriteLine("Bike pos " + string.Join(",", position));
                            Console.WriteLine("Bike height " + h);
                            if (h != 0)
                            {
                                if (h >= lh)
                                    powerA += 10;
                                if (h <= lh)
                                    powerA -= 10;
                                lh = h;
                                if (powerA > 0)
                                    bike.connector.sendData("PW " + powerA);
                            }
                        }
                        //bikeUUID = node.getUUID("Bike");

                        if ((bikeUUID != null) && (oldRpm != rpm))
                            node.updateSpeed(bikeUUID, rpm*0.1);
                        oldRpm = rpm;

                        var minutes = (int) timeD/60;
                        var seconds = (int) timeD%60;
                        var time = minutes.ToString("00") + ":" + seconds.ToString("00");
                        VRr.routeSpeed(cameraUUID, rpm/5);
                        Thread.Sleep(30);
                        //panelUUID = node.getUUID("BikePanel");
                        if (panelUUID != null)
                        {
                            panel.clearPanel(panelUUID);
                            panel.drawTextonPanel(panelUUID,
                                "Heartbeat                           " + bpm + " Bpm \\n" +
                                "Rotations                            " + rpm + " Rpm  \\n" +
                                "Speed                                 " + speed + " Km/h\\n" +
                                "Distance                             " + distance + " Km\\n" +
                                "Power                                 " + power + " Watt\\n" +
                                "Energy                                " + energy + " kJ \\n" +
                                "Time                                   " + time + " \\n" +
                                "Actual Power                       " + ap + " Watt"
                                , 20, 40, 20, 0, 0, 0, 1, "Segoeui");
                            if (newMessage != null)
                                try
                                {
                                    message3 = message2;
                                    message2 = message1;
                                    message1 = newMessage.Split(':')[3];
                                    if (message1.Length > 28)
                                    {
                                        message2 = message1.Substring(28);
                                        message1 = message1.Substring(0, 28);
                                        if (message2.Length > 28)
                                        {
                                            message3 = message2.Substring(28);
                                            message2 = message2.Substring(0, 28);
                                        }
                                    }
                                    newMessage = null;
                                }
                                catch (Exception ef)
                                {
                                    //Console.WriteLine(ef);
                                }

                            panel.drawTextonPanel(panelUUID, "Message: ", 80, 175, 20, 0, 0, 0, 1, "Segoeui");
                            Thread.Sleep(30);
                            panel.drawTextonPanel(panelUUID, message1 + "\\n" + message2 + "\\n" + message3, 10, 190, 20,
                                0, 0, 0, 1, "Segoeui");
                            Thread.Sleep(30);
                            panel.swapPanel(panelUUID);
                            Thread.Sleep(30);
                        }
                    }
                }
                Thread.Sleep(30);
            }
            Console.WriteLine("----- THREAD STOPPED ----");
        }


        public void resetScene()
        {
            sceneLoaded = false;
            node.resetScene();
        }

        /// <summary>
        ///     load a preset scene from a file
        /// </summary>
        /// <param name="file">string file</param>
        /// <param name="path">string path</param>
        /// <param name="totalNumber">int totalNumber (of steps to load scene)</param>
        public void loadSceneFromFile(string file, string path, int totalNumber)
        {
            var sceneFile = file;
            var allText = File.ReadAllText(sceneFile);
            var commands = allText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            vrt = null;
            //totalNumber -= 2;
            notStarted = false;
            bikeSet = false;
            changeDaytime = false;

            panel.addEmergencyPanel("LoadingPanel", new[] {0, 0, -5}, 0.5f, new[] {0, 0, 0}, new[] {10, 10},
                new[] {256, 256}, new[] {10, 10, 10, 10});
            loadingPanelUUID = node.getUUID("LoadingPanel");
            updateLoadingPanel(loadingPanelUUID, phase, 0, totalNumber);
            phase = "";

            cameraUUID = node.getUUID("Camera");
            Console.WriteLine("CAMEARUID = " + cameraUUID);


            var oldPath = path;
            foreach (var s in commands)
            {
                var command = s.Split(',');
                var random = new Random();


                //GetHeight should still be implemented.

                path = @"data\NetworkEngine\";
                switch (command[0].Trim().ToLower())
                {
                    case "terrain":
                        //if(vr.started)
                        //    vr.dataChecker(false);
                        phase = "Terrain";
                        updateLoadingPanel(loadingPanelUUID, phase, currentCount, totalNumber);
                        vrt = new VRTerrain(vr, node, int.Parse(command[1]), int.Parse(command[2]),
                            Path.Combine(oldPath, command[3].Trim()), Path.Combine(path, command[4].Trim()),
                            Path.Combine(path, command[5].Trim()), int.Parse(command[6]), int.Parse(command[7]));
                        countAdded(10);
                        break;
                    case "node":
                        phase = "Nodes";
                        updateLoadingPanel(loadingPanelUUID, phase, currentCount, totalNumber);
                        for (var i = 0; i < int.Parse(command[2]); i++)
                        {
                            //updateLoadingPanel(loadingPanelUUID, phase, currentCount, totalNumber);
                            var scale = 1.0f;
                            if (command[7].Trim() == "true")
                                scale = random.Next(80, 120)/100;
                            var x = random.Next(int.Parse(command[3]), int.Parse(command[4]));
                            var y = random.Next(int.Parse(command[5]), int.Parse(command[6]));
                            var z = vrt.getHeight(x, y);
                            node.addNode((command[1] + i).Trim(), new[] {x, z, y}, scale, new[] {0, 0, 0},
                                Path.Combine(path, command[8].Trim()), true, false, "");
                            countAdded();
                        }
                        break;
                    case "water":
                        phase = "Water";
                        updateLoadingPanel(loadingPanelUUID, phase, currentCount, totalNumber);
                        node.addWater(command[1],
                            new[]
                            {
                                int.Parse(command[2]), vrt.getHeight(int.Parse(command[2]), int.Parse(command[3])),
                                int.Parse(command[3])
                            }, float.Parse(command[4]), new[] {0, 0, 0},
                            new[] {int.Parse(command[5]), int.Parse(command[6])}, double.Parse(command[7]));
                        countAdded(2);
                        break;
                    case "bike":
                        phase = "Bike";
                        updateLoadingPanel(loadingPanelUUID, phase, currentCount, totalNumber);
                        vr.write(Path.Combine(path, command[2].Trim()));
                        node.addParentNode(command[1].Trim(), cameraUUID, new[] {0, 0, 0}, 0.01f, new[] {0, -105, 0},
                            Path.Combine(path, command[2].Trim()), true, true, command[3].Trim());

                        bikeUUID = node.getUUID("Bike");
                        Console.WriteLine("BIKE ID = " + bikeUUID);
                        node.updateSpeed(bikeUUID, 0);
                        countAdded(2);
                        break;
                    case "panel":
                        phase = "Panel";
                        updateLoadingPanel(loadingPanelUUID, phase, currentCount, totalNumber);
                        panel.addPanel("BikePanel", bikeUUID, new[] {-50, 120, 0}, 1, new[] {-45, 90, 0}, new[] {50, 50},
                            new[] {256, 256}, new[] {10, 10, 10, 10});
                        panelUUID = node.getUUID("BikePanel");
                        //node.deleteNodeUUID(loadingPanelUUID);
                        countAdded(2);
                        break;
                    case "route":
                        phase = "Route";
                        double speed = 0;
                        updateLoadingPanel(loadingPanelUUID, phase, currentCount, totalNumber);
                        if (command[1].Trim().ToLower() == "default")
                        {
                            VRr.createRoute(null);
                            VRr.createRoad();
                            speed = double.Parse(command[2]);
                        }
                        else if (command[1].Trim().ToLower() == "custom")
                        {
                            Console.WriteLine("It is in my code");
                            VRr.addRoutePoint(int.Parse(command[2]), int.Parse(command[3]), int.Parse(command[4]),
                                int.Parse(command[5]), int.Parse(command[6]), int.Parse(command[7]));
                            if (command.Length == 9)
                                if (command[8] == "true")
                                {
                                    VRr.createRoute(VRr.routeNodes);
                                    VRr.createRoad();
                                }
                        }
                        VRr.nodeFollow(cameraUUID, speed, 0.0, "XZ", new[] {0, 2, 0}, new[] {0, 0, 0});
                        node.updateSpeed(bikeUUID, speed*0.1);
                        countAdded(2);
                        break;
                    case "time":
                        phase = "Time";
                        updateLoadingPanel(loadingPanelUUID, phase, currentCount, totalNumber);
                        VRtime.setTime(double.Parse(command[2]));
                        if (command[1].Trim().ToLower() == "start")
                            changeDaytime = true;
                        countAdded(2);
                        break;
                }
            }
            if (!notStarted)
            {
                phase = "Finished";
                updateLoadingPanel(loadingPanelUUID, phase, currentCount, totalNumber);
                node.deleteNodeUUID(loadingPanelUUID);
                startUpdate();
                bike.newStats += UpdatePanel;
                bikeSet = true;
                notStarted = true;
                sceneLoaded = true;
            }
        }

        public void updateLoadingPanel(string loadingPanelUUID, string phase, int currentCount, int totalNumber)
        {
            phase = "Loading " + phase;
            percentage = (int) (currentCount/(double) totalNumber*100);
            var progress = "Part " + currentCount + " of " + totalNumber + " Percentage " + percentage + " %";
            panel.clearPanel(loadingPanelUUID);
            panel.drawTextonPanel(loadingPanelUUID, phase, 35, 35, 20, 0, 0, 0, 1, "Segoeui");
            panel.drawTextonPanel(loadingPanelUUID, progress, 35, 70, 20, 0, 0, 0, 1, "Segoeui");
            panel.swapPanel(loadingPanelUUID);
        }

        /// <summary>
        ///     create and add an emergency panel
        /// </summary>
        public void emergencyPanel()
        {
            node.resetScene();
            panel.addEmergencyPanel("EmergencyPanel", new[] {0, 0, -10}, 0.8f, new[] {0, 0, 0}, new[] {10, 10},
                new[] {256, 256}, new[] {10, 10, 10, 10});
            var emergencyUUID = node.getUUID("EmergencyPanel");
            panel.drawTextonPanel(emergencyUUID, "Dit is een noodstop.", 35, 35, 20, 0, 0, 0, 1, "Segoeui");
            panel.drawTextonPanel(emergencyUUID, "De VR mag afgezet worden.", 35, 70, 20, 0, 0, 0, 1, "Segoeui");
            panel.swapPanel(emergencyUUID);
        }

        /// <summary>
        ///     start the timer to update
        /// </summary>
        public void startUpdate()
        {
        }

        /// <summary>
        ///     stop the timer to update
        /// </summary>
        public void stopUpdate()
        {
        }


        /// <summary>
        ///     update the statistics
        /// </summary>
        /// <param name="bikeEvent">BikeStatEvent bikeEvent</param>
        public void UpdatePanel(BikeStatEvent bikeEvent)
        {
            bpm = bikeEvent.values.ElementAt((int) Stats.StatName.HEARTBEAT);
            rpm = bikeEvent.values.ElementAt((int) Stats.StatName.RPM);
            speed = bikeEvent.values.ElementAt((int) Stats.StatName.SPEED);
            distance = bikeEvent.values.ElementAt((int) Stats.StatName.DISTANCE);
            power = bikeEvent.values.ElementAt((int) Stats.StatName.PROGRAMWATTAGE);
            energy = bikeEvent.values.ElementAt((int) Stats.StatName.ENERGY);
            timeD = bikeEvent.values.ElementAt((int) Stats.StatName.TIME);
            ap = bikeEvent.values.ElementAt((int) Stats.StatName.WATTAGE);
        }

        public static void ModeChanged(bool mode)
        {
            powerBasedOnHeight = mode;
        }
    }
}