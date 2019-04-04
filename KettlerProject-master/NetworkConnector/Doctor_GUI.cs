using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;

namespace NetworkConnector
{
    public partial class Doctor_GUI : Form
    {
        public DoctorClient client;
        public List<HistoryStats> history = new List<HistoryStats>();


        public List<ClientIdentifier> Identifiers = new List<ClientIdentifier>();
        private string justSpoke;
        public List<HistoryStats> loadedHistory = new List<HistoryStats>();
        private bool manualControl;

        private int power;


        public List<bool> runStatus = new List<bool>();

        //bikecommand textbox
        private readonly string standardTextBikeCommand = "Enter your command here....";

        // messager textbox
        private readonly string standardTextMessager = "Your message Here...";
        public List<bool> stSyncer = new List<bool>();


        public Doctor_GUI(DoctorClient client)
        {
            this.client = client;
            Visible = true;
            InitializeComponent();

            client.receivedVRData += updateVRdata;
            client.MessageNotifier += updateMessages;
            client.ConnectionsUpdate += RefreshConnections;
            client.historyNotifier += updateHistory;
            client.connectionCloseNotifier += closeWindow;

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            client.sendData(Commands.NOTIFYONNEWCONNECTED);


            bikeCommand.Text = standardTextBikeCommand;
            bikeCommand.ForeColor = Color.Gray;
            messager.Text = standardTextMessager;
            messager.ForeColor = Color.Gray;
            var speech = new SpeechSynthesizer();
            speech.SpeakAsync("Hello" + client.name);
            startTraining.Text = "Start Training";
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Bounds = Screen.PrimaryScreen.Bounds;
        }


        public void RefreshConnections(List<ClientIdentifier> identifiers)
        {
            Identifiers = identifiers;
            if (InvokeRequired)
                Invoke(new MethodInvoker(delegate { RefreshConnections(identifiers); }));
            else
            {
                updateBikes(identifiers);
                updateTargets(identifiers);
            }
        }


        public void updateTargets(List<ClientIdentifier> identifiers)
        {
            var set = false;
            var prev = 0;
            set = targets.Items.Count == 0;
            if (!set) prev = targets.SelectedIndex;

            targets.Items.Clear();
            foreach (var ident in identifiers)
                if (!ident.self) targets.Items.Add(ident);


            if ((targets.Items.Count > 0) && set) targets.SelectedIndex = 0;
            else targets.SelectedIndex = prev;


            targets.Items.Add("[Broadcast]");
        }

        public void updateBikes(List<ClientIdentifier> identifiers)
        {
            var set = false;
            var prev = -1;
            set = comboBox1.Items.Count == 0;
            if (!set)
                prev = comboBox1.SelectedIndex;
            comboBox1.Items.Clear();

            foreach (var ident in identifiers)
            {
                if (runStatus.Count >= ident.serverID) runStatus.Insert(ident.serverID, false);
                if (stSyncer.Count >= ident.serverID) stSyncer.Insert(ident.serverID, true);
                if ((ident.rights == Authentication.Rights.PATIENT) && !ident.self)
                    comboBox1.Items.Add(ident);
            }

            if ((comboBox1.Items.Count > 0) && set) comboBox1.SelectedIndex = 0;
            else if ((comboBox1.Items.Count == 0) && (prev == 0)) comboBox1.SelectedItem = null;
            else comboBox1.SelectedIndex = prev;
        }


        public ClientIdentifier getSelectedTarget()
        {
            if (targets.SelectedItem.ToString() == "[Broadcast]") return null;
            var current = (ClientIdentifier) targets.SelectedItem;
            if (current is HistoryIdentifier) return current;
            var index = 0;
            foreach (var client in Identifiers)
            {
                if (client == current) return client;
                index++;
            }

            return null;
        }

        public void updateMessages(List<TextMessage> messages)
        {
            if ((messages.Count > 0) && (messages[messages.Count - 1].ToString() != justSpoke))
            {
                messages[messages.Count - 1].toSpeech();
                justSpoke = messages[messages.Count - 1].ToString();
            }

            if (InvokeRequired)
                Invoke(new MethodInvoker(delegate { updateMessages(messages); }));
            else
            {
                this.messages.Items.Clear();
                this.messages.Items.AddRange(messages.ToArray());
                this.messages.TopIndex = this.messages.Items.Count - 1;
            }
        }


