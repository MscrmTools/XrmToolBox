using McTools.Xrm.Connection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.AppCode;
using XrmToolBox.Controls;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Forms;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.New.EventArgs;
using XrmToolBox.Properties;
using XrmToolBox.ToolLibrary.AppCode;

namespace XrmToolBox.New
{
    public partial class PluginsForm2 : DockContent, IToolsForm
    {
        #region Variables

        private readonly PluginManagerExtended pluginsManager;
        private Dictionary<string, List<string>> categoriesList;
        private bool expanded = true;
        private string filterText;
        private List<NavLeftItem> items = new List<NavLeftItem>();
        private NavLeftItem mainItem;
        private int menuWidth;
        private Thread searchThread;
        private ToolLibrary.ToolLibrary store;
        private ToolTip tt = new ToolTip();
        private bool userOrFilterOperatorForCategory;

        #endregion Variables

        private Dictionary<Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>, Rectangle> paypals = new Dictionary<Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>, Rectangle>();

        public PluginsForm2()
        {
            // Set drawing optimizations
            SetStyle(
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.DoubleBuffer,
              true);

            InitializeComponent();

            pluginsManager = PluginManagerExtended.Instance;
            pluginsManager.IsWatchingForNewPlugins = true;
            pluginsManager.Initialize(this);
            pluginsManager.PluginsListUpdated += pManager_PluginsListUpdated;

            Options.Instance.OnSettingsChanged += Instance_OnSettingsChanged;

            pnlNavLeft.MouseWheel += PnlNavLeft_MouseWheel;
            imageList1.ImageSize = new Size(imageList1.ImageSize.Width, Options.Instance.DisplayLargeIcons ? 100 : 50);

            DisplayCategories(null);
            SetCategoriesBottomButtonsDisplay();
            menuWidth = CalculateLeftMenuWidth();
            SetCategoriesDisplay();
        }

        #region Properties

        public ConnectionDetail ConnectionDetail { get; set; }

        public PluginManagerExtended PluginManager => pluginsManager;
        public ToolLibrary.ToolLibrary Store
        { set { store = value; } }

        #endregion Properties

        #region Events

        public event EventHandler<PluginsListEventArgs> ActionRequested;

        public event EventHandler<PluginEventArgs> OpenPluginProjectUrlRequested;

        public event EventHandler<PluginEventArgs> OpenPluginRequested;

        public event EventHandler<PluginEventArgs> UninstallPluginRequested;

        #endregion Events

