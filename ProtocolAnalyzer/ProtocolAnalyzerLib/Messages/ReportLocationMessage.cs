using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolTesterLib
{
    class ReportLocationMessage : Message
    {
        #region [ Constant ]
        protected const int UUTIDIndex = 2;
        protected const int LocationIndex = 3;
        #endregion

        #region [ Data ]
        public string UUTID { get { return FieldAtIndex(UUTIDIndex); } }
        public string Location { get { return FieldAtIndex(LocationIndex); } }
        #endregion

        #region [ Life Circle ]
        public ReportLocationMessage(Message.MessageInfo info, string[] fields) : base(info, fields) 
        {
            this.Type = Message.MessageType.REPORT_UNIT_LOCATION;

            if (!HasField(UUTIDIndex))
            {
                AddFormatError(string.Format("'{0}' do NOT have <UUT ID> field", this.Type));
            }
            if (!HasField(LocationIndex))
            {
                AddFormatError(string.Format("'{0}' Do not have <Location> field", this.Type));
            }

            if (HasFormatError()) return;

            string strUUTID = FieldAtIndex(UUTIDIndex);
            string strLocation = FieldAtIndex(LocationIndex);
            if (strUUTID.Length <= 0)
            {
                AddFormatError(string.Format("'{0}' does NOT accept empty <UUT ID>", this.Type));
            }
            if (strLocation.Length <= 0)
            {
                AddFormatError(string.Format("'{0}' does NOT accept empty <Location>", this.Type));
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
            FunctionManager funMgr = Tester.getInstance().FunctionMgr;
            UUT anUUT = uutMgr.UUTWithIdentifier(this.UUTID);
            Location newLocation = locationMgr.LocationWithLocator(this.Location);
            if (newLocation != null && newLocation.isFirst)
            {
                anUUT = uutMgr.AddUUT(this.UUTID);
                anUUT.isLoaded = false;
                anUUT.isUnloaded = false;
                anUUT.Result = MessageResult.Unknown;
            }
            if (anUUT == null)
            {
                AddError(string.Format("UUT '{0}' is not ever Loaded", this.UUTID));
                return;
            }
            Location oldLocation = locationMgr.LocationWithLocator(anUUT.CurrentLocation);

            // 1st. Execute function of old fucntion
            // 2nd. Remove UUT from old function
            if (oldLocation != null)
            {
                funMgr.WillLeaveLocation(oldLocation.Function, anUUT, oldLocation, this);
                oldLocation.RemoveUUT(anUUT.Identifier);
                funMgr.DidLeaveLocation(oldLocation.Function, anUUT, oldLocation, this);
            }
            // 3rd. Check the new location
            if (newLocation == null)
            {
                AddError(string.Format("'{0}' is not a valid location", this.Location));
            }
            // 4th. Compare old and new locations
            if (oldLocation == null)
            {
                if (!newLocation.isFirst)
                {
                    AddError(string.Format("'{0}' is not the 1st location", this.Location));
                }
            }
            else
            {
                if (!oldLocation.isNextLocation(newLocation))
                {
                    AddError(string.Format("'{0}' is not after {1}", newLocation.Locator, oldLocation.Locator));
                }
            }
            // 5th. Add UUT to new location
            // 6th. Execute function
            funMgr.WillEnterLocation(newLocation.Function, anUUT, newLocation, this);
            newLocation.AddUUT(anUUT.Identifier);
            anUUT.CurrentLocation = newLocation.Locator;
            funMgr.DidEnterLocation(newLocation.Function, anUUT, newLocation, this);
            // 7th. Remove from last location
            if (newLocation.isLast)
            {
                funMgr.WillLeaveLocation(newLocation.Function, anUUT, newLocation, this);
                anUUT.CurrentLocation = string.Empty;
                newLocation.RemoveUUT(anUUT.Identifier);
                funMgr.DidLeaveLocation(newLocation.Function, anUUT, newLocation, this);
                uutMgr.RemoveUUT(anUUT.Identifier);
            }
        }
        #endregion
    }
}
