using System;

namespace XrmToolBox
{
    /// <summary>
    /// Object that passed as argument in message bus communication
    /// </summary>
    public class MessageBusEventArgs : EventArgs
    {
        public string TargetConnection;
        
        public string TargetPlugin;
    }
}
