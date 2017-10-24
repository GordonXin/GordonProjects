using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ProtocolAnalyzerUI.Comm
{
    class CommBase
    {
        #region [ Data ]
        public CommConfig Config { get; set; }
        virtual public bool IsConnected { get; }
        protected byte[] ReceiveBuffer { get; set; }
        #endregion

        #region [ Life Circiel ]
        public CommBase(CommConfig aConfig)
        {
            this.Config = aConfig;
            this.ReceiveBuffer = new byte[2048];
        }
        #endregion

        #region [ Thread ]
        public static string CommThreadName(CommConfig aConfig)
        {
            if (aConfig.Mode == CommConfig.CommMode.Client)
                return "TCPClientThread";
            else if (aConfig.Mode == CommConfig.CommMode.Sever)
                return "TCPServerThread";

            return "UnknownTCPModeThread";
        }
        #endregion

        #region [ Connection ]
        virtual public bool Connect(int nMilliSecondsTimeout = 1000) { return false; }
        virtual public void Close() { }
        virtual public NetworkStream Stream() { return null; }
        virtual public bool SendMessage(string strMessage) { return false; }
        virtual public string ReceiveMessage() { return string.Empty; }
        #endregion

        #region [ Factory ]
        public static CommBase CreateComm(CommConfig aConfig)
        {
            if (aConfig.Mode == CommConfig.CommMode.Sever)
            {
                return new CommServer(aConfig);
            }
            else if (aConfig.Mode == CommConfig.CommMode.Client)
            {
                return new CommClient(aConfig);
            }
            return new CommBase(aConfig);
        }
        #endregion
    }
}
