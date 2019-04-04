using System;
using System.Threading;
using System.Windows.Forms;

namespace NetworkConnector
{
    public partial class log_in : Form
    {
        private readonly Client client;

        private readonly string[] randomErrorAnswers =
        {
            "PLEASE ANSWER SOMETHING USEFULL...", "#NOTHACKABLE",
            "YOU SHALL NOT PASS", "OVERFLOWERRORS>MOTHERTOLARGE",
            "BITCHYOUCRAZY", "#BLAMEBART"
        };

        private readonly string[] values =
        {
            "Forgot password?", "You fcked up", "STOP CLICKING ME!",
            " I HAVE FEELINGS GODDAMIT!", "SUCK IT LOSER", "LOSING THIS PASSWORD",
            "ITS TOTALY NOT BLAMEBART", "I AM NO WIZARD", "YOOU SHALL NOT PASS",
            "OKAY MAYBE LITTLE PASS"
        };

        public Authentication authentication;

        private int index;

        public log_in(Client client)
        {
            this.client = client;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            InitializeComponent();
            // this.linkLabel1.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            authentication = new Authentication(usernameBox.Text, passwordBox.Text, Authentication.Rights.UNKNOWN);
            client.sendData(authentication);
            button1.Text = "The authentication is being verified";
            button1.Enabled = false;
        }

        public void setRandomResponse()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(setRandomResponse));
            }
        }


        private void checkForResponse()
        {
            while (client.loginResponse == Client.LoginResponse.Unknown)
            {
            }

            try
            {
                button1.Enabled = true;
                button1.Text = "Log in";


                switch (client.loginResponse)
                {
                    case Client.LoginResponse.Denied:
                        MessageBox.Show("The given in username and/or password is incorrect.");
                        break;
                    case Client.LoginResponse.AccountAlreadyInUse:
                        MessageBox.Show("The given acount is already being used at this moment");
                        break;
                    case Client.LoginResponse.AcountBanned:
                        MessageBox.Show("The given account has been banned and therefore unable to log in");
                        break;
                    case Client.LoginResponse.Accepted:
                        DialogResult = DialogResult.OK;
                        break;
                }
            }
            catch (Exception)
            {
            }
            if (client.loginResponse != Client.LoginResponse.Accepted)
            {
                client.loginResponse = Client.LoginResponse.Unknown;
                checkForResponse();
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            index++;
            if (values.Length - 1 < index) index = 0;

            //this.linkLabel1.Text = this.values[this.index];
            //this.linkLabel1.ForeColor = Color.Black;
        }

        private void log_in_Load(object sender, EventArgs e)
        {
            new Thread(checkForResponse).Start();
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void loginTextBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Select();
                button1.PerformClick();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}