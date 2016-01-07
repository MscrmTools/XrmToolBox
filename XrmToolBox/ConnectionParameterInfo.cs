using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrmToolBox
{
    /// <summary>
    /// Class to keep track of the Info panel, so it can be removed when the connection has succeeded
    /// </summary>
    internal class ConnectionParameterInfo
    {
        public object ConnectionParmater { get; set; }
        public System.Windows.Forms.Panel InfoPanel { get; set; }
    }
}
