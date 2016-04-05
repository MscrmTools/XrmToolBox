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
    public partial class PrivilegeControl : UserControl, ISiteMapSavable
    {
        private readonly Dictionary<string, string> collec;

        private string initialEntity = "";
        private bool initialPrivAll;
        private bool initialPrivAllowQuickCampaign;
        private bool initialPrivAppend;
        private bool initialPrivAppendTo;
        private bool initialPrivAssign;
        private bool initialPrivCreate;
        private bool initialPrivDelete;
        private bool initialPrivRead;
        private bool initialPrivShare;
        private bool initialPrivUseInternetMarketing;
        private bool initialPrivWrite;
        private ToolTip tip;

        #region Delegates

        public delegate void SaveEventHandler(object sender, SaveEventArgs e);

        #endregion Delegates

        #region Event Handlers

        public event SaveEventHandler Saved;

        #endregion Event Handlers

        public PrivilegeControl()
        {
            InitializeComponent();

            collec = new Dictionary<string, string>();

            tip = new ToolTip();
            tip.ToolTipTitle = "Information";
            tip.SetToolTip(txtPrivilegeEntity, "Specifies the name of the entity to check privileges for.");
        }

        public PrivilegeControl(Dictionary<string, string> collection)
            : this()
        {
            collec = collection;

            FillControls();
        }

        public void Save()
        {
            Dictionary<string, string> collection = new Dictionary<string, string>();

            string privilege = "";

            if (chkPrivilegeAll.Checked)
                privilege += "All";
            if (chkPrivilegeAllowQuickCampaign.Checked)
                privilege += ",AllowQuickCampaign";
            if (chkPrivilegeAppend.Checked)
                privilege += ",Append";
            if (chkPrivilegeAppendTo.Checked)
                privilege += ",AppendTo";
            if (chkPrivilegeAssign.Checked)
                privilege += ",Assign";
            if (chkPrivilegeCreate.Checked)
                privilege += ",Create";
            if (chkPrivilegeDelete.Checked)
                privilege += ",Delete";
            if (chkPrivilegeRead.Checked)
                privilege += ",Read";
            if (chkPrivilegeShare.Checked)
                privilege += ",Share";
            if (chkPrivilegeUseInternetMarketing.Checked)
                privilege += ",UseInternetMarketing";
            if (chkPrivilegeWrite.Checked)
                privilege += ",Write";

            if (privilege.StartsWith(","))
                privilege = privilege.Remove(0, 1);

            if (txtPrivilegeEntity.Text.Length > 0)
                collection.Add("Entity", txtPrivilegeEntity.Text);
            if (privilege.Length > 0)
                collection.Add("Privilege", privilege);

            if (collec.ContainsKey("_disabled"))
            {
                collection.Add("_disabled", collec["_disabled"]);
            }

            initialEntity = txtPrivilegeEntity.Text;
            initialPrivCreate = chkPrivilegeCreate.Checked;
            initialPrivRead = chkPrivilegeRead.Checked;
            initialPrivWrite = chkPrivilegeWrite.Checked;
            initialPrivDelete = chkPrivilegeDelete.Checked;
            initialPrivAppend = chkPrivilegeAppend.Checked;
            initialPrivAppendTo = chkPrivilegeAppendTo.Checked;
            initialPrivShare = chkPrivilegeShare.Checked;
            initialPrivAssign = chkPrivilegeAssign.Checked;
            initialPrivAll = chkPrivilegeAll.Checked;
            initialPrivAllowQuickCampaign = chkPrivilegeAllowQuickCampaign.Checked;
            initialPrivUseInternetMarketing = chkPrivilegeUseInternetMarketing.Checked;

            SendSaveMessage(collection);
        }

        private void FillControls()
        {
            txtPrivilegeEntity.Text = collec.ContainsKey("Entity") ? collec["Entity"] : "";
            chkPrivilegeAll.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("All") >= 0 : false;
            chkPrivilegeAllowQuickCampaign.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("AllowQuickCampaign") >= 0 : false;
            chkPrivilegeAppend.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("Append") >= 0 : false;
            chkPrivilegeAppendTo.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("AppendTo") >= 0 : false;
            chkPrivilegeAssign.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("Assign") >= 0 : false;
            chkPrivilegeCreate.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("Create") >= 0 : false;
            chkPrivilegeDelete.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("Delete") >= 0 : false;
            chkPrivilegeRead.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("Read") >= 0 : false;
            chkPrivilegeShare.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("Share") >= 0 : false;
            chkPrivilegeUseInternetMarketing.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("UseInternetMarketing") >= 0 : false;
            chkPrivilegeWrite.Checked = collec.ContainsKey("Privilege") ? collec["Privilege"].IndexOf("Write") >= 0 : false;

            initialEntity = txtPrivilegeEntity.Text;
            initialPrivCreate = chkPrivilegeCreate.Checked;
            initialPrivRead = chkPrivilegeRead.Checked;
            initialPrivWrite = chkPrivilegeWrite.Checked;
            initialPrivDelete = chkPrivilegeDelete.Checked;
            initialPrivAppend = chkPrivilegeAppend.Checked;
            initialPrivAppendTo = chkPrivilegeAppendTo.Checked;
            initialPrivShare = chkPrivilegeShare.Checked;
            initialPrivAssign = chkPrivilegeAssign.Checked;
            initialPrivAll = chkPrivilegeAll.Checked;
            initialPrivAllowQuickCampaign = chkPrivilegeAllowQuickCampaign.Checked;
            initialPrivUseInternetMarketing = chkPrivilegeUseInternetMarketing.Checked;
        }

        private void SiteMapControl_Leave(object sender, EventArgs e)
        {
            if (initialEntity != txtPrivilegeEntity.Text ||
                   initialPrivCreate != chkPrivilegeCreate.Checked ||
                    initialPrivRead != chkPrivilegeRead.Checked ||
                    initialPrivWrite != chkPrivilegeWrite.Checked ||
                    initialPrivDelete != chkPrivilegeDelete.Checked ||
                    initialPrivAppend != chkPrivilegeAppend.Checked ||
                    initialPrivAppendTo != chkPrivilegeAppendTo.Checked ||
                    initialPrivShare != chkPrivilegeShare.Checked ||
                    initialPrivAssign != chkPrivilegeAssign.Checked ||
                    initialPrivAll != chkPrivilegeAll.Checked ||
                    initialPrivAllowQuickCampaign != chkPrivilegeAllowQuickCampaign.Checked ||
                    initialPrivUseInternetMarketing != chkPrivilegeUseInternetMarketing.Checked)
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