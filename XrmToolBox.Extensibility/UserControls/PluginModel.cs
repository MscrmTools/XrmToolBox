// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility.UserControls
{
    public class PluginModel : UserControl
    {
        protected int numberOfDaysToShowNewRibbon = 7;
        private System.ComponentModel.IContainer components;

        #region Delegates

        public delegate void ClickedEventHandler(object sender, EventArgs e);

        #endregion Delegates

        #region Event Handlers

        public virtual event EventHandler<MouseEventArgs> Clicked;

        #endregion Event Handlers

        protected void DrawRotatedTextAt(Graphics gr, float angle,
            string txt, int x, int y, Font the_font, Brush the_brush)
        {
            // Save the graphics state.
            GraphicsState state = gr.Save();
            gr.ResetTransform();

            // Rotate.
            gr.RotateTransform(angle);

            // Translate to desired position. Be sure to append
            // the rotation so it occurs after the rotation.
            gr.TranslateTransform(x, y, MatrixOrder.Append);

            // Draw the text at the origin.
            gr.DrawString(txt, the_font, the_brush, 0, 0);

            // Restore the graphics state.
            gr.Restore(state);
        }

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