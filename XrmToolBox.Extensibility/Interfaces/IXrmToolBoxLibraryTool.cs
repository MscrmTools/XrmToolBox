using System;
using System.Collections.Generic;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IXrmToolBoxLibraryTool
    {
        string CategoriesList { get; }

        List<string> Files { get; }

        string LatestReleaseNote { get; }
        string LogoUrl { get; }
        string Name { get; }
        string Version { get; }
    }
}