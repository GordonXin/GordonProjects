using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sapphire.Xml;

namespace ProtocolAnalyzerLib
{
    class KPIManager
    {
        #region [ Data ]
        List<KPI> _KPIList;
        #endregion

        #region [ Life Circle ]
        public KPIManager()
        {
            _KPIList = new List<KPI>();
        }
        #endregion

        #region [ Interface ]
        public void Reset()
        {
            _KPIList.Clear();
        }
        public void AddKPI(string configString)
        {
            SimpleXmlParser sp = new SimpleXmlParser();
            sp.OpenInfo(configString, false);

            KPI kpi = new KPI(sp.GetData("/SysVar/Name"), sp.GetData("/SysVar/Unit"), sp.GetData("/SysVar/Max"), sp.GetData("/SysVar/Min"));
            _KPIList.Add(kpi);
        }
        #endregion
    }
}
