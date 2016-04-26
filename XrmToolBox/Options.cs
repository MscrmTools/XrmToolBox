using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

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
        }
    }

    public class Options : ICloneable
    {
        public Options()
        {
            CheckUpdateOnStartup = true;
            DisplayLargeIcons = true;
            DisplayMostUsedFirst = false;
            Size = new FormSize();
            MostUsedList = new List<PluginUseCount>();
            AllowLogUsage = null;
            DisplayPluginsStoreOnStartup = true;
        }

        public bool? AllowLogUsage { get; set; }
        public bool CheckUpdateOnStartup { get; set; }
        public bool CloseOpenedPluginsSilently { get; set; }
        public bool DisplayLargeIcons { get; set; }
        public bool DisplayMostUsedFirst { get; set; }
        public bool DisplayPluginsStoreOnStartup { get; set; }
        public List<string> HiddenPlugins { get; set; }
        public DateTime LastAdvertisementDisplay { get; set; }
        public DateTime LastUpdateCheck { get; set; }
        public List<PluginUseCount> MostUsedList { get; set; }

        [XmlElement("FormSize")]
        public FormSize Size { get; set; }

        public static Options Load()
        {
            if (File.Exists("XrmToolBox.Settings.xml"))
            {
                var document = new XmlDocument();
                document.Load("XrmToolBox.Settings.xml");

                return (Options)XmlSerializerHelper.Deserialize(document.OuterXml, typeof(Options));
            }

            return new Options();
        }

        public object Clone()
        {
            return new Options
            {
                CheckUpdateOnStartup = CheckUpdateOnStartup,
                DisplayLargeIcons = DisplayLargeIcons,
                DisplayMostUsedFirst = DisplayMostUsedFirst,
                MostUsedList = MostUsedList,
                LastAdvertisementDisplay = LastAdvertisementDisplay,
                HiddenPlugins = HiddenPlugins,
                LastUpdateCheck = LastUpdateCheck,
                AllowLogUsage = AllowLogUsage,
                CloseOpenedPluginsSilently = CloseOpenedPluginsSilently,
                DisplayPluginsStoreOnStartup = DisplayPluginsStoreOnStartup
            };
        }

        public void Save()
        {
            XmlSerializerHelper.SerializeToFile(this, "XrmToolBox.Settings.xml");
        }
    }
}