// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MsCrmTools.SiteMapEditor.Forms
{
    public partial class DashboardPicker : Form
    {
        private readonly IOrganizationService service;

        #region Properties

        /// <summary>
        /// Gets or sets the selected web resource
        /// </summary>
        public Entity SelectedDashboard { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class WebResourcePicker
        /// </summary>
        /// <param name="type">Type of web resource to select</param>
        public DashboardPicker(IOrganizationService service)
        {
            InitializeComponent();
            this.service = service;
            FillValues();
        }

        private void FillValues()
        {
            // Disables controls
            lstDashboards.Enabled = false;
            btnCancel.Enabled = false;
            btnOK.Enabled = false;
            btnRefresh.Enabled = false;
            lstDashboards.Items.Clear();

            // Run work
            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        #endregion Constructor

        #region Methods

        private void BtnRefreshClick(object sender, EventArgs e)
        {
            FillValues();
        }

        private void btnWebResourcePickerCancel_Click(object sender, EventArgs e)
        {
            SelectedDashboard = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnWebResourcePickerValidate_Click(object sender, EventArgs e)
        {
            if (lstDashboards.SelectedItems.Count > 0)
            {
                Entity dashboard = (Entity)lstDashboards.SelectedItems[0].Tag;
                SelectedDashboard = dashboard; ;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Please select a web resource!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private EntityCollection GetDashboards()
        {
            var qe = new QueryExpression("systemform")
            {
                Criteria = new FilterExpression
                {
                    Conditions =
                        {
                            new ConditionExpression("type", ConditionOperator.Equal, 0)
                        }
                },
                ColumnSet = { AllColumns = true }
            };

            return service.RetrieveMultiple(qe);
        }

        private void lstWebResources_DoubleClick(object sender, EventArgs e)
        {
            btnWebResourcePickerValidate_Click(null, null);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = GetDashboards();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, CrmExceptionHelper.GetErrorMessage(e.Error, false), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                foreach (Entity dashboard in ((EntityCollection)e.Result).Entities)
                {
                    var item = new ListViewItem(dashboard.Contains("name") ? dashboard["name"].ToString() : "N/A")
                    {
                        Tag = dashboard
                    };

                    lstDashboards.Items.Add(item);
                }

                lstDashboards.Sort();
                lstDashboards.Enabled = true;
                btnCancel.Enabled = true;
                btnOK.Enabled = true;
                btnRefresh.Enabled = true;
            }
        }

        #endregion Methods
    }
}