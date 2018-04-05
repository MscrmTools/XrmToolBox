using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using McTools.Xrm.Connection.WinForms;
using Microsoft.Xrm.Sdk;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI.ThemeVS2015;
using XrmToolBox.AppCode;
using XrmToolBox.Controls;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Forms;
using XrmToolBox.PluginsStore;
using XrmToolBox.New.EventArgs;

namespace XrmToolBox.New
{
    public partial class NewForm : Form
    {
        private readonly PluginsForm pluginsForm;
        private readonly Dictionary<PluginForm, ConnectionDetail> pluginConnections = new Dictionary<PluginForm, ConnectionDetail>();
        private readonly List<PluginControlStatus> pluginControlStatuses = new List<PluginControlStatus>();
        private StartPage startPage;

        private CrmConnectionStatusBar ccsb;
        private ConnectionManager cManager;
        private FormHelper fHelper;
        private IOrganizationService service;
        private ConnectionDetail connectionDetail;
        private IDockContent currentContent;

        private string initialConnectionName;
        private string initialPluginName;

        private IStore store;

        public NewForm(string[] args)
        {
            InitializeComponent();

            Text = $@"{Text} (v{Assembly.GetExecutingAssembly().GetName().Version})";

            // Set drawing optimizations
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            WelcomeDialog.ShowSplashScreen();

            SetTheme();

            // Connection Management
            WelcomeDialog.SetStatus("Loading connection controls...");
            ManageConnectionControl();
            ccsb.MergeConnectionsFiles = Options.Instance.MergeConnectionFiles;

            WelcomeDialog.SetStatus("Loading plugins...");
            try
            {
                pluginsForm = new PluginsForm();
            }
            catch
            {
            }

            if (pluginsForm != null)
            {
                pluginsForm.OpenPluginRequested += PluginsForm_OpenPluginRequested;
                pluginsForm.OpenPluginProjectUrlRequested += PluginsForm_OpenPluginProjectUrlRequested;
                pluginsForm.UninstallPluginRequested += PluginsForm_UninstallPluginRequested;
                pluginsForm.ActionRequested += PluginsForm_ActionRequested;
                if (!Options.Instance.DoNotShowStartPage)
                {
                    WelcomeDialog.SetStatus("Preparing Start page...");

                    ShowStartPage();
                }

                pluginsForm.Show(dpMain, Options.Instance.PluginsListDocking);
                pluginsForm.IsHidden = Options.Instance.PluginsListIsHidden;

                ProcessMenuItemsForPlugin();

                // Restore session management
                if (Options.Instance.RememberSession)
                {
                    if (!string.IsNullOrEmpty(Options.Instance.LastConnection))
                    {
                        initialConnectionName = Options.Instance.LastConnection;
                    }

                    if (!string.IsNullOrEmpty(Options.Instance.LastPlugin))
                    {
                        initialPluginName = Options.Instance.LastPlugin;
                    }
                }

                // Read arguments to detect if a plugin should be displayed automatically
                if (args.Length > 0)
                {
                    initialConnectionName = ExtractSwitchValue("/connection:", ref args);
                    initialPluginName = ExtractSwitchValue("/plugin:", ref args);

                    if (!string.IsNullOrEmpty(initialConnectionName))
                    {
                        pnlConnectLoading.BringToFront();

                        pnlConnectLoading.Visible = true;
                        lblConnecting.Text = string.Format(lblConnecting.Tag.ToString(), initialConnectionName);
                    }
                }
            }
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        #region Form management

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

        private void SetTheme()
        {
            if (Options.Instance.Theme != null)
            {
                switch (Options.Instance.Theme)
                {
                    case "Blue theme":
                        {
                            var theme = new VS2015BlueTheme();
                            dpMain.Theme = theme;
                        }
                        break;

                    case "Light theme":
                        {
                            var theme = new VS2015LightTheme();
                            dpMain.Theme = theme;
                        }
                        break;

                    case "Dark theme":
                        {
                            var theme = new VS2015DarkTheme();
                            dpMain.Theme = theme;
                        }
                        break;
                }
            }
        }

        private void BringToTop()
        {
            //Checks if the method is called from UI thread or not
            if (InvokeRequired)
            {
                Invoke(new Action(BringToTop));
            }
            else
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }
                //Keeps the current topmost status of form
                bool top = TopMost;
                //Brings the form to top
                TopMost = true;
                //Set form's topmost status back to whatever it was
                TopMost = top;
            }
        }

