﻿// PROJECT : XrmToolBox
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
           Process.Start("http://mscrmtools.blogspot.fr/p/xrmtoolbox-sponsoring.html");
       }

       private void linkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           Close();
       }
    }
}
