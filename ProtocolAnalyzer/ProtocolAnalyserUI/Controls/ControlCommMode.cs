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
    public partial class ControlCommMode : UserControl
    {
        #region [ Event ]
        public delegate void CommModeChanged(object sender, Comm.CommConfig.CommMode NewMode);
        public event CommModeChanged ModeChanged;
        #endregion

        #region [ Life Circle ]
        public ControlCommMode()
        {
            InitializeComponent();

            this.rbnClient.Checked = true;
            this.rbnServer.Checked = false;

            this.rbnClient.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
            this.rbnServer.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
        }
        #endregion

        #region [ Data ]
        public Comm.CommConfig.CommMode Mode
        {
            get { return (this.rbnClient.Checked) ? Comm.CommConfig.CommMode.Client : Comm.CommConfig.CommMode.Sever; }
        }

        public void SetNewMode(Comm.CommConfig.CommMode mode)
        {
            this.rbnClient.Checked = (mode == Comm.CommConfig.CommMode.Client);
            this.rbnServer.Checked = (mode == Comm.CommConfig.CommMode.Sever);
        }
        public void SetNewModeWithoutEvent(Comm.CommConfig.CommMode mode)
        {
            this.rbnClient.CheckedChanged -= new EventHandler(RadioButton_CheckedChanged);
            this.rbnServer.CheckedChanged -= new EventHandler(RadioButton_CheckedChanged);

            this.SetNewMode(mode);

            this.rbnClient.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
            this.rbnServer.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
        }
        #endregion

        #region [ Internal Handling ]

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.FireCommModeChanged();
        }

        private void FireCommModeChanged()
        {
            if (this.ModeChanged != null)
            {
                this.ModeChanged(this, this.Mode);
            }
        }
        #endregion
    }
}
