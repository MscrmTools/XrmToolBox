using System;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    public enum ToolBoxCloseReason
    {
        None,
        CloseAll,
        CloseAllExceptActive,
        CloseCurrent,
        CloseMiddleClick,
        CloseHotKey,
        PluginRequest,
    }

    public class PluginCloseInfo
    {
        public PluginCloseInfo()
        {
            FormReason = CloseReason.None;
            ToolBoxReason = ToolBoxCloseReason.None;
            Silent = false;
            Cancel = false;
        }

        public PluginCloseInfo(CloseReason reason) : this()
        {
            if (reason == CloseReason.None)
            {
                throw new ArgumentException("None is not a valid CloseReason");
            }
            FormReason = reason;
        }

        public PluginCloseInfo(ToolBoxCloseReason reason) : this()
        {
            if (reason == ToolBoxCloseReason.None)
            {
                throw new ArgumentException("None is not a valid ToolBoxCloseReason");
            }
            ToolBoxReason = reason;
        }

        public bool Silent { get; set; }
        public bool Cancel { get; set; }
        public CloseReason FormReason { get; set; }
        public ToolBoxCloseReason ToolBoxReason { get; set; }
    }
}