namespace FixtureSimulator
{
    partial class frmFixtureSimulator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFixtureSimulator));
            this.btnConnA = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendSelectedMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDataFileSelect = new System.Windows.Forms.Button();
            this.txtDataFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSendDataMsg = new System.Windows.Forms.Button();
            this.chkSingleStep = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDataStatus = new System.Windows.Forms.Label();
            this.btnReadDataFile = new System.Windows.Forms.Button();
            this.txtMsgIdentifier = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSecsIAStatus = new System.Windows.Forms.Label();
            this.chkRunFast = new System.Windows.Forms.CheckBox();
            this.ChkRunContinuous = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbIsServer = new System.Windows.Forms.RadioButton();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.rbIsClient = new System.Windows.Forms.RadioButton();
            this.lblSleepStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFSAlarm = new System.Windows.Forms.RadioButton();
            this.rbFSAutomation = new System.Windows.Forms.RadioButton();
            this.rbFSIdle = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnA
            // 
            this.btnConnA.Location = new System.Drawing.Point(395, 9);
            this.btnConnA.Name = "btnConnA";
            this.btnConnA.Size = new System.Drawing.Size(93, 23);
            this.btnConnA.TabIndex = 9;
            this.btnConnA.Text = "Start Listening";
            this.btnConnA.UseVisualStyleBackColor = true;
            this.btnConnA.Click += new System.EventHandler(this.btnConnA_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(335, 10);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(48, 20);
            this.txtPort.TabIndex = 14;
            this.txtPort.Text = "4096";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(306, 14);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 13;
            this.lblPort.Text = "Port:";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(4, 6);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(632, 334);
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Message";
            this.columnHeader2.Width = 274;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearMessagesToolStripMenuItem,
            this.sendSelectedMessageToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(196, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // clearMessagesToolStripMenuItem
            // 
            this.clearMessagesToolStripMenuItem.Name = "clearMessagesToolStripMenuItem";
            this.clearMessagesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.clearMessagesToolStripMenuItem.Text = "Clear Messages";
            this.clearMessagesToolStripMenuItem.Click += new System.EventHandler(this.clearMessagesToolStripMenuItem_Click);
            // 
            // sendSelectedMessageToolStripMenuItem
            // 
            this.sendSelectedMessageToolStripMenuItem.Name = "sendSelectedMessageToolStripMenuItem";
            this.sendSelectedMessageToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.sendSelectedMessageToolStripMenuItem.Text = "Send selected Message";
            this.sendSelectedMessageToolStripMenuItem.Click += new System.EventHandler(this.sendSelectedMessageToolStripMenuItem_Click);
            // 
            // btnDataFileSelect
            // 
            this.btnDataFileSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDataFileSelect.Location = new System.Drawing.Point(599, 40);
            this.btnDataFileSelect.Name = "btnDataFileSelect";
            this.btnDataFileSelect.Size = new System.Drawing.Size(24, 23);
            this.btnDataFileSelect.TabIndex = 24;
            this.btnDataFileSelect.Text = "...";
            this.btnDataFileSelect.UseVisualStyleBackColor = true;
            this.btnDataFileSelect.Click += new System.EventHandler(this.btnDataFileSelect_Click);
            // 
            // txtDataFile
            // 
            this.txtDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataFile.Location = new System.Drawing.Point(83, 41);
            this.txtDataFile.Name = "txtDataFile";
            this.txtDataFile.ReadOnly = true;
            this.txtDataFile.Size = new System.Drawing.Size(510, 20);
            this.txtDataFile.TabIndex = 23;
            this.txtDataFile.TextChanged += new System.EventHandler(this.txtDataFile_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Data file:";
            // 
            // btnSendDataMsg
            // 
            this.btnSendDataMsg.Location = new System.Drawing.Point(490, 104);
            this.btnSendDataMsg.Name = "btnSendDataMsg";
            this.btnSendDataMsg.Size = new System.Drawing.Size(136, 23);
            this.btnSendDataMsg.TabIndex = 25;
            this.btnSendDataMsg.Text = "SendMsg";
            this.btnSendDataMsg.UseVisualStyleBackColor = true;
            this.btnSendDataMsg.Click += new System.EventHandler(this.btnSendDataMsg_Click);
            // 
            // chkSingleStep
            // 
            this.chkSingleStep.AutoSize = true;
            this.chkSingleStep.Checked = true;
            this.chkSingleStep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingleStep.Location = new System.Drawing.Point(83, 107);
            this.chkSingleStep.Name = "chkSingleStep";
            this.chkSingleStep.Size = new System.Drawing.Size(80, 17);
            this.chkSingleStep.TabIndex = 26;
            this.chkSingleStep.Text = "Single Step";
            this.chkSingleStep.UseVisualStyleBackColor = true;
            this.chkSingleStep.CheckedChanged += new System.EventHandler(this.chkSingleStep_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Status:";
            // 
            // lblDataStatus
            // 
            this.lblDataStatus.AutoSize = true;
            this.lblDataStatus.Location = new System.Drawing.Point(80, 136);
            this.lblDataStatus.Name = "lblDataStatus";
            this.lblDataStatus.Size = new System.Drawing.Size(69, 13);
            this.lblDataStatus.TabIndex = 28;
            this.lblDataStatus.Text = "Port Number:";
            // 
            // btnReadDataFile
            // 
            this.btnReadDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadDataFile.Location = new System.Drawing.Point(546, 70);
            this.btnReadDataFile.Name = "btnReadDataFile";
            this.btnReadDataFile.Size = new System.Drawing.Size(77, 23);
            this.btnReadDataFile.TabIndex = 29;
            this.btnReadDataFile.Text = "Read File";
            this.btnReadDataFile.UseVisualStyleBackColor = true;
            this.btnReadDataFile.Click += new System.EventHandler(this.btnReadDataFile_Click);
            // 
            // txtMsgIdentifier
            // 
            this.txtMsgIdentifier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsgIdentifier.Location = new System.Drawing.Point(83, 71);
            this.txtMsgIdentifier.Name = "txtMsgIdentifier";
            this.txtMsgIdentifier.Size = new System.Drawing.Size(407, 20);
            this.txtMsgIdentifier.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Msg Identifier:";
            // 
            // lblSecsIAStatus
            // 
            this.lblSecsIAStatus.AutoSize = true;
            this.lblSecsIAStatus.Location = new System.Drawing.Point(80, 150);
            this.lblSecsIAStatus.Name = "lblSecsIAStatus";
            this.lblSecsIAStatus.Size = new System.Drawing.Size(69, 13);
            this.lblSecsIAStatus.TabIndex = 32;
            this.lblSecsIAStatus.Text = "Port Number:";
            // 
            // chkRunFast
            // 
            this.chkRunFast.AutoSize = true;
            this.chkRunFast.Checked = true;
            this.chkRunFast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRunFast.Location = new System.Drawing.Point(297, 107);
            this.chkRunFast.Name = "chkRunFast";
            this.chkRunFast.Size = new System.Drawing.Size(66, 17);
            this.chkRunFast.TabIndex = 33;
            this.chkRunFast.Text = "Run fast";
            this.chkRunFast.UseVisualStyleBackColor = true;
            this.chkRunFast.CheckedChanged += new System.EventHandler(this.chkRunFast_CheckedChanged);
            // 
            // ChkRunContinuous
            // 
            this.ChkRunContinuous.AutoSize = true;
            this.ChkRunContinuous.Checked = true;
            this.ChkRunContinuous.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkRunContinuous.Location = new System.Drawing.Point(179, 107);
            this.ChkRunContinuous.Name = "ChkRunContinuous";
            this.ChkRunContinuous.Size = new System.Drawing.Size(102, 17);
            this.ChkRunContinuous.TabIndex = 34;
            this.ChkRunContinuous.Text = "Run Continuous";
            this.ChkRunContinuous.UseVisualStyleBackColor = true;
            this.ChkRunContinuous.CheckedChanged += new System.EventHandler(this.ChkRunContinuous_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(9, 181);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(659, 374);
            this.tabControl1.TabIndex = 41;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(651, 348);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Message History";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbIsServer);
            this.groupBox2.Controls.Add(this.txtIPAddress);
            this.groupBox2.Controls.Add(this.lblIPAddress);
            this.groupBox2.Controls.Add(this.rbIsClient);
            this.groupBox2.Controls.Add(this.btnConnA);
            this.groupBox2.Controls.Add(this.lblPort);
            this.groupBox2.Controls.Add(this.txtPort);
            this.groupBox2.Location = new System.Drawing.Point(2, -2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 35);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            // 
            // rbIsServer
            // 
            this.rbIsServer.AutoSize = true;
            this.rbIsServer.Location = new System.Drawing.Point(79, 12);
            this.rbIsServer.Name = "rbIsServer";
            this.rbIsServer.Size = new System.Drawing.Size(56, 17);
            this.rbIsServer.TabIndex = 46;
            this.rbIsServer.Text = "Server";
            this.rbIsServer.UseVisualStyleBackColor = true;
            this.rbIsServer.CheckedChanged += new System.EventHandler(this.rbIsClient_CheckedChanged);
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(206, 10);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(90, 20);
            this.txtIPAddress.TabIndex = 49;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(145, 14);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(61, 13);
            this.lblIPAddress.TabIndex = 48;
            this.lblIPAddress.Text = "IP Address:";
            // 
            // rbIsClient
            // 
            this.rbIsClient.AutoSize = true;
            this.rbIsClient.Checked = true;
            this.rbIsClient.Location = new System.Drawing.Point(13, 12);
            this.rbIsClient.Name = "rbIsClient";
            this.rbIsClient.Size = new System.Drawing.Size(51, 17);
            this.rbIsClient.TabIndex = 47;
            this.rbIsClient.TabStop = true;
            this.rbIsClient.Text = "Client";
            this.rbIsClient.UseVisualStyleBackColor = true;
            this.rbIsClient.CheckedChanged += new System.EventHandler(this.rbIsClient_CheckedChanged);
            // 
            // lblSleepStatus
            // 
            this.lblSleepStatus.AutoSize = true;
            this.lblSleepStatus.Location = new System.Drawing.Point(360, 136);
            this.lblSleepStatus.Name = "lblSleepStatus";
            this.lblSleepStatus.Size = new System.Drawing.Size(40, 13);
            this.lblSleepStatus.TabIndex = 47;
            this.lblSleepStatus.Text = "0 Secs";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFSAlarm);
            this.groupBox1.Controls.Add(this.rbFSAutomation);
            this.groupBox1.Controls.Add(this.rbFSIdle);
            this.groupBox1.Location = new System.Drawing.Point(360, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 35);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            // 
            // rbFSAlarm
            // 
            this.rbFSAlarm.AutoSize = true;
            this.rbFSAlarm.Location = new System.Drawing.Point(191, 12);
            this.rbFSAlarm.Name = "rbFSAlarm";
            this.rbFSAlarm.Size = new System.Drawing.Size(62, 17);
            this.rbFSAlarm.TabIndex = 48;
            this.rbFSAlarm.Text = "ALARM";
            this.rbFSAlarm.UseVisualStyleBackColor = true;
            this.rbFSAlarm.CheckedChanged += new System.EventHandler(this.rbFStatus_CheckedChanged);
            // 
            // rbFSAutomation
            // 
            this.rbFSAutomation.AutoSize = true;
            this.rbFSAutomation.Location = new System.Drawing.Point(78, 12);
            this.rbFSAutomation.Name = "rbFSAutomation";
            this.rbFSAutomation.Size = new System.Drawing.Size(97, 17);
            this.rbFSAutomation.TabIndex = 46;
            this.rbFSAutomation.Text = "AUTOMATION";
            this.rbFSAutomation.UseVisualStyleBackColor = true;
            this.rbFSAutomation.CheckedChanged += new System.EventHandler(this.rbFStatus_CheckedChanged);
            // 
            // rbFSIdle
            // 
            this.rbFSIdle.AutoSize = true;
            this.rbFSIdle.Checked = true;
            this.rbFSIdle.Location = new System.Drawing.Point(13, 12);
            this.rbFSIdle.Name = "rbFSIdle";
            this.rbFSIdle.Size = new System.Drawing.Size(49, 17);
            this.rbFSIdle.TabIndex = 47;
            this.rbFSIdle.TabStop = true;
            this.rbFSIdle.Text = "IDLE";
            this.rbFSIdle.UseVisualStyleBackColor = true;
            this.rbFSIdle.CheckedChanged += new System.EventHandler(this.rbFStatus_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(285, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Fixture Status:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(301, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "Wait Time:";
            // 
            // frmFixtureSimulator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(676, 558);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblSleepStatus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.chkRunFast);
            this.Controls.Add(this.ChkRunContinuous);
            this.Controls.Add(this.lblSecsIAStatus);
            this.Controls.Add(this.txtMsgIdentifier);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnReadDataFile);
            this.Controls.Add(this.lblDataStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkSingleStep);
            this.Controls.Add(this.btnDataFileSelect);
            this.Controls.Add(this.btnSendDataMsg);
            this.Controls.Add(this.txtDataFile);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFixtureSimulator";
            this.Text = "Fixture Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnA;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnDataFileSelect;
        private System.Windows.Forms.TextBox txtDataFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSendDataMsg;
        private System.Windows.Forms.CheckBox chkSingleStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDataStatus;
        private System.Windows.Forms.Button btnReadDataFile;
        private System.Windows.Forms.TextBox txtMsgIdentifier;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendSelectedMessageToolStripMenuItem;
        private System.Windows.Forms.Label lblSecsIAStatus;
        private System.Windows.Forms.CheckBox chkRunFast;
        private System.Windows.Forms.CheckBox ChkRunContinuous;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbIsServer;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.RadioButton rbIsClient;
        private System.Windows.Forms.Label lblSleepStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFSAlarm;
        private System.Windows.Forms.RadioButton rbFSAutomation;
        private System.Windows.Forms.RadioButton rbFSIdle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
    }
}

