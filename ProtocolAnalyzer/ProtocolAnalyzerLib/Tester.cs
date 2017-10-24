using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib
{
    public class Tester
    {
        #region [ Data ]
        internal LocationManager LocationMgr;
        internal UUTManager UUTMgr;
        internal FunctionManager FunctionMgr;
        #endregion

        #region [ Single Instance ]
        private static Tester _Tester;
        public static Tester getInstance()
        {
            if (_Tester == null)
            {
                _Tester = new ProtocolTesterLib.Tester();
            }
            return _Tester;
        }
        #endregion

        #region [ Life Circle ]
        public Tester()
        {
            LocationMgr = new LocationManager();
            UUTMgr = new UUTManager();
            FunctionMgr = new FunctionManager();
        }
        #endregion

        #region [ Init ]
        public void AddLocation(string strLocationName, string strFunctionName)
        {
            LocationMgr.AddLocation(strLocationName, strFunctionName);
        }
        public void CheckLocations()
        {
            LocationMgr.CheckLocations();
        }
        #endregion

        #region [ Process Messages ]
        public Message ProcessMessage(string strMessage, Message.MessageMode mode, DateTime time)
        {
            Message aMessage = Message.MesssageFromString(strMessage, mode, time);
            aMessage.Process();
            return aMessage;
        }
        #endregion
    }
}