        private async void NewForm_Load(object sender, System.EventArgs e)
        {
            if (!Options.Instance.DoNotShowStartPage && startPage != null)
            {
                startPage.EnsureVisible(dpMain, DockState.Document);
            }

            WebProxyHelper.ApplyProxy();

            var tasks = new List<Task>
            {
                LaunchVersionCheck()
            };

            if (!string.IsNullOrEmpty(initialConnectionName))
            {
                var initialConnectionDetail = ConnectionManager.Instance.ConnectionsList.Connections.FirstOrDefault(x => x.ConnectionName == initialConnectionName);

                if (initialConnectionDetail != null)
                {
                    // If initiall connection is present, connect to given sever is initiated.
                    // After connection try to open intial plugin will be attempted.
                    tasks.Add(LaunchInitialConnection(initialConnectionDetail));
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
            if (Options.Instance.Size.IsMaximized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                Options.Instance.Size.ApplyFormSize(this);
            }

            // Hide & remove Welcome screen
            WelcomeDialog.CloseForm();
            Invoke(new Action(() =>
            {
                Opacity = 100;
                BringToTop();
            }));

            if (!Options.Instance.AllowLogUsage.HasValue)
            {
                Options.Instance.AllowLogUsage = LogUsage.PromptToLog();
                Options.Instance.Save();
            }

            if (Options.Instance.DisplayPluginsStoreOnStartup)
            {
                if (store == null)
                {
                    if (Options.Instance.PluginsStoreUseLegacy ?? false)
                    {
                        store = new Store();
                    }
                    else
                    {
                        store = new StoreFromPortal();
                    }
                }

                if (Options.Instance.DisplayPluginsStoreOnlyIfUpdates)
                {
                    if (store.PluginsCount == 0)
                    {
                        store.LoadNugetPackages();
                    }

                    if (store.HasUpdates)
                    {
                        tsddbTools_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(pluginsStoreToolStripMenuItem));
                    }
                }
                else
                {
                    tsddbTools_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(pluginsStoreToolStripMenuItem));
                }
            }
        }

        private void NewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Options.Instance.Size.Height = Height;
            Options.Instance.Size.CurrentSize = Size;
            Options.Instance.Size.IsMaximized = WindowState == FormWindowState.Maximized;
            Options.Instance.LastConnection = connectionDetail?.ConnectionName;
            Options.Instance.PluginsListDocking = pluginsForm?.DockState ?? DockState.Document;
            Options.Instance.PluginsListIsHidden = pluginsForm?.IsHidden ?? false;

            if (dpMain.ActiveContent is PluginForm pf)
            {
                Options.Instance.LastPlugin = pf.PluginTitle;
            }
            else
            {
                Options.Instance.LastPlugin = "";
            }

