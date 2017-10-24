using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib
{
    class FunctionManager
    {
        #region [ Data ]
        protected Dictionary<string, Function> _FunctionMap;

        public Function[] Functions { get { return _FunctionMap.Values.ToArray(); } }
        public string[] FunctionNames { get { return _FunctionMap.Keys.ToArray(); } }
        #endregion

        #region [ Life Circle ]
        public FunctionManager()
        {
            _FunctionMap = new Dictionary<string, Function>();
            _FunctionMap.Add(Function.Load, new LoadFunction(Function.Load));
            _FunctionMap.Add(Function.Operate, new OperateFunction(Function.Operate));
            _FunctionMap.Add(Function.Unload, new UnloadFunction(Function.Unload));
        }
        #endregion

        #region [ Functions ]
        public Function FunctionWithName(string strName)
        {
            if (strName != null && _FunctionMap.ContainsKey(strName))
            {
                return _FunctionMap[strName];
            }
            return null;
        }
        public void WillEnterLocation(string strFunction, UUT anUUT, Location aLocation, Message aMessage)
        {
            Function aFun = FunctionWithName(strFunction);
            if (aFun != null)
            {
                aFun.UUTWillEnterLocation(anUUT, aLocation, aMessage);
            }
        }
        public void DidEnterLocation(string strFunction, UUT anUUT, Location aLocation, Message aMessage)
        {
            Function aFun = FunctionWithName(strFunction);
            if (aFun != null)
            {
                aFun.UUTDidEnterLocation(anUUT, aLocation, aMessage);
            }
        }
        public void WillLeaveLocation(string strFunction, UUT anUUT, Location aLocation, Message aMessage)
        {
            Function aFun = FunctionWithName(strFunction);
            if (aFun != null)
            {
                aFun.UUTWillLeaveLocation(anUUT, aLocation, aMessage);
            }
        }
        public void DidLeaveLocation(string strFunction, UUT anUUT, Location aLocation, Message aMessage)
        {
            Function aFun = FunctionWithName(strFunction);
            if (aFun != null)
            {
                aFun.UUTDidLeaveLocation(anUUT, aLocation, aMessage);
            }
        }
        #endregion
    }
}
