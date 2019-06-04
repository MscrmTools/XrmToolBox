using System.Windows.Forms;

namespace XrmToolBox
{
    /// <summary>
    /// Class to keep track of the Info panel, so it can be removed when the connection has succeeded
    /// </summary>
    internal class ConnectionParameterInfo
    {
        public UserControl ConnControl { get; set; }
        public object ConnectionParmater { get; set; }
    }
}