using System.Collections.Generic;
using VRController;
using System.Speech.Synthesis;

namespace NetworkConnector
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Speech.Recognition;
    using System.Threading;
    using System.Windows.Forms;

    public partial class Patient_GUI : Form
    {
        private readonly PatientClient client;
        public VRController.SceneSelector scene;
        public VRController.SceneSelector handler = null;
        public Button sceneSelect;

        private SpeechRecognizer sRecognize ;
       
        public Patient_GUI(PatientClient client)
        {
            try
            {
                this.sRecognize = new SpeechRecognizer();
            }
            catch (Exception e)
            {
                sRecognize = null;
            }
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.scene = null;
            this.client = client;
            this.client.notifyAlert += alert;
            this.InitializeComponent();
            client.MessageNotifier += updateMessages;
            this.Text = string.Format($"{client.name}");
            client.vr.conHandler += switchButtons;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            client.connectionCloseNotifier += closeWindow;

            messager.Text = standardTextMessager;
            messager.ForeColor = Color.Gray;
            new SpeechSynthesizer().SpeakAsync("hello " + client.name);
        }

        public void alert()
        {
            if (handler != null)
            {

                handler.StopScene();
            }
            else
                Console.WriteLine("scene is null");
            //scene.handler.emergencyPanel();
        }

        public void updateMessages(List<TextMessage> messages)
        {
            if (messages.Count > 0)
                messages[messages.Count - 1].toSpeech();

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => updateMessages(messages)));
                return;
            }
            this.messages.Items.Clear();
            this.messages.Items.AddRange(messages.ToArray());
            this.messages.TopIndex = this.messages.Items.Count - 1;
            try
            {
                if (handler != null && handler.handler != null)
                    handler.handler.newMessage = messages.Last().ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void closeWindow()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(closeWindow));
            }
            else
            {
                this.Hide();
            }
        }

        private void Patient_GUI_Load(object sender, EventArgs e)
        {
            try
            {
                sRecognize = new SpeechRecognizer();
                if (sRecognize != null)
                    sRecognize.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sRecognize_SpeechRecognized);
            }
            catch (Exception)
            {
                MessageBox.Show(" No Speech Regonition", "ERROR", MessageBoxButtons.OK);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // BREAK CONNECTION
            client.sendData(Commands.BREAKCONNECTION);


        }
        private void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string incoming = e.Result.Text.ToString();
            if (incoming.Trim().ToLower().Contains("send"))
            {
                messager.Clear();
                button3.PerformClick();
            }
            else
            {
                messager.AppendText(incoming + " ");
                messager.Text += incoming;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!VRConnector_GUI.isAlreadyRunning)
            {
                var gui = new VRConnector_GUI(this.client.vr, false);

                gui.HasConnetected += switchButtons;
                if (!gui.autoConnected())
                    new Thread(() => gui.ShowDialog()).Start();
            }

        }


        private bool buttonDrawn = false;

        public void switchButtons(bool tunnel)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate () { switchButtons(tunnel); }));
            }
            else
            {
                if (tunnel)
                {
                    if (!buttonDrawn)
                    {
                        buttonDrawn = true;

                        button2.Dispose();
                        sceneSelect = new Button();
                        tableLayoutPanel1.Controls.Add(sceneSelect, 2, 1);
                        sceneSelect.Location = new System.Drawing.Point(612, 44);
                        sceneSelect.Text = "Select Scene";
                        sceneSelect.Size = new System.Drawing.Size(206, 60);
                        sceneSelect.Click += new System.EventHandler(this.button4_Click);
                        sceneSelect.BackColor = Color.LightGray;
                    }
                }
                else
                {

                }
            }
        }

        private void messager_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void sceneSelecter_Click(object sender, EventArgs e)
        {
            //SceneLoader.SceneSelector selecter = new SceneLoader.SceneSelector();
            //new Thread(() => selecter.ShowDialog()).Start();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            handler = new VRController.SceneSelector(client.vr, client.bike);

            new Thread(() => handler.ShowDialog()).Start();
        }

        // mesager send button
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.messager.Text != "" && this.messager.Text != standardTextMessager)
            {
                TextMessage message = new TextMessage(messager.Text, null, null);
                client.sendData(message);
                this.messager.Clear();
                this.messager_Leave(null, null);
            }
        }

        // messager textbox
        private string standardTextMessager = "Your message Here...";

        private void messager_Click(object sender, EventArgs e)
        {
            if (messager.Text == standardTextMessager)
                messager.Select(0, 0);
        }

        private void messager_Leave(object sender, EventArgs e)
        {
            if (messager.Text == "" || messager.Text == standardTextMessager)
            {
                messager.Text = standardTextMessager;
                messager.ForeColor = Color.Gray;
            }
        }

        private void messager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button3.PerformClick();
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
    }
}

