using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace ProtocolAnalyzerUI.Comm
{
    class CommManager
    {
        #region [ Singletion ]
        private static CommManager _Instance = null;
        public static CommManager Instance()
        {
            if (_Instance == null)
            {
                _Instance = new CommManager();
                _Instance.Init();
            }
            return _Instance;
        }
        #endregion

        #region [ Life Circle ]
        private CommManager()
        {
        }
        private void Init()
        {
        }
        #endregion

        #region [ Data ]
        private Thread _CommThread = null;
        private Queue<string> _SendQueue = new Queue<string>();
        private StringBuilder _sb = new StringBuilder();
        #endregion

        #region [ Comm Methods ]
        public bool StartCommWithConfig(CommConfig aConfig)
        {
            this.StopComm();

            string error = "";
            if (aConfig.IsGoodConfig(ref error) == false)
                return false;

            _SendQueue.Clear();
            
            _CommThread = new Thread(new ParameterizedThreadStart(ThreadStart));
            _CommThread.Name = CommBase.CommThreadName(aConfig);
            _CommThread.Start(aConfig);

            return true;
        }
        public void StopComm()
        {
            if (_CommThread != null)
            {
                _CommThread.Abort();
                Thread.Sleep(500);
                _CommThread = null;
            }
        }
        public void SendMessage(string strMessage)
        {
            if (string.IsNullOrWhiteSpace(strMessage))
            {
                return;
            }
            string strSend = strMessage.Trim() + "\r";
            lock (_SendQueue)
            {
                _SendQueue.Enqueue(strSend);
            }
        }
        #endregion

        #region [ Thread ]
        private void ThreadStart(object obj)
        {
            CommConfig aConfig = obj as CommConfig;
            CommBase aComm = null;
            if (aConfig == null)
            {
                return;
            }

            fireStateChangedEvent("ThreadStart", string.Format("{0} is started", _CommThread.Name));

            try
            {
                MainLoop(aConfig, ref aComm);
            }
            catch (System.Threading.ThreadAbortException) 
            {
                // Thread is aborted from end user
                CloseConnection(aComm);
            }
            catch (System.Threading.ThreadInterruptedException)
            {
                // Thread is Interruppted from end user
                CloseConnection(aComm);
            }
            catch(System.Exception ex)
            {
                fireStateChangedEvent("ThreadException", string.Format("{0} met excpetion: {1}", _CommThread.Name, ex.Message));
            }

            fireStateChangedEvent("ThreadEnd", string.Format("{0} is ended", _CommThread.Name));
        }
        private void MainLoop(CommConfig aConfig, ref CommBase aComm)
        {
            while (true)
            {
                try
                {
                    aComm = CommBase.CreateComm(aConfig);
                    // loops for setting up TCP connection
                    if (ConnectLoop(aComm))
                    {
                        fireStateChangedEvent("ConnectionStart", string.Format("Connected to {0}:{1}", aConfig.IPAddress, aConfig.Port));
                        HandleDataLoop(aComm);
                    }
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    fireStateChangedEvent("ConnectionException", string.Format("Connection to {0}:{1} met exception", aConfig.IPAddress, aConfig.Port, ex.Message));
                }
                finally
                {
                    CloseConnection(aComm);
                    fireStateChangedEvent("ConnectionStop", string.Format("Disonnected to {0}:{1}", aConfig.IPAddress, aConfig.Port));
                    Thread.Sleep(200);
                }
            }
        }
        private bool ConnectLoop(CommBase aComm)
        {
            while (!aComm.IsConnected)
            {
                aComm.Connect(1000);
            }
            return aComm.IsConnected;
        }
        private void HandleDataLoop(CommBase aComm)
        {
            while (true)
            {
                // send
                lock (_SendQueue)
                {
                    while (_SendQueue.Count > 0)
                    {
                        string strSend = _SendQueue.Dequeue();
                        aComm.SendMessage(strSend);
                        fireStateChangedEvent("MessageSent", strSend);
                    }
                }

                // receive
                string strReceive = aComm.ReceiveMessage();
                if (strReceive.Length > 0)
                {
                    _sb.Append(strReceive);
                }
                string wholeString = _sb.ToString();
                int index = wholeString.IndexOf('\r');
                while (index >= 0)
                {
                    string strMessage = wholeString.Substring(0, index);
                    fireStateChangedEvent("MessageReceived", strMessage);
                    wholeString = (index + 1 >= wholeString.Length) ? string.Empty : wholeString.Substring(index + 1);
                    index = wholeString.IndexOf('\r');
                }
                _sb = new StringBuilder(wholeString);

                Thread.Sleep(100);
            }
        }
        private CommBase CloseConnection(CommBase aComm)
        {
            try
            {
                if (aComm != null)
                {
                    aComm.Close();
                }
            }
            catch
            {
            }
            return null;
        }
        #endregion

        #region [ Event ]
        public event CommStateChangedHandler CommStateChangedEvent;
        private void fireStateChangedEvent(string state, string message)
        {
            CommStateEventArgs arg = new CommStateEventArgs();
            arg.State = state;
            arg.Message = message;

            if (this.CommStateChangedEvent != null)
            {
                this.CommStateChangedEvent(this, arg);
            }
        }
        #endregion
    }
}