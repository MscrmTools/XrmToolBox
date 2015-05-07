﻿// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Threading;
using McTools.Xrm.Connection;
using McTools.Xrm.Connection.WinForms;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.Forms;

namespace XrmToolBox
{
    public sealed partial class MainForm : Form
    {
        #region Variables

        private FormHelper fHelper;

        private ConnectionManager cManager;

        private CrmConnectionStatusBar ccsb;

        private IOrganizationService service;

        private ConnectionDetail currentConnectionDetail;

        private PluginManager pManager;

        private Options currentOptions;

        private string currentReleaseNote;

        private Panel infoPanel;

        private string initialConnectionName;

        private string initialPluginName;

        #endregion Variables

        #region Constructor

        public MainForm(string[] args)
        {
            if (args.Length > 0)
            {
                this.initialConnectionName = ExtractSwitchValue("/connection:", ref args);
                this.initialPluginName = ExtractSwitchValue("/plugin:", ref args);
            }

            InitializeComponent();

            ProcessMenuItemsForPlugin();
            MouseWheel += (sender, e) => HomePageTab.Focus();

            currentOptions = Options.Load();
            Text = string.Format("{0} (v{1})", Text, Assembly.GetExecutingAssembly().GetName().Version);

            ManageConnectionControl();
        }

        #endregion Constructor

        #region Connection methods

        private void ManageConnectionControl()
        {
            cManager = ConnectionManager.Instance;
            cManager.RequestPassword += (sender, e) => fHelper.RequestPassword(e.ConnectionDetail);
            cManager.StepChanged += (sender, e) => ccsb.SetMessage(e.CurrentStep);
            cManager.ConnectionSucceed += (sender, e) =>
            {
                Controls.Remove(infoPanel);
                if (infoPanel != null) infoPanel.Dispose();

                currentConnectionDetail = e.ConnectionDetail;
                service = e.OrganizationService;
                ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                ccsb.SetMessage(string.Empty);

                if (e.Parameter != null)
                {
                    var control = e.Parameter as UserControl;
                    if (control != null)
                    {
                        var realUserControl = control;
                        DisplayPluginControl(realUserControl);
                    }
                    else if (e.Parameter.ToString() == "ApplyConnectionToTabs" && tabControl1.TabPages.Count > 1)
                    {
                        ApplyConnectionToTabs();
                    }
                    else
                    {
                        var args = e.Parameter as RequestConnectionEventArgs;
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

                this.StartPluginWithConnection();
            };
            cManager.ConnectionFailed += (sender, e) =>
            {
                this.Invoke(new Action(() =>
                {
                    Controls.Remove(infoPanel);
                    if (infoPanel != null) infoPanel.Dispose();

                    MessageBox.Show(this, e.FailureReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    currentConnectionDetail = null;
                    service = null;
                    ccsb.SetConnectionStatus(false, null);
                    ccsb.SetMessage(e.FailureReason);

                    this.StartPluginWithConnection();
                }));
            };

            fHelper = new FormHelper(this);
            ccsb = new CrmConnectionStatusBar(fHelper) { Dock = DockStyle.Bottom };
            Controls.Add(ccsb);
        }

        private void StartPluginWithConnection()
        {
            if (!string.IsNullOrEmpty(this.initialConnectionName) && !string.IsNullOrEmpty(this.initialPluginName))
            {
                this.StartPluginWithoutConnection();

                // Resetting initial connection name
                this.initialConnectionName = string.Empty;
            }
        }

        private void StartPluginWithoutConnection()
        {
            if (!string.IsNullOrEmpty(this.initialPluginName) && this.pManager != null && this.pManager.PluginsControls != null)
            {
                var pluginControl = this.pManager.PluginsControls.FirstOrDefault(x => ((Type)x.Tag).GetTitle() == this.initialPluginName);
                if (pluginControl != null)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.DisplayPluginControl(pluginControl);
                    }));
                }

                // Resetting initial plugin name
                this.initialPluginName = string.Empty;
            }
        }

        #endregion Connection methods

        #region Tasks to launch during startup

        private Task LaunchVersionCheck()
        {
            return new Task(() =>
            {
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var cvc = new GithubVersionChecker(currentVersion, "MsCrmTools", "XrmToolBox");

                cvc.Run();

                if (cvc.Cpi != null && !string.IsNullOrEmpty(cvc.Cpi.Version))
                {
                    if (currentOptions.LastUpdateCheck.Date != DateTime.Now.Date)
                    {
                        this.Invoke(new Action(() =>
                        {
                            var nvForm = new NewVersionForm(currentVersion, cvc.Cpi.Version, cvc.Cpi.Description, "MsCrmTools", "XrmToolBox");
                            nvForm.ShowDialog(this);
                        }));
                    }
                }

                currentOptions.LastUpdateCheck = DateTime.Now;
                currentOptions.Save();
            });
        }

