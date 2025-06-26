﻿using McTools.Xrm.Connection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using XrmToolBox.Extensibility;

namespace XrmToolBox.AppCode
{
    public class MostRecentlyUsedItem
    {
        public string ConnectionFileName { get; set; }
        public Guid ConnectionId { get; set; }
        public string ConnectionName { get; set; }
        public DateTime Date { get; set; }
        public string PluginName { get; set; }
    }

    [XmlRoot(ElementName = "ArrayOfMostRecentlyUsedItem", Namespace = "")]
    [XmlInclude(typeof(MostRecentlyUsedItem))]
    public class MostRecentlyUsedItems
    {
        private static MostRecentlyUsedItems instance;

        private MostRecentlyUsedItems()
        {
        }

        public static MostRecentlyUsedItems Instance
        {
            get
            {
                if (instance == null)
                {
                    if (!Load(out instance, out var message))
                    {
                        throw new Exception(message);
                    }
                }

                return instance;
            }
        }

        [XmlElement("MostRecentlyUsedItem")]
        public List<MostRecentlyUsedItem> Items { get; set; } = new List<MostRecentlyUsedItem>();

        public void RemovePluginsWithNoConnection()
        {
            for (var i = Items.Count - 1; i >= 0; i--)
            {
                if (Items[i].ConnectionId == Guid.Empty)
                {
                    Items.Remove(Items[i]);
                }
            }
        }

        public void Save()
        {
            int count = Options.Instance.MruItemsToDisplay;

            if (Items.Count > count)
            {
                Items.RemoveRange(0, Items.Count - count);
            }

            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var settingsFile = Path.Combine(Paths.SettingsPath, "XrmToolBox.Mru.xml");

            XmlSerializerHelper.SerializeToFile(Items, settingsFile);
        }

        private static bool Load(out MostRecentlyUsedItems mrus, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var settingsFile = Path.Combine(Paths.SettingsPath, "XrmToolBox.Mru.xml");

            if (File.Exists(settingsFile))
            {
                try
                {
                    var document = new XmlDocument();
                    document.Load(settingsFile);

                    var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(document.OuterXml));

                    mrus = (MostRecentlyUsedItems)XmlSerializerHelper.Deserialize(memoryStream, typeof(MostRecentlyUsedItems));

                    return true;
                }
                catch (Exception error)
                {
                    errorMessage = error.Message;
                    mrus = new MostRecentlyUsedItems();
                    return false;
                }
            }

            mrus = new MostRecentlyUsedItems();
            return true;
        }
    }
}