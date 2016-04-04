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
    public partial class GroupControl : UserControl, ISiteMapSavable
    {
        private readonly Dictionary<string, string> collec;

        private string initialDescription = "";
        private string initialId = "";
        private bool initialIsProfile;
        private string initialTitle = "";
        private string initialUrl = "";
        private ToolTip tip;

        #region Delegates

        public delegate void SaveEventHandler(object sender, SaveEventArgs e);

        #endregion Delegates

        #region Event Handlers

        public event SaveEventHandler Saved;

        #endregion Event Handlers

        public GroupControl()
        {
            InitializeComponent();

            collec = new Dictionary<string, string>();

            tip = new ToolTip();
            tip.ToolTipTitle = "Information";
            tip.SetToolTip(txtGroupId, "A unique identifier for this Group element.");
            tip.SetToolTip(txtGroupUrl, "Specifies the URL to render for the Outlook folder that represents the Group in Microsoft Dynamics CRM for Outlook.");
            tip.SetToolTip(chkGroupIsProfile, "Controls whether this Group represents a user selectable Profile for the Workplace. This only applies for Groups within the Workplace Area.");
            tip.SetToolTip(txtGroupTitle, "Deprecated. Use the <Titles> (SiteMap) and <Title> (SiteMap) elements.");
            tip.SetToolTip(txtGroupDescription, "Deprecated. Use the <Description> (SiteMap) element.");
        }

        public GroupControl(Dictionary<string, string> collection)
            : this()
        {
            collec = collection;

            FillControls();
        }

        public void Save()
        {
            if (txtGroupId.Text.Length == 0)
            {
                MessageBox.Show(this, "Id is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Dictionary<string, string> collection = new Dictionary<string, string>();
                if (txtGroupUrl.Text.Length > 0)
                    collection.Add("Url", txtGroupUrl.Text);
                if (txtGroupId.Text.Length > 0)
                    collection.Add("Id", txtGroupId.Text);
                if (txtGroupResourceId.Text.Length > 0)
                    collection.Add("ResourceId", txtGroupResourceId.Text);
                if (txtGroupDescriptionResourceId.Text.Length > 0)
                    collection.Add("DescriptionResourceId", txtGroupDescriptionResourceId.Text);
                if (txtGroupDescription.Text.Length > 0)
                    collection.Add("Description", txtGroupDescription.Text);
                if (txtGroupTitle.Text.Length > 0)
                    collection.Add("Title", txtGroupTitle.Text);

                collection.Add("IsProfile", chkGroupIsProfile.Checked.ToString().ToLower());

                if (collec.ContainsKey("_disabled"))
                {
                    collection.Add("_disabled", collec["_disabled"]);
                }

                initialUrl = txtGroupUrl.Text;
                initialId = txtGroupId.Text;
                initialIsProfile = chkGroupIsProfile.Checked;
                initialDescription = txtGroupDescription.Text;
                initialTitle = txtGroupTitle.Text;

                SendSaveMessage(collection);
            }
        }

        private void FillControls()
        {
            txtGroupUrl.Text = collec.ContainsKey("Url") ? collec["Url"] : "";
            txtGroupId.Text = collec.ContainsKey("Id") ? collec["Id"] : "";
            chkGroupIsProfile.Checked = collec.ContainsKey("IsProfile") ? collec["IsProfile"].ToLower() == "true" || collec["IsProfile"] == "1" : false;
            txtGroupResourceId.Text = collec.ContainsKey("ResourceId") ? collec["ResourceId"] : "";
            txtGroupDescriptionResourceId.Text = collec.ContainsKey("DescriptionResourceId") ? collec["DescriptionResourceId"] : "";
            txtGroupTitle.Text = collec.ContainsKey("Title") ? collec["Title"] : "";
            txtGroupDescription.Text = collec.ContainsKey("Description") ? collec["Description"] : "";

            initialUrl = txtGroupUrl.Text;
            initialId = txtGroupId.Text;
            initialIsProfile = chkGroupIsProfile.Checked;
            initialDescription = txtGroupDescription.Text;
            initialTitle = txtGroupTitle.Text;
        }

        private void SiteMapControl_Leave(object sender, EventArgs e)
        {
            if (initialUrl != txtGroupUrl.Text ||
                initialId != txtGroupId.Text ||
                initialDescription != txtGroupDescription.Text ||
                initialTitle != txtGroupTitle.Text ||
                initialIsProfile != chkGroupIsProfile.Checked)
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