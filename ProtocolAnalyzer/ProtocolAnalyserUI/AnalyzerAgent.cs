using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ProtocolAnalyzerLib;

namespace ProtocolAnalyzerUI
{
    public class AgentEventArgs
    {
        public string State { get; set; }
        public string Message { get; set; }
    }
    public delegate void AgentEventHandler(object sender, AgentEventArgs args);
   
    class AnalyzerAgent
    {
        #region [ Life Circle ]
        public AnalyzerAgent()
        {

        }
        #endregion

        #region [ Data ]
        public Comm.CommConfig CommunicationConfig { get; set; }
        public string ConfigFilePath { get; set; }
        #endregion

        #region [ Methods ]
        public void Start()
        {
            StartTest();
            return;

            //Stop();

            //Comm.CommManager mgr = Comm.CommManager.Instance();
            //mgr.CommStateChangedEvent += new Comm.CommStateChangedHandler(this.CommStateChangedHandler);
            //mgr.StartCommWithConfig(this.CommunicationConfig);

            //ProtocolAnalyzer.Instance().InitWithConfigFilePath(this.ConfigFilePath);
        }
        public void Stop()
        {
            StopTest();
            return;

            //Comm.CommManager mgr = Comm.CommManager.Instance();
            //mgr.StopComm();
            //Thread.Sleep(500);
            //mgr.CommStateChangedEvent -= new Comm.CommStateChangedHandler(this.CommStateChangedHandler);
        }
        #endregion

        #region [ Event ]
        public event AgentEventHandler AgentEvent;
        private void CommStateChangedHandler(object sender, Comm.CommStateEventArgs args)
        {
            if (args.State.Equals("ThreadStart"))
            {
                FireEvent("Start", "");
            }
            else if (args.State.Equals("ThreadEnd"))
            {
                FireEvent("End", "");
            }
            else if (args.State.Equals("ThreadException"))
            {
                FireEvent("Exception", args.Message);
            }
            else if (args.State.Equals("ConnectionStart"))
            {
                FireEvent("Info", args.Message);
            }
            else if (args.State.Equals("ConnectionStop"))
            {
                FireEvent("Info", args.Message);
            }
            else if (args.State.Equals("ConnectionException"))
            {
                FireEvent("Alarm", args.Message);
            }
            else if (args.State.Equals("MessageSent"))
            {
                Message aMessage = ProtocolAnalyzer.Instance().HandleMessage(args.Message, DateTime.Now, Message.CommMode.Send);
                FireEvent("Info", aMessage.MessageString);
                foreach (MessageInfo anInfo in aMessage.AllInfo)
                {
                    switch(anInfo.Type)
                    {
                        case MessageInfo.InfoType.Alarm:
                        case MessageInfo.InfoType.Format:
                            FireEvent("Alarm", anInfo.Message);
                            break;
                        case MessageInfo.InfoType.Warn:
                            FireEvent("Warning", anInfo.Message);
                            break;
                        case MessageInfo.InfoType.Info:
                            FireEvent("Info", anInfo.Message);
                            break;
                    }
                }
            }
            else if (args.State.Equals("MessageReceived"))
            {
                Message aMessage = ProtocolAnalyzer.Instance().HandleMessage(args.Message, DateTime.Now, Message.CommMode.Recv);
                FireEvent("Info", aMessage.MessageString);
                foreach (MessageInfo anInfo in aMessage.AllInfo)
                {
                    switch (anInfo.Type)
                    {
                        case MessageInfo.InfoType.Alarm:
                        case MessageInfo.InfoType.Format:
                            FireEvent("Alarm", anInfo.Message);
                            break;
                        case MessageInfo.InfoType.Warn:
                            FireEvent("Warning", anInfo.Message);
                            break;
                        case MessageInfo.InfoType.Info:
                            FireEvent("Info", anInfo.Message);
                            break;
                    }
                }
            }
        }
        private void FireEvent(string state, string message)
        {
            AgentEventArgs arg = new AgentEventArgs();
            arg.State = state;
            arg.Message = message;
            if (this.AgentEvent != null)
            {
                this.AgentEvent(this, arg);
            }
        }
        #endregion

        #region [ Test ]
        private Thread _TestThread = null;
        private void StartTest()
        {
            _TestThread = new Thread(new ThreadStart(this.TestThreadStart));
            _TestThread.Start();
        }
        private void StopTest()
        {
            if (_TestThread != null && _TestThread.IsAlive)
            {
                _TestThread.Abort();
            }
            _TestThread = null;
        }
        private void TestThreadStart()
        {
            try
            {
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("ThreadStart", ""));
                ProtocolAnalyzer.Instance().InitWithConfigFilePath(this.ConfigFilePath);
                // Test Empty messages
                //CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageSent", ""));
                //Thread.Sleep(200);
                //CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", ""));
                //Thread.Sleep(200);
                //CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0"));
                //Thread.Sleep(200);
                //CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageSent", "0"));
                //Thread.Sleep(200);
                //CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,"));
                //Thread.Sleep(200);
                //CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageSent", "0,"));
                //Thread.Sleep(200);

                // Test Get_Config
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageSent", "0,GET_CONFIG"));
                Thread.Sleep(200);
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,GET_CONFIG"));
                Thread.Sleep(200);
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,GET_CONFIG,FC1"));
                Thread.Sleep(200);
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,GET_CONFIG,FC1,Win7"));
                Thread.Sleep(200);
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,GET_CONFIG,FC1,Win7,1.0"));
                Thread.Sleep(200);
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,GET_CONFIG,FC1,Win7,1.0,0.1"));
                Thread.Sleep(200);
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,GET_CONFIG,FC1,Win7,1.0,0.1,001"));
                Thread.Sleep(200);
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,GET_CONFIG,FC1,Win7,1.0,0.1,001,02/05/2017"));
                Thread.Sleep(200);
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,GET_CONFIG,FC1,Win7,1.0,0.1,001,02/05/2017,13:00:00"));
                Thread.Sleep(200);
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("MessageReceived", "0,GET_CONFIG,FC1,Win7,1.0,0.1,001,2017/05/2017,13:00:00"));
                Thread.Sleep(200);

                //PrintDebug(HandleMessage("0,REPORT_UNIT_LOCATION", DateTime.Now, Message.CommMode.Send));
                //PrintDebug(HandleMessage("0,REPORT_UNIT_LOCATION,ABC", DateTime.Now, Message.CommMode.Send));
                //PrintDebug(HandleMessage("0,REPORT_UNIT_LOCATION,ABC,RIGHT", DateTime.Now, Message.CommMode.Send));
                //PrintDebug(HandleMessage("0,REPORT_UNIT_LOCATION,,RIGHT", DateTime.Now, Message.CommMode.Send));

                //PrintDebug(HandleMessage("0,REPORT_RESULT", DateTime.Now, Message.CommMode.Send));
                //PrintDebug(HandleMessage("0,REPORT_RESULT,abc", DateTime.Now, Message.CommMode.Send));
                //PrintDebug(HandleMessage("0,REPORT_RESULT,abc,pass", DateTime.Now, Message.CommMode.Send));
            }
            catch
            {

            }
            finally
            {
                CommStateChangedHandler(this, new Comm.CommStateEventArgs("ThreadEnd", ""));
            }
        }
        #endregion
    }
}
