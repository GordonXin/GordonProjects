using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class Location : IDisposable
    {
        #region [ Exceptions ]
        public class LocationException : Exception
        {
            public LocationException(string strMessage) : base(strMessage) { }
        }
        #endregion

        #region [ Data ]
        protected string _Locator;
        protected string _NextLocator;
        protected string _SlotLocator;
        protected string _Name;
        protected string _Function;
        protected int _Index;
        protected List<string> _UUTList;
        protected bool _isFirst;
        protected bool _isLast;

        public string Locator { get { return _Locator; } protected set { _Locator = value != null ? value : string.Empty; } }
        public string NextLocator { get { return _NextLocator; } set { _NextLocator = value != null ? value : string.Empty; } }
        public string SlotLocator { get { return _SlotLocator; } protected set { _SlotLocator = value != null ? value : string.Empty; } }
        public string Name { get { return _Name; } protected set { _Name = value != null ? value : string.Empty; } }
        public string Function { get { return _Function; } protected set { _Function = value != null ? value : string.Empty; } }
        public int Index { get { return _Index; } set { _Index = value; } }
        public string[] UUTList { get { return _UUTList.ToArray(); } }
        public bool isFirst { get { return _isFirst; } set { _isFirst = value; } }
        public bool isLast { get { return _isLast; } set { _isLast = value; } }
        public string[] CurrentUUTs { get { return _UUTList.ToArray(); } }
        #endregion

        #region [ Life Circle]
        public Location(string strLocation, string strFunction)
        {
            if (strLocation == null || strLocation.Length <= 0)
            {
                throw new LocationException("Can't create Location on empty locator");
            }

            int index = strLocation.LastIndexOf(Convert.ToChar("."));
            if (index == 0 || index == strLocation.Length - 1)
            {
                throw new LocationException(string.Format("Wrong format of Location Name ({0})", strLocation));
            }
            else if (index > 0)
            {
                this.SlotLocator = strLocation.Substring(0, index);
                this.Locator = string.Copy(strLocation);
                this.Name = strLocation.Substring(index + 1);
            }
            else
            {
                this.SlotLocator = "DefaultSlot";
                this.Locator = string.Copy(strLocation);
                this.Name = string.Copy(strLocation);
            }
            this.Function = strFunction;
            this.Index = -1;
            this.isFirst = false;
            this.isLast = false;
            this.NextLocator = string.Empty;

            _UUTList = new List<string>();
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

        #region [ UUT ]
        virtual public bool AddUUT(string strUUTIdentifier)
        {
            if (strUUTIdentifier != null && strUUTIdentifier.Length <= 0)
            {
                _UUTList.Add(strUUTIdentifier);
                return true;
            }
            return false;
        }
        virtual public bool RemoveUUT(string strUUTIdentifier)
        {
            if (strUUTIdentifier != null && strUUTIdentifier.Length <= 0)
            {
                return _UUTList.Remove(strUUTIdentifier);
            }
            return false;
        }
        virtual public bool HasUUT(string strUUTIdentifier)
        {
            if (strUUTIdentifier != null && strUUTIdentifier.Length <= 0)
            {
                foreach (string aStr in _UUTList)
                {
                    if (aStr.Equals(strUUTIdentifier))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region [ Compare ]
        public bool isInSameSlot(Location other)
        {
            return other != null && this.SlotLocator.Equals(other.SlotLocator);
        }
        public bool isNextLocation(Location other)
        {
            return other != null && this.NextLocator.Equals(other.Locator);
        }
        public bool isPrevLocation(Location other)
        {
            return other != null && this.Locator.Equals(other.NextLocator);
        }
        #endregion
    }
}
