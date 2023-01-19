using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IConnectionControlUpdateSettings
    {
        bool NewVersion { get; set; }
        string ReleaseNotes { get; set; }
        bool RestartNow { get; set; }
        string Version { get; set; }
    }
}