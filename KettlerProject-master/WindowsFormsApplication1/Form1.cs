using System;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private readonly SpeechRecognizer srecon2 = new SpeechRecognizer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine(srecon2.AudioState);
        }

        private void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            var incoming = e.Result.Text;
            if (incoming.Trim().ToLower().Contains("enter") || incoming.Trim().ToLower().Contains("send"))
            {
                richTextBox1.Clear();
                richTextBox1.AppendText(incoming + " ");
            }
            else
            {
                richTextBox1.AppendText(incoming + " ");
            }
        }

        private void messager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                MessageBox.Show(" IT WORKS");
        }
    }
}