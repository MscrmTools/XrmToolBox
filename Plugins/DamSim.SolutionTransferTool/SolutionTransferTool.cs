using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Client.Services;
using XrmToolBox;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using XrmToolBox.Attributes;

[assembly: BackgroundColor("MediumBlue")]
[assembly: PrimaryFontColor("White")]
[assembly: SecondaryFontColor("LightGray")]
[assembly: SmallImageBase64("iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAACxIAAAsSAdLdfvwAAAAHdElNRQfdBRwPFgneQ4hIAAAAGnRFWHRTb2Z0d2FyZQBQYWludC5ORVQgdjMuNS4xMDD0cqEAAARBSURBVFhH7VZ7TFtVGC9lpVDaSwqXMiQttFCQh50WpKiw8YwPNGoQdVlkI2o0kqDBZGJ0icYxNrXhNZSaZUaMhpEsW7YosiywGqCrpS3t4NIic0s2mLO0c74ShPbz3EdbS9sJOPYXv+SXrz3nd37f1++cc29Zm9jESvSUVoj1hcVqomi7xualio7TKHppU5VopotKqEjO0+MlGgsam0DaCSr6STz8hPrG0V4xkyY0OvfsSTgl2moiGjHQpmFgi4hZGzkCMGzPBuKAGGzdcTQ/oaPudSGYKh8xLY0bE5h0gegouF96WpRkmv+YBx4HG4aV6yiAoR3H4UabHNzzMeBZiABAJD0Nh/hgrKg0uTRHpExaGupnnsVPJ4rMV1r41AKSZ5Wo8hDmXk4jEhE8Koaat3H48HNTLvLa4vP0LLBBvx8DY3mleXFkDKeSt21Tpp3Cceu19lha5KTFV0f5cC4fAztKstJ8Jl4EP36JwcxxHowfFMBYfGyQhuQ0Wvvr53lUYrILVCfQZ6MadWJHudXZ2Z3Gas9V7DaVZbrBEe0T0cIIGDnMC1nArDwDzXN9WoNaAFMhdCQv3pcKy5eFPk8yLs5HwYA8wUM8t6uepR0cjDySntUx99494Lke5ROS1PZyUAHBv+4iKgCctJak28mFQbEgSEcRHcw/Tsh8vn/PbYHva+PAkv/Ap/M/GCKpbTjZ1RXzlVw+4DpK7tnqC/AssCgdaa7bRW5XoM7L62/SvuBEZ2AvBvocxfBc3zEeldyL/TW1ScckUsOf32WuoQB/sWNvxYY9kJeqpEjDhskuPpxPlVsnm99JYdIG4v0dpbIzSrlj0SChFty6AP8ZIGlsxWFyhc7L2UIxXDrBh5F0scv6SsPdTLrQ+KhAVTxanOtcnhWtoQNs0DXzw3bAnpECw9Lkm5bqp8qYNLdGV4HqBWttNpz7LD58AegW/HsLdHXhzwDBwcDy2JMvMvarQ3euYt+ZvNRlW5hrCAv+W7Ds5MCQNPSDi4jC3BMl5S2M7erxwc6d0T3J4i8IdrAp3QF/AfZ+AVjCPAfMecq+8TeaYhjbtWGgr5/9dVLK2akVRdjxreBqSwQXem/M7YsFPR68TTY2D6xZitGrRj1919eL1rIKyUk8eSYowX/QmiL7iXitMZ2x+X9oeqg4fSBW6CCN6YNGtjscY+CCMNllebUhh1l+e3AoT1Gt5WK/67YlwlBRHCIWyAcx+FYlBKsg4S9T1aNPM8tuLz7Myqmbej53yX0FC7iC5EG8aePCUH68x1xa9RIj3xhoMjJbL+9VeDwLHF8BbgcHtNXoH09RSQcj2zi8vbue23OXpPeXdvSeR4mXrkWC7mUMXbf8flPzu9GMbGPRVP041p+Vof/teDaYWwSgl2eaiAMHhcz0nUFHTY3kG5mMGJfIZi40NKYxw3cWnaXl956vqy9kvm5iE+sAi/UPGYDwI+f5aW0AAAAASUVORK5CYII=")]
[assembly: BigImageBase64("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")]

namespace DamSim.SolutionTransferTool
{
    
    public partial class SolutionTransferTool : UserControl, IMsCrmToolsPluginUserControl
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
            btnDownloadLog.Enabled = true;
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
                btnDownloadLog.Enabled = false;

                var worker = new BackgroundWorker();
                worker.DoWork += (o, args) => DownloadLogFile(dialog.SelectedPath);
                worker.RunWorkerCompleted += (o, args) => {
                    if (args.Error != null) {
                        var message = string.Format("An error was encountered while downloading the log file.{0}Error:{0}{1}", Environment.NewLine, args.Error.Message);
                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } else {
                        MessageBox.Show("Log file download completed.", "File Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    btnSelectTarget.Enabled = true;
                    tsbTransfertSolution.Enabled = true;
                    tsbLoadSolutions.Enabled = true;
                    btnDownloadLog.Enabled = true;
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

                btnDownloadLog.Enabled = false;
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
        
    }
}
