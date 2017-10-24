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
        protected const int KPIStartIndex = 3;
        private void InitFieldConfig()
        {
            _FieldConfigs.Add(new MessageFieldConfig(UUTIDIndex, "Unit ID", typeof(string)));
        }
        #endregion

        #region [ Data ]
        protected MessageKPI[] _KPIs;
        public string UUTID { get { return FieldAtIndex(UUTIDIndex); } }
        public MessageKPI[] KPIs { get { return _KPIs; } }
        #endregion

        #region [ Life Circle ]
        public ReportKPIMessage(Message.MessageInfo info, string[] fields) : base(info, fields) 
        {
            this.Type = Message.MessageType.REPORT_KPI;

            if (!HasField(UUTIDIndex))
            {
                AddFormatError(string.Format("'{0}' do NOT have <UUT ID> field", this.Type));
            }
            int nRemainingCount = this.FieldCount - KPIStartIndex;
            if ((nRemainingCount % MessageKPI.FieldCount) != 0)
            {
                AddFormatError(string.Format("<KPI> list has missing fields", this.Type));
            }
            if (HasFormatError()) return;
            int nStartIndex = KPIStartIndex;
            List<MessageKPI> listKPI = new List<MessageKPI>();
            while (nStartIndex < this.FieldCount)
            {
                string name = FieldAtIndex(nStartIndex);
                string unit = FieldAtIndex(nStartIndex + 1);
                string minString = FieldAtIndex(nStartIndex + 2);
                string maxString = FieldAtIndex(nStartIndex + 3);
                string valString = FieldAtIndex(nStartIndex + 4);

                if (name.Length <= 0)
                {
                    AddFormatError(string.Format("'{0}' does NOT accept empty <KPI Name>", this.Type));
                }
                if (unit.Length <= 0)
                {
                    AddFormatError(string.Format("'{0}' does NOT accept empty <KPI Name>", this.Type));
                }
                if (minString.Length <= 0)
                {
                }
                else if (isGoodDouble(minString))
                {
                    AddFormatError(string.Format("'{0}' does NOT accept KPI Min <{1}>, it has to be a number", this.Type, minString));
                }
                if (maxString.Length <= 0)
                {
                }
                else if (isGoodDouble(minString))
                {
                    AddFormatError(string.Format("'{0}' does NOT accept KPI Max <{1}>, it has to be a number", this.Type, maxString));
                }
                if (valString.Length <= 0)
                {
                    AddFormatError(string.Format("'{0}' does NOT accept empty <KPI Name>", this.Type));
                }
                else if (isGoodDouble(minString))
                {
                    AddFormatError(string.Format("'{0}' does NOT accept KPI Value <{1}>, it has to be a number", this.Type, valString));
                }
                MessageKPI aKPI = new MessageKPI(name, unit, minString, maxString, valString);
                listKPI.Add(aKPI);
            }
            _KPIs = listKPI.ToArray();
        }
        #endregion

        #region [ Process Message ]
        public override void Process()
        {
            base.Process();

            if (HasFormatError()) return;
            // Now we are sure that all fields valid

            UUTManager uutMgr = Tester.getInstance().UUTMgr;
            UUT anUUT = uutMgr.UUTWithIdentifier(this.UUTID);
            if (anUUT == null)
            {
                AddError(string.Format("'UUT {0}' is not ever Loaded", this.UUTID));
                return;
            }
        }
        #endregion
    }
}
