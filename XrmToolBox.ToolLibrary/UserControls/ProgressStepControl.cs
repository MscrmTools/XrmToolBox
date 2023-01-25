using System;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.ToolLibrary.AppCode;

namespace XrmToolBox.ToolLibrary.UserControls
{
    public partial class ProgressStepControl : UserControl
    {
        private ToolTip tooltip = new ToolTip();

        public ProgressStepControl(string actionText, Image img)
        {
            InitializeComponent();

            lblAction.Text = actionText;
            pbAction.Image = img;
            lblAction.SetAutoWidth();
            lblAction.Width += 40;

            pbAction.Size = new Size(32, 32);
            pbState.Size = new Size(32, 32);
            Height = 52;
            pnlAction.Size = new Size(52,52);
            pnlState.Size = new Size(52,52);
        }

        public void SetFailure(string errorMessage)
        {
            pbState.Image = Resource.Error512;
            pbState.SizeMode = PictureBoxSizeMode.StretchImage;
            tooltip.SetToolTip(pbState, errorMessage);
        }

        public void SetSuccess(bool pending = false)
        {
            pbState.Image = pending ? Resource.Pending512 : Resource.Success512;
            pbState.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}