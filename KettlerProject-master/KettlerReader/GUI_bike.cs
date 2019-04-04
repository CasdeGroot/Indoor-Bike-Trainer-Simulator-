using System;
using System.Windows.Forms;

namespace KettlerReader
{
    /// <summary>
    ///     GUI that displays values of connected bike/sim
    ///     also offers possibilities to send data
    /// </summary>
    public partial class GUI_bike : Form
    {
        private readonly Bike bike;

        private readonly Timer timer;

        /// <summary>
        ///     Starts a form, also a timer to update the data.
        /// </summary>
        /// <param name="bike">Connected bike</param>
        public GUI_bike(Bike bike)
        {
            InitializeComponent();
            this.bike = bike;

            bike.connector.receivedHandler += changeLabels;

            timer = new Timer {Interval = 500};
            timer.Tick += requestStatus;
            timer.Start();
        }

        /// <summary>
        ///     CHANGE LABELS WHEN DATA RECEIVED
        /// </summary>
        /// <param name="sender">bike</param>
        /// <param name="e">changed value</param>
        public void changeLabels(string data)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => changeLabels(data)));
                return;
            }
            // PARSE DATA
            // CHANGE LABELS
            // SHOW ERROR WHEN RECEIVED
            if (data == "ACK\r") return;
            if (data == "ERR\r") return;

            var dict = Bike.getValuesFromInput(data);
            foreach (var name in dict.Keys)
            {
                double value = 0;
                dict.TryGetValue(name, out value);
                switch (name)
                {
                    case Stats.StatName.HEARTBEAT:
                        heartbeatLabel.Text = "Heartbeat: " + value;
                        break;
                    case Stats.StatName.RPM:
                        rpmLabel.Text = "RPM: " + value;
                        break;
                    case Stats.StatName.SPEED:
                        speedLabel.Text = "Speed: " + value;
                        break;
                    case Stats.StatName.DISTANCE:
                        distanceLabel.Text = "Distance: " + value;
                        break;
                    case Stats.StatName.PROGRAMWATTAGE:
                        wattageLabel.Text = "Wattage: " + value;
                        break;
                    case Stats.StatName.ENERGY:
                        energyLabel.Text = "Energy: " + value;
                        break;
                    case Stats.StatName.TIME:
                        timeLabel.Text = "Time: " + value;
                        break;
                    case Stats.StatName.WATTAGE:
                        aWattageLabel.Text = "Actual Wattage: " + value;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        ///     asks the connector what it's status is by sending the command "ST"
        /// </summary>
        /// <param name="sender">button id</param>
        /// <param name="e">button value</param>
        public void requestStatus(object sender, EventArgs args)
        {
            bike.connector.sendData("ST");
        }

        private void aWattageLabel_Click(object sender, EventArgs e)
        {
        }

        /**
                 private void ProgramValue_KeyDown_1(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        ProgramSend.PerformClick();
                    }
                }*/

        /// <summary>
        ///     Handles Random button
        /// </summary>
        /// <param name="sender">button id</param>
        /// <param name="e">button value</param>
        private void buttonRandom_Click_1(object sender, EventArgs e)
        {
            /*
            String[] commands = new String[2] {"PD", "PE"};
            for (int i = 0; i < commands.Length; i++)
            {
                bike.connector.sendData(commands[i] + " " + new Random().Next(262));
            }
            */
            var random = new Random();
            bike.connector.sendData("PD " + random.Next(262));
            bike.connector.sendData("PE " + random.Next(100));
            bike.connector.sendData("PW " + random.Next(100));
        }

        /// <summary>
        ///     Handles eset command button
        ///     restores data to default values
        /// </summary>
        /// <param name="sender">button id</param>
        /// <param name="e">button value</param>
        private void buttonReset_Click_1(object sender, EventArgs e)
        {
            bike.connector.sendData("RS");
        }

        private void GUI_bike_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Handles command down button
        /// </summary>
        /// <param name="sender">button id</param>
        /// <param name="e">button value</param>
        private void PD_Click(object sender, EventArgs e)
        {
            bike.connector.sendData("CD");
        }

        /// <summary>
        ///     Forwards inserted command
        /// </summary>
        /// <param name="sender">button id</param>
        /// <param name="e">button value</param>
        private void ProgramSend_Click(object sender, EventArgs e)
        {
            var programName = ProgramName.Text;
            var value = ProgramValue.Text;
            var command = string.Empty;
            switch (programName)
            {
                case "Distance":
                    command = "PD";
                    break;
                case "Energy":
                    command = "PE";
                    break;
                case "Power":
                    command = "PW";
                    value = int.Parse(value)*4 + string.Empty;
                    break;
            }

            bike.connector.sendData(command + " " + value); // sends data to bike.
        }

        /// <summary>
        ///     Handles command up button
        /// </summary>
        /// <param name="sender">button id</param>
        /// <param name="e">button value</param>
        private void PU_Click(object sender, EventArgs e)
        {
            bike.connector.sendData("CM");
        }

        private void ProgramName_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}