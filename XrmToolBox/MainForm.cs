// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using McTools.Xrm.Connection;
using McTools.Xrm.Connection.WinForms;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.UserControls;
using XrmToolBox.Forms;
using XrmToolBox.PluginsStore;
using XtbNuGetPackage = XrmToolBox.PluginsStore.XtbNuGetPackage;

namespace XrmToolBox
{
    public sealed partial class MainForm : Form
    {
        #region Variables

        private CrmConnectionStatusBar ccsb;
        private ConnectionManager cManager;
        private ConnectionDetail currentConnectionDetail;
        private Options currentOptions;
        private FormHelper fHelper;
        private string initialConnectionName;
        private string initialPluginName;
        private readonly List<PluginControlStatus> pluginControlStatuses;
        private PluginManagerExtended pManager;
        private IOrganizationService service;
        private Point scrollPosition;
        private PluginModel selectedPluginModel;
        private Store store;
        private readonly WelcomeDialog blackScreen;
        internal Options Options { get { return currentOptions; } }

        #endregion Variables

        #region Constructor

        public MainForm(string[] args)
        {
            pluginsModels = new List<PluginModel>();
            pluginControlStatuses = new List<PluginControlStatus>();

            // Set drawing optimizations
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            // Displaying Welcome screen
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            blackScreen = new WelcomeDialog(version) { StartPosition = FormStartPosition.CenterScreen };
            blackScreen.Show(this);
            blackScreen.SetWorkingMessage("Loading...");

            // Loading XrmToolBox options
            string errorMessage;
            if (!Options.Load(out currentOptions, out errorMessage))
            {
                MessageBox.Show(this,
                    "An error ocurred when loading XrmToolBox options. A new options file has been created.\n\n" + errorMessage);
            }
            
            // Read arguments to detect if a plugin should be displayed automatically
            if (args.Length > 0)
            {
                initialConnectionName = ExtractSwitchValue("/connection:", ref args);
                initialPluginName = ExtractSwitchValue("/plugin:", ref args);
            }

            InitializeComponent();
            
            ProcessMenuItemsForPlugin();
            MouseWheel += (sender, e) => pnlPlugins.Focus();

            Text = string.Format("{0} (v{1})", Text, Assembly.GetExecutingAssembly().GetName().Version);

            // Loading connection controls
            blackScreen.SetWorkingMessage("Loading connection controls...");
            ManageConnectionControl();
        }

        #endregion Constructor

        #region Connection methods

