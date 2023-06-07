using McTools.Xrm.Connection;
using System;
using System.Collections.Generic;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.New.EventArgs;

namespace XrmToolBox.AppCode
{
    internal interface IToolsForm
    {
        event EventHandler<PluginsListEventArgs> ActionRequested;

        event EventHandler<PluginEventArgs> OpenPluginProjectUrlRequested;

        event EventHandler<PluginEventArgs> OpenPluginRequested;

        event EventHandler<PluginEventArgs> UninstallPluginRequested;

        ConnectionDetail ConnectionDetail { get; set; }
        PluginManagerExtended PluginManager { get; }

        void DisplayCategories(Dictionary<string, List<string>> categories);

        void DuplicateTool(string pluginName, IDuplicatableTool sourceTool, object state, OpenMruPluginEventArgs e = null);

        void OpenPlugin(string pluginName, OpenMruPluginEventArgs e = null, bool needNewConnection = false);

        void ReloadPluginsList();
    }
}