namespace eMouse
{
    partial class eMouse
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
            this.debug = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.COM = new System.Windows.Forms.ComboBox();
            this.Baudrate = new System.Windows.Forms.TextBox();
            this.COM_lbl = new System.Windows.Forms.Label();
            this.Baudrate_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // debug
            // 
            this.debug.Location = new System.Drawing.Point(12, 12);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(255, 20);
            this.debug.TabIndex = 0;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(192, 51);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 1;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_OnClick);
            // 
            // COM
            // 
            this.COM.FormattingEnabled = true;
            this.COM.Location = new System.Drawing.Point(13, 54);
            this.COM.Name = "COM";
            this.COM.Size = new System.Drawing.Size(89, 21);
            this.COM.TabIndex = 2;
            // 
            // Baudrate
            // 
            this.Baudrate.Location = new System.Drawing.Point(109, 54);
            this.Baudrate.Name = "Baudrate";
            this.Baudrate.Size = new System.Drawing.Size(77, 20);
            this.Baudrate.TabIndex = 3;
            // 
            // COM_lbl
            // 
            this.COM_lbl.AutoSize = true;
            this.COM_lbl.Location = new System.Drawing.Point(13, 39);
            this.COM_lbl.Name = "COM_lbl";
            this.COM_lbl.Size = new System.Drawing.Size(58, 13);
            this.COM_lbl.TabIndex = 4;
            this.COM_lbl.Text = "COM Ports";
            // 
            // Baudrate_lbl
            // 
            this.Baudrate_lbl.AutoSize = true;
            this.Baudrate_lbl.Location = new System.Drawing.Point(109, 40);
            this.Baudrate_lbl.Name = "Baudrate_lbl";
            this.Baudrate_lbl.Size = new System.Drawing.Size(50, 13);
            this.Baudrate_lbl.TabIndex = 5;
            this.Baudrate_lbl.Text = "Baudrate";
            // 
            // eMouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 87);
            this.Controls.Add(this.Baudrate_lbl);
            this.Controls.Add(this.COM_lbl);
            this.Controls.Add(this.Baudrate);
            this.Controls.Add(this.COM);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.debug);
            this.Name = "eMouse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "eMouse";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.eMouse_OnClosing);
            this.Load += new System.EventHandler(this.eMouse_OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        public System.Windows.Forms.TextBox debug;
        private System.Windows.Forms.ComboBox COM;
        private System.Windows.Forms.TextBox Baudrate;
        private System.Windows.Forms.Label COM_lbl;
        private System.Windows.Forms.Label Baudrate_lbl;
    }
}

