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
            name = FormatName(name);
            ConditionallyCreateSettingsDirectory();

            var filePath = GetPluginSettingsPath(pluginType, name);

            XmlSerializerHelper.SerializeToFile(settings, filePath);

            filePath = GetLegacyPluginSettingsPath(pluginType, name);

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
            name = FormatName(name);
            ConditionallyCreateSettingsDirectory();

            var filePath = GetPluginSettingsPath(pluginType, name);

            if (File.Exists(filePath))
            {
                settingsObject = DeserializeXmlFile<T>(filePath);
                return true;
            }

            filePath = GetLegacyPluginSettingsPath(pluginType, name);

            if (File.Exists(filePath))
            {
                settingsObject = DeserializeXmlFile<T>(filePath);
                return true;
            }

            settingsObject = default(T);
            return false;
        }

        /// <summary>
        /// Settings used to be loaded by  Fix file created before using Assembly name
        /// </summary>
        /// <param name="pluginType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetLegacyPluginSettingsPath(Type pluginType, string name)
        {
            return Path.Combine(Paths.SettingsPath,
                GetSafeFilename(pluginType.Name + name));
        }

        private string GetPluginSettingsPath(Type pluginType, string name)
        {
            return Path.Combine(Paths.SettingsPath,
                GetSafeFilename(pluginType.Assembly.FullName.Split(',')[0] + name));
        }

        private static void ConditionallyCreateSettingsDirectory()
        {
            if (!Directory.Exists(Paths.SettingsPath))
            {
                Directory.CreateDirectory(Paths.SettingsPath);
            }
        }

        private static T DeserializeXmlFile<T>(string filePath)
        {
            try
            {
                var document = new XmlDocument();
                document.Load(filePath);

                var settingsObject = (T) XmlSerializerHelper.Deserialize(document.OuterXml, typeof(T));
                return settingsObject;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error attempting to loading and deserializing file \"{filePath}\"", ex);
            }
        }

        private string FormatName(string name)
        {
            name = string.IsNullOrEmpty(name)
                ? string.Empty
                : "_" + CleanStringForFileName(name);
            return name + ".xml";
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