using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XrmToolBox
{
    class UI
    {
        public static void InvokeControlAction<t>(t UIcontrol, Action<t> action) where t : Control
        {
            if (UIcontrol.InvokeRequired)
            {
                UIcontrol.Invoke(new Action<t, Action<t>>(InvokeControlAction),
                          new object[] { UIcontrol, action });
            }
            else
            { action(UIcontrol); }
        }
        public static void InvokeToolStripButton(Form form, ToolStripButton UIcontrol, Action<ToolStripButton> action)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new Action<Form, ToolStripButton, Action<ToolStripButton>>(InvokeToolStripButton), new object[] { form, UIcontrol, action });
            }
            else
            {
                action(UIcontrol);
            }
        }
    }
}
