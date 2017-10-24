using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerUI.Comm
{
    public class CommStateEventArgs
    {
        public string State { set; get; }
        public string Message { set; get; }
    }
    public delegate void CommStateChangedHandler(object sender, CommStateEventArgs args);
}