        public void updateHistory(List<HistoryStats> history)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(delegate { updateHistory(history); }));
            else
            {
                this.history = history;
                var set = false;
                var prev = -1;
                set = historylist.Items.Count == 0;
                if (!set) prev = historylist.SelectedIndex;

                historylist.Items.Clear();
                foreach (var historystat in history)
                    historylist.Items.Add(historystat.ToString());


                if ((historylist.Items.Count > 0) && set) historylist.SelectedIndex = 0;
                else historylist.SelectedIndex = prev;
            }
        }


        public void updateVRdata(string[] data)
        {
            // if (data.Length == 0) this.statusLabel.Text = "NO VR CONNECTIONS ON CLIENT PC";
            //for (var i = 0; i < data.Length; i++) this.vrDetails.Items.Add(data[i]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.sendData(new Forward(getSelectedBike().serverID, Commands.VR_AVAILABLE, null));
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshTrainingButton();
            refreshSyncButton();
            refreshHistoryList();
        }

        public void refreshHistoryList()
        {
            if (historylist.Items.Count > 0)
            {
                var stats = history.ElementAt(0);
                if ((getSelectedBike() == null) || (stats.username != getSelectedBike().name))
                    historylist.Items.Clear();
            }
        }

        public void refreshTrainingButton()
        {
            var bike = getSelectedBike().serverID;
            if (runStatus.Count > getSelectedBike().serverID)
                if (runStatus.ElementAt(bike))
                    startTraining.Text = "Stop Training";
                else
                    startTraining.Text = "Start Training";
        }

        public void refreshSyncButton()
        {
            var bike = getSelectedBike().serverID;
            if (stSyncer.Count > getSelectedBike().serverID)
                if (stSyncer.ElementAt(bike))
                {
                    //bikeSyncer.Text = "Stop Syncing";
                }
        }


        private void refreshBikes_Click(object sender, EventArgs e)
        {
            client.sendData(Commands.ALLCONNECTED);
        }


        private ClientIdentifier getSelectedBike()
        {
            return (ClientIdentifier) comboBox1.SelectedItem;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            client.sendData(Commands.ALLCONNECTED);
        }

        private void targets_SelectedIndexChanged(object sender, EventArgs e)
        {
            var target = getSelectedTarget();
            client.refreshMessages(target);
            if ((target != null) && target is HistoryIdentifier)
                messager.Enabled = false;
            else
                messager.Enabled = true;
        }

        /// <summary>
        ///     NEEDDS TO SEND LESS DATA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            power = trackBar1.Value*(100/8)*4;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (manualControl)
            {
                var forward = new Forward(getSelectedBike().serverID, Commands.BIKECOMMAND, "PW " + power);
                client.sendData(forward);
            }
        }

        private void startTraining_Click(object sender, EventArgs e)
        {
            var bike = getSelectedBike().serverID;

            if (startTraining.Text == "Start Training")
            {
                var training = new Training(360);
                training.generatePowerPeeks(training.generatePoints());

                client.sendData(new Forward(bike, Commands.STARTTRAINING, training));
                startTraining.Text = "Stop training";
            }
            else
            {
                startTraining.Text = "Start Training";
                client.sendData(new Forward(bike, Commands.STOPTRAINING, ""));
            }
        }

        private void Doctor_GUI_Load(object sender, EventArgs e)
        {
            SetAutoSizeMode(AutoSizeMode.GrowOnly);
            Size = new Size(1920, 1080);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // BREAK CONNECTION
            client.sendData(Commands.BREAKCONNECTION);
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                manualControl = true;
                client.sendData(Commands.MANUALCONTROL);
            }
            else
            {
                manualControl = false;
                client.sendData(Commands.AUTOCONTROL);
            }
        }


        private void button8_Click(object sender, EventArgs e)
        {
            client.sendData(new Message(Commands.HISTORYSESSIONS, getSelectedBike()));
        }

        public void loadHistory(HistoryStats stats)
        {
            foreach (var existing in loadedHistory)
                if (existing.historyID == stats.historyID)
                    return;

            loadedHistory.Add(stats);


            if (messages != null)
            {
                var ident = new HistoryIdentifier(stats.historyID, stats.ToString2(), Authentication.Rights.UNKNOWN);

                var otherident = new HistoryIdentifier(stats.historyID, client.name, Authentication.Rights.UNKNOWN);
                foreach (var message in stats.messages)
                    if (message != null)
                    {
                        if (message.source.rights == Authentication.Rights.PATIENT)
                        {
                            message.source = ident;
                            message.target = otherident;
                        }
                        else
                        {
                            message.target = ident;
                            message.source = otherident;
                        }
                        client.messages.Add(message);
                    }

                targets.Items.Add(ident);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            client.sendData(new Forward(getSelectedBike().serverID, Commands.EMERGENCYBREAK, ""));
        }


        private void button10_Click_1(object sender, EventArgs e)
        {
            client.sendData(new Message(Commands.SAVESESSION, getSelectedBike()));
        }

        private void closeWindow()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(closeWindow));
            else
                Visible = false;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (historylist.SelectedItem == null) return;
            var stat = history.ElementAt(historylist.SelectedIndex);
            loadHistory(stat);
        }


        public void showStatistics(object identifier)
        {
            var gui = new StatisticsGUI(client, identifier);
            new Thread(() => gui.ShowDialog()).Start();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            manualControl = checkBox1.Checked;
        }

        private void openHistory_Click(object sender, EventArgs e)
        {
            if (historylist.SelectedItem == null) return;
            var stat = history.ElementAt(historylist.SelectedIndex);
            showStatistics(stat);
        }

        private void openLive_Click(object sender, EventArgs e)
        {
            var bike = getSelectedBike();
            if (bike != null)
                showStatistics(bike);
        }


        //messager send button
        private void button5_Click(object sender, EventArgs e)
        {
            if ((messager.Text != "") && (messager.Text != standardTextMessager))
            {
                var message = new TextMessage(messager.Text, getSelectedTarget(), null);
                client.sendData(message);
                messager.Clear();
                messager_Leave(null, null);
            }
        }

        //bikecommand send button
        private void button1_Click(object sender, EventArgs e)
        {
            if (getSelectedBike() == null)
                return;
            if ((messager.Text != "") && (messager.Text != standardTextBikeCommand))
            {
                var forward = new Forward(getSelectedBike().serverID, Commands.BIKECOMMAND, bikeCommand.Text);
                client.sendData(forward);
                bikeCommand.Clear();
                bikeCommand_Leave(null, null);
            }
        }

        private void messager_Click(object sender, EventArgs e)
        {
            if (messager.Text == standardTextMessager)
                messager.Select(0, 0);
        }

        private void messager_Leave(object sender, EventArgs e)
        {
            if ((messager.Text == "") || (messager.Text == standardTextMessager))
            {
                messager.Text = standardTextMessager;
                messager.ForeColor = Color.Gray;
            }
        }

        private void messager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button5.PerformClick();
            else if (messager.Text == standardTextMessager)
            {
                messager.Text = "";
                messager.ForeColor = Color.Black;
            }
        }

        private void messager_KeyUp(object sender, KeyEventArgs e)
        {
            if (messager.Text == "")
            {
                messager.Text = standardTextMessager;
                messager.ForeColor = Color.Gray;
            }
        }

        private void bikeCommand_Click(object sender, EventArgs e)
        {
            if (bikeCommand.Text == standardTextBikeCommand)
                bikeCommand.Select(0, 0);
        }

        private void bikeCommand_Leave(object sender, EventArgs e)
        {
            if ((bikeCommand.Text == "") || (bikeCommand.Text == standardTextBikeCommand))
            {
                bikeCommand.Text = standardTextBikeCommand;
                bikeCommand.ForeColor = Color.Gray;
            }
        }

        private void bikeCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
            else if (bikeCommand.Text == standardTextBikeCommand)
            {
                bikeCommand.Text = "";
                bikeCommand.ForeColor = Color.Black;
            }
        }

        private void bikeCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (bikeCommand.Text == "")
            {
                bikeCommand.Text = standardTextBikeCommand;
                bikeCommand.ForeColor = Color.Gray;
            }
        }
    }
}