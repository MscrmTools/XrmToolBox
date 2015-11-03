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

        void RaiseRequestConnectionEvent(RequestConnectionEventArgs args);

        void SetWorkingMessage(string message, int width, int height);

        void WorkAsync(WorkAsyncInfo info);
    }
}