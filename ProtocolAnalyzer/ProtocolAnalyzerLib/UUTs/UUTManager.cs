using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class UUTManager
    {
        #region [ Data ]
        private Dictionary<string, UUT> _uutMap;

        public UUT[] UUTs { get { return _uutMap.Values.ToArray(); } }
        public string[] Identifiers { get { return _uutMap.Keys.ToArray(); } }
        #endregion

        #region [ Life Circle ]
        public UUTManager()
        {
            _uutMap = new Dictionary<string, UUT>();
        }
        public void Reset()
        {
            _uutMap.Clear();
        }
        #endregion

        #region [ UUTs ]
        public UUT UUTWithIdentifier(string strIdentifier)
        {
            UUT ret = null;
            if (!string.IsNullOrWhiteSpace(strIdentifier))
            {
                if (_uutMap.ContainsKey(strIdentifier))
                {
                    ret = _uutMap[strIdentifier];
                }
            }
            return ret;
        }
        public UUT AddUUT(string strIdentifier)
        {
            UUT ret = null;
            if (!string.IsNullOrWhiteSpace(strIdentifier))
            {
                ret = _uutMap.ContainsKey(strIdentifier) ? _uutMap[strIdentifier] : null;
                if (ret == null)
                {
                    ret = new UUT(strIdentifier);
                    _uutMap.Add(ret.Identifier, ret);
                }
            }
            return ret;
        }
        public void RemoveUUT(string strIdentifier)
        {
            if (!string.IsNullOrWhiteSpace(strIdentifier))
            {
                if (_uutMap.ContainsKey(strIdentifier))
                {
                    _uutMap.Remove(strIdentifier);
                }
            }
        }
        #endregion
    }
}
