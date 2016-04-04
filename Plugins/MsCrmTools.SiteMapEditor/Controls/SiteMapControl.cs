// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using MsCrmTools.SiteMapEditor.AppCode;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MsCrmTools.SiteMapEditor.Controls
{
    public partial class SiteMapControl : UserControl, ISiteMapSavable
    {
        private readonly Dictionary<string, string> collec;

        private string initialUrl = "";
        private ToolTip tip;

        #region Delegates

        public delegate void SaveEventHandler(object sender, SaveEventArgs e);

        #endregion Delegates

        #region Event Handlers

        public event SaveEventHandler Saved;

        #endregion Event Handlers

        public SiteMapControl()
        {
            InitializeComponent();

            collec = new Dictionary<string, string>();

            tip = new ToolTip();
            tip.ToolTipTitle = "Information";
            tip.SetToolTip(txtSiteMapUrl, "Specifies the URL for Microsoft Dynamics CRM for Outlook to render.");
        }

        public SiteMapControl(Dictionary<string, string> collection)
            : this()
        {
            collec = collection;

            FillControls();
        }

        public void Save()
        {
            Dictionary<string, string> collection = new Dictionary<string, string>();

            if (txtSiteMapUrl.Text.Length > 0)
                collection.Add("Url", txtSiteMapUrl.Text);

            initialUrl = txtSiteMapUrl.Text;

            SendSaveMessage(collection);
        }

        private void FillControls()
        {
            txtSiteMapUrl.Text = collec.ContainsKey("Url") ? collec["Url"] : "";
            initialUrl = txtSiteMapUrl.Text;
        }

        private void SiteMapControl_Leave(object sender, EventArgs e)
        {
            if (initialUrl != txtSiteMapUrl.Text)
            {
                if (MessageBox.Show("You didn't save your changes! Do you want to save them now?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Save();
                }
            }
        }

        #region Send Events

        /// <summary>
        /// Sends a connection success message
        /// </summary>
        /// <param name="service">IOrganizationService generated</param>
        /// <param name="parameters">Lsit of parameter</param>
        private void SendSaveMessage(Dictionary<string, string> collection)
        {
            SaveEventArgs sea = new SaveEventArgs { AttributeCollection = collection };

            if (Saved != null)
            {
                Saved(this, sea);
            }
        }

        #endregion Send Events
    }
}