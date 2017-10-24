﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProtocolAnalyzerLib
{
    class Slot
    {
        #region [ Exceptions ]
        public class SlotException : Exception
        {
            public SlotException(string strMessage) : base(strMessage) { }
        }
        #endregion

        #region [ Data ]
        protected string _Locator;
        protected List<Location> _LocationList;
        public string Locator { get { return _Locator; } protected set { _Locator = value != null ? value : string.Empty; } }
        public Location[] Locations { get { return _LocationList.ToArray(); } }
        #endregion

        #region [ Life Circle ]
        public Slot(string strLocator)
        {
            this.Locator = string.Copy(strLocator);
            _LocationList = new List<Location>();
        }
        #endregion

        #region [ Location ]
        public void AddLocation(Location pLocation)
        {
            if (pLocation != null)
            {
                _LocationList.Add(pLocation);
                Debug.Print(string.Format("Slot {0} added a new Location {1}", _Locator, pLocation.Locator));
            }
        }
        public Location LocationWithLocator(string strLocationLocator)
        {
            if (!string.IsNullOrWhiteSpace(strLocationLocator))
            {
                foreach(Location aLocation in _LocationList)
                {
                    if (aLocation.Locator.Equals(strLocationLocator))
                    {
                        return aLocation;
                    }
                }
            }
            return null;
        }
        virtual public void SetupLocations()
        {
            for (int i = 0; i < _LocationList.Count; ++i)
            {
                _LocationList[i].Index = i;
                _LocationList[i].NextLocator = (i + 1) < _LocationList.Count ? _LocationList[i + 1].Locator : string.Empty;
                _LocationList[i].isFirst = (i == 0);
                _LocationList[i].isLast = (i + 1) >= _LocationList.Count;
            }
        }
        virtual public void CheckErrorInLocations()
        {
            if (_LocationList.Count <= 0)
            {
                throw new SlotException(string.Format("Slot '{0}' doesn't have any Location", this.Locator));
            }

            int nLoadIndex = IndexOfLocationWithFunction("Load");
            int nUnloadIndex = IndexOfLocationWithFunction("Unload");
            int nOperationIndex = IndexOfLocationWithFunction("Operation");

            if (nLoadIndex < 0)
            {
                throw new SlotException(string.Format("Can't find Location for <Load>"));
            }
            if (nUnloadIndex < 0)
            {
                throw new SlotException(string.Format("Can't find Location for <Unload>"));
            }
            if (nOperationIndex < 0)
            {
                throw new SlotException(string.Format("Can't find Locaion for <Operation>"));
            }
            if (nLoadIndex >= nUnloadIndex)
            {
                throw new SlotException(string.Format("Location for <Load> is not prior to Location for <Unload>"));
            }
            if (nLoadIndex >= nOperationIndex)
            {
                throw new SlotException(string.Format("Location for <Load> is not prior to Location for <Operation>"));
            }
            if (nOperationIndex >= nUnloadIndex)
            {
                throw new SlotException(string.Format("Location for <Operation> is not prior to Location for <Unload>"));
            }
        }
        private int IndexOfLocationWithFunction(string strFunction)
        {
            if (strFunction != null && strFunction.Length > 0)
            {
                for (int i = 0; i < _LocationList.Count; ++i)
                {
                    if (_LocationList[i].Function.Equals(strFunction))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public Location FirstLocation { get { return _LocationList.Count > 0 ? _LocationList[0] : null; } }
        public Location LastLocation { get { return _LocationList.Count > 0 ? _LocationList[_LocationList.Count - 1] : null; } }
        #endregion

        #region [ Compare ]
        virtual public bool isSameSlot(Slot pOther)
        {
            return (pOther != null && this.Locator.Equals(pOther.Locator));
        }
        virtual public bool hasLocation(Location pLocation)
        {
            if (pLocation == null)
            {
                return false;
            }
            if (!pLocation.SlotLocator.Equals(this.Locator))
            {
                return false;
            }
            return (LocationWithLocator(pLocation.Locator) != null);
        }
        #endregion
    }
}
