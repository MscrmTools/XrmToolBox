using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IToolLibrarySettings
    {
        string AdditionalRepositories { get; set; }
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
    }
}