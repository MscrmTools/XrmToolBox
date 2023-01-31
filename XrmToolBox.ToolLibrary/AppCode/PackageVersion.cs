using System;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class PackageVersion
    {
        public PackageVersion(string packageName, string version, string releaseNotes, byte[] content)
        {
            var versionParts = version.Split('-');

            Version = new Version(versionParts[0]);

            if (versionParts.Length > 1)
            {
                Release = versionParts[1];
                IsPrerelease = true;
            }

            PackageName = packageName;
            Content = content;
            ReleaseNotes = releaseNotes;
        }

        public byte[] Content { get; }
        public bool IsPrerelease { get; }
        public string PackageName { get; }
        public string Release { get; }
        public string ReleaseNotes { get; }
        public Version Version { get; }

        public override string ToString()
        {
            return $"{Version}{(IsPrerelease ? "-" + Release : "")}";
        }
    }
}