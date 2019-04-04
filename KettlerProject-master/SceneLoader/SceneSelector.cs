using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SceneLoader
{
    public partial class SceneSelector : Form
    {
        public string path;
        private Size minButtonSize;
        private string selectedPath = null;
        private VRConnector vr;
        public Bike bike;
        public SceneSelector(VRConnector vr,Bike bike)
        {
            this.bike = bike;
            InitializeComponent();
            string[] stringSeparators = new string[] { "bin" };
            string[] dir = System.Environment.CurrentDirectory.Split(stringSeparators, StringSplitOptions.None);
            path = dir[0] + @"scenes\";
            minButtonSize = new Size(50, 25);
            //this.vr = vr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string sceneFile = path+b.Text+".txt";
            string allText = File.ReadAllText(sceneFile);
            string[] commands = allText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            textBox1.Text = null;
            pictureBox1.Image = null;
            foreach (string s in commands)
            {
                
                string[] command = s.Split(',');
                Random random = new Random();
                Console.WriteLine(command[0]);
                //GetHeight should still be implemented.
                switch (command[0].Trim().ToLower())
                {
                    case "description":
                        textBox1.Text = command[1].Trim();
                       break ;
                    case "imagename":
                        string imagePath = path + @"thumbnails\" + command[1].Trim();
                        Console.WriteLine(imagePath);
                        try { pictureBox1.Load(imagePath);
                        }
                        catch (Exception)
                        {
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
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


                }     


            }

            selectedPath = b.Name;
        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.Enabled = false;
            string[] fileEntries = Directory.GetFiles(path);
            try
            {
                foreach (string fileName in fileEntries)
                {
                    if (fileName.Contains(".txt"))
                    {
                        string[] stringSeparators = new string[] { "scenes\\" };
                        string[] sceneName = fileName.Split(stringSeparators, StringSplitOptions.None);
                        Button button = new Button();
                        button.AutoSize = true;
                        flowLayoutPanel1.Controls.Add(button);
                        string[] stringSeparators2 = new string[] { ".txt" };
                        string[] sceneName2 = sceneName[1].Split(stringSeparators2, StringSplitOptions.None);
                        button.Text = sceneName2[0];
                        button.Name = String.Join("", sceneName);
                        if (button.Size.Width > minButtonSize.Width || button.Size.Height > minButtonSize.Height)
                        {
                            minButtonSize = button.Size;
                        }
                        button.MinimumSize = minButtonSize;

                        button.Click += new EventHandler(button1_Click);

                    }
                }
            }
            catch (Exception)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Couldn't find any scenes.", "Error", buttons);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedPath != null)
            {
                //VRGenerator vrg = new VRGenerator(vr);
                //vrg.loadSceneFromFile();
                Console.WriteLine(selectedPath);
                new VRSceneHandler(vr, bike ).loadSceneFromFile(selectedPath);
                
            }
            else
            {
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show("Thumbnail not found", "Error", buttons);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
