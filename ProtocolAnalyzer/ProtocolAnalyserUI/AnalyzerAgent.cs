using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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

        #region [ Methods ]
        public void StartWithConfig(Comm.CommConfig aConfig)
        {
            Stop();

            Comm.CommManager mgr = Comm.CommManager.Instance();
            mgr.CommStateChangedEvent += new Comm.CommStateChangedHandler(this.CommStateChangedHandler);
            mgr.StartCommWithConfig(aConfig);
        }
        public void Stop()
        {
            Comm.CommManager mgr = Comm.CommManager.Instance();
            mgr.StopComm();
            Thread.Sleep(500);
            mgr.CommStateChangedEvent -= new Comm.CommStateChangedHandler(this.CommStateChangedHandler);
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
            }
            else if (args.State.Equals("MessageReceived"))
            {
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
    }
}
