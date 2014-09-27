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

        void WorkAsync(string message, Action<DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback);

        void WorkAsync(string message, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, Action<ProgressChangedEventArgs> progressChanged);

        void SetWorkingMessage(string message, int width, int height);

        void RaiseRequestConnectionEvent(RequestConnectionEventArgs args);
    }
}
