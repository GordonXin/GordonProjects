using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class ReportKPIMessage : Message
    {
        #region [ Constant ]
        protected const int UUTIDIndex = 2;
        protected const int FieldCountForEachKPI = 5;
        private void InitFieldConfig()
        {
            _FieldConfigs.Add(new MessageFieldConfig(UUTIDIndex, "Unit ID", typeof(string)));
            int remainFieldsCount = this.FieldCount - 1;
            int KPICount = remainFieldsCount / 5 + ((remainFieldsCount % 5 == 0) ? 0 : 1);
            int indexOffset = 1;
            for (int i = 0; i < KPICount; ++i)
            {
                _FieldConfigs.Add(new MessageFieldConfig(indexOffset++, "KPI Name", typeof(string)));
                _FieldConfigs.Add(new MessageFieldConfig(indexOffset++, "KPI Unit", typeof(string)));
                _FieldConfigs.Add(new MessageFieldConfig(indexOffset++, "KPI Min", typeof(double), true));
                _FieldConfigs.Add(new MessageFieldConfig(indexOffset++, "KPI Max", typeof(double), true));
                _FieldConfigs.Add(new MessageFieldConfig(indexOffset++, "KPI Value", typeof(double)));
            }
        }
        #endregion

        #region [ Data ]
        //protected MessageKPI[] _KPIs;
        public string UUTID { get { return FieldAtIndex(UUTIDIndex); } }
        //public MessageKPI[] KPIs { get { return _KPIs; } }
        #endregion

        #region [ Life Circle ]
        public ReportKPIMessage(MessageArgument arg, string[] fields, MessageType type) : base(arg, fields, type)
        {
            InitFieldConfig();
        }
        #endregion

        #region [ Process Message ]
        protected override bool CheckData()
        {
            if (!base.CheckData())
                return false;

            UUTManager uutMgr = ProtocolAnalyzer.Instance().UUTMgr;
            UUT anUUT = uutMgr.UUTWithIdentifier(this.UUTID);
            if (anUUT == null)
            {
                AddAlarm(string.Format("UUT '{0}' is not ever Loaded", this.UUTID));
            }

            return true;
        }
        #endregion
    }
}
