using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IToolLibrarySettings
    {
        string AdditionalRepositories { get; set; }
        bool DisplayPluginsStoreOnlyIfUpdates { get; set; }
        bool DisplayPluginsStoreOnStartup { get; set; }
        bool LibraryFilterMvp { get; set; }
        bool LibraryFilterNew { get; set; }
        bool LibraryFilterOpenSource { get; set; }
        bool LibraryFilterRating { get; set; }
        bool LibraryShowIncompatible { get; set; }
        bool LibraryShowInstalled { get; set; }
        bool LibraryShowNotInstalled { get; set; }
        bool LibraryShowUpdates { get; set; }
        int MostRatedMinNumberOfVotes { get; set; }
        decimal MostRatedMinRatingAverage { get; set; }
        string RepositoryUrl { get; set; }

        #region Proxy

        bool ByPassProxyOnLocal { get; set; }
        string Password { get; set; }
        string ProxyAddress { get; set; }
        bool UseCustomProxy { get; set; }
        bool UseDefaultCredentials { get; set; }
        bool UseInternetExplorerProxy { get; set; }
        string UserName { get; set; }

        #endregion Proxy
    }
}