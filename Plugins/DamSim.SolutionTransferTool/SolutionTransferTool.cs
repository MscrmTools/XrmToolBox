using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using InformationPanel = XrmToolBox.Extensibility.InformationPanel;

namespace DamSim.SolutionTransferTool
{
    public partial class SolutionTransferTool : UserControl, IXrmToolBoxPluginControl
    {
        #region Variables

        private IOrganizationService service;
        private IOrganizationService targetService;

        private Panel infoPanel;

        private int currentsColumnOrder;
        private Guid importId;
        #endregion

        #region Constructor

        public SolutionTransferTool()
        {
            InitializeComponent();
        }

        #endregion

        #region XrmToolbox

        public event EventHandler OnCloseTool;

        public event EventHandler OnRequestConnection;

        public Image PluginLogo
        {
            get { return imageList1.Images[0]; }
        }

        public IOrganizationService Service
        {
            get { return service; }
        }

        public void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName = "", object parameter = null)
        {
            if (actionName == "TargetOrganization")
            {
                targetService = newService;
                SetConnectionLabel(targetService, "Target");
                ((OrganizationServiceProxy) ((OrganizationService) targetService).InnerService).Timeout = new TimeSpan(
                    0, 1, 0, 0);
            }
            else
            {
                service = newService;
                SetConnectionLabel(service, "Source");
                ((OrganizationServiceProxy) ((OrganizationService) service).InnerService).Timeout = new TimeSpan(0, 1, 0,
                                                                                                                 0);
                RetrieveSolutions();
            }
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(infoPanel, e.UserState.ToString());
        }

        /// <summary>
        /// Executes once the work is done, ie the solution import.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            tsbLoadSolutions.Enabled = true;
            tsbTransfertSolution.Enabled = true;
            tsbDownloadLogFile.Enabled = true;
            btnSelectTarget.Enabled = true;
            Cursor = Cursors.Default;

            string message;

            if (e.Error != null)
            {
                message = string.Format("An error occured: {0}", e.Error.Message);
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                message = "Import finished successfully!";
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region UI Events

        private void BtnCloseClick(object sender, EventArgs e)
        {
            if (OnCloseTool != null)
            {
                const string message = "Are you sure to exit?";
                if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                    OnCloseTool(this, null);
            }
        }

        private void BtnSelectTargetClick(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs {ActionName = "TargetOrganization", Control = this};
                OnRequestConnection(this, args);
            }
        }

