namespace WIP_MOBA_Server
{
    partial class ServerClient
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
            this.lTimer = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lClient2 = new System.Windows.Forms.Label();
            this.lClient1 = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.textboxConsole = new System.Windows.Forms.RichTextBox();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lTimer
            // 
            this.lTimer.AutoSize = true;
            this.lTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTimer.Location = new System.Drawing.Point(148, 98);
            this.lTimer.Name = "lTimer";
            this.lTimer.Size = new System.Drawing.Size(54, 25);
            this.lTimer.TabIndex = 6;
            this.lTimer.Text = "3:00";
            this.lTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.lClient2);
            this.GroupBox1.Controls.Add(this.lClient1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(326, 69);
            this.GroupBox1.TabIndex = 5;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Connected Clients";
            // 
            // lClient2
            // 
            this.lClient2.AutoSize = true;
            this.lClient2.Location = new System.Drawing.Point(7, 40);
            this.lClient2.Name = "lClient2";
            this.lClient2.Size = new System.Drawing.Size(48, 13);
            this.lClient2.TabIndex = 1;
            this.lClient2.Text = "Client 2: ";
            // 
            // lClient1
            // 
            this.lClient1.AutoSize = true;
            this.lClient1.Location = new System.Drawing.Point(7, 20);
            this.lClient1.Name = "lClient1";
            this.lClient1.Size = new System.Drawing.Size(48, 13);
            this.lClient1.TabIndex = 0;
            this.lClient1.Text = "Client 1: ";
            // 
            // bStart
            // 
            this.bStart.Enabled = false;
            this.bStart.Location = new System.Drawing.Point(12, 137);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(327, 23);
            this.bStart.TabIndex = 4;
            this.bStart.Text = "Start Game";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // textboxConsole
            // 
            this.textboxConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxConsole.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textboxConsole.Location = new System.Drawing.Point(12, 167);
            this.textboxConsole.Name = "textboxConsole";
            this.textboxConsole.Size = new System.Drawing.Size(327, 161);
            this.textboxConsole.TabIndex = 7;
            this.textboxConsole.Text = "";
            // 
            // ServerClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 340);
            this.Controls.Add(this.lTimer);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.textboxConsole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerClient";
            this.ShowIcon = false;
            this.Text = "WIP Server GUI";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lTimer;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label lClient2;
        internal System.Windows.Forms.Label lClient1;
        internal System.Windows.Forms.Button bStart;
        internal System.Windows.Forms.RichTextBox textboxConsole;
    }
}

