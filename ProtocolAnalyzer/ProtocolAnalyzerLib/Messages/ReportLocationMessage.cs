using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class ReportLocationMessage : Message
    {
        #region [ Fields ]
        protected const int UUTIDIndex = 2;
        protected const int LocationIndex = 3;
        private void InitFieldConfig()
        {
            _FieldConfigs.Add(new MessageFieldConfig(UUTIDIndex, "Unit ID", typeof(string)));
            _FieldConfigs.Add(new MessageFieldConfig(LocationIndex, "Location", typeof(string)));
        }
        #endregion

        #region [ Data ]
        public string UUTID { get { return FieldAtIndex(UUTIDIndex); } }
        public string Location { get { return FieldAtIndex(LocationIndex); } }
        #endregion

        #region [ Life Circle ]
        public ReportLocationMessage(MessageArgument arg, string[] fields, MessageType type) : base(arg, fields, type)
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
            FunctionManager funMgr = ProtocolAnalyzer.Instance().FunctionMgr;
            bool isGoodData = true;

            UUT anUUT = uutMgr.UUTWithIdentifier(this.UUTID);
            Location newLocation = locationMgr.LocationWithLocator(this.Location);
            if (newLocation == null)
            {
                AddAlarm(string.Format("Location '{0}' is not defined", this.Location));
                return isGoodData;
            }
            if (newLocation.isFirst)
            {
                // UUT is coming to first location
                if (anUUT != null)
                {
                    if (anUUT.CurrentLocation.Length > 0)
                    {
                        Location old = locationMgr.LocationWithLocator(this.Location);
                        if (old != null)
                        {
                            old.RemoveUUT(anUUT.Identifier);
                        }
                    }
                    if (anUUT.isUnloaded == false)
                    {
                        AddAlarm(string.Format("UUT '{0}' is not ever unloaded, but it comes to first Location '{0}' now", anUUT.Identifier, newLocation.Locator));
                    }
                }
                // now reset this UUT
                anUUT = uutMgr.AddUUT(this.UUTID);
                anUUT.isLoaded = false;
                anUUT.isUnloaded = false;
                anUUT.Result = (UUTResult)(-1);

                funMgr.WillEnterLocation(newLocation.Function, anUUT, newLocation, this);
                newLocation.AddUUT(anUUT.Identifier);
                anUUT.CurrentLocation = newLocation.Locator;
                funMgr.DidEnterLocation(newLocation.Function, anUUT, newLocation, this);
            }
            else
            {
                if (anUUT == null)
                {
                    AddAlarm(string.Format("UUT '{0}' is not ever loaded", this.UUTID));

                    // now add this UUT for further handling
                    anUUT = uutMgr.AddUUT(this.UUTID);
                    anUUT.isLoaded = true;
                    anUUT.isUnloaded = false;
                    anUUT.Result = (UUTResult)(-1);
                }
                Location oldLocation = locationMgr.LocationWithLocator(anUUT.CurrentLocation);                
                if (oldLocation != null)
                {
                    if (!oldLocation.isNextLocation(newLocation))
                    {
                        AddAlarm(string.Format("Location '{0}' is not after Location {1}", newLocation.Locator, oldLocation.Locator));
                    }

                    funMgr.WillLeaveLocation(oldLocation.Function, anUUT, oldLocation, this);
                    oldLocation.RemoveUUT(anUUT.Identifier);
                    funMgr.DidLeaveLocation(oldLocation.Function, anUUT, oldLocation, this);
                }
                funMgr.WillEnterLocation(newLocation.Function, anUUT, newLocation, this);
                newLocation.AddUUT(anUUT.Identifier);
                anUUT.CurrentLocation = newLocation.Locator;
                funMgr.DidEnterLocation(newLocation.Function, anUUT, newLocation, this);

                if (newLocation.isLast)
                {
                    funMgr.WillLeaveLocation(newLocation.Function, anUUT, newLocation, this);
                    anUUT.CurrentLocation = string.Empty;
                    newLocation.RemoveUUT(anUUT.Identifier);
                    funMgr.DidLeaveLocation(newLocation.Function, anUUT, newLocation, this);
                    uutMgr.RemoveUUT(anUUT.Identifier);
                }
            }
            return true;
        }
        #endregion
    }
}
