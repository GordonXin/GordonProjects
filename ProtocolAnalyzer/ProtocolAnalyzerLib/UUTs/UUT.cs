using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib
{
    class UUT
    {
        #region [ Data ]
        protected string _Identifier;
        protected string _CurrentLocation;
        protected bool _isLoaded;
        protected bool _isUnloaded;
        protected string _Result;

        public string Identifier { get { return _Identifier; } protected set { _Identifier = value != null ? value : string.Empty; }}
        public string CurrentLocation { get { return _CurrentLocation; } set { _CurrentLocation = value != null ? value : string.Empty; } }
        public bool isLoaded { get { return _isLoaded; } set { _isLoaded = value; } }
        public bool isUnloaded { get { return _isUnloaded; } set { _isUnloaded = value; } }

        public bool hasResult { get { return _Result.Equals(MessageResult.Pass) || _Result.Equals(MessageResult.Fail); } }
        public string Result { get { return _Result; } set { _Result = MessageResult.isSupportedResult(value) ? value : MessageResult.Unknown; } }
        #endregion

        #region [ Life Circle ]
        public UUT(string strIdentifier)
        {
            this.Identifier = strIdentifier;
            this.CurrentLocation = string.Empty;
            this.isLoaded = false;
            this.isUnloaded = false;
            this.Result = MessageResult.Unknown;
        }
        #endregion
    }
}
