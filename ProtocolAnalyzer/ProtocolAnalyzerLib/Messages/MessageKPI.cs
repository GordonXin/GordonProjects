using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib
{
    class MessageKPI : IDisposable
    {
        #region [ Life Cycle ]
        public MessageKPI(string strName, string strUnit, string strMin, string strMax, string strValue)
        {
            this.Name = strName;
            this.Unit = strUnit;
            this.Min = DoubleValueFromString(strMin);
            this.Max = DoubleValueFromString(strMax);
            this.Value = DoubleValueFromString(strValue);
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

        #region [ Data ]
        protected string _Name;
        protected string _Unit;
        protected double _Min;
        protected double _Max;
        protected double _Value;
        public const int FieldCount = 5;

        public string Name { get { return _Name; } protected set { _Name = value != null ? value : string.Empty; } }
        public string Unit { get { return _Unit; } protected set { _Unit = value != null ? value : string.Empty; } }
        public double Min { get { return _Min; } protected set { _Min = value; } }
        public double Max { get { return _Max; } protected set { _Max = value; } }
        public double Value { get { return _Value; } protected set { _Value = value; } }
        #endregion

        #region [ Parse ]
        virtual protected double DoubleValueFromString(string strValue, double defaultIfEmpty = double.MinValue)
        {
            double ret;
            if (!double.TryParse(strValue, out ret))
            {
                ret = defaultIfEmpty;
            }
            return ret;
        }
        #endregion
    }
}
