using System;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class ToolOperationEventArgs : EventArgs
    {
        private bool succeeded;

        public ToolOperationEventArgs(bool isInstallation, XtbPlugin plugin, Version version, string downloadUrl)
        {
            IsInstallation = isInstallation;
            Plugin = plugin;
            Version = version;
            DownloadUrl = downloadUrl;
        }

        public event EventHandler<bool> OnOperationCompleted;

        public string DownloadUrl { get; }
        public bool IsInstallation { get; }
        public XtbPlugin Plugin { get; }

        public bool Succeeded
        {
            get
            {
                return succeeded;
            }
            internal set
            {
                succeeded = value;
                OnOperationCompleted?.Invoke(this, value);
            }
        }

        public Version Version { get; }
    }
}