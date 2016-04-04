using System;
using System.IO;
using System.Reflection;

namespace MsCrmTools.MetadataBrowser.Helpers
{
    internal class SettingsHelper
    {
        public static string[] LoadSettings()
        {
            string fileName = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "MscrmTools.MetadataBrowser.config");
            if (File.Exists(fileName))
            {
                using (var reader = new StreamReader(fileName))
                {
                    var settingsString = reader.ReadToEnd();

                    return settingsString.Split('|');
                }
            }

            return null;
        }

        public static void SaveSettings(string[] entitySelectedAttributes, string[] attributeSelectedAttributes, string[] otmRelSelectedAttributes, string[] mtmRelSelectedAttributes, string[] privSelectedAttributes)
        {
            var fileName = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "MscrmTools.MetadataBrowser.config");

            var settingsString = string.Format("{0}|{1}|{2}|{3}|{4}",
                entitySelectedAttributes != null ? String.Join(";", entitySelectedAttributes) : "",
                attributeSelectedAttributes != null ? String.Join(";", attributeSelectedAttributes) : "",
                otmRelSelectedAttributes != null ? String.Join(";", otmRelSelectedAttributes) : "",
                mtmRelSelectedAttributes != null ? String.Join(";", mtmRelSelectedAttributes) : "",
                privSelectedAttributes != null ? String.Join(";", privSelectedAttributes) : "");

            using (var writer = new StreamWriter(fileName, false))
            {
                writer.Write(settingsString);
            }
        }
    }
}