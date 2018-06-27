using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using XrmToolBox.Extensibility;

namespace XrmToolBox.Announcement
{
    public class AnnouncementSettings
    {
        private const string FileName = "XrmToolBox.Announcements.xml";
        private static AnnouncementSettings instance;

        private AnnouncementSettings()
        {
        }

        public static AnnouncementSettings Instance => instance ?? (instance = Load());

        public List<Guid> LastShown { get; set; } = new List<Guid>();
        public DateTime LastShownDate { get; set; }
        public List<Guid> ToHide { get; set; } = new List<Guid>();

        public void Save()
        {
            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var settingsFile = Path.Combine(Paths.SettingsPath, FileName);

            XmlSerializerHelper.SerializeToFile(this, settingsFile);
        }

        private static AnnouncementSettings Load()
        {
            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var file = Path.Combine(Paths.SettingsPath, FileName);

            if (File.Exists(file))
            {
                try
                {
                    var document = new XmlDocument();
                    document.Load(file);

                    return (AnnouncementSettings)XmlSerializerHelper.Deserialize(document.OuterXml, typeof(AnnouncementSettings));
                }
                catch
                {
                    return new AnnouncementSettings();
                }
            }

            return new AnnouncementSettings();
        }
    }
}