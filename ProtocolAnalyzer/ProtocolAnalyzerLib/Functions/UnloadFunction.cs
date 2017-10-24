using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib
{
    class UnloadFunction : Function
    {
        #region [ Data ]

        #endregion

        #region [ Life Circle ]
        public UnloadFunction(string strName) : base(strName)
        {
        }
        #endregion

        #region [ Execution ]
        public override void UUTWillEnterLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
            if (anUUT == null || aLocation == null || aMessage == null) return;

            if (!anUUT.isLoaded)
            {
                aMessage.AddError(string.Format("UUT '{0}' is not ever Loaded but it's unloaded now", anUUT.Identifier));
            }
            if (anUUT.isUnloaded)
            {
                aMessage.AddError(string.Format("UUT '{0}' is double Unloaded", anUUT.Identifier));
            }
            if (anUUT.Result.Equals(MessageResult.Unknown))
            {
                aMessage.AddError(string.Format("UUT '{0}' is Unloaded before have any valid result", anUUT.Identifier));
            }
        }
        public override void UUTDidEnterLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
            if (anUUT == null) return;
            // clear all possible errors to get clean error info in next steps
            anUUT.isLoaded = true;
            anUUT.isUnloaded = true;
        }
        #endregion
    }
}
