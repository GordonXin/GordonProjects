using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    public class UpdateStatusMessage : Message
    {
        #region [ Fields ]
        protected const int FCStateIndex = 2;
        protected const int IOMapIndex = 3;
        protected const int ErrorMapIndex = 4;
        private void InitFieldConfig()
        {
            _FieldConfigs.Add(new MessageFieldConfig(FCStateIndex, "FC State", typeof(FCState)));
            _FieldConfigs.Add(new MessageFieldConfig(IOMapIndex, "IO Bit Map", typeof(int)));
            _FieldConfigs.Add(new MessageFieldConfig(ErrorMapIndex, "Error Bit Map", typeof(int)));
        }
        #endregion

        #region [ Life Circle ]
        public UpdateStatusMessage(MessageArgument arg, string[] fields, MessageType type) : base(arg, fields, type) 
        {
            InitFieldConfig();
        }
        #endregion

        #region [ Process Message ]
        #endregion
    }
}
