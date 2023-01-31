using System.Collections.Generic;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class PluginUpdate
    {
        public string Destination { get; set; }
        public string Name { get; set; }
        public bool RequireRestart { get; set; }
        public string Source { get; set; }
    }

    public class PluginUpdates
    {
        public PluginUpdates()
        {
            Plugins = new List<PluginUpdate>();
        }

        public List<PluginUpdate> Plugins { get; set; }

        public int PreviousProcessId { get; set; }
    }
}