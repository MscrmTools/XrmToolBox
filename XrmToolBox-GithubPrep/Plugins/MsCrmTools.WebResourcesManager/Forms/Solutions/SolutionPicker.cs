// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.WebResourcesManager.DelegatesHelpers;

namespace MsCrmTools.WebResourcesManager.Forms.Solutions
{
    public partial class SolutionPicker : Form
    {
        readonly IOrganizationService innerService;
        public Entity SelectedSolution { get; set; }

        public SolutionPicker(IOrganizationService service)
        {
            InitializeComponent();

            innerService = service;
        }

        private void SolutionPicker_Load(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            FillList();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MethodInvoker mi = delegate
            {
                lstSolutions.Enabled = true;
                btnSolutionPickerValidate.Enabled = true;
            };

            if (InvokeRequired)
            {
                Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        private void FillList()
        {
            try
            {
                ListViewDelegates.ClearItems(lstSolutions);

                EntityCollection ec = RetrieveSolutions();

                foreach (Entity solution in ec.Entities)
                {
                    ListViewItem item = new ListViewItem(solution["friendlyname"].ToString());
                    item.SubItems.Add(solution["version"].ToString());
                    item.SubItems.Add(((EntityReference)solution["publisherid"]).Name);
                    item.Tag = solution;

                    ListViewDelegates.AddItem(lstSolutions, item);
                }
            }
            catch (Exception error)
            {
                CommonDelegates.DisplayMessageBox(this, error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }
        }

        private EntityCollection RetrieveSolutions()
        {
            try
            {
                QueryExpression qe = new QueryExpression("solution");
                qe.Distinct = true;
                qe.ColumnSet = new ColumnSet(true);
                qe.Criteria = new FilterExpression();
                qe.Criteria.AddCondition(new ConditionExpression("ismanaged", ConditionOperator.Equal, false));
                qe.Criteria.AddCondition(new ConditionExpression("isvisible", ConditionOperator.Equal, true));
                qe.Criteria.AddCondition(new ConditionExpression("uniquename", ConditionOperator.NotEqual, "Default"));
                
                return innerService.RetrieveMultiple(qe);
            }
            catch (Exception error)
            {
                if (error.InnerException != null && error.InnerException is FaultException)
                {
                    throw new Exception("Error while retrieving solutions: " + (error.InnerException).Message);
                }
                else
                {
                    throw new Exception("Error while retrieving solutions: " + error.Message);
                }
            }
        }

        private void lstSolutions_DoubleClick(object sender, EventArgs e)
        {
            btnSolutionPickerValidate_Click(null, null);
        }

        private void btnSolutionPickerValidate_Click(object sender, EventArgs e)
        {
            if (lstSolutions.SelectedItems.Count > 0)
            {
                SelectedSolution = (Entity)lstSolutions.SelectedItems[0].Tag;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Please select a solution!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSolutionPickerCancel_Click(object sender, EventArgs e)
        {
            SelectedSolution = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lstSolutions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var list = (ListView)sender;
            list.Sorting = list.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            list.ListViewItemSorter = new ListViewItemComparer(e.Column, list.Sorting);
        }
    }
}
