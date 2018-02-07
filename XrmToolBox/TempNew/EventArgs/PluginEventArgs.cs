using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.UserControls;

namespace XrmToolBox.TempNew.EventArgs
{
    public class PluginEventArgs : System.EventArgs
    {
        public PluginEventArgs(PluginModel pluginModel)
        {
            PluginModel = pluginModel;
            Plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadata>)pluginModel.Tag;
        }

        public PluginEventArgs(string pluginName)
        {
            PluginName = pluginName;
        }

        public PluginEventArgs(IXrmToolBoxPluginControl pluginControl)
        {
            PluginControl = pluginControl;
        }

        public PluginEventArgs(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin)
        {
            Plugin = plugin;
        }

        public Lazy<IXrmToolBoxPlugin, IPluginMetadata> Plugin { get; }
        public PluginModel PluginModel { get; }
        public string PluginName { get; }
        public IXrmToolBoxPluginControl PluginControl { get; }
    }
}