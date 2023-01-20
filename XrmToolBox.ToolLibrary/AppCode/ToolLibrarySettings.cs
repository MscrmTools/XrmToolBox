using System;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class ToolLibrarySettings : IToolLibrarySettings
    {
        public string AdditionalRepositories { get; set; }
        public int MostRatedMinNumberOfVotes { get; set; }
        public decimal MostRatedMinRatingAverage { get; set; }
        public string RepositoryUrl { get; set; }
    }
}