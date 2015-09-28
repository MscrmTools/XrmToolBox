// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.Extensibility
{
    public class RequestConnectionEventArgs : EventArgs
    {
        /// <summary>
        /// The name of the action
        /// </summary>
        /// <remarks>
        /// Useful when the connection call back arrives to understand which
        /// action required a connection
        /// </remarks>
        public string ActionName;

        /// <summary>
        /// The control that required a connection
        /// </summary>
        public IXrmToolBoxPluginControl Control;

        /// <summary>
        /// A parameter if passing extra data is needed
        /// </summary>
        public object Parameter;
    }
}