            Options.Instance.Save();
        }

        #endregion Form management

        #region Start page

        private void ShowStartPage()
        {
            if (startPage == null || startPage.IsDisposed)
            {
                startPage = new StartPage(pluginsForm.PluginManager);
                startPage.OpenMruPluginRequested += StartPage_OpenMruPluginRequested;
                startPage.OpenConnectionsManagementRequested += (s, evt) => { fHelper.DisplayConnectionsList(this); };
                startPage.OpenPluginsStoreRequested += (s, evt) =>
                {
                    tsddbTools_DropDownItemClicked(s, new ToolStripItemClickedEventArgs(pluginsStoreToolStripMenuItem));
                };
            }
            startPage.Show(dpMain, DockState.Document);
        }

        private void StartPage_OpenMruPluginRequested(object sender, OpenMruPluginEventArgs e)
        {
            var cid = e.Item.ConnectionId;

            if (cid == Guid.Empty)
            {
                initialPluginName = e.Item.PluginName;
                StartPluginWithoutConnection();
                return;
            }

            foreach (var file in ConnectionsList.Instance.Files)
            {
                var cList = CrmConnections.LoadFromFile(file.Path);
                var connection = cList.Connections.FirstOrDefault(c => c.ConnectionId == cid);
                if (connection != null)
                {
                    initialPluginName = e.Item.PluginName;
                    initialConnectionName = e.Item.ConnectionName;

                    if (e.Item.ConnectionName != connectionDetail?.ConnectionName)
                    {
                        var info = new ConnectionParameterInfo();

                        var connectingControl = new ConnectingControl { Anchor = AnchorStyles.None };
                        connectingControl.Left = Width / 2 - connectingControl.Width / 2;
                        connectingControl.Top = Height / 2 - connectingControl.Height / 2;
                        Controls.Add(connectingControl);
                        connectingControl.BringToFront();

                        info.ConnControl = connectingControl;

                        ConnectionManager.Instance.ConnectToServer(connection, info);
                    }
                    else
                    {
                        StartPluginWithConnection();
                    }
                }
            }
        }

        #endregion Start page

        #region Plugins Form events

        private void PluginsForm_ActionRequested(object sender, PluginsListEventArgs e)
        {
            switch (e.Action)
            {
                case PluginsListAction.OpenPluginsStore:
                    tsddbTools_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(pluginsStoreToolStripMenuItem));
                    break;
            }
        }

        private void PluginsForm_OpenPluginProjectUrlRequested(object sender, PluginEventArgs e)
        {
            var filePath = Assembly.GetAssembly(e.PluginControl.GetType()).Location;
            if (File.Exists(filePath))
            {
                var fileName = Path.GetFileName(filePath);

                if (store == null)
                {
                    MessageBox.Show(this,
                        @"The Plugins Store is not initialized so we cannot find the project url",
                        @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string projectUrl = store.GetPluginProjectUrlByFileName(fileName.ToLower());
                if (projectUrl != null)
                {
                    Process.Start(projectUrl);
                }
                else
                {
                    MessageBox.Show(this,
                        @"This plugin is not on the Plugins Store or its Project Url is not defined. Therefore, we cannot lead you to the project page",
                        @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PluginsForm_UninstallPluginRequested(object sender, PluginEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show(this,
                    @"Are you sure you want to uninstall this plugin?",
                    @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            var filePath = Assembly.GetAssembly(e.PluginControl?.GetType() ?? e.Plugin.Value.GetType()).Location;
            if (File.Exists(filePath))
            {
                var fileName = Path.GetFileName(filePath);

                if (store == null)
                {
                    MessageBox.Show(this,
                        @"The Plugins Store is not initialized so we cannot find files to uninstall",
                        @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                store.UninstallByFileName(fileName);
            }
        }

        private void PluginsForm_OpenPluginRequested(object sender, PluginEventArgs e)
        {
            if (e.Plugin.Value is INoConnectionRequired)
            {
                DisplayPluginControl(e.Plugin);
                return;
            }

            if (service == null)
            {
                var result = MessageBox.Show(new Form { TopMost = true }, @"Do you want to connect to an organization first?", $@"Opening {e.Plugin.Metadata.Name}",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ConnectUponApproval(e.Plugin);
                }
                else if (result == DialogResult.No)
                {
                    if (e.Plugin != null)
                    {
                        DisplayPluginControl(e.Plugin);
                    }
                }
            }
            else
            {
                if (e.Plugin != null)
                {
                    DisplayPluginControl(e.Plugin);
                }
            }

            FillPluginsListMenuDisplay();
        }

        private void NewForm_OnCloseTool(object sender, System.EventArgs e)
        {
            if (dpMain.ActiveContent is PluginForm pluginForm)
            {
                RequestCloseTab(pluginForm, new PluginCloseInfo(), Options.Instance.CloseEachPluginSilently);
            }
        }

        private void DisplayPluginControl(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin)
        {
            Guid pluginControlInstanceId = Guid.NewGuid();

            try
            {
                var mruItem = new MostRecentlyUsedItem
                {
                    PluginName = plugin.Metadata.Name,
                    Date = DateTime.Now
                };

                var pluginControl = (UserControl)plugin.Value.GetControl();
                pluginControl.Tag = pluginControlInstanceId;

                // ReSharper disable once SuspiciousTypeConversion.Global
                if (pluginControl is IMessageBusHost host)
                {
                    host.OnOutgoingMessage += MainForm_MessageBroker;
                }

                if (service != null)
                {
                    mruItem.ConnectionId = connectionDetail.ConnectionId ?? Guid.Empty;
                    mruItem.ConnectionName = connectionDetail.ConnectionName;

                    var crmSvcClient = connectionDetail.GetCrmServiceClient();

                    var clonedService = crmSvcClient.OrganizationServiceProxy;
                    var clonedWebClientService = crmSvcClient.OrganizationWebProxyClient;
                    if (clonedService != null)
                    {
                        clonedService.SdkClientVersion = connectionDetail.OrganizationVersion;
                    }
                    if (clonedWebClientService != null)
                    {
                        clonedWebClientService.SdkClientVersion = connectionDetail.OrganizationVersion;
                    }

                    // ReSharper disable once SuspiciousTypeConversion.Global
                    if (pluginControl is IEarlyBoundProxy earlyBoundProxiedControl)
                    {
                        clonedService?.EnableProxyTypes(earlyBoundProxiedControl.GetEarlyBoundProxyAssembly());
                    }

                    if (clonedService != null)
                    {
                        ((IXrmToolBoxPluginControl)pluginControl).UpdateConnection(clonedService, connectionDetail);
                    }
                    else
                    {
                        ((IXrmToolBoxPluginControl)pluginControl).UpdateConnection(clonedWebClientService, connectionDetail);
                    }
                }

                ((IXrmToolBoxPluginControl)pluginControl).OnRequestConnection += NewForm_OnRequestConnection;
                ((IXrmToolBoxPluginControl)pluginControl).OnCloseTool += NewForm_OnCloseTool;
                string name = $"{plugin.Metadata.Name} ({connectionDetail?.ConnectionName ?? "Not connected"})";

                if (pnlConnectLoading.Visible)
                {
                    pnlConnectLoading.SendToBack();
                    pnlConnectLoading.Visible = false;
                }

                var pluginForm = new PluginForm(pluginControl, name);
                pluginForm.Show(dpMain, DockState.Document);
                pluginForm.SendMessageToStatusBar += StatusBarMessenger_SendMessageToStatusBar;
                pluginForm.CloseRequested += PluginForm_CloseRequested;
                pluginForm.FormClosed += (sender, e) =>
                {
                    var pf = (PluginForm)sender;
                    pluginConnections.Remove(pf);

                    if (!pf.IsDisposed && !pf.Disposing)
                        pf.Dispose();

                    FillPluginsListMenuDisplay();
                };

                dpMain.Refresh();

                pluginConnections.Add(pluginForm, connectionDetail);
                if (connectionDetail == null)
                {
                    tssOpenOrg.Visible = false;
                    tsbOpenOrg.Visible = false;
                }

                var pluginInOption = Options.Instance.MostUsedList.FirstOrDefault(i => i.Name == plugin.Value.GetType().FullName);
                if (pluginInOption == null)
                {
                    pluginInOption = new PluginUseCount { Name = plugin.Value.GetType().FullName, Count = 0 };
                    Options.Instance.MostUsedList.Add(pluginInOption);
                }

                pluginInOption.Count++;

                if (Options.Instance.LastAdvertisementDisplay == new DateTime() ||
                    Options.Instance.LastAdvertisementDisplay > DateTime.Now ||
                    Options.Instance.LastAdvertisementDisplay.AddDays(7) < DateTime.Now)
                {
                    bool displayAdvertisement = true;
                    try
                    {
                        var assembly = Assembly.LoadFile(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory +
                                              "\\McTools.StopAdvertisement.dll");
                        {
                            Type type = assembly.GetType("McTools.StopAdvertisement.LicenseManager");
                            if (type != null)
                            {
                                MethodInfo methodInfo = type.GetMethod("IsValid");
                                if (methodInfo != null)
                                {
                                    object classInstance = Activator.CreateInstance(type, null);

                                    if ((bool)methodInfo.Invoke(classInstance, null))
                                    {
                                        displayAdvertisement = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                    }

                    if (displayAdvertisement)
                    {
                        pnlSupport.Visible = true;
                        Options.Instance.LastAdvertisementDisplay = DateTime.Now;
                    }
                }

                var existingItem = MostRecentlyUsedItems.Instance.Items.FirstOrDefault(i
                => i.PluginName == mruItem.PluginName && i.ConnectionId == mruItem.ConnectionId);
                if (existingItem == null)
                {
                    MostRecentlyUsedItems.Instance.Items.Add(mruItem);
                    MostRecentlyUsedItems.Instance.Save();
                }
                else
                {
                    existingItem.Date = DateTime.Now;
                    MostRecentlyUsedItems.Instance.Save();
                }

                if (Options.Instance.AllowLogUsage.HasValue && Options.Instance.AllowLogUsage.Value)
                {
#pragma warning disable CS4014
                    LogUsage.DoLog(plugin);
#pragma warning restore CS4014
                }

                Options.Instance.Save();
            }
            catch (Exception error)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, $@"An error occured when trying to display this plugin: {error.Message}", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (pnlConnectLoading.Visible)
                    {
                        pnlConnectLoading.SendToBack();
                        pnlConnectLoading.Visible = false;
                    }
                }));
            }
        }

        #endregion Plugins Form events

        #region Connection methods

        private void NewForm_OnRequestConnection(object sender, System.EventArgs e)
        {
            ConnectUponApproval(e is RequestConnectionEventArgs ? e : sender);
        }

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
                    Controls.Remove(parameter.ConnControl);
                    parameter.ConnControl.Dispose();
                }

                connectionDetail = e.ConnectionDetail;
                service = e.OrganizationService;

                Invoke(new Action(() =>
                {
                    ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                    ccsb.SetMessage(string.Empty);

                    if (parameter != null)
                    {
                        var us = parameter.ConnectionParmater as UserControl;
                        var p = parameter.ConnectionParmater as Lazy<IXrmToolBoxPlugin, IPluginMetadata>;
                        var rcea = parameter.ConnectionParmater as RequestConnectionEventArgs;

                        if (us != null)
                        {
                            if (!(us.Tag is Lazy<IXrmToolBoxPlugin, IPluginMetadata> pluginModel))
                            {
                                // Actual Plugin was passed, Just update the plugin's Tab.
                                UpdateTabConnection((PluginForm)us.Parent);
                            }
                            else
                            {
                                DisplayPluginControl(pluginModel);
                            }
                        }
                        else if (p != null)
                        {
                            DisplayPluginControl(p);
                        }
                        else if (parameter.ConnectionParmater?.ToString() == "ApplyConnectionToTabs" && dpMain.Contents.OfType<PluginForm>().Any())
                        {
                            ApplyConnectionToTabs();
                        }
                        else if (rcea != null)
                        {
                            var userControl = (UserControl)rcea.Control;

                            rcea.Control.UpdateConnection(e.OrganizationService, connectionDetail, rcea.ActionName, rcea.Parameter);

                            var indexOfParenthesis = userControl.Parent.Text?.IndexOf("(") ?? -1;
                            var pluginName = userControl.Parent.Text?.Substring(0, indexOfParenthesis - 1) ?? "N/A";

                            userControl.Parent.Text = $@"{pluginName} ({e.ConnectionDetail.ConnectionName})";
                        }
                    }
                    else if (dpMain.Contents.OfType<PluginForm>().Count() > 1)
                    {
                        ApplyConnectionToTabs();
                    }

                    StartPluginWithConnection();

                    tssOpenOrg.Visible = true;
                    tsbOpenOrg.Visible = true;
                }));
            };
            cManager.ConnectionFailed += (sender, e) =>
            {
                Invoke(new Action(() =>
                {
                    if (e.Parameter is ConnectionParameterInfo param && param.ConnControl != null)
                    {
                        Controls.Remove(param.ConnControl);
                        param.ConnControl.Dispose();
                    }

                    MessageBox.Show(this, e.FailureReason, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    connectionDetail = null;
                    service = null;
                    ccsb.SetConnectionStatus(false, null);
                    ccsb.SetMessage(e.FailureReason);

                    StartPluginWithConnection();
                }));
            };

            fHelper = new FormHelper(this);
            ccsb = new CrmConnectionStatusBar(fHelper) { Dock = DockStyle.Bottom };
            Controls.Add(ccsb);
            Controls.SetChildIndex(ccsb, Controls.Count - 1);
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
            if (!string.IsNullOrEmpty(initialPluginName))
            {
                pluginsForm.OpenPlugin(initialPluginName);

                // Resetting initial plugin name
                initialPluginName = string.Empty;
            }
        }

        private void ApplyConnectionToTabs()
        {
            var pluginForms = dpMain.Contents.OfType<PluginForm>().ToList();

            var tcu = new TabConnectionUpdater(pluginForms) { StartPosition = FormStartPosition.CenterParent };

            if (tcu.ShowDialog(this) == DialogResult.OK)
            {
                foreach (PluginForm form in tcu.SelectedPluginForms)
                {
                    UpdateTabConnection(form);
                }

                if (tcu.SelectedPluginForms.Any())
                {
                    FillPluginsListMenuDisplay();
                }
            }
        }

        private void UpdateTabConnection(PluginForm pluginForm, string actionName = "", object parameter = null)
        {
            var indexOfParenthesis = pluginForm.Text?.IndexOf("(") ?? -1;
            var pluginName = pluginForm.Text?.Substring(0, indexOfParenthesis - 1) ?? "N/A";

            if (pluginConnections.ContainsKey(pluginForm))
                pluginConnections[pluginForm] = connectionDetail;

            pluginForm.UpdateConnection(service, connectionDetail, actionName, parameter);
            pluginForm.Text = $@"{pluginName} ({connectionDetail?.ConnectionName ?? "Not connected"})";
        }

        private void ConnectUponApproval(object connectionParameter)
        {
            var info = new ConnectionParameterInfo
            {
                ConnectionParmater = connectionParameter
            };

            fHelper.AskForConnection(info, () =>
            {
                var connectingControl = new ConnectingControl { Anchor = AnchorStyles.None };
                connectingControl.Left = Width / 2 - connectingControl.Width / 2;
                connectingControl.Top = Height / 2 - connectingControl.Height / 2;
                Controls.Add(connectingControl);
                connectingControl.BringToFront();

                info.ConnControl = connectingControl;
            });
        }

        private Task LaunchInitialConnection(ConnectionDetail detail)
        {
            return new Task(() => ConnectionManager.Instance.ConnectToServer(detail));
        }

        private void tsbConnect_Click(object sender, System.EventArgs e)
        {
            ConnectUponApproval("ApplyConnectionToTabs");
        }

        #endregion Connection methods

        #region Message broker

        private void MainForm_MessageBroker(object sender, MessageBusEventArgs message)
        {
            if (!IsMessageValid(sender, message))
            {
                return;
            }

            var content = dpMain.Contents.FirstOrDefault(c => (c as PluginForm)?.PluginTitle == message.TargetPlugin);
            if (content != null && !message.NewInstance)
            {
                // TODO Mettre au premier plan
            }
            else
            {
                pluginsForm.OpenPlugin(message.TargetPlugin);
                MainForm_MessageBroker(sender, message);
                return;
            }

           ((PluginForm)content).SendIncomingBrokerMessage(message);
        }

        private bool IsMessageValid(object sender, MessageBusEventArgs message)
        {
            if (message == null || !(sender is UserControl) || !(sender is IXrmToolBoxPluginControl))
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

        #endregion Message broker

        #region StatusBar Messenger

        private void StatusBarMessenger_SendMessageToStatusBar(object sender, StatusBarMessageEventArgs e)
        {
            var currentPlugin = pluginControlStatuses.FirstOrDefault(pcs => pcs.Control == sender);
            if (currentPlugin == null)
            {
                pluginControlStatuses.Add(new PluginControlStatus(
                    (IXrmToolBoxPluginControl)sender,
                    e.Progress,
                    e.Message
                ));
            }
            else
            {
                currentPlugin.Percentage = e.Progress;
                currentPlugin.Message = e.Message;
            }

            void Mi()
            {
                if (dpMain.ActiveContent is PluginForm pf)
                {
                    if (pf.Control == sender)
                    {
                        ccsb.SetMessage(e.Message);
                        ccsb.SetProgress(e.Progress);
                    }
                }
                else
                {
                    ccsb.SetMessage(null);
                    ccsb.SetProgress(null);
                }
            }

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)Mi);
            }
            else
            {
                Mi();
            }
        }

        #endregion StatusBar Messenger

        #region Check for update

        private Task LaunchVersionCheck()
        {
            return new Task(() =>
            {
                if (Options.Instance.DoNotCheckForUpdates)
                {
                    return;
                }

                WelcomeDialog.SetStatus("Checking for XrmToolBox update...");
                // blackScreen.SetWorkingMessage("Checking for XrmToolBox update...");

                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                try
                {
                    var request = WebRequest.CreateHttp("https://www.xrmtoolbox.com/_odata/releases");
                    var response = request.GetResponse();
                    Releases releases = null;
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        if (dataStream != null)
                        {
                            var serializer = new DataContractJsonSerializer(typeof(Releases),
                                new DataContractJsonSerializerSettings
                                {
                                    UseSimpleDictionaryFormat = true,
                                    DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss",
                                        new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd'T'HH:mm:ss" })
                                });

                            releases = (Releases)serializer.ReadObject(dataStream);
                        }
                    }

                    if (releases != null)
                    {
                        var lastReleaseVersion = releases.Items.Max(i => new Version(i.Version));
                        if (lastReleaseVersion > currentVersion &&
                            Options.Instance.LastUpdateCheck.Date != DateTime.Now.Date)
                        {
                            var release =
                                releases.Items.FirstOrDefault(r => r.Version == lastReleaseVersion.ToString());

                            Invoke(new Action(() =>
                            {
                                var nvForm = new NewVersionForm(release?.Version, new Uri(release?.DownloadUrl ?? "https://www.xrmtoolbox.com"));
                                nvForm.Show(dpMain, DockState.Document);
                            }));
                        }
                    }

                    Options.Instance.LastUpdateCheck = DateTime.Now;
                    Options.Instance.Save();
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch
                {
                }
            });
        }

        #endregion Check for update

        #region Change of active content

        private void dpMain_ActiveContentChanged(object sender, System.EventArgs e)
        {
            ApplyActiveContentDisplay();
        }

        private void dpMain_ActiveDocumentChanged(object sender, System.EventArgs e)
        {
            ApplyActiveContentDisplay();
        }

        private void dpMain_ActivePaneChanged(object sender, System.EventArgs e)
        {
            ApplyActiveContentDisplay();
        }

        private void dpMain_DocumentDragged(object sender, System.EventArgs e)
        {
            ApplyActiveContentDisplay();
        }

        private void ApplyActiveContentDisplay()
        {
            if (dpMain.ActiveContent != null && currentContent != dpMain.ActiveContent)
            {
                currentContent = dpMain.ActiveContent;

                ProcessMenuItemsForPlugin();

                if (dpMain.ActiveContent is StartPage sp)
                {
                    sp.LoadMru();
                }
            }
        }

        #endregion Change of active content

        #region Menu Toolbar

        private void tsddbTools_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == pluginsStoreToolStripMenuItem)
            {
                if (pluginsForm == null)
                {
                    return;
                }

                // If the options were not initialized, it means we are using the
                // new plugins store for the first time. Copy values from main
                // options file
                if (!PluginsStore.Options.Instance.IsInitialized)
                {
                    PluginsStore.Options.Instance.DisplayPluginsStoreOnStartup = Options.Instance.DisplayPluginsStoreOnStartup;
                    PluginsStore.Options.Instance.PluginsStoreShowInstalled = Options.Instance.PluginsStoreShowInstalled;
                    PluginsStore.Options.Instance.PluginsStoreShowIncompatible = Options.Instance.PluginsStoreShowIncompatible;
                    PluginsStore.Options.Instance.PluginsStoreShowNew = Options.Instance.PluginsStoreShowNew;
                    PluginsStore.Options.Instance.PluginsStoreShowUpdates = Options.Instance.PluginsStoreShowUpdates;
                    PluginsStore.Options.Instance.UseLegacy = false;
                    PluginsStore.Options.Instance.IsInitialized = true;
                }

                IStoreForm form;

                if (Options.Instance.PluginsStoreUseLegacy ?? false)
                {
                    form = new StoreForm();
                }
                else
                {
                    form = new StoreFormFromPortal();
                }

                // Avoid scanning for new files during Plugins Store usage.
                pluginsForm.PluginManager.IsWatchingForNewPlugins = false;
                ((Form)form).ShowDialog(this);
                pluginsForm.PluginManager.IsWatchingForNewPlugins = true;
                pluginsForm.ReloadPluginsList();

                Options.Instance.PluginsStoreUseLegacy = PluginsStore.Options.Instance.UseLegacy;

                // Apply option to show Plugins Store on startup on main options
                if (Options.Instance.DisplayPluginsStoreOnStartup != PluginsStore.Options.Instance.DisplayPluginsStoreOnStartup)
                {
                    Options.Instance.DisplayPluginsStoreOnStartup = PluginsStore.Options.Instance.DisplayPluginsStoreOnStartup ?? false;
                }
            }
            else if (e.ClickedItem == manageConnectionsToolStripMenuItem)
            {
                fHelper.DisplayConnectionsList(this);
            }
            else if (e.ClickedItem == settingsToolStripMenuItem)
            {
                var oDialog = new OptionsDialog(Options.Instance, pluginsForm.PluginManager);
                if (oDialog.ShowDialog(this) == DialogResult.OK)
                {
                    bool reinitDisplay = Options.Instance.DisplayMostUsedFirst != oDialog.Option.DisplayMostUsedFirst
                                         || Options.Instance.MostUsedList.Count != oDialog.Option.MostUsedList.Count
                                         || Options.Instance.DisplayLargeIcons != oDialog.Option.DisplayLargeIcons
                                         || !oDialog.Option.HiddenPlugins.SequenceEqual(Options.Instance.HiddenPlugins)
                                         || Options.Instance.DisplayOrder != oDialog.Option.DisplayOrder;

                    if (Options.Instance.DoNotRememberPluginsWithoutConnection != oDialog.Option.DoNotRememberPluginsWithoutConnection
                        && oDialog.Option.DoNotRememberPluginsWithoutConnection)
                    {
                        MostRecentlyUsedItems.Instance.RemovePluginsWithNoConnection();
                        MostRecentlyUsedItems.Instance.Save();
                    }

                    if (Options.Instance.MruItemsToDisplay != oDialog.Option.MruItemsToDisplay)
                    {
                        MostRecentlyUsedItems.Instance.Save();
                    }

                    if (Options.Instance.Theme != oDialog.Option.Theme)
                    {
                        SetTheme();
                    }

                    Options.Instance.Replace(oDialog.Option);

                    if (reinitDisplay)
                    {
                        pluginsForm.ReloadPluginsList();
                    }

                    cManager.ReuseConnections = Options.Instance.ReuseConnections;
                }
            }
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (s, evt) =>
            {
                var request = WebRequest.CreateHttp("https://www.xrmtoolbox.com/_odata/releases");
                var response = request.GetResponse();
                using (Stream dataStream = response.GetResponseStream())
                {
                    if (dataStream != null)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(Releases),
                            new DataContractJsonSerializerSettings
                            {
                                UseSimpleDictionaryFormat = true,
                                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss", new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd'T'HH:mm:ss" })
                            });

                        evt.Result = (Releases)serializer.ReadObject(dataStream);
                    }
                }
            };
            worker.RunWorkerCompleted += (s, evt) =>
            {
                if (evt.Result is Releases releases)
                {
                    var lastReleaseVersion = releases.Items.Max(i => new Version(i.Version));
                    var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                    if (lastReleaseVersion > currentVersion)
                    {
                        var release = releases.Items.FirstOrDefault(r => r.Version == lastReleaseVersion.ToString());

                        Invoke(new Action(() =>
                        {
                            var nvForm = new NewVersionForm(release?.Version, new Uri(release?.DownloadUrl ?? "about:_blank"));
                            nvForm.Show(dpMain, DockState.Document);
                        }));
                    }
                    else
                    {
                        MessageBox.Show(this, @"No update available!", @"Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(this, @"No update information found!", @"Information", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                Options.Instance.LastUpdateCheck = DateTime.Now;
                Options.Instance.Save();
            };
            worker.RunWorkerAsync();
        }

        private void aboutXrmToolBoxToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var aForm = new WelcomeDialog(false) { StartPosition = FormStartPosition.CenterParent };
            aForm.ShowDialog(this);
        }

        private void tsbOpenOrg_Click(object sender, System.EventArgs e)
        {
            var activeContent = dpMain.ActiveContent as PluginForm;
            var url = string.Empty;
            if (activeContent == null)
            {
                url = connectionDetail.WebApplicationUrl;
            }
            else if (pluginConnections.ContainsKey(activeContent) && pluginConnections[activeContent] != null)
            {
                url = pluginConnections[activeContent].WebApplicationUrl;
            }

            if (url.Length > 0)
            {
                Process.Start(url);
            }
        }

        #region Document management

        private void tsbManageWindows_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == closeAllWindowsToolStripMenuItem)
            {
                RequestCloseTabs(dpMain.Contents.OfType<PluginForm>(), new PluginCloseInfo(ToolBoxCloseReason.CloseAll));
            }
            else if (e.ClickedItem == closeAllWindowsExceptActiveToolStripMenuItem)
            {
                RequestCloseTabs(dpMain.Contents.OfType<PluginForm>().Where(p => dpMain.ActiveContent != p), new PluginCloseInfo(ToolBoxCloseReason.CloseAllExceptActive));
            }
            else if (e.ClickedItem == closeCurrentWindowToolStripMenuItem)
            {
                if (dpMain.ActiveContent is PluginForm p)
                {
                    RequestCloseTab(p, new PluginCloseInfo(ToolBoxCloseReason.CloseCurrent));
                }
            }
            else if (e.ClickedItem == tsmiShowStartPage)
            {
                if (dpMain.Contents.OfType<StartPage>().Any())
                {
                    startPage.EnsureVisible(dpMain, DockState.Document);
                }
                else
                {
                    ShowStartPage();
                }
            }
            else if (e.ClickedItem.Tag is PluginForm pf)
            {
                pf.EnsureVisible(dpMain, DockState.Document);
            }
        }

        private void FillPluginsListMenuDisplay()
        {
            var staticItems = tsbManageWindows.DropDownItems.Cast<ToolStripItem>().Take(5).ToList();

            tsbManageWindows.DropDownItems.Clear();

            var pluginsPages = dpMain.Contents.OfType<PluginForm>().ToList().OrderBy(pf => pf.Text).ToList();

            tsbManageWindows.DropDownItems.AddRange(staticItems.ToArray());

            if (pluginsPages.Any())
            {
                tsbManageWindows.DropDownItems.Add(new ToolStripSeparator());
            }

            foreach (var pluginPage in pluginsPages)
            {
                tsbManageWindows.DropDownItems.Add(new ToolStripMenuItem
                {
                    Text = pluginPage.Text,
                    Tag = pluginPage
                });
            }
        }

        private void RequestCloseTab(PluginForm content, PluginCloseInfo info, bool forceSilent = false)
        {
            info.Silent = Options.Instance.CloseEachPluginSilently || forceSilent;
            var plugin = content.Control;
            plugin?.ClosingPlugin(info);
            if (info.Cancel)
            {
                return;
            }

            content.CloseRequested -= PluginForm_CloseRequested;
            pluginConnections.Remove(content);
            CloseTab(content);
        }

        private void RequestCloseTabs(IEnumerable<PluginForm> pages, PluginCloseInfo info)
        {
            var pagesList = pages.ToList();
            if ((info.FormReason != CloseReason.None ||
                 info.ToolBoxReason == ToolBoxCloseReason.CloseAll ||
                 info.ToolBoxReason == ToolBoxCloseReason.CloseAllExceptActive)
                && pagesList.Count > 0
                && !Options.Instance.CloseEachPluginSilently)
            {
                info.Cancel = MessageBox.Show(@"Are you sure you want to close " + pagesList.Count + @" tab(s)?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
                if (info.Cancel)
                {
                    return;
                }
            }

            foreach (var page in pagesList)
            {
                RequestCloseTab(page, info, true);
                if (info.Cancel) return;
            }
        }

        /// <summary>
        /// Only to be called from the RequestCloseTab
        /// </summary>
        /// <param name="page"></param>
        private void CloseTab(PluginForm page)
        {
            ((UserControl)page.Control)?.Dispose();
            page.Close();
        }

        private void PluginForm_CloseRequested(object sender, System.EventArgs e)
        {
            RequestCloseTab((PluginForm)sender, new PluginCloseInfo(ToolBoxCloseReason.CloseCurrent));
        }

        #endregion Document management

        #endregion Menu Toolbar

        #region Key Shortcuts

        private void NewForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.C)
            {
                tsbConnect_Click(null, null);
            }
            else
            {
                ((dpMain.ActiveContent as PluginForm)?.Control as IShortcutReceiver)?.ReceiveKeyDownShortcut(e);
            }
        }

        private void NewForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            ((dpMain.ActiveContent as PluginForm)?.Control as IShortcutReceiver)?.ReceiveKeyPressShortcut(e);
        }

        private void NewForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.Control && e.Shift && e.KeyCode == Keys.C))
            {
                ((dpMain.ActiveContent as PluginForm)?.Control as IShortcutReceiver)?.ReceiveKeyUpShortcut(e);
            }
        }

        private void NewForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!(e.Control && e.Shift && e.KeyCode == Keys.C))
            {
                ((dpMain.ActiveContent as PluginForm)?.Control as IShortcutReceiver)?.ReceivePreviewKeyDownShortcut(e);
            }
        }

        #endregion Key Shortcuts

        #region Support panel

        private void llDismiss_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlSupport.Visible = false;
        }

        private void llDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://mscrmtools.blogspot.fr/p/xrmtoolbox-sponsoring.html");
        }

        #endregion Support panel
    }
}