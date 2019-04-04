namespace NetworkConnector
{
    partial class ServerClientGUI
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
            this.connectedView = new System.Windows.Forms.TreeView();
            this.errorList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connectedView
            // 
            this.connectedView.Location = new System.Drawing.Point(-2, 3);
            this.connectedView.Name = "connectedView";
            this.connectedView.Size = new System.Drawing.Size(206, 458);
            this.connectedView.TabIndex = 0;
            // 
            // errorList
            // 
            this.errorList.FormattingEnabled = true;
            this.errorList.ItemHeight = 20;
            this.errorList.Location = new System.Drawing.Point(306, 3);
            this.errorList.Name = "errorList";
            this.errorList.Size = new System.Drawing.Size(420, 464);
            this.errorList.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(-2, 467);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 60);
            this.button1.TabIndex = 2;
            this.button1.Text = "Verwijder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ServerClientGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 543);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.errorList);
            this.Controls.Add(this.connectedView);
            this.Name = "ServerClientGUI";
            this.Text = "ServerClientGUI";
            this.Load += new System.EventHandler(this.ServerClientGUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView connectedView;
        private System.Windows.Forms.ListBox errorList;
        private System.Windows.Forms.Button button1;
    }
}