using System;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.Extensibility
{
    public abstract class PluginBase : IXrmToolBoxPlugin
    {
        public string GetCompany()
        {
            return GetType().GetCompany();
        }

        public abstract IXrmToolBoxPluginControl GetControl();

        public string GetMyType()
        {
            return GetType().FullName;
        }

        public Guid GetId()
        {
            return GetType().GUID;
        }

        public string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }
    }
}