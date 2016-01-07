// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.WebResourcesManager.AppCode;
using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.Forms.Solutions
{
    public partial class SolutionPicker : Form
    {
        private readonly IOrganizationService innerService;

        public SolutionPicker(IOrganizationService service)
        {
            InitializeComponent();

            innerService = service;
        }

        public Entity SelectedSolution { get; set; }

        private void btnSolutionPickerCancel_Click(object sender, EventArgs e)
        {
            SelectedSolution = null;
            DialogResult = DialogResult.Cancel;
            Close();
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

        private void lstSolutions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var list = (ListView)sender;
            list.Sorting = list.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            list.ListViewItemSorter = new ListViewItemComparer(e.Column, list.Sorting);
        }

        private void lstSolutions_DoubleClick(object sender, EventArgs e)
        {
            btnSolutionPickerValidate_Click(null, null);
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

        private void SolutionPicker_Load(object sender, EventArgs e)
        {
            lstSolutions.Items.Clear();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = RetrieveSolutions();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Entity solution in ((EntityCollection)e.Result).Entities)
            {
                ListViewItem item = new ListViewItem(solution["friendlyname"].ToString());
                item.SubItems.Add(solution["version"].ToString());
                item.SubItems.Add(((EntityReference)solution["publisherid"]).Name);
                item.Tag = solution;

                lstSolutions.Items.Add(item);
            }

            lstSolutions.Enabled = true;
            btnSolutionPickerValidate.Enabled = true;
        }
    }
}