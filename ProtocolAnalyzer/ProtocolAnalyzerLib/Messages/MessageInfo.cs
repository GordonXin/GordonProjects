using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    public class MessageInfo
    {
        #region [ Definitions ]
        public enum InfoType : short
        {
            Format,
            Alarm,
            Warn,
            Info,
        }
        #endregion

        #region [ Data ]
        protected string _Message;
        public string Message { get { return TranslateMessage(); } protected set { _Message = value != null ? value : string.Empty; } }
        public InfoType Type { get; protected set; }
        #endregion

        #region [ Life Circle ]
        public MessageInfo(string strErrorMessage, InfoType nType)
        {
            this.Message = string.Copy(strErrorMessage);
            this.Type = nType;
        }
        #endregion

        #region [ Format Message ]
        private string TranslateMessage()
        {
            return string.Format("[{0}] {1}", this.Type, _Message);
        }
        #endregion
    }
}
