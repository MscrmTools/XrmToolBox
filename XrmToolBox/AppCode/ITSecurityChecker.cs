using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace XrmToolBox.AppCode
{
    internal class ItSecurityChecker
    {
        private List<string> plugins;

        public bool IsDisabled()
        {
            var isDisabled = false;

            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
            {
                using (RegistryKey registryKey = hklm.OpenSubKey(@"SOFTWARE\MscrmTools\XrmToolBox"))
                {
                    if (registryKey != null)
                    {
                        isDisabled = (int)registryKey.GetValue("IsDisabled") == 1;
                    }
                }
            }

            if (isDisabled == false)
            {
                using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
                {
                    using (RegistryKey registryKey = hklm.OpenSubKey(@"SOFTWARE\MscrmTools\XrmToolBox"))
                    {
                        if (registryKey != null)
                        {
                            isDisabled = (int)registryKey.GetValue("IsDisabled") == 1;
                        }
                    }
                }
            }

            return isDisabled;
        }

        public bool HasPluginsRestriction => plugins?.Count > 0 && plugins.All(p => p != "*");

        public void LoadAllowedPlugins()
        {
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
            {
                using (RegistryKey registryKey = hklm.OpenSubKey(@"SOFTWARE\MscrmTools\XrmToolBox"))
                {
                    if (registryKey != null)
                    {
                        plugins = new List<string>((string[])registryKey.GetValue("AllowedPlugins"));
                    }
                }
            }

            if (plugins == null)
            {
                using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
                {
                    using (RegistryKey registryKey = hklm.OpenSubKey(@"SOFTWARE\MscrmTools\XrmToolBox"))
                    {
                        if (registryKey != null)
                        {
                            plugins = new List<string>((string[])registryKey.GetValue("AllowedPlugins"));
                        }
                    }
                }
            }
        }

        public bool IsPluginAllowed(string plugin)
        {
            if (plugins == null) return true;

            if (plugins.Any(p => p == "*")) return true;

            if (plugins.Any(p => plugin?.ToLower().IndexOf(p.ToLower(), StringComparison.Ordinal) >= 0)) return true;

            return false;
        }
    }
}