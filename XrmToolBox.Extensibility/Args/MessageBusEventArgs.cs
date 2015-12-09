using System;

namespace XrmToolBox.Extensibility
{
    /// <summary>
    /// Object that passed as argument in message bus communication
    /// </summary>
    public class MessageBusEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusEventArgs" /> class.
        /// </summary>
        /// <param name="targetPlugin">Unique string name (title) of plugin to call</param>
        /// <param name="newInstance">Switch instructing message broker start new instance of the plugin even if one is already present</param>
        public MessageBusEventArgs(string targetPlugin, bool newInstance = false)
        {
            this.TargetPlugin = targetPlugin;
            this.NewInstance = newInstance;
        }

        /// <summary>
        /// Gets or sets value indicating whether new instance of target plugin should be created.
        /// </summary>
        public bool NewInstance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets name of plugin to return.
        /// This value if not set is resolved by message broker.
        /// </summary>
        public string SourcePlugin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets data will be passed to target plugin.
        /// This is dynamic data type. New properties and methods could be added on the fly.
        /// </summary>
        public dynamic TargetArgument
        {
            get;
            set;
        }

        /// <summary>
        /// Gets name of the plugin to start.
        /// This value should be set in the <see cref="MessageBusEventArgs" /> constructor.
        /// </summary>
        public string TargetPlugin
        {
            get;
            private set;
        }
    }
}