        private Task LaunchWelcomeDialog()
        {
            return new Task(() => this.Invoke(new Action(() =>
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var blackScreen = new WelcomeDialog(version) { StartPosition = FormStartPosition.CenterScreen };
                blackScreen.ShowDialog(this);
            })));
        }

        private Task launchInitialConnection(ConnectionDetail connectionDetail)
        {
            return new Task(() => ConnectionManager.Instance.ConnectToServer(connectionDetail));
        }

        #endregion

        #region Form events

        private async void MainForm_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;

            pManager = new PluginManager();
            pManager.LoadPlugins();

            this.DisplayPlugins();

            var tasks = new List<Task>
            {
                this.LaunchWelcomeDialog(),
                this.LaunchVersionCheck()
            };

            if (!string.IsNullOrEmpty(this.initialConnectionName))
            {
                var connectionDetail = ConnectionManager.Instance.ConnectionsList.Connections.FirstOrDefault(x => x.ConnectionName == this.initialConnectionName); ;

                if (connectionDetail != null)
                {
                    // If initiall connection is present, connect to given sever is initiated.
                    // After connection try to open intial plugin will be attempted.
                    tasks.Add(this.launchInitialConnection(connectionDetail));
                }
                else
                {
                    // Connection detail was not found, so name provided was incorrect.
                    // But if name of the plugin is set, it should be started
                    if (!string.IsNullOrEmpty(this.initialPluginName))
                    {
                        this.StartPluginWithoutConnection();
                    }
                }
            }
            else if (!string.IsNullOrEmpty(this.initialPluginName))
            {
                // If there is no initial connection, but initial plugin is set, openning plugin
                this.StartPluginWithoutConnection();
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

            this.Opacity = 100;
        }

        private void MainForm_OnCloseTool(object sender, EventArgs e)
        {
            RequestCloseTab((TabPage)((UserControl)sender).Parent, new PluginCloseInfo(ToolBoxCloseReason.PluginRequest));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            AdaptPluginControlSize();
        }

        private void MainForm_OnRequestConnection(object sender, EventArgs e)
        {
            if (fHelper.AskForConnection(e))
            {
                infoPanel = InformationPanel.GetInformationPanel(this, "Connecting...", 340, 120);
            }
        }

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
                var targetModel = pManager.PluginsControls.FirstOrDefault(x => ((Type)x.Tag).GetTitle() == message.TargetPlugin);
                if (targetModel == null)
                {
                    throw new PluginNotFoundException("Plugin {0} was not found", message.TargetPlugin);
                }
                // Displaying plugin and keeping number of the tab where it was opened
                var tabIndex = DisplayPluginControl((UserControl)targetModel);
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
            if (message == null || sender == null || !(sender is UserControl) || !(sender is IMsCrmToolsPluginUserControl))
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
      
        private void TsbConnectClick(object sender, EventArgs e)
        {
            if (fHelper.AskForConnection("ApplyConnectionToTabs"))
            {
                infoPanel = InformationPanel.GetInformationPanel(this, "Connecting...", 340, 120);
            }
        }

        private void tsbManageConnections_Click(object sender, EventArgs e)
        {
            fHelper.DisplayConnectionsList(this);
        }

        private void TsbAboutClick(object sender, EventArgs e)
        {
            var aForm = new AboutForm { StartPosition = FormStartPosition.CenterParent };
            aForm.ShowDialog();
        }

        private void TsbOptionsClick(object sender, EventArgs e)
        {
            var oDialog = new OptionsDialog(currentOptions);
            if (oDialog.ShowDialog(this) == DialogResult.OK)
            {
                bool reinitDisplay = currentOptions.DisplayMostUsedFirst != oDialog.Option.DisplayMostUsedFirst
                                     || currentOptions.MostUsedList.Count != oDialog.Option.MostUsedList.Count
                                     || currentOptions.DisplayLargeIcons != oDialog.Option.DisplayLargeIcons
                                     || !oDialog.Option.HiddenPlugins.SequenceEqual(currentOptions.HiddenPlugins);

                currentOptions = oDialog.Option;

                if (reinitDisplay)
                {
                    pManager.PluginsControls.Clear();
                    tabControl1.SelectedIndex = 0;
                    DisplayPlugins(tstxtFilterPlugin.Text);
                    AdaptPluginControlSize();
                }
            }
        }

        private void PluginClicked(object sender, EventArgs e)
        {

            if (service == null && MessageBox.Show(this, "Do you want to connect to an organization first?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (fHelper.AskForConnection(sender))
                    infoPanel = InformationPanel.GetInformationPanel(this, "Connecting...", 340, 120);
            }
            else
            {
                DisplayPluginControl((UserControl)sender);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessMenuItemsForPlugin();
        }

        private Thread searchThread;

        private void tstxtFilterPlugin_TextChanged(object sender, EventArgs e)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }

            searchThread = new Thread(DisplayPlugins);
            searchThread.Start(tstxtFilterPlugin.Text);
        }

        #endregion Form events
        
        #region Close Tabs/Plugins

        private IEnumerable<TabPage> GetPluginPages()
        {
            for (var i = tabControl1.TabPages.Count - 1; i > 0; i--)
            {
                yield return tabControl1.TabPages[i];
            }
        }

        private void closeCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.TabIndex != 0)
            {
                RequestCloseTab(tabControl1.TabPages[tabControl1.SelectedTab.TabIndex], new PluginCloseInfo(ToolBoxCloseReason.CloseCurrent));
            }
        }

        private void CloseAllTabsToolStripMenuItemClick(object sender, EventArgs e)
        {
            RequestCloseTabs(GetPluginPages(), new PluginCloseInfo(ToolBoxCloseReason.CloseAll));
        }

        private void CloseAllTabsExceptActiveToolStripMenuItemClick(object sender, EventArgs e)
        {
            RequestCloseTabs(GetPluginPages().Where(p => tabControl1.SelectedTab != p), new PluginCloseInfo(ToolBoxCloseReason.CloseAllExceptActive));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save current form size for future usage
            currentOptions.Size.CurrentSize = Size;
            currentOptions.Size.IsMaximized = (WindowState == FormWindowState.Maximized);
            currentOptions.Save();

            // Warn to close opened plugins
            var info = new PluginCloseInfo(e.CloseReason);
            RequestCloseTabs(GetPluginPages(), info);
            e.Cancel = info.Cancel;
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

        private void RequestCloseTab(TabPage page, PluginCloseInfo info)
        {
            var plugin = page.GetPlugin();
            plugin.ClosingPlugin(info);
            if (info.Cancel)
            {
                return;
            }
            CloseTab(page);
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

        #endregion // Close Tabs/Plugins

        #region Other methods

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

        private void ApplyConnectionToTabs()
        {
            var tabs = tabControl1.TabPages.Cast<TabPage>().Where(tab => tab.TabIndex != 0).ToList();

            var tcu = new TabConnectionUpdater(tabs) { StartPosition = FormStartPosition.CenterParent };

            if (tcu.ShowDialog() == DialogResult.OK)
            {
                foreach (TabPage tab in tcu.SelectedTabs)
                {
                    tab.GetPlugin().UpdateConnection(service, currentConnectionDetail);

                    tab.Text = string.Format("{0} ({1})",
                                        tab.Controls[0].GetType().GetTitle(),
                                        currentConnectionDetail != null
                                            ? currentConnectionDetail.ConnectionName
                                            : "Not connected");
                }
            }
        }
     
        private void AdaptPluginControlSize()
        {
            if (GetVisibleScrollbars(HomePageTab) == ScrollBars.Vertical)
            {
                foreach (var ctrl in HomePageTab.Controls)
                {
                    if (ctrl is UserControl)
                    {
                        ((UserControl)ctrl).Width = HomePageTab.Width - 28;
                    }
                }
            }
            else
            {
                foreach (var ctrl in HomePageTab.Controls)
                {
                    if (ctrl is UserControl)
                    {
                        ((UserControl)ctrl).Width = HomePageTab.Width - 10;
                    }
                }
            }
        }

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

        #endregion Other methods
    }

    public static class Extensions
    {
        public static IMsCrmToolsPluginUserControl GetPlugin(this TabPage page)
        {
            return (IMsCrmToolsPluginUserControl)page.Controls[0];
        }

        public static ICodePlexPlugin GetCodePlexPlugin(this TabPage page)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return page.Controls[0] as ICodePlexPlugin;
        }

        public static IGitHubPlugin GetGithubPlugin(this TabPage page)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return page.Controls[0] as IGitHubPlugin;
        }

        public static IPayPalPlugin GetPaypalPlugin(this TabPage page)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return page.Controls[0] as IPayPalPlugin;
        }

        public static string GetTitle(this Type pluginType)
        {
            return ((AssemblyTitleAttribute) GetAssemblyAttribute(pluginType.Assembly, typeof (AssemblyTitleAttribute))).Title;
        }

        public static string GetDescription(this Type pluginType)
        {
            return ((AssemblyDescriptionAttribute)GetAssemblyAttribute(pluginType.Assembly, typeof(AssemblyDescriptionAttribute))).Description;
        }

        public static string GetCompany(this Type pluginType)
        {
            return ((AssemblyCompanyAttribute)GetAssemblyAttribute(pluginType.Assembly, typeof(AssemblyCompanyAttribute))).Company;
        }

        private static object GetAssemblyAttribute(Assembly assembly, Type attributeType)
        {
            return assembly.GetCustomAttributes(attributeType, true)[0];
        }
    }
}

