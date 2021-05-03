using System;

namespace XrmToolBox.PluginsStore
{
    public class ToolInformationEventArgs : EventArgs
    {
        public int ProgressPercentage { get; internal set; }
        public string ToolName { get; set; }
    }
}