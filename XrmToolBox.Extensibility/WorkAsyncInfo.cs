using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    public class WorkAsyncInfo
    {
        public WorkAsyncInfo()
        {
            Message = "Working...";
            MessageWidth = 340;
            MessageHeight = 150;
            IsCancelable = false;
        }

        /// <summary>
        /// Constructor for non cancelable Work
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="work">The work.</param>
        public WorkAsyncInfo(string message, Action<DoWorkEventArgs> work) : this(message, (s, e) => { work(e); })
        {
        }

        /// <summary>
        /// Constructor for cancelable Work
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="work">The work.</param>
        public WorkAsyncInfo(string message, Action<BackgroundWorker, DoWorkEventArgs> work) : this(message)
        {
            Work = work;
        }

        private WorkAsyncInfo(string message)
        {
            Message = message;
            MessageWidth = 340;
            MessageHeight = 150;
            IsCancelable = false;
        }

        public object AsyncArgument { get; set; }
        public Control Host { get; set; }
        public bool IsCancelable { get; set; }
        public string Message { get; set; }
        public int MessageHeight { get; set; }

        public int MessageWidth { get; set; }

        public Action<RunWorkerCompletedEventArgs> PostWorkCallBack { get; set; }

        public Action<ProgressChangedEventArgs> ProgressChanged { get; set; }

        /// <summary>
        /// Gets or sets the work to be performed Asynchronously.
        /// </summary>
        /// <value>
        /// The work.
        /// </value>
        public Action<BackgroundWorker, DoWorkEventArgs> Work { get; set; }

        internal void PerformProgressChange(object worker, ProgressChangedEventArgs args)
        {
            ProgressChanged(args);
        }

        internal void PerformWork(object worker, DoWorkEventArgs args)
        {
            Work((BackgroundWorker)worker, args);
        }
    }
}