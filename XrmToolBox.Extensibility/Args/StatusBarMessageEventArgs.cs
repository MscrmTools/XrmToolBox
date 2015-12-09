using System;

namespace XrmToolBox.Extensibility.Args
{
    public class StatusBarMessageEventArgs : EventArgs
    {
        public StatusBarMessageEventArgs(string message)
        {
            Message = message;
        }

        public StatusBarMessageEventArgs(int? progress)
        {
            if (progress.HasValue && (progress < 0 || progress > 100))
            {
                throw new Exception("Progress value has to be between 0 and 100");
            }

            Progress = progress;
        }

        public StatusBarMessageEventArgs(int? progress, string message)
        {
            Message = message;

            if (progress.HasValue && (progress < 0 || progress > 100))
            {
                throw new Exception("Progress value has to be between 0 and 100");
            }

            Progress = progress;
        }

        public string Message { get; private set; }
        public int? Progress { get; private set; }
    }
}