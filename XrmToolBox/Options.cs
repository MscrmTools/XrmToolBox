using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XrmToolBox
{
    public class Options : ICloneable 
    {
        public bool DisplayLargeIcons { get; set; }
        public bool DisplayMostUsedFirst { get; set; }
        public List<PluginUseCount> MostUsedList { get; set; }
        public DateTime LastAdvertisementDisplay { get; set; }
        public DateTime LastUpdateCheck { get; set; }

        public bool CheckUpdateOnStartup { get; set; }
        public List<string> HiddenPlugins { get; set; }

        public Options()
        {
            CheckUpdateOnStartup = true;
            DisplayLargeIcons = true;
            DisplayMostUsedFirst = false;
            MostUsedList = new List<PluginUseCount>();
        }

        public static Options Load()
        {
            if (File.Exists("XrmToolBox.Settings.xml"))
            {
                var document = new XmlDocument();
                document.Load("XrmToolBox.Settings.xml");

                return (Options) XmlSerializerHelper.Deserialize(document.OuterXml, typeof (Options));
            }

            return new Options();
        }

        public void Save()
        {
            XmlSerializerHelper.SerializeToFile(this, "XrmToolBox.Settings.xml");
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
                LastUpdateCheck = LastUpdateCheck
            };
        }
    }
}
