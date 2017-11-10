using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Management;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace ProtocolTester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        #region manage Form init & dispose
        private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private GroupBox groupBox2;
        private RadioButton rbIsServer;
        private TextBox txtIPAddress;
        private Button btnConnA;
        private Label lblIPAddress;
        private Label lblPort;
        private TextBox txtPort;
        private RadioButton rbIsClient;
        private TextBox txtSndMsg;
        private Button btnSend;
        private Button btnTestMsgID;
        private Button btnTestCmdID;
        private Button btnGetConfig;
        private Button btnTestStatus;
        private Button btnTestReportEvent;
        private Button btnTestLocatoin;
        private Button btnTestResult;
        private Button btnTestKPI;
        private Button btnTestSequence;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbIsServer = new System.Windows.Forms.RadioButton();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.btnConnA = new System.Windows.Forms.Button();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.rbIsClient = new System.Windows.Forms.RadioButton();
            this.txtSndMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnTestMsgID = new System.Windows.Forms.Button();
            this.btnTestCmdID = new System.Windows.Forms.Button();
            this.btnGetConfig = new System.Windows.Forms.Button();
            this.btnTestStatus = new System.Windows.Forms.Button();
            this.btnTestReportEvent = new System.Windows.Forms.Button();
            this.btnTestLocatoin = new System.Windows.Forms.Button();
            this.btnTestResult = new System.Windows.Forms.Button();
            this.btnTestKPI = new System.Windows.Forms.Button();
            this.btnTestSequence = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(10, 175);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(694, 215);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Request";
            this.columnHeader2.Width = 135;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Response";
            this.columnHeader3.Width = 255;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbIsServer);
            this.groupBox2.Controls.Add(this.txtIPAddress);
            this.groupBox2.Controls.Add(this.btnConnA);
            this.groupBox2.Controls.Add(this.lblIPAddress);
            this.groupBox2.Controls.Add(this.lblPort);
            this.groupBox2.Controls.Add(this.txtPort);
            this.groupBox2.Controls.Add(this.rbIsClient);
            this.groupBox2.Location = new System.Drawing.Point(10, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(697, 41);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            // 
            // rbIsServer
            // 
            this.rbIsServer.AutoSize = true;
            this.rbIsServer.Location = new System.Drawing.Point(92, 13);
            this.rbIsServer.Name = "rbIsServer";
            this.rbIsServer.Size = new System.Drawing.Size(71, 21);
            this.rbIsServer.TabIndex = 34;
            this.rbIsServer.Text = "Server";
            this.rbIsServer.UseVisualStyleBackColor = true;
            this.rbIsServer.CheckedChanged += new System.EventHandler(this.rbIsClient_CheckedChanged);
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(245, 10);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(108, 22);
            this.txtIPAddress.TabIndex = 45;
            // 
            // btnConnA
            // 
            this.btnConnA.Location = new System.Drawing.Point(474, 9);
            this.btnConnA.Name = "btnConnA";
            this.btnConnA.Size = new System.Drawing.Size(112, 27);
            this.btnConnA.TabIndex = 9;
            this.btnConnA.Text = "Start Listening";
            this.btnConnA.UseVisualStyleBackColor = true;
            this.btnConnA.Click += new System.EventHandler(this.btnConnA_Click);
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(172, 15);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(80, 17);
            this.lblIPAddress.TabIndex = 44;
            this.lblIPAddress.Text = "IP Address:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(367, 15);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(38, 17);
            this.lblPort.TabIndex = 13;
            this.lblPort.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(406, 10);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(58, 22);
            this.txtPort.TabIndex = 14;
            this.txtPort.Text = "4096";
            // 
            // rbIsClient
            // 
            this.rbIsClient.AutoSize = true;
            this.rbIsClient.Checked = true;
            this.rbIsClient.Location = new System.Drawing.Point(13, 13);
            this.rbIsClient.Name = "rbIsClient";
            this.rbIsClient.Size = new System.Drawing.Size(64, 21);
            this.rbIsClient.TabIndex = 35;
            this.rbIsClient.TabStop = true;
            this.rbIsClient.Text = "Client";
            this.rbIsClient.UseVisualStyleBackColor = true;
            this.rbIsClient.CheckedChanged += new System.EventHandler(this.rbIsClient_CheckedChanged);
            // 
            // txtSndMsg
            // 
            this.txtSndMsg.Location = new System.Drawing.Point(23, 58);
            this.txtSndMsg.Name = "txtSndMsg";
            this.txtSndMsg.Size = new System.Drawing.Size(306, 22);
            this.txtSndMsg.TabIndex = 48;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(358, 58);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(112, 26);
            this.btnSend.TabIndex = 49;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnTestMsgID
            // 
            this.btnTestMsgID.Location = new System.Drawing.Point(23, 107);
            this.btnTestMsgID.Name = "btnTestMsgID";
            this.btnTestMsgID.Size = new System.Drawing.Size(114, 23);
            this.btnTestMsgID.TabIndex = 50;
            this.btnTestMsgID.Text = "TestMsgID";
            this.btnTestMsgID.UseVisualStyleBackColor = true;
            this.btnTestMsgID.Click += new System.EventHandler(this.btnTestMsgID_Click);
            // 
            // btnTestCmdID
            // 
            this.btnTestCmdID.Location = new System.Drawing.Point(143, 107);
            this.btnTestCmdID.Name = "btnTestCmdID";
            this.btnTestCmdID.Size = new System.Drawing.Size(114, 23);
            this.btnTestCmdID.TabIndex = 51;
            this.btnTestCmdID.Text = "TestCmdID";
            this.btnTestCmdID.UseVisualStyleBackColor = true;
            this.btnTestCmdID.Click += new System.EventHandler(this.btnTestCmdID_Click);
            // 
            // btnGetConfig
            // 
            this.btnGetConfig.Location = new System.Drawing.Point(263, 107);
            this.btnGetConfig.Name = "btnGetConfig";
            this.btnGetConfig.Size = new System.Drawing.Size(114, 23);
            this.btnGetConfig.TabIndex = 52;
            this.btnGetConfig.Text = "TestGetConfig";
            this.btnGetConfig.UseVisualStyleBackColor = true;
            this.btnGetConfig.Click += new System.EventHandler(this.btnGetConfig_Click);
            // 
            // btnTestStatus
            // 
            this.btnTestStatus.Location = new System.Drawing.Point(383, 107);
            this.btnTestStatus.Name = "btnTestStatus";
            this.btnTestStatus.Size = new System.Drawing.Size(114, 23);
            this.btnTestStatus.TabIndex = 53;
            this.btnTestStatus.Text = "TestGetStatus";
            this.btnTestStatus.UseVisualStyleBackColor = true;
            this.btnTestStatus.Click += new System.EventHandler(this.btnTestStatus_Click);
            // 
            // btnTestReportEvent
            // 
            this.btnTestReportEvent.Location = new System.Drawing.Point(503, 107);
            this.btnTestReportEvent.Name = "btnTestReportEvent";
            this.btnTestReportEvent.Size = new System.Drawing.Size(135, 23);
            this.btnTestReportEvent.TabIndex = 54;
            this.btnTestReportEvent.Text = "TestReportEvent";
            this.btnTestReportEvent.UseVisualStyleBackColor = true;
            this.btnTestReportEvent.Click += new System.EventHandler(this.btnTestReportEvent_Click);
            // 
            // btnTestLocatoin
            // 
            this.btnTestLocatoin.Location = new System.Drawing.Point(23, 136);
            this.btnTestLocatoin.Name = "btnTestLocatoin";
            this.btnTestLocatoin.Size = new System.Drawing.Size(150, 23);
            this.btnTestLocatoin.TabIndex = 55;
            this.btnTestLocatoin.Text = "TestReportLocation";
            this.btnTestLocatoin.UseVisualStyleBackColor = true;
            this.btnTestLocatoin.Click += new System.EventHandler(this.btnTestLocatoin_Click);
            // 
            // btnTestResult
            // 
            this.btnTestResult.Location = new System.Drawing.Point(185, 136);
            this.btnTestResult.Name = "btnTestResult";
            this.btnTestResult.Size = new System.Drawing.Size(114, 23);
            this.btnTestResult.TabIndex = 56;
            this.btnTestResult.Text = "TestReportResult";
            this.btnTestResult.UseVisualStyleBackColor = true;
            this.btnTestResult.Click += new System.EventHandler(this.btnTestResult_Click);
            // 
            // btnTestKPI
            // 
            this.btnTestKPI.Location = new System.Drawing.Point(305, 136);
            this.btnTestKPI.Name = "btnTestKPI";
            this.btnTestKPI.Size = new System.Drawing.Size(114, 23);
            this.btnTestKPI.TabIndex = 57;
            this.btnTestKPI.Text = "TestReportKPI";
            this.btnTestKPI.UseVisualStyleBackColor = true;
            this.btnTestKPI.Click += new System.EventHandler(this.btnTestKPI_Click);
            // 
            // btnTestSequence
            // 
            this.btnTestSequence.Location = new System.Drawing.Point(425, 136);
            this.btnTestSequence.Name = "btnTestSequence";
            this.btnTestSequence.Size = new System.Drawing.Size(114, 23);
            this.btnTestSequence.TabIndex = 58;
            this.btnTestSequence.Text = "TestSequence";
            this.btnTestSequence.UseVisualStyleBackColor = true;
            this.btnTestSequence.Click += new System.EventHandler(this.btnTestSequence_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(719, 406);
            this.Controls.Add(this.btnTestSequence);
            this.Controls.Add(this.btnTestKPI);
            this.Controls.Add(this.btnTestResult);
            this.Controls.Add(this.btnTestLocatoin);
            this.Controls.Add(this.btnTestReportEvent);
            this.Controls.Add(this.btnTestStatus);
            this.Controls.Add(this.btnGetConfig);
            this.Controls.Add(this.btnTestCmdID);
            this.Controls.Add(this.btnTestMsgID);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSndMsg);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Text = "Tester";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            rbIsServer.Checked = true;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sc != null)
                sc.Close();
            sc = null;
            //
            try
            {
                if (scSrv != null)
                    scSrv.StopServer();
            }
            finally
            {
                scSrv = null;
            }
        }
        #endregion

        #region manage logging
        private delegate void logInner(string title, string cmd, string response, Color textColor);
        private string LogAppName = "DM_SFW";
        private void log(string title, string cmd, string response, Color textColor)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new logInner(log), new Object[] { title, cmd, response, textColor });
                return;
            }
            try
            {
                string lstr1 = "[" +
                    System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00000") +
                    "][" + Sapphire.LogBug.GetUniqueTimeStamp() + "]::" +
                    cmd;
                Sapphire.LogBug.PrintIt(LogAppName, lstr1 + " : " + response); // Sapphire.Network.SocketClient20.makePrintable(response));
                //Sapphire.LogBug.PrintIt("DM_SFWT", lstr1);

                lock (listView1)
                {
                    while (listView1.Items.Count > 3000)
                        listView1.Items.RemoveAt(0);
                    ListViewItem it = new ListViewItem();
                    listView1.Items.Add(it);
                    it.SubItems.Add(cmd);
                    it.SubItems.Add(response);
                    it.Text = title;
                    it.Selected = true;
                    it.ForeColor = textColor;
                    listView1.EnsureVisible(listView1.Items.Count - 1);
                }
                Application.DoEvents();
            }
            catch (Exception)
            {
            }
        }
        private void log(string cmd, string response)
        {
            log(DateTime.Now.ToString("hh:mm:ss.fff tt"), cmd, response, listView1.ForeColor);
        }
        private void log(string cmd, string response, Color textColor)
        {
            log(DateTime.Now.ToString("hh:mm:ss.fff tt"), cmd, response, textColor);
        }
        #endregion

        #region manage socket type
        private void rbIsClient_CheckedChanged(object sender, EventArgs e)
        {
            ConnOptions();
        }
        private bool ConnOptionsInProcess = false;
        private void ConnOptions()
        {
            if (ConnOptionsInProcess == true) return;
            ConnOptionsInProcess = true;
            try
            {
                btnConnA.Enabled = true;
                if (rbIsServer.Checked == true)
                {
                    txtIPAddress.Enabled = false;
                    txtPort.Enabled = true;
                    if((scSrv == null) || (scSrv.vars["IsCommunicating"].boolValue() == false))
                    {
                        btnConnA.Text = "Start Listening";
                    }
                    else
                    {
                        btnConnA.Text = "Stop Listening";
                    }
                    //this.Text = "Protocol Tester [Server]";
                    _IsServer = true;
                }
                else
                {
                    txtIPAddress.Enabled = true;
                    txtPort.Enabled = true;
                    if ((sc == null) || (sc.vars["IsCommunicating"].boolValue() == false))
                    {
                        btnConnA.Text = "Connect";
                    }
                    else
                    {
                        btnConnA.Text = "Disconnect";
                    }
                    //this.Text = "Protocol Tester [Client]";
                    _IsServer = false;
                }
            }
            finally
            {
                ConnOptionsInProcess = false;
            }
        }
        private bool _IsServer = false;
        private bool IsServer
        {
            get
            {
                return _IsServer;
            }
        }
        #endregion

        #region manage Connection/listen
        private Sapphire.Network.SocketClient20 sc = null;
        private Sapphire.Network.SocketServer scSrv = null;
        private void btnConnA_Click(object sender, EventArgs e)
        {
            try
            {
                #region clean old objects
                if (sc != null)
                {
                    try
                    {
                        string conStr = sc.RemoteAddress + ":" + sc.RemotePort.ToString();
                        sc.DataBinaryReceived -= Sc_DataBinaryReceived;
                        sc.vars[Sapphire.Network.SocketClient20.VarInfo.IsCommunicating].ChangedEvt -=
                                                        new Sapphire.SAFW.EquipmentInfo.ParameterChanged(IsCommunicating_ChangedEvt);
                        sc.CompleteStop();
                        System.Threading.Thread.Sleep(1000);
                        log("Stopped connecion", " at " + conStr);
                    }
                    catch { }
                    sc = null;
                    ConnOptions();
                    return;
                }
                if (scSrv != null)
                {
                    string serverPort = scSrv.ServerPort.ToString();
                    scSrv.SetConnectionAcceptHandler(null);
                    scSrv.SetConnectionCloseHandler(null);
                    scSrv.SetInputHandler((Sapphire.Network.BinaryInputHandlerDelegate)null);
                    scSrv.LowLevelLog = false;
                    scSrv.AppName = LogAppName;
                    scSrv.vars["IsCommunicating"].ChangedEvt -= new Sapphire.SAFW.EquipmentInfo.ParameterChanged(ServerIsCommunicating_ChangedEvt);
                    scSrv.StopServer();
                    System.Threading.Thread.Sleep(1000);
                    scSrv = null;
                    ConnOptions();
                    log("StoptListen", "Stop listening on " + serverPort);
                    return;
                }
                #endregion
                if (rbIsClient.Checked==true)
                {
                    sc = new Sapphire.Network.SocketClient20("testclient");
                    //
                    sc.RemoteAddress = txtIPAddress.Text;
                    sc.RemotePort = Sapphire.SAFW.EquipmentInfo.ParameterValue.GetIntegerFromString(txtPort.Text, 1000);
                    sc.LogData = true;
                    sc.AppName = LogAppName;
                    sc.LowLevelLog = false;
                    sc.TryAutoConnect = false;
                    sc.comType = Sapphire.SAFW.Agent.ComType.SocketClientCom;
                    sc.DataBinaryReceived += Sc_DataBinaryReceived;
                    //sc.DataStringReceived += Sc_DataStringReceived;
                    sc.vars[Sapphire.Network.SocketClient20.VarInfo.IsCommunicating].ChangedEvt +=
                                                    new Sapphire.SAFW.EquipmentInfo.ParameterChanged(IsCommunicating_ChangedEvt);
                    _IsServer = false;
                }
                else
                {
                    scSrv = new Sapphire.Network.SocketServer("testsrv", "localhost", 
                                Sapphire.SAFW.EquipmentInfo.ParameterValue.GetIntegerFromString(txtPort.Text, 1000));
                    scSrv.comType = Sapphire.SAFW.Agent.ComType.SocketServerCom;
                    scSrv.SetConnectionAcceptHandler(Server_ClientConnected);
                    scSrv.SetConnectionCloseHandler(Server_ClientDisconnected);
                    scSrv.SetInputHandler(Server_DataReceived);
                    scSrv.LogData = true;
                    scSrv.LowLevelLog = false;
                    scSrv.AppName = LogAppName;
                    scSrv.vars["IsCommunicating"].ChangedEvt += new Sapphire.SAFW.EquipmentInfo.ParameterChanged(ServerIsCommunicating_ChangedEvt);
                    _IsServer = true;
                }

                Sapphire.SAFW.Threading.ReadIOService.QueueServiceRequest(
                                    new System.Threading.WaitCallback(OpenOnThread), null);
            }
            catch (Exception ex)
            {
                log("connect", "Excp=" + ex.Message);
            }
        }
        private void OpenOnThread(object stat)
        {
            if (sc != null)
            {
                if (sc.Open() == true)
                {
                    log("StartComm", "connected at " + sc.RemoteAddress + ":" + sc.RemotePort.ToString());
                    return;
                }
                log("StartComm", "Failed, " + sc.LastError);
            }
            if (scSrv != null)
            {
                if (scSrv.StartServer() == true)
                {
                    log("StartListen", "listening on "+scSrv.ServerPort.ToString());
                    return;
                }
                log("StartListen", "Failed, " + scSrv.vars["LastError"].stringValue());
            }
        }
        #endregion

        #region events for channels
        private void Sc_DataBinaryReceived(Sapphire.SAFW.Agent.IBaseCom source, byte[] DataBuffer, int len)
        {
            ReportDataReceived(DataBuffer, len);
        }
        void IsCommunicating_ChangedEvt(Sapphire.SAFW.EquipmentInfo.Parameter source, Sapphire.SAFW.EquipmentInfo.ParameterValue NewVal)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Sapphire.SAFW.EquipmentInfo.ParameterChanged(IsCommunicating_ChangedEvt), new Object[] { source, NewVal });
                return;
            }
            if (NewVal.boolValue() == true)
            {
                RemoteDidStartComm();
            }
            else
            {
                RemoteDidStopComm();
            }
            ConnOptions();
        }
        void ServerIsCommunicating_ChangedEvt(Sapphire.SAFW.EquipmentInfo.Parameter source, Sapphire.SAFW.EquipmentInfo.ParameterValue NewVal)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Sapphire.SAFW.EquipmentInfo.ParameterChanged(ServerIsCommunicating_ChangedEvt), new Object[] { source, NewVal });
                return;
            }
            ConnOptions();
        }
        private class ConnectedClient
        {
            public ConnectedClient(string lRemoteAddress, int lRemotePort)
            {
                Key = lRemoteAddress + ":" + lRemotePort.ToString();
                RemoteAddress = lRemoteAddress;
                RemotePort = lRemotePort;
            }
            public string Key = "";
            public string RemoteAddress = "";
            public int RemotePort = 0;
        }
        private ConnectedClient curClient = null;
        private void Server_ClientDisconnected(string address, int port)
        {
            try
            {
                log("Server_ClientDisconnected", "address = " + address + " port=" + port.ToString());
                if (curClient == null)
                    return;
                if ((curClient.RemoteAddress == address) && (curClient.RemotePort == port))
                {
                    curClient = null;
                    RemoteDidStopComm();
                    return;
                }
            }
            catch (Exception ex)
            {
                log("Server_ClientDisconnected", ex.ToString());
            }
        }
        private void Server_DataReceived(string sRemoteAddress, int nRemotePort, byte[] pData)
        {
            ReportDataReceived(pData, pData.Length);
        }
        private void Server_ClientConnected(string address, int port)
        {
            if (curClient != null)
            {
                log("Server_ClientConnected", "currently connected is Key=" + curClient.Key);
                curClient = null;
            }
            try
            {
                curClient = new ConnectedClient(address, port);
                log("Server_ClientConnected", "address = " + address + " port=" + port.ToString());

                RemoteDidStartComm();
            }
            catch (Exception ex)
            {
                log("Server_ClientConnected", ex.ToString());
            }
        }
        private void RemoteDidStartComm()
        {
        }
        private void RemoteDidStopComm()
        {
        }
        #endregion

        #region Send/Recv messsage
        //private int seqNum = 100;
        private string CR = "\r";
        private void ReportDataReceived(byte[] DataBuffer, int len)
        {
            System.Text.Encoding encode = System.Text.Encoding.GetEncoding("GB18030");
            string received = encode.GetString(DataBuffer, 0, len);

            log("Recv", received);
        }
        private byte[] GetBytesToSend(string message)
        {
            System.Text.Encoding encode = System.Text.Encoding.GetEncoding("GB18030");
            byte[] toSend = encode.GetBytes(message);
            return toSend;
        }
        private bool SendCommand(params object[] paras)
        {
            List<string> lst = new List<string>();
            if (paras != null)
            {
                foreach (object i in paras)
                {
                    lst.Add(i.ToString());
                }
            }
            return SendCommand(lst.ToArray());
        }
        private bool SendCommand(params string[] paras)
        {
            if (paras == null || paras.Length <= 0)
            {
                log("Send", "Failed because of null arguments");
                return false;
            }
            string msgSend = string.Join(",", paras);
            log("Send", msgSend);
            if (IsServer == false)
            {
                if (sc.Write(GetBytesToSend(msgSend + CR.ToString())) == false)
                {
                    log("Send", "Failed, " + sc.vars["LastError"].stringValue());
                    return false;
                }
            }
            else
            {
                if (curClient == null) return false;
                //
                if (scSrv.SendData(curClient.RemoteAddress, curClient.RemotePort,
                    GetBytesToSend(msgSend + CR.ToString())) == false)
                {
                    log("Send", "Failed, " + scSrv.vars["LastError"].stringValue());
                    return false;
                }
            }
            return true;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            SendCommand(txtSndMsg.Text);
        }
        #endregion

        #region [ Format UI]
        private delegate void FormatUIDelegateInner(bool bEnable);
        void EnableFormatUI(bool bEnable)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new FormatUIDelegateInner(this.EnableFormatUI), bEnable);
                return;
            }
            this.btnConnA.Enabled = bEnable;
            this.btnSend.Enabled = bEnable;
            this.rbIsClient.Enabled = bEnable;
            this.rbIsServer.Enabled = bEnable;
            this.btnTestMsgID.Enabled = bEnable;
            this.btnTestCmdID.Enabled = bEnable;
            this.btnTestStatus.Enabled = bEnable;
            this.btnTestReportEvent.Enabled = bEnable;
            this.btnGetConfig.Enabled = bEnable;
            this.btnTestLocatoin.Enabled = bEnable;
            this.btnTestResult.Enabled = bEnable;
            this.btnTestKPI.Enabled = bEnable;
            this.btnTestSequence.Enabled = bEnable;
        }
        #endregion

        #region [ Format Tests ]
        private void btnTestCmdID_Click(object sender, EventArgs e)
        {
            Task begin = new Task(() => { EnableFormatUI(false); Thread.Sleep(2000); });
            Task current = TestMsgCommandFormat(begin);
            current = current.ContinueWith((t) => { EnableFormatUI(true); });
            begin.Start();
        }
        private void btnTestMsgID_Click(object sender, EventArgs e)
        {
            Task begin = new Task(() => { EnableFormatUI(false); Thread.Sleep(2000); });
            Task current = TestMsgIDFormat(begin);
            current = current.ContinueWith((t) => { EnableFormatUI(true); });
            begin.Start();
        }
        private void btnGetConfig_Click(object sender, EventArgs e)
        {
            Task begin = new Task(() => { EnableFormatUI(false); Thread.Sleep(2000); });
            Task current = TestGetConfig(begin);
            current = current.ContinueWith((t) => { EnableFormatUI(true); });
            begin.Start();
        }
        private void btnTestStatus_Click(object sender, EventArgs e)
        {
            Task begin = new Task(() => { EnableFormatUI(false); Thread.Sleep(2000); });
            Task current = TestGetStatus(begin);
            current = current.ContinueWith((t) => { EnableFormatUI(true); });
            begin.Start();
        }
        private void btnTestReportEvent_Click(object sender, EventArgs e)
        {
            Task begin = new Task(() => { EnableFormatUI(false); Thread.Sleep(2000); });
            Task current = TestReportEvent(begin);
            current = current.ContinueWith((t) => { EnableFormatUI(true); });
            begin.Start();
        }
        private void btnTestLocatoin_Click(object sender, EventArgs e)
        {
            Task begin = new Task(() => { EnableFormatUI(false); Thread.Sleep(2000); });
            Task current = TestReportUnitLocation(begin);
            current = current.ContinueWith((t) => { EnableFormatUI(true); });
            begin.Start();
        }
        private void btnTestResult_Click(object sender, EventArgs e)
        {
            Task begin = new Task(() => { EnableFormatUI(false); Thread.Sleep(2000); });
            Task current = TestReportResult(begin);
            current = current.ContinueWith((t) => { EnableFormatUI(true); });
            begin.Start();
        }
        private void btnTestKPI_Click(object sender, EventArgs e)
        {
            Task begin = new Task(() => { EnableFormatUI(false); Thread.Sleep(2000); });
            Task current = TestKPI(begin);
            current = current.ContinueWith((t) => { EnableFormatUI(true); });
            begin.Start();
        }

        private void btnTestSequence_Click(object sender, EventArgs e)
        {
            Task begin = new Task(() => { EnableFormatUI(false); Thread.Sleep(2000); });
            Task current = TestSequence(begin);
            current = current.ContinueWith((t) => { EnableFormatUI(true); });
            begin.Start();
        }
        private void Delay(int ms = 2000)
        {
            Thread.Sleep(2000);
        }
        private Task TestMsgIDFormat(Task begin)
        {
            return begin.ContinueWith((t) => {
                SendCommand(0, "UPDATE_STATUS", "AUTOMATION", 0, 0);
                Delay();
                SendCommand(1, "UPDATE_STATUS", "AUTOMATION", 0, 0);
                Delay();
                SendCommand(100, "UPDATE_STATUS", "AUTOMATION", 0, 0);
                Delay();
                SendCommand(101, "UPDATE_STATUS", "AUTOMATION", 0, 0);
                Delay();
                SendCommand(200, "UPDATE_STATUS", "AUTOMATION", 0, 0);
                Delay();
                SendCommand("abc", "UPDATE_STATUS", "AUTOMATION", 0, 0);
                Delay();
            });
        }
        private Task TestMsgCommandFormat(Task begin)
        {
            return begin.ContinueWith((t) => {

                int idx = 1;
                SendCommand(idx++, "GETCONFIG", 0, 0);
                Delay();
                SendCommand(idx++, "GET_CONFIG", 0, 0);
                Delay();

                SendCommand(idx++, "GETSTATUS", 0, 0);
                Delay();
                SendCommand(idx++, "GET_STATUS", 0, 0);
                Delay();

                SendCommand(idx++, "UPDATESTATUS", "AUTOMATION", 0, 0);
                Delay();
                SendCommand(idx++, "UPDATESTATUS", "AUTOMATION", 0, 0);
                Delay();
                SendCommand(idx++, "UPDATE_STATUS", "AUTOMATION", 0, 0);
                Delay();

                SendCommand(idx++, "REPORTEVENT", "", "", "", "", "");
                Delay();
                SendCommand(idx++, "REPORT_Event", "", "", "", "", "");
                Delay();
                SendCommand(idx++, "REPORT_EVENT", "", "", "", "", "");
                Delay();

                SendCommand(idx++, "REPORTUNIT_LOCATION", "", "");
                Delay();
                SendCommand(idx++, "REPORT_Unit_LOCATION", "", "");
                Delay();
                SendCommand(idx++, "REPORT_UNIT_LOCATION", "", "");
                Delay();

                SendCommand(idx++, "REPORTRESULT", "", "", "", "", "", "", "");
                Delay();
                SendCommand(idx++, "REPORT_Result", "", "", "", "", "", "", "");
                Delay();
                SendCommand(idx++, "REPORT_RESULT", "", "", "", "", "", "", "");
                Delay();

                SendCommand(idx++, "REPORTKPI", "", "", "", "", "", "");
                Delay();
                SendCommand(idx++, "REPORT_Kpi", "", "", "", "", "", "");
                Delay();
                SendCommand(idx++, "REPORT_KPI", "", "", "", "", "", "");
                Delay();
            });
        }
        private Task TestGetConfig(Task begin)
        {
            // GET_CONFIG,DEV_TYPE,DEV_OS_VER,DEV_APP_SW, PROTO_VER,FC_ID,DATE,TIME
            return begin.ContinueWith((t) => {
                SendCommand(0, "GET_CONFIG");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1", "FC1");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1", "FC1", "11/05/2017");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1", "FC1", "11/05/2017", "15:00:00");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1", "FC1", "11/5/2017", "15:00:00");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1", "FC1", "11-05-2017", "15:00:00");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1", "FC1", "2017/5/30", "15:00:00");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1", "FC1", "30/5/2017", "15:00:00");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1", "FC1", "11/05/2017", "15:00");
                Delay();
                SendCommand(0, "GET_CONFIG", "BCM", "Win7", "1.0", "0.1", "FC1", "11/05/2017", "3:00 PM");
                Delay();
            });
        }
        private Task TestGetStatus(Task begin)
        {
            // UPDATE_STATUS, FC_STATE,IO_BIT_MAP,ERROR_BIT_MAP
            return begin.ContinueWith((t) => {
                SendCommand(0, "GET_STATUS");
                Delay();
                SendCommand(0, "UPDATE_STATUS", "AUTOMATION");
                Delay();
                SendCommand(0, "UPDATE_STATUS", "AUTOMATION", 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "AUTOMATION", 0, 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "IDLE", 0, 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "MANUAL", 0, 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "ALARM", 0, 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "Automation", 0, 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "Idle", 0, 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "Manual", 0, 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "Alarm", 0, 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "AUTOMATION", "00ABC", 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "AUTOMATION", "00abc", 0);
                Delay();
                SendCommand(0, "UPDATE_STATUS", "AUTOMATION", "00HTC", 0);
                Delay();
            });
        }
        private Task TestReportEvent(Task begin)
        {
            // 0,REPORT_EVENT,UUT_ID,UUT_LOCATION,EVT_ID,EVT_TYPE,EVT
            return begin.ContinueWith((t) => {
                SendCommand(0, "REPORT_EVENT");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "ALARM");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "ALARM", "This one should be good");
                Delay();
                SendCommand(0, "REPORT_EVENT", "", "DISPENSE_STA.LOAD", "1", "ALARM", "Empty UUT ID");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "", "1", "ALARM", "Empty Location");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.Load", "1", "ALARM", "Wrong Location");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "", "ALARM", "Empty event id");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "abc", "ALARM", "Wrong event id");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "USER", "This one should be good");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "User", "Wrong Event Type");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "INFO", "This one should be good");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "Info", "Wrong Event Type");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "WARNING", "This one should be good");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "WARN", "Wrong Event Type");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "Warning", "Wrong Event Type");
                Delay();
                SendCommand(0, "REPORT_EVENT", "abc", "DISPENSE_STA.LOAD", "1", "WARNING", "");
                Delay();
            });
        }
        private Task TestReportUnitLocation(Task begin)
        {
            // 0,REPORT_UNIT_LOCATION,UUT_ID,UUT_LOCATION
            return begin.ContinueWith((t) => {
                SendCommand(0, "REPORT_UNIT_LOCATION");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.LOAD");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.DISPENSE");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.ASSY_RIGHT_COIL");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.ASSY_LEFT_COIL");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.HOLD");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.UNLOAD");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.Load");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.Dispense");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.Assy_right");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.Assy_left");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.hold");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "DISPENSE_STA.Unload");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "", "DISPENSE_STA.LOAD");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "abc", "");
                Delay();
            });
        }
        private Task TestReportResult(Task begin)
        {
            // 0,REPORT_RESULT,UUT_ID,UNIT_RESULT,START_TIME,END_TIME,UUT_LOCATION,GRADE_CODE,MESSAGE
            return begin.ContinueWith((t) => {
                SendCommand(0, "REPORT_RESULT");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "05/15/2017 15:00:00");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "05/15/2017 16:00:00", "05/15/2017 15:00:00", "DISPENSE_STA.HOLD", "", "wrong time order");
                Delay();
                SendCommand(0, "REPORT_RESULT", "", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "Empty UUT ID");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "FAIL", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "Fail result");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "Pass", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "Wrong result");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "Fail", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "Wrong result");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "05/15/201715:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "Wrong time");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "15/05/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "Wrong time");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "2017-05-15 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "Wrong time");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "", "", "Empty location");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.Hold", "", "Wrong location");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "FAIL", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "Fail with empty grade");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "FAIL", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "abc", "Fail with wrong grade");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "FAIL", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "11", "");
                Delay();
                SendCommand(0, "REPORT_RESULT", "abc", "FAIL", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.LOAD", "11", "Should not have result");
                Delay();
            });
        }
        private Task TestKPI(Task begin)
        {
            // 0,REPORT_KPI,UUT_ID,KPI_NAME,UNITS,MIN,MAX,VALUE,…
            return begin.ContinueWith((t) => {
                SendCommand(0, "REPORT_KPI");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force", "kg");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force", "kg", "3.5");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force", "kg", "3.5", "4.5");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force", "kg", "3.5", "4.5", "4.0");
                Delay();
                SendCommand(0, "REPORT_KPI", "", "Coil_holding_force", "kg", "3.5", "4.5", "4.0");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force", "g", "3.5", "4.5", "4.0");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force", "kg", "", "4.5", "4.0");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force", "kg", "3.5", "", "4.0");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force", "kg", "3.5", "4.6", "");
                Delay();
                SendCommand(0, "REPORT_KPI", "abc", "Coil_holding_force", "kg", "3.5", "4.6", "4.0", "Coil_holding_force");
                Delay();
            });
        }
        private Task TestSequence(Task begin)
        {
            return begin.ContinueWith((t) => {
                // good sequence
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.LOAD");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.DISPENSE");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.ASSY_RIGHT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.ASSY_LEFT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.HOLD");
                //Delay();
                //SendCommand(0, "REPORT_RESULT", "GoodSequence", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "");
                //Delay();
                //SendCommand(0, "REPORT_KPI", "GoodSequence", "Coil_holding_force", "kg", "3.5", "4.5", "4.0");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.UNLOAD");
                //Delay();

                // wrong order
                //SendCommand(0, "REPORT_UNIT_LOCATION", "WrongLocationOrder", "DISPENSE_STA.LOAD");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "WrongLocationOrder", "DISPENSE_STA.ASSY_RIGHT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "WrongLocationOrder", "DISPENSE_STA.DISPENSE");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "WrongLocationOrder", "DISPENSE_STA.ASSY_LEFT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "WrongLocationOrder", "DISPENSE_STA.HOLD");
                //Delay();
                //SendCommand(0, "REPORT_RESULT", "WrongLocationOrder", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "");
                //Delay();
                //SendCommand(0, "REPORT_KPI", "WrongLocationOrder", "Coil_holding_force", "kg", "3.5", "4.5", "4.0");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "WrongLocationOrder", "DISPENSE_STA.UNLOAD");
                //Delay();

                // Missing Load
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingLoad", "DISPENSE_STA.DISPENSE");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingLoad", "DISPENSE_STA.ASSY_RIGHT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingLoad", "DISPENSE_STA.ASSY_LEFT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingLoad", "DISPENSE_STA.HOLD");
                //Delay();
                //SendCommand(0, "REPORT_RESULT", "MissingLoad", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "");
                //Delay();
                //SendCommand(0, "REPORT_KPI", "MissingLoad", "Coil_holding_force", "kg", "3.5", "4.5", "4.0");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingLoad", "DISPENSE_STA.UNLOAD");
                //Delay();

                // missing result
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingResult", "DISPENSE_STA.LOAD");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingResult", "DISPENSE_STA.DISPENSE");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingResult", "DISPENSE_STA.ASSY_RIGHT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingResult", "DISPENSE_STA.ASSY_LEFT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingResult", "DISPENSE_STA.HOLD");
                //Delay();
                //SendCommand(0, "REPORT_KPI", "MissingResult", "Coil_holding_force", "kg", "3.5", "4.5", "4.0");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "MissingResult", "DISPENSE_STA.UNLOAD");
                //Delay();

                // Report Result at Wrong location
                //SendCommand(0, "REPORT_UNIT_LOCATION", "ReportResultAtWrongLocation", "DISPENSE_STA.LOAD");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "ReportResultAtWrongLocation", "DISPENSE_STA.DISPENSE");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "ReportResultAtWrongLocation", "DISPENSE_STA.ASSY_RIGHT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "ReportResultAtWrongLocation", "DISPENSE_STA.ASSY_LEFT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_RESULT", "ReportResultAtWrongLocation", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.ASSY_LEFT_COIL", "", "");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "ReportResultAtWrongLocation", "DISPENSE_STA.HOLD");
                //Delay();
                //SendCommand(0, "REPORT_KPI", "ReportResultAtWrongLocation", "Coil_holding_force", "kg", "3.5", "4.5", "4.0");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "ReportResultAtWrongLocation", "DISPENSE_STA.UNLOAD");
                //Delay();

                // Report Result with wrong location
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.LOAD");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.DISPENSE");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.ASSY_RIGHT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.ASSY_LEFT_COIL");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.HOLD");
                //Delay();
                //SendCommand(0, "REPORT_RESULT", "GoodSequence", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.ASSY_LEFT_COIL", "", "");
                //Delay();
                //SendCommand(0, "REPORT_KPI", "GoodSequence", "Coil_holding_force", "kg", "3.5", "4.5", "4.0");
                //Delay();
                //SendCommand(0, "REPORT_UNIT_LOCATION", "GoodSequence", "DISPENSE_STA.UNLOAD");
                //Delay();

                // Reload aborted UUT
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.LOAD");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.DISPENSE");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.ASSY_RIGHT_COIL");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.ASSY_LEFT_COIL");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.HOLD");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.LOAD");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.DISPENSE");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.ASSY_RIGHT_COIL");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.ASSY_LEFT_COIL");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.HOLD");
                Delay();
                SendCommand(0, "REPORT_RESULT", "ReloadAbortedUnit", "PASS", "05/15/2017 15:00:00", "05/15/2017 16:00:00", "DISPENSE_STA.HOLD", "", "");
                Delay();
                SendCommand(0, "REPORT_KPI", "ReloadAbortedUnit", "Coil_holding_force", "kg", "3.5", "4.5", "4.0");
                Delay();
                SendCommand(0, "REPORT_UNIT_LOCATION", "ReloadAbortedUnit", "DISPENSE_STA.UNLOAD");
                Delay();
            });
        }
        #endregion
    }
}
