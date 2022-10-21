using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;

namespace XrmToolBox.PluginsStore
{
    public class InstallationInfo
    {
        private const string OptionFileName = "XrmToolBox.Installation.xml";

        private static InstallationInfo instance;

        private InstallationInfo()
        {
        }

        public static InstallationInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Load();
                }

                if (instance?.InstallationId == Guid.Empty)
                {
                    instance?.Save();
                }

                return instance;
            }
        }

        public Guid InstallationId { get; set; }
        public DateTime InstallationDate { get; set; } = DateTime.Now;

        public void Save()
        {
            if (InstallationId == Guid.Empty)
            {
                InstallationId = Guid.NewGuid();
            }
            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var settingsFile = Path.Combine(Paths.SettingsPath, OptionFileName);

            XmlSerializerHelper.SerializeToFile(this, settingsFile);
        }

        private static InstallationInfo Load()
        {
            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var settingsFile = Path.Combine(Paths.SettingsPath, OptionFileName);

            if (File.Exists(settingsFile))
            {
                try
                {
                    var document = new XmlDocument();
                    document.Load(settingsFile);

                    return (InstallationInfo)XmlSerializerHelper.Deserialize(document.OuterXml, typeof(InstallationInfo));
                }
                catch { }
            }
            return new InstallationInfo();
        }
    }
}