using McTools.Xrm.Connection;
using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace XrmToolBox.Extensibility
{
    public class SettingsManager
    {
        private static readonly char[] IllegalChars = Path.GetInvalidFileNameChars();

        /// <summary>
        /// singleton instance of SettingsManager
        /// </summary>
        private static SettingsManager _instance;

        /// <summary>
        /// Initialize a new instance of class <see cref="SettingsManager"/>
        /// </summary>
        private SettingsManager()
        { }

        /// <summary>
        /// Gets the singleton instance of SettingsManager
        /// </summary>
        public static SettingsManager Instance => _instance ?? (_instance = new SettingsManager());

        /// <summary>
        /// Save a settings object to a XML serialized file
        /// </summary>
        /// <param name="pluginType">Type of the plugin<remarks>This type is used to name the settings file on disk</remarks></param>
        /// <param name="settings">Settings object to save</param>
        /// <param name="name">Name of the settings file<remarks>Optional parameter that completes the settings file name</remarks></param>
        public void Save(Type pluginType, object settings, string name = null)
        {
            if (name != null)
            {
                name = CleanStringForFileName(name);
            }

            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var filePath = Path.Combine(Paths.SettingsPath,
                $"{pluginType.Assembly.FullName.Split(',')[0]}{(string.IsNullOrEmpty(name) ? "" : "_")}{name}.xml");

            XmlSerializerHelper.SerializeToFile(settings, filePath);

            // Fix file created before using Assembly name
            filePath = Path.Combine(Paths.SettingsPath,
               GetSafeFilename($"{pluginType.Name}{(string.IsNullOrEmpty(name) ? "" : "_")}{name}.xml"));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Loads a settings file for a specified plugin
        /// </summary>
        /// <typeparam name="T">Settings object to load</typeparam>
        /// <param name="pluginType">Type of the plugin<remarks>This type is used to name the settings file on disk</remarks></param>
        /// <param name="settingsObject">Settings object</param>
        /// <param name="name">Name of the settings file<remarks>Optional parameter that completes the settings file name</remarks></param>
        /// <returns>Settings object</returns>
        /// <remarks>
        /// The format of the settings file name is {Type}_{Name}.xml
        /// If name argument is not specified, the format of the settings file name is {Type}.xml
        /// </remarks>
        public bool TryLoad<T>(Type pluginType, out T settingsObject, string name = null)
        {
            if (name != null)
            {
                name = CleanStringForFileName(name);
            }

            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }

            var filePath = Path.Combine(Paths.SettingsPath,
               GetSafeFilename(
                   $"{pluginType.Assembly.FullName.Split(',')[0]}{(string.IsNullOrEmpty(name) ? "" : "_")}{name}.xml"));

            if (File.Exists(filePath))
            {
                var document = new XmlDocument();
                document.Load(filePath);

                settingsObject = (T)XmlSerializerHelper.Deserialize(document.OuterXml, typeof(T));
                return true;
            }

            // Check again with a different name to handle settings files
            // created before fixing the name used
            filePath = Path.Combine(Paths.SettingsPath,
                $"{pluginType.Name}{(string.IsNullOrEmpty(name) ? "" : "_")}{name}.xml");

            if (File.Exists(filePath))
            {
                var document = new XmlDocument();
                document.Load(filePath);

                settingsObject = (T)XmlSerializerHelper.Deserialize(document.OuterXml, typeof(T));
                return true;
            }

            settingsObject = default(T);
            return false;
        }

        private string CleanStringForFileName(string text)
        {
            var cleanedText = "";

            foreach (var t in text)
            {
                cleanedText += IllegalChars.Contains(t) ? '_' : t;
            }

            return cleanedText;
        }

        private string GetSafeFilename(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }
    }
}