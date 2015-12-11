// PROJECT : MsCrmTools.AccessChecker
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.AccessChecker.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.AccessChecker
{
    public partial class AccessChecker : PluginControlBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of class <see cref="AccessChecker"/>
        /// </summary>
        public AccessChecker()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Methods

        private void BrowseUser()
        {
            var form = new CrmUserPickerForm(new CrmAccess(Service));

            if (form.ShowDialog() == DialogResult.OK)
            {
                foreach (Guid userId in form.SelectedUsers.Keys)
                {
                    textBox_UserID.Text = form.SelectedUsers[userId];
                    textBox_UserID.Tag = userId;
                }
            }
        }

        private void BtnBrowseClick(object sender, EventArgs e)
        {
            ExecuteMethod(BrowseUser);
        }

        private void BtnRetrieveEntitiesClick(object sender, EventArgs e)
        {
            ExecuteMethod(ProcessRetrieveEntities);
        }

        private void BtnRetrieveRightsClick(object sender, EventArgs e)
        {
            ExecuteMethod(RetrieveAccessRights);
        }

        private void BtnSearchRecordIdClick(object sender, EventArgs e)
        {
            if (cBoxEntities.SelectedIndex < 0)
            {
                MessageBox.Show(ParentForm, "Please select an entity in the list before using the search action",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var lp = new LookupSingle(((EntityInfo)cBoxEntities.SelectedItem).LogicalName, Service);
            lp.StartPosition = FormStartPosition.CenterParent;
            if (lp.ShowDialog() == DialogResult.OK)
            {
                txtObjectId.Text = lp.SelectedRecordId.ToString("B");
            }
        }

        private void cbbEntity_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index == -1) return;

            // The ComboBox is bound to a DataTable,
            // so the items are DataRowView objects.
            var attr = (EntityInfo)((ComboBox)sender).Items[e.Index];

            // Retrieve the value of each column.
            string displayName = attr.DisplayName;
            string logicalName = attr.LogicalName;

            // Get the bounds for the first column
            Rectangle r1 = e.Bounds;
            r1.Width /= 2;

            // Draw the text on the first column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(displayName, e.Font, sb, r1);
            }

            // Get the bounds for the second column
            Rectangle r2 = e.Bounds;
            r2.X = e.Bounds.Width / 2;
            r2.Width /= 2;

            // Draw the text on the second column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(logicalName, e.Font, sb, r2);
            }
        }

        private void CBoxEntitiesSelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearchRecordId.Enabled = cBoxEntities.SelectedItem != null;
        }

        private void ProcessRetrieveEntities()
        {
            cBoxEntities.Items.Clear();

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving Entities...",
                AsyncArgument = null,
                Work = (bw, e) =>
                {
                    var request = new RetrieveAllEntitiesRequest { EntityFilters = EntityFilters.Entity };
                    var response = (RetrieveAllEntitiesResponse)Service.Execute(request);

                    e.Result = response.EntityMetadata;
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var emds = (EntityMetadata[])e.Result;

                        foreach (var emd in emds)
                        {
                            cBoxEntities.Items.Add(new EntityInfo(emd.LogicalName, emd.DisplayName != null && emd.DisplayName.UserLocalizedLabel != null ? emd.DisplayName.UserLocalizedLabel.Label : "N/A", emd.PrimaryNameAttribute));
                        }

                        cBoxEntities.DrawMode = DrawMode.OwnerDrawFixed;
                        cBoxEntities.DrawItem += cbbEntity_DrawItem;

                        cBoxEntities.SelectedIndex = 0;
                    }
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
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

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving access rights...",
                AsyncArgument = parameters,
                Work = (bw, e) =>
                {
                    var crmAccess = new CrmAccess(Service);
                    var inParameters = (List<string>)e.Argument;
                    var recordId = new Guid(inParameters[0]);
                    var userId = new Guid(inParameters[1]);
                    var entityName = inParameters[2];
                    var primaryAttribute = inParameters[3];
                    var result = new CheckAccessResult();

                    RetrievePrincipalAccessResponse response = crmAccess.RetrieveRights(
                        userId,
                        recordId,
                        entityName);

                    Dictionary<string, Guid> privileges = crmAccess.RetrievePrivileges(entityName);

                    // Get primary attribute value for the current record
                    Entity de = crmAccess.RetrieveDynamicWithPrimaryAttr(recordId, entityName, primaryAttribute);
                    result.RecordName = de.GetAttributeValue<string>(primaryAttribute);

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
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                    else
                    {
                        var result = (CheckAccessResult)e.Result;
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
                }
            });
        }

        private void TsbCloseClick(object sender, System.EventArgs e)
        {
            CloseTool();
        }

        #endregion Methods
    }
}