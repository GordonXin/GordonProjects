using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    public class Message
    {
        #region [ Definition ]
        public enum CommMode : short
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
        public class MessageArgument
        {
            public readonly string MessageString;
            public readonly CommMode MessageCommMode;
            public readonly DateTime MessageTime;
            public readonly string MessageProtocolVersion;
            private const string DefaultMessageProtocolVerstion = "";
            public string CleanMessage { get { return this.MessageString.Trim('\r'); } }

            public MessageArgument(string str, CommMode mode, DateTime time)
            {
                this.MessageString = str != null ? string.Copy(str) : string.Empty;
                this.MessageCommMode = mode;
                this.MessageTime = time != null ? time : DateTime.Now;
                this.MessageProtocolVersion = DefaultMessageProtocolVerstion;
            }
            public MessageArgument(string str, CommMode mode, DateTime time, string ver)
            {
                this.MessageString = str != null ? string.Copy(str) : string.Empty;
                this.MessageCommMode = mode;
                this.MessageTime = time != null ? time : DateTime.Now;
                this.MessageProtocolVersion = (string.IsNullOrWhiteSpace(ver)) ? DefaultMessageProtocolVerstion : ver;
            }
        }
        #endregion

        #region [ Factory ]
        public static Message CreateMessage(MessageArgument arg)
        {
            if (arg == null)
            {
                return new Message();
            }
            // Empty string?
            Message ret = null;
            if (string.IsNullOrWhiteSpace(arg.MessageString))
            {
                ret = new Message(arg);
                ret.AddFormatError("Empty message string");
                return ret;
            }
            // Has enough fields?
            string[] fields = arg.CleanMessage.Split(',');
            if (fields.Length < Message.MinFieldCount)
            {
                ret = new Message(arg, fields, MessageType.UNKNOWN);
                ret.AddFormatError("Message doesn't have at least 2 fields");
                return ret;
            }
            // Has good command type?
            MessageType type = MessageType.UNKNOWN;
            if (!Enum.TryParse(fields[Message.MsgTypeIndex], out type))
            {
                ret = new Message(arg, fields, MessageType.UNKNOWN);
                ret.AddFormatError("Unsupported message type");
                return ret;
            }
            // Create subclass
            switch (type)
            {
                case MessageType.REPORT_UNIT_LOCATION:
                    ret = new ReportLocationMessage(arg, fields, type);
                    break;
                case MessageType.REPORT_EVENT:
                    ret = new ReportEventMessage(arg, fields, type);
                    break;
                case MessageType.REPORT_KPI:
                    //ret = new ReportLocationMessage(arg, fields, type);
                    break;
                case MessageType.REPORT_RESULT:
                    ret = new ReportResultMessage(arg, fields, type);
                    break;
                case MessageType.GET_STATUS:
                    ret = new GetStatusMessage(arg, fields, type);
                    break;
                case MessageType.GET_CONFIG:
                    ret = new GetConfigMessage(arg, fields, type);
                    break;
                case MessageType.UPDATE_STATUS:
                    ret = new UpdateStatusMessage(arg, fields, type);
                    break;
                default:
                    // should never go here
                    ret = new Message(arg, fields, type);
                    ret.AddFormatError("Unknown format error");
                    return ret;
            }
            // Check detail
            ret.Process();
            return ret;
        }
        public static Message CreateMessage(string str, CommMode mode, DateTime time)
        {
            return CreateMessage(new MessageArgument(str, mode, time));
        }
        public static Message CreateMessage(string str, CommMode mode, DateTime time, string ver)
        {
            return CreateMessage(new MessageArgument(str, mode, time, ver));
        }
        #endregion

        #region [ Fields ]
        protected const int MinFieldCount = 2;
        protected const int MsgIDIndex = 0;
        protected const int MsgTypeIndex = 1;
        protected List<string> _Fields;
        protected List<MessageFieldConfig> _FieldConfigs;
        public string[] Fields { get { return _Fields.ToArray(); } }
        public int FieldCount { get { return _Fields.Count; } }
        private void InitFieldConfig()
        {
            _Fields = new List<string>();
            _FieldConfigs = new List<MessageFieldConfig>();
            _FieldConfigs.Add(new MessageFieldConfig(MsgIDIndex, "Message ID", typeof(int)));
            _FieldConfigs.Add(new MessageFieldConfig(MsgTypeIndex, "Command ID"));
        }
        protected virtual bool CheckFields()
        {
            bool isFieldsGood = true;
            foreach (MessageFieldConfig aConfig in _FieldConfigs)
            {
                // 1st. Check field presence
                if (!HasField(aConfig.Index))
                {
                    AddFormatError(string.Format("'{0}' has no {1} field", this.Type, aConfig.PrettyName));
                    return false;
                }
                // 2nd. Check empty string
                string fieldString = FieldAtIndex(aConfig.Index).Trim();
                if (string.IsNullOrWhiteSpace(fieldString))
                {
                    if (!aConfig.AllowsEmpty)
                    {
                        AddFormatError(string.Format("'{0}' has empty {1} field", this.Type, aConfig.PrettyName));
                        isFieldsGood = false;
                    }
                    else
                    {
                        AddWarn(string.Format("'{0}' has empty {1} field", this.Type, aConfig.PrettyName));
                    }
                    continue;
                }
                // 3rd. Check good data format
                if (aConfig.DataType == typeof(int))
                {
                    if (!isGoodInt(fieldString))
                    {
                        AddFormatError(string.Format("'{0}' requires integer for {1} field, but '{2}' is not a good one", this.Type, aConfig.PrettyName, fieldString));
                        isFieldsGood = false;
                    }
                }
                else if (aConfig.DataType == typeof(double))
                {
                    if (!isGoodDouble(fieldString))
                    {
                        AddFormatError(string.Format("'{0}' requires number for {1} field, but '{2}' is not a good one", this.Type, aConfig.PrettyName, fieldString));
                        isFieldsGood = false;
                    }
                }
                else if (aConfig.DataType.IsEnum)
                {
                    if (!isGoodEnum(aConfig.DataType, fieldString))
                    {
                        AddFormatError(string.Format("'{0}' has undefined string '{2}' in {1} field", this.Type, aConfig.PrettyName, fieldString));
                        isFieldsGood = false;
                    }
                }
                else if (aConfig.DataType == typeof(string))
                {
                    if (!string.IsNullOrWhiteSpace(aConfig.StringFormat))
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(fieldString, aConfig.StringFormat))
                        {
                            AddFormatError(string.Format("'{0}' requires '{2}' for {1} field, but '{3}' is not a good one", this.Type, aConfig.PrettyName, aConfig.StringFormat, fieldString));
                            isFieldsGood = false;
                        }
                    }
                }
                else if (aConfig.DataType == typeof(DateTime))
                {
                    if (!string.IsNullOrWhiteSpace(aConfig.StringFormat))
                    {
                        DateTime dt = DateTime.Now;
                        if (!DateTime.TryParseExact(fieldString, aConfig.StringFormat, null, System.Globalization.DateTimeStyles.None, out dt))
                        {
                            AddFormatError(string.Format("'{0}' requires '{2}' for {1} field, but '{3}' is not a good one", this.Type, aConfig.PrettyName, aConfig.StringFormat, fieldString));
                            isFieldsGood = false;
                        }
                    }
                }
            }
            return isFieldsGood;
        }
        protected MessageFieldConfig FieldConfigWithIndex(int index)
        {
            foreach (MessageFieldConfig aConfig in _FieldConfigs)
            {
                if (aConfig.Index == index)
                    return aConfig;
            }
            return null;
        }
        protected MessageFieldConfig FieldConfigWithName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                foreach (MessageFieldConfig aConfig in _FieldConfigs)
                {
                    if (aConfig.Name.Equals(name))
                        return aConfig;
                }
            }
            return null;
        }
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
        protected double DoubleFieldAtIndex(int index, double defaultVal = double.MinValue)
        {
            if (HasField(index))
            {
                double ret;
                if (double.TryParse(_Fields[index], out ret))
                {
                    return ret;
                }
            }
            return defaultVal;
        }
        protected bool isGoodInt(string str)
        {
           return System.Text.RegularExpressions.Regex.IsMatch(str, "\\d");
        }
        protected bool isGoodDouble(string str)
        {
            double tmp;
            return double.TryParse(str, out tmp);
        }
        protected bool isGoodEnum(Type enumType, string str)
        {
            try
            {
                System.Enum.Parse(enumType, str);
            }
            catch
            {
                return false;
            }
            return true;
        }
        protected class MessageFieldConfig
        {
            public readonly int Index;
            public readonly string Name;
            public readonly Type DataType;
            public readonly bool AllowsEmpty;
            public readonly string StringFormat;
            public string PrettyName { get { return string.Format("<{0}>", this.Name); } }
            public MessageFieldConfig(int index, string name, Type dataType, bool allowsEmpty)
            {
                this.Index = index;
                this.Name = name;
                this.AllowsEmpty = allowsEmpty;
                this.DataType = dataType;
                this.StringFormat = null;
            }
            public MessageFieldConfig(int index, string name, Type dataType) : this(index, name, dataType, false) { }
            public MessageFieldConfig(int index, string name) : this(index, name, typeof(string), false) { }
            public MessageFieldConfig(int index, string name, Type dataType, bool allowsEmpty, string format) : this(index, name, dataType, false)
            {
                this.StringFormat = format;
            }
        }
        #endregion

        #region [ Data ]
        private MessageArgument _Argument;
        protected List<MessageInfo> _AllInfo;
        public string MessageString { get { return string.Format("[{2:T}][{0}] {1}",this.Mode==CommMode.Send? "DDD --> FC" : "FC --> DDD",_Argument.MessageString, this.Time); } }
        public CommMode Mode { get { return _Argument.MessageCommMode; } }
        public DateTime Time { get { return _Argument.MessageTime; } }
        public MessageInfo[] AllInfo { get { return _AllInfo.ToArray(); } }
        public MessageType Type { get; private set; }
        
        #endregion

        #region [ Life Circle ]
        private Message()
        {
            _Argument = new MessageArgument("", CommMode.Send, DateTime.Now);
            _AllInfo = new List<MessageInfo>();
            this.Type = MessageType.UNKNOWN;
            InitFieldConfig();

            AddFormatError("Invalid message argument");
        }
        protected Message(MessageArgument arg) : this(arg, null, MessageType.UNKNOWN)
        {
        }
        protected Message(MessageArgument arg, string[] fields, MessageType type)
        {
            _Argument = arg;
            _AllInfo = new List<MessageInfo>();
            this.Type = type;

            InitFieldConfig();
            if (fields != null)
            {
                _Fields.AddRange(fields);
            }
        }
        #endregion

        #region [ Errors ]
        public void AddInfo(MessageInfo info)
        {
            if (info != null)
            {
                _AllInfo.Add(info);
            }
        }
        public void AddInfo(string strInfo, MessageInfo.InfoType type)
        {
            if (string.IsNullOrWhiteSpace(strInfo))
                return;
            if (!Enum.IsDefined(typeof(MessageInfo.InfoType), type))
                return;

            AddInfo(new MessageInfo(strInfo, type));
        }
        public void AddInfoString(string strInfo)
        {
            AddInfo(strInfo, MessageInfo.InfoType.Info);
        }        
        public void AddFormatError(string strInfo)
        {
            AddInfo(strInfo, MessageInfo.InfoType.Format);
        }
        public void AddAlarm(string strInfo)
        {
            AddInfo(strInfo, MessageInfo.InfoType.Alarm);
        }
        public void AddWarn(string strInfo)
        {
            AddInfo(strInfo, MessageInfo.InfoType.Warn);
        }
        public bool HasError(MessageInfo.InfoType type)
        {
            foreach (MessageInfo anInfo in _AllInfo)
            {
                if (anInfo.Type == type)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasError(params MessageInfo.InfoType[] types)
        {
            if (types != null && types.Length > 0)
            {
                foreach (MessageInfo anInfo in _AllInfo)
                {
                    if (Array.IndexOf(types, anInfo.Type) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool HasAlarm()
        {
            return HasError(MessageInfo.InfoType.Alarm);
        }
        public bool HasFormatError()
        {
            return HasError(MessageInfo.InfoType.Format);
        }
        public bool HasWarning()
        {
            return HasError(MessageInfo.InfoType.Warn);
        }
        #endregion

        #region [ Process Message ]
        public virtual void Process()
        {
            if (!CheckFields())
                return;

            if (!CheckData())
                return;
        }

        protected virtual bool CheckData()
        {
            if (HasFormatError())
                return false;

            return true;
        }
        #endregion
    }
}
