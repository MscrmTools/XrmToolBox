using System;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class ToolLibrarySettings : IToolLibrarySettings
    {
        public string AdditionalRepositories { get; set; }
        public bool ByPassProxyOnLocal { get; set; }
        public bool DisplayPluginsStoreOnlyIfUpdates { get; set; }
        public bool DisplayPluginsStoreOnStartup { get; set; }
        public bool LibraryFilterMvp { get; set; }
        public bool LibraryFilterNew { get; set; }
        public bool LibraryFilterOpenSource { get; set; }
        public bool LibraryFilterRating { get; set; }
        public bool LibraryShowIncompatible { get; set; }
        public bool LibraryShowInstalled { get; set; } = true;
        public bool LibraryShowNotInstalled { get; set; } = true;
        public bool LibraryShowUpdates { get; set; } = true;
        public int MostRatedMinNumberOfVotes { get; set; }
        public decimal MostRatedMinRatingAverage { get; set; }
        public string Password { get; set; }
        public string ProxyAddress { get; set; }
        public string RepositoryUrl { get; set; }
        public bool UseCustomProxy { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool UseInternetExplorerProxy { get; set; }
        public string UserName { get; set; }
    }
}