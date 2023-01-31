using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using XrmToolBox.ToolLibrary.AppCode;
using XrmToolBox.ToolLibrary.UserControls;

namespace XrmToolBox.ToolLibrary.Forms
{
    public partial class ToolLibraryForm : DockContent
    {
        public void AskForPluginsClosing()
        {
            PluginsClosingRequested?.Invoke(this, new EventArgs());
        }

        private void Ctrl_OnToolOperationRequested(object sender, ToolOperationEventArgs e)
        {
            if (e.IsInstallation)
            {
                if (e.Plugin.RequireLicenseAcceptance ?? false)
                {
                    using (var licenseAcceptanceForm = new LicenseAcceptanceForm(new List<XtbPlugin> { e.Plugin }))
                    {
                        if (licenseAcceptanceForm.ShowDialog(this) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }

                var pc = ShowProgressPanel();
                var bw = new BackgroundWorker();
                bw.DoWork += (s, evt) =>
                {
                    Invoke(new Action(() =>
                    {
                        pc.AddDownloadStep(e.Plugin.Name);
                        toolLibrary.DownloadPackage(e, toolLibrary.PendingUpdates);
                        pc.SetSuccess();
                    }));

                    Invoke(new Action(() =>
                    {
                        pc.AddInstallStep(e.Plugin.Name);
                        evt.Result = toolLibrary.PerformInstallation(toolLibrary.PendingUpdates, this);
                    }));
                };
                bw.RunWorkerCompleted += (s, evt) =>
                {
                    if (evt.Error != null)
                    {
                        pc.SetError(evt.Error.Message);
                    }
                    else
                    {
                        if ((bool)evt.Result)
                        {
                            e.Plugin.CurrentVersion = e.Version;
                            toolLibrary.ScanInstalledTools();
                            toolLibrary.AnalyzePackage(e.Plugin);
                        }
                        else
                        {
                            ShowRestartButton();
                        }

                        e.Succeeded = (bool)evt.Result;

                        pc.SetSuccess(!e.Succeeded);
                    }

                    pc.End();

                    lvTools.Invalidate();
                };
                bw.RunWorkerAsync();
            }
            else
            {
                toolLibrary.PrepareUninstallPlugins(new List<XtbPlugin> { e.Plugin }, toolLibrary.PendingDeletions);
                toolLibrary.PerformUninstallation(toolLibrary.PendingDeletions);
                ShowRestartButton();
            }
        }

        private void tsbBulkDelete_Click(object sender, EventArgs e)
        {
            if (lvTools.CheckedItems.Count == 0) return;

            if (MessageBox.Show(this, $"Are you sure you want to uninstall {lvTools.CheckedItems.Count} tools?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            var toolsToUninstall = lvTools.CheckedItems.Cast<ListViewItem>().Where(i => ((XtbPlugin)i.Tag).Action == PackageInstallAction.None).Select(i => (XtbPlugin)i.Tag).ToList();

            toolLibrary.PrepareUninstallPlugins(toolsToUninstall, toolLibrary.PendingDeletions);
            toolLibrary.PerformUninstallation(toolLibrary.PendingDeletions);
            ShowRestartButton();
        }

        private void tsbBulkInstall_Click(object sender, EventArgs e)
        {
            if (lvTools.CheckedItems.Count == 0) return;

            var sb = new StringBuilder();

            var nbInstall = lvTools.CheckedItems.Cast<ListViewItem>().Count(i => ((XtbPlugin)i.Tag).Action == PackageInstallAction.Install);
            if (nbInstall > 0) sb.AppendLine($"- Install {nbInstall} tool{(nbInstall == 1 ? "" : "s")}");
            var nbUpdate = lvTools.CheckedItems.Cast<ListViewItem>().Count(i => ((XtbPlugin)i.Tag).Action == PackageInstallAction.Update);
            if (nbUpdate > 0) sb.AppendLine($"- Update {nbUpdate} tool{(nbUpdate == 1 ? "" : "s")}");

            if (MessageBox.Show(this, $"Are you sure you want to:\n{sb}", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            var toolsToInstall = lvTools.CheckedItems.Cast<ListViewItem>().Where(i => ((XtbPlugin)i.Tag).Action == PackageInstallAction.Install || ((XtbPlugin)i.Tag).Action == PackageInstallAction.Update).Select(i => (XtbPlugin)i.Tag).ToList();

            var licensedPlugins = toolsToInstall.Where(p => p.RequireLicenseAcceptance ?? false).ToList();
            if (licensedPlugins.Any())
            {
                using (var licenseAcceptanceForm = new LicenseAcceptanceForm(licensedPlugins))
                {
                    if (licenseAcceptanceForm.ShowDialog(this) != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }

            var pc = ShowProgressPanel();

            var bw = new BackgroundWorker();
            bw.DoWork += (s, evt) =>
            {
                foreach (var tool in toolsToInstall)
                {
                    Invoke(new Action(() => { pc.AddDownloadStep(tool.Name); }));

                    toolLibrary.DownloadPackage(new ToolOperationEventArgs(true, tool, new Version(tool.Version), tool.DownloadUrl), toolLibrary.PendingUpdates);

                    Invoke(new Action(() => { pc.SetSuccess(); }));
                }

                Invoke(new Action(() => { pc.AddInstallStep(null); }));

                evt.Result = toolLibrary.PerformInstallation(toolLibrary.PendingUpdates, this);
            };
            bw.RunWorkerCompleted += (s, evt) =>
            {
                SetState(false);

                if (evt.Error != null)
                {
                    pc.SetError(evt.Error.Message);
                }
                else
                {
                    pc.SetSuccess(!(bool)evt.Result);
                }

                pc.End();

                if ((bool)evt.Result)
                {
                    plugins = new DirectoryInfo(Paths.PluginsPath).GetFiles();

                    toolLibrary.ScanInstalledTools();

                    Parallel.ForEach(toolsToInstall,
                        plugin =>
                        {
                            toolLibrary.AnalyzePackage(plugin);
                        });

                    lvTools.Invalidate();
                }
                else
                {
                    ShowRestartButton();
                }
            };
            bw.RunWorkerAsync();
        }

        #region Methods

        private InstallProgressControl ShowProgressPanel()
        {
            var pc = new InstallProgressControl();
            pc.Size = new System.Drawing.Size(800, 400);
            pc.Location = new System.Drawing.Point((Width - pc.Width) / 2, (Height - pc.Height) / 2);
            pc.OnCloseRequested += (ipc, evt) =>
            {
                Controls.Remove(pc);
                pc.Dispose();
            };
            Controls.Add(pc);
            pc.BringToFront();

            return pc;
        }

        private void ShowRestartButton()
        {
            var installCount = toolLibrary.PendingUpdates.Plugins.GroupBy(p => p.Name).Count();
            var install = $"to install {installCount} tool{(installCount > 1 ? "s" : "")}";

            var deleteCount = toolLibrary.PendingDeletions.Plugins.GroupBy(p => p.Name).Count();
            var delete = $"to remove {deleteCount} tool{(deleteCount > 1 ? "s" : "")}";

            if (installCount > 0 || deleteCount > 0)
            {
                tsbRestart.ToolTipText = $"Restart {(installCount > 0 ? install : "")}{(installCount > 0 && deleteCount > 0 ? " and " : "")}{(deleteCount > 0 ? delete : "")}";
                tsbRestart.Visible = true;
                tssSettings.Visible = true;
            }
        }

        #endregion Methods
    }
}