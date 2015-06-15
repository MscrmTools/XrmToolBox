using System;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.Extensibility
{
    public static class Extensions
    {
        public static IXrmToolBoxPluginControl GetPlugin(this TabPage page)
        {
            return (IXrmToolBoxPluginControl)page.Controls[0];
        }

        public static ICodePlexPlugin GetCodePlexPlugin(this TabPage page)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return page.Controls[0] as ICodePlexPlugin;
        }

        public static IGitHubPlugin GetGithubPlugin(this TabPage page)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return page.Controls[0] as IGitHubPlugin;
        }

        public static IHelpedPlugin GetHelpEnabledPlugin(this TabPage page)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return page.Controls[0] as IHelpedPlugin;
        }

        public static IPayPalPlugin GetPaypalPlugin(this TabPage page)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return page.Controls[0] as IPayPalPlugin;
        }

        public static string GetTitle(this Type pluginType)
        {
            return ((AssemblyTitleAttribute)GetAssemblyAttribute(pluginType.Assembly, typeof(AssemblyTitleAttribute))).Title;
        }

        public static string GetDescription(this Type pluginType)
        {
            return ((AssemblyDescriptionAttribute)GetAssemblyAttribute(pluginType.Assembly, typeof(AssemblyDescriptionAttribute))).Description;
        }

        public static string GetCompany(this Type pluginType)
        {
            return ((AssemblyCompanyAttribute)GetAssemblyAttribute(pluginType.Assembly, typeof(AssemblyCompanyAttribute))).Company;
        }

        private static object GetAssemblyAttribute(Assembly assembly, Type attributeType)
        {
            return assembly.GetCustomAttributes(attributeType, true)[0];
        }
    }
}
