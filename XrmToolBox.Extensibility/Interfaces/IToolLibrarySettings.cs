using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IToolLibrarySettings
    {
        string AdditionalRepositories { get; set; }
        int MostRatedMinNumberOfVotes { get; set; }
        decimal MostRatedMinRatingAverage { get; set; }
        string RepositoryUrl { get; set; }
    }
}