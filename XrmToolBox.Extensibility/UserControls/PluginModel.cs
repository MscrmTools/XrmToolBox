// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility.UserControls
{
    public class PluginModel : UserControl
    {
        #region Delegates

        public delegate void ClickedEventHandler(object sender, EventArgs e);

        #endregion Delegates

        #region Event Handlers

        public virtual event ClickedEventHandler Clicked;

        #endregion Event Handlers
    }
}