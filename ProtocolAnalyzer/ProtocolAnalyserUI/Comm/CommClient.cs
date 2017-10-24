using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace ProtocolAnalyzerUI.Comm
{
    class CommClient : CommBase
    {
        #region [ Data ]
        private TcpClient _Client;
        private ManualResetEvent _ConnectEvent;
        #endregion

        #region [ Properties ]
        override public bool IsConnected { get { return _Client.Connected; } }
        #endregion

        #region [ Life Circle ]
        public CommClient(CommConfig aConfig) : base(aConfig)
        {
            _Client = new TcpClient();
            _Client.SendTimeout = 1000;
            _Client.SendBufferSize = 2048;
            _Client.ReceiveTimeout = 100;
            _Client.ReceiveBufferSize = 2048;
            _ConnectEvent = new ManualResetEvent(false);
        }
        #endregion

        #region [ Connection ]
        override public bool Connect(int nMilliSecondsTimeout = 1000)
        {
            _ConnectEvent.Reset();

            System.Net.IPAddress address = System.Net.IPAddress.Parse(this.Config.IPAddress);
            IAsyncResult result = _Client.BeginConnect(
                address,
                this.Config.Port,
                new AsyncCallback(this.ConnectedAsyncCallback),
                this);

            this._ConnectEvent.WaitOne(nMilliSecondsTimeout);
            if (this.IsConnected == false)
            {
                // Timeout
                _Client.EndConnect(result);
                return false;
            }

            return true;
        }
        private void ConnectedAsyncCallback(IAsyncResult ar)
        {
            CommClient client = ar.AsyncState as CommClient;
            if (client != null && ar.IsCompleted)
            {
                client._ConnectEvent.Set();
            }
        }
        override public void Close()
        {
            _Client.Close();
        }
        override public NetworkStream Stream()
        {
            return _Client.GetStream();
        }
        public override bool SendMessage(string strMessage)
        {
            if (this.IsConnected == false)
                return false;
            
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(strMessage);

            NetworkStream stream = _Client.GetStream();
            if (stream != null)
            {
                stream.Write(bytes, 0, bytes.Length);
                return true;
            }
            return false;
        }
        override public string ReceiveMessage()
        {
            if (this.IsConnected == false)
                return string.Empty;

            NetworkStream stream = _Client.GetStream();
            if (stream != null && stream.DataAvailable)
            {
                int read = stream.Read(this.ReceiveBuffer, 0, 512);
                if (read > 0)
                {
                    return System.Text.Encoding.ASCII.GetString(this.ReceiveBuffer, 0, read);
                }
            }
            return string.Empty;

        }
        #endregion
    }
}
