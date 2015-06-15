using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.Extensibility
{
    public abstract class PluginBase : IXrmToolBoxPlugin
    {
        public abstract IXrmToolBoxPluginControl GetControl();

        public string GetMyType()
        {
            return GetType().FullName;
        }

        public string GetCompany()
        {
            return GetType().GetCompany();
        }

        public string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }
    }
}
