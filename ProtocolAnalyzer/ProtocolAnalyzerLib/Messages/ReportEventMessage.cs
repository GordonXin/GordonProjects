using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtocolAnalyzerLib;

namespace ProtocolAnalyzerLib
{
    public class ReportEventMessage : Message
    {
        #region [ Fields ]
        protected const int UUTIDIndex = 2;
        protected const int LocationIndex = 3;
        protected const int EventIDIndex = 4;
        protected const int EventTypeIndex = 5;
        protected const int EventMessageIndex = 6;
        private void InitFieldConfig()
        {
            _FieldConfigs.Add(new MessageFieldConfig(UUTIDIndex, "Unit ID", typeof(string), true));
            _FieldConfigs.Add(new MessageFieldConfig(LocationIndex, "Unit Location", typeof(string), true));
            _FieldConfigs.Add(new MessageFieldConfig(EventIDIndex, "Event ID", typeof(int)));
            _FieldConfigs.Add(new MessageFieldConfig(EventTypeIndex, "Event Type", typeof(EventLevel)));
            _FieldConfigs.Add(new MessageFieldConfig(EventMessageIndex, "Event Message", typeof(string)));
        }
        #endregion

        #region [ Definitions ]
        public enum EventLevel : short
        {
            AUTOMATION,
            IDLE,
            MANUAL,
            ALARM,
        }
        #endregion

        #region [ Data ]
        public string UUTID { get { return FieldAtIndex(UUTIDIndex); } }
        public string Location { get { return FieldAtIndex(LocationIndex); } }
        public int EventID { get { return IntFieldAtIndex(EventIDIndex); } }
        public string EventType { get { return FieldAtIndex(EventTypeIndex); } }
        public string EventMessage { get { return FieldAtIndex(EventMessageIndex); } }
        #endregion

        #region [ Life Circle ]
        public ReportEventMessage(MessageArgument arg, string[] fields, MessageType type) : base(arg, fields, type) 
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
            UUT anUUT = uutMgr.UUTWithIdentifier(this.UUTID);
            Location aLocation = locationMgr.LocationWithLocator(this.Location);

            if (this.Location.Length > 0 && aLocation == null)
            {
                AddAlarm(string.Format("Location '{0}' is not defined", this.Location));
            }
            if (this.UUTID.Length > 0 && anUUT == null)
            {
                AddAlarm(string.Format("UUT '{0}' is not ever loaded", this.UUTID));
            }
            if (anUUT != null && aLocation != null)
            {
                if (!aLocation.HasUUT(anUUT.Identifier))
                {
                    AddAlarm(string.Format("UUT '{0}' is not at Location '{1}'", this.UUTID, this.Location));
                }
            }

            return true;
        }
        #endregion
    }
}
