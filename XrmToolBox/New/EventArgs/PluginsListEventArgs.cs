using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrmToolBox.New.EventArgs
{
    public class PluginsListEventArgs : System.EventArgs
    {
        public PluginsListEventArgs(PluginsListAction action)
        {
            Action = action;
        }

        public PluginsListAction Action { get; }
    }


    public abstract class PluginsListAction { }

    public class OpenPluginsStoreAction : PluginsListAction
    {
        public OpenPluginsStoreAction(string searchTerm)
        {
            SearchTerm = searchTerm;
        }

        public string SearchTerm { get; }
    }

    public class ResetSearchFilterAction : PluginsListAction { }

}