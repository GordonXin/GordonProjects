using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib
{
    class MessageEvent : IDisposable
    {
        #region [ Enum Definitions ]
        public class EventType
        {
            public const string Automation = "AUTOMATION";
            public const string Idle = "IDLE";
            public const string Manual = "MANUAL";
            public const string Alarm = "ALARM";

            public static bool isTypeSupported(string strType)
            {
                if (strType.Equals(Automation)
                    || strType.Equals(Idle)
                    || strType.Equals(Manual)
                    || strType.Equals(Alarm))
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region [ Data ]
        protected int _ID;
        protected string _Type;
        protected string _Message;

        public int ID { get { return _ID; } protected set { _ID = value; } }
        public string Type { get { return _Type; } protected set { _Type = value; } }
        public string Message { get { return _Message; } protected set { _Message = value != null ? value : string.Empty; } }
        #endregion

        #region [ Life Cycle ]
        public MessageEvent(int nID, string strType, string strMessage)
        {
            this.ID = nID;
            this.Type = string.Copy(strType);
            this.Message = string.Copy(strMessage);
        }

        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
