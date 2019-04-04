using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KettlerReader;

namespace NetworkConnector
{
    public partial class StatisticsGUI : Form
    {
        private readonly DoctorClient client;
        private BikeStatEvent.StatTypes current = BikeStatEvent.StatTypes.COMBINED;
        private double heartbeatAverageValue;
        private readonly object identifier;
        private readonly int registeredIndex = -1;
        private readonly int selectedFilter;


        public StatisticsGUI(DoctorClient client, object identifier)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            this.client = client;
            InitializeComponent();
            this.identifier = identifier;
            FormClosing += onCloseGUI;
            linkLabel1_LinkClicked(null, null);
            client.receivedAllBikeData += updateAllPatientData;
            loadIdentifier(identifier);
            //this.Text = identifier.ToString();
            label1.Text = $"STATISTICS {identifier}";
            if (identifier is ClientIdentifier)
            {
                var ident = (ClientIdentifier) identifier;
                selectedFilter = ident.serverID;
                client.receivedBikeData += updateGraphPatientData;
                client.receivedBikeData += updateLabels;

                registeredIndex = client.registerListener(ident);
                statTabs.TabPages.RemoveAt(2);
            }
            if (identifier is HistoryIdentifier)
                selectedFilter = ((HistoryIdentifier) identifier).HistoryStats.historyID;
            if (identifier is HistoryStats)
            {
                selectedFilter = ((HistoryStats) identifier).historyID;
                messages.Items.AddRange(((HistoryStats) identifier).messages.ToArray());
            }
        }


        private void onCloseGUI(object sender, EventArgs args)
        {
            if ((registeredIndex != -1) && identifier is ClientIdentifier)
                client.removeListener(registeredIndex, (ClientIdentifier) identifier);
        }

