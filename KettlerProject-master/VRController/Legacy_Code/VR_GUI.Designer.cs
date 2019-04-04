namespace VRController
{
    partial class VR_GUI
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
            this.ConnLabel = new System.Windows.Forms.Label();
            this.Routebtn = new System.Windows.Forms.Button();
            this.nodeButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.nodes = new System.Windows.Forms.TreeView();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteNode = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.DrawTerrain = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnLabel
            // 
            this.ConnLabel.AutoSize = true;
            this.ConnLabel.Location = new System.Drawing.Point(44, 24);
            this.ConnLabel.Name = "ConnLabel";
            this.ConnLabel.Size = new System.Drawing.Size(365, 20);
            this.ConnLabel.TabIndex = 0;
            this.ConnLabel.Text = "Connected to {user}@{host} with session {session}";
            // 
            // Routebtn
            // 
            this.Routebtn.Location = new System.Drawing.Point(48, 86);
            this.Routebtn.Name = "Routebtn";
            this.Routebtn.Size = new System.Drawing.Size(206, 60);
            this.Routebtn.TabIndex = 1;
            this.Routebtn.Text = "Route toevoegen";
            this.Routebtn.UseVisualStyleBackColor = true;
            this.Routebtn.Click += new System.EventHandler(this.Routebtn_Click);
            // 
            // nodeButton
            // 
            this.nodeButton.Location = new System.Drawing.Point(796, 403);
            this.nodeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nodeButton.Name = "nodeButton";
            this.nodeButton.Size = new System.Drawing.Size(206, 60);
            this.nodeButton.TabIndex = 3;
            this.nodeButton.Text = "Refresh";
            this.nodeButton.UseVisualStyleBackColor = true;
            this.nodeButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(48, 195);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(206, 60);
            this.button3.TabIndex = 4;
            this.button3.Text = "Creeer Route";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(48, 266);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(206, 60);
            this.button4.TabIndex = 5;
            this.button4.Text = "Creeer Weg";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // nodes
            // 
            this.nodes.Location = new System.Drawing.Point(586, 24);
            this.nodes.Name = "nodes";
            this.nodes.Size = new System.Drawing.Size(405, 374);
            this.nodes.TabIndex = 6;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 25);
            this.trackBar1.Maximum = 24;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(194, 69);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.Value = 12;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Location = new System.Drawing.Point(333, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 100);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tijd";
            // 
            // deleteNode
            // 
            this.deleteNode.Location = new System.Drawing.Point(586, 403);
            this.deleteNode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deleteNode.Name = "deleteNode";
            this.deleteNode.Size = new System.Drawing.Size(206, 60);
            this.deleteNode.TabIndex = 10;
            this.deleteNode.Text = "Delete";
            this.deleteNode.UseVisualStyleBackColor = true;
            this.deleteNode.Click += new System.EventHandler(this.deleteNode_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 60);
            this.button1.TabIndex = 11;
            this.button1.Text = "Add Panel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(333, 266);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(206, 60);
            this.button2.TabIndex = 12;
            this.button2.Text = "DrawOn";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // DrawTerrain
            // 
            this.DrawTerrain.Location = new System.Drawing.Point(333, 195);
            this.DrawTerrain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DrawTerrain.Name = "DrawTerrain";
            this.DrawTerrain.Size = new System.Drawing.Size(206, 60);
            this.DrawTerrain.TabIndex = 13;
            this.DrawTerrain.Text = "DrawTerrain";
            this.DrawTerrain.UseVisualStyleBackColor = true;
            this.DrawTerrain.Click += new System.EventHandler(this.DrawTerrain_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(333, 338);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(206, 60);
            this.button5.TabIndex = 14;
            this.button5.Text = "LoadScene1";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // VR_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 502);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.DrawTerrain);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.deleteNode);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nodes);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.nodeButton);
            this.Controls.Add(this.Routebtn);
            this.Controls.Add(this.ConnLabel);
            this.Name = "VR_GUI";
            this.Text = "CONNECTION TO SIMULATOR";
            this.Load += new System.EventHandler(this.VR_GUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ConnLabel;
        private System.Windows.Forms.Button Routebtn;
        private System.Windows.Forms.Button nodeButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TreeView nodes;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button deleteNode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button DrawTerrain;
        private System.Windows.Forms.Button button5;
    }
}