using System;
using System.Windows.Forms;

namespace XrmToolBox
{
    /// <summary>
    /// Object that passed as argument in message bus communication
    /// </summary>
    public class MessageBusEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets name of plugin to return
        /// </summary>
        public string SourcePlugin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets name of the plugin to start
        /// </summary>
        public string TargetPlugin
        {
            get;
            private set;
        }

        /// <summary>
        /// Indicates if new instance of target plugin should be created
        /// </summary>
        public bool NewInstance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets action should be executed inside target plugin
        /// </summary>
        public Action<UserControl> TargetAction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets data will be passed to target plugin
        /// </summary>
        public object TargetArgument
        {
            get;
            set;
        }

        public MessageBusEventArgs(string targetPlugin, bool newInstance = false)
        {
            this.TargetPlugin = targetPlugin;
            this.NewInstance = newInstance;
        }
    }
}
