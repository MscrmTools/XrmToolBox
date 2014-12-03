// PROJECT : XrmToolBox
// Author : Daryl LaBar http://www.linkedin.com/pub/daryl-labar/4/988/5b8/
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://www.dotnetdust.blogspot.com/
using Microsoft.Xrm.Sdk;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using McTools.Xrm.Connection;

namespace XrmToolBox
{
    /// <summary>
    /// This class adds the following three major features:
    /// Fully Implements IMsCrmToolsPluginUserControl
    /// Defines an Event for when the Connection is Updated, useful if needing to know when to refresh a connection specific cache
    /// Fully Implements the IWorkerHost which provides a much nicer api for requesting a connection then calling a method
    /// </summary>

    [IgnorePlugin]
    public class PluginBase : UserControl, IMsCrmToolsPluginUserControl, IWorkerHost
    {
        public ConnectionDetail ConnectionDetail { get; set; }

        [Obsolete("Use OnCloseTool, and add any required logic for Closing in an override of the ClosingPlugin Method", true)]
        public virtual void CloseToolPrompt()
        {
            OnCloseTool(this, null);
        }

        #region IMsCrmToolsPluginUserControl Members

        public event EventHandler OnCloseTool;

        public event EventHandler OnRequestConnection;

        public virtual Image PluginLogo
        {
            get { return null; }
        }

        public IOrganizationService Service { get; private set; }

        /// <summary>
        /// Allows for the plugin to prevent the form from closing, or preform some action before closing
        /// By default, if the Form is being closed, or a close all or all except active is being called, it won't prompt the user to ensure they wanted to close 
        /// </summary>
        /// <param name="info"></param>
        public virtual void ClosingPlugin(PluginCloseInfo info)
        {
            if (info.FormReason != CloseReason.None ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAll ||
                info.ToolBoxReason == ToolBoxCloseReason.CloseAllExceptActive)
            {
                return;
            }

            info.Cancel = MessageBox.Show(@"Are you sure you want to close this tab?", @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
        }

        public virtual void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            Service = newService;
            ConnectionDetail = detail;
            OnConnectionUpdated(new ConnectionUpdatedEventArgs(newService, detail));
            if (actionName == String.Empty)
            {
                return;
            }

            MethodInfo method;
            if (parameter == null)
            {
                method = GetType().GetMethod(actionName);
                if (method == null)
                {
                    throw new Exception("Unable to find method " + GetType().Name + "." + actionName);
                }
                method.Invoke(this, null);
            }
            else
            {
                var externalCaller = parameter as ExternalMethodCallerInfo;
                if (externalCaller == null)
                {
                    method = GetType().GetMethod(actionName, new[] { parameter.GetType() });
                    if (method == null)
                    {
                        throw new Exception("Unable to find method " + GetType().Name + "." + actionName);
                    }
                    method.Invoke(this, new[] { parameter });
                }
                else
                {
                    externalCaller.ExternalAction();
                }
            }
        }

        #endregion

        #region IWorkerHost

        private readonly Worker _worker = new Worker();

        public void WorkAsync(string message, Action<DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, object argument = null, int messageWidth = 340, int messageHeight = 150)
        {
            _worker.WorkAsync(this, message, work, callback, argument, messageWidth, messageHeight);
        }

        public void WorkAsync(string message, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback,
                              Action<ProgressChangedEventArgs> progressChanged, object argument = null, int messageWidth = 340, int messageHeight = 150)
        {
            _worker.WorkAsync(this, message, work, callback, progressChanged, argument, messageWidth, messageHeight);
        }

        public void SetWorkingMessage(string message, int width = 340, int height = 100)
        {
            _worker.SetWorkingMessage(this, message);
        }

        public void RaiseRequestConnectionEvent(RequestConnectionEventArgs args)
        {
            if (OnRequestConnection != null)
            {
                OnRequestConnection(this, args);
            }
        }

        #endregion // IWorkerHost

        #region ExecuteMethod

        /// <summary>
        /// Checks to make sure that the Plugin has an IOrganizationService Connection, before calling the action.
        /// </summary>
        /// <param name="action"></param>
        public void ExecuteMethod(Action action)
        {
            if (Service == null)
            {
                var name = action.GetMethodInfo().Name;
                if (name.Contains("__"))
                {
                    throw new ArgumentOutOfRangeException("action",
                        @"The Action of an Execute Method must not be a lambda.  Use the ExecuteAction(action, parameter) Method.");
                }

                OnRequestConnection(this, new RequestConnectionEventArgs
                {
                    ActionName = action.GetMethodInfo().Name,
                    Control = this
                });
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Checks to make sure that the Plugin has an IOrganizationService Connection, before calling the action.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="parameter"></param>
        public void ExecuteMethod<T>(Action<T> action, T parameter)
        {
            var caller = parameter as ExternalMethodCallerInfo;
            if (Service == null)
            {
                if (caller == null)
                {
                    OnRequestConnection(this, new RequestConnectionEventArgs
                    {
                        ActionName = action.GetMethodInfo().Name,
                        Control = this,
                        Parameter = parameter
                    });
                }
                else
                {
                    OnRequestConnection(this, new RequestConnectionEventArgs
                    {
                        ActionName = "Recaller",
                        Control = this,
                        Parameter = parameter
                    });
                }
            }
            else if (caller == null)
            {
                action(parameter);
            }
            else
            {
                caller.ExternalAction.Invoke();
            }
        }

        // ReSharper disable once UnusedMember.Local
        private void Recaller(ExternalMethodCallerInfo info)
        {
            info.ExternalAction.Invoke();
        }

        #endregion // ExecuteMethod

        #region Connection Updated

        public event ConnectionUpdatedHandler ConnectionUpdated;

        protected virtual void OnConnectionUpdated(ConnectionUpdatedEventArgs e)
        {
            var handler = ConnectionUpdated;
            if (handler != null) { handler(this, e); }
        }

        public delegate void ConnectionUpdatedHandler(object sender, ConnectionUpdatedEventArgs e);

        public class ConnectionUpdatedEventArgs : EventArgs
        {
            public IOrganizationService Service { get; set; }
            public ConnectionDetail ConnectionDetail { get; set; }

            public ConnectionUpdatedEventArgs(IOrganizationService service, ConnectionDetail connectionDetail)
            {
                Service = service;
                ConnectionDetail = connectionDetail;
            }
        }

        #endregion // Connection Updated
    }

    /// <summary>
    /// Can be removed after upgrading to 4.5
    /// </summary>
    internal static class FourFiveReflectionExtension
    {
        public static MethodInfo GetMethodInfo(this Delegate del)
        {
            if (del == null) throw new ArgumentNullException("del");

            return del.Method;
        }
    }
}
