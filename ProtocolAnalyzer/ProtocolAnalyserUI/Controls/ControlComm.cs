using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProtocolAnalyzerUI.Controls
{
    public partial class ControlComm : UserControl
    {
        #region [ Life Circle ]
        public ControlComm()
        {
            InitializeComponent();

            Comm.CommConfig defaultConfig = new Comm.CommConfig();
            this.tbxIP.Text = defaultConfig.IPAddress;
            this.tbxPort.Text = defaultConfig.PortString;
            this.tbxPort.Enabled = NeedPortInput(defaultConfig.Mode);
            this.controlCommMode1.SetNewModeWithoutEvent(defaultConfig.Mode);

            this.controlCommMode1.ModeChanged += new ControlCommMode.CommModeChanged(CommModeChangedHandler);
        }
        #endregion

        #region [ Data ]
        public Comm.CommConfig Config
        {
            get
            {
                Comm.CommConfig NewConfig = new Comm.CommConfig(this.controlCommMode1.Mode, this.tbxIP.Text, this.tbxPort.Text);
                return NewConfig;
            }
        }
        public bool EnablePing { get { return this.tbxIP.EnablePing; } set { this.tbxIP.EnablePing = value; } }
        #endregion

        #region [ Inter Handling ]
        private void CommModeChangedHandler(object sender, Comm.CommConfig.CommMode NewMode)
        {
            this.tbxPort.Enabled = NeedPortInput(NewMode);
        }
        private bool NeedPortInput(Comm.CommConfig.CommMode mode)
        {
            return (mode == Comm.CommConfig.CommMode.Client);
        }
        #endregion
    }
}
