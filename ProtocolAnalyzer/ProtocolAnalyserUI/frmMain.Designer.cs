namespace ProtocolAnalyzerUI
{
    partial class frmMain
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
            this.panelControl = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panelDisplay = new System.Windows.Forms.Panel();
            this.tbxMessages = new System.Windows.Forms.RichTextBox();
            this.filePathControl = new ProtocolAnalyzerUI.Controls.ControlFilePath();
            this.commControl = new ProtocolAnalyzerUI.Controls.ControlComm();
            this.panelControl.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.groupBox3);
            this.panelControl.Controls.Add(this.groupBox2);
            this.panelControl.Controls.Add(this.groupBox1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(840, 192);
            this.panelControl.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.filePathControl);
            this.groupBox3.Location = new System.Drawing.Point(38, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(547, 79);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Definitions";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.commControl);
            this.groupBox2.Location = new System.Drawing.Point(35, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(550, 80);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Communication";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(608, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 166);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(35, 93);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 32);
            this.button3.TabIndex = 2;
            this.button3.Text = "Clean";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelDisplay
            // 
            this.panelDisplay.Controls.Add(this.tbxMessages);
            this.panelDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDisplay.Location = new System.Drawing.Point(0, 192);
            this.panelDisplay.Name = "panelDisplay";
            this.panelDisplay.Size = new System.Drawing.Size(840, 390);
            this.panelDisplay.TabIndex = 1;
            // 
            // tbxMessages
            // 
            this.tbxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxMessages.Location = new System.Drawing.Point(35, 6);
            this.tbxMessages.Name = "tbxMessages";
            this.tbxMessages.Size = new System.Drawing.Size(773, 359);
            this.tbxMessages.TabIndex = 0;
            this.tbxMessages.Text = "";
            // 
            // filePathControl
            // 
            this.filePathControl.BrowseFolder = null;
            this.filePathControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filePathControl.FilePath = "File Path";
            this.filePathControl.Location = new System.Drawing.Point(3, 18);
            this.filePathControl.Name = "filePathControl";
            this.filePathControl.Size = new System.Drawing.Size(541, 58);
            this.filePathControl.TabIndex = 0;
            this.filePathControl.Title = "Definition File:";
            // 
            // commControl
            // 
            this.commControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commControl.EnablePing = false;
            this.commControl.Location = new System.Drawing.Point(3, 18);
            this.commControl.Name = "commControl";
            this.commControl.Size = new System.Drawing.Size(544, 59);
            this.commControl.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 582);
            this.Controls.Add(this.panelDisplay);
            this.Controls.Add(this.panelControl);
            this.Name = "frmMain";
            this.Text = "Protocol Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.panelControl.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panelDisplay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Panel panelDisplay;
        private System.Windows.Forms.RichTextBox tbxMessages;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ProtocolAnalyzerUI.Controls.ControlComm commControl;
        private System.Windows.Forms.GroupBox groupBox3;
        private ProtocolAnalyzerUI.Controls.ControlFilePath filePathControl;
    }
}