        private void ManageConnectionControl()
        {
            if (!Directory.Exists(Paths.ConnectionsPath))
            {
                Directory.CreateDirectory(Paths.ConnectionsPath);
            }

            ConnectionsList.ConnectionsListFilePath = Path.Combine(Paths.ConnectionsPath, "MscrmTools.ConnectionsList.xml");
            cManager = ConnectionManager.Instance;
            cManager.RequestPassword += (sender, e) => fHelper.RequestPassword(e.ConnectionDetail);
            cManager.StepChanged += (sender, e) => ccsb.SetMessage(e.CurrentStep);
            cManager.ConnectionSucceed += (sender, e) =>
            {
                var parameter = e.Parameter as ConnectionParameterInfo;
                if (parameter != null)
                {
                    Controls.Remove(parameter.InfoPanel);
                    parameter.InfoPanel.Dispose();
                }

                currentConnectionDetail = e.ConnectionDetail;
                service = e.OrganizationService;
                ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                ccsb.SetMessage(string.Empty);

                if (parameter != null)
                {
                    var control = parameter.ConnectionParmater as UserControl;
                    if (control != null)
                    {
                        var pluginModel = control.Tag as Lazy<IXrmToolBoxPlugin, IPluginMetadata>;
                        if (pluginModel == null)
                        {
                            // Actual Plugin was passed, Just update the plugin's Tab.
                            UpdateTabConnection((TabPage)control.Parent);
                        }
                        else
                        {
                            DisplayPluginControl(pluginModel);
                        }
                    }
                    else if (parameter.ConnectionParmater.ToString() == "ApplyConnectionToTabs" && tabControl1.TabPages.Count > 1)
                    {
                        ApplyConnectionToTabs();
                    }
                    else
                    {
                        var args = parameter.ConnectionParmater as RequestConnectionEventArgs;
                        if (args != null)
                        {
                            var userControl = (UserControl)args.Control;

                            args.Control.UpdateConnection(e.OrganizationService, currentConnectionDetail, args.ActionName, args.Parameter);

                            userControl.Parent.Text = string.Format("{0} ({1})",
                                userControl.Parent.Text.Split(' ')[0],
                                e.ConnectionDetail.ConnectionName);
                        }
                    }
                }
                else if (tabControl1.TabPages.Count > 1)
                {
                    ApplyConnectionToTabs();
                }

                StartPluginWithConnection();
            };
            cManager.ConnectionFailed += (sender, e) =>
            {
                this.Invoke(new Action(() =>
                {
                    var parameter = e.Parameter as ConnectionParameterInfo;
                    if (parameter != null)
                    {
                        Controls.Remove(parameter.InfoPanel);
                        parameter.InfoPanel.Dispose();
                    }

                    MessageBox.Show(this, e.FailureReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    currentConnectionDetail = null;
                    service = null;
                    ccsb.SetConnectionStatus(false, null);
                    ccsb.SetMessage(e.FailureReason);

                    StartPluginWithConnection();
                }));
            };

            fHelper = new FormHelper(this);
            ccsb = new CrmConnectionStatusBar(fHelper) { Dock = DockStyle.Bottom };
            Controls.Add(ccsb);
        }

        private void StartPluginWithConnection()
        {
            if (!string.IsNullOrEmpty(initialConnectionName) && !string.IsNullOrEmpty(initialPluginName))
            {
                StartPluginWithoutConnection();

                // Resetting initial connection name
                initialConnectionName = string.Empty;
            }
        }

        private void StartPluginWithoutConnection()
        {
            if (!string.IsNullOrEmpty(initialPluginName) && pManager?.Plugins != null)
            {
                var plugin = pManager.Plugins.FirstOrDefault(p => p.Metadata.Name == initialPluginName);
                if (plugin != null)
                {
                    Invoke(new Action(() =>
                    {
                        DisplayPluginControl(plugin);
                    }));
                }

                // Resetting initial plugin name
                initialPluginName = string.Empty;
            }
        }

        private void ConnectUponApproval(object connectionParameter)
        {
            var info = new ConnectionParameterInfo
            {
                ConnectionParmater = connectionParameter
            };
            fHelper.AskForConnection(info, () => info.InfoPanel = InformationPanel.GetInformationPanel(this, "Connecting...", 340, 120));
        }

        #endregion Connection methods

        #region Tasks to launch during startup

        private Task launchInitialConnection(ConnectionDetail connectionDetail)
        {
            return new Task(() => ConnectionManager.Instance.ConnectToServer(connectionDetail));
        }

        private Task LaunchVersionCheck()
        {
            return new Task(() =>
            {
                if (Options.DoNotCheckForUpdates == false)
                {
                    return;
                }

                blackScreen.SetWorkingMessage("Checking for XrmToolBox update...");

                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var cvc = new GithubVersionChecker(currentVersion, "MsCrmTools", "XrmToolBox");
                cvc.Run();

                if (!string.IsNullOrEmpty(cvc.Cpi?.Version))
                {
                    if (currentOptions.LastUpdateCheck.Date != DateTime.Now.Date)
                    {
                        Invoke(new Action(() =>
                        {
                            var nvForm = new NewVersionForm(currentVersion, cvc.Cpi.Version, cvc.Cpi.Description, "MsCrmTools", "XrmToolBox", new Uri(cvc.Cpi.PackageUrl));
                            var result = nvForm.ShowDialog(this);
                            if (result == DialogResult.OK)
                            {
                                Close();
                            }
                        }));
                    }
                }

                currentOptions.LastUpdateCheck = DateTime.Now;
                currentOptions.Save();
            });
        }

        private Task CheckForPluginsUpdate()
        {
            return new Task(() => Invoke(new Action(() =>
            {
                try
                {
                    store = new Store();
                    store.LoadNugetPackages();
                    var packages = store.Packages;

                    if (packages.Any(p => p.Action == PluginsStore.PackageInstallAction.Update))
                    {
                        var image = pluginsCheckerImageList.Images[3];
                        var text = packages.Count(p => p.Action == PluginsStore.PackageInstallAction.Update).ToString();

                        using (Graphics graphics = Graphics.FromImage(image))
                        {
                            using (Font arialFont = new Font("Courrier MS", 8, FontStyle.Bold))
                            {
                                var location = new Point(16 - (text.Length * 9), 5);
                                graphics.DrawString(text, arialFont, Brushes.Black, location);
                            }
                        }
                        
                        tsbPlugins.Image = image;

                        tsbPlugins.ToolTipText = string.Format("{0} new plugins\r\n{1} plugins updates",
                            packages.Count(p => p.Action == PluginsStore.PackageInstallAction.Install),
                            packages.Count(p => p.Action == PluginsStore.PackageInstallAction.Update));
                    }
                    else
                    {
                        tsbPlugins.Image = pluginsCheckerImageList.Images[2];
                    }
                }
                catch (Exception error)
                {
                    tsbPlugins.ToolTipText = "Failed to retrieve plugins updates: " + error.Message;
                }
            })));
        }

        #endregion Tasks to launch during startup

        #region Form events

        private async void MainForm_Load(object sender, EventArgs e)
        {
            tstxtFilterPlugin.Focus();

            pManager = new PluginManagerExtended(this) { IsWatchingForNewPlugins = true };
            pManager.Initialize();
            pManager.PluginsListUpdated += pManager_PluginsListUpdated;

            tstxtFilterPlugin.AutoCompleteCustomSource.AddRange(pManager.Plugins.Select(p => p.Metadata.Name).ToArray());

            blackScreen.SetWorkingMessage("Loading plugins...");
            DisplayPlugins();

            var tasks = new List<Task>
            {
                LaunchVersionCheck(),
                CheckForPluginsUpdate()
            };

            if (!string.IsNullOrEmpty(initialConnectionName))
            {
                var connectionDetail = ConnectionManager.Instance.ConnectionsList.Connections.FirstOrDefault(x => x.ConnectionName == initialConnectionName); ;

                if (connectionDetail != null)
                {
                    // If initiall connection is present, connect to given sever is initiated.
                    // After connection try to open intial plugin will be attempted.
                    tasks.Add(launchInitialConnection(connectionDetail));
                }
                else
                {
                    // Connection detail was not found, so name provided was incorrect.
                    // But if name of the plugin is set, it should be started
                    if (!string.IsNullOrEmpty(initialPluginName))
                    {
                        StartPluginWithoutConnection();
                    }
                }
            }
            else if (!string.IsNullOrEmpty(initialPluginName))
            {
                // If there is no initial connection, but initial plugin is set, openning plugin
                StartPluginWithoutConnection();
            }

            tasks.ForEach(x => x.Start());
            await Task.WhenAll(tasks.ToArray());

            // Adapt size of current form
            if (currentOptions.Size.IsMaximized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                currentOptions.Size.ApplyFormSize(this);
            }

            AdaptPluginControlSize();

            WebProxyHelper.ApplyProxy();

            // Hide & remove Welcome screen
            Opacity = 100;
            blackScreen.Hide();
            blackScreen.Dispose();

            if (!currentOptions.AllowLogUsage.HasValue)
            {
                currentOptions.AllowLogUsage = LogUsage.PromptToLog();
                currentOptions.Save();
            }

            if (currentOptions.DisplayPluginsStoreOnStartup)
            {
                if (currentOptions.DisplayPluginsStoreOnlyIfUpdates)
                {
                    if (store.Packages == null)
                    {
                        store.LoadNugetPackages();
                    }
                    if (store.Packages.Any(p => p.Action == PluginsStore.PackageInstallAction.Update))
                    {
                        pbOpenPluginsStore_Click(sender, e);
                    }
                }
                else
                {
                        pbOpenPluginsStore_Click(sender, e);
                }
            }
        }

        private void MainForm_OnCloseTool(object sender, EventArgs e)
        {
            //Issue 443: Restore scroll position after tool has been closed, if scroll bar has moved. Ref: https://support.microsoft.com/en-us/kb/829417
            scrollPosition = pnlPlugins.AutoScrollPosition;
            RequestCloseTab((TabPage)((UserControl)sender).Parent, new PluginCloseInfo(ToolBoxCloseReason.PluginRequest));
            if (scrollPosition.Y != 0)
            {
                pnlPlugins.AutoScrollPosition = new Point(Math.Abs(pnlPlugins.AutoScrollPosition.X), Math.Abs(scrollPosition.Y));
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            AdaptPluginControlSize();
        }

        private void aboutXrmToolBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var aForm = new WelcomeDialog(version, false) { StartPosition = FormStartPosition.CenterParent };
            aForm.ShowDialog(this);
        }
      
        private void MainForm_OnRequestConnection(object sender, EventArgs e)
        {
            if (e is RequestConnectionEventArgs)
            {
                ConnectUponApproval(e);
            }
            else
            {
                ConnectUponApproval(sender);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessMenuItemsForPlugin();

            if (tabControl1.SelectedIndex == 0)
            {
                tstxtFilterPlugin.Focus();
            }
            else
            {
                var control = (IXrmToolBoxPluginControl)tabControl1.SelectedTab.Controls[0];
                ((UserControl)control).Focus();
                var currentPluginStatus = pluginControlStatuses.FirstOrDefault(pcs => pcs.Control == control);
                if (currentPluginStatus == null)
                {
                    ccsb.SetMessage(null);
                    ccsb.SetProgress(null);
                }
                else
                {
                    ccsb.SetMessage(currentPluginStatus.Message);
                    ccsb.SetProgress(currentPluginStatus.Percentage);
                }
            }
        }


        #endregion Form events

        #region Main menu events

        private Thread searchThread;

        private void TsbConnectClick(object sender, EventArgs e)
        {
            ConnectUponApproval("ApplyConnectionToTabs");
        }

        private void tsbManageConnections_Click(object sender, EventArgs e)
        {
            fHelper.DisplayConnectionsList(this);
        }

        private void TsbOptionsClick(object sender, EventArgs e)
        {
            var oDialog = new OptionsDialog(currentOptions, pManager);
            if (oDialog.ShowDialog(this) == DialogResult.OK)
            {
                bool reinitDisplay = currentOptions.DisplayMostUsedFirst != oDialog.Option.DisplayMostUsedFirst
                                     || currentOptions.MostUsedList.Count != oDialog.Option.MostUsedList.Count
                                     || currentOptions.DisplayLargeIcons != oDialog.Option.DisplayLargeIcons
                                     || !oDialog.Option.HiddenPlugins.SequenceEqual(currentOptions.HiddenPlugins);

                currentOptions = oDialog.Option;

                if (reinitDisplay)
                {
                    //pManager.PluginsControls.Clear();
                    pluginsModels.Clear();
                    tabControl1.SelectedIndex = 0;
                    DisplayPlugins(tstxtFilterPlugin.Text);
                    AdaptPluginControlSize();
                }
            }
        }

        private void tstxtFilterPlugin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var name = ((ToolStripTextBox)(sender)).Text.ToLower();
                var plugin = pManager.Plugins.FirstOrDefault(p => p.Metadata.Name.ToLower().Contains(name));

                if (plugin != null)
                {
                    this.PluginClicked(new UserControl { Tag = plugin }, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));

                    // Clear the textbox
                    // ReSharper disable once PossibleNullReferenceException
                    tstxtFilterPlugin.TextBox.Text = string.Empty;
                }
            }
        }

