using System.IO;
using System.Reflection;
using System.Xml;

namespace MsCrmTools.WebResourcesManager.AppCode
{
    public class Options
    {
        private static Options instance;
        private readonly string filePath;

        private Options()
        {
            var currentPath = Assembly.GetExecutingAssembly().Location;
            var fi = new FileInfo(currentPath);
            filePath = fi.FullName.Replace(fi.Extension, ".xml");
        }

        public static Options Instance
        {
            get
            {
                if (instance == null)
                {
                    var currentPath = Assembly.GetExecutingAssembly().Location;
                    var fi = new FileInfo(currentPath);
                    var optionFile = fi.FullName.Replace(fi.Extension, ".xml");

                    if (File.Exists(optionFile))
                    {
                        var document = new XmlDocument();
                        document.Load(optionFile);

                        instance = (Options)XmlSerializerHelper.Deserialize(document.OuterXml, typeof(Options));
                    }
                    else
                    {
                        instance = new Options();
                    }
                }

                return instance;
            }
        }

        public string AfterPublishCommand { get; set; }
        public string AfterUpdateCommand { get; set; }
        public string CompareToolArgs { get; set; }
        public string CompareToolPath { get; set; }
        public string LastFolderUsed { get; set; }
        public bool SaveOnDisk { get; set; }
        public bool PushTsMapFiles { get; set; }

        public void Save()
        {
            XmlSerializerHelper.SerializeToFile(this, filePath);
        }
    }
}