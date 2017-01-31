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
        private System.ComponentModel.IContainer components;
        #region Delegates

        public delegate void ClickedEventHandler(object sender, EventArgs e);

        #endregion Delegates

        #region Event Handlers

        public virtual event EventHandler<MouseEventArgs> Clicked;

        #endregion Event Handlers

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PluginModel
            // 
            this.Name = "PluginModel";
            this.ResumeLayout(false);

        }
    }
}