// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.Forms
{
    public partial class InvalidFileListDialog : Form
    {
        public InvalidFileListDialog(List<string> invalidFilenames)
        {
            InitializeComponent();

            foreach (var filename in invalidFilenames)
            {
                lbInvalidFiles.Items.Add(filename);
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}