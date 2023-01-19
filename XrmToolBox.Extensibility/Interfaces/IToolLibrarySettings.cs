using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IToolLibrarySettings
    {
        int MostRatedMinNumberOfVotes { get; set; }
        decimal MostRatedMinRatingAverage { get; set; }
        string RepositoryUrl { get; set; }
    }
}