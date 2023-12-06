using McTools.Xrm.Connection;
using McTools.Xrm.Connection.WinForms.AppCode;
using OrderedPropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;
using System.Xml.Serialization;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Forms;

namespace XrmToolBox
{
    public enum DisplayIcons
    {
        [Description("Large")]
        Large,

        [Description("Small")]
        Small
    }

    public enum DisplayOrder
    {
        [Description("Alphabetically")]
        Alphabetically,

        [Description("Most used")]
        MostUsed,

        [Description("Recently updated")]
        RecentlyUpdated,

        [Description("By Rating")]
        Rating
    }

    public enum DisplayPluginsStoreOnStartup
    {
        [Description("Always")]
        Always,

        [Description("Only if updates")]
        OnlyIfUpdates,

        [Description("Never")]
        Never
    }

    public enum ThemeName
    {
        [Description("Classic theme")]
        Classic,

        [Description("Blue theme")]
        Blue,

        [Description("Light theme")]
        Light,

        [Description("Dark theme")]
        Dark
    }

    public class FormSize
    {
        public Size CurrentSize
        {
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public int Height { get; set; }
        public bool IsMaximized { get; set; }
        public int Width { get; set; }

        public void ApplyFormSize(Form form)
        {
            var size = new Size(Width, Height);

            if (Screen.PrimaryScreen.WorkingArea.Height < size.Height)
            {
                size.Height = Screen.PrimaryScreen.WorkingArea.Height;
            }

            if (Screen.PrimaryScreen.WorkingArea.Width < size.Width)
            {
                size.Width = Screen.PrimaryScreen.WorkingArea.Width;
            }

            form.Invoke(new Action(() =>
            {
                if (form.MinimumSize.Width > size.Width)
                {
                    size.Width = form.MinimumSize.Width;
                }

                if (form.MinimumSize.Height > size.Height)
                {
                    size.Height = form.MinimumSize.Height;
                }

                if (size.Height != 0)
                {
                    form.Size = size;
                    form.Top = (Screen.PrimaryScreen.WorkingArea.Height - size.Height) / 2;
                    form.Left = (Screen.PrimaryScreen.WorkingArea.Width - size.Width) / 2;
                }
            }));
        }
    }

    public class Options : ICloneable, IToolLibrarySettings, IConnectionControlSettings
    {
        private static Options innerOptions;

        public Options()
        {
            CheckUpdateOnStartup = true;
            IconDisplayMode = DisplayIcons.Large;
            DisplayMostUsedFirst = false;
            DisplayRecentlyUpdatedFirst = false;
            Size = new FormSize();
            MostUsedList = new List<PluginUseCount>();
            AllowLogUsage = null;
            DisplayPluginsStoreOnStartup = false;
            HiddenPlugins = new List<string>();
        }

        public event EventHandler<SettingsPropertyEventArgs> OnSettingsChanged;

        public static Options Instance
        {
            get
            {
                if (innerOptions == null)
                {
                    if (!Load(out innerOptions, out var message))
                    {
                        throw new Exception(message);
                    }
                }

                return innerOptions;
            }
        }

        #region Properties

        #region ToolLibrary

