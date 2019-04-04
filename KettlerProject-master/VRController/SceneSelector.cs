using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using KettlerReader;
using Timer = System.Timers.Timer;

namespace VRController
{
    public partial class SceneSelector : Form
    {
        public Bike bike;

        private bool buttonDrawn = true;
        public string folderName;
        public VRSceneHandler handler;
        private Size minButtonSize;
        public string path;
        private ProgressBar progress;
        public string sceneFile;
        private string selectedPath;
        private Thread thread;
        private int totalNumber;
        public Timer updateTimer;
        private readonly VRConnector vr;

        public SceneSelector(VRConnector vr, Bike bike)
        {
            updateTimer = new Timer();
            progress = null;
            this.bike = bike;
            InitializeComponent();
            string[] stringSeparators = {"bin"};
            var dir = Environment.CurrentDirectory.Split(stringSeparators, StringSplitOptions.None);
            path = dir[0] + @"scenes\";
            minButtonSize = new Size(50, 25);
            this.vr = vr;
            button2.Enabled = false;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }


        public void StopScene()
        {
            handler.emergencyPanel();
            button1.Enabled = true;
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            totalNumber = 0;
            var b = (Button) sender;
            folderName = b.Text;
            sceneFile = path + b.Text + ".txt";
            var allText = File.ReadAllText(sceneFile);
            var commands = allText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            textBox1.Text = null;
            pictureBox1.Image = null;

            foreach (var s in commands)
            {
                var command = s.Split(',');
                var random = new Random();
                //Console.WriteLine(command[0]);

                switch (command[0].Trim().ToLower())
                {
                    case "terrain":
                        totalNumber += 10;
                        break;
                    case "node":
                        totalNumber += int.Parse(command[2]);
                        break;
                    case "description":
                        textBox1.Text = command[1].Trim();
                        break;
                    case "imagename":
                        var imagePath = path + command[1].Trim();
                        //Console.WriteLine(imagePath);
                        try
                        {
                            pictureBox1.Load(imagePath);
                        }
                        catch (Exception)
                        {
                            var buttons = MessageBoxButtons.OK;
                            MessageBox.Show("Thumbnail not found", "Error", buttons);
                        }
                        switch (command[2].Trim().ToLower())
                        {
                            case "autosize":
                                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                                break;
                            case "centerimage":
                                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                break;
                            case "normal":
                                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                                break;
                            case "stretchimage":
                                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                break;
                            case "zoom":
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                                break;
                            default:
                                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                                break;
                        }
                        break;

                    default:
                        if (!command[0].Contains("//"))
                            totalNumber += 2;
                        break;
                }
            }

            selectedPath = b.Name;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            var b = (Button) sender;
            b.Enabled = false;
            var buttonsAdded = new List<Button>();
            var fileEntries = Directory.GetFiles(path);
            try
            {
                foreach (var fileName in fileEntries)
                    if (fileName.Contains(".txt"))
                    {
                        string[] stringSeparators = {"scenes\\"};
                        var sceneName = fileName.Split(stringSeparators, StringSplitOptions.None);
                        var button4 = new Button();
                        button4.AutoSize = false;
                        button4.Size = new Size(75, 40);
                        button4.BackColor = Color.LightGray;
                        flowLayoutPanel1.Controls.Add(button4);
                        string[] stringSeparators2 = {".txt"};
                        var sceneName2 = sceneName[1].Split(stringSeparators2, StringSplitOptions.None);
                        button4.Text = sceneName2[0];
                        button4.Name = string.Join("", sceneName);
                        if ((button4.Size.Width > minButtonSize.Width) || (button4.Size.Height > minButtonSize.Height))
                            minButtonSize = button4.Size;
                        button4.MinimumSize = minButtonSize;

                        button4.Click += button1_Click;
                        buttonsAdded.Add(button4);
                    }
                button2.Enabled = true;
            }
            catch (Exception)
            {
                var buttons = MessageBoxButtons.OK;
                MessageBox.Show("Couldn't find any scenes.", "Error", buttons);
            }
            if (fileEntries.Length > 0)
            {
                //buttonsAdded[0].Select();
                button2.Select();
                buttonsAdded[0].PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var player = new SoundPlayer();
            button2.Enabled = false;
            if (progress == null)
                progress = new ProgressBar();
            progress.Value = 0;
            tableLayoutPanel1.Controls.Add(progress, 1, 2);
            var widths = tableLayoutPanel1.GetColumnWidths();
            progress.Width = widths[1];

            progress.Visible = true;
            if (selectedPath != null)
            {
                //Console.WriteLine(path);
                //Console.WriteLine(sceneFile);
                updateTimer.Enabled = false;
                handler = new VRSceneHandler(vr, bike);


                if (thread != null)
                {
                    while (thread.ThreadState == ThreadState.Running)
                        Console.WriteLine("WAITING FOR AIDS");
                    vr.started = true;
                }

                handler.threadStop = true;

                Thread.Sleep(3000);
                Console.WriteLine("DOEING THE AIDS");

                thread = new Thread(() => handler.loadSceneFromFile(sceneFile, path, totalNumber));
                Console.WriteLine("AIDS RECEIVED");
                player.SoundLocation = path + folderName + "\\background.wav";
                player.PlayLooping();


                handler.resetScene();
                Console.WriteLine("OLD AIDS REMOVED");

                thread.Start();
                Console.WriteLine("NEW  AIDS STARTED");

                handler.notifyOnCountChanged = calcTotal;
            }
            else
            {
                var buttons = MessageBoxButtons.OK;
                MessageBox.Show("SceneFile not Found", "Error", buttons);
            }
        }

        public void calcTotal(int newTotal)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { calcTotal(newTotal); }));
            }
            else
            {
                progress.Value = (int) (newTotal/(double) totalNumber*100);
                Console.WriteLine(progress.Value + "%");
                if (progress.Value == 100)
                {
                    //progress.Dispose();
                    //this.WindowState = FormWindowState.Minimized;
                    //this.Dispose();
                    button2.Enabled = true;
                    //updateTimer.Enabled = true;
                    new Thread(() => handler.tickingTimer()).Start();
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.PerformClick();
        }
    }
}