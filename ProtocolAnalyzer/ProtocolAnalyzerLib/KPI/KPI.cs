using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    class KPI
    {
        public readonly string Name;
        public readonly string Unit;
        public readonly string Max;
        public readonly string Min;

        public KPI(string name, string unit, string max, string min)
        {
            this.Name = name;
            this.Unit = unit;
            this.Max = max;
            this.Min = min;
        }
    }
}
