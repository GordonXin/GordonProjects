using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class LoadFunction : Function
    {
        #region [ Data ]

        #endregion

        #region [ Life Circle ]
        public LoadFunction(string strName) : base(strName)
        {
        }
        #endregion

        #region [ Execution ]
        public override void UUTWillEnterLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
            if (anUUT == null || aLocation == null || aMessage == null) return;
            if (anUUT.isLoaded)
            {
                aMessage.AddAlarm(string.Format("UUT '{0}' is already Loaded", anUUT.Identifier));
            }
            if (anUUT.isUnloaded)
            {
                aMessage.AddAlarm(string.Format("UUT '{0}' is Unloaded", anUUT.Identifier));
            }
        }
        public override void UUTDidLeaveLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
            if (anUUT == null) return;

            anUUT.isLoaded = true;
            anUUT.isUnloaded = false;
            anUUT.Result = (UUTResult)(-1);
        }
        #endregion
    }
}
