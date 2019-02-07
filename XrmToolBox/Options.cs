using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;

namespace XrmToolBox
{
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
            DisplayLargeIcons = true;
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

        public bool? AllowLogUsage { get; set; }
        public bool CheckUpdateOnStartup { get; set; }
        public bool CloseEachPluginSilently { get; set; }
        public bool CloseOpenedPluginsSilently { get; set; }
        public bool ClosePluginsSilentlyOnWindowsShutdown { get; set; } = true;
        public bool DisplayLargeIcons { get; set; }
        public bool DisplayMostUsedFirst { get; set; }
        public string DisplayOrder { get; set; }
        public bool DisplayPluginsStoreOnlyIfUpdates { get; set; }
        public bool DisplayPluginsStoreOnStartup { get; set; }
        public bool DisplayRecentlyUpdatedFirst { get; set; }

        public bool DoNotCheckForUpdates { get; set; }
        public bool DoNotRememberPluginsWithoutConnection { get; set; }
        public bool DoNotShowStartPage { get; set; }
        public List<string> HiddenPlugins { get; set; }
        public DateTime LastAdvertisementDisplay { get; set; }
        public string LastConnection { get; set; }
        public string LastPlugin { get; set; }
        public DateTime LastUpdateCheck { get; set; }
        public bool MergeConnectionFiles { get; set; }
        public List<PluginUseCount> MostUsedList { get; set; }
        public int MruItemsToDisplay { get; set; } = 10;
        public DockState PluginsListDocking { get; set; } = DockState.Document;
        public bool PluginsListIsHidden { get; set; } = false;
        public bool? PluginsStoreShowIncompatible { get; set; }
        public bool? PluginsStoreShowInstalled { get; set; }
        public bool? PluginsStoreShowNew { get; set; }
        public bool? PluginsStoreShowUpdates { get; set; }

        public List<PluginUpdateSkip> PluginsUpdateSkip { get; set; } = new List<PluginUpdateSkip>();
        public bool RememberSession { get; set; }

        public bool ReuseConnections { get; set; }

        public bool ShowPluginUpdatesPanelAtStartup { get; set; } = true;

        [XmlElement("FormSize")]
        public FormSize Size { get; set; }

        public string Theme { get; set; } = "Light theme";

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

                    if (options.DisplayOrder == null)
                    {
                        if (options.DisplayMostUsedFirst)
                        {
                            options.DisplayOrder = "Most used";
                        }
                        else if (options.DisplayRecentlyUpdatedFirst)
                        {
                            options.DisplayOrder = "Recently updated";
                        }
                        else
                        {
                            options.DisplayOrder = "Alphabetically";
                        }
                    }

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
                PluginsUpdateSkip = PluginsUpdateSkip
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
}