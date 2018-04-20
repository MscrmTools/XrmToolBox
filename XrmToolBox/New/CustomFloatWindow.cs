using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace XrmToolBox.New
{
    public class CustomFloatWindow : FloatWindow
    {
        public CustomFloatWindow(DockPanel dockPanel, DockPane pane)
            : base(dockPanel, pane)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        public CustomFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
            : base(dockPanel, pane, bounds)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
        }
    }

    public class CustomFloatWindowFactory : DockPanelExtender.IFloatWindowFactory
    {
        public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
        {
            return new CustomFloatWindow(dockPanel, pane, bounds);
        }

        public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane)
        {
            return new CustomFloatWindow(dockPanel, pane);
        }
    }
}