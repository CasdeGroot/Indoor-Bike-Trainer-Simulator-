using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace VRController
{
    public partial class VRConnector_GUI : Form
    {
        public delegate void Connetected(bool value);

        public static bool isAlreadyRunning;
        private readonly List<string> ids = new List<string>();

        private readonly VRConnector vr;

        private string[][] filled;

        private bool guiOpened;
        private bool showconnGUI = true;


        public VRConnector_GUI(VRConnector vrConnector, bool showConnGUI = true)
        {
            vr = vrConnector;

            isAlreadyRunning = true;

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeComponent();
            refresh(true);
            showconnGUI = showConnGUI;
            ConnectionList.DoubleClick += selected;
            panel1.MouseClick += showSecret;
        }

        public Connetected HasConnetected { get; set; }

        public bool autoConnected()
        {
            var avaiList = new List<string[]>();
            foreach (var info in filled)
                if ((info[0].ToLower() == Environment.MachineName.ToLower()) &&
                    (info[1].ToLower() == Environment.UserName.ToLower()))
                    avaiList.Add(info);
            if (avaiList.Count == 1)
                return selectedSession(avaiList[0]);
            return false;
        }

        public void showSecret(object sender, EventArgs args)
        {
            killSwitch.Visible = !killSwitch.Visible;
            Hack.Visible = !Hack.Visible;
        }

        public void reopen(object sender, EventArgs e)
        {
            Show();
            guiOpened = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selected();
        }

        private void scanList(object sender = null, EventArgs e = null)
        {
            foreach (ListViewItem item in ConnectionList.Items)
                if (!connect(item.SubItems[2].Text, item))
                    item.ForeColor = Color.Red;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var proc = new Process();
            //vr.write(Environment.CurrentDirectory);
            proc.StartInfo.FileName = "C:\\NetworkEngine\\sim.bat";
            proc.StartInfo.WorkingDirectory = "C:\\NetworkEngine";
            new Thread(() => proc.Start()).Start();

            refresh(false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new VRBruteForcer(vr, new BruteForce(), ids[ConnectionList.SelectedIndices[0]], 5);
        }

        private bool connect(string id, ListViewItem item)
        {
            // if (ConnectionList.SelectedItems.Count != 0)
            // {
            return vr.testTunnel(id, vr.key);


            // }
        }

        private void connect(bool invisble)
        {
            if (ConnectionList.SelectedItems.Count != 0)
            {
                if (vr.testTunnel(ids[ConnectionList.SelectedIndices[0]], vr.key) && !invisble)
                {
                    var selected = filled[ConnectionList.SelectedIndices[0]];
                    HasConnetected.Invoke(true);
                    executeWhenConnected(selected[0], selected[1], selected[2]);
                }
                else
                {
                    ConnectionList.SelectedItems[0].ForeColor = Color.Red;
                }
            }
            else
            {
                if (invisble) refresh(false);
            }
        }

        private void ConnectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void executeWhenConnected(string host, string username, string session)
        {
            if (guiOpened) return;
            Hide();
            vr.executeWhenConnected();
            //BikeManager bikeM = new KettlerReader.BikeManager(false,true,true);
            //bike = bikeM.bike;

            //if (showconnGUI)
            //{
            //    using (var vrGui = new VR_GUI(this.vr, username, host, session))
            //    {
            //        vrGui.Closed += this.reopen;
            //        ThreadPool.QueueUserWorkItem(delegate { vrGui.ShowDialog(); });
            //        this.guiOpened = true;
            //    }
            //}
        }

        private void refresh(bool first, bool rescan = true)
        {
            ConnectionList.Items.Clear();
            ids.Clear();
            ListViewItem last;
            filled = vr.getSessions();
            foreach (var stri in filled)
            {
                string[] stri2 = {stri[1], stri[2]};
                last = new ListViewItem(stri[0]);
                last.SubItems.AddRange(stri2);
                ConnectionList.Items.Add(last);
                ids.Add(stri[2]);
            }

            if (rescan)
                scanList();
            if (ConnectionList.Items.Count != 0)
                ConnectionList.Items[0].Selected = true;
        }

        private bool selectedSession(string[] selectedident)
        {
            if (vr.testTunnel(selectedident[2]))
            {
                executeWhenConnected(selectedident[0], selectedident[1], selectedident[2]);
                HasConnetected.Invoke(true);
                return true;
            }
            return false;
        }


        private void selected(object sender = null, EventArgs e = null)
        {
            if (ConnectionList.SelectedItems.Count != 0)
                if (vr.testTunnel(ids[ConnectionList.SelectedIndices[0]]))
                {
                    var selected = filled[ConnectionList.SelectedIndices[0]];
                    executeWhenConnected(selected[0], selected[1], selected[2]);
                    HasConnetected.Invoke(true);
                }
                else
                {
                    ConnectionList.SelectedItems[0].ForeColor = Color.Red;
                }
            else refresh(false);
        }

        private void VRConnector_GUI_Load(object sender, EventArgs e)
        {
        }

        private void killSwitch_Click(object sender, EventArgs e)
        {
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void VRConnector_GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            isAlreadyRunning = false;
        }
    }
}