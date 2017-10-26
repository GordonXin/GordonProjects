using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class ReportResultMessage : Message
    {
        #region [ Fields ]
        protected const int UUTIDIndex = 2;
        protected const int ResultIndex = 3;
        protected const int StartTimeIndex = 4;
        protected const int EndTimeIndex = 5;
        protected const int LocationIndex = 6;
        protected const int GradeIndex = 7;
        protected const int GradeMessageIndex = 8;
        private void InitFieldConfig()
        {
            _FieldConfigs.Add(new MessageFieldConfig(UUTIDIndex, "Unit ID", typeof(string)));
            _FieldConfigs.Add(new MessageFieldConfig(ResultIndex, "Unit Result", typeof(UUTResult)));
            _FieldConfigs.Add(new MessageFieldConfig(StartTimeIndex, "Start Time", typeof(DateTime), false, "MM/dd/yyyy HH:mm:ss"));
            _FieldConfigs.Add(new MessageFieldConfig(EndTimeIndex, "End Time", typeof(string), false, "MM/dd/yyyy HH:mm:ss"));
            _FieldConfigs.Add(new MessageFieldConfig(LocationIndex, "Location", typeof(string)));
            _FieldConfigs.Add(new MessageFieldConfig(GradeIndex, "Grade", typeof(int), true));
            _FieldConfigs.Add(new MessageFieldConfig(GradeMessageIndex, "Grade Message", typeof(string), true));
        }
        #endregion

        #region [ Data ]
        public string UUTID { get { return FieldAtIndex(UUTIDIndex); } }
        public UUTResult Result { get { return (UUTResult)IntFieldAtIndex(ResultIndex); } }
        public string StartTime { get { return FieldAtIndex(StartTimeIndex); } }
        public string EndTime { get { return FieldAtIndex(EndTimeIndex); } }
        public string Location { get { return FieldAtIndex(LocationIndex); } }
        public int Grade { get { return IntFieldAtIndex(GradeIndex); } }
        public string GradeMessage { get { return FieldAtIndex(GradeMessageIndex); } }
        #endregion

        #region [ Life Circle ]
        public ReportResultMessage(MessageArgument arg, string[] fields, MessageType type) : base(arg, fields, type)
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
            LocationManager locationMgr = ProtocolAnalyzer.Instance().LocationMgr;
            Location aLocation = locationMgr.LocationWithLocator(this.Location);
            UUT anUUT = uutMgr.UUTWithIdentifier(this.UUTID);
            bool isGoodData = true;

            if (aLocation == null)
            {
                AddAlarm(string.Format("Location '{0}' is not defined", this.Location));
                return false;
            }
            if (anUUT == null)
            {
                AddAlarm(string.Format("UUT '{0}' is not ever Loaded", this.UUTID));
                return false;
            }
            if (!aLocation.HasUUT(anUUT.Identifier))
            {
                AddAlarm(string.Format("UUT '{0}' is not at '{1}'", anUUT.Identifier, aLocation.Locator));
            }
            if (!aLocation.Function.Equals(Function.Operate))
            {
                AddAlarm(string.Format("Location'{0}' does NOT expect result", aLocation.Locator));
            }
            return isGoodData;
        }
        #endregion
    }
}
