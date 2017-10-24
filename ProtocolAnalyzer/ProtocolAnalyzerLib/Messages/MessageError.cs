using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib.Messages
{
    public class MessageError
    {
        #region [ Definitions ]
        public enum ErrorType : short
        {
            Format,
            Alarm,
            Warn,
        }
        #endregion

        #region [ Data ]
        protected string _Message;
        protected ErrorType _Type;

        public string Message { get { return TranslateMessage(); } protected set { _Message = value != null ? value : string.Empty; } }
        public ErrorType Type { get; protected set; }
        #endregion

        #region [ Life Circle ]
        public MessageError(string strErrorMessage, ErrorType nType)
        {
            this.Message = strErrorMessage;
            this.Type = nType;
        }
        #endregion

        #region [ Message ]
        private string TranslateMessage()
        {
            return string.Format("[{0}] {1}", _Type, _Message);
        }
        #endregion
    }
}
