// PROJECT : XrmToolBox
// Author : Daryl LaBar http://www.linkedin.com/pub/daryl-labar/4/988/5b8/
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://www.dotnetdust.blogspot.com/

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IWorkerHost
    {
        Control.ControlCollection Controls { get; }

        void WorkAsync(string message, Action<DoWorkEventArgs> work, object argument, int messageWidth, int messageHeight);

        void WorkAsync(string message, Action<DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, object argument, int messageWidth, int messageHeight);

        void WorkAsync(string message, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, Action<ProgressChangedEventArgs> progressChanged, object argument, int messageWidth, int messageHeight);

        void SetWorkingMessage(string message, int width, int height);

        void RaiseRequestConnectionEvent(RequestConnectionEventArgs args);
    }
}