        private void tstxtFilterPlugin_TextChanged(object sender, EventArgs e)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }

            searchThread = new Thread(DisplayPlugins);
            searchThread.Start(tstxtFilterPlugin.Text);
        }

        private void tsbPlugins_Click(object sender, EventArgs e)
        {
            pbOpenPluginsStore_Click(sender, e);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.C)
            {
                TsbConnectClick(null, null);
            }
        }

        private void pbOpenPluginsStore_Click(object sender, EventArgs e)
        {
            // If the options were not initialized, it means we are using the
            // new plugins store for the first time. Copy values from main
            // options file
            if (!PluginsStore.Options.Instance.IsInitialized)
            {
                PluginsStore.Options.Instance.PluginsStoreShowInstalled = Options.PluginsStoreShowInstalled;
                PluginsStore.Options.Instance.PluginsStoreShowIncompatible = Options.PluginsStoreShowIncompatible;
                PluginsStore.Options.Instance.PluginsStoreShowNew = Options.PluginsStoreShowNew;
                PluginsStore.Options.Instance.PluginsStoreShowUpdates = Options.PluginsStoreShowUpdates;
                PluginsStore.Options.Instance.IsInitialized = true;
            }

            var pc = new StoreForm();
            pc.PluginsUpdated += (storeForm, evt) =>
            {
                // If plugins list gets updated, refresh the list
                ReloadPluginsList();
            };

            // Avoid scanning for new files during Plugins Store usage.
            EnableNewPluginsWatching(false);
            pc.ShowDialog(this);
            EnableNewPluginsWatching(true);

            // Apply option to show Plugins Store on startup on main options
            if (Options.DisplayPluginsStoreOnStartup != PluginsStore.Options.Instance.DisplayPluginsStoreOnStartup)
            {
                Options.DisplayPluginsStoreOnStartup = PluginsStore.Options.Instance.DisplayPluginsStoreOnStartup.HasValue && PluginsStore.Options.Instance.DisplayPluginsStoreOnStartup.Value;
            }
        }

        private void pnlNoPluginFound_Resize(object sender, EventArgs e)
        {
            pbOpenPluginsStore.Location = new Point((HomePageTab.Width - pbOpenPluginsStore.Width) / 2, pbOpenPluginsStore.Location.Y);
            llResetSearchFilter.Location = new Point((HomePageTab.Width - llResetSearchFilter.Width) / 2, llResetSearchFilter.Location.Y);
        }

        private void llResetSearchFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tstxtFilterPlugin.Text = string.Empty;
        }

        private void tsbCheckForUpdate_Click(object sender, EventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (s, evt) =>
            {
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var cvc = new GithubVersionChecker(currentVersion, "MsCrmTools", "XrmToolBox");
                cvc.Run();

                evt.Result = cvc;
            };
            worker.RunWorkerCompleted += (s, evt) =>
            {
                GithubVersionChecker cvc = (GithubVersionChecker)evt.Result;
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (!string.IsNullOrEmpty(cvc.Cpi?.Version))
                {
                    if (currentOptions.LastUpdateCheck.Date != DateTime.Now.Date)
                    {
                        Invoke(new Action(() =>
                        {
                            var nvForm = new NewVersionForm(currentVersion, cvc.Cpi.Version, cvc.Cpi.Description,
                                "MsCrmTools", "XrmToolBox", new Uri(cvc.Cpi.PackageUrl));
                            var result = nvForm.ShowDialog(this);
                            if (result == DialogResult.OK)
                            {
                                Close();
                            }
                        }));
                    }
                }
                else
                {
                    MessageBox.Show(this, "No update available!", "Information", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                currentOptions.LastUpdateCheck = DateTime.Now;
                currentOptions.Save();
            };
            worker.RunWorkerAsync();
        }

        #endregion

        #region Message broker

        private void MainForm_MessageBroker(object sender, MessageBusEventArgs message)
        {
            if (!IsMessageValid(sender, message))
            {
                return;
            }

            // Trying to find the tab where plugin is located
            var tab = tabControl1.TabPages.Cast<TabPage>().FirstOrDefault(x => x.Controls[0].GetType().GetTitle() == message.TargetPlugin);

            if (tab != null && !message.NewInstance)
            {
                // Using existing plugin instance, switching to plugin tab
                tabControl1.SelectTab(tabControl1.TabPages.IndexOf(tab));
            }
            else
            {
                // Searching for suitable plugin
                var target = pManager.Plugins.FirstOrDefault(p => p.Metadata.Name == message.TargetPlugin);
                if (target == null)
                {
                    throw new PluginNotFoundException("Plugin {0} was not found", message.TargetPlugin);
                }
                // Displaying plugin and keeping number of the tab where it was opened
                var tabIndex = DisplayPluginControl(target);
                // Getting the tab where plugin was opened
                tab = tabControl1.TabPages[tabIndex];
                // New intance of the plugin was created, even if user did not explicitly asked about this.
                message.NewInstance = true;
            }

            var targetControl = (UserControl)tab.Controls[0];

            if (targetControl is IMessageBusHost)
            {
                ((IMessageBusHost)targetControl).OnIncomingMessage(message);
            }
        }

        private bool IsMessageValid(object sender, MessageBusEventArgs message)
        {
            if (message == null || sender == null || !(sender is UserControl) || !(sender is IXrmToolBoxPluginControl))
            {
                // Error. Possible reasons are:
                // * empty sender
                // * empty message
                // * sender is not UserControl
                // * sender is not XrmToolBox Plugin
                return false;
            }

            var sourceControl = (UserControl)sender;

            if (string.IsNullOrEmpty(message.SourcePlugin))
            {
                message.SourcePlugin = sourceControl.GetType().GetTitle();
            }
            else if (message.SourcePlugin != sourceControl.GetType().GetTitle())
            {
                // For some reason incorrect name was set in Source Plugin field
                return false;
            }

            // Everything went ok
            return true;
        }

        #endregion

        #region Close Tabs/Plugins

        private void CloseAllTabsExceptActiveToolStripMenuItemClick(object sender, EventArgs e)
        {
            RequestCloseTabs(GetPluginPages().Where(p => tabControl1.SelectedTab != p), new PluginCloseInfo(ToolBoxCloseReason.CloseAllExceptActive));
        }

        private void CloseAllTabsToolStripMenuItemClick(object sender, EventArgs e)
        {
            RequestCloseTabs(GetPluginPages(), new PluginCloseInfo(ToolBoxCloseReason.CloseAll));
        }

        private void closeCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.TabIndex != 0)
            {
                RequestCloseTab(tabControl1.TabPages[tabControl1.SelectedTab.TabIndex], new PluginCloseInfo(ToolBoxCloseReason.CloseCurrent));
            }
        }

        /// <summary>
        /// Only to be called from the RequestCloseTab
        /// </summary>
        /// <param name="page"></param>
        private void CloseTab(TabPage page)
        {
            tabControl1.TabPages.Remove(page);
            if (page.Controls.Count == 0)
            {
                return;
            }
            var plugin = page.Controls[0] as UserControl;
            if (plugin == null)
            {
                return;
            }

            plugin.Dispose();
        }

        private IEnumerable<TabPage> GetPluginPages()
        {
            for (var i = tabControl1.TabPages.Count - 1; i > 0; i--)
            {
                yield return tabControl1.TabPages[i];
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            PluginCloseInfo info;

            // Save current form size for future usage
            currentOptions.Size.CurrentSize = Size;
            currentOptions.Size.IsMaximized = (WindowState == FormWindowState.Maximized);
            currentOptions.Save();

            // Warn to close opened plugins
            if (currentOptions.CloseOpenedPluginsSilently)
            {
                foreach (var page in GetPluginPages())
                {
                    info = new PluginCloseInfo(ToolBoxCloseReason.CloseAll);
                    RequestCloseTab(page, info);
                    if (info.Cancel) return;
                }
                return;
            }

            info = new PluginCloseInfo(e.CloseReason);
            RequestCloseTabs(GetPluginPages(), info);
            e.Cancel = info.Cancel;
        }

        private void RequestCloseTab(TabPage page, PluginCloseInfo info)
        {
            info.Silent = currentOptions.CloseEachPluginSilently;
            var plugin = page.GetPlugin();
            plugin.ClosingPlugin(info);
            if (info.Cancel)
            {
                return;
            }
            CloseTab(page);
        }

        private void RequestCloseTabs(IEnumerable<TabPage> pages, PluginCloseInfo info)
        {
            var pagesList = pages.ToList();
            if ((info.FormReason != CloseReason.None ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAll ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAllExceptActive)
                && pagesList.Count > 0)
            {
                info.Cancel = MessageBox.Show(@"Are you sure you want to close " + pagesList.Count + @" tab(s)?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
                if (info.Cancel)
                {
                    return;
                }
            }

            foreach (var page in pagesList)
            {
                RequestCloseTab(page, info);
                if (info.Cancel) return;
            }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            var tabControl = sender as TabControl;
            if (tabControl == null || e.Button != MouseButtons.Middle) { return; }

            var tabs = tabControl.TabPages;
            var tabPage = tabs.Cast<TabPage>()
                .Where((t, i) => tabControl.GetTabRect(i).Contains(e.Location))
                .FirstOrDefault();

            if (tabPage != null && tabControl1.TabPages[0] != tabPage)
            {
                RequestCloseTab(tabPage, new PluginCloseInfo(ToolBoxCloseReason.CloseMiddleClick));
            }
        }

        #endregion Close Tabs/Plugins

        #region Other methods

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                // Focus on plugins filter box on Ctrl+F should work on home screen only
                if (keyData == (Keys.Control | Keys.F))
                {
                    tstxtFilterPlugin.Focus();

                    return true;
                }
            }
            else
            {
                // Close current plugin on Ctrl+Q, Ctrl+W and Ctrl+F4
                if (keyData == (Keys.Control | Keys.Q) ||
                    keyData == (Keys.Control | Keys.W) ||
                    keyData == (Keys.Control | Keys.F4))
                {
                    RequestCloseTab(tabControl1.TabPages[tabControl1.SelectedIndex], new PluginCloseInfo(ToolBoxCloseReason.CloseHotKey));

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private static ScrollBars GetVisibleScrollbars(ScrollableControl ctl)
        {
            if (ctl.HorizontalScroll.Visible)
                return ctl.VerticalScroll.Visible ? ScrollBars.Both : ScrollBars.Horizontal;
            else
                return ctl.VerticalScroll.Visible ? ScrollBars.Vertical : ScrollBars.None;
        }

        private void AdaptPluginControlSize()
        {
            if (GetVisibleScrollbars(pnlPlugins) == ScrollBars.Vertical)
            {
                foreach (var ctrl in pnlPlugins.Controls)
                {
                    if (ctrl is UserControl)
                    {
                        ((UserControl)ctrl).Width = pnlPlugins.Width - 28;
                    }
                }
            }
            else
            {
                foreach (var ctrl in pnlPlugins.Controls)
                {
                    if (ctrl is UserControl)
                    {
                        ((UserControl)ctrl).Width = pnlPlugins.Width - 10;
                    }
                }
            }
        }

        private void ApplyConnectionToTabs()
        {
            var tabs = tabControl1.TabPages.Cast<TabPage>().Where(tab => tab.TabIndex != 0).ToList();

            var tcu = new TabConnectionUpdater(tabs) { StartPosition = FormStartPosition.CenterParent };

            if (tcu.ShowDialog() == DialogResult.OK)
            {
                foreach (TabPage tab in tcu.SelectedTabs)
                {
                    UpdateTabConnection(tab);
                }
            }
        }

        private string ExtractSwitchValue(string key, ref string[] args)
        {
            var name = string.Empty;

            foreach (var arg in args)
            {
                if (arg.StartsWith(key))
                {
                    name = arg.Substring(key.Length);
                }
            }

            return name;
        }

        private void UpdateTabConnection(TabPage tab)
        {
            tab.GetPlugin().UpdateConnection(service, currentConnectionDetail);

            tab.Text = string.Format("{0} ({1})",
                ((Lazy<IXrmToolBoxPlugin, IPluginMetadata>)tab.Tag).Metadata.Name,
                currentConnectionDetail != null
                    ? currentConnectionDetail.ConnectionName
                    : "Not connected");
        }

        #endregion Other methods

        #region Plugins list context menu events

        private void tsmiOpenProjectHomePage_Click(object sender, EventArgs e)
        {
            var plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadata>) selectedPluginModel.Tag;

            var filePath = Assembly.GetAssembly(plugin.Value.GetType()).Location;
            if (File.Exists(filePath))
            {
                var fileName = Path.GetFileName(filePath);

                var package = store.GetPackageByFileName(fileName.ToLower());
                if (package != null && package.Package.ProjectUrl != null)
                {
                    Process.Start(package.Package.ProjectUrl.AbsoluteUri);
                }
                else
                {
                    MessageBox.Show(this,
                        "This plugin is not on the Plugins Store or its Project Url is not defined. Therefore, we cannot lead you to the project page",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tsmiHidePlugin_Click(object sender, EventArgs e)
        {
            var plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadata>)selectedPluginModel.Tag;
            Options.HiddenPlugins.Add(plugin.Metadata.Name);
            ReloadPluginsList();
        }

        private void tsmiUninstallPlugin_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBox.Show(this,
                "Are you sure you want to uninstall this plugin?",
                "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            var plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadata>)selectedPluginModel.Tag;

            var filePath = Assembly.GetAssembly(plugin.Value.GetType()).Location;
            if (File.Exists(filePath))
            {
                var fileName = Path.GetFileName(filePath);

                var package = store.GetPackageByFileName(fileName.ToLower());

                if (package != null)
                {
                    var pds = store.PrepareUninstallPlugins(new List<XtbNuGetPackage> {package});
                    store.PerformUninstallation(pds);
                }
            }
        }

        #endregion

        
    }
}