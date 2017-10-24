using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace ProtocolAnalyzerUI.Controls
{
    public partial class ControlIPTextBox : TextBox
    {
        #region [ Life Circle ]
        public ControlIPTextBox()
        {
            InitializeComponent();

            this.ChangeColorToUnknown();

            this.PingCommand = new System.Net.NetworkInformation.Ping();
            this.PingCommand.PingCompleted += new System.Net.NetworkInformation.PingCompletedEventHandler(PingCompletedEventHandler);

            this.MyTimer = new Timer();
            this.MyTimer.Interval = 5000;
            this.MyTimer.Tick += new EventHandler(MyTimer_TickHandler);
        }
        #endregion

        #region [ Data ]
        private string OriginalText { get; set; }
        private Color OriginalColor { get; set; }
        private Ping PingCommand { get; set; }
        private Timer MyTimer { get; set; }
        public bool EnablePing
        {
            get { return this.MyTimer.Enabled; }
            set
            {
                if (value)
                    this.MyTimer.Start();
                else
                    this.MyTimer.Stop();
            }
        }
        #endregion

        #region [ Color control ]
        private delegate void ChangeColorHandler();
        private void ChangeColorToSuccess()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ChangeColorHandler(ChangeColorToSuccess));
                return;
            }
            this.BackColor = Color.LightSeaGreen;
        }
        private void ChangeColorToFail()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ChangeColorHandler(ChangeColorToFail));
                return;
            }
            this.BackColor = Color.PaleVioletRed;
        }
        private void ChangeColorToUnknown()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ChangeColorHandler(ChangeColorToUnknown));
                return;
            }
            this.BackColor = Color.LemonChiffon;
        }
        #endregion

        #region [ Test IP Address ]
        protected override void OnGotFocus(EventArgs e)
        {
            if (this.EnablePing)
            {
                this.OriginalText = this.Text;
                this.OriginalColor = this.BackColor;
                this.MyTimer.Stop();
                CancelPing();
            }

            base.OnGotFocus(e);
        }
        protected override void OnLostFocus(EventArgs e)
        {
            if (this.Text != this.OriginalText)
            {
                this.MyTimer.Stop();
                CancelPing();
                StartPing();
            }
            else
            {
                this.BackColor = this.OriginalColor;
            }
            base.OnLostFocus(e);
        }
        private void CancelPing()
        {
            try
            {
                this.PingCommand.SendAsyncCancel();
            }
            catch
            {
            }
            finally
            {
                this.ChangeColorToUnknown();
            }
        }
        private void StartPing()
        {
            try
            {
                this.PingCommand.SendAsync(this.Text, this);
            }
            catch
            {
                this.ChangeColorToFail();
            }
        }
        private void PingCompletedEventHandler(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                this.ChangeColorToSuccess();
            }
            else
            {
                this.ChangeColorToFail();
            }
            if (this.EnablePing)
            {
                this.MyTimer.Start();
            }
        }
        private void MyTimer_TickHandler(object sender, EventArgs e)
        {
            StartPing();
        }
        #endregion
    }
}
