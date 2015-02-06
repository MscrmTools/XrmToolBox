// PROJECT : MsCrmTools.AttributeBulkUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MsCrmTools.AttributeBulkUpdater
{
    /// <summary>
    /// Displays information about this program
    /// </summary>
    public partial class AboutForm : Form
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of class AboutForm
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();

            // Display version number
            lblVersion.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version; 
        }

        #endregion

        #region Handlers

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://searchpropupdater.codeplex.com/");
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Error while trying to open your browser: " + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://mscrmtools.blogspot.com");
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Error while trying to open your browser: " + error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}