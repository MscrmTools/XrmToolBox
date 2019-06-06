using McTools.Xrm.Connection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.UserControls;
using XrmToolBox.New.EventArgs;

namespace XrmToolBox.New
{
    public partial class PluginsForm : DockContent
    {
        #region Variables

        private readonly PluginManagerExtended pluginsManager;
        private readonly List<PluginModel> pluginsModels;
        private string filterText;
        private Thread searchThread;
        private PluginModel selectedPluginModel;

        #endregion Variables

        #region Constructor

        public PluginsForm()
        {
            InitializeComponent();

            // Set drawing optimizations
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            pluginsModels = new List<PluginModel>();

            pluginsManager = PluginManagerExtended.Instance;
            pluginsManager.IsWatchingForNewPlugins = true;
            pluginsManager.Initialize(this);
            pluginsManager.PluginsListUpdated += pManager_PluginsListUpdated;

            MouseWheel += (sender, e) => pnlPlugins.Focus();
        }

        #endregion Constructor

        #region Properties

        public ConnectionDetail ConnectionDetail { get; set; }

        public PluginManagerExtended PluginManager => pluginsManager;

        #endregion Properties

        #region Events

        public event EventHandler<PluginsListEventArgs> ActionRequested;

        public event EventHandler<PluginEventArgs> OpenPluginProjectUrlRequested;

        public event EventHandler<PluginEventArgs> OpenPluginRequested;

        public event EventHandler<PluginEventArgs> UninstallPluginRequested;

        #endregion Events

