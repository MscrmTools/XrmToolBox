// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.SolutionImport
{
    public partial class SolutionImport : PluginControlBase
    {
        #region Variables

        private Guid currentOperationId;
        private SolutionManager sManager;

        #endregion Variables

        public SolutionImport()
        {
            InitializeComponent();

            gbImportSolution.AllowDrop = true;
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
                OverwriteUnmanagedCustomizations = chkOverwriteUnmanagedCustomizations.Checked,
            };

            ExecuteMethod(ImportArchive, iSettings);
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

        private void GbImportSolutionDragDrop(object sender, DragEventArgs e)
        {
            var iSettings = new ImportSettings
            {
                ImportId = Guid.NewGuid(),
                DownloadLog = chkDownload.Checked,
                IsFolder = false,
                Path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0],
                Publish = chkPublish.Checked,
                Activate = chkActivate.Checked,
                ConvertToManaged = chkConvertToManaged.Checked,
                OverwriteUnmanagedCustomizations = chkOverwriteUnmanagedCustomizations.Checked,
            };
            ExecuteMethod(ImportArchive, iSettings);
        }

        private void GbImportSolutionDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 1 || !files[0].EndsWith(".zip"))
                    e.Effect = DragDropEffects.None;
                else
                    e.Effect = DragDropEffects.All;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void ImportArchive(ImportSettings iSettings)
        {
            if (ConnectionDetail.OrganizationMajorVersion == 8)
            {
                if (DialogResult.No == MessageBox.Show(ParentForm,
                        "This plugin has not been tested with CRM 2016 yet, especially regarding new solution framework\r\n\r\nAre you sure you want to continue?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    return;
                }
            }

            sManager = new SolutionManager(Service);
            iSettings.MajorVersion = ConnectionDetail.OrganizationMajorVersion;

            currentOperationId = iSettings.ImportId;

            EnableControls(false);

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Importing solution...",
                AsyncArgument = iSettings,
                Work = (bw, e) =>
                {
                    var settings = (ImportSettings)e.Argument;

                    // Launch a new thread to monitor import status
                    if (settings.IsFolder)
                    {
                        sManager.ImportSolutionFolder(settings);
                    }
                    else
                    {
                        sManager.ImportSolutionArchive(settings.Path, settings);
                    }

                    if (((ImportSettings)e.Argument).Publish)
                    {
                        bw.ReportProgress(101, "Publishing solution...");
                        sManager.PublishAll();
                    }
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        var eForm = new ErrorForm(e.Error.Message);
                        eForm.ShowDialog();
                    }

                    EnableControls(true);
                },
                ProgressChanged = e => { SetWorkingMessage(e.ProgressPercentage <= 100 ? "Importing solution..." : "Publishing solution..."); }
            });
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            CloseTool();
        }
    }
}