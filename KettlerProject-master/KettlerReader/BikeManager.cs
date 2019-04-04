using System;
using System.Threading;

namespace KettlerReader
{
    public class BikeManager
    {
        public bool gui;

        public bool test;

        /// <summary>
        ///     Starts a con and handles multiple bikes
        /// </summary>
        /// <param name="test">SIMULATOR OR NO SIMULATOR</param>
        public BikeManager(bool test, bool gui = true, bool run = true)
        {
            this.test = test;
            this.gui = gui;
            if (run) construct();
        }

        public Bike bike { get; set; }

        /// <summary>
        ///     Contruct the connection between BikeManager and BikeConnector
        /// </summary>
        public void construct()
        {
            Connector con;
            if (BikeConnector.getPortNames().Length <= 0) test = true;
            if (test)
            {
                var simulator = new Simulator();
                con = simulator;
                bike = new Bike(simulator);
                if (gui)
                    ThreadPool.QueueUserWorkItem(delegate { new SIM_GUI(con).ShowDialog(); }); // thread for sim gui
            }
            else
            {
                var bikeConnector = new BikeConnector(BikeConnector.getPortNames()[0]);
                Console.WriteLine(BikeConnector.getPortNames()[0]);
                bike = new Bike(bikeConnector);
                con = bikeConnector;
            }


            if (gui)
            {
                var gui2 = new GUI_bike(bike);
                gui2.Activate();
                gui2.Visible = true;
                gui2.Show();
                gui2.SetDesktopLocation(0, 0);
                //Application.Run(gui2);
            }
        }
    }
}