        public void OpenPlugin(string pluginName, OpenMruPluginEventArgs e = null)
        {
            if (e != null)
            {
                pluginName = e.Item.PluginName;
            }

            var plugin = pluginsManager.ValidatedPlugins.FirstOrDefault(p => p.Metadata.Name == pluginName);
            if (plugin == null)
            {
                var message = $"Plugin '{pluginName}' was not found.\n\nYou can install it from the Plugins Store";
                MessageBox.Show(this, message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenPluginRequested?.Invoke(this, e != null ? new PluginEventArgs(e, plugin) : new PluginEventArgs(plugin));
        }

        public void ReloadPluginsList()
        {
            pluginsManager.Recompose();
            pluginsModels.Clear();
            DisplayPlugins(filterText);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.Tab:

                    if (TabIndex == DockPanel.Documents.OfType<DockContent>().Count() - 1)
                    {
                        DockPanel.Documents.OfType<DockContent>().First(d => d.TabIndex == 0).Activate();
                        return true;
                    }

                    foreach (var document in DockPanel.Documents.OfType<DockContent>())
                    {
                        if (document.TabIndex == TabIndex + 1)
                        {
                            document.Activate();
                            return true;
                        }
                    }
                    break;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            return true;
        }

        private static ScrollBars GetVisibleScrollbars(ScrollableControl ctl)
        {
            if (ctl.HorizontalScroll.Visible)
                return ctl.VerticalScroll.Visible ? ScrollBars.Both : ScrollBars.Horizontal;

            return ctl.VerticalScroll.Visible ? ScrollBars.Vertical : ScrollBars.None;
        }

        private void AdaptPluginControlSize()
        {
            if (pnlPlugins == null) return;

            if (GetVisibleScrollbars(pnlPlugins) == ScrollBars.Vertical)
            {
                foreach (var ctrl in pnlPlugins.Controls)
                {
                    if (ctrl is UserControl control)
                    {
                        control.Width = pnlPlugins.Width - 28;
                    }
                }
            }
            else
            {
                foreach (var ctrl in pnlPlugins.Controls)
                {
                    if (ctrl is UserControl control)
                    {
                        control.Width = pnlPlugins.Width - 10;
                    }
                }
            }
        }

        private void AddToFavorites(string pluginName)
        {
            if (Favorites.Instance.Items.All(i => i.PluginName != pluginName))
            {
                Favorites.Instance.Items.Add(new Favorite { PluginName = pluginName });
                Favorites.Instance.Save();
            }
        }

        private void cmsOnePlugin_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsmiOpenProjectHomePage)
            {
                OpenPluginProjectUrlRequested?.Invoke(this, new PluginEventArgs(selectedPluginModel));
            }
            else if (e.ClickedItem == tsmiUninstallPlugin)
            {
                UninstallPluginRequested?.Invoke(this, new PluginEventArgs(selectedPluginModel));
            }
            else if (e.ClickedItem == tsmiHidePlugin)
            {
                var plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadata>)selectedPluginModel.Tag;
                Options.Instance.HiddenPlugins.Add(plugin.Metadata.Name);
                ReloadPluginsList();
            }
            else if (e.ClickedItem == tsmiShortcutTool)
            {
                var plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadata>)selectedPluginModel.Tag;
                CreateShortcut(plugin.Metadata.Name);
            }
            else if (e.ClickedItem == tsmiShortcutToolConnection)
            {
                var plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadata>)selectedPluginModel.Tag;
                CreateShortcut(plugin.Metadata.Name, ConnectionDetail?.ConnectionName);
            }
            else if (e.ClickedItem == tsmiAddToFavorites)
            {
                var plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadata>)selectedPluginModel.Tag;
                AddToFavorites(plugin.Metadata.Name);
            }
        }

        private void CreateModel<T>(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin, ref int top, int width, int count)
          where T : PluginModel
        {
            var name = plugin.Value.GetAssemblyQualifiedName();
            var pm = (T)pluginsModels.FirstOrDefault(t => ((Lazy<IXrmToolBoxPlugin, IPluginMetadata>)t.Tag).Value.GetType().AssemblyQualifiedName == name && t is T);
            var small = (typeof(T) == typeof(SmallPluginModel));

            if (pm == null)
            {
                var title = plugin.Metadata.Name;
                var desc = plugin.Metadata.Description;

                var author = plugin.Value.GetCompany();
                var version = plugin.Value.GetVersion();

                var backColor = ColorTranslator.FromHtml(plugin.Metadata.BackgroundColor);
                var primaryColor = ColorTranslator.FromHtml(plugin.Metadata.PrimaryFontColor);
                var secondaryColor = ColorTranslator.FromHtml(plugin.Metadata.SecondaryFontColor);

                var args = new[]
                {
                    typeof(Image),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(Color),
                    typeof(Color),
                    typeof(Color),
                    typeof(int)
                };

                var vals = new object[]
                {
                    GetImage(small ? plugin.Metadata.SmallImageBase64 : plugin.Metadata.BigImageBase64, small),
                    title,
                    desc,
                    author,
                    version,
                    backColor,
                    primaryColor,
                    secondaryColor,
                    count
                };

                var ctor = typeof(T).GetConstructor(args);
                if (ctor == null)
                {
                    throw new Exception("Unable to find a constructor of type " + typeof(T).FullName + "(" + string.Join(Environment.NewLine, args.Select(c => c.FullName)) + ")");
                }

                pm = (T)ctor.Invoke(vals);

                pm.Tag = plugin;
                pm.Clicked += Pm_Clicked;

                pluginsModels.Add(pm);
            }

            var localTop = top;

            Invoke(new Action(() =>
            {
                pm.Left = 4;
                pm.Top = localTop;
                pm.Width = width;
            }));
            top += pm.Height + 4;
        }

        private void CreateShortcut(string toolName, string connectionName = "")
        {
            Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"));//Windows Script Host Shell Object
            dynamic shell = Activator.CreateInstance(t);
            var xrmToolBoxArgs = string.Empty;
            var shortcutName = string.Empty;
            if (!string.IsNullOrEmpty(toolName))
            {
                xrmToolBoxArgs += $@" /plugin:""{toolName}""";
                shortcutName = toolName;
            }
            if (!string.IsNullOrEmpty(connectionName))
            {
                xrmToolBoxArgs += $@" /connection:""{connectionName}""";
                shortcutName = $"{toolName} ({connectionName})";
            }

            var currentArgs = Environment.GetCommandLineArgs();
            var overrideArg = currentArgs.FirstOrDefault(a => a.StartsWith("/overridepath:"));
            if (overrideArg != null)
            {
                xrmToolBoxArgs += $@" {overrideArg}";
            }

            try
            {
                var lnk = shell.CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{shortcutName}.lnk"));
                try
                {
                    lnk.TargetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XrmToolBox.exe");
                    lnk.Arguments = xrmToolBoxArgs;
                    lnk.Save();
                    MessageBox.Show(this, $@"Shortcut {shortcutName} has been created in the Desktop",
                        @"Success", MessageBoxButtons.OK);
                }
                finally
                {
                    Marshal.FinalReleaseComObject(lnk);
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(shell);
            }
        }

        private void DisplayOnePlugin(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin, ref int top, int width, int count = -1)
        {
            if (Options.Instance.IconDisplayMode == DisplayIcons.Large)
            {
                CreateModel<LargePluginModel>(plugin, ref top, width, count);
            }
            else
            {
                CreateModel<SmallPluginModel>(plugin, ref top, width, count);
            }
        }

        private void DisplayPlugins(object filter = null)
        {
            var isc = new ItSecurityChecker();
            isc.LoadAllowedPlugins();
            Invoke(new Action(() =>
            {
                pnlTop.Controls.Clear();

                if (isc.HasPluginsRestriction)
                {
                    var secInfo = new PluginsFilterInfo { Dock = DockStyle.Fill };
                    pnlTop.Controls.Add(secInfo);
                }

                if (pluginsManager.ValidationErrors?.Any() ?? false)
                {
                    var invalidInfo = new InvalidPluginsInfo(pluginsManager.ValidationErrors) { Dock = DockStyle.Fill };
                    pnlTop.Controls.Add(invalidInfo);
                }

                pnlTop.Visible = pnlTop.Controls.Count > 0;
            }));

            if (!pluginsManager.ValidatedPlugins?.Any() ?? false)
            {
                Invoke(new Action(() =>
                {
                    pnlHelp.Visible = true;
                }));

                return;
            }

            var top = 4;
            int lastWidth = pnlPlugins.Width - 28;

            // Search with filter defined
            var filteredPlugins = (filter != null && filter.ToString().Length > 0
                ? pluginsManager.ValidatedPlugins.Where(p
                    => p.Metadata.Name.ToLower().Contains(filter.ToString().ToLower())
                    || p.Metadata.Description.ToLower().Contains(filter.ToString().ToLower())
                    || p.Value.GetType().GetCompany().ToLower().Contains(filter.ToString().ToLower()))
                : pluginsManager.ValidatedPlugins).OrderBy(p => p.Metadata.Name).ToList();

            if (Options.Instance.PluginsDisplayOrder == DisplayOrder.MostUsed)
            {
                foreach (var item in Options.Instance.MostUsedList.OrderByDescending(i => i.Count).ThenBy(i => i.Name))
                {
                    var plugin = filteredPlugins.FirstOrDefault(x => x.Value.GetType().FullName == item.Name);
                    if (plugin != null && (Options.Instance.HiddenPlugins == null || !Options.Instance.HiddenPlugins.Contains(plugin.Metadata.Name)))
                    {
                        if (isc.IsPluginAllowed(plugin.Value.GetType().FullName))
                            DisplayOnePlugin(plugin, ref top, lastWidth, item.Count);
                    }
                }

                foreach (var plugin in filteredPlugins.OrderBy(p => p.Metadata.Name))
                {
                    if (Options.Instance.MostUsedList.All(i => i.Name != plugin.Value.GetType().FullName) && (Options.Instance.HiddenPlugins == null || !Options.Instance.HiddenPlugins.Contains(plugin.Metadata.Name)))
                    {
                        if (isc.IsPluginAllowed(plugin.Value.GetType().FullName))
                            DisplayOnePlugin(plugin, ref top, lastWidth);
                    }
                }
            }
            else if (Options.Instance.PluginsDisplayOrder == DisplayOrder.RecentlyUpdated)
            {
                var pluginAssemblies = Directory.EnumerateFiles(Paths.PluginsPath, "*.dll")
                    .Select(d => new
                    {
                        UpdatedOn = File.GetLastAccessTime(d),
                        FileName = d.Substring(d.LastIndexOf('\\') + 1)
                    })
                    .OrderByDescending(x => x.UpdatedOn);
                foreach (var pluginAssembly in pluginAssemblies)
                {
                    var plugins =
                        filteredPlugins.Where(
                                x => $"{x.Value.GetAssemblyQualifiedName().Split(',')[1]}.dll".Trim()
                                    .Equals(pluginAssembly.FileName, StringComparison.CurrentCultureIgnoreCase))
                            .ToList();
                    foreach (var plugin in plugins)
                    {
                        if (Options.Instance.HiddenPlugins == null
                            || !Options.Instance.HiddenPlugins.Contains(plugin.Metadata.Name))
                        {
                            if (isc.IsPluginAllowed(plugin.Value.GetType().FullName))
                                DisplayOnePlugin(plugin, ref top, lastWidth);
                        }
                    }
                }
            }
            else
            {
                foreach (var plugin in filteredPlugins.OrderBy(p => p.Metadata.Name))
                {
                    if (Options.Instance.HiddenPlugins == null || !Options.Instance.HiddenPlugins.Contains(plugin.Metadata.Name))
                    {
                        if (isc.IsPluginAllowed(plugin.Value.GetType().FullName))
                            DisplayOnePlugin(plugin, ref top, lastWidth);
                    }
                }
            }

            Invoke(new Action(() =>
            {
                pnlPlugins.Controls.Clear();

                var pluginsToDisplay = pluginsModels.Where(p => filteredPlugins.Contains((Lazy<IXrmToolBoxPlugin, IPluginMetadata>)p.Tag)).ToList();

                foreach (PluginModel ctrl in pluginsToDisplay)
                {
                    ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    pnlPlugins.Controls.Add(ctrl);
                }

                if (!pluginsToDisplay.Any())
                {
                    lblPluginsNotFoundText.Text = string.Format(lblPluginsNotFoundText.Tag.ToString(), filter);
                    pnlNoPluginFound.Visible = true;
                    pnlPlugins.Visible = false;
                }
                else
                {
                    pnlNoPluginFound.Visible = false;
                    pnlPlugins.Visible = true;
                }

                AdaptPluginControlSize();
            }));
        }

        private Image GetImage(string base64ImageContent, bool small = false)
        {
            // Default logo (no-logo)
            var thisAssembly = Assembly.GetExecutingAssembly();
            var logoStream = thisAssembly.GetManifestResourceStream(small ? "XrmToolBox.Images.nologo32.png" : "XrmToolBox.Images.nologo.png");
            if (logoStream == null)
            {
                throw new Exception("Unable to find no-logo stream!");
            }

            var logo = Image.FromStream(logoStream);

            // Replace by plugin logo if specified
            if (!string.IsNullOrEmpty(base64ImageContent))
            {
                var bytes = Convert.FromBase64String(base64ImageContent);
                var ms = new MemoryStream(bytes, 0, bytes.Length);
                ms.Write(bytes, 0, bytes.Length);
                logo = Image.FromStream(ms);
                ms.Close();
            }

            return logo;
        }

        private void llResetSearchFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtSearch.Text = string.Empty;
        }

        private void pbOpenPluginsStore_Click(object sender, System.EventArgs e)
        {
            ActionRequested?.Invoke(this, new PluginsListEventArgs(PluginsListAction.OpenPluginsStore));
        }

        private void PluginsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall
                || e.CloseReason == CloseReason.FormOwnerClosing
                || e.CloseReason == CloseReason.MdiFormClosing
                || e.CloseReason == CloseReason.TaskManagerClosing
                || e.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }

            e.Cancel = true;
        }

        private void PluginsForm_Load(object sender, System.EventArgs e)
        {
            DisplayPlugins();

            txtSearch.AutoCompleteCustomSource.AddRange(PluginManager.Plugins.Where(p => !Options.Instance.HiddenPlugins.Contains(p.Metadata.Name)).Select(p => p.Metadata.Name).ToArray());
        }

        private void Pm_Clicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OpenPluginRequested?.Invoke(this, new PluginEventArgs((PluginModel)sender));
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (sender is PluginModel ctrl)
                {
                    selectedPluginModel = ctrl;
                    cmsOnePlugin.Show(Cursor.Position);
                }
            }
        }

        private async void pManager_PluginsListUpdated(object sender, System.EventArgs e)
        {
            await WaitFileIsCopied();

            pluginsManager.Recompose();
            pluginsModels.Clear();
            DisplayPlugins(filterText);

            if (pluginsManager.ValidationErrors.Count > 0)
            {
            }
        }

        private void pnlNoPluginFound_Resize(object sender, System.EventArgs e)
        {
            pbOpenPluginsStore.Location = new Point((pnlNoPluginFound.Width - pbOpenPluginsStore.Width) / 2, pbOpenPluginsStore.Location.Y);
            llResetSearchFilter.Location = new Point((pnlNoPluginFound.Width - llResetSearchFilter.Width) / 2, llResetSearchFilter.Location.Y);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var name = ((TextBox)sender).Text.ToLower();
                var plugin = pluginsManager.ValidatedPlugins.FirstOrDefault(p => p.Metadata.Name.ToLower().Contains(name));

                if (plugin != null)
                {
                    OpenPluginRequested?.Invoke(this, new PluginEventArgs(plugin));

                    // Clear the textbox
                    txtSearch.Text = string.Empty;
                }
            }
        }

        private void txtSearch_TextChanged(object sender, System.EventArgs e)
        {
            filterText = txtSearch.Text;

            searchThread?.Abort();
            searchThread = new Thread(DisplayPlugins);
            searchThread.Start(filterText);
        }

        private async Task WaitFileIsCopied()
        {
            await Task.Delay(1000);
        }
    }
}