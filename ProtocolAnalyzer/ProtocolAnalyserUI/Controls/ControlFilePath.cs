using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProtocolAnalyzerUI.Controls
{
    public partial class ControlFilePath : UserControl
    {
        public delegate void FilePathChangedHanlder(object sender, string NewPath);
        public event FilePathChangedHanlder FilePathChanged;
        public ControlFilePath()
        {
            InitializeComponent();
        }

        private static OpenFileDialog OpenDialog = new OpenFileDialog();

        [Browsable(true)]
        public string Title { get { return this.lblTitle.Text; } set { this.lblTitle.Text = value; } }

        private string _BrowseFolder;
        public string BrowseFolder { get { return _BrowseFolder; } set { _BrowseFolder = value; } }
        public string FilePath
        {
            get { return tbxFilePath.Text; }
            set
            {
                this.tbxFilePath.Text = value;

                if (this.FilePathChanged != null)
                {
                    this.FilePathChanged(this, this.tbxFilePath.Text);
                }
            }
        }
        public string FullPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.BrowseFolder) == false)
                {
                    return Path.Combine(this.BrowseFolder, this.FilePath);
                }
                return this.FilePath;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenDialog.Title = "Select " + this.Title;
            OpenDialog.Filter = ".xml|.XML";
            OpenDialog.CheckFileExists = true;
            OpenDialog.Multiselect = false;
            if (string.IsNullOrWhiteSpace(this.BrowseFolder) == false)
            {
                OpenDialog.InitialDirectory = this.BrowseFolder;
            }

            DialogResult result = OpenDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = OpenDialog.FileName;
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    MessageBox.Show("Empty File Path!", "Error", MessageBoxButtons.OK);
                    return;
                }
                if (File.Exists(fileName) == false)
                {
                    MessageBox.Show("Selected file path doesn't exist!", "Error", MessageBoxButtons.OK);
                    return;
                }
                string dir = Path.GetDirectoryName(fileName);
                if (string.IsNullOrWhiteSpace(this.BrowseFolder) == false && dir != Path.GetDirectoryName(this.BrowseFolder))
                {
                    MessageBox.Show("Will copy selected file to Config Folder !", "Info", MessageBoxButtons.OK);

                    string pureFileName = Path.GetFileName(fileName);
                    string targetFilePath = Path.Combine(this.BrowseFolder, pureFileName);
                    if (File.Exists(targetFilePath))
                    {
                        if (DialogResult.OK != MessageBox.Show("The file already exists! Are you sure to replace?", "Confirm", MessageBoxButtons.OKCancel))
                        {
                            return;
                        }
                        try
                        { 
                            File.Delete(targetFilePath);
                            File.Move(fileName, targetFilePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to replace file because " + ex.Message, "Alarm", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(this.BrowseFolder) == false)
                {
                    this.FilePath = Path.GetFileName(fileName);
                }
                else
                {
                    this.FilePath = fileName;
                }
            }
        }
        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            if (sender == this.tbxFilePath)
            {
                if (this.FilePathChanged != null)
                {
                    this.FilePathChanged(this, this.tbxFilePath.Text);
                }
            }
        }
    }
}
