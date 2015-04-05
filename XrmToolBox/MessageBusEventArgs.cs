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
        /// Plugin to return
        /// </summary>
        public string SourcePlugin
        {
            get;
            set;
        }

        /// <summary>
        /// Plugin to start
        /// </summary>
        public string TargetPlugin
        {
            get;
            private set;
        }

        public bool ExistingInstance
        {
            get;
            private set;
        }

        public Action<UserControl> TargetAction;

        public MessageBusEventArgs(UserControl sourceControl, string targetPlugin, Action<UserControl> targetAction, bool existingInstance = true)
        {
            if (sourceControl != null && sourceControl.Tag != null)
            {
                this.SourcePlugin = ((Type)sourceControl.Tag).GetTitle();
            }

            this.TargetPlugin = targetPlugin;
            this.TargetAction = targetAction;
            this.ExistingInstance = existingInstance;
        }
    }
}
