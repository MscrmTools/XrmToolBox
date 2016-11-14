using System.Collections.Generic;
using System.Xml.Serialization;

namespace XrmToolBox.PluginsStore
{
    public class PluginDeletion
    {
        public List<string> Files { get; set; }
        [XmlIgnore]
        public bool Conflict { get; set; }
        [XmlIgnore]
        public List<string> ConfictedPackages { get; set; }

        [XmlIgnore]
        public XtbNuGetPackage Package { get; internal set; }
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