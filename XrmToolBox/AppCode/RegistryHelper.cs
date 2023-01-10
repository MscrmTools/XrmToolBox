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

        public static string XtbProtocolPath()
        {
            if (Registry.ClassesRoot.OpenSubKey("xrmtoolbox") is RegistryKey xtbKey &&
                xtbKey.OpenSubKey("shell") is RegistryKey shellKey &&
                shellKey.OpenSubKey("open") is RegistryKey openKey &&
                openKey.OpenSubKey("command") is RegistryKey commandKey &&
                commandKey.GetValue("") is string command)
            {
                if (command.Contains(" /overridepath"))
                {
                    command = command.Split(new string[] { " /overridepath" }, StringSplitOptions.None)[0].Trim();
                }
                command = command.Trim('"');
                return command;
            }
            return null;
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