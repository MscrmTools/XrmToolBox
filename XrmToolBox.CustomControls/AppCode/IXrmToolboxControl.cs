using System;
using System.ComponentModel;
using Microsoft.Xrm.Sdk;

namespace XrmToolBox.CustomControls
{
    /// <summary>
    /// General interface for XrmToolBox helper controls
    /// </summary>
    interface IXrmToolBoxControl
    {
        IOrganizationService Service { get; set; }
        int LanguageCode { get; set; }
        // bool AutoLoadData { get; set; }

        event EventHandler<ProgressChangedEventArgs> ProgressChanged;
        event EventHandler<NotificationEventArgs> NotificationMessage;
        void UpdateConnection(IOrganizationService newService);

        // these events can be called from the parent control but they could also be invoked internally.
        // Ex: toolbar within the control that loads entities.

        void LoadData();
        event EventHandler BeginLoadData;
        event EventHandler LoadDataComplete;

        void ClearData();
        event EventHandler BeginClearData;
        event EventHandler ClearDataComplete;

        void Close();
        event EventHandler BeginClose;
        event EventHandler CloseComplete;
    }

    /// <summary>
    /// Message level being raised 
    /// </summary>
    public enum MessageLevel
    {
        /// <summary>
        /// Informational only
        /// </summary>
        Information = 1,

        /// <summary>
        /// Warning, something bad might be happening...
        /// </summary>
        Warning,

        /// <summary>
        /// Oh man, it's bad.
        /// </summary>
        Exception
    }

    /// <summary>
    /// Event arguments for a notifcaiton to the parent control
    /// </summary>
    public class NotificationEventArgs : EventArgs
    {
        /// <summary>
        /// Message being raised
        /// </summary>
        public readonly string Message = null;

        /// <summary>
        /// Optional Exception for MessageLevel == Exception
        /// </summary>
        public readonly Exception Exception = null;

        /// <summary>
        /// Message level riased with this notification
        /// </summary>
        public readonly MessageLevel Level = MessageLevel.Information;

        /// <summary>
        /// Contstructor!
        /// </summary>
        /// <param name="message">Message for the user</param>
        /// <param name="level">Message Level being raised</param>
        /// <param name="ex">Optional Exception being thrown reported</param>
        public NotificationEventArgs(string message, MessageLevel level, Exception ex = null)
        {
            Message = message;
            Exception = ex;
            Level = level;
        }
    }
}