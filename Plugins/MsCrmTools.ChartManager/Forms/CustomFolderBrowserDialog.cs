using System;
using System.IO;
using System.Windows.Forms;

namespace MsCrmTools.ChartManager.Forms
{
    public partial class CustomFolderBrowserDialog : Form
    {
        public CustomFolderBrowserDialog()
        {
            InitializeComponent();
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
                MessageBox.Show(this, "Invalid folder specified!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
