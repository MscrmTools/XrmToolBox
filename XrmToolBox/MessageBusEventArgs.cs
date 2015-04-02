using System;
using System.Windows.Controls;

namespace XrmToolBox
{
    /// <summary>
    /// Object that passed as argument in message bus communication
    /// </summary>
    public class MessageBusEventArgs : EventArgs
    {
        /// <summary>
        /// Plugin to start
        /// </summary>
        public string TargetPlugin;

        public delegate void TargetAction(UserControl plugin);
    }
}
