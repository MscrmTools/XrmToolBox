using System;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    public class PluginCloseInfo
    {
        public CloseReason FormReason { get; set; }
        public ToolBoxCloseReason ToolBoxReason { get; set; }
        public bool Cancel { get; set; }

        public PluginCloseInfo()
        {
            FormReason = CloseReason.None;
            ToolBoxReason = ToolBoxCloseReason.None;
            Cancel = false;
        }

        public PluginCloseInfo(CloseReason reason)
        {
            if (reason == CloseReason.None)
            {
                throw new ArgumentException("None is not a valid CloseReason");
            }
            FormReason = reason;
            ToolBoxReason = ToolBoxCloseReason.None;
            Cancel = false;
        }

        public PluginCloseInfo(ToolBoxCloseReason reason)
        {
            if (reason == ToolBoxCloseReason.None)
            {
                throw new ArgumentException("None is not a valid ToolBoxCloseReason");
            }
            FormReason = CloseReason.None;
            ToolBoxReason = reason;
            Cancel = false;
        }
    }

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
}
