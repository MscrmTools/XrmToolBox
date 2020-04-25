using System;

namespace XrmToolBox.PluginsStore
{
    public interface IStore
    {
        event EventHandler PluginsUpdated;

        bool HasUpdates { get; }

        int PluginsCount { get; }

        string GetPluginProjectUrlByFileName(string fileName);

        void LoadNugetPackages(bool fromPluginStoreForm);

        void UninstallByFileName(string fileName);
    }
}