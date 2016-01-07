using MsCrmTools.WebResourcesManager.AppCode;
using System;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.Forms
{
    public partial class CompareSettingsDialog : Form
    {
        private readonly bool _isConfigured;

        public CompareSettingsDialog(bool isConfigured)
        {
            _isConfigured = isConfigured;

            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "exe files (*.exe)|*.exe|All files (*.*)|*.*";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;
                dialog.Multiselect = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtCommandPath.Text = dialog.FileName;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var commandPath = txtCommandPath.Text.Trim().TrimStart('"').TrimEnd('"');
            var commandArgs = txtCommandArgs.Text.Trim().TrimStart('"').TrimEnd('"');

            Options.Instance.CompareToolPath = commandPath;
            Options.Instance.CompareToolArgs = commandArgs;
            Options.Instance.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CompareSettingsDialog_Load(object sender, EventArgs e)
        {
            OpenInstructions.Enabled = true;

            txtCommandPath.Text = Options.Instance.CompareToolPath;
            txtCommandArgs.Text = Options.Instance.CompareToolArgs;
        }

        private void OpenInstructions_Tick(object sender, EventArgs e)
        {
            OpenInstructions.Enabled = false;

            if (!_isConfigured)
                MessageBox.Show(string.Format("In order to do a compare, you must first setup you compare tool.{0}{0}In the command field, enter the path to the compare tool executable file.{0}{0}In the arguments field, enter any command line arguments used by the compare tool executable.", Environment.NewLine), "Compare Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}