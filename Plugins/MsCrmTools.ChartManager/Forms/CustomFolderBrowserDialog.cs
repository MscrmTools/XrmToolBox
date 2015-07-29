using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Windows.Forms;

namespace MsCrmTools.ChartManager.Forms
{
    public partial class CustomFolderBrowserDialog : Form
    {
        private readonly bool isForLoad;

        public CustomFolderBrowserDialog(bool isForLoad)
        {
            InitializeComponent();

            this.isForLoad = isForLoad;
        }

        public string FolderPath { get; set; }

        private void CustomFolderBrowserDialog_Load(object sender, EventArgs e)
        {
            txtFolderPath.Text = FolderPath;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtFolderPath.Text))
            {
                if (!isForLoad && MessageBox.Show(this, "This folder does not exist. Would you like to create it?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(txtFolderPath.Text);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(this, error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Folder does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            FolderPath = txtFolderPath.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog
            {
                Description = "Select the folder where the files are located",
                ShowNewFolderButton = true
            };

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                txtFolderPath.Text = fbd.SelectedPath;
            }
        }
    }
}
