using System;
using System.Windows.Forms;

namespace MscrmTools.SyncFilterManager.Forms
{
    public partial class ViewPropertiesDialog : Form
    {
        public ViewPropertiesDialog()
        {
            InitializeComponent();
        }

        public string ViewDescription { get; set; }
        public string ViewName { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            if (txtNewName.Text.Length > 0)
            {
                ViewName = txtNewName.Text;
                ViewDescription = txtDescription.Text;

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Please provide a valid name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtFolderName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnValidate_Click(null, null);
            }
        }

        private void ViewPropertiesDialog_Load(object sender, EventArgs e)
        {
            txtNewName.Text = ViewName;
            txtDescription.Text = ViewDescription;
        }
    }
}