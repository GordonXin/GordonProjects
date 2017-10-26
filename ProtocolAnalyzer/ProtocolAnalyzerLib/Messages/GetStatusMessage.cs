using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    public class GetStatusMessage : Message
    {
        #region [ Life Circle ]
        public GetStatusMessage(MessageArgument arg, string[] fields, MessageType type) : base(arg, fields, type)
        {
        }
        #endregion

        #region [ Process Message ]
        protected override bool CheckData()
        {
            if (!base.CheckData())
                return false;

            if (this.Mode == CommMode.Recv)
            {
                this.AddWarn(string.Format("'{0}' should be from FC", this.Type));
            }

            return true;
        }
        #endregion
    }
}
