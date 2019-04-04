using System.Windows.Forms;

namespace VRController
{
    partial class VRConnector_GUI
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
            this.ConnectionList = new System.Windows.Forms.ListView();
            this.Host = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.username = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Hack = new System.Windows.Forms.Button();
            this.killSwitch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectionList
            // 
            this.ConnectionList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Host,
            this.username,
            this.ID});
            this.tableLayoutPanel1.SetColumnSpan(this.ConnectionList, 2);
            this.ConnectionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionList.FullRowSelect = true;
            this.ConnectionList.GridLines = true;
            this.ConnectionList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ConnectionList.Location = new System.Drawing.Point(7, 55);
            this.ConnectionList.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ConnectionList.MultiSelect = false;
            this.ConnectionList.Name = "ConnectionList";
            this.ConnectionList.Size = new System.Drawing.Size(649, 167);
            this.ConnectionList.TabIndex = 0;
            this.ConnectionList.UseCompatibleStateImageBehavior = false;
            this.ConnectionList.View = System.Windows.Forms.View.Details;
            this.ConnectionList.SelectedIndexChanged += new System.EventHandler(this.ConnectionList_SelectedIndexChanged);
            // 
            // Host
            // 
            this.Host.Text = "HostNaam";
            this.Host.Width = 191;
            // 
            // username
            // 
            this.username.Text = "Gebruikersnaam";
            this.username.Width = 257;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 200;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(428, 242);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "REFRESH";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Location = new System.Drawing.Point(97, 242);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 39);
            this.button2.TabIndex = 2;
            this.button2.Text = "CONNECT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(431, 2);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 39);
            this.button3.TabIndex = 3;
            this.button3.Text = "SCAN OPEN";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.scanList);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(0, 2);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(129, 39);
            this.button4.TabIndex = 4;
            this.button4.Text = "OPEN SIM";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Hack
            // 
            this.Hack.Location = new System.Drawing.Point(142, 2);
            this.Hack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Hack.Name = "Hack";
            this.Hack.Size = new System.Drawing.Size(129, 39);
            this.Hack.TabIndex = 5;
            this.Hack.Text = "HACK";
            this.Hack.UseVisualStyleBackColor = true;
            this.Hack.Visible = false;
            this.Hack.Click += new System.EventHandler(this.button5_Click);
            // 
            // killSwitch
            // 
            this.killSwitch.Location = new System.Drawing.Point(291, 2);
            this.killSwitch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.killSwitch.Name = "killSwitch";
            this.killSwitch.Size = new System.Drawing.Size(129, 39);
            this.killSwitch.TabIndex = 6;
            this.killSwitch.Text = "KILL";
            this.killSwitch.UseVisualStyleBackColor = true;
            this.killSwitch.Visible = false;
            this.killSwitch.Click += new System.EventHandler(this.killSwitch_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.killSwitch);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.Hack);
            this.panel1.Controls.Add(this.button4);
            this.panel1.ForeColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(2, 298);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 1);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ConnectionList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.23932F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.54416F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.79202F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(663, 301);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.ForeColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(659, 45);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(327, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "CHOOSE A CONNECTION";
            // 
            // VRConnector_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(663, 301);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "VRConnector_GUI";
            this.Text = "CONNECT THAT MOTHERFOCKA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VRConnector_GUI_FormClosing);
            this.Load += new System.EventHandler(this.VRConnector_GUI_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ConnectionList;
        private System.Windows.Forms.ColumnHeader Host;
        private System.Windows.Forms.ColumnHeader username;
        private System.Windows.Forms.ColumnHeader ID;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button Hack;
        private Button killSwitch;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
    }
}