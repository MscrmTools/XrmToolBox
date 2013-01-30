// PROJECT : MsCrmTools.AccessChecker
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.AccessChecker.Forms;
using XrmToolBox;

namespace MsCrmTools.AccessChecker
{
    public partial class MainControl : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Microsoft Dynamics CRM 2011 Organization Service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Panel used to display progress information
        /// </summary>
        private Panel infoPanel;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class <see cref="MainControl"/>
        /// </summary>
        public MainControl()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the organization service used by the tool
        /// </summary>
        public IOrganizationService Service
        {
            get { return service; }
        }

        /// <summary>
        /// Gets the logo to display in the tools list
        /// </summary>
        public Image PluginLogo
        {
            get { return toolImageList.Images[0]; }
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// EventHandler to request a connection to an organization
        /// </summary>
        public event EventHandler OnRequestConnection;

        /// <summary>
        /// EventHandler to close the current tool
        /// </summary>
        public event EventHandler OnCloseTool;

        #endregion EventHandlers

        #region Methods

        /// <summary>
        /// Updates the organization service used by the tool
        /// </summary>
        /// <param name="newService">Organization service</param>
        /// <param name="actionName">Action that requested a service update</param>
        /// <param name="parameter">Parameter passed when requesting a service update</param>
        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;

            if (actionName == "RetrieveEntities")
            {
                ProcessRetrieveEntities();
            }
            if (actionName == "BrowseUser")
            {
                BrowseUser();
            }
            if (actionName == "RetrieveAccessRights")
            {
                RetrieveAccessRights();
            }
        }

        private void BtnRetrieveEntitiesClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs { ActionName = "RetrieveEntities", Control = this, Parameter = null };
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                ProcessRetrieveEntities();
            }
        }

        private void ProcessRetrieveEntities()
        {
            infoPanel = InformationPanel.GetInformationPanel(this, "Retrieving entities...", 340, 100);

            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var request = new RetrieveAllEntitiesRequest {EntityFilters = EntityFilters.Entity};
            var response = (RetrieveAllEntitiesResponse) service.Execute(request);

            e.Result = response.EntityMetadata;
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                var emds = (EntityMetadata[])e.Result;

                foreach (var emd in emds)
                {
                    var eInfo = new EntityInfo(emd.LogicalName,
                                               emd.DisplayName != null && emd.DisplayName.UserLocalizedLabel != null
                                                   ? emd.DisplayName.UserLocalizedLabel.Label
                                                   : "N/A", emd.PrimaryNameAttribute);
                    
                    cBoxEntities.Items.Add(eInfo);
                }

                cBoxEntities.SelectedIndex = 0;
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
        }

        private void TsbCloseClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }

        #endregion Methods

        private void BtnBrowseClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs { ActionName = "BrowseUser", Control = this, Parameter = null };
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                BrowseUser();
            }
        }

        private void BrowseUser()
        {
            var form = new CrmUserPickerForm(new CrmAccess(service));

            if (form.ShowDialog() == DialogResult.OK)
            {
                foreach (Guid userId in form.SelectedUsers.Keys)
                {
                    textBox_UserID.Text = form.SelectedUsers[userId];
                    textBox_UserID.Tag = userId;
                }
            }
        }

        private void BtnRetrieveRightsClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs { ActionName = "RetrieveAccessRights", Control = this, Parameter = null };
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                RetrieveAccessRights();
            }
        }

        private void RetrieveAccessRights()
        {
            if (cBoxEntities.SelectedIndex < 0)
            {
                MessageBox.Show(this, "Please select an entity", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtObjectId.Text.Length == 0)
            {
                MessageBox.Show(this, "Please specify an object Id", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox_UserID.Text.Length == 0)
            {
                MessageBox.Show(this, "Please select a user", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var g = new Guid(txtObjectId.Text);
            }
            catch 
            {
                MessageBox.Show(this, "The object ID is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var parameters = new List<string> {txtObjectId.Text, textBox_UserID.Tag.ToString(), 
                ((EntityInfo)cBoxEntities.SelectedItem).LogicalName,
            ((EntityInfo)cBoxEntities.SelectedItem).PrimaryAttribute};

            infoPanel = InformationPanel.GetInformationPanel(this, "Retrieving access rights...", 340, 100);

            // Reset panel colors


            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoRetrieveRightsWork;
            worker.RunWorkerCompleted += WorkerRunRetrieveRightsWorkerCompleted;
            worker.RunWorkerAsync(parameters);
        }
       
        private void WorkerDoRetrieveRightsWork(object sender, DoWorkEventArgs e)
        {
            var crmAccess = new CrmAccess(service);
            var parameters = (List<string>) e.Argument;
            var recordId = new Guid(parameters[0]);
            var userId = new Guid(parameters[1]);
            var entityName = parameters[2];
            var primaryAttribute = parameters[3];
            var result = new CheckAccessResult();

            RetrievePrincipalAccessResponse response = crmAccess.RetrieveRights(
                userId,
                recordId,
                entityName);

            Dictionary<string, Guid> privileges = crmAccess.RetrievePrivileges(entityName);
            
            // Get primary attribute value for the current record
            Entity de = crmAccess.RetrieveDynamicWithPrimaryAttr(recordId, entityName, primaryAttribute);
            result.RecordName = de[primaryAttribute].ToString();

            // Check Privileges
            foreach (string privlegeName in privileges.Keys)
            {
                if (privlegeName.StartsWith("prvappend"))
                {
                    result.HasAppendAccess = (response.AccessRights & AccessRights.AppendAccess) == AccessRights.AppendAccess;
                    result.AppendPrivilegeId = privileges[privlegeName];
                }

                if (privlegeName.StartsWith("prvappendto"))
                {
                    result.HasAppendToAccess = (response.AccessRights & AccessRights.AppendToAccess) == AccessRights.AppendToAccess;
                    result.AppendToPrivilegeId = privileges[privlegeName];
                }

                if (privlegeName.StartsWith("prvassign"))
                {
                    result.HasAssignAccess = (response.AccessRights & AccessRights.AssignAccess) == AccessRights.AssignAccess;
                    result.AssignPrivilegeId = privileges[privlegeName];
                }

                if (privlegeName.StartsWith("prvcreate"))
                {
                    result.HasCreateAccess = (response.AccessRights & AccessRights.CreateAccess) == AccessRights.CreateAccess;
                    result.CreatePrivilegeId = privileges[privlegeName];
                }

                if (privlegeName.StartsWith("prvdelete"))
                {
                    result.HasDeleteAccess = (response.AccessRights & AccessRights.DeleteAccess) == AccessRights.DeleteAccess;
                    result.DeletePrivilegeId = privileges[privlegeName];
                }

                if (privlegeName.StartsWith("prvread"))
                {
                    result.HasReadAccess = (response.AccessRights & AccessRights.ReadAccess) == AccessRights.ReadAccess;
                    result.ReadPrivilegeId = privileges[privlegeName];
                }

                if (privlegeName.StartsWith("prvshare"))
                {
                    result.HasShareAccess = (response.AccessRights & AccessRights.ShareAccess) == AccessRights.ShareAccess;
                    result.SharePrivilegeId = privileges[privlegeName];
                }

                if (privlegeName.StartsWith("prvwrite"))
                {
                    result.HasWriteAccess = (response.AccessRights & AccessRights.WriteAccess) == AccessRights.WriteAccess;
                    result.WritePrivilegeId = privileges[privlegeName];
                }
            }

            e.Result = result;
        }

        private void WorkerRunRetrieveRightsWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                var result = (CheckAccessResult) e.Result;
                var greenColor = Color.FromArgb(0, 158, 73);
                var redColor = Color.FromArgb(232, 17, 35);

                textBox_PrimaryAttribute.Text = result.RecordName;

                pnlAppend.BackColor = result.HasAppendAccess
                                          ? greenColor
                                          : result.AppendPrivilegeId != Guid.Empty ? redColor : Color.DarkGray;
                pnlAppendTo.BackColor = result.HasAppendToAccess
                                            ? greenColor
                                            : result.AppendToPrivilegeId != Guid.Empty ? redColor : Color.DarkGray;
                pnlAssign.BackColor = result.HasAssignAccess
                                          ? greenColor
                                          : result.AssignPrivilegeId != Guid.Empty ? redColor : Color.DarkGray;
                pnlCreate.BackColor = result.HasCreateAccess
                                          ? greenColor
                                          : result.CreatePrivilegeId != Guid.Empty ? redColor : Color.DarkGray;
                pnlDelete.BackColor = result.HasDeleteAccess
                                          ? greenColor
                                          : result.DeletePrivilegeId != Guid.Empty ? redColor : Color.DarkGray;
                pnlRead.BackColor = result.HasReadAccess
                                        ? greenColor
                                        : result.ReadPrivilegeId != Guid.Empty ? redColor : Color.DarkGray;
                pnlShare.BackColor = result.HasShareAccess
                                         ? greenColor
                                         : result.SharePrivilegeId != Guid.Empty ? redColor : Color.DarkGray;
                pnlWrite.BackColor = result.HasWriteAccess
                                         ? greenColor
                                         : result.WritePrivilegeId != Guid.Empty ? redColor : Color.DarkGray;

                lblPrivilegeAppend.Text = result.AppendPrivilegeId == Guid.Empty
                                              ? "Privilege does not exist for this entity"
                                              : string.Format("Privilege Id: {0}",
                                                              result.AppendPrivilegeId.ToString("B"));
                lblPrivilegeAppendTo.Text = result.AppendToPrivilegeId == Guid.Empty
                                                ? "Privilege does not exist for this entity"
                                                : string.Format("Privilege Id: {0}",
                                                                result.AppendToPrivilegeId.ToString("B"));
                lblPrivilegeAssign.Text = result.AssignPrivilegeId == Guid.Empty
                                              ? "Privilege does not exist for this entity"
                                              : string.Format("Privilege Id: {0}",
                                                              result.AssignPrivilegeId.ToString("B"));
                lblPrivilegeCreate.Text = result.CreatePrivilegeId == Guid.Empty
                                              ? "Privilege does not exist for this entity"
                                              : string.Format("Privilege Id: {0}",
                                                              result.CreatePrivilegeId.ToString("B"));
                lblPrivilegeDelete.Text = result.DeletePrivilegeId == Guid.Empty
                                              ? "Privilege does not exist for this entity"
                                              : string.Format("Privilege Id: {0}",
                                                              result.DeletePrivilegeId.ToString("B"));
                lblPrivilegeRead.Text = result.ReadPrivilegeId == Guid.Empty
                                            ? "Privilege does not exist for this entity"
                                            : string.Format("Privilege Id: {0}", result.ReadPrivilegeId.ToString("B"));
                lblPrivilegeShare.Text = result.SharePrivilegeId == Guid.Empty
                                             ? "Privilege does not exist for this entity"
                                             : string.Format("Privilege Id: {0}", result.SharePrivilegeId.ToString("B"));
                lblPrivilegeWrite.Text = result.WritePrivilegeId == Guid.Empty
                                             ? "Privilege does not exist for this entity"
                                             : string.Format("Privilege Id: {0}", result.WritePrivilegeId.ToString("B"));
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
        }

        private void BtnSearchRecordIdClick(object sender, EventArgs e)
        {
            var lp = new LookupSingle(((EntityInfo)cBoxEntities.SelectedItem).LogicalName, service);
            lp.StartPosition = FormStartPosition.CenterParent;
            if (lp.ShowDialog() == DialogResult.OK)
            {
                txtObjectId.Text = lp.SelectedRecordId.ToString("B");
            }
        }

        private void CBoxEntitiesSelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearchRecordId.Enabled = cBoxEntities.SelectedItem != null;
        }
    }
}
