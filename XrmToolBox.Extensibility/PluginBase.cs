using System;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.Extensibility
{
    public abstract class PluginBase : IXrmToolBoxPlugin
    {
        public string GetAssemblyQualifiedName()
        {
            return GetType().AssemblyQualifiedName;
        }

        public string GetCompany()
        {
            return GetType().GetCompany();
        }

        public abstract IXrmToolBoxPluginControl GetControl();

        public virtual Guid GetId()
        {
            return Guid.Empty;
        }

        public string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }
    }
}