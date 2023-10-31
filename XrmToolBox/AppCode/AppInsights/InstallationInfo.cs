using System;
using System.IO;
using System.Xml;
using XrmToolBox.Extensibility;
using XrmToolBox.ToolLibrary.AppCode;

namespace XrmToolBox.AppCode.AppInsights
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

        public DateTime InstallationDate { get; set; } = DateTime.Now;
        public Guid InstallationId { get; set; }

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