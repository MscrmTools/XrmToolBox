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
            Plugin = (Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>)pluginModel.Tag;
        }

        public PluginEventArgs(string pluginName)
        {
            PluginName = pluginName;
        }

        public PluginEventArgs(OpenMruPluginEventArgs mruInfo, Lazy<IXrmToolBoxPlugin, IPluginMetadataExt> plugin)
        {
            MruInfo = mruInfo;
            PluginName = mruInfo.Item.PluginName;
            Plugin = plugin;
        }

        public PluginEventArgs(IXrmToolBoxPluginControl pluginControl)
        {
            PluginControl = pluginControl;
        }

        public PluginEventArgs(Lazy<IXrmToolBoxPlugin, IPluginMetadataExt> plugin)
        {
            Plugin = plugin;
        }

        public OpenMruPluginEventArgs MruInfo { get; }
        public bool NeedNewConnection { get; set; }
        public Lazy<IXrmToolBoxPlugin, IPluginMetadataExt> Plugin { get; }
        public IXrmToolBoxPluginControl PluginControl { get; }
        public PluginModel PluginModel { get; }
        public string PluginName { get; }
        public IDuplicatableTool SourceTool { get; set; }
        public object State { get; set; }
    }
}