using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib
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
        #endregion

        #region [ UUTs ]
        public UUT UUTWithIdentifier(string strIdentifier, bool createNewIfNotExist = false)
        {
            UUT ret = null;
            if (strIdentifier != null && strIdentifier.Length > 0)
            {
                if (_uutMap.ContainsKey(strIdentifier))
                {
                    ret = _uutMap[strIdentifier];
                }
                if (ret == null && createNewIfNotExist)
                {
                    ret = AddUUT(strIdentifier);
                }
            }
            return ret;
        }
        public UUT AddUUT(string strIdentifier)
        {
            UUT ret = null;
            if (strIdentifier != null && strIdentifier.Length > 0)
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
            if (strIdentifier != null && strIdentifier.Length > 0)
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
