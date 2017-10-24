using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    public class ProtocolAnalyzer
    {
        #region [ Singleton ]
        private static ProtocolAnalyzer _Instance = null;
        public static ProtocolAnalyzer Instance()
        {
            if (_Instance == null)
            {
                _Instance = new ProtocolAnalyzer();
                _Instance.Init();
            }
            return _Instance;
        }
        #endregion

        #region [ Life Circle ]
        public ProtocolAnalyzer()
        {
        }
        private void Init()
        { }
        #endregion

        #region [ Data ]
        public object CommunicationWorker;
        public object UnitManager;
        public object LocationManager;
        public object FunctionManager;
        #endregion

        #region [ Event ]
        public delegate void NewMessageHandler(object sender, object NewMessage);
        public event NewMessageHandler NewMessageEvent;
        #endregion
        }
}
