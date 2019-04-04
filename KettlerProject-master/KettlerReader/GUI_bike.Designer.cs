namespace KettlerReader
{
    partial class GUI_bike
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProgramName = new System.Windows.Forms.ComboBox();
            this.ProgramValue = new System.Windows.Forms.TextBox();
            this.ProgramSend = new System.Windows.Forms.Button();
            this.PU = new System.Windows.Forms.Button();
            this.PD = new System.Windows.Forms.Button();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.heartbeatLabel = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.rpmLabel = new System.Windows.Forms.Label();
            this.wattageLabel = new System.Windows.Forms.Label();
            this.energyLabel = new System.Windows.Forms.Label();
            this.aWattageLabel = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonRandom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProgramName
            // 
            this.ProgramName.FormattingEnabled = true;
            this.ProgramName.Items.AddRange(new object[] {
            "Distance",
            "Time",
            "Energy",
            "Power"});
            this.ProgramName.Location = new System.Drawing.Point(36, 12);
            this.ProgramName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ProgramName.Name = "ProgramName";
            this.ProgramName.Size = new System.Drawing.Size(184, 24);
            this.ProgramName.TabIndex = 0;
            this.ProgramName.Text = "Distance";
            this.ProgramName.SelectedIndexChanged += new System.EventHandler(this.ProgramName_SelectedIndexChanged);
            // 
            // ProgramValue
            // 
            this.ProgramValue.Location = new System.Drawing.Point(36, 37);
            this.ProgramValue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ProgramValue.Name = "ProgramValue";
            this.ProgramValue.Size = new System.Drawing.Size(184, 22);
            this.ProgramValue.TabIndex = 1;
            // 
            // ProgramSend
            // 
            this.ProgramSend.Location = new System.Drawing.Point(36, 71);
            this.ProgramSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ProgramSend.Name = "ProgramSend";
            this.ProgramSend.Size = new System.Drawing.Size(184, 36);
            this.ProgramSend.TabIndex = 2;
            this.ProgramSend.Text = "Send";
            this.ProgramSend.UseVisualStyleBackColor = true;
            this.ProgramSend.Click += new System.EventHandler(this.ProgramSend_Click);
            // 
            // PU
            // 
            this.PU.Location = new System.Drawing.Point(290, 10);
            this.PU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PU.Name = "PU";
            this.PU.Size = new System.Drawing.Size(153, 26);
            this.PU.TabIndex = 4;
            this.PU.Text = "Program Up";
            this.PU.UseVisualStyleBackColor = true;
            this.PU.Click += new System.EventHandler(this.PU_Click);
            // 
            // PD
            // 
            this.PD.Location = new System.Drawing.Point(290, 37);
            this.PD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PD.Name = "PD";
            this.PD.Size = new System.Drawing.Size(153, 25);
            this.PD.TabIndex = 5;
            this.PD.Text = "Program Down";
            this.PD.UseVisualStyleBackColor = true;
            this.PD.Click += new System.EventHandler(this.PD_Click);
            // 
            // distanceLabel
            // 
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Location = new System.Drawing.Point(80, 245);
            this.distanceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(67, 17);
            this.distanceLabel.TabIndex = 6;
            this.distanceLabel.Text = "Distance:";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(285, 213);
            this.timeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(43, 17);
            this.timeLabel.TabIndex = 7;
            this.timeLabel.Text = "Time:";
            // 
            // heartbeatLabel
            // 
            this.heartbeatLabel.AutoSize = true;
            this.heartbeatLabel.Location = new System.Drawing.Point(80, 146);
            this.heartbeatLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.heartbeatLabel.Name = "heartbeatLabel";
            this.heartbeatLabel.Size = new System.Drawing.Size(75, 17);
            this.heartbeatLabel.TabIndex = 8;
            this.heartbeatLabel.Text = "Heartbeat:";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(80, 213);
            this.speedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(53, 17);
            this.speedLabel.TabIndex = 9;
            this.speedLabel.Text = "Speed:";
            // 
            // rpmLabel
            // 
            this.rpmLabel.AutoSize = true;
            this.rpmLabel.Location = new System.Drawing.Point(80, 181);
            this.rpmLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rpmLabel.Name = "rpmLabel";
            this.rpmLabel.Size = new System.Drawing.Size(42, 17);
            this.rpmLabel.TabIndex = 10;
            this.rpmLabel.Text = "RPM:";
            // 
            // wattageLabel
            // 
            this.wattageLabel.AutoSize = true;
            this.wattageLabel.Location = new System.Drawing.Point(285, 146);
            this.wattageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.wattageLabel.Name = "wattageLabel";
            this.wattageLabel.Size = new System.Drawing.Size(51, 17);
            this.wattageLabel.TabIndex = 11;
            this.wattageLabel.Text = "Power:";
            // 
            // energyLabel
            // 
            this.energyLabel.AutoSize = true;
            this.energyLabel.Location = new System.Drawing.Point(285, 181);
            this.energyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.energyLabel.Name = "energyLabel";
            this.energyLabel.Size = new System.Drawing.Size(57, 17);
            this.energyLabel.TabIndex = 12;
            this.energyLabel.Text = "Energy:";
            // 
            // aWattageLabel
            // 
            this.aWattageLabel.AutoSize = true;
            this.aWattageLabel.Location = new System.Drawing.Point(285, 245);
            this.aWattageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aWattageLabel.Name = "aWattageLabel";
            this.aWattageLabel.Size = new System.Drawing.Size(94, 17);
            this.aWattageLabel.TabIndex = 13;
            this.aWattageLabel.Text = "Actual Power:";
            this.aWattageLabel.Click += new System.EventHandler(this.aWattageLabel_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(363, 71);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(80, 36);
            this.buttonReset.TabIndex = 15;
            this.buttonReset.Text = "Reset";
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click_1);
            // 
            // buttonRandom
            // 
            this.buttonRandom.Location = new System.Drawing.Point(290, 71);
            this.buttonRandom.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRandom.Name = "buttonRandom";
            this.buttonRandom.Size = new System.Drawing.Size(80, 36);
            this.buttonRandom.TabIndex = 14;
            this.buttonRandom.Text = "Random";
            this.buttonRandom.Click += new System.EventHandler(this.buttonRandom_Click_1);
            // 
            // GUI_bike
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 313);
            this.Controls.Add(this.aWattageLabel);
            this.Controls.Add(this.energyLabel);
            this.Controls.Add(this.wattageLabel);
            this.Controls.Add(this.rpmLabel);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.heartbeatLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.distanceLabel);
            this.Controls.Add(this.buttonRandom);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.PD);
            this.Controls.Add(this.PU);
            this.Controls.Add(this.ProgramSend);
            this.Controls.Add(this.ProgramValue);
            this.Controls.Add(this.ProgramName);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GUI_bike";
            this.Text = "GUI_bike";
            this.Load += new System.EventHandler(this.GUI_bike_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ProgramName;
        private System.Windows.Forms.TextBox ProgramValue;
        private System.Windows.Forms.Button ProgramSend;
        private System.Windows.Forms.Button PU;
        private System.Windows.Forms.Button PD;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label heartbeatLabel;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label rpmLabel;
        private System.Windows.Forms.Label wattageLabel;
        private System.Windows.Forms.Label energyLabel;
        private System.Windows.Forms.Label aWattageLabel;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonRandom;
    }
}