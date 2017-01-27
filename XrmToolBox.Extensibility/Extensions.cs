using System;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.Extensibility
{
    public static class Extensions
    {
        public static ICodePlexPlugin GetCodePlexPlugin(this TabPage page)
        {
            for (var i = 0; i < page.Controls.Count; i++)
            {
                var ctrl = page.Controls[i] as ICodePlexPlugin;
                if (ctrl != null)
                {
                    return ctrl;
                }
            }

            return null;
        }

        public static string GetCompany(this Type pluginType)
        {
            return ((AssemblyCompanyAttribute)GetAssemblyAttribute(pluginType.Assembly, typeof(AssemblyCompanyAttribute))).Company;
        }

        public static string GetDescription(this Type pluginType)
        {
            return ((AssemblyDescriptionAttribute)GetAssemblyAttribute(pluginType.Assembly, typeof(AssemblyDescriptionAttribute))).Description;
        }

        public static IGitHubPlugin GetGithubPlugin(this TabPage page)
        {
            for (var i = 0; i < page.Controls.Count; i++)
            {
                var ctrl = page.Controls[i] as IGitHubPlugin;
                if (ctrl != null)
                {
                    return ctrl;
                }
            }

            return null;
        }

        public static IHelpPlugin GetHelpEnabledPlugin(this TabPage page)
        {
            for (var i = 0; i < page.Controls.Count; i++)
            {
                var ctrl = page.Controls[i] as IHelpPlugin;
                if (ctrl != null)
                {
                    return ctrl;
                }
            }

            return null;
        }

        public static IPayPalPlugin GetPaypalPlugin(this TabPage page)
        {
            for (var i = 0; i < page.Controls.Count; i++)
            {
                var ctrl = page.Controls[i] as IPayPalPlugin;
                if (ctrl != null)
                {
                    return ctrl;
                }
            }

            return null;
        }

        public static IXrmToolBoxPluginControl GetPlugin(this TabPage page)
        {
            for (var i = 0; i < page.Controls.Count; i++)
            {
                var ctrl = page.Controls[i] as IXrmToolBoxPluginControl;
                if (ctrl != null)
                {
                    return ctrl;
                }
            }

            return null;
        }

        public static string GetPluginName(this TabPage page)
        {
            return ((Lazy<IXrmToolBoxPlugin, IPluginMetadata>)page.Tag).Metadata.Name;
        }

        public static string GetTitle(this Type pluginType)
        {
            return ((AssemblyTitleAttribute)GetAssemblyAttribute(pluginType.Assembly, typeof(AssemblyTitleAttribute))).Title;
        }

        private static object GetAssemblyAttribute(Assembly assembly, Type attributeType)
        {
            return assembly.GetCustomAttributes(attributeType, true)[0];
        }
    }
}