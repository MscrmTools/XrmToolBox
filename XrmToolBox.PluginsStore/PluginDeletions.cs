using System.Collections.Generic;
using System.Xml.Serialization;

namespace XrmToolBox.PluginsStore
{
    public class PluginDeletion
    {
        [XmlIgnore]
        public List<string> ConfictedPackages { get; set; }

        [XmlIgnore]
        public bool Conflict { get; set; }

        public List<string> Files { get; set; }
    }

    public class PluginDeletions
    {
        public PluginDeletions()
        {
            Plugins = new List<PluginDeletion>();
        }

        public List<PluginDeletion> Plugins { get; set; }

        public int PreviousProcessId { get; set; }
    }
}