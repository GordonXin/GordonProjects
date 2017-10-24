using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProtocolTesterLib
{
    class LocationManager
    {
        #region [ Data ]
        protected Dictionary<string, Slot> _SlotMap;

        public Slot[] SlotList { get { return _SlotMap.Values.ToArray<Slot>(); } }
        #endregion

        #region [ Life Circle ]
        public LocationManager()
        {
            _SlotMap = new Dictionary<string, Slot>();
        }

        private void init()
        {
        }

        #endregion

        #region [ Locations & Slots ]
        public void AddLocation(string strLocator, string strFunction)
        {
            if (strLocator != null && strLocator.Length > 0)
            {
                Location pLocation = new Location(strLocator, strFunction);
                Slot pSlot = SlotWithLocator(pLocation.SlotLocator);
                if (pSlot == null)
                {
                    pSlot = new Slot(pLocation.SlotLocator);
                    _SlotMap.Add(pLocation.SlotLocator, pSlot);
                    Debug.Print(string.Format("Add slot {0}", pSlot.Locator));
                }
                if(pSlot.LocationWithLocator(pLocation.Locator) == null)
                {
                    pSlot.AddLocation(pLocation);
                }
            }
        }

        public Slot SlotWithLocator(string strSlotLocator)
        {
            Slot ret = null;
            if (_SlotMap.ContainsKey(strSlotLocator))
            {
                ret = _SlotMap[strSlotLocator];
            }
            return ret;
        }

        public Location LocationWithLocator(string strLocationLocator)
        {
            Location ret = null;
            foreach (Slot aSlot in _SlotMap.Values)
            {
                ret = aSlot.LocationWithLocator(strLocationLocator);
                if (ret != null)
                {
                    break;
                }
            }
            return ret;
        }

        public void CheckLocations()
        {
            foreach (Slot aSlot in _SlotMap.Values)
            {
                aSlot.SetupLocations();
                aSlot.CheckErrorInLocations();
            }
        }
        #endregion
    }
}
