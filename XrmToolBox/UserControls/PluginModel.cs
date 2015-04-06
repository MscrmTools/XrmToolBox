using System;
using System.Drawing;
using System.Windows.Forms;

namespace XrmToolBox.UserControls
{
    public class PluginModel : UserControl
    {
        #region Delegates

        public delegate void ClickedEventHandler(object sender, EventArgs e);

        #endregion

        #region Event Handlers

        public virtual event ClickedEventHandler Clicked;

        #endregion
    }
}
