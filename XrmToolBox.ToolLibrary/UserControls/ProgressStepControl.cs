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