using System;
using System.IO;

namespace XrmToolBox.Extensibility
{
    public static class Paths
    {
        private static string rootPath;
      
        public static string PluginsPath
        {
            get
            {
                var userApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                return Path.Combine(rootPath?? Path.Combine(userApplicationDataFolder, "MscrmTools", "XrmToolBox"), "Plugins");
            }
        }
        public static string LogsPath
        {
            get
            {
                var userApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(rootPath ?? Path.Combine(userApplicationDataFolder, "MscrmTools", "XrmToolBox"), "Logs");
            }
        }
        public static string SettingsPath
        {
            get
            {
                var userApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(rootPath ?? Path.Combine(userApplicationDataFolder, "MscrmTools", "XrmToolBox"), "Settings");
            }
        }
        public static string ConnectionsPath
        {
            get
            {
                var userApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(rootPath ?? Path.Combine(userApplicationDataFolder, "MscrmTools", "XrmToolBox"), "Connections");
            }
        }

        public static string XrmToolBoxPath
        {
            get
            {
                var userApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(rootPath ?? Path.Combine(userApplicationDataFolder, "MscrmTools", "XrmToolBox"));
            }
        }

        public static void OverrideRootPath(string newRootPath)
        {
            if(!Directory.Exists(newRootPath))
                throw new DirectoryNotFoundException(newRootPath);

            rootPath = newRootPath;
        }
    }
}
