// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MsCrmTools.SiteMapEditor.AppCode;
using MsCrmTools.SiteMapEditor.Forms.WebRessources;

namespace MsCrmTools.SiteMapEditor.Controls
{
    public partial class AreaControl : UserControl, ISiteMapSavable
    {
        private readonly Dictionary<string, string> collec;

        private string initialUrl = "";
        private string initialId = "";
        private string initialIcon = "";
        private string initialTitle = "";
        private string initialDescription = "";
        private bool initialShowGroups;

        private List<Entity> webResourcesImageCache;
        private List<Entity> webResourcesHtmlCache;
        private IOrganizationService service;

        #region Delegates

        public delegate void SaveEventHandler(object sender, SaveEventArgs e);

        #endregion

        #region Event Handlers

        public event SaveEventHandler Saved;

        #endregion

        public AreaControl(List<Entity> webResourcesImageCache, List<Entity> webResourcesHtmlCache, IOrganizationService service)
        {
            InitializeComponent();
            this.webResourcesImageCache = webResourcesImageCache;
            this.webResourcesHtmlCache = webResourcesHtmlCache;
            this.service = service;

            collec = new Dictionary<string, string>();

            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = "Information";
            tip.SetToolTip(txtAreaIcon, "Specifies a URL to a 16x16 pixel image.");
            tip.SetToolTip(txtAreaId, "Specifies a unique identifier in ASCII. Spaces are not allowed.\r\n\r\nValid values:a-z, A-Z, 0-9, and underscore (_).");
            tip.SetToolTip(txtAreaUrl, "Specifies the Microsoft Dynamics CRM for Outlook URL to render for the Outlook folder that represents the Area.");
            tip.SetToolTip(chkAreaShowGroups, "Control whether groups of sub-areas are shown in the navigation pane.");
            tip.SetToolTip(txtAreaTitle, "Deprecated. Use the <Titles> (SiteMap) and <Title> (SiteMap) elements.");
            tip.SetToolTip(txtAreaDescription, "Deprecated. Use the <Description> (SiteMap) element.");
        }

        public AreaControl(Dictionary<string, string> collection, List<Entity> webResourcesImageCache, List<Entity> webResourcesHtmlCache, IOrganizationService service)
            : this(webResourcesImageCache, webResourcesHtmlCache, service)
        {
            collec = collection;

            FillControls();
        }

        private void FillControls()
        {
            txtAreaUrl.Text = collec.ContainsKey("Url") ? collec["Url"] : "";
            txtAreaId.Text = collec.ContainsKey("Id") ? collec["Id"] : "";
            txtAreaIcon.Text = collec.ContainsKey("Icon") ? collec["Icon"] : "";
            chkAreaShowGroups.Checked = collec.ContainsKey("ShowGroups") && collec["ShowGroups"].ToLower() == "true";
            txtAreaResourceId.Text = collec.ContainsKey("ResourceId") ? collec["ResourceId"] : "";
            txtAreaDescriptionResourceId.Text = collec.ContainsKey("DescriptionResourceId") ? collec["DescriptionResourceId"] : "";

            txtAreaTitle.Text = collec.ContainsKey("Title") ? collec["Title"] : "";
            txtAreaDescription.Text = collec.ContainsKey("Description") ? collec["Description"] : "";

            initialUrl = txtAreaUrl.Text;
            initialId = txtAreaId.Text;
            initialIcon = txtAreaIcon.Text;
            initialShowGroups = chkAreaShowGroups.Checked;
            initialDescription = txtAreaDescription.Text;
            initialTitle = txtAreaTitle.Text;
        }

        private void SiteMapControl_Leave(object sender, EventArgs e)
        {
            if (initialUrl != txtAreaUrl.Text ||
                initialId != txtAreaId.Text ||
                initialIcon != txtAreaIcon.Text ||
                initialDescription != txtAreaDescription.Text ||
                initialTitle != txtAreaTitle.Text ||
                initialShowGroups != chkAreaShowGroups.Checked)
            {
                if (MessageBox.Show("You didn't save your changes! Do you want to save them now?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Save();
                }
            }
        }

        public void Save()
        {
            if (txtAreaId.Text.Length == 0)
            {
                MessageBox.Show(this, "Id is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Dictionary<string, string> collection = new Dictionary<string, string>();
                if (txtAreaUrl.Text.Length > 0)
                    collection.Add("Url", txtAreaUrl.Text);
                if (txtAreaId.Text.Length > 0)
                    collection.Add("Id", txtAreaId.Text);
                if (txtAreaIcon.Text.Length > 0)
                    collection.Add("Icon", txtAreaIcon.Text);
                if (txtAreaResourceId.Text.Length > 0)
                    collection.Add("ResourceId", txtAreaResourceId.Text);
                if (txtAreaDescriptionResourceId.Text.Length > 0)
                    collection.Add("DescriptionResourceId", txtAreaDescriptionResourceId.Text);
                if (txtAreaDescription.Text.Length > 0)
                    collection.Add("Description", txtAreaDescription.Text);
                if (txtAreaTitle.Text.Length > 0)
                    collection.Add("Title", txtAreaTitle.Text);
                collection.Add("ShowGroups", chkAreaShowGroups.Checked.ToString().ToLower());

                initialUrl = txtAreaUrl.Text;
                initialId = txtAreaId.Text;
                initialIcon = txtAreaIcon.Text;
                initialShowGroups = chkAreaShowGroups.Checked;
                initialDescription = txtAreaDescription.Text;
                initialTitle = txtAreaTitle.Text;

                SendSaveMessage(collection);
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

        #endregion

        private void btnBrowsIcon_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                MessageBox.Show(ParentForm,
                    "You are not connected to an organization! Please connect to an organization and reopen this Area item",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            WebResourcePicker wrp = new WebResourcePicker(WebResourcePicker.WebResourceType.Image, webResourcesImageCache,webResourcesHtmlCache, service );
            wrp.StartPosition = FormStartPosition.CenterParent;

            if (wrp.ShowDialog() == DialogResult.OK)
            {
                txtAreaIcon.Text = "$webresource:" + wrp.SelectedResource;
            }
        }

        private void buttonBrowseUrl_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                MessageBox.Show(ParentForm,
                    "You are not connected to an organization! Please connect to an organization and reopen this Area item",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } 
            
            WebResourcePicker wrp = new WebResourcePicker(WebResourcePicker.WebResourceType.WebPage, webResourcesImageCache, webResourcesHtmlCache, service);
            wrp.StartPosition = FormStartPosition.CenterParent;

            if (wrp.ShowDialog() == DialogResult.OK)
            {
                txtAreaUrl.Text = "$webresource:" + wrp.SelectedResource;
            }
        }
    }
}
