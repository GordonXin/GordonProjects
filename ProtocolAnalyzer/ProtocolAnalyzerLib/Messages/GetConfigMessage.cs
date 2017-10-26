using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class GetConfigMessage : Message
    {
        #region [ Fields ]
        protected const int DevTypeIndex = 2;
        protected const int DevOSVerIndex = 3;
        protected const int DevAppVerIndex = 4;
        protected const int ProtocolVerIndex = 5;
        protected const int FCIdIndex = 6;
        protected const int DateIndex = 7;
        protected const int TimeIndex = 8;
        private void InitFieldConfig()
        {
            _FieldConfigs.Add(new MessageFieldConfig(DevTypeIndex, "Device Type", typeof(string)));
            _FieldConfigs.Add(new MessageFieldConfig(DevOSVerIndex, "Device OS Type", typeof(string)));
            _FieldConfigs.Add(new MessageFieldConfig(DevAppVerIndex, "Device App Version", typeof(string)));
            _FieldConfigs.Add(new MessageFieldConfig(ProtocolVerIndex, "Protocol Version", typeof(string)));
            _FieldConfigs.Add(new MessageFieldConfig(FCIdIndex, "FC ID", typeof(string)));
            _FieldConfigs.Add(new MessageFieldConfig(DateIndex, "Date", typeof(DateTime), false, "MM/dd/yyyy"));
            _FieldConfigs.Add(new MessageFieldConfig(TimeIndex, "Time", typeof(DateTime), false, "HH:mm:ss"));
        }
        #endregion

        #region [ Life Circle ]
        public GetConfigMessage(MessageArgument arg, string[] fields, MessageType type) : base(arg, fields, type) 
        {
            if (this.Mode == CommMode.Recv)
            {
                InitFieldConfig();
            }
        }
        #endregion

        #region [ Process Message ]
        #endregion
    }
}
