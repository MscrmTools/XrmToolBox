using McTools.Xrm.Connection;
using McTools.Xrm.Connection.WinForms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Announcement;
using XrmToolBox.AppCode;
using XrmToolBox.Controls;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Forms;
using XrmToolBox.New.EventArgs;
using XrmToolBox.ToolLibrary.Forms;

namespace XrmToolBox.New
{
    public partial class NewForm : Form
    {
        private const string AiEndpoint = "https://dc.services.visualstudio.com/v2/track";
        private const string AiKey = "77a2080e-f82c-4b2f-bb77-eb407236b729";
        private readonly Dictionary<PluginForm, ConnectionDetail> pluginConnections = new Dictionary<PluginForm, ConnectionDetail>();
        private readonly List<PluginControlStatus> pluginControlStatuses = new List<PluginControlStatus>();
        private readonly IToolsForm pluginsForm;
        private readonly string windowTitleSuffix;
        private AppInsights ai = new AppInsights(new AiConfig(AiEndpoint, AiKey));
        private CrmConnectionStatusBar ccsb;
        private ConnectionManager cManager;
        private ConnectionDetail connectionDetail;
        private IDockContent currentContent;
        private FormHelper fHelper;
        private string initialConnectionName;
        private string initialData;
        private string initialPluginName;
        private ToolLibraryForm libraryForm;
        private int numberOfConnectionReceived;
        private IOrganizationService service;
        private SettingsForm settingsForm;
        private StartPage startPage;
        private IToolLibrary store;
        private ToolTip toolTip = new ToolTip();

