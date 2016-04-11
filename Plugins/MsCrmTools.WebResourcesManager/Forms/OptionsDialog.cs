using MsCrmTools.WebResourcesManager.AppCode;
using System;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.Forms
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            Options.Instance.SaveOnDisk = chkSaveOnDisk.Checked;
            Options.Instance.PushTsMapFiles = chkPushMapAndTsFiles.Checked;
            Options.Instance.AfterUpdateCommand = txtUpdateEvent.Text;
            Options.Instance.AfterPublishCommand = txtPublishEvent.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            chkSaveOnDisk.Checked = Options.Instance.SaveOnDisk;
            chkPushMapAndTsFiles.Checked = Options.Instance.PushTsMapFiles;
            txtUpdateEvent.Text = Options.Instance.AfterUpdateCommand;
            txtPublishEvent.Text = Options.Instance.AfterPublishCommand;
        }
    }
}