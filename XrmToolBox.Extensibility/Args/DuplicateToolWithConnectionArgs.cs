using System;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.Extensibility.Args
{
    public class DuplicateToolWithConnectionArgs : EventArgs
    {
        public IDuplicatableTool SourceTool { get; set; }

        public object State { get; set; }
        public Lazy<IXrmToolBoxPlugin, IPluginMetadata> Tool { get; set; }
    }
}