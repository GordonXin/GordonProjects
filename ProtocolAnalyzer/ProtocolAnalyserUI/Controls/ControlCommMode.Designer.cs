namespace ProtocolAnalyzerUI.Controls
{
    partial class ControlCommMode
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
            this.rbnClient = new System.Windows.Forms.RadioButton();
            this.rbnServer = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbnClient
            // 
            this.rbnClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rbnClient.AutoSize = true;
            this.rbnClient.Checked = true;
            this.rbnClient.Location = new System.Drawing.Point(6, 11);
            this.rbnClient.Name = "rbnClient";
            this.rbnClient.Size = new System.Drawing.Size(64, 21);
            this.rbnClient.TabIndex = 0;
            this.rbnClient.TabStop = true;
            this.rbnClient.Text = "Client";
            this.rbnClient.UseVisualStyleBackColor = true;
            // 
            // rbnServer
            // 
            this.rbnServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rbnServer.AutoSize = true;
            this.rbnServer.Location = new System.Drawing.Point(80, 11);
            this.rbnServer.Name = "rbnServer";
            this.rbnServer.Size = new System.Drawing.Size(71, 21);
            this.rbnServer.TabIndex = 1;
            this.rbnServer.Text = "Server";
            this.rbnServer.UseVisualStyleBackColor = true;
            // 
            // ControlCommMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbnServer);
            this.Controls.Add(this.rbnClient);
            this.Name = "ControlCommMode";
            this.Size = new System.Drawing.Size(153, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbnClient;
        private System.Windows.Forms.RadioButton rbnServer;
    }
}
