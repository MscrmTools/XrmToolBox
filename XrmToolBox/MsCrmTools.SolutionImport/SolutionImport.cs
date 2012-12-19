// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Tanguy.WinForm.Utilities.DelegatesHelpers;
using XrmToolBox;

namespace MsCrmTools.SolutionImport
{
    public partial class SolutionImport : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        private Panel infoPanel;
        private SolutionManager sManager;
        private IOrganizationService service;

        #endregion Variables

        public SolutionImport()
        {
            InitializeComponent();
        }

        public IOrganizationService Service
        {
            get { return service; }
        }

        public Image PluginLogo
        {
            get { return imageList1.Images[0]; }
        }

        public event EventHandler OnRequestConnection;
        public event EventHandler OnCloseTool;

        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;
            sManager= new SolutionManager(service);

            if (actionName == "ImportSolution")
            {
                ImportArchive((ImportSettings) parameter);
            }
        }

        private void GbImportSolutionDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[]) e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 1 || !files[0].EndsWith(".zip"))
                    e.Effect = DragDropEffects.None;
                else
                    e.Effect = DragDropEffects.All;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void GbImportSolutionDragDrop(object sender, DragEventArgs e)
        {
            var iSettings = new ImportSettings
                                {
                                    ImportId = Guid.NewGuid(),
                                    DownloadLog = chkDownload.Checked,
                                    IsFolder = false,
                                    Path = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0],
                                    Publish = chkPublish.Checked,
                                    Activate = chkActivate.Checked,
                                    ConvertToManaged = chkConvertToManaged.Checked,
                                    OverwriteUnmanagedCustomizations = chkOverwriteUnmanagedCustomizations.Checked
                                };

            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs {ActionName = "ImportSolution", Control = this, Parameter = iSettings};
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                ImportArchive(iSettings);
            }
        }

        private void ImportArchive(ImportSettings iSettings)
        {
            CommonDelegates.SetCursor(this, Cursors.WaitCursor);

            infoPanel = InformationPanel.GetInformationPanel(this, "Importing solution...", 340, 100);

            EnableControls(false);

            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.ProgressChanged += WorkerProgressChanged;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(iSettings);
        }

        void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage <= 100)
            {
                InformationPanel.ChangeInformationPanelMessage(infoPanel, "Importing solution...");
            }
            else
            {
                InformationPanel.ChangeInformationPanelMessage(infoPanel, "Publishing solution...");
            }
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker) sender;
            var settings = (ImportSettings) e.Argument;

            if (settings.IsFolder)
            {
                sManager.ImportSolutionFolder(settings);
            }
            else
            {
                sManager.ImportSolutionArchive(settings.Path, settings);
            }

            if (((ImportSettings) e.Argument).Publish)
            {
                worker.ReportProgress(101, "Publishing solution...");
                sManager.PublishAll();
            }
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            if (e.Error != null)
            {
                var eForm = new ErrorForm(e.Error.Message);
                eForm.ShowDialog();
            }

            EnableControls(true);
            CommonDelegates.SetCursor(this, Cursors.Default);
        }

        private void BtnBrowseFolderClick(object sender, EventArgs e)
        {
            var fbDialog = new FolderBrowserDialog
                               {
                                   Description =
                                       "Select a folder containing the three files of a solution.\r\nThe folder must be name like a solution archive (ie. MySolution_1_0_0_0)",
                                   ShowNewFolderButton = true
                               };

            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = fbDialog.SelectedPath;
            }
        }

        private void BtnImportClick(object sender, EventArgs e)
        {
            var iSettings = new ImportSettings
                                {
                                    ImportId = Guid.NewGuid(),
                                    DownloadLog = chkDownload.Checked,
                                    IsFolder = true,
                                    Path = txtFolderPath.Text,
                                    Publish = chkPublish.Checked,
                                    Activate = chkActivate.Checked,
                                    ConvertToManaged = chkConvertToManaged.Checked,
                                    OverwriteUnmanagedCustomizations = chkOverwriteUnmanagedCustomizations.Checked
                                };

            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                                   {
                                       ActionName = "Import",
                                       Control = this,
                                       Parameter = iSettings
                                   };
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                ImportArchive(iSettings);
            }
        }

        private void EnableControls(bool enable)
        {
            MethodInvoker mi = delegate
                                   {
                                       chkDownload.Enabled = enable;
                                       chkPublish.Enabled = enable;
                                       chkActivate.Enabled = enable;
                                       chkConvertToManaged.Enabled = enable;
                                       chkOverwriteUnmanagedCustomizations.Enabled = enable;
                                       gbImportByFolder.Enabled = enable;
                                       gbImportSolution.Enabled = enable;
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

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
                OnCloseTool(this, null);
        }


    }
}