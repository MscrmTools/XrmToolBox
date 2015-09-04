using System;
using System.Windows.Forms;

namespace MsCrmTools.FormAttributeManager.Forms
{
    public partial class UpdateLabelDialog : Form
    {
        public UpdateLabelDialog(int localeId)
        {
            InitializeComponent();

            lblDescription.Text = string.Format(lblDescription.Text, localeId);
        }

        public string Label
        {
            get { return txtNewLabel.Text; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtNewLabel.Text.Length == 0)
            {
                MessageBox.Show(this, "Please specify a new label");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}