using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib
{
    abstract class Function
    {
        #region [ Function Definitions]
        public const string Load = "Load";
        public const string Operate = "Operation";
        public const string Unload = "Unload";
        #endregion

        #region [ Exceptions ]
        public class FunctionException : Exception
        {
            public FunctionException(string str) : base(string.Format("Failed to init Function: {0}", str)) { }
        }
        #endregion

        #region [ Data ]
        protected string _Name;
    
        public string Name { get { return _Name; }  protected set { _Name = value != null ? value : string.Empty; } }
        #endregion

        #region [Life Circle]
        public Function(string strName)
        {
            this.Name = string.Copy(strName);
        }
        #endregion

        #region [ Execution ]
        public virtual void UUTWillEnterLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
        }
        public virtual void UUTDidEnterLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
        }
        public virtual void UUTWillLeaveLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
        }
        public virtual void UUTDidLeaveLocation(UUT anUUT, Location aLocation, Message aMessage)
        {
        }
        #endregion
    }
}
