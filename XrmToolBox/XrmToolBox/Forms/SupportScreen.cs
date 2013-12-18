// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace XrmToolBox.Forms
{
    public partial class SupportScreen : Form
    {
        public SupportScreen()
        {
            InitializeComponent();
        }

       private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           Process.Start("https://xrmtoolbox.codeplex.com/");
       }

       private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           var url = string.Format("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business={0}&lc={1}&item_name={2}&currency_code={3}&bn=PP%2dDonationsBF",
               "tanguy92@hotmail.com",
               "FR",
               "Donation%20for%20MSCRM%20Tools%20-%20Xrm%20Tool%20Box",
               "EUR");

           Process.Start(url);
       }

       private void linkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           Close();
       }
    }
}
