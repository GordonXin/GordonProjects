using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib.Messages
{
    class ReportResultMessage : Message
    {
        #region [ Constant ]
        protected const int UUTIDIndex = 2;
        protected const int ResultIndex = 3;
        protected const int StartTimeIndex = 4;
        protected const int EndTimeIndex = 5;
        protected const int LocationIndex = 6;
        protected const int GradeIndex = 7;
        protected const int GradeMessageIndex = 8;
        #endregion

        #region [ Data ]
        public string UUTID { get { return FieldAtIndex(UUTIDIndex); } }
        public string Result { get { return FieldAtIndex(ResultIndex); } }
        public string StartTime { get { return FieldAtIndex(StartTimeIndex); } }
        public string EndTime { get { return FieldAtIndex(EndTimeIndex); } }
        public string Location { get { return FieldAtIndex(LocationIndex); } }
        public int Grade { get { return IntFieldAtIndex(GradeIndex); } }
        public string GradeMessage { get { return FieldAtIndex(GradeMessageIndex); } }
        #endregion

        #region [ Life Circle ]
        public ReportResultMessage(Message.MessageInfo info, string[] fields) : base(info, fields)
        {
            this.Type = Message.MessageType.REPORT_RESULT;

            if (!HasField(UUTIDIndex))
            {
                AddFormatError(string.Format("'{0}' do NOT have <UUT ID> field", this.Type));
            }
            if (!HasField(ResultIndex))
            {
                AddFormatError(string.Format("'{0}' do NOT have <Result> field", this.Type));
            }
            if (!HasField(StartTimeIndex))
            {
                AddFormatError(string.Format("'{0}' do NOT have <Start Time> field", this.Type));
            }
            if (!HasField(EndTimeIndex))
            {
                AddFormatError(string.Format("'{0}' do NOT have <End Time> field", this.Type));
            }
            if (!HasField(LocationIndex))
            {
                AddFormatError(string.Format("'{0}' Do not have <Location> field", this.Type));
            }
            if (!HasField(LocationIndex))
            {
                AddFormatError(string.Format("'{0}' Do not have <Grade Code> field", this.Type));
            }
            if (!HasField(GradeMessageIndex))
            {
                AddFormatError(string.Format("'{0}' Do not have <Grade Message> field", this.Type));
            }
            if (HasFormatError()) return;

            if (this.UUTID.Length <= 0)
            {
                AddFormatError(string.Format("'{0}' does NOT accept empty <UUT ID>", this.Type));
            }
            if (this.Result.Length <= 0)
            {
                AddFormatError(string.Format("'{0}' does NOT accept empty <Result>", this.Type));
            }
            else if (!MessageResult.isSupportedResult(this.Result))
            {
                AddFormatError(string.Format("'{0}' does NOT accept Result <{1}>", this.Type, this.Result));
            }
            else if (this.Result.Equals(MessageResult.Pass))
            {
                if (this.FieldAtIndex(GradeIndex).Length <= 0)
                {
                    AddError(string.Format("'{0}' doesn't have PASS result but <Grade Code> is empty", this.Type), MessageError.ErrorType.Warn);
                }
                if (this.GradeMessage.Length <= 0)
                {
                    AddError(string.Format("'{0}' doesn't have PASS result but <Grade Message> is empty", this.Type), MessageError.ErrorType.Warn);
                }
            }
            if (this.Location.Length <= 0)
            {
                AddFormatError(string.Format("'{0}' does NOT accept empty <Location>", this.Type));
            }
            if (this.StartTime.Length <= 0)
            {
                AddError(string.Format("'{0}' has an empty <Start Time>", this.Type), MessageError.ErrorType.Warn);
            }
            if (this.EndTime.Length <= 0)
            {
                AddError(string.Format("'{0}' has an empty <End Time>", this.Type), MessageError.ErrorType.Warn);
            }
            if (this.FieldAtIndex(GradeIndex).Length > 0)
            {
                if (!isGoodInt(this.FieldAtIndex(GradeIndex)))
                {
                    AddFormatError(string.Format("'{0}' does NOT accept <Grade Code>, it has to be a integer", this.Type));
                }
            }
        }
        #endregion

        #region [ Process Message ]
        public override void Process()
        {
            base.Process();

            if (HasFormatError()) return;
            // Now we are sure that all fields valid

            UUTManager uutMgr = Tester.getInstance().UUTMgr;
            LocationManager locationMgr = Tester.getInstance().LocationMgr;
            UUT anUUT = uutMgr.UUTWithIdentifier(this.UUTID);
            Location aLocation = locationMgr.LocationWithLocator(this.Location);

            if (anUUT == null)
            {
                AddError(string.Format("'UUT {0}' is not ever Loaded", this.UUTID));
                return;
            }
            if (aLocation == null)
            {
                AddError(string.Format("<{0}> is not defined", this.Location));
                return;
            }
            if (!aLocation.HasUUT(anUUT.Identifier))
            {
                AddError(string.Format("UUT <{0}> is not at <{1}>", anUUT.Identifier, aLocation.Locator));
                return;
            }
            if (!aLocation.Function.Equals(Function.Operate))
            {
                AddError(string.Format("Location <{0}> does NOT expect result", aLocation.Locator));
            }
        }
        #endregion
    }
}
