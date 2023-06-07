using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace XrmToolBox.Controls
{
    public partial class CreditsControl : UserControl
    {
        public CreditsControl()
        {
            InitializeComponent();
        }

        private void rtbCredits_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}