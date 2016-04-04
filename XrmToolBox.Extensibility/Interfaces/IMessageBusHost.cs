using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    /// <summary>
    /// Interface which plugins should implement to be able communicate between each other
    /// </summary>
    public interface IMessageBusHost
    {
        /// <summary>
        /// Event raised by plugin notifying broker that new message is ready to dispatch
        /// </summary>
        event EventHandler<MessageBusEventArgs> OnOutgoingMessage;

        /// <summary>
        /// Method that accepts dispatched message
        /// </summary>
        /// <param name="message"></param>
        void OnIncomingMessage(MessageBusEventArgs message);
    }
}