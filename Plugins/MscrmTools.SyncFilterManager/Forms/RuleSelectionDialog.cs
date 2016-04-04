using Microsoft.Xrm.Sdk;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MscrmTools.SyncFilterManager.Forms
{
    public partial class RuleSelectionDialog : Form
    {
        private bool isSystem;
        private string returnedTypeExpected;

        public RuleSelectionDialog(IOrganizationService service, string returnedTypeExpected = null, bool isSystem = false)
        {
            InitializeComponent();
            Service = service;
            crmSystemViewList1.Service = Service;
            this.returnedTypeExpected = returnedTypeExpected;
            this.isSystem = isSystem;

            crmSystemViewList1.DisplaySystemRules = isSystem;
            crmSystemViewList1.DisplayRulesTemplate = !isSystem;
        }

        public Entity SelectedRule { private set; get; }

        public IOrganizationService Service { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedRule = crmSystemViewList1.GetSelectedSystemView().FirstOrDefault();

            if (SelectedRule == null)
            {
                MessageBox.Show(this, "Please select a view", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void RuleSelectionDialog_Load(object sender, EventArgs e)
        {
            crmSystemViewList1.LoadSystemViews(returnedTypeExpected: returnedTypeExpected);
        }
    }
}