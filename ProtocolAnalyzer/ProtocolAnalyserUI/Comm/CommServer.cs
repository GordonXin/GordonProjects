using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ProtocolAnalyzerUI.Comm
{
    class CommServer : CommBase
    {
        #region [ Data ]
        private TcpListener _Listener;
        private TcpClient _RemoteClient;
        private ManualResetEvent _ConnectEvent;

        public override bool IsConnected { get { return _RemoteClient != null && _RemoteClient.Connected; } }
        #endregion

        #region [ Life Circle ]
        public CommServer(CommConfig aConfig) : base(aConfig)
        {
            System.Net.IPAddress address = System.Net.IPAddress.Parse(this.Config.IPAddress);
            _Listener = new TcpListener(address, this.Config.Port);
            _ConnectEvent = new ManualResetEvent(false);
            _RemoteClient = null;
        }
        #endregion

        #region [ Connection ]
        override public bool Connect(int nMilliSecondsTimeout = 1000)
        {
            _ConnectEvent.Reset();
            if (_RemoteClient != null)
            {
                _RemoteClient.Close();
                _RemoteClient = null;
            }
            _Listener.Start();
            IAsyncResult result = _Listener.BeginAcceptTcpClient(new AsyncCallback(this.ConnectedAsyncCallback), this);
            _ConnectEvent.WaitOne(nMilliSecondsTimeout);
            _RemoteClient = _Listener.EndAcceptTcpClient(result);
            if (_RemoteClient != null)
            {
                _RemoteClient.SendTimeout = 1000;
                _RemoteClient.SendBufferSize = 2048;

                _RemoteClient.ReceiveTimeout = 100;
                _RemoteClient.ReceiveBufferSize = 2048;             
            }

            return (_RemoteClient != null);
        }
        private void ConnectedAsyncCallback(IAsyncResult ar)
        {
            CommServer server = ar.AsyncState as CommServer;
            if (server != null && ar.IsCompleted)
            {
                server._ConnectEvent.Set();
            }
        }
        override public void Close()
        {
            _Listener.Stop();
        }
        override public NetworkStream Stream()
        {
            return _RemoteClient.GetStream();
        }
        public override bool SendMessage(string strMessage)
        {
            if (this.IsConnected == false)
                return false;
            
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(strMessage);

            NetworkStream stream = _RemoteClient.GetStream();
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

            NetworkStream stream = _RemoteClient.GetStream();
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