        private void StatisticsGUI_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     add new data to a following patient
        /// </summary>
        private void updateGraphPatientData(BikeStatEvent bikeEvent)
        {
            if ((bikeEvent.type != BikeStatEvent.StatTypes.CURRENT_STATISTICS) || (bikeEvent.source != selectedFilter))
                return;
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { updateGraphPatientData(bikeEvent); }));
            }
            else
            {
                graphPatientData.Series["Heartbeat"].Points.Add(bikeEvent.values[(int) Stats.StatName.HEARTBEAT]);
                graphPatientData.Series["RPM"].Points.Add(bikeEvent.values[(int) Stats.StatName.RPM]);
                graphPatientData.Series["Actual power"].Points.Add(bikeEvent.values[(int) Stats.StatName.WATTAGE]);
                graphPatientData.Series["HeartBeatAverage"].Points.Add(heartbeatAverageValue);
                updateGraphScrollBar();
            }
            updateGraphAvrageHeartBeatLine();
        }

        /// <summary>
        ///     switch between patient's current or history data in the graph
        /// </summary>
        public void updateAllPatientData(Stats stats)
        {
            if (graphPatientData.InvokeRequired)
                graphPatientData.Invoke(new MethodInvoker(delegate { updateAllPatientData(stats); }));
            else
            {
                if (stats.source != selectedFilter)
                    return;
                graphPatientData.Series["Heartbeat"].Points.Clear();
                graphPatientData.Series["RPM"].Points.Clear();
                graphPatientData.Series["Actual power"].Points.Clear();

                // fill gpaph with new data
                var templist = stats.dictionary[(int) Stats.StatName.HEARTBEAT];
                for (var i = 0; i < templist.Count(); i++)
                    graphPatientData.Series["Heartbeat"].Points.Add(templist[i]);

                // fill gpaph with new data
                templist = stats.dictionary[(int) Stats.StatName.RPM];
                for (var i = 0; i < templist.Count(); i++)
                    graphPatientData.Series["RPM"].Points.Add(templist[i]);
                // fill gpaph with new data
                templist = stats.dictionary[(int) Stats.StatName.PROGRAMWATTAGE];
                for (var i = 0; i < templist.Count(); i++)
                    graphPatientData.Series["Actual power"].Points.Add(templist[i]);

                updateGraphScrollBar(true);
                updateGraphAvrageHeartBeatLine();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="scrollBarToEnd">boolean scrollBarToEnd : default false; if the scroll bar should scroll to the end</param>
        private void updateGraphScrollBar(bool scrollBarToEnd = false)
        {
            if (graphPatientData.InvokeRequired)
                graphPatientData.Invoke(new MethodInvoker(delegate { updateGraphScrollBar(scrollBarToEnd); }));
            else
            {
                if (graphPatientData.Series["Heartbeat"].Points.Count() > 60)
                {
                    graphPatientData.ChartAreas[0].AxisX.Maximum = graphPatientData.Series["Heartbeat"].Points.Count();
                    var position = graphPatientData.Series["Heartbeat"].Points.Count() - 60;
                    if ((graphPatientData.ChartAreas[0].AxisX.ScaleView.Position > position - 10) && !scrollBarToEnd)
                        graphPatientData.ChartAreas[0].AxisX.ScaleView.Position = position;
                    else if (scrollBarToEnd)
                        graphPatientData.ChartAreas[0].AxisX.ScaleView.Position = position;
                }
                else
                {
                    graphPatientData.ChartAreas[0].AxisX.Maximum = graphPatientData.Series["Heartbeat"].Points.Count();
                    graphPatientData.ChartAreas[0].AxisX.ScaleView.Position = 0;
                }
            }
        }


        public void updateLabels(BikeStatEvent bikeEvent)
        {
            Console.WriteLine("CLIENT IS CHANGING " + bikeEvent);
            if (bikeEvent.type != current) return;
            if (bikeEvent.source != selectedFilter) return;
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { updateLabels(bikeEvent); }));
            }
            else
            {
                var values = bikeEvent.values;
                var gap = "     ";

                heartbeatLabel.Text = values[(int) Stats.StatName.HEARTBEAT] + gap + "beats/minute" +
                                      Stats.getPrefix(current, (int) Stats.StatName.HEARTBEAT);
                RPMLabel.Text = values[(int) Stats.StatName.RPM] + gap + "rounds/minute" +
                                Stats.getPrefix(current, (int) Stats.StatName.RPM);
                speedLabel.Text = values[(int) Stats.StatName.SPEED] + gap + "km/hour" +
                                  Stats.getPrefix(current, (int) Stats.StatName.SPEED);
                distanceLabel.Text = values[(int) Stats.StatName.DISTANCE] + gap + "km" +
                                     Stats.getPrefix(current, (int) Stats.StatName.DISTANCE);
                powerLabel.Text = values[(int) Stats.StatName.PROGRAMWATTAGE] + gap + "watt" +
                                  Stats.getPrefix(current, (int) Stats.StatName.PROGRAMWATTAGE);
                energyLabel.Text = values[(int) Stats.StatName.ENERGY] + gap + "kilo joule" +
                                   Stats.getPrefix(current, (int) Stats.StatName.ENERGY);
                timeLabel.Text = values[(int) Stats.StatName.TIME] + gap + "seconds" +
                                 Stats.getPrefix(current, (int) Stats.StatName.TIME);
                acPowerLabel.Text = values[(int) Stats.StatName.WATTAGE] + gap + "watt" +
                                    Stats.getPrefix(current, (int) Stats.StatName.WATTAGE);
            }
        }


        public void loadIdentifier(object focus)
        {
            if (focus is ClientIdentifier)
            {
                client.sendData(new Message(Commands.GETSTATS, focus));
                current = BikeStatEvent.StatTypes.CURRENT_STATISTICS;
                linkLabel1.Visible = true;
            }


            if (focus is HistoryStats)
            {
                client.receivedAllBikeData.Invoke((HistoryStats) focus);
                linkLabel1.Visible = false;
                current = BikeStatEvent.StatTypes.COMBINED;
                updateLabels(((HistoryStats) focus).getCombined());
            }
        }

        private void updateGraphAvrageHeartBeatLine()
        {
            if (graphPatientData.InvokeRequired)
                graphPatientData.Invoke(new MethodInvoker(delegate { updateGraphAvrageHeartBeatLine(); }));
            else
            {
                // clear current avrage line
                graphPatientData.Series["HeartBeatAverage"].Points.Clear();

                var tempList = new List<double>();

                // turn data from chart into double data
                foreach (
                    var point in
                    graphPatientData.Series["Heartbeat"].Points)
                    tempList.Add(point.YValues[0]);

                // put all hearbeat data a higher value then 0 in another list
                var tempAvrageList = new List<double>();
                for (var i = 0; i < tempList.Count(); i++)
                    if (tempList[i] != 0)
                        tempAvrageList.Add(tempList[i]);

                // of the new list calculate the avrage and put it int the graph
                // also check if there is any heartbeat data that is not 0
                if (tempAvrageList.Count() != 0)
                {
                    // if list is smaller then 60 there will not be a full line if this not done
                    int xAmount;
                    if (tempList.Count() < 60)
                        xAmount = 60;
                    else
                        xAmount = tempList.Count();
                    for (var i = 0; i < xAmount; i++)
                        graphPatientData.Series["HeartBeatAverage"].Points.Add(tempAvrageList.Average());
                    heartbeatAverageValue = tempAvrageList.Average();
                    graphPatientData.Series["HeartBeatAverage"].Enabled = true; // niet zeker of nog nodig is
                }
                else
                    graphPatientData.Series["HeartBeatAverage"].Enabled = false; // niet zeker of nog nodig is
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var values = Enum.GetNames(typeof(BikeStatEvent.StatTypes));
            var current = (int) this.current + 1;

            if (current > values.Length - 1) current = 0;

            this.current = (BikeStatEvent.StatTypes) Enum.Parse(typeof(BikeStatEvent.StatTypes), values[current]);
            linkLabel1.Text = values[current];

            if (sender != null) client.sendData(this.current);
        }

        private void StatTab_Click(object sender, EventArgs e)
        {
        }
    }
}