        public NewForm(string[] args)
        {
            InitializeComponent();

            ai.WriteEvent("XrmToolBox-Start");

            Text = windowTitleSuffix = $@"{Text} (v{Assembly.GetExecutingAssembly().GetName().Version})";

            // Set drawing optimizations
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            WelcomeDialog.ShowSplashScreen();

            SetTheme();
            dpMain.Theme.Extender.FloatWindowFactory = new CustomFloatWindowFactory();

            // Connection Management
            WelcomeDialog.SetStatus("Loading connection controls...");
            ManageConnectionControl();
            ccsb.MergeConnectionsFiles = Options.Instance.MergeConnectionFiles;

            WelcomeDialog.SetStatus("Loading tools...");
            if (Options.Instance.UseLegacyToolsList)
                pluginsForm = new PluginsForm();
            else pluginsForm = new PluginsForm2();

            if (pluginsForm != null)
            {
                if (!Options.Instance.DoNotShowStartPage)
                {
                    WelcomeDialog.SetStatus("Preparing Start page...");

                    ShowStartPage();
                }

                pluginsForm.OpenPluginRequested += PluginsForm_OpenPluginRequested;
                pluginsForm.OpenPluginProjectUrlRequested += PluginsForm_OpenPluginProjectUrlRequested;
                pluginsForm.UninstallPluginRequested += PluginsForm_UninstallPluginRequested;
                pluginsForm.ActionRequested += PluginsForm_ActionRequested;
                ((DockContent)pluginsForm).Show(dpMain, Options.Instance.PluginsListDocking);
                ((DockContent)pluginsForm).IsHidden = Options.Instance.PluginsListIsHidden;

                ProcessMenuItemsForPlugin2();

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
                    var protocolArg = args.FirstOrDefault(a => a.Contains("xrmtoolbox:"));
                    if (protocolArg != null)
                    {
                        var protocolArgs = HttpUtility.UrlDecode(protocolArg.Split(':')[1]);

                        var rg = new Regex(@"\/([a-zA-Z]*)\:\""(.*?)\""", RegexOptions.Multiline | RegexOptions.Singleline);

                        var matches = rg.Matches(protocolArgs);

                        foreach (Match match in matches)
                        {
                            if (match.Groups.Count == 3)
                            {
                                if (match.Groups[1].Value == "plugin")
                                {
                                    initialPluginName = match.Groups[2].Value;
                                }

                                if (match.Groups[1].Value == "connection")
                                {
                                    initialConnectionName = match.Groups[2].Value;
                                }

                                if (match.Groups[1].Value == "data")
                                {
                                    initialData = match.Groups[2].Value;
                                }
                            }
                        }
                    }
                    else
                    {
                        initialConnectionName = ExtractSwitchValue("/connection:", ref args);
                        initialPluginName = ExtractSwitchValue("/plugin:", ref args);
                    }
                }
            }

            Options.Instance.OnSettingsChanged += Instance_OnSettingsChanged;
        }

        public override sealed string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void CheckForEarlyBoundEntities()
        {
            var assemblies = (from a in AppDomain.CurrentDomain.GetAssemblies()
                              where a.CustomAttributes.Any(ca => ca.AttributeType == typeof(ProxyTypesAssemblyAttribute))
                                    && !a.GetName().Name.StartsWith("Microsoft.")
                              select a).ToList();

            if (assemblies.Any())
            {
                string message = $@"The following files are not respecting XrmToolBox development best practices:

{string.Join(Environment.NewLine, assemblies.Select(a => "- " + a.GetName().Name))}

They can impact the behavior of XrmToolBox or its tools.

We recommend that you remove the corresponding files from XrmToolBox Plugins folder";

                MessageBox.Show(this, message, @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void Instance_OnSettingsChanged(object sender, SettingsPropertyEventArgs e)
        {
            if (e.PropertyName == nameof(Options.Instance.ConnectionControlsAllowPreReleaseUpdates))
            {
                store.AllowConnectionControlPreRelease = (bool)e.Value;
                if (!store.AllowConnectionControlPreRelease)
                {
                    var message =
                                    @"You asked to not use Connection Controls pre release anymore.

Would you like to reinstall last stable release of connection controls?";

                    if (MessageBox.Show(this, message, @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        await store.IsConnectionControlsUpdateAvailable(Options.Instance.ConnectionControlsVersion);
                        var ccSettings = await store.PrepareConnectionControlsUpdate(this, false);//.GetAwaiter().GetResult();

                        Options.Instance.ConnectionControlsVersion = ccSettings.Version;
                        Options.Instance.Save();

                        if (ccSettings.RestartNow)
                        {
                            Application.Restart();
                        }
                    }
                }
                else
                {
                    await CheckForConnectionControlsUpdate(true);
                }
            }
            else if (e.PropertyName == nameof(Options.Instance.DoNotRememberPluginsWithoutConnection))
            {
                MostRecentlyUsedItems.Instance.RemovePluginsWithNoConnection();
                MostRecentlyUsedItems.Instance.Save();
            }
            else if (e.PropertyName == nameof(Options.Instance.MruItemsToDisplay))
            {
                MostRecentlyUsedItems.Instance.Save();
            }
            else if (e.PropertyName == nameof(Options.Instance.Theme))
            {
                SetTheme();
            }
            else if (e.PropertyName == nameof(Options.Instance.ReuseConnections))
            {
                cManager.ReuseConnections = Options.Instance.ReuseConnections;
            }
        }

        #region Form management

        private void BringToTop()
        {
            if (!Options.Instance.BringToTop) return;

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

        private async Task CheckForConnectionControlsUpdate(bool force = false)
        {
            if (store == null)
            {
                if (!await LoadStore()) return;
            }

            IConnectionControlUpdateSettings result = await store.IsConnectionControlsUpdateAvailable(Options.Instance.ConnectionControlsVersion);

            if (result.NewVersion)
            {
                Options.Instance.ConnectionControlsVersion = result.Version;
                Options.Instance.Save();

                if (force || !Options.Instance.PluginsUpdateSkip.Any(
                    x => x.Name == "McTools.Xrm.ConnectionControls"
                         && x.Version == result.Version
                         && x.Date > DateTime.Now))
                {
                    using (var dialog = new NewConnectionVersion(result.Version, result.ReleaseNotes))
                    {
                        if (dialog.ShowDialog(this) == DialogResult.Yes)
                        {
                            result = await store.PrepareConnectionControlsUpdate(this, dialog.OnNextRestart);

                            Options.Instance.ConnectionControlsVersion = result.Version;
                            Options.Instance.Save();

                            if (result.RestartNow)
                            {
                                Application.Restart();
                            }
                        }
                        else
                        {
                            UpdatePluginUpdateSkip("McTools.Xrm.ConnectionControls", result.Version, dialog.IsVersionSkipped,
                                dialog.NumberOfDaysToSkip);
                        }
                    }
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

        private async Task<bool> LoadStore()
        {
            try
            {
                ItSecurityChecker isc = new ItSecurityChecker();
                isc.LoadRepositories();
                store = new ToolLibrary.ToolLibrary(Options.Instance, isc.Repositories);
                store.AllowConnectionControlPreRelease = Options.Instance.ConnectionControlsAllowPreReleaseUpdates;
                ((PluginsForm2)pluginsForm).Store = (ToolLibrary.ToolLibrary)store;

                if (!((ToolLibrary.ToolLibrary)store).HasConnectivityToXrmToolBoxPortal)
                {
                    throw new Exception("Unable to connect to load tools. Please check your network settings");
                }

                return true;
            }
            catch (Exception error)
            {
                toolTip.SetToolTip(pbToolLibraryError, error.Message);
                pnlNoToolLibraryAccess.Visible = true;

                return false;
            }
        }

        private void NewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var pci = new PluginCloseInfo(ToolBoxCloseReason.CloseAll);
            RequestCloseTabs(dpMain.Contents.OfType<PluginForm>(), pci);
            e.Cancel = pci.Cancel;

            AnnouncementSettings.Instance.LastShownDate = DateTime.Now;
            AnnouncementSettings.Instance.Save();

            Options.Instance.Size.Height = Height;
            Options.Instance.Size.CurrentSize = Size;
            Options.Instance.Size.IsMaximized = WindowState == FormWindowState.Maximized;
            Options.Instance.LastConnection = connectionDetail?.ConnectionName;
            Options.Instance.PluginsListDocking = ((DockContent)pluginsForm)?.DockState ?? DockState.Document;
            Options.Instance.PluginsListIsHidden = ((DockContent)pluginsForm)?.IsHidden ?? false;

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

        private async void NewForm_Load(object sender, System.EventArgs e)
        {
            if (!Options.Instance.DoNotShowStartPage && startPage != null)
            {
                startPage.EnsureVisible(dpMain, DockState.Document);
            }

            if (Options.Instance.DisplayToolsListFirst)
            {
                ((DockContent)pluginsForm).Show(dpMain, DockState.Document);
            }

            WebProxyHelper.ApplyProxy();

            await LoadStore();

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
                Invoke(new Action(() =>
                {
                    WindowState = FormWindowState.Maximized;
                }));
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

                CheckForEarlyBoundEntities();
            }));

            try
            {
                await CheckForConnectionControlsUpdate();

                if (store.PluginsCount == 0)
                {
                    await store.LoadTools(false);
                }

                if (Options.Instance.ShowPluginUpdatesPanelAtStartup)
                {
                    int count = store.PluginsUpdatesCount;

                    pnlPluginsUpdate.Visible = count > 0;
                    if (count > 0)
                    {
                        lblPluginsUpdateAvailable.Text = string.Format(lblPluginsUpdateAvailable.Tag.ToString(),
                            count,
                            count > 1 ? "s" : "",
                            count > 1 ? "are" : "is");
                    }
                }

                if (Options.Instance.DisplayPluginsStoreOnStartup)
                {
                    if (Options.Instance.DisplayPluginsStoreOnlyIfUpdates)
                    {
                        if (store.PluginsUpdatesCount > 0)
                        {
                            tsddbTools_DropDownItemClicked(sender,
                            new ToolStripItemClickedEventArgs(pluginsStoreToolStripMenuItem));
                        }
                    }
                    else
                    {
                        tsddbTools_DropDownItemClicked(sender,
                            new ToolStripItemClickedEventArgs(pluginsStoreToolStripMenuItem));
                    }
                }

                // Prepare Categories
                PrepareCategories();
            }
            catch (Exception error)
            {
                // We avoid to make XrmToolBox crash if querying web services fail
                pluginsForm.DisplayCategories(null);
                pnlNoToolLibraryAccess.Visible = true;
                toolTip.SetToolTip(pbToolLibraryError, error.Message);
            }

            var ctrls = Controls.OfType<ConnectingControl>();
            if (ctrls.Any()) Controls.Remove(ctrls.First());
            var ctrls2 = Controls.OfType<ConnectingCdsControl>();
            if (ctrls2.Any()) Controls.Remove(ctrls2.First());
        }

        private void PrepareCategories()
        {
            if (store == null || store.PluginsCount == 0) return;

            var dico = new Dictionary<string, List<string>>();
            foreach (var plugin in store.Tools)
            {
                if (plugin.CategoriesList == null) continue;

                foreach (var category in plugin.CategoriesList.Split(','))
                {
                    if (!dico.ContainsKey(category))
                    {
                        dico.Add(category, new List<string>());
                    }

                    foreach (var file in plugin.Files)
                    {
                        foreach (var tool in PluginManagerExtended.Instance.PluginsExt)
                        {
                            var assemblyFile = Path.GetFileName(tool.Metadata.AssemblyFilename);
                            if (assemblyFile.ToLower() == Path.GetFileName(file).ToLower())
                            {
                                dico[category].Add(tool.Metadata.Name);

                                tool.Metadata.Categories.Clear();
                                tool.Metadata.Categories.AddRange(plugin.CategoriesList.Split(','));
                            }
                        }
                    }
                }
            }

            dico = dico.OrderByDescending(obj => obj.Key).ToDictionary(obj => obj.Key, obj => obj.Value);

            pluginsForm.DisplayCategories(dico);
        }

        private void SetTheme()
        {
            if (Options.Instance.Theme != null)
            {
                switch (Options.Instance.ThemeValue)
                {
                    case ThemeName.Blue:
                        {
                            var theme = new VS2015BlueTheme();
                            dpMain.Theme = theme;
                        }
                        break;

                    case ThemeName.Light:
                        {
                            var theme = new VS2015LightTheme();
                            dpMain.Theme = theme;
                        }
                        break;

                    case ThemeName.Dark:
                        {
                            var theme = new VS2015DarkTheme();
                            dpMain.Theme = theme;

                            CustomTheme.Instance.SetTheme(new DarkTheme());
                            CustomTheme.Instance.ApplyTheme(this);
                        }
                        break;
                }
            }
        }

        #endregion Form management

        #region Start page

        private void ShowStartPage()
        {
            if (startPage == null || startPage.IsDisposed)
            {
                startPage = new StartPage(pluginsForm.PluginManager);
                startPage.OpenMruPluginRequested += StartPage_OpenMruPluginRequested;
                startPage.OpenPluginRequested += StartPage_OpenPluginRequested;
                startPage.OpenSettingsRequested += StartPage_OpenSettingsRequested;
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
                StartPluginWithoutConnection(e);
                return;
            }

            bool connectionFound = false;
            foreach (var file in ConnectionsList.Instance.Files)
            {
                var cList = CrmConnections.LoadFromFile(file.Path);
                var connection = cList.Connections.FirstOrDefault(c => c.ConnectionId == cid);
                if (connection != null)
                {
                    connection.ParentConnectionFile = file;
                    connectionFound = true;
                    initialPluginName = e.Item.PluginName;
                    initialConnectionName = e.Item.ConnectionName;

                    if (connection.ConnectionName != connectionDetail?.ConnectionName)
                    {
                        var info = new ConnectionParameterInfo();

                        UserControl connectingControl;
                        if (connection.UseOnline)
                        {
                            connectingControl = new ConnectingCdsControl { Anchor = AnchorStyles.None };
                        }
                        else
                        {
                            connectingControl = new ConnectingControl { Anchor = AnchorStyles.None };
                        }
                        connectingControl.Left = Width / 2 - connectingControl.Width / 2;
                        connectingControl.Top = Height / 2 - connectingControl.Height / 2;
                        Controls.Add(connectingControl);
                        connectingControl.BringToFront();

                        info.ConnControl = connectingControl;
                        numberOfConnectionReceived = 0;
                        fHelper.AskForConnection(connection, info, null);
                    }
                    else
                    {
                        StartPluginWithConnection();
                    }

                    break;
                }
            }

            if (!connectionFound)
            {
                if (MessageBox.Show(this, "The connection specified cannot be found. Would you like to use another connection instead?",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var plugin = pluginsForm.PluginManager.PluginsExt.First(p => p.Metadata.Name == e.Item.PluginName);

                    ConnectUponApproval(new RequestConnectionEventArgs { Parameter = plugin });
                }
            }
        }

        private void StartPage_OpenPluginRequested(object sender, OpenFavoritePluginEventArgs e)
        {
            initialPluginName = e.Item.PluginName;
            if (e.NewConnectionNeeded)
            {
                pluginsForm.OpenPlugin(initialPluginName, null, true);
                return;
            }
            else if (e.Item.ConnectionId != Guid.Empty)
            {
                StartPage_OpenMruPluginRequested(sender, new OpenMruPluginEventArgs(new MostRecentlyUsedItem
                {
                    ConnectionId = e.Item.ConnectionId,
                    ConnectionName = e.Item.ConnectionName,
                    PluginName = e.Item.PluginName
                }));
                return;
            }

            StartPluginWithoutConnection();
        }

        private void StartPage_OpenSettingsRequested(object sender, System.EventArgs e)
        {
            tsddbTools_DropDownItemClicked(sender, new ToolStripItemClickedEventArgs(tsmiXtbSettings));
        }

        #endregion Start page

        #region Plugins Form events

        private UserControl DisplayPluginControl(Lazy<IXrmToolBoxPlugin, IPluginMetadataExt> plugin, bool displayConnected = true)
        {
            var isc = new ItSecurityChecker();
            isc.LoadAllowedPlugins();
            if (!isc.IsPluginAllowed(plugin.Metadata.PluginType))
            {
                MessageBox.Show(this, "This plugin is not allowed by your IT administrators. Please contact them", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            Guid pluginControlInstanceId = Guid.NewGuid();
            UserControl pluginControl = null;
            try
            {
                var mruItem = new MostRecentlyUsedItem
                {
                    PluginName = plugin.Metadata.Name,
                    Date = DateTime.Now
                };

                pluginControl = (UserControl)plugin.Value.GetControl();
                pluginControl.Tag = pluginControlInstanceId;

                if (pluginControl is PluginControlBase pcb)
                {
                    pcb.ToolName = plugin.Metadata.Name;
                }

                // ReSharper disable once SuspiciousTypeConversion.Global
                if (pluginControl is IMessageBusHost host)
                {
                    host.OnOutgoingMessage += MainForm_MessageBroker;
                }

                string name;

                if (service != null && displayConnected)
                {
                    mruItem.ConnectionId = connectionDetail.ConnectionId ?? Guid.Empty;
                    mruItem.ConnectionName = connectionDetail.ConnectionName;
                    mruItem.ConnectionFileName = connectionDetail.ParentConnectionFile?.Name;

                    if (connectionDetail.IsFromSdkLoginCtrl)
                    {
                        ((IXrmToolBoxPluginControl)pluginControl).UpdateConnection(service, connectionDetail);
                    }
                    else
                    {
                        var crmSvcClient = connectionDetail.GetCrmServiceClient();

                        ((IXrmToolBoxPluginControl)pluginControl).UpdateConnection(crmSvcClient, connectionDetail);
                    }

                    name = $"{plugin.Metadata.Name} ({connectionDetail?.ConnectionName ?? "Not connected"})";
                }
                else
                {
                    name = $"{plugin.Metadata.Name} (Not connected)";
                }

                ((IXrmToolBoxPluginControl)pluginControl).OnRequestConnection += NewForm_OnRequestConnection;
                ((IXrmToolBoxPluginControl)pluginControl).OnWorkAsync += (sender, e) =>
               {
                   //var bw = new BackgroundWorker();
                   //bw.DoWork += (s, evt) => { evt.Result = AnnouncementManager.GetItemToDisplay(); };
                   //bw.RunWorkerCompleted += (s, evt) => { AnnouncementManager.Display(evt.Result as AnnouncementItem); };
                   //bw.RunWorkerAsync();
               };

                if (pnlConnectLoading.Visible)
                {
                    pnlConnectLoading.SendToBack();
                    pnlConnectLoading.Visible = false;
                }

                if (pluginControl is IDuplicatableTool idt)
                {
                    idt.DuplicateRequested += (sender, e) =>
                    {
                        var pluginName = ((PluginForm)((PluginControlBase)sender).ParentForm).PluginName;
                        if (e.NewConnection)
                        {
                            var pluginToDuplicate = pluginsForm.PluginManager.ValidatedPluginsExt.FirstOrDefault(p => p.Metadata.Name == pluginName);

                            if (((PluginForm)dpMain.ActiveContent).Control is IDuplicatableTool dt)
                            {
                                ConnectUponApproval(new DuplicateToolWithConnectionArgs { SourceTool = dt, Tool = pluginToDuplicate, State = e.State });
                            }
                        }
                        else
                        {
                            if (sender is IDuplicatableTool dt)
                            {
                                pluginsForm.DuplicateTool(((PluginForm)dpMain.ActiveContent).PluginName, dt, e.State);
                            }
                        }
                    };
                }

                var pluginForm = new PluginForm(pluginControl, name, plugin.Metadata.Name);
                pluginForm.PluginName = plugin.Metadata.Name;

                if (pluginControl is ICompanion ic)
                {
                    DockState position;
                    switch (ic.GetPosition())
                    {
                        case RightOrLeft.Left:
                            position = DockState.DockLeft;
                            break;

                        case RightOrLeft.Bottom:
                            position = DockState.DockBottom;
                            break;

                        default:
                            position = DockState.DockRight;
                            break;
                    }

                    pluginForm.Show(dpMain, position);
                }
                else
                {
                    pluginForm.Show(dpMain, DockState.Document);
                }
                pluginForm.SendMessageToStatusBar += StatusBarMessenger_SendMessageToStatusBar;
                pluginForm.TabPageContextMenuStrip = cmsMain;
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
                    tsbImpersonate.Visible = false;
                }

                var pluginInOption =
                    Options.Instance.MostUsedList.FirstOrDefault(i => i.Name == plugin.Metadata.PluginType);
                if (pluginInOption == null)
                {
                    pluginInOption = new PluginUseCount { Name = plugin.Metadata.PluginType, Count = 0 };
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
                        var assembly = Assembly.LoadFile(
                            new FileInfo(Assembly.GetExecutingAssembly().Location).Directory +
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

                if (!(pluginControl is IPrivatePlugin))
                {
                    ai.WritePageView(plugin.Metadata.Name, plugin.Metadata.Version);
                }

                Options.Instance.Save();

                if (pluginControl is IMessageBusHost host2)
                {
                    if (!string.IsNullOrEmpty(initialData))
                    {
                        host2.OnIncomingMessage(new MessageBusEventArgs("URL Protocol")
                        {
                            SourcePlugin = "URL Protocol",
                            TargetArgument = initialData
                        });
                    }
                }

                return pluginControl;
            }
            catch (Exception error)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show(this, $@"An error occured when trying to display this tool: {error.Message}",
                        @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (pnlConnectLoading.Visible)
                    {
                        pnlConnectLoading.SendToBack();
                        pnlConnectLoading.Visible = false;
                    }
                }));

                return pluginControl;
            }
        }

        private void PluginsForm_ActionRequested(object sender, PluginsListEventArgs e)
        {
            switch (e.Action)
            {
                case OpenPluginsStoreAction openStore:
                    OpenPluginsStore(false, openStore.SearchTerm);
                    break;
            }
        }

        private void PluginsForm_OpenPluginProjectUrlRequested(object sender, PluginEventArgs e)
        {
            var type = e.PluginControl?.GetType();

            if (type == null)
            {
                type = e.Plugin == null
                    ? null
                    : AppDomain.CurrentDomain.GetAssemblies()
                        .First(a => a.FullName == e.Plugin.Metadata.AssemblyQualifiedName)
                        .GetType(e.Plugin.Metadata.PluginType);

                if (type == null)
                {
                    MessageBox.Show(this, @"Unable to determine tool location on disk");
                    return;
                }
            }

            var filePath = Assembly.GetAssembly(type).Location;
            if (File.Exists(filePath))
            {
                var fileName = Path.GetFileName(filePath);

                if (store == null)
                {
                    MessageBox.Show(this,
                        @"The Tool Library is not initialized so we cannot find the project url",
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
                        @"This tool is not on the Tool Library, its Project Url is not defined or it is not possible to connect to XrmToolBox portal to retrieve this information. Therefore, we cannot lead you to the project page",
                        @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void PluginsForm_OpenPluginRequested(object sender, PluginEventArgs e)
        {
            if (store != null && store.PluginsCount > 0)
            {
                var location = e.Plugin.Metadata.AssemblyFilename;

                var updatedPlugin = store.GetPluginUpdateByFile(location);
                if (updatedPlugin != null)
                {
                    if (!Options.Instance.PluginsUpdateSkip.Any(
                        x => x.Name == updatedPlugin.Name
                             && x.Version == updatedPlugin.Version
                             && x.Date > DateTime.Now))
                    {
                        using (var dialog = new NewPluginVersion(updatedPlugin))
                        {
                            if (dialog.ShowDialog(this) == DialogResult.Yes)
                            {
                                store.InstallOneToolUpdate(updatedPlugin, dialog.OnNextRestart, this);
                            }
                            else
                            {
                                UpdatePluginUpdateSkip(updatedPlugin.Name, updatedPlugin.Version, dialog.IsVersionSkipped, dialog.NumberOfDaysToSkip);
                            }
                        }
                    }
                }
            }

            Cursor = Cursors.WaitCursor;

            if (e.NeedNewConnection)
            {
                ConnectUponApproval(e.Plugin);
                Cursor = Cursors.Default;
                return;
            }

            if (e.Plugin.Metadata.Interfaces.Contains(nameof(INoConnectionRequired)))
            {
                var ctrl = DisplayPluginControl(e.Plugin);
                if (ctrl is IDuplicatableTool dt && e.SourceTool != null)
                {
                    dt.ApplyState(e.State ?? e.SourceTool.GetState());
                }
                Cursor = Cursors.Default;
                return;
            }

            if (service == null && e.MruInfo == null)
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
                        var ctrl = DisplayPluginControl(e.Plugin);
                        if (ctrl is IDuplicatableTool dt && e.SourceTool != null)
                        {
                            dt.ApplyState(e.State ?? e.SourceTool.GetState());
                        }
                    }
                }
            }
            else
            {
                if (e.Plugin != null)
                {
                    var ctrl = DisplayPluginControl(e.Plugin, e.MruInfo == null || e.MruInfo.Item.ConnectionId != Guid.Empty);
                    if (ctrl is IDuplicatableTool dt && e.SourceTool != null)
                    {
                        dt.ApplyState(e.State ?? e.SourceTool.GetState());
                    }
                }
            }

            FillPluginsListMenuDisplay();

            Cursor = Cursors.Default;
        }

        private void PluginsForm_UninstallPluginRequested(object sender, PluginEventArgs e)
        {
            if (DialogResult.No == MessageBox.Show(this,
                    @"Are you sure you want to uninstall this tool?",
                    @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            var filePath = e.Plugin.Metadata.AssemblyFilename;
            if (File.Exists(filePath))
            {
                var fileName = Path.GetFileName(filePath);

                if (store == null)
                {
                    MessageBox.Show(this,
                        @"The Tool Library is not initialized so we cannot find files to uninstall",
                        @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                store.UninstallByFileName(fileName);
            }
        }

        private void UpdatePluginUpdateSkip(string name, string version, bool isVersionSkipped, int nbDays)
        {
            var existing = Options.Instance.PluginsUpdateSkip.FirstOrDefault(
                x => x.Name == name);

            if (existing != null)
            {
                Options.Instance.PluginsUpdateSkip.Remove(existing);
            }

            var nextDate = DateTime.Now.AddDays(nbDays);
            if (isVersionSkipped)
            {
                nextDate = DateTime.Now.AddYears(10);
            }

            Options.Instance.PluginsUpdateSkip.Add(new PluginUpdateSkip
            {
                Name = name,
                Version = version,
                Date = nextDate
            });
            Options.Instance.Save();
        }

        #endregion Plugins Form events

        #region Connection methods

        private void ApplyConnectionToTabs(ConnectionDetail detail)
        {
            var pluginForms = dpMain.Contents.OfType<PluginForm>().ToList();

            using (var tcu = new TabConnectionUpdater(pluginForms, detail) { StartPosition = FormStartPosition.CenterParent })
            {
                if (tcu.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (PluginForm form in tcu.SelectedPluginForms)
                    {
                        UpdateTabConnection(form, detail);
                    }

                    if (tcu.SelectedPluginForms.Any())
                    {
                        FillPluginsListMenuDisplay();
                    }
                }
            }
        }

        private void ConnectUponApproval(object connectionParameter)
        {
            numberOfConnectionReceived = 0;

            var info = new ConnectionParameterInfo
            {
                ConnectionParmater = connectionParameter
            };

            fHelper.AskForConnection(info, details =>
            {
                if (details.Any(d => d.UseOnline == false))
                {
                    info.ConnControl = new ConnectingControl { Anchor = AnchorStyles.None };
                }
                else
                {
                    info.ConnControl = new ConnectingCdsControl { Anchor = AnchorStyles.None };
                }

                info.ConnControl.Left = Width / 2 - info.ConnControl.Width / 2;
                info.ConnControl.Top = Height / 2 - info.ConnControl.Height / 2;
                Controls.Add(info.ConnControl);
                info.ConnControl.BringToFront();
            });
        }

        private Task LaunchInitialConnection(ConnectionDetail detail)
        {
            var info = new ConnectionParameterInfo();

            if (!detail.UseOnline)
            {
                info.ConnControl = new ConnectingControl { Anchor = AnchorStyles.None };
            }
            else
            {
                info.ConnControl = new ConnectingCdsControl { Anchor = AnchorStyles.None };
            }

            info.ConnControl.Left = Width / 2 - info.ConnControl.Width / 2;
            info.ConnControl.Top = Height / 2 - info.ConnControl.Height / 2;
            Controls.Add(info.ConnControl);
            info.ConnControl.BringToFront();

            return new Task(() => ConnectionManager.Instance.ConnectToServer(new List<ConnectionDetail> { detail }));
        }

        private void ManageConnectionControl()
        {
            if (!Directory.Exists(Paths.ConnectionsPath))
            {
                Directory.CreateDirectory(Paths.ConnectionsPath);
            }

            ConnectionsList.ConnectionsListFilePath = Path.Combine(Paths.ConnectionsPath, "MscrmTools.ConnectionsList.xml");
            cManager = ConnectionManager.Instance;
            cManager.FromXrmToolBox = true;
            cManager.RequestPassword += (sender, e) => fHelper.RequestPassword(e.ConnectionDetail);
            cManager.StepChanged += (sender, e) => ccsb.SetMessage(e.CurrentStep);
            cManager.ConnectionSucceed += (sender, e) =>
            {
                numberOfConnectionReceived++;
                var parameter = e.Parameter as ConnectionParameterInfo;
                if (parameter != null && e.NumberOfConnectionsRequested <= numberOfConnectionReceived)
                {
                    Controls.Remove(parameter.ConnControl);
                    parameter.ConnControl.Dispose();
                }

                _ = Invoke(new Action(() =>
                  {
                      pluginsForm.ConnectionDetail = e.ConnectionDetail;

                      if (e.ConnectionDetail.UseOnline)
                      {
                          tsbOpenOrg.Text = @"Open environment";
                          tsbOpenOrg.ToolTipText = @"Open the connected environment in your web browser";
                          tsbOpenOrg.Image = Properties.Resources.Dataverse24;
                          tsbOpenMaker.Visible = true;
                      }
                      else
                      {
                          tsbOpenOrg.Text = @"Open organization";
                          tsbOpenOrg.ToolTipText = @"Open the connected organization in your web browser";
                          tsbOpenOrg.Image = Properties.Resources.Dynamics365_24;
                          tsbOpenMaker.Visible = false;
                      }

                      tsbImpersonate.Enabled = e.ConnectionDetail.CanImpersonate;
                      tsbImpersonate.Visible = true;

                      if (parameter != null)
                      {
                          var us = parameter.ConnectionParmater as UserControl;
                          var p = parameter.ConnectionParmater as Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>;
                          var rcea = parameter.ConnectionParmater as RequestConnectionEventArgs;
                          var dtwc = parameter.ConnectionParmater as DuplicateToolWithConnectionArgs;

                          if (us != null)
                          {
                              ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                              ccsb.SetMessage(string.Empty);
                              connectionDetail = e.ConnectionDetail;
                              service = e.OrganizationService;

                              if (!(us.Tag is Lazy<IXrmToolBoxPlugin, IPluginMetadataExt> pluginModel))
                              {
                                  // Actual Plugin was passed, Just update the plugin's Tab.
                                  UpdateTabConnection((PluginForm)us.ParentForm, e.ConnectionDetail);
                              }
                              else
                              {
                                  DisplayPluginControl(pluginModel);
                              }
                          }
                          else if (p != null)
                          {
                              ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                              ccsb.SetMessage(string.Empty);
                              connectionDetail = e.ConnectionDetail;
                              service = e.OrganizationService;

                              DisplayPluginControl(p);
                          }
                          else if (parameter.ConnectionParmater?.ToString() == "ApplyConnectionToTabs" && dpMain.Contents.OfType<PluginForm>().Any())
                          {
                              ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                              ccsb.SetMessage(string.Empty);
                              connectionDetail = e.ConnectionDetail;
                              service = e.OrganizationService;

                              ApplyConnectionToTabs(e.ConnectionDetail);
                          }
                          else if (rcea != null)
                          {
                              if (rcea.Parameter is Lazy<IXrmToolBoxPlugin, IPluginMetadataExt> plugin)
                              {
                                  ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                                  ccsb.SetMessage(string.Empty);
                                  connectionDetail = e.ConnectionDetail;
                                  service = e.OrganizationService;

                                  DisplayPluginControl(plugin);
                              }
                              else
                              {
                                  var userControl = (UserControl)rcea.Control;

                                  if (rcea.ActionName != "AdditionalOrganization")
                                  {
                                      ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                                      ccsb.SetMessage(string.Empty);
                                      connectionDetail = e.ConnectionDetail;
                                      service = e.OrganizationService;

                                      if (userControl.ParentForm != null)
                                      {
                                          var indexOfParenthesis = userControl.ParentForm?.Text?.IndexOf("(") ?? -1;
                                          var pluginName =
                                              userControl.ParentForm?.Text?.Substring(0, indexOfParenthesis - 1) ??
                                              "N/A";

                                          userControl.ParentForm.Text =
                                              $@"{pluginName} ({e.ConnectionDetail.ConnectionName})";
                                      }
                                  }

                                  var pForm = ((PluginForm)userControl.ParentForm);

                                  if (pluginConnections.ContainsKey(pForm))
                                  { pluginConnections[pForm] = connectionDetail; }
                                  else
                                  { pluginConnections.Add(pForm, connectionDetail); }

                                  pForm.UpdateConnection(e.OrganizationService,
                                      e.ConnectionDetail, rcea.ActionName, rcea.Parameter);
                              }
                          }
                          else if (dtwc != null)
                          {
                              ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                              ccsb.SetMessage(string.Empty);
                              connectionDetail = e.ConnectionDetail;
                              service = e.OrganizationService;

                              var state = dtwc.State ?? dtwc.SourceTool.GetState();
                              var duplicatedTool = DisplayPluginControl(dtwc.Tool);
                              ((IDuplicatableTool)duplicatedTool).ApplyState(state);
                          }
                          else
                          {
                              ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                              ccsb.SetMessage(string.Empty);
                              connectionDetail = e.ConnectionDetail;
                              service = e.OrganizationService;
                          }
                      }
                      else if (dpMain.Contents.OfType<PluginForm>().Any())
                      {
                          ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                          ccsb.SetMessage(string.Empty);
                          connectionDetail = e.ConnectionDetail;
                          service = e.OrganizationService;

                          ApplyConnectionToTabs(e.ConnectionDetail);
                      }
                      else
                      {
                          ccsb.SetConnectionStatus(true, e.ConnectionDetail);
                          ccsb.SetMessage(string.Empty);
                          connectionDetail = e.ConnectionDetail;
                          service = e.OrganizationService;
                      }

                      StartPluginWithConnection();

                      tssOpenOrg.Visible = true;
                      tsbOpenOrg.Visible = true;
                      tsbImpersonate.Visible = true;
                  }));
            };
            cManager.ConnectionFailed += (sender, e) =>
            {
                numberOfConnectionReceived++;
                Invoke(new Action(() =>
                {
                    if (e.Parameter is ConnectionParameterInfo param && param.ConnControl != null && e.NumberOfConnectionsRequested == numberOfConnectionReceived)
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

            fHelper = new FormHelper(this, Options.Instance);
            ccsb = new CrmConnectionStatusBar(fHelper) { Dock = DockStyle.Bottom };
            Controls.Add(ccsb);
            Controls.SetChildIndex(ccsb, Controls.Count - 1);
        }

        private void NewForm_OnRequestConnection(object sender, System.EventArgs e)
        {
            ConnectUponApproval(e is RequestConnectionEventArgs ? e : sender);
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

        private void StartPluginWithoutConnection(OpenMruPluginEventArgs e = null)
        {
            if (!string.IsNullOrEmpty(initialPluginName))
            {
                pluginsForm.OpenPlugin(initialPluginName, e);

                // Resetting initial plugin name
                initialPluginName = string.Empty;
            }
        }

        private void tsbConnect_Click(object sender, System.EventArgs e)
        {
            ConnectUponApproval("ApplyConnectionToTabs");
        }

        private void UpdateTabConnection(PluginForm pluginForm, ConnectionDetail detail, string actionName = "", object parameter = null)
        {
            var indexOfParenthesis = pluginForm.Text?.IndexOf("(") ?? -1;
            var pluginName = pluginForm.Text?.Substring(0, indexOfParenthesis - 1) ?? "N/A";

            if (pluginConnections.ContainsKey(pluginForm))
            { pluginConnections[pluginForm] = connectionDetail; }
            else
            { pluginConnections.Add(pluginForm, connectionDetail); }

            pluginForm.UpdateConnection(service, detail, actionName, parameter);
            pluginForm.Text = $@"{pluginName} ({detail?.ConnectionName ?? "Not connected"})";
        }

        #endregion Connection methods

        #region Message broker

        private PluginForm GetPluginByName(string name, Guid connectionDetailId)
        {
            if (Guid.TryParse(name, out Guid pluginId) && !pluginId.Equals(Guid.Empty))
            {
                var expectedPlugin = PluginManagerExtended.Instance.PluginsExt.FirstOrDefault(p =>
                    p.Metadata.Id.Equals(pluginId));

                if (expectedPlugin != null)
                {
                    name = expectedPlugin.Metadata.Name;
                }
            }

            return dpMain.Contents
                .OfType<PluginForm>()
                .LastOrDefault(c => c.PluginName == name
                                     && (((PluginControlBase)c.Control).ConnectionDetail?.ConnectionId ?? Guid.Empty).Equals(connectionDetailId));
        }

        private bool IsMessageValid(object sender, MessageBusEventArgs message)
        {
            if (message == null ||
                !(sender is UserControl sourceControl) ||
                !(sourceControl is IXrmToolBoxPluginControl) ||
                !(sourceControl.ParentForm is PluginForm pluginForm))
            {
                // Error. Possible reasons are:
                // * empty sender
                // * empty message
                // * sender is not UserControl
                // * sender is not XrmToolBox Plugin
                return false;
            }

            if (string.IsNullOrEmpty(message.SourcePlugin))
            {
                message.SourcePlugin = pluginForm.PluginName;
            }
            // Commenting below - perhaps the caller wanted to override the default name for some reason // J Rapp 2018-05-30
            //else if (message.SourcePlugin != sourceControl.GetType().GetTitle())
            //{
            //    // For some reason incorrect name was set in Source Plugin field
            //    return false;
            //}

            // Everything went ok
            return true;
        }

        private void MainForm_MessageBroker(object sender, MessageBusEventArgs message)
        {
            if (!IsMessageValid(sender, message))
            {
                return;
            }

            var sourceDetail = ((PluginControlBase)sender).ConnectionDetail;

            var content = message.NewInstance ? null : GetPluginByName(message.TargetPlugin, sourceDetail?.ConnectionId ?? Guid.Empty);
            if (content == null)
            {
                pluginsForm.OpenPlugin(message.TargetPlugin);
            }

            content = GetPluginByName(message.TargetPlugin, sourceDetail?.ConnectionId ?? Guid.Empty);
            if (content == null)
            {
                MessageBox.Show($@"Cannot switch to tool {message.TargetPlugin}.", message.SourcePlugin, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            content.Show(dpMain, content.DockState);
            content.SendIncomingBrokerMessage(message);
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
                if (Options.Instance.DoNotCheckForUpdates || new ItSecurityChecker().IsCheckForUpdateDisabled())
                {
                    return;
                }

                WelcomeDialog.SetStatus("Checking for XrmToolBox update...");

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

        private void ApplyActiveContentDisplay()
        {
            if (dpMain.ActiveContent != null)
            {
                if (currentContent != dpMain.ActiveContent)
                {
                    currentContent = dpMain.ActiveContent;

                    ProcessMenuItemsForPlugin2();

                    if (dpMain.ActiveContent is StartPage sp)
                    {
                        sp.LoadMru();
                    }

                    if (dpMain.ActiveContent is PluginForm pf)
                    {
                        if (pf.Control is PluginControlBase pcb)
                        {
                            ccsb.SetConnectionStatus(pcb.ConnectionDetail != null, pcb.ConnectionDetail);
                            connectionDetail = pcb.ConnectionDetail;
                            pluginsForm.ConnectionDetail = pcb.ConnectionDetail;
                        }
                    }
                }
                var activeContent = ((DockContent)dpMain.ActiveContent);

                var isOnAdditionnalGroup = activeContent.Pane?.NestedDockingStatus?.NestedPanes?.Count > 1
                    && (activeContent.Pane?.NestedDockingStatus?.NestedPanes?.Skip(1).Any(p => p.ActiveContent == activeContent) ?? false);

                tsmiMoveToHorizontalGroup.Visible = !isOnAdditionnalGroup;
                tsmiMoveToVerticalGroup.Visible = !isOnAdditionnalGroup;
                tsmiMoveToOrigin.Visible = isOnAdditionnalGroup;
            }
        }

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

        private void tsmiMoveToGroup_Click(object sender, System.EventArgs e)
        {
            if (dpMain.ActiveDocument != null)
            {
                if (sender == tsmiMoveToVerticalGroup)
                {
                    ((DockContent)dpMain.ActiveContent).Show(dpMain.Panes.First(), DockAlignment.Right, 0.5);
                }
                else if (sender == tsmiMoveToHorizontalGroup)
                {
                    ((DockContent)dpMain.ActiveContent).Show(dpMain.Panes.First(), DockAlignment.Bottom, 0.5);
                }
                else
                {
                    ((DockContent)dpMain.ActiveContent).DockTo(((DockContent)pluginsForm).Pane, DockStyle.Fill, dpMain.Contents.Count - 1);
                }

                ApplyActiveContentDisplay();
            }
        }

        #endregion Change of active content

        #region Menu Toolbar

        private void checkForUpdateToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (new ItSecurityChecker().IsCheckForUpdateDisabled())
            {
                MessageBox.Show(this, "Your company policy does not allow you to search for update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

        private void OpenPluginsStore(bool showUpdateOnly = false, string defaultSearchTerm = null)
        {
            if (pluginsForm == null)
            {
                return;
            }

            pnlPluginsUpdate.Visible = false;

            if (libraryForm == null || libraryForm.IsDisposed)
            {
                if (!(store is ToolLibrary.ToolLibrary))
                {
                    ItSecurityChecker isc = new ItSecurityChecker();
                    isc.LoadRepositories();
                    store = new ToolLibrary.ToolLibrary(Options.Instance, isc.Repositories);
                    store.LoadTools().GetAwaiter().GetResult();
                }

                if (store.PluginsCount == 0 || store.Categories == null)
                {
                    MessageBox.Show(this, "Tool Library is not yet initialzed, please wait few seconds", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                libraryForm = new ToolLibraryForm((ToolLibrary.ToolLibrary)store, Options.Instance);
                libraryForm.PluginsClosingRequested += (s, evt) =>
                {
                    RequestCloseTabs(dpMain.Contents.OfType<PluginForm>(), new PluginCloseInfo(ToolBoxCloseReason.CloseAll));
                };
                libraryForm.PluginsOpeningRequested += (s, evt) =>
                {
                    var plm = PluginManagerExtended.Instance.Manifest.PluginMetadata.FirstOrDefault(pm => evt.Plugin.Files.Any(f => Path.GetFileName(f).Equals(Path.GetFileName(pm.AssemblyFilename), StringComparison.InvariantCultureIgnoreCase)));
                    if (plm == null)
                    {
                        MessageBox.Show(this, "Unable to find the tool metadata!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var plugin = PluginManagerExtended.Instance.ValidatedPluginsExt.FirstOrDefault(p => p.Metadata.Name == plm.Name);
                    if (plugin == null)
                    {
                        MessageBox.Show(this, "Unable to find the tool metadata!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (service == null)
                    {
                        ConnectUponApproval(plugin);
                    }
                    else
                    {
                        DisplayPluginControl(plugin);
                    }
                };
                ((ToolLibrary.ToolLibrary)store).OnToolsMetadataRefreshRequested += (s, evt) =>
                {
                    PluginManagerExtended.Instance.Recompose();
                };
                ((ToolLibrary.ToolLibrary)store).PluginsUpdated += (s, evt) =>
                {
                    pluginsForm.ReloadPluginsList();
                    PrepareCategories();
                };

                if (showUpdateOnly)
                {
                    libraryForm.ShowUpdatesOnly();
                }
            }

            if (defaultSearchTerm != null)
            {
                libraryForm.SetSearchTerm(defaultSearchTerm);
            }
            libraryForm.Show(dpMain, DockState.Document);
        }

        private void SettingsForm_OnProxySettingsChanged(object sender, System.EventArgs e)
        {
            llRetryInitToolLibrary_LinkClicked(this, new LinkLabelLinkClickedEventArgs(llRetryInitToolLibrary.Links[0]));
        }

        private void tsbImpersonate_Click(object sender, System.EventArgs e)
        {
            using (var dialog = new UserSelectionDialog(connectionDetail.ServiceClient))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    connectionDetail.Impersonate(dialog.SelectedUser.Id, dialog.SelectedUser.GetAttributeValue<string>("fullname"));
                }
            }
        }

        private void tsbOpenMaker_Click(object sender, System.EventArgs e)
        {
            string url;
            if (connectionDetail.EnvironmentId == null)
            {
                url = "https://make.powerapps.com/";
            }
            else
            {
                url = string.Format("https://make.powerapps.com/environments/{0}/Home", connectionDetail.EnvironmentId);
            }

            if (url.Length > 0)
            {
                connectionDetail.OpenUrlWithBrowserProfile(new Uri(url));
            }
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
                connectionDetail.OpenUrlWithBrowserProfile(new Uri(url));
            }
        }

        private async void tsddbTools_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == pluginsStoreToolStripMenuItem)
            {
                OpenPluginsStore();
            }
            else if (e.ClickedItem == manageConnectionsToolStripMenuItem)
            {
                fHelper.DisplayConnectionsList(this);
            }
            else if (e.ClickedItem == tsmiXtbSettings)
            {
                if (settingsForm == null || settingsForm.IsDisposed)
                {
                    settingsForm = new SettingsForm();
                    settingsForm.OnProxySettingsChanged += SettingsForm_OnProxySettingsChanged;
                }

                settingsForm.Show(dpMain, DockState.Document);
            }
            else if (e.ClickedItem == tsmiToolSettings)
            {
                var plugin = (ISettingsPlugin)((PluginForm)dpMain.ActiveContent).Control;
                plugin.ShowSettings();
            }
        }

        #region Document management

        private void cmsMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == cmsMainCloseAll)
            {
                RequestCloseTabs(dpMain.Contents.OfType<PluginForm>().Where(c => !(c.Control is ICompanion)), new PluginCloseInfo(ToolBoxCloseReason.CloseAll));
            }
            else if (e.ClickedItem == cmsMainCloseExceptThis)
            {
                RequestCloseTabs(dpMain.Contents.OfType<PluginForm>().Where(p => dpMain.ActiveContent != p && !(p.Control is ICompanion)), new PluginCloseInfo(ToolBoxCloseReason.CloseAllExceptActive));
            }
            else if (e.ClickedItem == cmsMainCloseThis)
            {
                RequestCloseTab((PluginForm)dpMain.ActiveContent, new PluginCloseInfo(ToolBoxCloseReason.CloseCurrent));
            }
            else if (e.ClickedItem == cmsMainDuplicateTool)
            {
                if (((PluginForm)dpMain.ActiveContent).Control is IDuplicatableTool dt)
                {
                    pluginsForm.DuplicateTool(((PluginForm)dpMain.ActiveContent).PluginName, dt, null);
                }
                else
                {
                    pluginsForm.OpenPlugin(((PluginForm)dpMain.ActiveContent).PluginName);
                }
            }
            else if (e.ClickedItem == cmsMainDuplicateToolWithConnection)
            {
                var pluginName = ((PluginForm)dpMain.ActiveContent).PluginName;
                var plugin = pluginsForm.PluginManager.ValidatedPluginsExt.FirstOrDefault(p => p.Metadata.Name == pluginName);

                if (((PluginForm)dpMain.ActiveContent).Control is IDuplicatableTool dt)
                {
                    ConnectUponApproval(new DuplicateToolWithConnectionArgs { SourceTool = dt, Tool = plugin });
                }
                else
                {
                    ConnectUponApproval(new RequestConnectionEventArgs
                    { ActionName = "", Control = ((PluginForm)dpMain.ActiveDocument).Control, Parameter = plugin });
                }
            }
            else if (e.ClickedItem == tsmiChangeTabConnection)
            {
                ConnectUponApproval(new RequestConnectionEventArgs { ActionName = "", Control = ((PluginForm)dpMain.ActiveContent).Control });
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
            if (content.CloseWithReason(info.ToolBoxReason, forceSilent))
            {
                pluginConnections.Remove(content);
            }
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

        private void tsbManageWindows_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == closeAllWindowsToolStripMenuItem)
            {
                RequestCloseTabs(dpMain.Contents.OfType<PluginForm>().Where(c => !(c.Control is ICompanion)), new PluginCloseInfo(ToolBoxCloseReason.CloseAll));
            }
            else if (e.ClickedItem == closeAllWindowsExceptActiveToolStripMenuItem)
            {
                RequestCloseTabs(dpMain.Contents.OfType<PluginForm>().Where(p => dpMain.ActiveContent != p && !(p.Control is ICompanion)), new PluginCloseInfo(ToolBoxCloseReason.CloseAllExceptActive));
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

        private void tsmiNameTopLevelWindow_TextChanged(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tsmiNameTopLevelWindow.Text))
            {
                Text = windowTitleSuffix;
            }
            else
            {
                Text = $"{tsmiNameTopLevelWindow.Text} - {windowTitleSuffix}";
            }
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
            string pluginName = null;
            if (currentContent is PluginForm pf)
            {
                pluginName = pf.PluginName;
            }

            using (var form = new DonationIntroForm(pluginName))
            {
                form.ShowDialog(this);
            }
        }

        #endregion Support panel

        private void llCloseNugetNotAvailPanel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlNoToolLibraryAccess.Visible = false;
        }

        private void llClosePluginsUpdatePanel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlPluginsUpdate.Visible = false;
        }

        private async void llRetryInitToolLibrary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlNoToolLibraryAccess.Visible = false;
            try
            {
                await LoadStore();
                await store.LoadTools(false);
            }
            catch (Exception error)
            {
                pnlNoToolLibraryAccess.Visible = true;
                toolTip.SetToolTip(pbToolLibraryError, error.Message);
            }
        }

        private void openPluginsStoreButton_Click(object sender, System.EventArgs e)
        {
            pnlPluginsUpdate.Visible = false;
            OpenPluginsStore(true);
        }
        private void ApplyThemeButton_Click(object sender, System.EventArgs e)
        {
            CustomTheme.Instance.ApplyTheme(this);

            var allControls = this.GetAllControls();

            ThemeHelpers.ApplyThemeCallbacks(allControls, this);
        }
    }
}