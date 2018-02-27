using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.New.EventArgs
{
    public class OpenPluginEventArgs : System.EventArgs
    {
        public OpenPluginEventArgs(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin)
        {
            Plugin = plugin;
        }

        public Lazy<IXrmToolBoxPlugin, IPluginMetadata> Plugin { get; }
    }
}