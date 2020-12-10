using McTools.Xrm.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        RecentlyUpdated
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

    public class Options : ICloneable
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
            DisplayPluginsStoreOnStartup = true;
            HiddenPlugins = new List<string>();
        }

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

        [Browsable(false)] public bool? AllowLogUsage { get; set; }

        [Category("Startup")]
        [DisplayName("Check for update")]
        [Description("Check if update is available for XrmToolBox application")]
        public bool CheckUpdateOnStartup
        {
            get => !DoNotCheckForUpdates;
            set => DoNotCheckForUpdates = !value;
        }

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

        [Category("Connection controls")]
        [DisplayName("Search pre release updates")]
        [Description("Allow XrmToolBox to search pre release update (alpha, beta, etc.) of connection controls")]
        public bool ConnectionControlsAllowPreReleaseUpdates { get; set; }

        [ReadOnly(true)]
        [Category("Connection controls")]
        [DisplayName("Version")]
        [Description("Current version of connection controls")]
        public string ConnectionControlsVersion { get; set; }

        [Browsable(false)]
        public bool DisplayLargeIcons
        {
            get => IconDisplayMode == DisplayIcons.Large;
            set => IconDisplayMode = value ? DisplayIcons.Large : DisplayIcons.Small;
        }

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
                }

                return string.Empty;
            }
            set => PluginsDisplayOrder = value == "Alphabetically"
                ? XrmToolBox.DisplayOrder.Alphabetically
                : value == "Most used"
                    ? XrmToolBox.DisplayOrder.MostUsed
                    : XrmToolBox.DisplayOrder.RecentlyUpdated;
        }

        [Browsable(false)]
        [Category("Startup")]
        [DisplayName("Open Tool Library only if updates are available")]
        public bool DisplayPluginsStoreOnlyIfUpdates { get; set; }

        [Browsable(false)]
        [Category("Startup")]
        [DisplayName("Open Tool Library")]
        public bool DisplayPluginsStoreOnStartup { get; set; }

        [Browsable(false)]
        public bool DisplayRecentlyUpdatedFirst { get; set; }

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

        [Browsable(false)]
        public bool DoNotCheckForUpdates { get; set; }

        [Category("Start Page")]
        [DisplayName("Forget tools without connection")]
        [Description("Do not remember tools opened without connection in Most Recently Used tools")]
        public bool DoNotRememberPluginsWithoutConnection { get; set; }

        [Category("Start Page")]
        [DisplayName("Hide at startup")]
        [Description("Indicates if Start page should be opened or not when XrmToolBox starts up")]
        public bool DoNotShowStartPage { get; set; }

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

        [Browsable(false)]
        public DateTime LastAdvertisementDisplay { get; set; }

        [Browsable(false)]
        public string LastConnection { get; set; }

        [Browsable(false)]
        public string LastPlugin { get; set; }

        [Browsable(false)]
        public DateTime LastUpdateCheck { get; set; }

        [Category("Connections")]
        [DisplayName("Display all connections")]
        [Description("Indicates if bottom left connection control should display all connections regardless the connection files they come from")]
        public bool MergeConnectionFiles { get; set; }

        [Browsable(false)]
        public List<PluginUseCount> MostUsedList { get; set; }

        [Category("Start Page")]
        [DisplayName("Tools to display")]
        [Description("Indicates number of tools to display in Moste Recently Used items section of Start Page")]
        public int MruItemsToDisplay { get; set; } = 10;

        [Category("Tools list display")]
        [DisplayName("New ribbon display duration")]
        [Description("Number of days after having installed a tool a \"NEW\" ribbon is displayed on the tool")]
        public int NumberOfDaysToShowNewRibbon { get; set; } = 7;

        [Browsable(false)]
        public bool OptinForApplicationInsights { get; set; } = true;

        [Category("Tools list display")]
        [DisplayName("Order")]
        [TypeConverter(typeof(CustomEnumConverter))]
        [XmlIgnore]
        public DisplayOrder PluginsDisplayOrder { get; set; }

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

        [Category("Session")]
        [DisplayName("Remember session")]
        [Description("Indicates if the current session must be saved to be reused at the next XrmToolBox startup")]
        public bool RememberSession { get; set; }

        [Category("Connections")]
        [DisplayName("Reuse connections")]
        [Description("Indicates if connecting to an already connected organization should instanciate a new connection or use the existing one")]
        public bool ReuseConnections { get; set; }

        [Category("Startup")]
        [DisplayName("Show tools update notification")]
        [Description("Indicates if a notification should be displayed when updates are present for installed tools")]
        public bool ShowPluginUpdatesPanelAtStartup { get; set; } = true;

        [XmlElement("FormSize")]
        [Browsable(false)]
        public FormSize Size { get; set; }

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
                RememberSession = RememberSession
            };
        }

        public void Replace(Options newOption)
        {
            innerOptions = newOption;
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