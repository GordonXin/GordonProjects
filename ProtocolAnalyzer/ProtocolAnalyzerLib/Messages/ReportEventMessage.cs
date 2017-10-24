using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib
{
    class ReportEventMessage : Message
    {
        #region [ Constant ]
        protected const int UUTIDIndex = 2;
        protected const int LocationIndex = 3;
        protected const int EventIDIndex = 4;
        protected const int EventTypeIndex = 5;
        protected const int EventMessageIndex = 6;
        #endregion

        #region [ Data ]
        protected MessageEvent _Event;
        public string UUTID { get { return FieldAtIndex(UUTIDIndex); } }
        public string Location { get { return FieldAtIndex(LocationIndex); } }
        public int EventID { get { return IntFieldAtIndex(EventIDIndex); } }
        public string EventType { get { return FieldAtIndex(EventTypeIndex); } }
        public string EventMessage { get { return FieldAtIndex(EventMessageIndex); } }
        #endregion

        #region [ Life Circle ]
        public ReportEventMessage(Message.MessageInfo info, string[] fields) : base(info, fields) 
        {
            this.Type = Message.MessageType.REPORT_EVENT;

            if (!HasField(UUTIDIndex))
            {
                AddFormatError(string.Format("'{0}' do NOT have <UUT ID> field", this.Type));
            }
            if (!HasField(LocationIndex))
            {
                AddFormatError(string.Format("'{0}' Do not have <Location> field", this.Type));
            }
            if (!HasField(EventIDIndex))
            {
                AddFormatError(string.Format("'{0}' Do not have <Event ID> field", this.Type));
            }
            if (!HasField(EventTypeIndex))
            {
                AddFormatError(string.Format("'{0}' Do not have <Event Type> field", this.Type));
            }
            if (!HasField(EventMessageIndex))
            {
                AddFormatError(string.Format("'{0}' Do not have <Event Message> field", this.Type));
            }

            if (HasFormatError()) return;

            if (this.UUTID.Length <= 0)
            {
                AddError(string.Format("'{0}' does NOT accept empty <UUT ID>", this.Type), MessageError.ErrorType.Warn);
            }
            if (this.Location.Length <= 0)
            {
                AddError(string.Format("'{0}' does NOT accept empty <Location>", this.Type), MessageError.ErrorType.Warn);
            }
            if (this.FieldAtIndex(EventIDIndex).Length <= 0)
            {
                AddFormatError(string.Format("'{0}' does NOT accept empty <Event ID>", this.Type));
            }
            else
            {
                if (!isGoodInt(this.FieldAtIndex(EventIDIndex)))
                {
                    AddFormatError(string.Format("'{0}' does NOT have correct <Event ID> '{1}', it has to be an integer", this.Type, this.FieldAtIndex(EventIDIndex)));
                }
            }
            if (this.EventType.Length <= 0)
            {
                AddFormatError(string.Format("'{0}' does NOT accept empty <Event Type>", this.Type));
            }
            else if (!MessageEvent.EventType.isTypeSupported(this.EventType))
            {
                AddFormatError(string.Format("'{0}' does NOT accept empty Event Type of <{1}>", this.Type, this.EventType));
            }
            if (this.EventMessage.Length <= 0)
            {
                AddFormatError(string.Format("'{0}' does NOT accept empty <Event Message>", this.Type));
            }
            _Event = new MessageEvent(this.EventID, this.EventType, this.EventMessage);
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
            LocationManager locationMgr = Tester.getInstance().LocationMgr;
            Location aLocation = locationMgr.LocationWithLocator(this.Location);

            if (this.Location.Length > 0 && aLocation == null)
            {
                AddError(string.Format("Location <{0}> is not defined", this.Location));
            }
            if (this.UUTID.Length > 0 && anUUT == null)
            {
                AddError(string.Format("UUT <{0}> is not ever loaded", this.UUTID));
            }
            if (anUUT == null || aLocation == null) return;

            if (!aLocation.HasUUT(anUUT.Identifier))
            {
                AddError(string.Format("UUT <{0}> is not at Location <{1}>", this.UUTID, this.Location));
            }
        }
        #endregion
    }
}
