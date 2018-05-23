using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using XrmToolBox.Extensibility;

namespace XrmToolBox.AppCode
{
    public class Favorite
    {
        public string PluginName { get; set; }
    }

    [XmlRoot(ElementName = "ArrayOfFavorite", Namespace = "")]
    [XmlInclude(typeof(Favorites))]
    public class Favorites
    {
        private static Favorites instance;

        private Favorites()
        {
        }

        public static Favorites Instance
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

        [XmlElement("Favorite")]
        public List<Favorite> Items { get; set; } = new List<Favorite>();

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

            var settingsFile = Path.Combine(Paths.SettingsPath, "XrmToolBox.Favorites.xml");

            XmlSerializerHelper.SerializeToFile(Items, settingsFile);
        }

        private static bool Load(out Favorites favorites, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var settingsFile = Path.Combine(Paths.SettingsPath, "XrmToolBox.Favorites.xml");

            if (File.Exists(settingsFile))
            {
                try
                {
                    var document = new XmlDocument();
                    document.Load(settingsFile);

                    favorites = (Favorites)XmlSerializerHelper.Deserialize(document.OuterXml, typeof(Favorites), "ArrayOfFavorite");

                    return true;
                }
                catch (Exception error)
                {
                    errorMessage = error.Message;
                    favorites = new Favorites();
                    return false;
                }
            }

            favorites = new Favorites();
            return true;
        }
    }
}