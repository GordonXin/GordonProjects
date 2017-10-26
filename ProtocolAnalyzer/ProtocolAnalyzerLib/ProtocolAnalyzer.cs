using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sapphire.Xml;
using System.Diagnostics;
using System.Threading;

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
            }
            return _Instance;
        }
        #endregion

        #region [ Life Circle ]
        public ProtocolAnalyzer()
        {
            UUTMgr = new UUTManager();
            LocationMgr = new LocationManager();
            FunctionMgr = new FunctionManager();
            KPIMgr = new KPIManager();
        }
        #endregion

        #region [ Data ]
        internal UUTManager UUTMgr;
        internal LocationManager LocationMgr;
        internal FunctionManager FunctionMgr;
        internal KPIManager KPIMgr;
        #endregion

        #region [ Interface ]
        public void Reset()
        {
            UUTMgr.Reset();
            LocationMgr.Reset();
            FunctionMgr.Reset();
            KPIMgr.Reset();
        }
        public string[] InitWithConfigFilePath(string configFilePath)
        {
            Reset();

            List<string> errors = new List<string>();
            SimpleXmlParser sp = new SimpleXmlParser();
            sp.OpenInfo(configFilePath, true);

            // Load Locations
            ArrayList locationList = sp.GetDataList("/PE/Structure/ModuleList/Module['DashBoard']/LocationList/Location", SimpleXmlParser.XmlData.OuterMetaData);
            foreach (string aConfig in locationList)
            {
                try
                {
                    LocationMgr.AddLocation(aConfig);
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }
            try
            {
                LocationMgr.CheckLocations();
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }

            ArrayList kpiList = sp.GetDataList("/PE/Structure/ModuleList/Module['DashBoard']/SysVars/SysVar", SimpleXmlParser.XmlData.OuterMetaData);
            foreach (string aConfig in kpiList)
            {
                KPIMgr.AddKPI(aConfig);
            }
            
            return errors.ToArray();
        }
        public Message HandleMessage(string msg, DateTime time, Message.CommMode mode)
        {
            return Message.CreateMessage(msg, mode, time);
        }
        #endregion

        public void TestRun()
        {
            //PrintDebug(HandleMessage("", DateTime.Now, Message.CommMode.Send));
            //PrintDebug(HandleMessage("0", DateTime.Now, Message.CommMode.Send));
            //PrintDebug(HandleMessage("0,", DateTime.Now, Message.CommMode.Send));

            //PrintDebug(HandleMessage("0,REPORT_UNIT_LOCATION", DateTime.Now, Message.CommMode.Send));
            //PrintDebug(HandleMessage("0,REPORT_UNIT_LOCATION,ABC", DateTime.Now, Message.CommMode.Send));
            //PrintDebug(HandleMessage("0,REPORT_UNIT_LOCATION,ABC,RIGHT", DateTime.Now, Message.CommMode.Send));
            //PrintDebug(HandleMessage("0,REPORT_UNIT_LOCATION,,RIGHT", DateTime.Now, Message.CommMode.Send));

            //PrintDebug(HandleMessage("0,REPORT_RESULT", DateTime.Now, Message.CommMode.Send));
            //PrintDebug(HandleMessage("0,REPORT_RESULT,abc", DateTime.Now, Message.CommMode.Send));
            //PrintDebug(HandleMessage("0,REPORT_RESULT,abc,pass", DateTime.Now, Message.CommMode.Send));
        }
        private void PrintDebug(Message aMessage)
        {
            Debug.Print(aMessage.MessageString);
            foreach (MessageInfo anInfo in aMessage.AllInfo)
            {
                Debug.Print(anInfo.Message);
            }
            Debug.Print("");
            Thread.Sleep(100);
        }
    }
}