        public void DisplayCategories(Dictionary<string, List<string>> categories)
        {
            if (categories == null)
            {
                var result = WebRequestHelper.MakeGet("https://www.xrmtoolbox.com/_odata/categories");
                var data = JObject.Parse(result);
                categories = new Dictionary<string, List<string>>();
                foreach (JToken jo in (JArray)data["value"])
                {
                    categories.Add(jo["mctools_name"].ToString(), new List<string>());
                }

                if (categories.Count == 0)
                {
                    pnlNavLeftMain.Visible = false;
                    return;
                }
            }

            pnlNavLeftMain.Visible = true;

            if (categoriesList == null)
            {
                categoriesList = categories;

                int mw = CalculateLeftMenuWidth();
                pnlNavLeft.Width = Options.Instance.ShowCategoriesExpanded ? mw : 46;
                pnlNavLeftMain.Width = pnlNavLeft.Width;
                menuWidth = mw;
            }
            else if (categoriesList.All(c => c.Value.Count == 0))
            {
                categoriesList = categories;

                foreach (var category in categoriesList.Keys)
                {
                    var item = pnlNavLeft.Controls.OfType<NavLeftItem>().FirstOrDefault(nli => nli.Tag?.ToString() == category);
                    if (item != null)
                    {
                        item.Text = $"{category} ({categoriesList[category].Count})";
                        item.Invalidate();
                    }
                }

                int mw = CalculateLeftMenuWidth();
                pnlNavLeft.Width = Options.Instance.ShowCategoriesExpanded ? mw : 46;
                pnlNavLeftMain.Width = pnlNavLeft.Width;
                menuWidth = mw;

                SetCategoriesDisplay();
                DisplayPlugins(txtSearch.Text);

                return;
            }
            else
            {
                return;
            }

            mainItem = new NavLeftItem { Text = "Categories", Index = 1, Height = 40, Image = Resources.left_arrow, Dock = DockStyle.Top, Expanded = Options.Instance.ShowCategoriesExpanded };
            mainItem.OnSelectedChanged += Menu_OnSelectedChanged;
            pnlLeftNavTop.Height = 40;
            pnlLeftNavTop.Controls.Add(mainItem);
            pnlTopSearch.Height = 40;

            int index = 0;

            foreach (var category in categories.Keys.OrderBy(k => k))
            {
                var imageName = category.Replace(" ", "") + "32";
                var image = (Bitmap)Resources.ResourceManager.GetObject(imageName);
                if (image == null)
                {
                    var imagePath = Path.Combine(Paths.XrmToolBoxPath, "ToolLogoCache", $"{imageName}.png");

                    try
                    {
                        image = new Bitmap(Image.FromFile(imagePath));
                    }
                    catch
                    {
                        try
                        {
                            using (WebClient wc = new WebClient())
                            {
                                using (Stream s = wc.OpenRead($"https://www.xrmtoolbox.com/{imageName}.png"))
                                {
                                    image = new Bitmap(s);
                                }
                            }

                            new Bitmap(image).Save(imagePath);
                        }
                        catch (Exception error)
                        { }
                    }
                }

                var item = new NavLeftItem
                {
                    Tag = category,
                    Text = category,
                    Index = index,
                    DisplayIndex = index,
                    Height = 40,
                    Width = 70,
                    Image = image,
                    Location = new Point(0, index * 40)
                };
                item.OnSelectedChanged += Menu_OnSelectedChanged;
                items.Add(item);
                index++;

                tt.SetToolTip(item, category);
            }

            pnlNavLeft.Controls.AddRange(items.ToArray());

            int textSizeWidth = CalculateLeftMenuWidth();

            menuWidth = pnlNavLeftMain.Width;

            SetCategoriesDisplay();
        }

