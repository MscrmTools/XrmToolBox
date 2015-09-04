// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace XrmToolBox.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void LinkCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void llFatCow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.fatcow.com/free-icons");
        }

        private void llIconsMind_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.iconfinder.com/iconsmind");
        }

        private void llVictorPetrovich_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.iconfinder.com/krasnoyarsk");
        }
    }
}