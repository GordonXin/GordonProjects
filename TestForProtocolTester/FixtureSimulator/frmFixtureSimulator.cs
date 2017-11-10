using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLite.Net.Attributes;

namespace FixtureSimulator
{
    public partial class frmFixtureSimulator : Form
    {
        public frmFixtureSimulator()
        {
            InitializeComponent();
            lblDataStatus.Text = "";
            lblSecsIAStatus.Text = "";
            rbIsServer.Checked = false;
            rbIsClient.Checked = true;
        }
        #region [ log ]
        private string _LogAppName = "DM_SFW";
        public string LogAppName
        {
            get
            { return _LogAppName; }
            set
            { _LogAppName = value; }
        }
        protected virtual void log(string strfuncName, string strMsg)
        {
            string lstr = "[" +
                System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00000") +
                "][" + Sapphire.Clock.GetUniqueTimeStamp() + "] :" + strfuncName + " : " + strMsg;
            Sapphire.LogBug.PrintIt(LogAppName, lstr);
        }
        //
        private delegate void logSentMsgHdrInfoInner(string devId, string srcId, string transId);
        private object logSentMsgHdrInfoLock = new object();
        private delegate void logSentMsgDirInfoInner(bool IsEqToHost);
        private delegate void logInner(string response, object ld);
        private object listView1Lock = new object();
        private void logClient(string response, object ld)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new logInner(logClient), new Object[] { response, ld });
                return;
            }
            try
            {
                lock (listView1Lock)
                {
                    while (listView1.Items.Count > 3000)
                    {
                        listView1.Items.RemoveAt(0);
                        System.Threading.Thread.Sleep(100);
                    }
                    ListViewItem it = new ListViewItem();
                    listView1.Items.Add(it);
                    it.SubItems.Add(response);
                    it.Text = DateTime.Now.ToString("hh:mm:ss.fff tt");
                    it.Selected = true;
                    it.Tag = ld;
                    listView1.EnsureVisible(listView1.Items.Count - 1);
                }
                Application.DoEvents();
            }
            catch (Exception)
            {
            }
        }
        private delegate void SendMsgButLableDelegate(string lblText);
        private void SendMsgButLable(string lblText)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new SendMsgButLableDelegate(SendMsgButLable), new Object[] { lblText });
                return;
            }
            btnSendDataMsg.Text = lblText;
            Application.DoEvents();
        }
        private void SetLblDataStatusText(string lblText)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new SendMsgButLableDelegate(SetLblDataStatusText), new Object[] { lblText });
                return;
            }
            lblDataStatus.Text = lblText;
            lblDataStatus.Refresh();
            //Application.DoEvents();
        }
        private void SetLblSleepStatusStatusText(string lblText)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new SendMsgButLableDelegate(SetLblSleepStatusStatusText), new Object[] { lblText });
                return;
            }
            lblSleepStatus.Text = lblText;
            Application.DoEvents();
        }
        private delegate void SetSendMessageStatusDelegate(bool enableOp);
        private void SetSendMessageStatus(bool enableOp)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new SetSendMessageStatusDelegate(SetSendMessageStatus), new Object[] { enableOp });
                return;
            }
            btnReadDataFile.Enabled = enableOp;
            btnDataFileSelect.Enabled = enableOp;
            Application.DoEvents();
        }
        private void SettxtMsgIdentifier(string txt)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new SendMsgButLableDelegate(SettxtMsgIdentifier), new Object[] { txt });
                return;
            }
            txtMsgIdentifier.Text = txt;
            Application.DoEvents();
        }
        #endregion
        #region cmd line args
        private string[] CmdLineArgs = new string[0];
        private long GetLongFromString(string numStr, long defVal)
        {
            try
            {
                if (numStr == null)
                    return defVal;
                return Convert.ToInt64(numStr);
            }
            catch
            {
            }
            return defVal;
        }
        private bool GetBoolFromString(string numStr, bool defVal)
        {
            if (numStr == null)
                return defVal;
            numStr = numStr.Trim();
            if (numStr.Length == 0) return defVal;
            //
            long lln = GetLongFromString(numStr, -1);
            if ((lln != -1) && (lln > 0))
                return true;
            numStr = numStr.Substring(0, 1);
            if ((numStr.CompareTo("t") == 0) ||
                (numStr.CompareTo("T") == 0) ||
                (numStr.CompareTo("1") == 0) ||
                (numStr.CompareTo("y") == 0) ||
                (numStr.CompareTo("Y") == 0))
                return true;
            return false;
        }
        private bool NeedArgAutoStart = false;
        private bool AutoArgInProcess = false;
        private void understandCmdArgs()
        {
            if (CmdLineArgs.Length == 0)
                return;
            foreach (string keyVal in CmdLineArgs)
            {
                string[] kvList = keyVal.Split(new Char[] { '=' });
                if (kvList == null)
                    continue;
                if (kvList.Length != 2)
                    continue;
                understandCmdArg(kvList[0].Trim(), kvList[1].Trim());
            }
            if(NeedArgAutoStart == true)
            {
                AutoArgInProcess = true;
                try
                {
                    ReadFileForData();
                    //
                    //if ((filelines != null) && (filelines.Length > 0))
                    {
                        chkSingleStep.Checked = false;
                        chkRunFast.Checked = false;
                        ChkRunContinuous.Checked = true;
                    }
                    Task.Delay(1000).ContinueWith(t => btnConnA_Click(null, null));
                    //
                    Task.Delay(5000).ContinueWith(t => btnSendDataMsg_Click(null, null));
                }
                catch { }
                finally
                {
                    AutoArgInProcess = false;
                }
            }
        }
        private void understandCmdArg(string key, string val)
        {
            // SRV=False IP=192.168.8.1 PORT=4096 DF=c:\td\test.log AUTO=False
            if (key == "SRV")
            {
                if(GetBoolFromString(val, false))
                {
                    rbIsServer.Checked = true;
                }
                else
                {
                    rbIsClient.Checked = true;
                }
            }
            if (key == "AUTO")
            {
                NeedArgAutoStart = GetBoolFromString(val, false);
            }
            if (key == "DF")
            {
                txtDataFile.Text = val;
            }
            if (key == "IP")
            {
                if(string.IsNullOrEmpty(val) == false)
                    txtIPAddress.Text = val;
            }
            if (key == "PORT")
            {
                long port = GetLongFromString(val, -1);
                if (port > 0)
                    txtPort.Text = port.ToString();
            }
        }


        #endregion
        #region form mgt
        private string formTitleStr = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            CmdLineArgs = System.Environment.GetCommandLineArgs();
            //CommandLine.Parser.Default.ParseArguments(System.Environment.GetCommandLineArgs())
            txtIPAddress.Text = FixtureSimulator.FixtureSimulatorSettings.Default.IPAddress;
            txtPort.Text = FixtureSimulator.FixtureSimulatorSettings.Default.Port;
            txtDataFile.Text = FixtureSimulator.FixtureSimulatorSettings.Default.DataFile;
            txtMsgIdentifier.Text = FixtureSimulator.FixtureSimulatorSettings.Default.MsgIdentifier;
            Text = Text + $" [{Application.ProductVersion}]";
            formTitleStr = Text;
            if (txtMsgIdentifier.Text.Length == 0)
                txtMsgIdentifier.Text = "PortA.[0-9]+::----> : ";
            ConnOptions();
            ProcessRBFStatus();
            understandCmdArgs();

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                try
                {
                    if (scSrv != null)
                        scSrv.StopServer();
                    curClient = null;
                    if (sc != null)
                        sc.CompleteStop();
                }
                finally
                {
                    scSrv = null;
                    sc = null;
                }
                curClient = null;

                log("Form1_FormClosing", "Server Stopped.");
                return;
            }
            catch (Exception ex)
            {
                log("Form1_FormClosing", ex.ToString());
                return;
            }
        }
        #endregion form mgt
        #region current connected client
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
                    return;
                }
            }
            catch (Exception ex)
            {
                log("Server_ClientDisconnected", ex.ToString());
            }
        }
        private void Sc_DataStringReceived(Sapphire.SAFW.Agent.IBaseCom source, string sData)
        {
            string msg = "<--- " + sData;
            logClient(msg, sData);
            Task.Delay(50).ContinueWith(t => ProcessClientCommand(sData.Trim()));
        }
        private void Server_DataReceived(string sRemoteAddress, int nRemotePort, string sData)
        {
            string msg = "<--- " + sData;
            logClient(msg, sData);
            Task.Delay(50).ContinueWith(t => ProcessClientCommand(sData.Trim()));
        }
        #region fixture staus mgt
        private string _lastStatusSent = "IDLE";
        private string lastIOStatusSent = "4c584a644a680,00";
        private object lastStatusSentLock = new object();
        private string lastStatusSent
        {
            get
            {
                lock(lastStatusSentLock)
                {
                    return _lastStatusSent;
                }
            }
            set
            {
                lock (lastStatusSentLock)
                {
                    bool needUIUpdate = (_lastStatusSent != value);
                    if (needUIUpdate == false) return;
                    _lastStatusSent = value;
                    if(needUIUpdate)
                    {
                        SetrbFStatus(_lastStatusSent);
                    }
                }
            }
        }
        private object rbFStatus_CheckedChangedLock = new object();
        private void rbFStatus_CheckedChanged(object sender, EventArgs e)
        {
            ProcessRBFStatus();
        }
        private void ProcessRBFStatus()
        {
            lock (rbFStatus_CheckedChangedLock)
            {
                string newStatus = "IDLE";
                if (rbFSAlarm.Checked == true)
                    newStatus = "ALARM";
                if (rbFSIdle.Checked == true)
                    newStatus = "IDLE";
                if (rbFSAutomation.Checked == true)
                    newStatus = "AUTOMATION";
                if (lastStatusSent == newStatus) return;
                lastStatusSent = newStatus;
            }
        }
        private delegate void SetrbFStatusDelegate(string newStatus);
        private void SetrbFStatus(string newStatus)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new SetrbFStatusDelegate(SetrbFStatus), new Object[] { newStatus });
                return;
            }
            if(newStatus == "IDLE")
            {
                if (rbFSIdle.Checked == true) return;
                rbFSIdle.Checked = true;
                rbFSAutomation.Checked = false;
                rbFSAlarm.Checked = false;
            }
            if (newStatus == "ALARM")
            {
                if (rbFSAlarm.Checked == true) return;
                rbFSAlarm.Checked = true;
                rbFSAutomation.Checked = false;
                rbFSIdle.Checked = false;
            }
            if (newStatus == "AUTOMATION")
            {
                if (rbFSAutomation.Checked == true) return;
                rbFSAutomation.Checked = true;
                rbFSIdle.Checked = false;
                rbFSAlarm.Checked = false;
            }
            Application.DoEvents();
        }
        #endregion

        private void ProcessClientCommand(string cmd)
        {
            //0,REPORT_UNIT_LOCATION,1234567_20,LEFT.ASSEMBLE[0xD]
            string[] lineParts = cmd.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (lineParts.Length > 0)
            {
                int seqNum = Sapphire.SAFW.EquipmentInfo.ParameterValue.GetIntegerFromString(lineParts[0].Trim(), 0);
                if (seqNum == 0)
                    return;
                if(lineParts.Length < 2) return;
                string clientCmd = lineParts[1].Trim();
                string responseToClient = "";
                switch(clientCmd)
                {
                    case "GET_CONFIG":
                        responseToClient = seqNum.ToString("000") + ",GET_CONFIG,FC1,Win7,JH4_Fargo_V1.39,0.1,001-2315,"
                                            + DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "," + DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture); // 02 /27/2017,13:12:23";
                        break;
                    case "GET_STATUS":
                        // 0,UPDATE_STATUS,AUTOMATION,4c584a644a680,00
                        responseToClient = seqNum.ToString("000") + ",UPDATE_STATUS," + lastStatusSent + "," + lastIOStatusSent;
                        break;
                    default:
                        responseToClient = cmd.Trim()+",99,UNknownCmd";
                        break;
                }
                sendCommand(responseToClient, null);
            }
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
            }
            catch (Exception ex)
            {
                log("Server_ClientConnected", ex.ToString());
            }
        }
        #endregion
        #region server-client connections
        private bool ConnOptionsInProcess = false;
        private void rbIsClient_CheckedChanged(object sender, EventArgs e)
        {
            ConnOptions();
        }
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
                    btnConnA.Text = "Start Listening";
                    _IsServer = true;
                }
                else
                {
                    txtIPAddress.Enabled = true;
                    txtPort.Enabled = true;
                    btnConnA.Text = "Connect";
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

        private void btnConnA_Click(object sender, EventArgs e)
        {
            #region clean old objects
            bool isStarted = false;
            if (sc != null)
            {
                isStarted = sc.vars["IsCommunicating"].boolValue();
                try
                {
                    sc.DataStringReceived -= Sc_DataStringReceived;
                    sc.vars[Sapphire.Network.SocketClient20.VarInfo.IsCommunicating].ChangedEvt -=
                                                    new Sapphire.SAFW.EquipmentInfo.ParameterChanged(IsCommunicating_ChangedEvt);
                    sc.CompleteStop();
                    System.Threading.Thread.Sleep(1000);
                    if (isStarted)
                    {
                        btnConnA.Text = "Connect";
                        sc = null;
                        return;
                    }
                }
                catch { }
                sc = null;
            }
            if (scSrv != null)
            {
                isStarted = scSrv.vars["IsCommunicating"].boolValue();
                scSrv.SetConnectionAcceptHandler(null);
                scSrv.SetConnectionCloseHandler(null);
                scSrv.SetInputHandler((Sapphire.Network.StringInputHandlerDelegate)null);
                scSrv.vars["IsCommunicating"].ChangedEvt -= new Sapphire.SAFW.EquipmentInfo.ParameterChanged(ServerIsCommunicating_ChangedEvt);
                scSrv.StopServer();
                System.Threading.Thread.Sleep(1000);
                if (isStarted)
                {
                    btnConnA.Text = "Start Listening";
                    scSrv = null;
                    return;
                }
                scSrv = null;
            }
            #endregion
            if (rbIsClient.Checked == true)
            {
                sc = new Sapphire.Network.SocketClient20("testclient");
                //
                sc.RemoteAddress = txtIPAddress.Text;
                sc.RemotePort = Sapphire.SAFW.EquipmentInfo.ParameterValue.GetIntegerFromString(txtPort.Text, 1000);
                sc.LogData = true;
                sc.AppName = LogAppName;
                sc.LowLevelLog = true;
                sc.TryAutoConnect = false;
                sc.comType = Sapphire.SAFW.Agent.ComType.SocketClientCom;
                sc.DataStringReceived += Sc_DataStringReceived;
                sc.vars[Sapphire.Network.SocketClient20.VarInfo.IsCommunicating].ChangedEvt +=
                                                new Sapphire.SAFW.EquipmentInfo.ParameterChanged(IsCommunicating_ChangedEvt);
                _IsServer = false;
                FixtureSimulator.FixtureSimulatorSettings.Default.IPAddress = txtIPAddress.Text;
                FixtureSimulator.FixtureSimulatorSettings.Default.Port = txtPort.Text;
                FixtureSimulator.FixtureSimulatorSettings.Default.Save();
            }
            else
            {
                scSrv = new Sapphire.Network.SocketServer("testsrv", "localhost",
                Sapphire.SAFW.EquipmentInfo.ParameterValue.GetIntegerFromString(txtPort.Text, 4096));
                scSrv.comType = Sapphire.SAFW.Agent.ComType.SocketServerCom;
                scSrv.SetConnectionAcceptHandler(Server_ClientConnected);
                scSrv.SetConnectionCloseHandler(Server_ClientDisconnected);
                scSrv.SetInputHandler(Server_DataReceived);
                scSrv.LogData = true;
                scSrv.LowLevelLog = true;
                scSrv.AppName = LogAppName;
                scSrv.vars["IsCommunicating"].ChangedEvt += new Sapphire.SAFW.EquipmentInfo.ParameterChanged(ServerIsCommunicating_ChangedEvt);
                if (FixtureSimulator.FixtureSimulatorSettings.Default.Port != txtPort.Text)
                {
                    FixtureSimulator.FixtureSimulatorSettings.Default.Port = txtPort.Text;
                    FixtureSimulator.FixtureSimulatorSettings.Default.Save();
                }
                _IsServer = true;
            }
            log("StartServer", "called..");
            Sapphire.SAFW.Threading.ReadIOService.QueueServiceRequest(new System.Threading.WaitCallback(OpenOnThread), null);
        }

        private void OpenOnThread(object stat)
        {
            try
            {
                if(IsServer)
                {
                    if (scSrv != null)
                    {
                        if (scSrv.StartServer() == true)
                        {
                            log("StartListen", "listening on " + scSrv.ServerPort.ToString());
                            return;
                        }
                        log("StartListen", "Failed, " + scSrv.vars["LastError"].stringValue());
                    }
                }
                else
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
                }
            }
            catch { }
        }

        private object IsStarted_ChangedEvtLock = new object();
        void IsCommunicating_ChangedEvt(Sapphire.SAFW.EquipmentInfo.Parameter source, Sapphire.SAFW.EquipmentInfo.ParameterValue NewVal)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Sapphire.SAFW.EquipmentInfo.ParameterChanged(IsCommunicating_ChangedEvt), new Object[] { source, NewVal });
                return;
            }
            lock (IsStarted_ChangedEvtLock)
            {
                string frmText = formTitleStr + "- " + txtIPAddress.Text; // + ":" + txtPort.Text;
                Text = frmText;
                if (NewVal.boolValue() == true)
                {
                    btnConnA.Text = "Disconnect";
                }
                else
                {
                    btnConnA.Text = "Connect";
                }
                log("IsStarted", "IsCommunicating=" + NewVal.boolValue().ToString());
            }
        }
        void ServerIsCommunicating_ChangedEvt(Sapphire.SAFW.EquipmentInfo.Parameter source, Sapphire.SAFW.EquipmentInfo.ParameterValue NewVal)
        {
            if (InvokeRequired == true)
            {
                BeginInvoke(new Sapphire.SAFW.EquipmentInfo.ParameterChanged(ServerIsCommunicating_ChangedEvt), new Object[] { source, NewVal });
                return;
            }
            lock (IsStarted_ChangedEvtLock)
            {
                if (NewVal.boolValue() == true)
                {
                    btnConnA.Text = "Stop Listening";
                }
                else
                {
                    btnConnA.Text = "Start Listening";
                }
                log("IsStarted", "IsCommunicating=" + NewVal.boolValue().ToString());
            }
        }
        private Sapphire.Network.SocketServer scSrv = null;
        private Sapphire.Network.SocketClient20 sc = null;
        #endregion server-client connections

        #region data file support
        private void btnDataFileSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (txtDataFile.Text.Length != 0)
            {
                string loc = System.IO.Path.GetDirectoryName(txtDataFile.Text);
                if (System.IO.Directory.Exists(loc) == true)
                {
                    openFileDialog1.InitialDirectory = loc;
                }
            }
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            //
            string diagFilter = "All files (*.*)|*.*";
            //
            openFileDialog1.Filter = diagFilter;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Select Data File";
            //
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            txtDataFile.Text = openFileDialog1.FileName;
        }

        private void btnReadDataFile_Click(object sender, EventArgs e)
        {
            ReadFileForData();
        }
        private bool ReadFileForData()
        {
            filelines = new List<string>().ToArray();
            FixtureSimulator.FixtureSimulatorSettings.Default.DataFile = txtDataFileText;
            FixtureSimulator.FixtureSimulatorSettings.Default.Save();
            if (System.IO.File.Exists(txtDataFileText) == false)
            {
                SetLblDataStatusText("File '" + txtDataFileText + "' not found");
                return false;
            }

            if ((txtDataFileText.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase)) ||
                (txtDataFileText.EndsWith(".log", StringComparison.InvariantCultureIgnoreCase)))
            {
                string rootTemp = GetNewTempFolder();
                System.IO.Compression.ZipFile.ExtractToDirectory(txtDataFileText, rootTemp);
                string filePath = Path.Combine(rootTemp, "SAFW", "DataFiles", "FixtureCommunication.sqlite");
                if (System.IO.File.Exists(filePath) == false)
                {
                    //Logs
                    filePath = Path.Combine(rootTemp, "Logs", "SAFW", "DataFiles", "FixtureCommunication.sqlite");
                    if (System.IO.File.Exists(filePath) == false)
                    {
                        SetLblDataStatusText("File '" + filePath + "' not found");
                        return false;
                    }
                }
                filelines = ReadSqlLiteFile(filePath);
                if((filelines != null) && (filelines.Length > 0))
                    SettxtMsgIdentifier("::Rcv : ");
            }
            else
            {
                if (txtDataFileText.EndsWith(".sqlite", StringComparison.InvariantCultureIgnoreCase))
                {
                    filelines = ReadSqlLiteFile(txtDataFileText);
                    if ((filelines != null) && (filelines.Length > 0))
                        SettxtMsgIdentifier("::Rcv : ");
                    // "::Rcv : "
                }
                else
                {
                    System.IO.StreamReader fileSR = System.IO.File.OpenText(txtDataFileText);
                    string file = fileSR.ReadToEnd();
                    fileSR.Close();
                    fileSR = null;
                    //
                    filelines = file.Split('\n');
                    file = "";
                }
            }

            lineIndex = 0;
            SetLblDataStatusText("File read done, ready to send");
            return true;
        }
        #region read fixture data
        public class FixtureCommunication
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public DateTime TimeStamp { get; set; }
            public string Direction { get; set; }
            public string Message { get; set; }

            public FixtureCommunication()
            {

            }
            public FixtureCommunication(string Direction, string Message)
            {
                TimeStamp = Sapphire.Clock.Now;
                this.Direction = Direction;
                this.Message = Message;
            }
        }
        private string[] ReadSqlLiteFile(string DbFileLocation)
        {
            List<string> fileData = new List<string>();
            try
            {
                Sapphire.SAFW.SQLite.SQLiteDBAccess dbAccess = new Sapphire.SAFW.SQLite.SQLiteDBAccess(DbFileLocation);
                dbAccess.CreateTable<FixtureCommunication>();
                var data = dbAccess.db.Table<FixtureCommunication>().OrderBy(f => f.TimeStamp).ToList();
                foreach(FixtureCommunication fc in data)
                {
                    string lstr1 = "<BR>[0-1][" +
                        System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("00000") +
                        "][" + Sapphire.Clock.GetTimeStampString(fc.TimeStamp) + "]::" +
                        fc.Direction + " : " + fc.Message;
                    fileData.Add(lstr1);
                }
            }
            catch (Exception ex)
            {
            }
            return fileData.ToArray();
        }
        private string GetNewTempFolder()
        {
            return Path.Combine(Path.GetTempPath(), "FixtureSimulator", DateTime.Now.Ticks.ToString());
        }
        #endregion
        //
        private bool SendMsgInProcess = false;
        private bool StopSendMsgReq = false;
        private bool chkSingleStepVal = false;
        private bool chkRunFastVal = false;
        private bool chkRunContinuous = false;
        private string txtDataFileText = "";
        // Equipment.Machine1.FurnaceTube1.EquipmentGem.[0-9]+::<---- : 
        // PortB.[0-9]+::<---- : 
        private void btnSendDataMsg_Click(object sender, EventArgs e)
        {
            if (SendMsgInProcess == true)
            {
                StopSendMsgReq = true;
            }
            else
            {
                StopSendMsgReq = false;
                if (FixtureSimulator.FixtureSimulatorSettings.Default.MsgIdentifier != txtMsgIdentifier.Text)
                {
                    FixtureSimulator.FixtureSimulatorSettings.Default.MsgIdentifier = txtMsgIdentifier.Text;
                    FixtureSimulator.FixtureSimulatorSettings.Default.Save();
                }
                chkSingleStepVal = chkSingleStep.Checked;
                chkRunFastVal = chkRunFast.Checked;
                chkRunContinuous = ChkRunContinuous.Checked;
                MsgSendPattern = timeStampPattern + txtMsgIdentifier.Text + @"(?<Msg>[\W | \w | \d]+)";
                Sapphire.SAFW.Threading.ReadIOService.QueueServiceRequest(
                                        new System.Threading.WaitCallback(SendOnAThread), null);
            }
        }
        private void chkRunFast_CheckedChanged(object sender, EventArgs e)
        {
            chkRunFastVal = chkRunFast.Checked;
        }

        private void ChkRunContinuous_CheckedChanged(object sender, EventArgs e)
        {
            chkRunContinuous = ChkRunContinuous.Checked;
        }

        private void chkSingleStep_CheckedChanged(object sender, EventArgs e)
        {
            chkSingleStepVal = chkSingleStep.Checked;
        }
        private void SendOnAThread(object stat)
        {
            try
            {
                DateTime lastDt = DateTime.MinValue;
                SendMsgInProcess = true;
                SendMsgButLable("Stop SendMsg");
                SetSendMessageStatus(false);
                bool msgSentSure = false;
                while (true)
                {
                    LineData ld = GetNextLine();
                    if (ld == null)
                        return;
                    if ((scSrv == null) && (sc == null))
                        return;
                    if ((msgSentSure == true) && (lastDt != DateTime.MinValue))
                    {
                        TimeSpan ts = ld.TimeStamp.Subtract(lastDt);
                        if ((ts == TimeSpan.MinValue) || (ts == TimeSpan.MaxValue) || (ts.TotalMilliseconds < 0))
                        {
                            ts = new TimeSpan(100);
                        }
                        if (chkRunFastVal == true)
                        {
                            if (ts.TotalMilliseconds < 1000)
                            {
                                MonitorSleep(ts);
                            }
                            else
                            {
                                MonitorSleep(1000);
                            }
                        }
                        else
                        {
                            MonitorSleep(ts);
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    string dataToSend = ld.MessageToSend();
                    if(dataToSend.Contains("UPDATE_STATUS,"))
                    {
                        // 0,UPDATE_STATUS,AUTOMATION,4c584a644a680,00
                        int idx = dataToSend.IndexOf("UPDATE_STATUS,");
                        if(idx > 0)
                        {
                            string part = dataToSend.Substring(idx + "UPDATE_STATUS,".Length);
                            idx = part.IndexOf(",");
                            // AUTOMATION,4c584a644a680,00
                            if (idx > 0)
                            {
                                lastStatusSent = part.Substring(0, idx);
                                lastIOStatusSent = part.Substring(idx + 1);
                            }
                        }
                    }
                    if (sendCommand(dataToSend, ld) == true)
                    {
                        msgSentSure = true;
                        lastDt = ld.TimeStamp;
                    }
                    else
                    {
                        msgSentSure = false;
                    }
                    if (StopSendMsgReq == true)
                        break;
                    if (chkSingleStepVal == true)
                        break;
                }
            }
            catch { }
            finally
            {
                SendMsgButLable("SendMsg");
                SendMsgInProcess = false;
                StopSendMsgReq = false;
                SetSendMessageStatus(true);
            }
        }
        private void MonitorSleep(int numSecs)
        {
            TimeSpan sleepSpan = new TimeSpan(0, 0, numSecs);
            MonitorSleep(sleepSpan);
        }
        private void MonitorSleep(TimeSpan sleepSpan)
        {
            DateTime sleepTill = DateTime.Now.Add(sleepSpan);
            try
            {
                if (sleepSpan.TotalSeconds > 60 * 15)
                {
                    SetLblSleepStatusStatusText(sleepTill.Subtract(DateTime.Now).ToString("h'h 'm'm 's's'"));
                    System.Threading.Thread.Sleep(1000);
                    return;
                }
                while (DateTime.Now < sleepTill)
                {
                    SetLblSleepStatusStatusText(sleepTill.Subtract(DateTime.Now).ToString("h'h 'm'm 's's'"));
                    System.Threading.Thread.Sleep(200);
                    if (StopSendMsgReq == true)
                        return;
                }
            }
            finally
            {
                SetLblSleepStatusStatusText("");
            }
        }
        private bool sendCommand(string cmd, LineData ld)
        {
            string purpose = "--->";
            if(ld != null)
            {
                purpose = ld.TimeStamp.ToString("MMMddyy:HH::mm:ss.fffffff");
            }
            if(IsServer)
            {
                if (curClient == null)
                {
                    return true;
                }
                if (scSrv.SendData(curClient.RemoteAddress, curClient.RemotePort, cmd + "\r") == true)
                {
                    logClient(purpose + "  " + Sapphire.Network.SocketClient20.makePrintable(cmd), ld);
                    return true;
                }
                logClient(purpose + "  " + scSrv.vars["LastError"].stringValue(), null);
            }
            else
            {
                if (sc == null)
                {
                    return true;
                }
                if (sc.Write(cmd + "\r") == true)
                {
                    logClient(purpose + "  " + Sapphire.Network.SocketClient20.makePrintable(cmd), ld);
                    return true;
                }
                logClient(purpose + "  " + sc.vars["LastError"].stringValue(), null);
            }
            return false;
        }

        public class LineData
        {
            public readonly int LineNumber;
            public readonly DateTime TimeStamp;
            public string Message;
            public LineData(int oLineNumber, DateTime oTimeStamp, string oMessage)
            {
                LineNumber = oLineNumber;
                TimeStamp = oTimeStamp;
                Message = oMessage;
            }

            public string MessageToSend()
            {
                StringBuilder sb = new StringBuilder();
                string[] lineParts = this.Message.Split(new string[] { "[0x", "]" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string prt in lineParts)
                {
                    if (prt == null)
                        continue;
                    if (prt.Length == 0)
                        continue;
                    byte bt = 0;
                    if (byte.TryParse(prt, System.Globalization.NumberStyles.AllowHexSpecifier, null, out bt) == false)
                    {
                        sb.Append(prt);
                        continue;
                    }
                    if(bt == 0x20)
                        sb.Append(" ");
                    else if (bt == 0xd)
                        break;
                }
                string dataStr = sb.ToString();
                return dataStr;
            }
        }
        //private string pattern =
        //    @"(?<thId>[\d]+)\]\[(?<Month>[a-zA-Z]+)(?<DtYY>[0-9]+):(?<HH>[0-9]+)::(?<MM>[0-9]+):(?<SS>[0-9]+).(?<mSec>[0-9]+)\](?<flId>[\.\w\d]+)::(?<type>(RunConditionTask)|(RunTask)) : ((-------------------)|(--------XXXX------))(?<state>\w+), Run\((?<func>[\w]+)";
        private string MsgSendPattern = "";
        private string nextLineOnlyPattern = @"<BR>(?<Msg>[\W | \w | \d]+)";
        private string timeStampPattern = @"(?<cpuID1>[0-9]+)-(?<cpuID2>[0-9]+)\]\[(?<thId>[\d]+)\]" +
                                             @"\[(?<Month>[a-zA-Z\?]+)(?<DtYY>[0-9]+):(?<HH>[0-9]+)::(?<MM>[0-9]+):(?<SS>[0-9]+).(?<mSec>[0-9]+)\]";
        private LineData GetNextLine()
        {
            LineData ret = null;
            int sleepCounter = 0;
            while (true)
            {
                sleepCounter++;
                if (sleepCounter > 200)
                {
                    sleepCounter = 0;
                    System.Threading.Thread.Sleep(50);
                }
                if (StopSendMsgReq == true)
                    break;
                string nxtLine = GetNextFileLine();
                if (nxtLine.Length == 0)
                    return ret;
                System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(timeStampPattern);
                System.Text.RegularExpressions.Match m = rx.Match(nxtLine);
                if (m.Success == false)
                    continue;
                // now we can get started.
                rx = new System.Text.RegularExpressions.Regex(MsgSendPattern);
                m = rx.Match(nxtLine);
                if (m.Success == false)
                    continue;
                string month = m.Groups["Month"].Value;
                string dtYY = m.Groups["DtYY"].Value;
                string hour = m.Groups["HH"].Value;
                string min = m.Groups["MM"].Value;
                string sec = m.Groups["SS"].Value;
                string mSec = m.Groups["mSec"].Value;
                string msg = m.Groups["Msg"].Value;
                if (month == "??")
                    month = DateTime.Today.ToString("MMM"); // "Feb";
                DateTime timeStamp = DateTime.ParseExact(month + dtYY + hour + min + sec + mSec, "MMMddyyHHmmssfffffff", null);
                ret = new LineData(lineIndex, timeStamp,  ConvertByteToString(msg));
                while (true)
                {
                    sleepCounter++;
                    if (sleepCounter > 200)
                    {
                        sleepCounter = 0;
                        System.Threading.Thread.Sleep(50);
                    }
                    if (StopSendMsgReq == true)
                        break;
                    string pNxtLine = PeekNextFileLine();
                    if (pNxtLine.Length == 0)
                    {
                        break;
                    }
                    // chk if this has date pattern at start?
                    rx = new System.Text.RegularExpressions.Regex(timeStampPattern);
                    m = rx.Match(pNxtLine);
                    if (m.Success == true)
                    {
                        break;
                    }
                    // looks like this might be use full for extension of message
                    nxtLine = GetNextFileLine();
                    if (nxtLine.Length == 0)
                    {
                        break;
                    }
                    rx = new System.Text.RegularExpressions.Regex(timeStampPattern);
                    m = rx.Match(nxtLine);
                    if (m.Success == true)
                    {
                        break;
                    }
                    // last check for nextLineOnlyPattern
                    rx = new System.Text.RegularExpressions.Regex(nextLineOnlyPattern);
                    m = rx.Match(nxtLine);
                    if (m.Success == false)
                    {
                        break;
                    }
                    // extract extension and append to message for previous find
                    msg = m.Groups["Msg"].Value;
                    ret.Message = ret.Message + ConvertByteToString(msg);
                }
                //Failed to send! msg=GET_STATUS
                if(string.IsNullOrEmpty(ret.Message) == false)
                {
                    // we have data,
                    // check, if we have error message?
                    if (ret.Message.Contains(",") == false)
                        continue;
                }
                break;
            }
            return ret;
        }
        private string[] filelines = null;
        private int lineIndex = 0;
        private string GetNextFileLine()
        {
            string ret = "";
            if (filelines == null)
            {
                SetLblDataStatusText("Please read file first!!");
                return ret;
            }
            if (lineIndex >= filelines.Length)
            {
                SetLblDataStatusText("100%, " + lineIndex.ToString() + "/" + filelines.Length.ToString());
                if (chkRunContinuous == false) return ret;
                if (ReadFileForData() == false) return ret;
                if (lineIndex >= filelines.Length) return ret;
            }
            while (true)
            {
                try
                {
                    if (StopSendMsgReq == true)
                        break;
                    if (lineIndex >= filelines.Length)
                    {
                        SetLblDataStatusText("100%, " + lineIndex.ToString() + "/" + filelines.Length.ToString());
                        if (chkRunContinuous == false) return ret;
                        if (ReadFileForData() == false) return ret;
                        if (lineIndex >= filelines.Length) return ret;
                    }
                    ret = filelines[lineIndex].Trim();
                    lineIndex++;
                    if (string.IsNullOrEmpty(ret))
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    break;
                }
                catch { }
                finally
                {
                    float percentComplete = (float)lineIndex / filelines.Length * 100.0F;
                    SetLblDataStatusText(percentComplete.ToString("f0") + "%, " + lineIndex.ToString() + "/" + filelines.Length.ToString());
                }
            }
            return ret;
        }
        private string PeekNextFileLine()
        {
            string ret = "";
            if (filelines == null)
                return ret;
            if (lineIndex >= filelines.Length)
                return ret;
            try
            {
                ret = filelines[lineIndex].Trim();
            }
            catch { }
            if (string.IsNullOrEmpty(ret) == true)
                return "";
            return ret;
        }
        public string ConvertByteToString(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return "";
            if (!msg.StartsWith("[")) return msg;
            //
            System.Collections.Generic.List<byte> dataBytes = new List<byte>();
            string[] lineParts = msg.Split(new string[] { "[0x", "]" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string prt in lineParts)
            {
                if (prt == null)
                    continue;
                if (prt.Length == 0)
                    continue;
                byte bt = 0;
                if (byte.TryParse(prt, System.Globalization.NumberStyles.AllowHexSpecifier, null, out bt) == false)
                    continue;
                dataBytes.Add(bt);
            }
            System.Text.Encoding encode = System.Text.Encoding.GetEncoding("GB18030");
            string cnvStr = encode.GetString(dataBytes.ToArray(), 0, dataBytes.Count);
            cnvStr = cnvStr.Replace("\r", "[0xD]");
            return cnvStr;
        }

        #endregion

        private void sendSelectedMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.Items == null)
            {
                return;
            }
            if (listView1.Items.Count == 0)
            {
                return;
            }
            if (listView1.SelectedItems == null)
            {
                return;
            }
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            if (listView1.SelectedItems[0] == null)
            {
                return;
            }
            if (listView1.SelectedItems[0].Tag == null)
            {
                return;
            }
            LineData ld = listView1.SelectedItems[0].Tag as LineData;
            if (ld == null)
                return;
            if ((scSrv == null) && (sc == null))
                return;
            string dataToSend = ld.MessageToSend();
            sendCommand(dataToSend, ld);
        }

        private void clearMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            sendSelectedMessageToolStripMenuItem.Visible = false;
            if (listView1.Items == null)
            {
                return;
            }
            if (listView1.Items.Count == 0)
            {
                return;
            }
            if (listView1.SelectedItems == null)
            {
                return;
            }
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            if (listView1.SelectedItems[0] == null)
            {
                return;
            }
            if (listView1.SelectedItems[0].Tag == null)
            {
                return;
            }
            LineData ld = listView1.SelectedItems[0].Tag as LineData;
            if (ld == null)
                return;
            sendSelectedMessageToolStripMenuItem.Visible = true;
        }

        private void txtDataFile_TextChanged(object sender, EventArgs e)
        {
            txtDataFileText = txtDataFile.Text;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.Items == null)
            {
                return;
            }
            if (listView1.Items.Count == 0)
            {
                return;
            }
            if (listView1.SelectedItems == null)
            {
                return;
            }
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            if (listView1.SelectedItems[0] == null)
            {
                return;
            }
            if (listView1.SelectedItems[0].Tag == null)
            {
                return;
            }
            //Sapphire.SAFW.SecsGem.SecsIIMessage msg = listView1.SelectedItems[0].Tag as Sapphire.SAFW.SecsGem.SecsIIMessage;
            //if (msg == null)
            //{
            //    return;
            //}
            //if (msg.IsValid == false)
            //    return;
            //Sapphire.SAFW.Client.GuiHelper.XMLEditDialog xed = new Sapphire.SAFW.Client.GuiHelper.XMLEditDialog();
            ////xed.ShowExpressInsOnToolBar = true;
            ////xed.GetUserInputForExpressionInsert = new Client.GuiHelper.GetUserInputDelegate(EditExpression);
            //xed.StartPosition = FormStartPosition.CenterScreen;
            ////xed.Location = new Point(this.Left + ((this.Right - this.Left) / 2), this.Top);
            //xed.ShowInTaskbar = false;
            //xed.ModifyRootNodeOnOpen = false;
            //xed.ReadOnly = true;
            //xed.CustomEditToolTip = "Hex Editor";
            //xed.ShowCustomEditOnToolBar = true;
            //xed.GetUserInputForCustomEdit = new Sapphire.SAFW.Client.GuiHelper.GetUserInputDelegate(EditHex);
            //xed.EditXML(this, " SECS II Message", msg.XMLMessage.ToString(), null);
        }
        private Byte[] parseForBytes(string msg)
        {
            System.Collections.Generic.List<byte> dataBytes = new List<byte>();
            string[] lineParts = msg.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string prt in lineParts)
            {
                if (prt == null)
                    continue;
                if (prt.Length == 0)
                    continue;
                byte bt = 0;
                if (byte.TryParse(prt, System.Globalization.NumberStyles.AllowHexSpecifier, null, out bt) == false)
                {
                    if (prt.StartsWith("0x") == false)
                        continue;
                    if (byte.TryParse(prt.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier, null, out bt) == false)
                        continue;
                }
                dataBytes.Add(bt);
            }
            return dataBytes.ToArray();
        }

        public Byte[] GetBytes(string dataHex)
        {
            System.Collections.Generic.List<byte> dataBytes = new List<byte>();
            string[] lineParts = dataHex.Split(new string[] { "[0x", "]" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string prt in lineParts)
            {
                if (prt == null)
                    continue;
                if (prt.Length == 0)
                    continue;
                byte bt = 0;
                if (byte.TryParse(prt, System.Globalization.NumberStyles.AllowHexSpecifier, null, out bt) == false)
                    continue;
                dataBytes.Add(bt);
            }
            return dataBytes.ToArray();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
        }

    }
}
