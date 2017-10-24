using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolAnalyzerLib.Messages
{
    class MessageResult
    {
        public const string Pass = "PASS";
        public const string Fail = "FAIL";
        public const string Unknown = "UNKNOWN";

        public static bool isSupportedResult(string str)
        {
            if (str == null || str.Length <= 0)
            {
                return false;
            }
            if (str.Equals(Pass) || str.Equals(Fail))
            {
                return true;
            }
            return false;
        }
    }
}