        private void BtnDownloadLogClick(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LastFolderUsed))
                dialog.SelectedPath = Properties.Settings.Default.LastFolderUsed;

            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                Properties.Settings.Default.LastFolderUsed = dialog.SelectedPath;
                Properties.Settings.Default.Save();

                Cursor = Cursors.WaitCursor;
                btnSelectTarget.Enabled = false;
                tsbTransfertSolution.Enabled = false;
                tsbLoadSolutions.Enabled = false;
                tsbDownloadLogFile.Enabled = false;

                var worker = new BackgroundWorker();
                worker.DoWork += (o, args) => DownloadLogFile(dialog.SelectedPath);
                worker.RunWorkerCompleted += (o, args) => {
                    if (args.Error != null) {
                        var message = string.Format("An error was encountered while downloading the log file.{0}Error:{0}{1}", Environment.NewLine, args.Error.Message);
                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else {
                        MessageBox.Show("Download completed!", "File Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    btnSelectTarget.Enabled = true;
                    tsbTransfertSolution.Enabled = true;
                    tsbLoadSolutions.Enabled = true;
                    tsbDownloadLogFile.Enabled = true;
                    Cursor = Cursors.Default;
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the connections labels on either the source/target section
        /// </summary>
        /// <param name="serviceToLabel"></param>
        /// <param name="serviceType"></param>
        private void SetConnectionLabel(IOrganizationService serviceToLabel, string serviceType)
        {
            var serviceProxy = (OrganizationServiceProxy) ((OrganizationService) serviceToLabel).InnerService;
            var uri = serviceProxy.EndpointSwitch.PrimaryEndpoint;
            var hostName = uri.Host;
            string orgName;
            if (hostName.ToLower().Contains("dynamics.com"))
            {
                orgName = hostName.Split('.')[0];
                hostName = hostName.Remove(0, orgName.Length + 1);
            }
            else
            {
                orgName = uri.AbsolutePath.Substring(1);
                var index = orgName.IndexOf("/", 0, StringComparison.Ordinal);
                orgName = orgName.Substring(0, index);
            }

            var connectionName = string.Format("{0} ({1})", hostName, orgName);
            switch (serviceType)
            {
                case "Source":
                    lblSource.Text = connectionName;
                    lblSource.ForeColor = Color.Green;
                    break;
                case "Target":
                    lblTarget.Text = connectionName;
                    lblTarget.ForeColor = Color.Green;
                    break;
            }
        }

        /// <summary>
        /// Retrieves unmanaged solutions from the source organization
        /// </summary>
        private void RetrieveSolutions()
        {
            lstSourceSolutions.Items.Clear();

            var sourceSolutionsQuery = new QueryExpression
                                           {
                                               EntityName = "solution",
                                               ColumnSet =
                                                   new ColumnSet(new[]
                                                                     {
                                                                         "publisherid", "installedon", "version",
                                                                         "uniquename", "friendlyname", "description"
                                                                     }),
                                               Criteria = new FilterExpression()
                                           };

            sourceSolutionsQuery.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, false);
            sourceSolutionsQuery.Criteria.AddCondition("isvisible", ConditionOperator.Equal, true);
            sourceSolutionsQuery.Criteria.AddCondition("uniquename", ConditionOperator.NotEqual, "Default");

            var solutions = service.RetrieveMultiple(sourceSolutionsQuery);

            foreach (var solution in solutions.Entities)
            {
                var item = new ListViewItem();
                item.Tag = solution.GetAttributeValue<Guid>("solutionid");
                item.Text = solution.GetAttributeValue<String>("uniquename");
                item.SubItems.Add(solution.GetAttributeValue<String>("friendlyname"));
                item.SubItems.Add(solution.GetAttributeValue<String>("version"));
                item.SubItems.Add(solution.GetAttributeValue<DateTime>("installedon").ToShortDateString());
                item.SubItems.Add(solution.GetAttributeValue<EntityReference>("publisherid").Name);
                item.SubItems.Add(solution.GetAttributeValue<String>("description"));
                lstSourceSolutions.Items.Add(item);
            }
        }

        /// <summary>
        /// Exports the selected solution as a managed one, and imports it on the target organization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerDoWorkExport(object sender, DoWorkEventArgs e)
        {
            var bw = (BackgroundWorker) sender;
            var requests = (List<OrganizationRequest>) e.Argument;

            bw.ReportProgress(0, "Exporting solution...");
            var exportResponse = (ExportSolutionResponse) service.Execute(requests[0]);

            bw.ReportProgress(0, "Importing solution...");
            ((ImportSolutionRequest) requests[1]).CustomizationFile = exportResponse.ExportSolutionFile;
            targetService.Execute(requests[1]);

            if (requests.Count == 3)
            {
                bw.ReportProgress(0, "Publishing...");
                targetService.Execute(requests[2]);
            }
        }

        /// <summary>
        /// Downloads the Log file
        /// </summary>
        /// <param name="path"></param>
        private void DownloadLogFile(string path)
        {
            var importLogRequest = new RetrieveFormattedImportJobResultsRequest
            {
                ImportJobId = importId
            };
            var importLogResponse = (RetrieveFormattedImportJobResultsResponse)targetService.Execute(importLogRequest);
            var filePath = string.Format(@"{0}\{1}.xml", path, DateTime.Now.ToString("yyyy_MM_dd__HH_mm"));
            File.WriteAllText(filePath, importLogResponse.FormattedResults);
        }

        #endregion

        private void TsbLoadSolutionsClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs {ActionName = "WhoAmI", Control = this, Parameter = null};
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                RetrieveSolutions();
            }
        }

        private void TsbTransfertSolutionClick(object sender, EventArgs e)
        {
            if (lstSourceSolutions.SelectedItems.Count == 1 && targetService != null) 
            {
                importId = Guid.NewGuid();

                var item = lstSourceSolutions.SelectedItems[0];

                infoPanel = InformationPanel.GetInformationPanel(this, "Initializing...", 340, 120);

                var requests = new List<OrganizationRequest>();
                requests.Add(new ExportSolutionRequest
                                 {
                                     Managed = chkExportAsManaged.Checked,
                                     SolutionName = item.Text,
                                     ExportAutoNumberingSettings = chkAutoNumering.Checked,
                                     ExportCalendarSettings = chkCalendar.Checked,
                                     ExportCustomizationSettings = chkCustomization.Checked,
                                     ExportEmailTrackingSettings = chkEmailTracking.Checked,
                                     ExportGeneralSettings = chkGeneral.Checked,
                                     ExportIsvConfig = chkIsvConfig.Checked,
                                     ExportMarketingSettings = chkMarketing.Checked,
                                     ExportOutlookSynchronizationSettings = chkOutlookSynchronization.Checked,
                                     ExportRelationshipRoles = chkRelationshipRoles.Checked
                                 });
                requests.Add(new ImportSolutionRequest
                                 {
                                     ConvertToManaged = chkConvertToManaged.Checked,
                                     OverwriteUnmanagedCustomizations = chkOverwriteUnmanagedCustomizations.Checked,
                                     PublishWorkflows = chkActivate.Checked,
                                     ImportJobId = importId
                                 });
                
                if (!chkExportAsManaged.Checked && chkPublish.Checked)
                {
                    requests.Add(new PublishAllXmlRequest());
                }

                tsbDownloadLogFile.Enabled = false;
                tsbLoadSolutions.Enabled = false;
                tsbTransfertSolution.Enabled = false;
                btnSelectTarget.Enabled = false;
                Cursor = Cursors.WaitCursor;

                var worker = new BackgroundWorker();
                worker.DoWork += WorkerDoWorkExport;
                worker.ProgressChanged += WorkerProgressChanged;
                worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.RunWorkerAsync(requests);
            }
            else
            {
                MessageBox.Show("You have to select a source solution and a target organization to continue.", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ChkExportAsManagedCheckedChanged(object sender, EventArgs e)
        {
            chkConvertToManaged.Enabled = !chkExportAsManaged.Checked;
            chkOverwriteUnmanagedCustomizations.Enabled = chkExportAsManaged.Checked;

            if (chkExportAsManaged.Checked)
            {
                chkConvertToManaged.Checked = false;
            }
        }

        private void lstSourceSolutions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == currentsColumnOrder)
            {
                lstSourceSolutions.Sorting = lstSourceSolutions.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                lstSourceSolutions.ListViewItemSorter = new ListViewItemComparer(e.Column, lstSourceSolutions.Sorting);
            }
            else
            {
                currentsColumnOrder = e.Column;
                lstSourceSolutions.ListViewItemSorter = new ListViewItemComparer(e.Column, SortOrder.Ascending);
            }
        }

        public void ClosingPlugin(PluginCloseInfo info)
        {
            
            if (info.FormReason != CloseReason.None ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAll ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAllExceptActive)
            {
                return;
            }

            info.Cancel = MessageBox.Show(@"Are you sure you want to close this tab?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
        }

        public string GetMyType()
        {
            return GetType().FullName;
        }

        public string GetCompany()
        {
            return GetType().GetCompany();
        }

        public string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }
    }
}
