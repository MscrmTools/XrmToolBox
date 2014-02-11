﻿// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MsCrmTools.SiteMapEditor.AppCode;
using MsCrmTools.SiteMapEditor.Forms;
using MsCrmTools.SiteMapEditor.Forms.WebRessources;

namespace MsCrmTools.SiteMapEditor.Controls
{
   
    public partial class SubAreaControl : UserControl, ISiteMapSavable
    {
        private readonly Dictionary<string,string> collec;

        private string initialEntity = "";
        private string initialGetStartedPanePath = "";
        private string initialGetStartedPanePathAdmin = "";
        private string initialGetStartedPanePathOutlook = "";
        private string initialGetStartedPanePathAdminOutlook = "";
        private string initialIcon = "";
        private string initialId = "";
        private string initialUrl = "";
        private string initialTitle = "";
        private string initialDescription = "";
        private string initialDefaultDashboard = "";
        private bool isCrm2013Area = false;

        private bool initialAvailableOffline;
        private bool initialPassParams;
        private bool initialClientOutlook;
        private bool initialClientOutlookLaptopClient;
        private bool initialClientOutlookWorkstationClient;
        private bool initialClientWeb;
        private bool initialSkuAll;
        private bool initialSkuLive;
        private bool initialSkuOnPremise;
        private bool initialSkuSPLA;

        #region Delegates

        public delegate void SaveEventHandler(object sender, SaveEventArgs e);

        #endregion

        #region Event Handlers

        public event SaveEventHandler Saved;

        #endregion

        public SubAreaControl()
        {
            InitializeComponent();

            collec = new Dictionary<string, string>();

            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = "Information";
            tip.SetToolTip(chkSubAreaAvailableOffline, "Controls whether SubArea is available offline.");
            tip.SetToolTip(chkSubAreaPassParams, "Specifies whether information about the organization and language context are passed to the URL.");
            tip.SetToolTip(txtOutlookShortcutIcon, "Specifies the icon to display in Microsoft Dynamics CRM for Microsoft Office Outlook.");
            tip.SetToolTip(txtSubAreaEntity, "Specifies the name for the entity. If a Url is not specified, the default view of the specified entity will be displayed.");
            tip.SetToolTip(txtSubAreaGetStartedPanePath, "Specifies the path to the Get Started page for this subarea.");
            tip.SetToolTip(txtSubAreaGetStartedPanePathAdmin, "Specifies the path to the Get Started page for this subarea if the user is logged in as an administrator.");
            tip.SetToolTip(txtSubAreaGetStartedPanePathAdminOutlook, "Specifies the path to the Get Started page for this subarea if the user is logged in as an administrator and Microsoft Dynamics CRM for Outlook is in use.");
            tip.SetToolTip(txtSubAreaGetStartedPanePathOutlook, "Specifies the path to the Get Started page for this subarea when Microsoft Dynamics CRM for Outlook is in use.");
            tip.SetToolTip(txtSubAreaIcon, "Specifies a URL for an 18x18 pixel image to display for the SubArea.");
            tip.SetToolTip(txtSubAreaId, "A unique identifier for this SubArea element.\r\n\r\nValid values: a-z, A-Z, 0-9, and underscore (_)");
            tip.SetToolTip(txtSubAreaUrl, "Specifies a URL or HTML Web Resource for a page to display in the main frame of the application when this subarea is selected.");
            tip.SetToolTip(txtSubAreaTitle, "Deprecated. Use the <Titles> (SiteMap) and <Title> (SiteMap) elements.");
            tip.SetToolTip(txtSubAreaDescription, "Deprecated. Use the <Description> (SiteMap) element.");
            tip.SetToolTip(txtDefaultDashboardId, "This functionality is introduced in Microsoft Dynamics CRM 2013 and the Microsoft Dynamics CRM Online Fall '13 Service Update. \r\nSpecifies the GUID for default dashboard to be displayed for this subarea.");

        }

        public SubAreaControl(Dictionary<string, string> collection)
            : this()
        {

            collec = collection;

            FillControls();
        }