        [Category("Tool Library")]
        [DisplayName("Additional repositories")]
        [Description("Additional repositories where to find tools. One line per repository. Format: <repository name>|<repository path>")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [PropertyOrder(2)]
        public string AdditionalRepositories { get; set; }

        [Browsable(false)] public bool LibraryFilterMvp { get; set; }

        [Browsable(false)] public bool LibraryFilterNew { get; set; }

        [Browsable(false)] public bool LibraryFilterOpenSource { get; set; }

        [Browsable(false)] public bool LibraryFilterRating { get; set; }

        [Browsable(false)] public bool LibraryShowIncompatible { get; set; }

        [Browsable(false)] public bool LibraryShowInstalled { get; set; } = true;

        [Browsable(false)] public bool LibraryShowNotInstalled { get; set; } = true;

        [Browsable(false)] public bool LibraryShowUpdates { get; set; } = true;

        [Category("Tool Library")]
        [DisplayName("Most Rated - Minimum number of votes")]
        [Description("Indicates the minimum number of votes for a tool to be considered as a most rated tool")]
        [PropertyOrder(3)]
        public int MostRatedMinNumberOfVotes { get; set; } = 10;

        [Category("Tool Library")]
        [DisplayName("Most Rated - Minimum average rate")]
        [Description("Indicates the minimum average rate for a tool to be considered as a most rated tool")]
        [PropertyOrder(4)]
        public decimal MostRatedMinRatingAverage { get; set; } = (decimal)4.5;

        [Category("Tool Library")]
        [DisplayName("Repository Url")]
        [Description("Repository Url for tools list. You can use your own if needed")]
        [PropertyOrder(1)]
        public string RepositoryUrl { get; set; } = "https://www.xrmtoolbox.com/_odata/plugins";

        #endregion ToolLibrary

        #region Startup

        [Category("Startup")]
        [DisplayName("Bring to top")]
        [Description("Make XrmToolBox the top application when it successfuly started")]
        public bool BringToTop { get; set; } = true;

        [Category("Startup")]
        [DisplayName("Check for update")]
        [Description("Check if update is available for XrmToolBox application")]
        public bool CheckUpdateOnStartup
        {
            get => !DoNotCheckForUpdates;
            set => DoNotCheckForUpdates = !value;
        }

        [Browsable(false)]
        public bool DisplayPluginsStoreOnlyIfUpdates { get; set; }

        [Browsable(false)]
        public bool DisplayPluginsStoreOnStartup { get; set; }

        [Category("Startup")]
        [DisplayName("Open Tool Library")]
        [Description("Indicates when Tool Library should be opened when XrmToolBox starts")]
        [TypeConverter(typeof(CustomEnumConverter))]
        [XmlIgnore]
        public DisplayPluginsStoreOnStartup DisplayStoreOnStartup
        {
            get
            {
                if (DisplayPluginsStoreOnStartup && DisplayPluginsStoreOnlyIfUpdates)
                    return XrmToolBox.DisplayPluginsStoreOnStartup.OnlyIfUpdates;
                if (DisplayPluginsStoreOnStartup && !DisplayPluginsStoreOnlyIfUpdates)
                    return XrmToolBox.DisplayPluginsStoreOnStartup.Always;

                return XrmToolBox.DisplayPluginsStoreOnStartup.Never;
            }
            set
            {
                switch (value)
                {
                    case XrmToolBox.DisplayPluginsStoreOnStartup.Always:
                        DisplayPluginsStoreOnStartup = true;
                        DisplayPluginsStoreOnlyIfUpdates = false;
                        break;

                    case XrmToolBox.DisplayPluginsStoreOnStartup.OnlyIfUpdates:
                        DisplayPluginsStoreOnStartup = true;
                        DisplayPluginsStoreOnlyIfUpdates = true;
                        break;

                    case XrmToolBox.DisplayPluginsStoreOnStartup.Never:
                        DisplayPluginsStoreOnStartup = false;
                        DisplayPluginsStoreOnlyIfUpdates = false;
                        break;
                }
            }
        }

        [Category("Startup")]
        [DisplayName("Show tools list first")]
        [Description("Indicates if tools list is displayed at startup")]
        public bool DisplayToolsListFirst { get; set; }

        [Browsable(false)]
        public bool DoNotCheckForUpdates { get; set; }

        [Category("Startup")]
        [DisplayName("Show tools update notification")]
        [Description("Indicates if a notification should be displayed when updates are present for installed tools")]
        public bool ShowPluginUpdatesPanelAtStartup { get; set; } = true;

        #endregion Startup

        #region Log usage

        [Browsable(false)] public bool? AllowLogUsage { get; set; }

        #endregion Log usage

        #region Close Behavior

        [Category("Close behavior")]
        [DisplayName("Close tool silently")]
        [Description("When closing an individual plugin, do not prompt to confirm tool close")]
        public bool CloseEachPluginSilently { get; set; }

        [Category("Close behavior")]
        [DisplayName("Close XrmToolBox silently")]
        [Description("When closing XrmToolBox, do not prompt to confirm opened tools close")]
        public bool CloseOpenedPluginsSilently { get; set; }

        [Category("Close behavior")]
        [DisplayName("Shutdown silently")]
        [Description("When Windows shuts down and XrmToolBox have opened tools, do not prompt to confirm opened tools close")]
        public bool ClosePluginsSilentlyOnWindowsShutdown { get; set; } = true;

        #endregion Close Behavior

        #region Connection controls

        private bool showMostRecentConnections;
        private bool showSearchBarInCompactSelector;
        private bool useDetailsViewForConnectionManager;
        private bool useDetailsViewForConnectionSelector;

        [Category("Connection controls")]
        [DisplayName("Search pre release updates")]
        [Description("Allow XrmToolBox to search pre release update (alpha, beta, etc.) of connection controls")]
        [PropertyOrder(1)]
        public bool ConnectionControlsAllowPreReleaseUpdates
        {
            get; set;
        }

        [ReadOnly(true)]
        [Category("Connection controls")]
        [DisplayName("Version")]
        [Description("Current version of connection controls")]
        [PropertyOrder(2)]
        public string ConnectionControlsVersion
        {
            get; set;
        }

        [Browsable(false)]
        public int DisplaySizeFactor { get; set; }

        [Category("Connection controls")]
        [DisplayName("Display all connections")]
        [Description("Indicates if bottom left connection control should display all connections regardless the connection files they come from")]
        [PropertyOrder(4)]
        public bool MergeConnectionFiles
        {
            get; set;
        }

        [Category("Connection controls")]
        [DisplayName("Number of recent connections")]
        [Description("Indicates number of connections to display when using the display mode \"Most recently used connections\" in Connection Selector")]
        [PropertyOrder(5)]
        public int NumberOfRecentConnectionsToDisplay
        {
            get; set;
        } = 10;

        [Category("Connection controls")]
        [DisplayName("Reuse connections")]
        [Description("Indicates if connecting to an already connected organization should instanciate a new connection or use the existing one")]
        [PropertyOrder(3)]
        public bool ReuseConnections
        {
            get; set;
        }

        [Category("Connection controls")]
        [DisplayName("Show most recent connections")]
        [Description("Indicates if connection selector should display \"Most recently used connections\" or all connections")]
        [PropertyOrder(6)]
        public bool ShowMostRecentConnections
        {
            get => showMostRecentConnections;
            set
            {
                showMostRecentConnections = value;
                OnSettingsChanged?.Invoke(this, new SettingsPropertyEventArgs(nameof(ShowMostRecentConnections), value));
            }
        }

        [Category("Connection controls")]
        [DisplayName("Show search bar")]
        [Description("Indicates if search bar should be displayed in Connection Selector")]
        [PropertyOrder(7)]
        public bool ShowSearchBarInCompactSelector
        {
            get => showSearchBarInCompactSelector;
            set
            {
                showSearchBarInCompactSelector = value;
                OnSettingsChanged?.Invoke(this, new SettingsPropertyEventArgs(nameof(ShowSearchBarInCompactSelector), value));
            }
        }

        [Category("Connection controls")]
        [DisplayName("Use Details view for Connection Manager")]
        [Description("Indicates if Connection Manager should display the details view of connections or the compact view")]
        [PropertyOrder(8)]
        public bool UseDetailsViewForConnectionManager
        {
            get => useDetailsViewForConnectionManager;
            set
            {
                useDetailsViewForConnectionManager = value;
                OnSettingsChanged?.Invoke(this, new SettingsPropertyEventArgs(nameof(UseDetailsViewForConnectionManager), value));
            }
        }

        [Category("Connection controls")]
        [DisplayName("Use Details view for Connection Selector")]
        [Description("Indicates if Connection Selector should display the details view of connections or the compact view")]
        [PropertyOrder(9)]
        public bool UseDetailsViewForConnectionSelector
        {
            get => useDetailsViewForConnectionSelector;
            set
            {
                useDetailsViewForConnectionSelector = value;
                OnSettingsChanged?.Invoke(this, new SettingsPropertyEventArgs(nameof(UseDetailsViewForConnectionSelector), value));
            }
        }

        #endregion Connection controls

        #region Display

        [Browsable(false)]
        [Obsolete]
        public bool DisplayMostUsedFirst { get; set; }

        [Browsable(false)]
        public string DisplayOrder
        {
            get
            {
                switch (PluginsDisplayOrder)
                {
                    case XrmToolBox.DisplayOrder.Alphabetically:
                        return "Alphabetically";

                    case XrmToolBox.DisplayOrder.MostUsed:
                        return "Most used";

                    case XrmToolBox.DisplayOrder.RecentlyUpdated:
                        return "Recently updated";

                    case XrmToolBox.DisplayOrder.Rating:
                        return "Rating";
                }

                return string.Empty;
            }
            set => PluginsDisplayOrder = value == "Alphabetically"
                    ? XrmToolBox.DisplayOrder.Alphabetically
                : value == "Most used"
                    ? XrmToolBox.DisplayOrder.MostUsed
                : value == "Rating"
                    ? XrmToolBox.DisplayOrder.Rating
                    : XrmToolBox.DisplayOrder.RecentlyUpdated;
        }

        [Browsable(false)]
        public bool DisplayRecentlyUpdatedFirst { get; set; }

        [Browsable(false)]
        public string Theme { get; set; } = "Light theme";

        [Category("Display")]
        [DisplayName("Theme")]
        [Description("Indicates the theme to use with XrmToolBox (restart required)")]
        [TypeConverter(typeof(CustomEnumConverter))]
        [XmlIgnore]
        public ThemeName ThemeValue
        {
            get
            {
                if (Theme == "Light theme")
                    return ThemeName.Light;
                if (Theme == "Dark theme")
                    return ThemeName.Dark;
                if (Theme == "Blue theme")
                    return ThemeName.Blue;
                return ThemeName.Classic;
            }
            set
            {
                if (value == ThemeName.Light)
                    Theme = "Light theme";
                else if (value == ThemeName.Dark)
                    Theme = "Dark theme";
                else if (value == ThemeName.Blue)
                    Theme = "Blue theme";
                else
                    Theme = "Classic theme";
            }
        }

        #endregion Display

        #region Start Page

        [Category("Start Page")]
        [DisplayName("Forget tools without connection")]
        [Description("Do not remember tools opened without connection in Most Recently Used tools")]
        public bool DoNotRememberPluginsWithoutConnection { get; set; }

        [Category("Start Page")]
        [DisplayName("Hide at startup")]
        [Description("Indicates if Start page should be opened or not when XrmToolBox starts up")]
        public bool DoNotShowStartPage { get; set; }

        [Category("Start Page")]
        [DisplayName("Tools to display")]
        [Description("Indicates number of tools to display in Moste Recently Used items section of Start Page")]
        public int MruItemsToDisplay { get; set; } = 10;

        #endregion Start Page

        #region Tools list display

        private bool doNotUseToolColors;
        private bool showCategoriesExpanded;

        [Browsable(false)]
        public bool DisplayLargeIcons
        {
            get => IconDisplayMode == DisplayIcons.Large;
            set
            {
                IconDisplayMode = value ? DisplayIcons.Large : DisplayIcons.Small;
                OnSettingsChanged?.Invoke(this, new SettingsPropertyEventArgs(nameof(IconDisplayMode), IconDisplayMode));
            }
        }

        [Category("Tools list display")]
        [DisplayName("Do not use tools color")]
        [Description("If enabled, Tools list will be displayed with homogeneous colors for all tools")]
        public bool DoNotUseToolColors
        {
            get => doNotUseToolColors;
            set
            {
                doNotUseToolColors = value;
                OnSettingsChanged?.Invoke(this, new SettingsPropertyEventArgs(nameof(DoNotUseToolColors), value));
            }
        }

        [Category("Tools list display")]
        [DisplayName("Hidden tools")]
        [Description("Defines the installed tools that should not be displayed in the tools list")]
        [Editor(typeof(HiddenPluginsEditor), typeof(UITypeEditor))]
        public List<string> HiddenPlugins { get; set; }

        [Category("Tools list display")]
        [DisplayName("Icons size")]
        [TypeConverter(typeof(CustomEnumConverter))]
        [XmlIgnore]
        public DisplayIcons IconDisplayMode { get; set; }

        [Category("Tools list display")]
        [DisplayName("New ribbon display duration")]
        [Description("Number of days after having installed a tool a \"NEW\" ribbon is displayed on the tool")]
        public int NumberOfDaysToShowNewRibbon { get; set; } = 7;

        [Category("Tools list display")]
        [DisplayName("Order")]
        [TypeConverter(typeof(CustomEnumConverter))]
        [XmlIgnore]
        public DisplayOrder PluginsDisplayOrder { get; set; }

        [Category("Tools list display")]
        [DisplayName("Show categories expanded")]
        public bool ShowCategoriesExpanded
        {
            get => showCategoriesExpanded;
            set
            {
                showCategoriesExpanded = value;
                OnSettingsChanged?.Invoke(this, new SettingsPropertyEventArgs(nameof(ShowCategoriesExpanded), value));
            }
        }

        [Category("Tools list display")]
        [DisplayName("Use legacy Tools list")]
        [Description("Indicates if tools list should be displayed using legacy layout or new layout (restart required)")]
        public bool UseLegacyToolsList { get; set; }

        #endregion Tools list display

        #region Others

        [Browsable(false)]
        public DateTime LastAdvertisementDisplay { get; set; }

        [Browsable(false)]
        public string LastConnection { get; set; }

        [Browsable(false)]
        public string LastPlugin { get; set; }

        [Browsable(false)]
        public DateTime LastUpdateCheck { get; set; }

        [Browsable(false)]
        public List<PluginUseCount> MostUsedList { get; set; }

        [Browsable(false)]
        public bool OptinForApplicationInsights { get; set; } = true;

        [Browsable(false)]
        public List<string> OrderForSettingsTab { get; set; } = new List<string>();

        [Browsable(false)]
        public DockState PluginsListDocking { get; set; } = DockState.Document;

        [Browsable(false)]
        public bool PluginsListIsHidden { get; set; } = false;

        [Browsable(false)]
        public bool? PluginsStoreShowIncompatible { get; set; }

        [Browsable(false)]
        public bool? PluginsStoreShowInstalled { get; set; }

        [Browsable(false)]
        public bool? PluginsStoreShowNew { get; set; }

        [Browsable(false)]
        public bool? PluginsStoreShowUpdates { get; set; }

        [Browsable(false)]
        public List<PluginUpdateSkip> PluginsUpdateSkip { get; set; } = new List<PluginUpdateSkip>();

        [XmlElement("FormSize")]
        [Browsable(false)]
        public FormSize Size { get; set; }

        #endregion Others

        #region Session

        [Category("Session")]
        [DisplayName("Remember session")]
        [Description("Indicates if the current session must be saved to be reused at the next XrmToolBox startup")]
        public bool RememberSession { get; set; }

        #endregion Session

        #region Proxy

        [Browsable(false)]
        public bool ByPassProxyOnLocal { get; set; }

        [Browsable(false)]
        public string Password { get; set; }

        [Browsable(false)]
        public string ProxyAddress { get; set; }

        [Browsable(false)]
        public bool UseCustomProxy { get; set; }

        [Browsable(false)]
        public bool UseDefaultCredentials { get; set; }

        [Browsable(false)]
        public bool UseInternetExplorerProxy { get; set; }

        [Browsable(false)]
        public string UserName { get; set; }

        #endregion Proxy

        #region Methods

        public static bool Load(out Options options, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var settingsFile = Path.Combine(Paths.SettingsPath, "XrmToolBox.Settings.xml");

            if (File.Exists(settingsFile))
            {
                try
                {
                    var document = new XmlDocument();
                    document.Load(settingsFile);

                    options = (Options)XmlSerializerHelper.Deserialize(document.OuterXml, typeof(Options));

                    return true;
                }
                catch (Exception error)
                {
                    errorMessage = error.Message;
                    options = new Options();
                    return false;
                }
            }

            options = new Options();
            return true;
        }

        public object Clone()
        {
            return new Options
            {
                CheckUpdateOnStartup = CheckUpdateOnStartup,
                DisplayLargeIcons = DisplayLargeIcons,
                DisplayMostUsedFirst = DisplayMostUsedFirst,
                DisplayRecentlyUpdatedFirst = DisplayRecentlyUpdatedFirst,
                MostUsedList = MostUsedList,
                LastAdvertisementDisplay = LastAdvertisementDisplay,
                HiddenPlugins = HiddenPlugins,
                LastUpdateCheck = LastUpdateCheck,
                AllowLogUsage = AllowLogUsage,
                CloseEachPluginSilently = CloseEachPluginSilently,
                CloseOpenedPluginsSilently = CloseOpenedPluginsSilently,
                ClosePluginsSilentlyOnWindowsShutdown = ClosePluginsSilentlyOnWindowsShutdown,
                DisplayPluginsStoreOnStartup = DisplayPluginsStoreOnStartup,
                DisplayPluginsStoreOnlyIfUpdates = DisplayPluginsStoreOnlyIfUpdates,
                DoNotCheckForUpdates = DoNotCheckForUpdates,
                MergeConnectionFiles = MergeConnectionFiles,
                DisplayOrder = DisplayOrder,
                ShowPluginUpdatesPanelAtStartup = ShowPluginUpdatesPanelAtStartup,
                PluginsStoreShowIncompatible = PluginsStoreShowIncompatible,
                PluginsStoreShowInstalled = PluginsStoreShowInstalled,
                PluginsStoreShowNew = PluginsStoreShowNew,
                PluginsStoreShowUpdates = PluginsStoreShowUpdates,
                MruItemsToDisplay = MruItemsToDisplay,
                DoNotRememberPluginsWithoutConnection = DoNotRememberPluginsWithoutConnection,
                DoNotShowStartPage = DoNotShowStartPage,
                Theme = Theme,
                PluginsUpdateSkip = PluginsUpdateSkip,
                OptinForApplicationInsights = OptinForApplicationInsights,
                ConnectionControlsVersion = ConnectionControlsVersion,
                ConnectionControlsAllowPreReleaseUpdates = ConnectionControlsAllowPreReleaseUpdates,
                NumberOfDaysToShowNewRibbon = NumberOfDaysToShowNewRibbon,
                RememberSession = RememberSession,
                DisplayStoreOnStartup = DisplayStoreOnStartup,
                IconDisplayMode = IconDisplayMode,
                LastConnection = LastConnection,
                LastPlugin = LastPlugin,
                PluginsDisplayOrder = PluginsDisplayOrder,
                PluginsListDocking = PluginsListDocking,
                PluginsListIsHidden = PluginsListIsHidden,
                ReuseConnections = ReuseConnections,
                Size = Size,
                ThemeValue = ThemeValue,
                BringToTop = BringToTop,
                MostRatedMinNumberOfVotes = MostRatedMinNumberOfVotes,
                MostRatedMinRatingAverage = MostRatedMinRatingAverage,
                RepositoryUrl = RepositoryUrl,
                AdditionalRepositories = AdditionalRepositories,
                LibraryFilterMvp = LibraryFilterMvp,
                LibraryFilterNew = LibraryFilterNew,
                LibraryFilterOpenSource = LibraryFilterOpenSource,
                LibraryFilterRating = LibraryFilterRating,
                LibraryShowIncompatible = LibraryShowIncompatible,
                LibraryShowInstalled = LibraryShowInstalled,
                LibraryShowNotInstalled = LibraryShowNotInstalled,
                LibraryShowUpdates = LibraryShowUpdates,
                UseLegacyToolsList = UseLegacyToolsList,
                ShowCategoriesExpanded = ShowCategoriesExpanded,
                DoNotUseToolColors = DoNotUseToolColors
            };
        }

        public void Replace(Options newOption)
        {
            innerOptions = newOption;
        }

        public void ReportSettingsChange(SettingsPropertyEventArgs e)
        {
            OnSettingsChanged?.Invoke(this, e);
            Save();
        }

        public void Save()
        {
            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var settingsFile = Path.Combine(Paths.SettingsPath, "XrmToolBox.Settings.xml");

            XmlSerializerHelper.SerializeToFile(this, settingsFile);
        }

        #endregion Methods

        #endregion Properties
    }

    internal class CustomEnumConverter : EnumConverter
    {
        private Type enumType;

        public CustomEnumConverter(Type type) : base(type)
        {
            enumType = type;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType)
        {
            return srcType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            return destType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture,
            object value)
        {
            foreach (FieldInfo fi in enumType.GetFields())
            {
                DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi,
                    typeof(DescriptionAttribute));
                if ((dna != null) && ((string)value == dna.Description))
                    return Enum.Parse(enumType, fi.Name);
            }
            return Enum.Parse(enumType, (string)value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
                    object value, Type destType)
        {
            FieldInfo fi = enumType.GetField(Enum.GetName(enumType, value));
            DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi,
                typeof(DescriptionAttribute));
            if (dna != null)
                return dna.Description;
            else
                return value.ToString();
        }
    }

    internal class HiddenPluginsEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (provider?.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService svc)
            {
                var option = (Options)context?.Instance;

                using (HiddenPluginsDialog form = new HiddenPluginsDialog(option.HiddenPlugins))
                {
                    if (svc.ShowDialog(form) == DialogResult.OK)
                    {
                        value = form.HiddenPlugins; // update object
                    }
                }
            }
            return value; // can also replace the wrapper object here
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }
}