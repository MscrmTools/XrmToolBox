using System;
using System.IO;

namespace XrmToolBox.Extensibility
{
    public static class Paths
    {
        private static string rootPath;
        private static string userApplicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string PluginsPath
        {
            get
            {
                return Path.Combine(rootPath?? XrmToolBoxPath, "Plugins");
            }
        }
        public static string LogsPath
        {
            get
            {
                return Path.Combine(rootPath ?? XrmToolBoxPath, "Logs");
            }
        }
        public static string SettingsPath
        {
            get
            {
                return Path.Combine(rootPath ?? XrmToolBoxPath, "Settings");
            }
        }
        public static string ConnectionsPath
        {
            get
            {
                return Path.Combine(rootPath ?? XrmToolBoxPath, "Connections");
            }
        }

        public static string XrmToolBoxPath
        {
            get
            {
                return Path.Combine(rootPath ?? Path.Combine(userApplicationDataFolder, "MscrmTools", "XrmToolBox"));
            }
        }

        public static void OverrideRootPath(string newRootPath)
        {
            if(!Directory.Exists(Path.GetFullPath(Environment.ExpandEnvironmentVariables(newRootPath))))
                throw new DirectoryNotFoundException(newRootPath);

            rootPath = newRootPath;
        }
    }
}
