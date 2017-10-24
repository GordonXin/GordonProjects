using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class OperateFunction : Function
    {
        #region [ Life Circle ]
        public OperateFunction(string strName) : base(strName)
        {
        }
        #endregion

        #region [ Execution ]
        public override void UUTWillEnterLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
            if (anUUT == null || aLocation == null || aMessage == null) return;

            if (!anUUT.isLoaded)
            {
                aMessage.AddAlarm(string.Format("UUT '{0}' is not ever Loaded", anUUT.Identifier));
            }
            if (anUUT.isUnloaded)
            {
                aMessage.AddAlarm(string.Format("UUT '{0}' is Unloaded", anUUT.Identifier));
            }
            if (!anUUT.hasResult)
            {
                aMessage.AddAlarm(string.Format("UUT '{0}' already has a result before entering <{1}>", anUUT.Identifier, aLocation.Locator));
            }
        }
        public override void UUTDidEnterLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
            if (anUUT == null) return;
            // clear all possible errors to get clean error info in next steps
            anUUT.isLoaded = true;
            anUUT.isUnloaded = false;
            anUUT.Result = (UUTResult)(-1);
        }
        public override void UUTWillLeaveLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
            if (anUUT == null || aLocation == null || aMessage == null) return;

            if (!anUUT.hasResult)
            {
                aMessage.AddAlarm(string.Format("UUT '{0}' does NOT have a valid result before leaving <{1}>", anUUT.Identifier, aLocation.Locator));
            }
        }
        public override void UUTDidLeaveLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
            if (anUUT == null) return;
            // clear all possible errors to get clean error info in next steps
            anUUT.isLoaded = true;
            anUUT.isUnloaded = false;
        }
        #endregion
    }
}
