using System;
using System.Windows.Forms;

namespace MscrmTools.SyncFilterManager.Forms
{
    public partial class TemplateTypeSelection : Form
    {
        private readonly bool isSystem;

        public TemplateTypeSelection(bool disableOutlookTemplate, bool isSystem)
        {
            this.isSystem = isSystem;

            InitializeComponent();

            lblHeader.Text = string.Format(lblHeader.Text, isSystem
                ? "System Rule"
                : "Rule Template");

            Text = string.Format(Text, isSystem
                ? "System Rule"
                : "Rule Template");

            if (disableOutlookTemplate)
            {
                rdbOfflineTemplate.Checked = true;
                rdbOutlookTemplate.Enabled = false;
                label3.Enabled = false;
            }
        }

        public int TemplateType { get; private set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TemplateType = isSystem ? (rdbOfflineTemplate.Checked ? 16 : 256) : (rdbOfflineTemplate.Checked ? 8192 : 131072);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}