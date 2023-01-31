using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class ConnectionControlsUpdateSettings : IConnectionControlUpdateSettings
    {
        public bool NewVersion { get; set; }
        public string ReleaseNotes { get; set; }
        public bool RestartNow { get; set; }
        public string Version { get; set; }
    }
}