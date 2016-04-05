using System;
using XrmToolBox.Extensibility.Args;

namespace XrmToolBox.Extensibility.Interfaces
{
    /// <summary>
    /// This interface allows to send message or progress to main application
    /// to display progress data in the status bar
    /// </summary>
    public interface IStatusBarMessenger
    {
        event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;
    }
}