using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class UUT
    {
        #region [ Data ]
        protected string _Identifier;
        protected string _CurrentLocation;

        public string Identifier { get { return _Identifier; } protected set { _Identifier = value != null ? value : string.Empty; }}
        public string CurrentLocation { get { return _CurrentLocation; } set { _CurrentLocation = value != null ? value : string.Empty; } }
        public bool isLoaded { get; set; }
        public bool isUnloaded { get; set; }
        public bool hasResult { get { return System.Enum.IsDefined(typeof(UUTResult), this.Result); } }
        public UUTResult Result { get; set; }
        #endregion

        #region [ Life Circle ]
        public UUT(string strIdentifier)
        {
            this.Identifier = strIdentifier;
            this.CurrentLocation = string.Empty;
            this.isLoaded = false;
            this.isUnloaded = false;
            this.Result = (UUTResult)(-1);
        }
        #endregion
    }
}
