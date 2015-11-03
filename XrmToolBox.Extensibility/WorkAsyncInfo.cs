using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    public class WorkAsyncInfo
    {
        public Control Host { get; set; }
        public string Message { get; private set; }
        /// <summary>
        /// Gets or sets the work to be performed Asynchronously.
        /// </summary>
        /// <value>
        /// The work.
        /// </value>
        public Action<BackgroundWorker, DoWorkEventArgs> Work { get; private set; }
        public bool IsCancelable { get; set; }
        public object AsyncArgument { get; set; }
        public Action<RunWorkerCompletedEventArgs> PostWorkCallBack { get; set; }
        public Action<ProgressChangedEventArgs> ProgressChanged { get; set; }
        public int MessageWidth { get; set; }
        public int MessageHeight { get; set; }

        private WorkAsyncInfo(string message)
        {
            Message = message;
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
        public WorkAsyncInfo(string message, Action<BackgroundWorker, DoWorkEventArgs> work) : this( message)
        {
            Work = work;
        }

        internal void PerformWork(object worker, DoWorkEventArgs args)
        {
            Work((BackgroundWorker) worker, args);
        }

        internal void PerformProgressChange(object worker, ProgressChangedEventArgs args)
        {
            ProgressChanged(args);
        }
    }
}