        private void FillControls()
        {
            txtSubAreaEntity.Text = collec.ContainsKey("Entity") ? collec["Entity"]: "";
            txtSubAreaGetStartedPanePath.Text = collec.ContainsKey("GetStartedPanePath") ? collec["GetStartedPanePath"]: "";
            txtSubAreaGetStartedPanePathAdmin.Text = collec.ContainsKey("GetStartedPanePathAdmin") ? collec["GetStartedPanePathAdmin"] : "";
            txtSubAreaGetStartedPanePathAdminOutlook.Text = collec.ContainsKey("GetStartedPanePathAdminOutlook") ? collec["GetStartedPanePathAdminOutlook"]: "";
            txtSubAreaGetStartedPanePathOutlook.Text = collec.ContainsKey("GetStartedPanePathOutlook") ? collec["GetStartedPanePathOutlook"] : "";
            txtSubAreaIcon.Text = collec.ContainsKey("Icon") ? collec["Icon"] : "";
            txtSubAreaId.Text = collec.ContainsKey("Id") ? collec["Id"] : "";
            txtSubAreaUrl.Text = collec.ContainsKey("Url") ? collec["Url"] : "";
            txtSubAreaResourceId.Text = collec.ContainsKey("ResourceId") ? collec["ResourceId"] : "";
            txtSubAreaDescriptionResourceId.Text = collec.ContainsKey("DescriptionResourceId") ? collec["DescriptionResourceId"] : "";
            txtSubAreaTitle.Text = collec.ContainsKey("Title") ? collec["Title"] : "";
            txtSubAreaDescription.Text = collec.ContainsKey("Description") ? collec["Description"] : "";
            if (collec.ContainsKey("DefaultDashboard") && collec.ContainsKey("Url") && collec["Url"].ToLower() == "/workplace/home_dashboards.aspx")
            {
                txtDefaultDashboardId.Text = collec["DefaultDashboard"];
                isCrm2013Area = true;
            }
            initialEntity = txtSubAreaEntity.Text;
            initialGetStartedPanePath = txtSubAreaGetStartedPanePath.Text;
            initialGetStartedPanePathAdmin = txtSubAreaGetStartedPanePathAdmin.Text;
            initialGetStartedPanePathOutlook = txtSubAreaGetStartedPanePathOutlook.Text;
            initialGetStartedPanePathAdminOutlook = txtSubAreaGetStartedPanePathAdminOutlook.Text;
            initialIcon = txtSubAreaIcon.Text;
            initialId = txtSubAreaId.Text;
            initialUrl = txtSubAreaUrl.Text;
            initialDescription = txtSubAreaDescription.Text;
            initialTitle = txtSubAreaTitle.Text;
            initialDefaultDashboard = txtDefaultDashboardId.Text;

            //label4.Enabled = isCrm2013Area;
            //txtDefaultDashboardId.Enabled = isCrm2013Area;
            //btnBrowseDashboard.Enabled = isCrm2013Area;

            chkSubAreaAvailableOffline.Checked = collec.ContainsKey("AvailableOffline") ? collec["AvailableOffline"].ToLower() == "true" : false;
            chkSubAreaPassParams.Checked = collec.ContainsKey("PassParams") ? collec["PassParams"].ToLower() == "true" : false;
            chkSubAreaClientOutlook.Checked = collec.ContainsKey("Client") ? collec["Client"].IndexOf("Outlook") >= 0 : false;
            chkSubAreaClientOutlookLaptopClient.Checked = collec.ContainsKey("Client") ? collec["Client"].IndexOf("OutlookLaptopClient") >= 0 : false;
            chkSubAreaClientOutlookWorkstationClient.Checked = collec.ContainsKey("Client") ? collec["Client"].IndexOf("OutlookWorkstationClient") >= 0 : false;
            chkSubAreaClientWeb.Checked = collec.ContainsKey("Client") ? collec["Client"].IndexOf("Web") >= 0 : false;
            chkSubAreaSkuAll.Checked = collec.ContainsKey("Sku") ? collec["Sku"].IndexOf("All") >= 0 : false;
            chkSubAreaSkuLive.Checked = collec.ContainsKey("Sku") ? collec["Sku"].IndexOf("Live") >= 0 : false;
            chkSubAreaSkuOnPremise.Checked = collec.ContainsKey("Sku") ? collec["Sku"].IndexOf("OnPremise") >= 0 : false;
            chkSubAreaSkuSPLA.Checked = collec.ContainsKey("Sku") ? collec["Sku"].IndexOf("SPLA") >= 0 : false;

            initialAvailableOffline = chkSubAreaAvailableOffline.Checked;
            initialPassParams= chkSubAreaPassParams.Checked;
            initialClientOutlook = chkSubAreaClientOutlook.Checked;
            initialClientOutlookLaptopClient = chkSubAreaClientOutlookLaptopClient.Checked;
            initialClientOutlookWorkstationClient = chkSubAreaClientOutlookWorkstationClient.Checked;
            initialClientWeb = chkSubAreaClientWeb.Checked;
            initialSkuAll = chkSubAreaSkuAll.Checked;
            initialSkuLive= chkSubAreaSkuLive.Checked;
            initialSkuOnPremise = chkSubAreaSkuOnPremise.Checked;
            initialSkuSPLA = chkSubAreaSkuSPLA.Checked;
        }

