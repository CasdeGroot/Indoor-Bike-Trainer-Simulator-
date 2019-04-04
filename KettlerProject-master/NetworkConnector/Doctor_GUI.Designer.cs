using System.Windows.Forms.DataVisualization.Charting;

namespace NetworkConnector
{
    partial class Doctor_GUI
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
            this.components = new System.ComponentModel.Container();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.refreshBikes = new System.Windows.Forms.Button();
            this.openLive = new System.Windows.Forms.Button();
            this.openHistory = new System.Windows.Forms.Button();
            this.loadbtn = new System.Windows.Forms.Button();
            this.historylist = new System.Windows.Forms.ComboBox();
            this.button10 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.startTraining = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button9 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.messager = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.messages = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.targets = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.bikeCommand = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tableLayoutPanel6.SetColumnSpan(this.comboBox1, 2);
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 31);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(313, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // refreshBikes
            // 
            this.refreshBikes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshBikes.Location = new System.Drawing.Point(22, 62);
            this.refreshBikes.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.refreshBikes.Name = "refreshBikes";
            this.refreshBikes.Size = new System.Drawing.Size(137, 39);
            this.refreshBikes.TabIndex = 3;
            this.refreshBikes.Text = "refresh";
            this.refreshBikes.UseVisualStyleBackColor = true;
            this.refreshBikes.Click += new System.EventHandler(this.refreshBikes_Click);
            // 
            // openLive
            // 
            this.openLive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openLive.Location = new System.Drawing.Point(181, 62);
            this.openLive.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.openLive.Name = "openLive";
            this.openLive.Size = new System.Drawing.Size(137, 39);
            this.openLive.TabIndex = 5;
            this.openLive.Text = "Open";
            this.openLive.UseVisualStyleBackColor = true;
            this.openLive.Click += new System.EventHandler(this.openLive_Click);
            // 
            // openHistory
            // 
            this.openHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openHistory.Location = new System.Drawing.Point(174, 78);
            this.openHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.openHistory.Name = "openHistory";
            this.openHistory.Size = new System.Drawing.Size(137, 39);
            this.openHistory.TabIndex = 6;
            this.openHistory.Text = "Open";
            this.openHistory.UseVisualStyleBackColor = true;
            this.openHistory.Click += new System.EventHandler(this.openHistory_Click);
            // 
            // loadbtn
            // 
            this.loadbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadbtn.Location = new System.Drawing.Point(15, 78);
            this.loadbtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loadbtn.Name = "loadbtn";
            this.loadbtn.Size = new System.Drawing.Size(137, 39);
            this.loadbtn.TabIndex = 4;
            this.loadbtn.Text = "Refresh";
            this.loadbtn.UseVisualStyleBackColor = true;
            this.loadbtn.Click += new System.EventHandler(this.button8_Click);
            // 
            // historylist
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.historylist, 2);
            this.historylist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.historylist.FormattingEnabled = true;
            this.historylist.Location = new System.Drawing.Point(2, 54);
            this.historylist.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.historylist.Name = "historylist";
            this.historylist.Size = new System.Drawing.Size(309, 21);
            this.historylist.TabIndex = 1;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(78, 82);
            this.button10.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(137, 39);
            this.button10.TabIndex = 10;
            this.button10.Text = "Save session";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click_1);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button10);
            this.groupBox9.Controls.Add(this.startTraining);
            this.groupBox9.Location = new System.Drawing.Point(2, 244);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox9.Size = new System.Drawing.Size(291, 130);
            this.groupBox9.TabIndex = 10;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Training";
            // 
            // startTraining
            // 
            this.startTraining.Location = new System.Drawing.Point(78, 18);
            this.startTraining.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.startTraining.Name = "startTraining";
            this.startTraining.Size = new System.Drawing.Size(137, 39);
            this.startTraining.TabIndex = 9;
            this.startTraining.Text = "Start Training";
            this.startTraining.UseVisualStyleBackColor = true;
            this.startTraining.Click += new System.EventHandler(this.startTraining_Click);
            // 
            // groupBox8
            // 
            this.tableLayoutPanel8.SetColumnSpan(this.groupBox8, 2);
            this.groupBox8.Controls.Add(this.button7);
            this.groupBox8.Controls.Add(this.trackBar1);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Location = new System.Drawing.Point(2, 73);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox8.Size = new System.Drawing.Size(287, 91);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Power control";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(76, 52);
            this.button7.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(137, 39);
            this.button7.TabIndex = 5;
            this.button7.Text = "Apply";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(22, 17);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.trackBar1.Maximum = 9;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(246, 56);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(235, 59);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 15);
            this.label12.TabIndex = 4;
            this.label12.Text = "100%";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(29, 59);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "10%";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(202, 43);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(109, 19);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Override active";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // button9
            // 
            this.button9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button9.BackColor = System.Drawing.Color.Red;
            this.button9.ForeColor = System.Drawing.SystemColors.Control;
            this.button9.Location = new System.Drawing.Point(99, 58);
            this.button9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(126, 85);
            this.button9.TabIndex = 10;
            this.button9.Text = "EMERGENCY BREAK";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(67, 4);
            // 
            // messager
            // 
            this.messager.ForeColor = System.Drawing.Color.Gray;
            this.messager.Location = new System.Drawing.Point(5, 259);
            this.messager.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.messager.MaxLength = 90;
            this.messager.Name = "messager";
            this.messager.Size = new System.Drawing.Size(219, 20);
            this.messager.TabIndex = 29;
            this.messager.Text = "Default text will show when run";
            this.messager.Click += new System.EventHandler(this.messager_Click);
            this.messager.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messager_KeyDown);
            this.messager.KeyUp += new System.Windows.Forms.KeyEventHandler(this.messager_KeyUp);
            this.messager.Leave += new System.EventHandler(this.messager_Leave);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(230, 258);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(137, 39);
            this.button5.TabIndex = 32;
            this.button5.Text = "Send";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // messages
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.messages, 2);
            this.messages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messages.FormattingEnabled = true;
            this.messages.Location = new System.Drawing.Point(5, 75);
            this.messages.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(362, 178);
            this.messages.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Target";
            // 
            // targets
            // 
            this.targets.FormattingEnabled = true;
            this.targets.Location = new System.Drawing.Point(92, 3);
            this.targets.Name = "targets";
            this.targets.Size = new System.Drawing.Size(263, 21);
            this.targets.TabIndex = 0;
            this.targets.SelectedIndexChanged += new System.EventHandler(this.targets_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 223F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143F));
            this.tableLayoutPanel1.Controls.Add(this.messages, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button5, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.messager, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(648, 41);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel3.SetRowSpan(this.tableLayoutPanel1, 3);
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.22244F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.22044F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.6879F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.47619F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.944445F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(359, 334);
            this.tableLayoutPanel1.TabIndex = 32;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.84076F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.15923F));
            this.tableLayoutPanel2.Controls.Add(this.targets, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 41);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(362, 29);
            this.tableLayoutPanel2.TabIndex = 33;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(362, 32);
            this.flowLayoutPanel1.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "MESSAGE A CLIENT";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 317F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 329F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 625F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox9, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel4, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel7, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel8, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel9, 1, 4);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 2, 4);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1015, 669);
            this.tableLayoutPanel3.TabIndex = 33;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.flowLayoutPanel4.Controls.Add(this.label8);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(319, 212);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(325, 28);
            this.flowLayoutPanel4.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(243, 25);
            this.label8.TabIndex = 3;
            this.label8.Text = "SHOW PATIENT DATA";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.refreshBikes, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.openLive, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.comboBox1, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(320, 245);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(323, 129);
            this.tableLayoutPanel6.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.label7, 2);
            this.label7.Location = new System.Drawing.Point(6, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(285, 15);
            this.label7.TabIndex = 1;
            this.label7.Text = "Choose A patient and click open to show more data";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.BackColor = System.Drawing.SystemColors.Highlight;
            this.tableLayoutPanel3.SetColumnSpan(this.flowLayoutPanel5, 3);
            this.flowLayoutPanel5.Controls.Add(this.label9);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(1267, 35);
            this.flowLayoutPanel5.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label9.Location = new System.Drawing.Point(3, 3);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(317, 36);
            this.label9.TabIndex = 4;
            this.label9.Text = "WELCOME DOCTOR";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.32249F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.67751F));
            this.tableLayoutPanel5.Controls.Add(this.loadbtn, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.historylist, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.openHistory, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(2, 379);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(313, 288);
            this.tableLayoutPanel5.TabIndex = 25;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanel5.SetColumnSpan(this.flowLayoutPanel3, 2);
            this.flowLayoutPanel3.Controls.Add(this.label5);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(309, 26);
            this.flowLayoutPanel3.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(172, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "LOAD HISTORY";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.label6, 2);
            this.label6.Location = new System.Drawing.Point(3, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Chose the History you want to review";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.051948F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.94805F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel7.Controls.Add(this.button9, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel6, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(319, 41);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.40078F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.59922F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(325, 167);
            this.tableLayoutPanel7.TabIndex = 36;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanel7.SetColumnSpan(this.flowLayoutPanel6, 4);
            this.flowLayoutPanel6.Controls.Add(this.label10);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(321, 31);
            this.flowLayoutPanel6.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label10.Location = new System.Drawing.Point(3, 3);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(225, 25);
            this.label10.TabIndex = 4;
            this.label10.Text = "EMERGENCY BREAK";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.flowLayoutPanel7, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.groupBox8, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.checkBox1, 1, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(2, 41);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(313, 167);
            this.tableLayoutPanel8.TabIndex = 37;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tableLayoutPanel8.SetColumnSpan(this.flowLayoutPanel7, 2);
            this.flowLayoutPanel7.Controls.Add(this.label13);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(309, 36);
            this.flowLayoutPanel7.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label13.Location = new System.Drawing.Point(3, 3);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(156, 25);
            this.label13.TabIndex = 5;
            this.label13.Text = "OVERRIDE VR";
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.flowLayoutPanel8.Controls.Add(this.label14);
            this.flowLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel8.Location = new System.Drawing.Point(2, 212);
            this.flowLayoutPanel8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(313, 28);
            this.flowLayoutPanel8.TabIndex = 38;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label14.Location = new System.Drawing.Point(3, 3);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(242, 25);
            this.label14.TabIndex = 5;
            this.label14.Text = "TRAINING PROGRAMS";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.button1, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.bikeCommand, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(319, 379);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 4;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.01923F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(325, 288);
            this.tableLayoutPanel9.TabIndex = 39;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(186, 97);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 39);
            this.button1.TabIndex = 22;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bikeCommand
            // 
            this.bikeCommand.ForeColor = System.Drawing.Color.Gray;
            this.bikeCommand.Location = new System.Drawing.Point(2, 72);
            this.bikeCommand.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.bikeCommand.MaxLength = 90;
            this.bikeCommand.Name = "bikeCommand";
            this.bikeCommand.Size = new System.Drawing.Size(321, 20);
            this.bikeCommand.TabIndex = 21;
            this.bikeCommand.Text = "Default text will show when run";
            this.bikeCommand.Click += new System.EventHandler(this.bikeCommand_Click);
            this.bikeCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bikeCommand_KeyDown);
            this.bikeCommand.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bikeCommand_KeyUp);
            this.bikeCommand.Leave += new System.EventHandler(this.bikeCommand_Leave);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(321, 44);
            this.flowLayoutPanel2.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "COMMAND BIKE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "Enter a command for the Bike";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NetworkConnector.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(648, 379);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(367, 288);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            // 
            // Doctor_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1028, 677);
            this.Controls.Add(this.tableLayoutPanel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Doctor_GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doctor_GUI";
            this.Load += new System.EventHandler(this.Doctor_GUI_Load);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.flowLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button refreshBikes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox messager;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox targets;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox messages;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ComboBox historylist;
        private System.Windows.Forms.Button loadbtn;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button openHistory;
        private System.Windows.Forms.Button openLive;
        private System.Windows.Forms.Button startTraining;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox bikeCommand;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}