using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib.Messages
{
    public class Message
    {
        public class MessageInfo
        {
            public readonly string MsgString;
            public readonly MessageMode MsgMode;
            public readonly DateTime MsgTime;

            public MessageInfo(string str, MessageMode mode, DateTime time)
            {
                str = str != null ? string.Copy(str) : string.Empty;
                if (str.EndsWith("\r"))
                    str = str.Remove(str.Length - 1);

                MsgString = str;
                MsgMode = mode;
                MsgTime = time != null ? time : DateTime.Now;
            }
        }

        public enum MessageMode : short
        {
            Send,
            Recv,
        }

        public enum MessageType : short
        {
            UNKNOWN,
            GET_CONFIG,
            GET_STATUS,
            UPDATE_STATUS,
            REPORT_UNIT_LOCATION,
            REPORT_RESULT,
            REPORT_KPI,
            REPORT_EVENT,
        }

        #region [ MessageType ]
        public static MessageType[] SupportedTypes = 
        {
            MessageType.GET_CONFIG,
            MessageType.GET_STATUS,
            MessageType.UPDATE_STATUS,
            MessageType.REPORT_UNIT_LOCATION,
            MessageType.REPORT_RESULT,
            MessageType.REPORT_KPI,
            MessageType.REPORT_EVENT,
        };
        public static string[] SupportedTypeStrings =
        {
            MessageType.GET_CONFIG.ToString(),
            MessageType.GET_STATUS.ToString(),
            MessageType.UPDATE_STATUS.ToString(),
            MessageType.REPORT_UNIT_LOCATION.ToString(),
            MessageType.REPORT_RESULT.ToString(),
            MessageType.REPORT_KPI.ToString(),
            MessageType.REPORT_EVENT.ToString(),
        };
        public static bool isSupportedType(string type)
        {
            if (type == null || type.Length <= 0)
                return false;
            
            foreach (string aType in SupportedTypeStrings)
            {
                if (aType.Equals(type))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region [Constant]
        protected const int MinFieldCount = 2;
        protected const int MsgIDIndex = 0;
        protected const int MsgTypeIndex = 1;
        #endregion

        #region [ Data ]
        private MessageInfo _Info;
        protected List<string> _Fields;
        protected List<MessageError> _Errors;
        protected MessageType _Type;

        public string[] Fields { get { return _Fields.ToArray(); } }
        public int FieldCount { get { return _Fields.Count; } }
        public string MessageString { get { return _Info.MsgString; } }
        public MessageMode Mode { get { return _Info.MsgMode; } }
        public DateTime Time { get { return _Info.MsgTime; } }
        public MessageError[] Errors { get { return _Errors.ToArray(); } }
        public MessageType Type { get { return _Type; } protected set { _Type = value; } }
        #endregion

        #region [ Life Circle ]
        protected Message(MessageInfo info, string[] fields = null)
        {
            _Info = info;
            _Fields = new List<string>((fields != null && fields.Length > 0) ? fields : new string[0]);
            _Errors = new List<MessageError>();
            _Type = MessageType.UNKNOWN;
        }
        #endregion

        #region [ Factory Methods ]
        public static string[] FieldsFromString(string str)
        {
            string[] ret = null;
            if (str != null && str.Length > 0)
            {
                ret = str.Trim(' ', '\r').Split(',');
            }

            return ret != null ? ret : new string[0];
        }
        public static Message MesssageFromString(string str, MessageMode mode, DateTime time)
        {
            Message ret = null;
            MessageInfo info = new MessageInfo(str, mode, time);

            if (str == null || str.Length <= 0)
            {
                ret = new Message(info);
                ret.AddFormatError("Empty message string");
                return ret;
            }

            string[] fields = Message.FieldsFromString(str);
            if (fields.Length < Message.MinFieldCount)
            {
                ret = new Message(info);
                ret.AddFormatError("Message doesn't have at least 2 fields");
                return ret;
            }

            string type = fields[Message.MsgTypeIndex];
            if (!Message.isSupportedType(type))
            {
                ret = new Message(info);
                ret.AddFormatError("Unsupported message type");
                return ret;
            }

            if (type.Equals(MessageType.REPORT_UNIT_LOCATION.ToString()))
            {
                ret = new ReportLocationMessage(info, fields);
            }
            else if (type.Equals(MessageType.REPORT_EVENT.ToString()))
            {
                ret = new ReportEventMessage(info, fields);
            }
            else if (type.Equals(MessageType.REPORT_KPI.ToString()))
            {
                ret = new ReportKPIMessage(info, fields);
            }
            else if (type.Equals(MessageType.REPORT_RESULT.ToString()))
            {
                ret = new ReportResultMessage(info, fields);
            }

            if (ret == null)
            {
                ret = new Message(info);
                ret.AddFormatError("Unknown format error");
                return ret;
            }
            return ret;
        }
        #endregion

        #region [ Errors ]
        public void AddError(MessageError error)
        {
            if (error != null)
            {
                _Errors.Add(error);
            }
        }

        public void AddError(string strErrorMsg, MessageError.ErrorType type = MessageError.ErrorType.Alarm)
        {
            if (strErrorMsg != null && strErrorMsg.Length > 0)
            {
                _Errors.Add(new MessageError(strErrorMsg, type));
            }
        }
        
        public void AddFormatError(string strErrorMsg)
        {
            AddError(strErrorMsg, MessageError.ErrorType.Format);
        }

        public bool HasError(MessageError.ErrorType type)
        {
            foreach (MessageError anError in _Errors)
            {
                if (anError.Type == type)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasAlarm()
        {
            return HasError(MessageError.ErrorType.Alarm);
        }

        public bool HasFormatError()
        {
            return HasError(MessageError.ErrorType.Format);
        }

        public bool HasWarning()
        {
            return HasError(MessageError.ErrorType.Warn);
        }
        #endregion

        #region [ Field Access]
        protected bool HasField(int index)
        {
            if (index >= 0 && index < _Fields.Count)
            {
                return true;
            }
            return false;
        }
        protected string FieldAtIndex(int index, string defaultVal = "")
        {
            if (HasField(index))
            {
                return _Fields[index];
            }
            return defaultVal;
        }
        protected int IntFieldAtIndex(int index, int defaultVal = int.MinValue)
        {
            if (HasField(index))
            {
                int ret;
                if (int.TryParse(_Fields[index], out ret))
                {
                    return ret;
                }
            }
            return defaultVal;
        }
        protected bool isGoodInt(string str)
        {
            int tmp;
            return int.TryParse(str, out tmp);
        }
        protected bool isGoodDouble(string str)
        {
            double tmp;
            return double.TryParse(str, out tmp);
        }
        #endregion

        #region [ Process Message ]
        public virtual void Process()
        {
            // No action in base implementation
            // derived class should override this method
        }
        #endregion
    }
}