        private void SubAreaControl_Leave(object sender, EventArgs e)
        {
            if (initialEntity != txtSubAreaEntity.Text ||
            initialGetStartedPanePath != txtSubAreaGetStartedPanePath.Text ||
            initialGetStartedPanePathAdmin != txtSubAreaGetStartedPanePathAdmin.Text ||
            initialGetStartedPanePathOutlook != txtSubAreaGetStartedPanePathOutlook.Text ||
            initialGetStartedPanePathAdminOutlook != txtSubAreaGetStartedPanePathAdminOutlook.Text ||
            initialIcon != txtSubAreaIcon.Text ||
            initialId != txtSubAreaId.Text ||
            initialUrl != txtSubAreaUrl.Text ||
            initialDescription != txtSubAreaDescription.Text ||
            initialTitle != txtSubAreaTitle.Text ||
            initialAvailableOffline != chkSubAreaAvailableOffline.Checked ||
            initialClientOutlook != chkSubAreaClientOutlook.Checked ||
            initialClientOutlookLaptopClient != chkSubAreaClientOutlookLaptopClient.Checked ||
            initialClientOutlookWorkstationClient != chkSubAreaClientOutlookWorkstationClient.Checked ||
            initialClientWeb != chkSubAreaClientWeb.Checked ||
            initialSkuAll != chkSubAreaSkuAll.Checked ||
            initialSkuLive != chkSubAreaSkuLive.Checked ||
            initialSkuOnPremise != chkSubAreaSkuOnPremise.Checked ||
            initialSkuSPLA != chkSubAreaSkuSPLA.Checked ||
                initialDefaultDashboard != txtDefaultDashboardId.Text)
            {
                if (MessageBox.Show("You didn't save your changes! Do you want to save them now?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Save();
                }
            }
        }

        public void Save()
        {
            if (txtSubAreaId.Text.Length == 0)
            {
                MessageBox.Show(this, "Id is required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Dictionary<string, string> collection = new Dictionary<string, string>();
                if (txtSubAreaEntity.Text.Length > 0)
                    collection.Add("Entity", txtSubAreaEntity.Text);
                if (txtSubAreaGetStartedPanePath.Text.Length > 0)
                    collection.Add("GetStartedPanePath", txtSubAreaGetStartedPanePath.Text);
                if (txtSubAreaGetStartedPanePathAdmin.Text.Length > 0)
                    collection.Add("GetStartedPanePathAdmin", txtSubAreaGetStartedPanePathAdmin.Text);
                if (txtSubAreaGetStartedPanePathAdminOutlook.Text.Length > 0)
                    collection.Add("GetStartedPanePathAdminOutlook", txtSubAreaGetStartedPanePathAdminOutlook.Text);
                if (txtSubAreaGetStartedPanePathOutlook.Text.Length > 0)
                    collection.Add("GetStartedPanePathOutlook", txtSubAreaGetStartedPanePathOutlook.Text);
                if (txtSubAreaIcon.Text.Length > 0)
                    collection.Add("Icon", txtSubAreaIcon.Text);
                if (txtSubAreaId.Text.Length > 0)
                    collection.Add("Id", txtSubAreaId.Text);
                if (txtSubAreaUrl.Text.Length > 0)
                    collection.Add("Url", txtSubAreaUrl.Text);
                if (txtSubAreaResourceId.Text.Length > 0)
                    collection.Add("ResourceId", txtSubAreaResourceId.Text);
                if (txtSubAreaDescriptionResourceId.Text.Length > 0)
                    collection.Add("DescriptionResourceId", txtSubAreaDescriptionResourceId.Text);
                if (txtSubAreaDescription.Text.Length > 0)
                    collection.Add("Description", txtSubAreaDescription.Text);
                if (txtSubAreaTitle.Text.Length > 0)
                    collection.Add("Title", txtSubAreaTitle.Text);
                if (txtDefaultDashboardId.Text.Length > 0)// && isCrm2013Area)
                {
                    collection.Add("DefaultDashboard", txtDefaultDashboardId.Text);
                }
                collection.Add("AvailableOffline", chkSubAreaAvailableOffline.Checked.ToString().ToLower());
                collection.Add("PassParams", chkSubAreaPassParams.Checked.ToString().ToLower());

                string client = "";

                if (chkSubAreaAll.Checked)
                    client += "All";
                if (chkSubAreaClientOutlook.Checked)
                    client += ",Outlook";
                if (chkSubAreaClientOutlookLaptopClient.Checked)
                    client += ",OutlookLaptopClient";
                if (chkSubAreaClientOutlookWorkstationClient.Checked)
                    client += ",OutlookWorkstationClient";
                if (chkSubAreaClientWeb.Checked)
                    client += ",Web";

                if (client.StartsWith(","))
                    client = client.Remove(0, 1);

                if (client.Length > 0)
                    collection.Add("Client", client);

                string sku = "";

                if (chkSubAreaSkuAll.Checked)
                    sku += "All";
                if (chkSubAreaSkuLive.Checked)
                    sku += ",Live";
                if (chkSubAreaSkuOnPremise.Checked)
                    sku += ",OnPremise";
                if (chkSubAreaSkuSPLA.Checked)
                    sku += ",SPLA";

                if (sku.StartsWith(","))
                    sku = sku.Remove(0, 1);

                if (sku.Length > 0)
                    collection.Add("Sku", sku);

                initialEntity = txtSubAreaEntity.Text;
                initialGetStartedPanePath = txtSubAreaGetStartedPanePath.Text;
                initialGetStartedPanePathAdmin = txtSubAreaGetStartedPanePathAdmin.Text;
                initialGetStartedPanePathOutlook = txtSubAreaGetStartedPanePathOutlook.Text;
                initialGetStartedPanePathAdminOutlook = txtSubAreaGetStartedPanePathAdminOutlook.Text;
                initialIcon = txtSubAreaIcon.Text;
                initialId = txtSubAreaId.Text;
                initialUrl = txtSubAreaUrl.Text;
                initialDescription = txtSubAreaDescription.Text;
                initialTitle = txtSubAreaTitle.Text;
                initialDefaultDashboard = txtDefaultDashboardId.Text;

                initialAvailableOffline = chkSubAreaAvailableOffline.Checked;
                initialPassParams = chkSubAreaPassParams.Checked;
                initialClientOutlook = chkSubAreaClientOutlook.Checked;
                initialClientOutlookLaptopClient = chkSubAreaClientOutlookLaptopClient.Checked;
                initialClientOutlookWorkstationClient = chkSubAreaClientOutlookWorkstationClient.Checked;
                initialClientWeb = chkSubAreaClientWeb.Checked;
                initialSkuAll = chkSubAreaSkuAll.Checked;
                initialSkuLive = chkSubAreaSkuLive.Checked;
                initialSkuOnPremise = chkSubAreaSkuOnPremise.Checked;
                initialSkuSPLA = chkSubAreaSkuSPLA.Checked;

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

        private void buttonSelectEntity_Click(object sender, EventArgs e)
        {
            EntityPicker picker = new EntityPicker();
            picker.StartPosition = FormStartPosition.CenterParent;

            if (picker.ShowDialog() == DialogResult.OK)
            {
                txtSubAreaEntity.Text = picker.SelectedEntity;
            }
        }

        private void btnBrowsIcon_Click(object sender, EventArgs e)
        {
            WebResourcePicker wrp = new WebResourcePicker(WebResourcePicker.WebResourceType.Image);
            wrp.StartPosition = FormStartPosition.CenterParent;

            if (wrp.ShowDialog() == DialogResult.OK)
            {
                txtSubAreaIcon.Text = "$webresource:" + wrp.SelectedResource;
            }
        }

        private void buttonBrowseUrl_Click(object sender, EventArgs e)
        {
            WebResourcePicker wrp = new WebResourcePicker(WebResourcePicker.WebResourceType.WebPage);
            wrp.StartPosition = FormStartPosition.CenterParent;

            if (wrp.ShowDialog() == DialogResult.OK)
            {
                txtSubAreaUrl.Text = "$webresource:" + wrp.SelectedResource;
            }
        }

        private void btnBrowseDashboard_Click(object sender, EventArgs e)
        {
            var dPicker = new DashboardPicker() { StartPosition = FormStartPosition.CenterParent };
            if (dPicker.ShowDialog(ParentForm) == DialogResult.OK)
            {
                txtDefaultDashboardId.Text = dPicker.SelectedDashboard.Id.ToString("B");
            }
        }
    }
}
