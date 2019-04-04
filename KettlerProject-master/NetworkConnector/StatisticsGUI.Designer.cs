namespace NetworkConnector
{
    partial class StatisticsGUI
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.statTabs = new System.Windows.Forms.TabControl();
            this.StatTab = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.acPowerLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.powerLabel = new System.Windows.Forms.Label();
            this.heartbeatLabel = new System.Windows.Forms.Label();
            this.RPMLabel = new System.Windows.Forms.Label();
            this.energyLabel = new System.Windows.Forms.Label();
            this.graphTab = new System.Windows.Forms.TabPage();
            this.graphPatientData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.messageTab = new System.Windows.Forms.TabPage();
            this.messages = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.statTabs.SuspendLayout();
            this.StatTab.SuspendLayout();
            this.graphTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphPatientData)).BeginInit();
            this.messageTab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statTabs
            // 
            this.statTabs.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.statTabs.Controls.Add(this.StatTab);
            this.statTabs.Controls.Add(this.graphTab);
            this.statTabs.Controls.Add(this.messageTab);
            this.statTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statTabs.ItemSize = new System.Drawing.Size(206, 60);
            this.statTabs.Location = new System.Drawing.Point(3, 70);
            this.statTabs.Name = "statTabs";
            this.statTabs.SelectedIndex = 0;
            this.statTabs.Size = new System.Drawing.Size(636, 616);
            this.statTabs.TabIndex = 33;
            // 
            // StatTab
            // 
            this.StatTab.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.StatTab.Controls.Add(this.linkLabel1);
            this.StatTab.Controls.Add(this.label8);
            this.StatTab.Controls.Add(this.label3);
            this.StatTab.Controls.Add(this.label7);
            this.StatTab.Controls.Add(this.label4);
            this.StatTab.Controls.Add(this.label9);
            this.StatTab.Controls.Add(this.label5);
            this.StatTab.Controls.Add(this.distanceLabel);
            this.StatTab.Controls.Add(this.acPowerLabel);
            this.StatTab.Controls.Add(this.label10);
            this.StatTab.Controls.Add(this.label6);
            this.StatTab.Controls.Add(this.speedLabel);
            this.StatTab.Controls.Add(this.timeLabel);
            this.StatTab.Controls.Add(this.powerLabel);
            this.StatTab.Controls.Add(this.heartbeatLabel);
            this.StatTab.Controls.Add(this.RPMLabel);
            this.StatTab.Controls.Add(this.energyLabel);
            this.StatTab.Location = new System.Drawing.Point(4, 64);
            this.StatTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StatTab.Name = "StatTab";
            this.StatTab.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.StatTab.Size = new System.Drawing.Size(628, 548);
            this.StatTab.TabIndex = 0;
            this.StatTab.Text = "Statistieken";
            this.StatTab.Click += new System.EventHandler(this.StatTab_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(84, 51);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(131, 20);
            this.linkLabel1.TabIndex = 21;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Current Statistics";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(105, 358);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Energy:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Heartbeat:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(111, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "Power:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(120, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "RPM:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(123, 408);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(108, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Speed:";
            // 
            // distanceLabel
            // 
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Location = new System.Drawing.Point(195, 257);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(18, 20);
            this.distanceLabel.TabIndex = 16;
            this.distanceLabel.Text = "0";
            // 
            // acPowerLabel
            // 
            this.acPowerLabel.AutoSize = true;
            this.acPowerLabel.Location = new System.Drawing.Point(195, 457);
            this.acPowerLabel.Name = "acPowerLabel";
            this.acPowerLabel.Size = new System.Drawing.Size(18, 20);
            this.acPowerLabel.TabIndex = 20;
            this.acPowerLabel.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(54, 457);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Actually Power:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(93, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Distance:";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Location = new System.Drawing.Point(195, 208);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(18, 20);
            this.speedLabel.TabIndex = 15;
            this.speedLabel.Text = "0";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(195, 408);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(18, 20);
            this.timeLabel.TabIndex = 19;
            this.timeLabel.Text = "0";
            // 
            // powerLabel
            // 
            this.powerLabel.AutoSize = true;
            this.powerLabel.Location = new System.Drawing.Point(195, 308);
            this.powerLabel.Name = "powerLabel";
            this.powerLabel.Size = new System.Drawing.Size(18, 20);
            this.powerLabel.TabIndex = 17;
            this.powerLabel.Text = "0";
            // 
            // heartbeatLabel
            // 
            this.heartbeatLabel.AutoSize = true;
            this.heartbeatLabel.Location = new System.Drawing.Point(195, 108);
            this.heartbeatLabel.Name = "heartbeatLabel";
            this.heartbeatLabel.Size = new System.Drawing.Size(18, 20);
            this.heartbeatLabel.TabIndex = 13;
            this.heartbeatLabel.Text = "0";
            // 
            // RPMLabel
            // 
            this.RPMLabel.AutoSize = true;
            this.RPMLabel.Location = new System.Drawing.Point(195, 157);
            this.RPMLabel.Name = "RPMLabel";
            this.RPMLabel.Size = new System.Drawing.Size(18, 20);
            this.RPMLabel.TabIndex = 14;
            this.RPMLabel.Text = "0";
            // 
            // energyLabel
            // 
            this.energyLabel.AutoSize = true;
            this.energyLabel.Location = new System.Drawing.Point(195, 358);
            this.energyLabel.Name = "energyLabel";
            this.energyLabel.Size = new System.Drawing.Size(18, 20);
            this.energyLabel.TabIndex = 18;
            this.energyLabel.Text = "0";
            // 
            // graphTab
            // 
            this.graphTab.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.graphTab.Controls.Add(this.graphPatientData);
            this.graphTab.Location = new System.Drawing.Point(4, 64);
            this.graphTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.graphTab.Name = "graphTab";
            this.graphTab.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.graphTab.Size = new System.Drawing.Size(628, 548);
            this.graphTab.TabIndex = 1;
            this.graphTab.Text = "Grafiek";
            // 
            // graphPatientData
            // 
            this.graphPatientData.BackColor = System.Drawing.Color.Transparent;
            this.graphPatientData.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.Maximum = 60D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.ScaleView.Position = 0D;
            chartArea1.AxisX.ScaleView.Size = 60D;
            chartArea1.AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 80F;
            chartArea1.Position.Width = 94F;
            chartArea1.Position.X = 3F;
            chartArea1.Position.Y = 10F;
            this.graphPatientData.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend1";
            legend1.Position.Auto = false;
            legend1.Position.Height = 9.16456F;
            legend1.Position.Width = 35F;
            legend1.Position.X = 60F;
            legend2.BackColor = System.Drawing.Color.Transparent;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            legend2.Name = "Legend2";
            legend2.Position.Auto = false;
            legend2.Position.Height = 9.571789F;
            legend2.Position.Width = 42.34234F;
            legend2.Position.X = 3F;
            this.graphPatientData.Legends.Add(legend1);
            this.graphPatientData.Legends.Add(legend2);
            this.graphPatientData.Location = new System.Drawing.Point(3, 0);
            this.graphPatientData.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.graphPatientData.Name = "graphPatientData";
            this.graphPatientData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend2";
            series1.Name = "Heartbeat";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.ForestGreen;
            series2.Legend = "Legend1";
            series2.Name = "RPM";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.MediumBlue;
            series3.Legend = "Legend1";
            series3.Name = "Actual power";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend2";
            series4.Name = "HeartBeatAverage";
            series4.YValuesPerPoint = 2;
            this.graphPatientData.Series.Add(series1);
            this.graphPatientData.Series.Add(series2);
            this.graphPatientData.Series.Add(series3);
            this.graphPatientData.Series.Add(series4);
            this.graphPatientData.Size = new System.Drawing.Size(607, 566);
            this.graphPatientData.TabIndex = 0;
            this.graphPatientData.Text = "chart1";
            // 
            // messageTab
            // 
            this.messageTab.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.messageTab.Controls.Add(this.messages);
            this.messageTab.Location = new System.Drawing.Point(4, 64);
            this.messageTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messageTab.Name = "messageTab";
            this.messageTab.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messageTab.Size = new System.Drawing.Size(631, 550);
            this.messageTab.TabIndex = 2;
            this.messageTab.Text = "Berichten";
            // 
            // messages
            // 
            this.messages.BackColor = System.Drawing.SystemColors.ControlLight;
            this.messages.FormattingEnabled = true;
            this.messages.ItemHeight = 20;
            this.messages.Location = new System.Drawing.Point(6, 8);
            this.messages.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(523, 504);
            this.messages.TabIndex = 32;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.statTabs, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.855072F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.14493F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(642, 689);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(636, 61);
            this.flowLayoutPanel1.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "STATISTICS {ID}";
            // 
            // StatisticsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 689);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StatisticsGUI";
            this.Text = "StatisticsGUI";
            this.Load += new System.EventHandler(this.StatisticsGUI_Load);
            this.statTabs.ResumeLayout(false);
            this.StatTab.ResumeLayout(false);
            this.StatTab.PerformLayout();
            this.graphTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.graphPatientData)).EndInit();
            this.messageTab.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl statTabs;
        private System.Windows.Forms.TabPage StatTab;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Label acPowerLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label powerLabel;
        private System.Windows.Forms.Label heartbeatLabel;
        private System.Windows.Forms.Label RPMLabel;
        private System.Windows.Forms.Label energyLabel;
        private System.Windows.Forms.TabPage graphTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart graphPatientData;
        private System.Windows.Forms.TabPage messageTab;
        private System.Windows.Forms.ListBox messages;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
    }
}