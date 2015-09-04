// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;

namespace MsCrmTools.SolutionImport
{
    public class ImportSettings
    {
        public bool Activate { get; set; }
        public bool ConvertToManaged { get; set; }
        public bool DownloadLog { get; set; }
        public Guid ImportId { get; set; }
        public bool IsFolder { get; set; }
        public int MajorVersion { get; set; }
        public bool OverwriteUnmanagedCustomizations { get; set; }
        public string Path { get; set; }
        public bool Publish { get; set; }
    }
}