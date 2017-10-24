using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProtocolAnalyzerUI
{
    public partial class frmMain : Form
    {
        #region [ Life Circle ]
        public frmMain()
        {
            InitializeComponent();

            InitUI();
        }
        private void InitUI()
        {
            string configPath = Path.Combine(Application.StartupPath, "ConfigFiles");
            if (Directory.Exists(configPath) == false)
            {
                try
                {
                    Directory.CreateDirectory(configPath);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Can't create directory for config files because " + e.Message, "Error", MessageBoxButtons.OK);
                    return;
                }
            }
            string configFile = Properties.Settings.Default.DefinitionFileName;
            if (string.IsNullOrWhiteSpace(configFile))
            {
                configFile = "PE_DEF.xml";
                Properties.Settings.Default.DefinitionFileName = configFile;
            }
            this.filePathControl.BrowseFolder = configPath;
            this.filePathControl.FilePath = configFile;
            this.filePathControl.FilePathChanged += new ProtocolAnalyzerUI.Controls.ControlFilePath.FilePathChangedHanlder(FilePathChangedHandler);
        }
        private void InitLib()
        {
            ProtocolAnalyzerLib.ProtocolAnalyzer.Instance();
        }
        #endregion

        #region [ Event handler ]

        private void FilePathChangedHandler(object sender, string NewPath)
        {
            try
            {
                Properties.Settings.Default.DefinitionFileName = Path.GetFileName(NewPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to set user default 'DefinitionFileName' because " + ex.Message, "Alarm", MessageBoxButtons.OK);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Agent != null)
            {
                _Agent.Stop();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_Agent == null)
            {
                _Agent = new AnalyzerAgent();
                _Agent.AgentEvent += new ProtocolAnalyzerUI.AgentEventHandler(this.AgentEventHandler);

                this.button1.Text = "Stop";
                this.commControl.Enabled = false;
                this.filePathControl.Enabled = false;
                this.tbxMessages.Clear();

                _Agent.StartWithConfig(this.commControl.Config, this.filePathControl.FullPath);
            }
            else
            {
                _Agent.Stop();
                _Agent = null;
                this.button1.Text = "Start";
                this.commControl.Enabled = true;
                this.filePathControl.Enabled = true;
            }
        }
        private void AgentEventHandler(object sender, AgentEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new AgentEventHandler(this.AgentEventHandler), sender, args);
                return;
            }
            this.tbxMessages.Select(this.tbxMessages.TextLength, 0);
            if (args.State.Equals("Exception"))
            {
                this.tbxMessages.ForeColor = Color.Red;
                this.tbxMessages.AppendText(string.Format("{0}\r\n", args.Message));
            }
            else if (args.State.Equals("Info"))
            {
                this.tbxMessages.ForeColor = Color.Black;
                this.tbxMessages.AppendText(string.Format("{0}\r\n", args.Message));
            }
            else if (args.State.Equals("Alarm"))
            {
                this.tbxMessages.ForeColor = Color.Red;
                this.tbxMessages.AppendText(string.Format("{0}\r\n", args.Message));
            }
            else if (args.State.Equals("Warning"))
            {
                this.tbxMessages.ForeColor = Color.Yellow;
                this.tbxMessages.AppendText(string.Format("{0}\r\n", args.Message));
            }
        }
        #endregion

        #region [ Property ]
        public string ConfigFilePath
        {
            get { return this.filePathControl.FullPath; }
        }
        private AnalyzerAgent _Agent = null;
        #endregion
    }
}
