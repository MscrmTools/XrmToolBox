using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrmToolBox.PluginsStore
{
    public interface IStoreForm
    {
        event EventHandler PluginsUpdated;
    }
}