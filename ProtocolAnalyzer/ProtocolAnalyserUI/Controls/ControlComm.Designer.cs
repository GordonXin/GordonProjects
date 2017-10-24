namespace ProtocolAnalyzerUI.Controls
{
    partial class ControlComm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbxIP = new ProtocolAnalyzerUI.Controls.ControlIPTextBox();
            this.controlCommMode1 = new ProtocolAnalyzerUI.Controls.ControlCommMode();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxPort
            // 
            this.tbxPort.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tbxPort.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbxPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPort.Location = new System.Drawing.Point(327, 12);
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.Size = new System.Drawing.Size(69, 26);
            this.tbxPort.TabIndex = 3;
            this.tbxPort.Text = "4096";
            this.tbxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbxIP);
            this.panel1.Controls.Add(this.controlCommMode1);
            this.panel1.Controls.Add(this.tbxPort);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 50);
            this.panel1.TabIndex = 0;
            this.panel1.Text = "Communication";
            // 
            // tbxIP
            // 
            this.tbxIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxIP.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbxIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxIP.Location = new System.Drawing.Point(96, 12);
            this.tbxIP.Name = "tbxIP";
            this.tbxIP.Size = new System.Drawing.Size(162, 26);
            this.tbxIP.TabIndex = 5;
            this.tbxIP.Text = "127.0.0.1";
            this.tbxIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // controlCommMode1
            // 
            this.controlCommMode1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.controlCommMode1.Location = new System.Drawing.Point(416, 6);
            this.controlCommMode1.Name = "controlCommMode1";
            this.controlCommMode1.Size = new System.Drawing.Size(155, 39);
            this.controlCommMode1.TabIndex = 4;
            // 
            // ControlComm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ControlComm";
            this.Size = new System.Drawing.Size(575, 50);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxPort;
        private ControlCommMode controlCommMode1;
        private ControlIPTextBox tbxIP;
    }
}
