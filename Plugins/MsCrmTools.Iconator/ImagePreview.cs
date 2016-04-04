// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Drawing;
using System.Windows.Forms;

namespace MsCrmTools.Iconator
{
    public partial class ImagePreview : Form
    {
        public ImagePreview(Image img)
        {
            InitializeComponent();

            pBox.Image = img;
            pBox.Width = img.Width;
            pBox.Height = img.Height;

            if (img.Width < 300 && img.Height < 300)
            {
                pBox.Location = new Point(Width / 2 - img.Width / 2, Height / 2 - img.Height / 2);
            }
        }
    }
}