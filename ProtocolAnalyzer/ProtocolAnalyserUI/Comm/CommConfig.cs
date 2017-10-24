using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ProtocolAnalyzerUI.Comm
{
    public class CommConfig
    {
        public enum CommMode : short
        {
            Sever,
            Client,
        }

        #region [ Constant ]
        private int DefaultPort = 4096;
        private string DefaultIP = "127.0.0.1";
        private CommMode DefaultMode = CommMode.Sever;
        #endregion

        #region [ Life Circle ]
        public static CommConfig DefaultConfig()
        {
            return new CommConfig();
        }

        public CommConfig()
        {
            this.Mode = DefaultMode;
            this.IPAddress = DefaultIP;
            this.PortString = DefaultPort.ToString();
        }

        public CommConfig(CommMode mode, string ip)
        {
            this.Mode = mode;
            this.IPAddress = ip;
            this.PortString = DefaultPort.ToString();
        }

        public CommConfig(CommMode mode, string ip, string port)
        {
            this.Mode = mode;
            this.IPAddress = ip;
            this.PortString = port;
        }

        #endregion

        #region [ Data ]
        public CommMode Mode { get; set; }
        public string IPAddress { get; set; }
        public string PortString { get; set; }
        public int Port { get { return int.Parse(this.PortString); } }
        #endregion

        #region [ Compare ]
        public static bool IsSame(CommConfig c1, CommConfig c2)
        {
            return c1.IsSame(c2);
        }
        public bool IsSame(CommConfig other)
        {
            if (!this.IPAddress.Equals(other.IPAddress))
                return false;
            if (!this.PortString.Equals(other.PortString))
                return false;
            if (this.Mode != other.Mode)
                return false;

            return true;
        }
        #endregion

        #region [ Validation ]
        public bool IsGoodConfig(ref string ErrorMessage)
        {
            bool isGoodMode = true;
            try
            {
                isGoodMode = Enum.IsDefined(typeof(CommConfig.CommMode), this.Mode);
            }
            catch
            {
                isGoodMode = false;
            }
            if (!isGoodMode)
            {
                if (ErrorMessage != null)
                {
                    ErrorMessage = "User shall select correct mode, Client or Server";
                }
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.IPAddress))
            {
                if (ErrorMessage != null)
                {
                    ErrorMessage = "IP Address should not be empty";
                }
                return false;
            }
            bool isGoodIP = true;
            try
            {
                IPAddress ipAddr = System.Net.IPAddress.Parse(this.IPAddress);
            }
            catch
            {
                isGoodIP = false;
            }
            if (!isGoodIP)
            {
                if (ErrorMessage != null)
                {
                    ErrorMessage = "IP Address is not correct";
                }
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.PortString))
            {
                if (ErrorMessage != null)
                {
                    ErrorMessage = "Port should not be empty";
                }
                return false;
            }
            int port = 0;
            if (!int.TryParse(this.PortString, out port))
            {
                if (ErrorMessage != null)
                {
                    ErrorMessage = "Port is not an integer";
                }
                return false;
            }
            return true;
        }
        #endregion
    }
}
