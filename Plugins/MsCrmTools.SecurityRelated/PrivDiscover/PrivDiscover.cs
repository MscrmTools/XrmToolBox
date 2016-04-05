// PROJECT : MsCrmTools.RolePrivilegeAnalyzer
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.PrivDiscover.AppCode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.PrivDiscover
{
    public partial class MainControl : PluginControlBase
    {
        #region Variables

        private List<EntityMetadata> entities;

        private Thread fillPrivThread;

        /// <summary>
        /// List of privileges for the current organization
        /// </summary>
        private DataCollection<Entity> privileges;

        /// <summary>
        /// List of security roles for the current organization
        /// </summary>
        private List<SecurityRole> roles;

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

        #region Methods

        private void BtnAddClick(object sender, EventArgs e)
        {
            if (lvPrivileges.SelectedItems.Count == 0)
                return;

            foreach (ListViewItem item in lvPrivileges.SelectedItems)
            {
                if (lvSelectedPrivileges.Items.Cast<ListViewItem>().Any(selectedItem => ((Privilege)selectedItem.Tag).Id == ((Entity)item.Tag).Id))
                {
                    MessageBox.Show(this, "You can't add the same privilege twice!", "Warning", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                var privilege = (Entity)item.Tag;
                string groupName;
                var group = (from entity in entities
                             where entity.Privileges.Any(p => p.PrivilegeId == privilege.Id)
                             select new { entity.LogicalName, entity.DisplayName.UserLocalizedLabel.Label }).FirstOrDefault();

                if (group == null)
                {
                    if (privilege["name"].ToString().EndsWith("Entity"))
                        groupName = "Entity";
                    else if (privilege["name"].ToString().EndsWith("Attribute"))
                        groupName = "Attribute";
                    else if (privilege["name"].ToString().EndsWith("Relationship"))
                        groupName = "Relationship";
                    else if (privilege["name"].ToString().EndsWith("OptionSet"))
                        groupName = "OptionSet";
                    else
                        groupName = "_Common";
                }
                else
                {
                    if (group.LogicalName == "customeraddress")
                        groupName =
                            entities.First(x => x.LogicalName == "account").DisplayName.UserLocalizedLabel.Label;
                    else if (group.LogicalName == "email" || group.LogicalName == "task"
                        || group.LogicalName == "letter" || group.LogicalName == "phonecall"
                        || group.LogicalName == "appointment" || group.LogicalName == "serviceappointment"
                        || group.LogicalName == "campaignresponse" || group.LogicalName == "fax")
                        groupName =
                            entities.First(x => x.LogicalName == "activitypointer").DisplayName.UserLocalizedLabel.Label;
                    else
                        groupName = group.Label;
                }

                //if (ListViewDelegates.GetGroup(lvSelectedPrivileges, groupName) == null)
                //    ListViewDelegates.AddGroup(lvSelectedPrivileges, groupName);
                if (lvSelectedPrivileges.Groups[groupName] == null)
                {
                    lvSelectedPrivileges.Groups.Add(groupName, groupName);
                }

                var priv = new Privilege { Id = privilege.Id, Name = privilege.GetAttributeValue<string>("name"), IsAnyDepth = true };

                if (rdbLevelNone.Checked)
                {
                    priv.IsAnyDepth = false;
                    priv.IsNoDepth = true;
                }
                if (rdbLevelUser.Checked)
                {
                    priv.Depth = PrivilegeDepth.Basic;
                    priv.IsAnyDepth = false;
                }
                if (rdbLevelDiv.Checked)
                {
                    priv.Depth = PrivilegeDepth.Local;
                    priv.IsAnyDepth = false;
                }
                if (rdbLevelSubDiv.Checked)
                {
                    priv.Depth = PrivilegeDepth.Deep;
                    priv.IsAnyDepth = false;
                }
                if (rdbLevelOrg.Checked)
                {
                    priv.Depth = PrivilegeDepth.Global;
                    priv.IsAnyDepth = false;
                }

                var clonedItem = (ListViewItem)item.Clone();
                clonedItem.SubItems.Add(priv.IsAnyDepth ? "Any" : priv.IsNoDepth ? "None" : GetPrivilegeDepthLabel(priv.Depth));
                clonedItem.Tag = priv;
                clonedItem.Group =
                    groupName != null
                        //? ListViewDelegates.GetGroup(lvSelectedPrivileges, groupName)
                        //: ListViewDelegates.GetGroup(lvSelectedPrivileges, "_Common");
                        ? lvSelectedPrivileges.Groups[groupName]
                        : lvSelectedPrivileges.Groups["_Common"];

                lvSelectedPrivileges.Items.Add(clonedItem);
            }

            //ListViewDelegates.SortGroup(lvSelectedPrivileges, true);
            //ListViewDelegates.Sort(lvSelectedPrivileges, true);
            ((ListViewGroupSorter)lvSelectedPrivileges).SortGroups(true);
            lvSelectedPrivileges.Sort();
        }

        private void BtnRemoveClick(object sender, EventArgs e)
        {
            if (lvSelectedPrivileges.SelectedItems.Count == 0)
                return;

            foreach (ListViewItem item in lvSelectedPrivileges.SelectedItems)
            {
                lvSelectedPrivileges.Items.Remove(item);
            }
        }

        private string GetPrivilegeDepthLabel(PrivilegeDepth privilegeDepth)
        {
            switch (privilegeDepth)
            {
                case PrivilegeDepth.Basic:
                    return "User";

                case PrivilegeDepth.Local:
                    return "Division";

                case PrivilegeDepth.Deep:
                    return "Division and sub-Divisions";

                case PrivilegeDepth.Global:
                    return "Organization";

                default:
                    throw new Exception("Not supported!");
            }
        }

        private void LoadRolesAndPrivs()
        {
            lvPrivileges.Items.Clear();
            lvSelectedPrivileges.Items.Clear();
            lvRoles.Items.Clear();

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving roles...",
                AsyncArgument = null,
                Work = (bw, e) =>
                {
                    var rManager = new RolesManager(Service);
                    roles = rManager.GetRoles();

                    bw.ReportProgress(0, "Retrieving privileges...");

                    privileges = rManager.GetPrivileges();

                    bw.ReportProgress(0, "Retrieving entities privileges...");

                    var mdManager = new MetadataManager(Service);
                    entities = mdManager.GetEntitiesWithPrivileges();
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        fillPrivThread = new Thread(DoWork);
                        fillPrivThread.Start();
                    }

                    txtSearch.Enabled = true;
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private void TsbCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void TsbLoadRolesAndPrivsClick(object sender, EventArgs e)
        {
            ExecuteMethod(LoadRolesAndPrivs);
        }

        #endregion Methods

        private void BtnDisplayRolesClick(object sender, EventArgs e)
        {
            if (lvSelectedPrivileges.Items.Count == 0)
                return;

            lvRoles.Items.Clear();

            var lviList = new List<ListViewItem>();

            foreach (SecurityRole role in roles.OrderBy(r => r.Name))
            {
                bool matchPrivileges = false;

                foreach (ListViewItem item in lvSelectedPrivileges.Items)
                {
                    var currentPrivilege = (Privilege)item.Tag;

                    if (currentPrivilege.IsNoDepth)
                    {
                        matchPrivileges = role.Privileges.All(p => p.Id != currentPrivilege.Id);
                    }
                    else if (currentPrivilege.IsAnyDepth)
                    {
                        matchPrivileges = role.Privileges.Any(p => p.Id == currentPrivilege.Id);
                    }
                    else
                    {
                        matchPrivileges = role.Privileges.Any(p => p.Id == currentPrivilege.Id && currentPrivilege.Depth == p.Depth);
                    }

                    if (!matchPrivileges)
                    {
                        break;
                    }
                }

                if (matchPrivileges)
                {
                    var item = new ListViewItem(role.Name) { Tag = role.Id, ImageIndex = 0 };
                    lviList.Add(item);
                }
            }

            lvRoles.Items.AddRange(lviList.ToArray());
        }

        private void BtnOpenSecurityRoleClick(object sender, EventArgs e)
        {
            if (lvRoles.SelectedItems.Count == 0)
                return;

            Process.Start(string.Format("{0}/biz/roles/edit.aspx?id={1}", ConnectionDetail.OriginalUrl, (Guid)lvRoles.SelectedItems[0].Tag));
        }

        private void DoWork()
        {
            var filterTerm = txtSearch.Text;
            IEnumerable<Entity> matchingPrivileges = null;

            if (filterTerm.Length != 0)
            {
                matchingPrivileges =
                    privileges.Where(
                        x => x["name"].ToString().ToLower().IndexOf(filterTerm.ToLower(), StringComparison.Ordinal) >= 0);
            }
            else
            {
                matchingPrivileges = privileges;
            }

            var lviList = new List<ListViewItem>();
            var lvgList = new List<ListViewGroup>();

            foreach (var privilege in matchingPrivileges)
            {
                var groupName = string.Empty;
                var group = (from entity in entities
                             where entity.Privileges.Any(p => p.PrivilegeId == privilege.Id)
                             select new { entity.LogicalName, entity.DisplayName.UserLocalizedLabel.Label }).FirstOrDefault();
                var entitySchemaName = (from entity in entities
                                        where entity.Privileges.Any(p => p.Name == privilege["name"].ToString())
                                        select entity.SchemaName).FirstOrDefault();

                if (group == null)
                {
                    if (privilege["name"].ToString().EndsWith("Entity"))
                        groupName = "Entity";
                    else if (privilege["name"].ToString().EndsWith("Attribute"))
                        groupName = "Attribute";
                    else if (privilege["name"].ToString().EndsWith("Relationship"))
                        groupName = "Relationship";
                    else if (privilege["name"].ToString().EndsWith("OptionSet"))
                        groupName = "OptionSet";
                    else
                        groupName = "_Common";
                }
                else
                {
                    if (group.LogicalName == "customeraddress")
                        groupName =
                            entities.First(x => x.LogicalName == "account").DisplayName.UserLocalizedLabel.Label;
                    else if (group.LogicalName == "email" || group.LogicalName == "task"
                        || group.LogicalName == "letter" || group.LogicalName == "phonecall"
                        || group.LogicalName == "appointment" || group.LogicalName == "serviceappointment"
                        || group.LogicalName == "campaignresponse" || group.LogicalName == "fax")
                        groupName =
                            entities.First(x => x.LogicalName == "activitypointer").DisplayName.UserLocalizedLabel.Label;
                    else
                        groupName = group.Label;
                }

                var lvGroup = lvgList.FirstOrDefault(f => f.Name == groupName);

                if (lvGroup == null)
                {
                    lvgList.Add(new ListViewGroup(groupName, groupName));
                }

                var item = new ListViewItem
                {
                    Text = privilege["name"].ToString().Remove(0, 3),
                    ImageIndex = 1,
                    Tag = privilege,
                    Group =
                        groupName != null
                            ? lvgList.First(f => f.Name == groupName)
                            : lvgList.First(f => f.Name == "_Common")
                };

                if (entitySchemaName != null)
                    item.Text = item.Text.Replace(entitySchemaName, "");

                lviList.Add(item);
            }

            ListViewDelegates.AddGroupsRange(lvPrivileges, lvgList.ToArray());
            ListViewDelegates.AddItemsRange(lvPrivileges, lviList.ToArray());

            ListViewDelegates.SortGroup(lvPrivileges, true);
            ListViewDelegates.Sort(lvPrivileges, true);

            CommonDelegates.SetEnableState(btnAdd, true);
            CommonDelegates.SetEnableState(btnRemove, true);
        }

        private void LvPrivilegesMouseDoubleClick(object sender, MouseEventArgs e)
        {
            BtnAddClick(null, null);
        }

        private void LvPrivilegesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPrivileges.SelectedItems.Count <= 0) return;

            bool canBeLocal = true;
            bool canBeBasic = true;
            bool canBeDeep = true;
            bool canBeGlobal = true;

            foreach (ListViewItem item in lvPrivileges.SelectedItems)
            {
                var privilege = item.Tag as Entity;

                if (privilege == null) continue;

                if ((bool)privilege["canbelocal"] == false)
                    canBeLocal = false;
                if ((bool)privilege["canbebasic"] == false)
                    canBeBasic = false;
                if ((bool)privilege["canbedeep"] == false)
                    canBeDeep = false;
                if ((bool)privilege["canbeglobal"] == false)
                    canBeGlobal = false;
            }

            rdbLevelUser.Enabled = canBeBasic;
            pbLevelUser.Enabled = canBeBasic;
            rdbLevelDiv.Enabled = canBeLocal;
            pbLevelDiv.Enabled = canBeLocal;
            rdbLevelSubDiv.Enabled = canBeDeep;
            pbLevelSubDiv.Enabled = canBeDeep;
            rdbLevelOrg.Enabled = canBeGlobal;
            pbLevelOrg.Enabled = canBeGlobal;

            if (!canBeBasic && rdbLevelUser.Checked
                || !canBeLocal && rdbLevelDiv.Checked
                || !canBeDeep && rdbLevelSubDiv.Checked
                || !canBeGlobal && rdbLevelOrg.Checked)
                rdbLevelAny.Checked = true;
        }

        private void LvSelectedPrivilegesMouseDoubleClick(object sender, MouseEventArgs e)
        {
            BtnRemoveClick(null, null);
        }

        private void TxtSearchTextChanged(object sender, EventArgs e)
        {
            if (fillPrivThread != null)
                fillPrivThread.Abort();

            //ListViewDelegates.ClearItems(lvPrivileges);
            //ListViewDelegates.ClearGroups(lvPrivileges);
            lvPrivileges.Items.Clear();
            lvPrivileges.Groups.Clear();

            btnAdd.Enabled = false;
            btnRemove.Enabled = false;

            fillPrivThread = new Thread(DoWork);
            fillPrivThread.Start();
        }
    }
}