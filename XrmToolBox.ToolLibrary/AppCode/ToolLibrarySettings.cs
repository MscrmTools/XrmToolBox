using System;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class ToolLibrarySettings : IToolLibrarySettings
    {
        public int MostRatedMinNumberOfVotes { get; set; }
        public decimal MostRatedMinRatingAverage { get; set; }
        public string RepositoryUrl { get; set; }
    }
}