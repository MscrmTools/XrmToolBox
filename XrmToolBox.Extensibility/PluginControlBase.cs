﻿// PROJECT : XrmToolBox
// Author : Daryl LaBar http://www.linkedin.com/pub/daryl-labar/4/988/5b8/
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://www.dotnetdust.blogspot.com/

using McTools.Xrm.Connection;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Xrm.Sdk;
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Forms;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.UserControls;

namespace XrmToolBox.Extensibility
{
    /// <summary>
    /// This class adds the following three major features:
    /// Fully Implements IMsCrmToolsPluginUserControl
    /// Defines an Event for when the Connection is Updated, useful if needing to know when to refresh a connection specific cache
    /// Fully Implements the IWorkerHost which provides a much nicer api for requesting a connection then calling a method
    /// </summary>
    [PartNotDiscoverable]
    public class PluginControlBase : UserControl, IXrmToolBoxPluginControl, IWorkerHost
    {
        private readonly LogManager logManager;

        public PluginControlBase()
        {
            logManager = new LogManager(GetType());

            Tag = PluginControlId;

            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);
                if (args.Contains("PluginControlId") && Guid.TryParse(args["PluginControlId"], out Guid id) && id == PluginControlId)
                {
                    Invoke(new Action(() =>
                    {
                        HandleToastActivation(toastArgs);
                    }));
                }
            };
        }

        public ConnectionDetail ConnectionDetail { get; set; }
        public Guid PluginControlId { get; } = Guid.NewGuid();

        [Category("Tool Control Properties")]
        [Description("Icon")]
        public Icon PluginIcon { get; set; }

        public Image TabIcon { get; set; }

        public string ToolName { get; set; }

        public void CloseTool()
        {
            OnCloseTool?.Invoke(this, null);
        }

        [Obsolete("This has been renamed to CloseTool.  Call that method instead, and if there is any required logic for Closing override the ClosingPlugin Method", true)]
        public virtual void CloseToolPrompt()
        {
            CloseTool();
        }

        public virtual void HandleToastActivation(ToastNotificationActivatedEventArgsCompat args)
        {
            var toastArgs = ToastArguments.Parse(args.Argument);
            if (!toastArgs.Contains("action") || toastArgs["action"] != "showXrmToolBox")
            {
                return;
            }
            string pid = toastArgs["pid"];

            try
            {
                // Get the process by ID
                Process process = Process.GetProcessById(int.Parse(pid));

                // Check if the process has a main window
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    // Bring the process to the foreground
                    SetForegroundWindow(process.MainWindowHandle);
                }
            }
            catch (Exception ex)
            {
                // Do not fail on notification click
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        #region IMsCrmToolsPluginUserControl Members

        public event EventHandler OnCloseTool;

        public event EventHandler OnRequestConnection;

        public event EventHandler OnWorkAsync;

        public IOrganizationService Service { get; private set; }

        /// <summary>
        /// Allows for the plugin to prevent the form from closing, or preform some action before closing
        /// By default, if the Form is being closed, or a close all or all except active is being called, it won't prompt the user to ensure they wanted to close
        /// </summary>
        /// <param name="info"></param>
        public virtual void ClosingPlugin(PluginCloseInfo info)
        {
            if (info.Silent ||
                info.FormReason != CloseReason.None ||
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
                method = GetType().GetMethod(actionName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
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
                    method = GetType().GetMethod(actionName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { parameter.GetType() }, null);
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

        protected virtual void OnConnectionRequested(object sender, RequestConnectionEventArgs e)
        {
            OnRequestConnection?.Invoke(sender, e);
        }

        #endregion IMsCrmToolsPluginUserControl Members

        #region IWorkerHost

        private Worker _worker;

        public void CancelWorker()
        {
            _worker.CancelWorker();
        }

        public void RaiseRequestConnectionEvent(RequestConnectionEventArgs args)
        {
            if (OnRequestConnection != null)
            {
                OnRequestConnection(this, args);
            }
        }

        public void SetWorkingMessage(string message, int width = 340, int height = 150)
        {
            _worker.SetWorkingMessage(this, message, width, height);
        }

        public void WorkAsync(WorkAsyncInfo info)
        {
            info.Host = this;
            _worker = new Worker();
            _worker.WorkAsync(info);

            OnWorkAsync?.Invoke(this, new EventArgs());
        }

        #region Obsolete WorkAsync Calls

        [Obsolete("Use IWorkerHost Interface WorkAsync(WorkAsynInfo) method", true)]
        public void WorkAsync(string message, Action<DoWorkEventArgs> work, object argument = null, int messageWidth = 340, int messageHeight = 150)
        {
            var info = new WorkAsyncInfo(message, work)
            {
                AsyncArgument = argument,
                Host = this,
                MessageWidth = messageWidth,
                MessageHeight = messageHeight
            };
            WorkAsync(info);
        }

        [Obsolete("Use IWorkerHost Interface WorkAsync(WorkAsynInfo) method", true)]
        public void WorkAsync(string message, Action<DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, object argument = null, int messageWidth = 340, int messageHeight = 150)
        {
            var info = new WorkAsyncInfo(message, work)
            {
                AsyncArgument = argument,
                Host = this,
                MessageWidth = messageWidth,
                MessageHeight = messageHeight,
                PostWorkCallBack = callback
            };
            WorkAsync(info);
        }

        [Obsolete("Use IWorkerHost Interface WorkAsync(WorkAsynInfo) method", true)]
        public void WorkAsync(string message, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback,
                              Action<ProgressChangedEventArgs> progressChanged, object argument = null, int messageWidth = 340, int messageHeight = 150)
        {
            var info = new WorkAsyncInfo(message, work)
            {
                AsyncArgument = argument,
                Host = this,
                MessageWidth = messageWidth,
                MessageHeight = messageHeight,
                PostWorkCallBack = callback,
                ProgressChanged = progressChanged
            };
            WorkAsync(info);
        }

        [Obsolete("Use IWorkerHost Interface WorkAsync(WorkAsynInfo) method", true)]
        public void WorkAsync(string message, Action<BackgroundWorker, DoWorkEventArgs> work, object argument, bool enableCancellation, int messageWidth, int messageHeight)
        {
            var info = new WorkAsyncInfo(message, work)
            {
                AsyncArgument = argument,
                Host = this,
                IsCancelable = enableCancellation,
                MessageWidth = messageWidth,
                MessageHeight = messageHeight
            };
            WorkAsync(info);
        }

        [Obsolete("Use IWorkerHost Interface WorkAsync(WorkAsynInfo) method", true)]
        public void WorkAsync(string message, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, object argument, bool enableCancellation, int messageWidth, int messageHeight)
        {
            var info = new WorkAsyncInfo(message, work)
            {
                AsyncArgument = argument,
                Host = this,
                IsCancelable = enableCancellation,
                MessageWidth = messageWidth,
                MessageHeight = messageHeight,
                PostWorkCallBack = callback
            };
            WorkAsync(info);
        }

        [Obsolete("Use IWorkerHost Interface WorkAsync(WorkAsynInfo) method", true)]
        public void WorkAsync(string message, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, Action<ProgressChangedEventArgs> progressChanged, object argument, bool enableCancellation, int messageWidth, int messageHeight)
        {
            var info = new WorkAsyncInfo(message, work)
            {
                AsyncArgument = argument,
                Host = this,
                IsCancelable = enableCancellation,
                MessageWidth = messageWidth,
                MessageHeight = messageHeight,
                PostWorkCallBack = callback,
                ProgressChanged = progressChanged
            };
            WorkAsync(info);
        }

        #endregion Obsolete WorkAsync Calls

        #endregion IWorkerHost

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

                if (OnRequestConnection != null)
                {
                    OnRequestConnection(this, new RequestConnectionEventArgs
                    {
                        ActionName = action.GetMethodInfo().Name,
                        Control = this
                    });
                }
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
                if (OnRequestConnection == null)
                {
                    return;
                }
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

        #endregion ExecuteMethod

        #region Connection Updated

        public delegate void ConnectionUpdatedHandler(object sender, ConnectionUpdatedEventArgs e);

        public event ConnectionUpdatedHandler ConnectionUpdated;

        protected virtual void OnConnectionUpdated(ConnectionUpdatedEventArgs e)
        {
            logManager.SetConnection(e.ConnectionDetail);

            var handler = ConnectionUpdated;
            if (handler != null) { handler(this, e); }
        }

        public class ConnectionUpdatedEventArgs : EventArgs
        {
            public ConnectionUpdatedEventArgs(IOrganizationService service, ConnectionDetail connectionDetail)
            {
                Service = service;
                ConnectionDetail = connectionDetail;
            }

            public ConnectionDetail ConnectionDetail { get; set; }
            public IOrganizationService Service { get; set; }
        }

        #endregion Connection Updated

        #region Logs

        /// <summary>
        /// Path for the log file
        /// </summary>
        public string LogFilePath => logManager?.FilePath;

        /// <summary>
        /// Writes an error message in the log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Message parameters</param>
        public void LogError(string message, params object[] args)
        {
            logManager.LogError(message, args);
        }

        /// <summary>
        /// Writes an information message in the log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Message parameters</param>
        public void LogInfo(string message, params object[] args)
        {
            logManager.LogInfo(message, args);
        }

        /// <summary>
        /// Writes a warning message in the log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="args">Message parameters</param>
        public void LogWarning(string message, params object[] args)
        {
            logManager.LogWarning(message, args);
        }

        /// <summary>
        /// Opens the log file associated with the current plugin
        /// </summary>
        public void OpenLogFile()
        {
            logManager.OpenLog();
        }

        #endregion Logs

        #region Noticiation zone

        protected void HideNotification()
        {
            if (Parent == null)
            {
                throw new Exception($"Cannot find the control parent. Try to use the method {nameof(HideNotification)} only when the control is loaded");
            }

            var ctrls = Parent.Controls.Find("NotifPanel", false);
            if (ctrls.Length == 1)
            {
                ctrls[0].Visible = false;
            }
        }

        protected void ShowErrorNotification(string message, Uri moreInfoUri, int height = 32)
        {
            if (Parent == null)
            {
                throw new Exception($"Cannot find the control parent. Try to use the method {nameof(ShowErrorNotification)} only when the control is loaded");
            }

            var ctrls = Parent.Controls.Find("NotifPanel", false);
            if (ctrls.Length == 1)
            {
                ((NotificationArea)ctrls[0]).ShowErrorNotification(message, moreInfoUri, height);
            }
            else
            {
                throw new Exception("Unable to find Notification Area control");
            }
        }

        protected void ShowInfoNotification(string message, Uri moreInfoUri, int height = 32)
        {
            if (Parent == null)
            {
                throw new Exception($"Cannot find the control parent. Try to use the method {nameof(ShowInfoNotification)} only when the control is loaded");
            }

            var ctrls = Parent.Controls.Find("NotifPanel", false);
            if (ctrls.Length == 1)
            {
                ((NotificationArea)ctrls[0]).ShowInfoNotification(message, moreInfoUri, height);
            }
            else
            {
                throw new Exception("Unable to find Notification Area control");
            }
        }

        protected void ShowWarningNotification(string message, Uri moreInfoUri, int height = 32)
        {
            if (Parent == null)
            {
                throw new Exception($"Cannot find the control parent. Try to use the method {nameof(ShowWarningNotification)} only when the control is loaded");
            }

            var ctrls = Parent.Controls.Find("NotifPanel", false);
            if (ctrls.Length == 1)
            {
                ((NotificationArea)ctrls[0]).ShowWarningNotification(message, moreInfoUri, height);
            }
            else
            {
                throw new Exception("Unable to find Notification Area control");
            }
        }

        #endregion Noticiation zone

        #region Show Error Detail

        /// <summary>
        /// Generic Error Details show a dialog
        /// </summary>
        /// <param name="exception">Exception or any descendants class</param>
        /// <param name="heading">Title of the problem the tool want to show. Null = 'Error Detail'</param>
        /// <param name="extrainfo">If the tool may have more information, this will be used to a new issue.</param>
        /// <param name="allownewissue">If tool is `IGitHubPlugin` and send this as True new issues can be added by the user.</param>
        public void ShowErrorDialog(Exception exception, string heading = null, string extrainfo = null, bool allownewissue = true)
        {
            if (exception == null)
            {
                return;
            }

            exception.Source = ToolName;

            new ErrorDetail(this, exception, heading, extrainfo, allownewissue).ShowDialog(this);
        }

        #endregion Show Error Detail
    }
}