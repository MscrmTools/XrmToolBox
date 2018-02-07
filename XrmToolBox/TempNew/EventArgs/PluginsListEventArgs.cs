using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrmToolBox.TempNew.EventArgs
{
    public class PluginsListEventArgs : System.EventArgs
    {
        public PluginsListEventArgs(PluginsListAction action)
        {
            Action = action;
        }

        public PluginsListAction Action { get; }
    }

    public enum PluginsListAction
    {
        OpenPluginsStore,
        ResetSearchFilter,
    }
}