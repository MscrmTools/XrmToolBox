using System;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class OpenPluginEventArgs : EventArgs
    {
        public XtbPlugin Plugin { get; set; }
    }
}