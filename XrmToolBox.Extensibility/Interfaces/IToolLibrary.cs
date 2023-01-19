using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IToolLibrary
    {
        bool AllowConnectionControlPreRelease { get; set; }

        List<string> Categories { get; }

        int PluginsCount { get; }

        int PluginsUpdatesCount { get; }

        List<IXrmToolBoxLibraryTool> Tools { get; }

        string GetPluginProjectUrlByFileName(string fileName);

        IXrmToolBoxLibraryTool GetPluginUpdateByFile(string location);

        void InstallOneToolUpdate(IXrmToolBoxLibraryTool tool, bool onNextRestart, Form form);

        Task<IConnectionControlUpdateSettings> IsConnectionControlsUpdateAvailable(string connectionControlsVersion);

        Task LoadTools(bool fromStorePortal = true);

        Task<IConnectionControlUpdateSettings> PrepareConnectionControlsUpdate(Form form, bool restart);

        void UninstallByFileName(string fileName);
    }
}