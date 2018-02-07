using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrmToolBox.PluginsStore
{
    public interface IStore
    {
        void LoadNugetPackages();

        string GetPluginProjectUrlByFileName(string fileName);

        void UninstallByFileName(string fileName);

        int PluginsCount { get; }
        bool HasUpdates { get; }

        event EventHandler PluginsUpdated;
    }
}