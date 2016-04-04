// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Windows.Forms;

namespace MsCrmTools.Iconator
{
    public partial class ImageSizeSelectionDialog : Form
    {
        public ImageSizeSelectionDialog()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        public int ImageSizeSelected { get; set; }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                ImageSizeSelected = 16;
            else
                ImageSizeSelected = 32;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}