using System;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.UserControls;

namespace XrmToolBox.New.EventArgs
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

        public PluginEventArgs(OpenMruPluginEventArgs mruInfo, Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin)
        {
            MruInfo = mruInfo;
            PluginName = mruInfo.Item.PluginName;
            Plugin = plugin;
        }

        public PluginEventArgs(IXrmToolBoxPluginControl pluginControl)
        {
            PluginControl = pluginControl;
        }

        public PluginEventArgs(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin)
        {
            Plugin = plugin;
        }

        public OpenMruPluginEventArgs MruInfo { get; }
        public Lazy<IXrmToolBoxPlugin, IPluginMetadata> Plugin { get; }
        public IXrmToolBoxPluginControl PluginControl { get; }
        public PluginModel PluginModel { get; }
        public string PluginName { get; }
    }
}