// PROJECT : XrmToolBox
// Author : Daryl LaBar http://www.linkedin.com/pub/daryl-labar/4/988/5b8/
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://www.dotnetdust.blogspot.com/
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace XrmToolBox
{
    public interface IWorkerHost
    {
        Control.ControlCollection Controls { get; }

        void WorkAsync(object argument, Action<DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, string message, int messageWidth, int messageHeight);

        void WorkAsync(object argument, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, Action<ProgressChangedEventArgs> progressChanged, string message, int messageWidth, int messageHeight);

        void SetWorkingMessage(string message, int messageWidth, int messageHeight);

        void RaiseRequestConnectionEvent(RequestConnectionEventArgs args);
    }
}