        public void DuplicateTool(string pluginName, IDuplicatableTool sourceTool, object state, OpenMruPluginEventArgs e = null)
        {
            if (e != null)
            {
                pluginName = e.Item.PluginName;
            }

            var plugin = pluginsManager.ValidatedPluginsExt.FirstOrDefault(p => p.Metadata.Name == pluginName);
            if (plugin == null)
            {
                var message = $"Tool '{pluginName}' was not found.\n\nYou can install it from the Tool Library";
                MessageBox.Show(this, message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenPluginRequested?.Invoke(this, e != null ? new PluginEventArgs(e, plugin) { SourceTool = sourceTool, State = state } : new PluginEventArgs(plugin) { SourceTool = sourceTool, State = state });
        }

        public void OpenPlugin(string pluginName, OpenMruPluginEventArgs e = null, bool needNewConnection = false)
        {
            if (e != null)
            {
                pluginName = e.Item.PluginName;
            }
            var plugin = pluginsManager.ValidatedPluginsExt.FirstOrDefault(p => PluginFinderByIdOrName(p, pluginName));
            if (plugin == null)
            {
                var message = $"Tool '{pluginName}' was not found.\n\nYou can install it from the Tool Library";
                MessageBox.Show(this, message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenPluginRequested?.Invoke(this, e != null ? new PluginEventArgs(e, plugin) { NeedNewConnection = needNewConnection } : new PluginEventArgs(plugin) { NeedNewConnection = needNewConnection });
        }

        public void ReloadPluginsList()
        {
            pluginsManager.Recompose();

            var mi = new MethodInvoker(() =>
            {
                txtSearch.AutoCompleteCustomSource.Clear();
                txtSearch.AutoCompleteCustomSource.AddRange(PluginManager.PluginsExt.Where(p => !Options.Instance.HiddenPlugins.Contains(p.Metadata.Name)).Select(p => p.Metadata.Name).ToArray());

                DisplayPlugins(filterText);
            });

            if (InvokeRequired)
            {
                Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        private static bool PluginFinderByIdOrName(Lazy<IXrmToolBoxPlugin, IPluginMetadataExt> plugin, string identifier)
        {
            if (Guid.TryParse(identifier, out Guid pluginid) && !pluginid.Equals(Guid.Empty))
            {
                return plugin.Metadata.Id.Equals(pluginid);
            }
            return plugin.Metadata.Name.Equals(identifier);
        }

        private void AddToFavorites(string pluginName, bool withCurrentConnection = false)
        {
            if (Favorites.Instance.Items.All(i => i.PluginName != pluginName || i.PluginName == pluginName && i.ConnectionId != (withCurrentConnection ? ConnectionDetail?.ConnectionId.Value ?? Guid.Empty : Guid.Empty)))
            {
                Favorites.Instance.Items.Add(new Favorite { PluginName = pluginName, ConnectionId = withCurrentConnection ? ConnectionDetail?.ConnectionId.Value ?? Guid.Empty : Guid.Empty, ConnectionName = withCurrentConnection ? ConnectionDetail?.ConnectionName ?? "" : "" });
                Favorites.Instance.Save();
            }
        }

        private void btnChangeSize_Click(object sender, System.EventArgs e)
        {
            Options.Instance.DisplayLargeIcons = !Options.Instance.DisplayLargeIcons;
            Options.Instance.Save();

            imageList1.ImageSize = new Size(imageList1.ImageSize.Width, Options.Instance.DisplayLargeIcons ? 100 : 50);
            lvTools.Invalidate();

            lvTools_Resize(lvTools, new System.EventArgs());

            SetCategoriesBottomButtonsDisplay();
        }

        private void btnFilterOperator_Click(object sender, System.EventArgs e)
        {
            userOrFilterOperatorForCategory = !userOrFilterOperatorForCategory;
            SetCategoriesBottomButtonsDisplay();
            if (pnlNavLeft.Controls.OfType<NavLeftItem>().Any(nli => nli.Selected))
            {
                DisplayPlugins(txtSearch.Text);
            }
        }

        private int CalculateLeftMenuWidth()
        {
            string maxWidthText = "";
            //foreach (var category in categoriesList.Keys)
            foreach (var category in pnlNavLeft.Controls.OfType<NavLeftItem>().Select(nli => nli.Text))
            {
                if (maxWidthText.Length < category.Length)
                {
                    maxWidthText = category;
                }
            }
            var textSize = TextRenderer.MeasureText(maxWidthText, pnlNavLeft.Font);

            return textSize.Width;
        }

        private void cmsOnePlugin_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (lvTools.SelectedItems.Count == 0) return;

            var selectedPluginModel = lvTools.SelectedItems[0];

            var plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>)selectedPluginModel.Tag;

            if (e.ClickedItem == tsmiOpenProjectHomePage)
            {
                OpenPluginProjectUrlRequested?.Invoke(this, new PluginEventArgs((Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>)selectedPluginModel.Tag));
            }
            else if (e.ClickedItem == tsmiUninstallPlugin)
            {
                UninstallPluginRequested?.Invoke(this, new PluginEventArgs((Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>)selectedPluginModel.Tag));
            }
            else if (e.ClickedItem == tsmiHidePlugin)
            {
                Options.Instance.HiddenPlugins.Add(plugin.Metadata.Name);
                ReloadPluginsList();
            }
            else if (e.ClickedItem == tsmiShortcutTool)
            {
                CreateShortcut(plugin.Metadata.Name);
            }
            else if (e.ClickedItem == tsmiShortcutToolConnection)
            {
                CreateShortcut(plugin.Metadata.Name, ConnectionDetail?.ConnectionName);
            }
            else if (e.ClickedItem == tsmiAddToFavorites)
            {
                AddToFavorites(plugin.Metadata.Name);
            }
            else if (e.ClickedItem == addToFavoritesWithCurrentConnectionToolStripMenuItem)
            {
                AddToFavorites(plugin.Metadata.Name, true);
            }
            else if (e.ClickedItem == tsmiOpenWithNewConection)
            {
                OpenPluginRequested?.Invoke(this, new PluginEventArgs(plugin) { NeedNewConnection = true });
            }
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

        private void DisplayPlugins(object filter = null)
        {
            Thread.Sleep(200);

            var isc = new ItSecurityChecker();
            isc.LoadAllowedPlugins();

            if (IsHandleCreated)
            {
                Invoke(new Action(
                    () =>
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
            }

            if (!pluginsManager.ValidatedPluginsExt?.Any() ?? false)
            {
                Invoke(new Action(() =>
                {
                    pnlHelp.Visible = true;
                }));

                return;
            }

            var categories = pnlNavLeft.Controls.OfType<NavLeftItem>()
                .Where(nli => nli.Selected)
                .Select(nli => nli.Tag.ToString())
                .ToList();

            var availablePlugins = userOrFilterOperatorForCategory ? new List<Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>>() : pluginsManager.ValidatedPluginsExt;

            var list = new List<Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>>();
            foreach (var category in categories)
            {
                var matchingPlugins = pluginsManager.ValidatedPluginsExt.Where(p => categoriesList
                    .Where(c => category == c.Key)
                    .SelectMany(c => c.Value)
                    .ToList().Contains(p.Metadata.Name)
                );

                if (userOrFilterOperatorForCategory)
                {
                    availablePlugins = availablePlugins.Union(matchingPlugins);
                }
                else
                {
                    availablePlugins = availablePlugins.Intersect(matchingPlugins);
                }
            }

            // Search with filter defined
            var filteredPlugins = (filter != null && filter.ToString().Length > 0
                ? availablePlugins.Where(p
                    => p.Metadata.Name.ToLower().Contains(filter.ToString().ToLower())
                    || p.Metadata.Description.ToLower().Contains(filter.ToString().ToLower())
                    || p.Metadata.Company.ToLower().Contains(filter.ToString().ToLower()))
                : availablePlugins)
                .Where(p => isc.IsPluginAllowed(p.Metadata.PluginType))
                .OrderBy(p => p.Metadata.Name).ToList();

            foreach (var item in Options.Instance.MostUsedList.OrderByDescending(i => i.Count).ThenBy(i => i.Name))
            {
                var plugin = filteredPlugins.FirstOrDefault(x => x.Metadata.PluginType == item.Name);
                if (plugin != null) plugin.Metadata.numberOfUse = item.Count;
            }
            if (Options.Instance.PluginsDisplayOrder == DisplayOrder.MostUsed)
            {
                filteredPlugins = filteredPlugins.OrderByDescending(p => p.Metadata.numberOfUse).ToList();
            }
            else if (Options.Instance.PluginsDisplayOrder == DisplayOrder.RecentlyUpdated)
            {
                var pluginAssemblies = Directory.EnumerateFiles(Paths.PluginsPath, "*.dll")
                    .Select(d => new
                    {
                        UpdatedOn = File.GetLastWriteTime(d),
                        FileName = d.Substring(d.LastIndexOf('\\') + 1)
                    })
                    .OrderByDescending(x => x.UpdatedOn);
                foreach (var pluginAssembly in pluginAssemblies)
                {
                    var plugin =
                        filteredPlugins.FirstOrDefault(
                                x => $"{x.Metadata.AssemblyQualifiedName.Split(',')[1]}.dll".Trim()
                                    .Equals(pluginAssembly.FileName, StringComparison.CurrentCultureIgnoreCase));
                    if (plugin != null)
                    {
                        plugin.Metadata.AddedOn = pluginAssembly.UpdatedOn;
                    }
                }

                filteredPlugins = filteredPlugins.OrderByDescending(p => p.Metadata.AddedOn).ToList();
            }
            else if (Options.Instance.PluginsDisplayOrder == DisplayOrder.Rating)
            {
                if (store == null)
                {
                    store = new ToolLibrary.ToolLibrary(Options.Instance, new Dictionary<string, string>());
                }

                if (store.XrmToolBoxPlugins == null)
                {
                    store.LoadTools();
                }

                var storePlugins = store.Tools;

                foreach (XtbPlugin storePlugin in storePlugins.OfType<XtbPlugin>())
                {
                    var tool = filteredPlugins.FirstOrDefault(fp => fp.Metadata.Name == storePlugin.Name);
                    if (tool != null)
                    {
                        tool.Metadata.Rating = storePlugin.TotalFeedbackRating + 1000000 * storePlugin.AverageFeedbackRating;
                    }
                }

                filteredPlugins = filteredPlugins.OrderByDescending(p => p.Metadata.Rating).ToList();
            }

            Invoke(new Action(() =>
            {
                lvTools.Items.Clear();
                lvTools.Items.AddRange(filteredPlugins.Select(
                    fp => new ListViewItem(fp.Metadata.Name) { Tag = fp, ToolTipText = $"{fp.Metadata.Description}\r\n\r\nCategories: {string.Join(", ", fp.Metadata.Categories)}" }
                    ).ToArray());

                if (!filteredPlugins.Any())
                {
                    var filterCategory = categories.Count == 0 ? "" : $" in categor{(categories.Count > 1 ? "ies" : "y")} {string.Join(", ", categories)}";

                    lblPluginsNotFoundText.Text = string.Format(lblPluginsNotFoundText.Tag.ToString(), filterText, filterCategory);
                    pnlNoPluginFound.Visible = true;
                }
                else
                {
                    pnlNoPluginFound.Visible = false;
                }
            }));
        }

        private void DrawItemsBottom()
        {
            pnlNavLeft.Invalidate();
        }

        private Image GetImage(string base64ImageContent, bool small = false)
        {
            Image logo = null;
            // Replace by plugin logo if specified
            if (!string.IsNullOrEmpty(base64ImageContent))
            {
                try
                {
                    var bytes = Convert.FromBase64String(base64ImageContent);
                    var ms = new MemoryStream(bytes, 0, bytes.Length);
                    ms.Write(bytes, 0, bytes.Length);
                    logo = Image.FromStream(ms);
                    ms.Close();
                    ms.Dispose();
                }
                catch (Exception)
                {
                }
            }

            if (logo == null)
            {
                // Default logo (no-logo)
                var thisAssembly = Assembly.GetExecutingAssembly();
                var logoStream = thisAssembly.GetManifestResourceStream(small ? "XrmToolBox.Images.nologo32.png" : "XrmToolBox.Images.nologo.png");
                if (logoStream == null)
                {
                    throw new Exception("Unable to find no-logo stream!");
                }

                logo = Image.FromStream(logoStream);
            }

            return logo;
        }

        private void Instance_OnSettingsChanged(object sender, SettingsPropertyEventArgs e)
        {
            if (e.PropertyName == nameof(Options.Instance.IconDisplayMode)
                || e.PropertyName == nameof(Options.Instance.ShowCategoriesExpanded)
                || e.PropertyName == nameof(Options.Instance.PluginsDisplayOrder)
                || e.PropertyName == nameof(Options.Instance.HiddenPlugins)
                || e.PropertyName == nameof(Options.Instance.NumberOfDaysToShowNewRibbon)
                || e.PropertyName == nameof(Options.Instance.MostUsedList))
            {
                if (e.PropertyName == nameof(Options.Instance.IconDisplayMode))
                {
                    imageList1.ImageSize = new Size(imageList1.ImageSize.Width, ((DisplayIcons)e.Value) == DisplayIcons.Large ? 100 : 50);
                    SetCategoriesBottomButtonsDisplay();
                }

                if (e.PropertyName == nameof(Options.Instance.ShowCategoriesExpanded))
                {
                    mainItem.Expanded = (bool)e.Value;
                    SetCategoriesDisplay();
                    SetCategoriesBottomButtonsDisplay();
                }

                DisplayPlugins(filterText);
            }
        }

        private void llResetSearchFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            ResetCategories();
            txtSearch_TextChanged(txtSearch, new System.EventArgs());
        }

        private void lvTools_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            var ti = (Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>)e.Item.Tag;
            var imageSize = Options.Instance.DisplayLargeIcons ? 64 : 32;

            var backColor = ColorTranslator.FromHtml(ti.Metadata.BackgroundColor);
            var primaryColor = ColorTranslator.FromHtml(ti.Metadata.PrimaryFontColor);
            var secondaryColor = ColorTranslator.FromHtml(ti.Metadata.SecondaryFontColor);
            var logo = GetImage(Options.Instance.DisplayLargeIcons ? ti.Metadata.BigImageBase64 : ti.Metadata.BigImageBase64, !Options.Instance.DisplayLargeIcons);
            var time = new FileInfo(ti.Metadata.AssemblyFilename).CreationTime;

            int myOffSet = 0;
            e.Graphics.FillRectangle(new SolidBrush(backColor), new Rectangle(new Point(e.Bounds.X, e.Bounds.Y), new Size(e.Bounds.Width, e.Bounds.Height - 4)));

            e.Graphics.DrawImage(logo.ResizeImage(imageSize, imageSize), new Point(e.Bounds.X + 10, e.Bounds.Y + e.Bounds.Height / 2 - imageSize / 2));
            myOffSet += imageSize + 20;

            using (var colorBrush = new SolidBrush(primaryColor))
            {
                using (var smallFont = new Font(e.Item.ListView.Font.FontFamily, e.Item.ListView.Font.Size - (Options.Instance.DisplayLargeIcons ? 2 : 4)))
                {
                    if (Options.Instance.DisplayLargeIcons)
                    {
                        e.Graphics.DrawString(ti.Metadata.Name, e.Item.ListView.Font, colorBrush, new Point(e.Bounds.X + myOffSet, e.Bounds.Y));
                        e.Graphics.DrawString(ti.Metadata.Version, smallFont, colorBrush, new Point(e.Bounds.X + myOffSet, e.Bounds.Y + 30));
                        e.Graphics.DrawString("Author(s) : " + ti.Metadata.Company, smallFont, colorBrush, new Point(e.Bounds.X + myOffSet, e.Bounds.Y + 50));
                        e.Graphics.DrawString(ti.Metadata.Description, smallFont, colorBrush, new Point(e.Bounds.X + myOffSet, e.Bounds.Y + 70));

                        var numberOfUseSize = TextRenderer.MeasureText(ti.Metadata.numberOfUse.ToString(), smallFont);
                        e.Graphics.DrawString(ti.Metadata.numberOfUse.ToString(), smallFont, colorBrush, new Point(e.Bounds.X + e.Bounds.Width - numberOfUseSize.Width, e.Bounds.Y));

                        if (DateTime.Now - time < new TimeSpan(Options.Instance.NumberOfDaysToShowNewRibbon, 0, 0, 0))
                        {
                            e.Graphics.DrawImage(Resources.New32.ResizeImage(32, 32), new Point(e.Bounds.X + e.Bounds.Width - 40, e.Bounds.Y + e.Bounds.Height / 2 - 18));
                        }

                        if (ti.Metadata.Interfaces.Contains("IPayPalPlugin"))
                        {
                            e.Graphics.DrawImage(Resources.paypal.ResizeImage(32, 32), new Point(e.Bounds.X + e.Bounds.Width - 80, e.Bounds.Y + e.Bounds.Height / 2 - 18));

                            paypals.Remove(ti);
                            paypals.Add(ti, new Rectangle(e.Bounds.X + e.Bounds.Width - 80, e.Bounds.Y + e.Bounds.Height / 2 - 18, 32, 32));
                        }
                    }
                    else
                    {
                        e.Graphics.DrawString(ti.Metadata.Name, e.Item.ListView.Font, colorBrush, new Point(e.Bounds.X + myOffSet, e.Bounds.Y + 10));
                        myOffSet += TextRenderer.MeasureText(ti.Metadata.Name, e.Item.ListView.Font).Width;

                        using (var secondColorBrush = new SolidBrush(secondaryColor))
                        {
                            e.Graphics.DrawString("by " + ti.Metadata.Company, smallFont, secondColorBrush, new Point(e.Bounds.X + myOffSet, e.Bounds.Y + 15));
                            myOffSet += TextRenderer.MeasureText("by " + ti.Metadata.Company, smallFont).Width;
                        }

                        e.Graphics.DrawString(ti.Metadata.Version, e.Item.ListView.Font, colorBrush, new Point(e.Bounds.X + myOffSet, e.Bounds.Y + 10));
                        myOffSet += TextRenderer.MeasureText(ti.Metadata.Version, e.Item.ListView.Font).Width;

                        var numberOfUseSize = TextRenderer.MeasureText(ti.Metadata.numberOfUse.ToString(), smallFont);
                        e.Graphics.DrawString(ti.Metadata.numberOfUse.ToString(), smallFont, colorBrush, new Point(e.Bounds.X + e.Bounds.Width - numberOfUseSize.Width, e.Bounds.Y));

                        if (DateTime.Now - time < new TimeSpan(Options.Instance.NumberOfDaysToShowNewRibbon, 0, 0, 0))
                        {
                            e.Graphics.DrawImage(Resources.New32.ResizeImage(32, 32), new Point(e.Bounds.X + myOffSet, e.Bounds.Y + e.Bounds.Height / 2 - 18));
                            myOffSet += 40;
                        }

                        if (ti.Metadata.Interfaces.Contains("IPayPalPlugin"))
                        {
                            e.Graphics.DrawImage(Resources.paypal.ResizeImage(32, 32), new Point(e.Bounds.X + e.Bounds.Width - 70, e.Bounds.Y + e.Bounds.Height / 2 - 18));
                            paypals.Remove(ti);
                            paypals.Add(ti, new Rectangle(e.Bounds.X + e.Bounds.Width - 70, e.Bounds.Y + e.Bounds.Height / 2 - 18, 32, 32));
                        }
                    }
                }
            }
        }

        private void lvTools_MouseClick(object sender, MouseEventArgs e)
        {
            var item = lvTools.GetItemAt(e.X, e.Y);
            var plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>)item.Tag;

            if (e.Button == MouseButtons.Left)
            {
                if (paypals.Values.Any(r => r.X < e.X - item.Position.X && r.X + r.Width > e.X - item.Position.X && r.Y < e.Y - item.Position.Y && r.Y + r.Height > e.Y - item.Position.Y))
                {
                    if (plugin.Value is IPayPalPlugin pp)
                    {
                        using (var dialog = new CurrencySelectionDialog(pp))
                        {
                            dialog.ShowDialog(Parent);
                        }
                        return;
                    }
                }

                OpenPluginRequested?.Invoke(this, new PluginEventArgs(plugin));
            }
            else if (e.Button == MouseButtons.Right)
            {
                cmsOnePlugin.Show(Cursor.Position);
            }
        }

        private void lvTools_Resize(object sender, System.EventArgs e)
        {
            if (lvTools.Height > lvTools.Items.Count * imageList1.ImageSize.Height)
            {
                lvTools.Columns[0].Width = lvTools.Width;
            }
            else
            {
                lvTools.Columns[0].Width = lvTools.Width - 20;
            }
        }

        private void Menu_OnSelectedChanged(object sender, System.EventArgs e)
        {
            var item = (NavLeftItem)sender;

            if (item.Text == "Categories")
            {
                item.Expanded = !item.Expanded;

                Options.Instance.ShowCategoriesExpanded = item.Expanded;
                Options.Instance.Save();

                SetCategoriesDisplay();

                return;
            }

            ReloadPluginsList();
        }

        private void pbOpenPluginsStore_Click(object sender, System.EventArgs e)
        {
            ActionRequested?.Invoke(this, new PluginsListEventArgs(PluginsListAction.OpenPluginsStore));
        }

        private void PluginsForm2_FormClosing(object sender, FormClosingEventArgs e)
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

        private void PluginsForm2_Load(object sender, System.EventArgs e)
        {
            DisplayPlugins();

            txtSearch.AutoCompleteCustomSource.AddRange(PluginManager.PluginsExt.Where(p => !Options.Instance.HiddenPlugins.Contains(p.Metadata.Name)).Select(p => p.Metadata.Name).ToArray());
        }

        private async void pManager_PluginsListUpdated(object sender, System.EventArgs e)
        {
            await WaitFileIsCopied();

            pluginsManager.Recompose();
            DisplayPlugins(filterText);

            txtSearch.AutoCompleteCustomSource.AddRange(PluginManager.PluginsExt.Where(p => !Options.Instance.HiddenPlugins.Contains(p.Metadata.Name)).Select(p => p.Metadata.Name).ToArray());
        }

        private void PnlNavLeft_MouseWheel(object sender, MouseEventArgs e)
        {
            var ctrls = pnlNavLeft.Controls.OfType<NavLeftItem>().OrderByDescending(c => c.Selected).ThenBy(c => c.Index);

            var lastControlY = ctrls.LastOrDefault()?.Location.Y + ctrls.LastOrDefault()?.Height;
            var firstControlY = ctrls.FirstOrDefault()?.Location.Y;

            if (e.Delta < 0)
            {
                while (lastControlY > pnlNavLeft.Height)
                {
                    foreach (Control ctrl in pnlNavLeft.Controls)
                    {
                        ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - 10);
                    }

                    lastControlY = ctrls.FirstOrDefault()?.Location.Y + ctrls.FirstOrDefault()?.Height;

                    DrawItemsBottom();
                }
            }
            else
            {
                while (firstControlY < 0)
                {
                    foreach (Control ctrl in pnlNavLeft.Controls)
                    {
                        ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + 10);
                    }

                    firstControlY = ctrls.LastOrDefault()?.Location.Y;
                }
            }
        }

        private void ResetCategories()
        {
            foreach (var categoryItem in pnlNavLeft.Controls.OfType<NavLeftItem>().Where(nli => nli.Selected))
            {
                categoryItem.Selected = false;
            }
        }

        private void SetCategoriesBottomButtonsDisplay()
        {
            pnlLeftBottom.Height = 100;
            btnChangeSize.Height = 40;
            btnChangeSize.ImageAlign = expanded ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleCenter;
            btnChangeSize.Text = expanded ? (Options.Instance.DisplayLargeIcons ? "Use small layout" : "Use big layout") : "";
            btnChangeSize.Image = Options.Instance.DisplayLargeIcons ? Resources.Shrink32.ResizeImage(24, 24) : Resources.Expand32.ResizeImage(24, 24);
            btnChangeSize.FlatStyle = FlatStyle.Flat;
            btnChangeSize.FlatAppearance.BorderSize = 0;

            btnFilterOperator.Height = 40;
            btnFilterOperator.Font = new Font(btnFilterOperator.Font, expanded ? FontStyle.Regular : FontStyle.Bold);
            btnFilterOperator.Text = expanded ? (userOrFilterOperatorForCategory ? "Use AND operator" : "Use OR operator") : (userOrFilterOperatorForCategory ? "& &" : "||");
            btnFilterOperator.FlatStyle = FlatStyle.Flat;
            btnFilterOperator.FlatAppearance.BorderSize = 0;
            tt.SetToolTip(btnFilterOperator, userOrFilterOperatorForCategory ? "Use AND operator" : "Use OR operator");
        }

        private void SetCategoriesDisplay()
        {
            expanded = mainItem.Expanded;

            mainItem.Image = mainItem.Expanded ? Resources.left_arrow : Resources.right_arrow;

            pnlNavLeftMain.Width = mainItem.Expanded ? menuWidth + 70 : 46;
            pnlNavLeft.Width = mainItem.Expanded ? menuWidth + 70 : 46;

            foreach (var navItem in pnlNavLeft.Controls.OfType<NavLeftItem>())
            {
                navItem.Expanded = mainItem.Expanded;
                navItem.Width = mainItem.Expanded ? menuWidth + 70 : 46;
                navItem.Invalidate();
            }

            SetCategoriesBottomButtonsDisplay();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var name = ((TextBox)sender).Text.ToLower();
                var plugin = pluginsManager.ValidatedPluginsExt.FirstOrDefault(p => p.Metadata.Name.ToLower().Contains(name));

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