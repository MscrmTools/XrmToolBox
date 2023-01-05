using Microsoft.Win32;
using System;

namespace XrmToolBox.AppCode
{
    internal static class RegistryHelper
    {
        public static void AddXtbProtocol(string xtbPath, string xtbStoragePath)
        {
            var softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            var classesKey = softwareKey.OpenSubKey("Classes", true);
            var xtbKey = classesKey.CreateSubKey("xrmtoolbox");
            xtbKey.SetValue("", "XrmToolBox");
            xtbKey.SetValue("URL Protocol", "");
            var shellKey = xtbKey.CreateSubKey("shell");
            var openKey = shellKey.CreateSubKey("open");
            var commandKey = openKey.CreateSubKey("command");
            commandKey.SetValue("", $"\"{xtbPath}\" /overridepath:\"{xtbStoragePath}\" \"%1\"");

            commandKey.Dispose();
            openKey.Dispose();
            shellKey.Dispose();
            xtbKey.Dispose();
            classesKey.Dispose();
            softwareKey.Dispose();
        }

        public static bool DoesXtbProtocolExist()
        {
            return Registry.ClassesRoot.OpenSubKey("xrmtoolbox") != null;
        }

        public static void RemoveXtbProtocol()
        {
            var softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            var classesKey = softwareKey.OpenSubKey("Classes", true);
            classesKey.DeleteSubKeyTree("xrmtoolbox");
            classesKey.Dispose();
            softwareKey.Dispose();
        }
    }
}