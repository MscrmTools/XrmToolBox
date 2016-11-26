// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.RoleUpdater.DelegatesHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.RoleUpdater.Controls
{
    public partial class PrivilegeLevelSelectionControl : UserControl
    {
        #region Variables

        /// <summary>
        /// List of privilege updates
        /// </summary>
        private readonly List<PrivilegeAction> actions;

        /// <summary>
        /// List of entities
        /// </summary>
        private readonly List<EntityMetadata> entities;

        /// <summary>
        /// Role manager
        /// </summary>
        private readonly RoleManager rManager;

        private readonly UpdateSettings settings;

        private Thread fillPrivThread;

        private Panel infoPanel;

        public delegate void SettingsAppliedHandler(object sender, EventArgs e);

        public event SettingsAppliedHandler SettingsApplied;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class PrivilegeLevelSelectionControl
        /// </summary>
        public PrivilegeLevelSelectionControl(RoleManager roleManager, List<EntityMetadata> entities, UpdateSettings settings)
        {
            InitializeComponent();

            actions = new List<PrivilegeAction>();
            rManager = roleManager;
            this.entities = entities;
            this.settings = settings;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the list of privileges to update
        /// </summary>
        public List<PrivilegeAction> Actions
        {
            get { return actions; }
        }

        #endregion Properties

        #region Methods

        private void AddNoneLevelClick(object sender, EventArgs e)
        {
            foreach (var prv in from ListViewItem item in lvPrivileges.SelectedItems select (Entity)item.Tag)
            {
                AddPrivilegeLevel(PrivilegeLevel.None, "None", prv);
            }
        }

        private void AddPrivilegeLevel(PrivilegeLevel level, string privilegeLevelName, Entity privilege)
        {
            if (actions.Any(x => x.PrivilegeId == privilege.Id))
            {
                lblAlreadyAdded.Visible = true;
            }
            else
            {
                lblAlreadyAdded.Visible = false;

                actions.Add(new PrivilegeAction
                                 {
                                     Level = level,
                                     PrivilegeId = privilege.Id,
                                     PrivilegeName = privilege["name"].ToString()
                                 });

                var item = new ListViewItem(privilege["name"].ToString());
                item.SubItems.Add(privilegeLevelName);
                item.Tag = privilege;
                listView1.Items.Add(item);
            }
        }

        private void BtnAddBusinessUnitLevelClick(object sender, EventArgs e)
        {
            foreach (var prv in from ListViewItem item in lvPrivileges.SelectedItems select (Entity)item.Tag)
            {
                AddPrivilegeLevel(PrivilegeLevel.BusinessUnit, "Business Unit", prv);
            }
        }

        private void BtnAddOrganizationLevelClick(object sender, EventArgs e)
        {
            foreach (var prv in from ListViewItem item in lvPrivileges.SelectedItems select (Entity)item.Tag)
            {
                AddPrivilegeLevel(PrivilegeLevel.Organization, "Organization", prv);
            }
        }

        private void BtnAddParentChildLevelClick(object sender, EventArgs e)
        {
            foreach (var prv in from ListViewItem item in lvPrivileges.SelectedItems select (Entity)item.Tag)
            {
                AddPrivilegeLevel(PrivilegeLevel.ParentChildBusinessUnit, "Parent : Child Business Unit", prv);
            }
        }

        private void BtnAddUserLevelClick(object sender, EventArgs e)
        {
            foreach (var prv in from ListViewItem item in lvPrivileges.SelectedItems select (Entity)item.Tag)
            {
                AddPrivilegeLevel(PrivilegeLevel.User, "User", prv);
            }
        }

        private void BtnRemoveClick(object sender, EventArgs e)
        {
            var itemsToRemove = listView1.SelectedItems.Cast<ListViewItem>().ToList();
            var actionsToRemove = new List<PrivilegeAction>();

            foreach (ListViewItem item in itemsToRemove)
            {
                var prv = (Entity)item.Tag;

                foreach (var action in actions.Where(action => action.PrivilegeId == prv.Id))
                {
                    actionsToRemove.Add(action);
                    listView1.Items.Remove(item);
                }

                foreach (var pAction in actionsToRemove)
                {
                    actions.Remove(pAction);
                }
            }
        }

        #endregion Methods

        public void ApplyChanges()
        {
            settings.Actions = actions;

            if (settings.Actions.Count == 0)
            {
                MessageBox.Show(this, "Please select at least one privilege", "Warning", MessageBoxButtons.OK,
                                   MessageBoxIcon.Warning);

                return;
            }

            infoPanel = InformationPanel.GetInformationPanel(this, "Updating roles...", 500, 120);

            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.ProgressChanged += WorkerProgressChanged;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }

        private void DoWork()
        {
            var filterTerm = textBox1.Text;
            IEnumerable<Entity> privileges = rManager.Privileges;

            if (filterTerm.Length != 0)
            {
                privileges =
                    rManager.Privileges.Where(
                        x => x["name"].ToString().ToLower().IndexOf(filterTerm.ToLower(), StringComparison.Ordinal) >= 0);
            }

            var items = new List<ListViewItem>();

            foreach (var privilege in privileges)
            {
                string entitySchemaName = null;
                var groupName = string.Empty;
                var entitiesWithPrivilege = (from emd in entities
                             where emd.Privileges.Any(p => p.PrivilegeId == privilege.Id)
                             select emd).ToList();
                EntityMetadata entity;
                if (entitiesWithPrivilege.Count > 0 && entitiesWithPrivilege.Any(g => g.IsActivity.Value))
                {
                    entity = entitiesWithPrivilege.FirstOrDefault(g => g.LogicalName == "activitypointer");
                    entitySchemaName = "Activity";
                }
                else
                {
                    entity = entitiesWithPrivilege.FirstOrDefault();
                    if (entity != null)
                    {
                        entitySchemaName = entity.SchemaName;
                    }
                }

                if (entity == null)
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
                    
                    if (entity.LogicalName == "customeraddress")
                        groupName =
                            entities.First(x => x.LogicalName == "account").DisplayName.UserLocalizedLabel.Label;
                    else if(entity.IsActivity.Value || entity.LogicalName == "bulkoperation")
                    {
                        groupName =
                            entities.First(x => x.LogicalName == "activitypointer").DisplayName.UserLocalizedLabel.Label;
                    }
                    else
                        groupName = entity.DisplayName.UserLocalizedLabel.Label;
                }

                if (ListViewDelegates.GetGroup(lvPrivileges, groupName) == null)
                    ListViewDelegates.AddGroup(lvPrivileges, groupName);

                var item = new ListViewItem
                {
                    Text = privilege["name"].ToString().Remove(0, 3),
                    Tag = privilege,
                    Group =
                        groupName != null
                            ? ListViewDelegates.GetGroup(lvPrivileges, groupName)
                            : ListViewDelegates.GetGroup(lvPrivileges, "_Common")
                };

                if (entitySchemaName != null)
                    item.Text = item.Text.Replace(entitySchemaName, "");


                items.Add(item);
            }

            ListViewDelegates.AddRange(lvPrivileges, items);
            ListViewDelegates.SortGroup(lvPrivileges, true);
            ListViewDelegates.Sort(lvPrivileges, true);
        }

        private void ListView1MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
                listView1.Items.Remove(listView1.SelectedItems[0]);
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

            btnAddUserLevel.Enabled = canBeLocal;
            btnAddBusinessUnitLevel.Enabled = canBeBasic;
            btnAddParentChildLevel.Enabled = canBeDeep;
            btnAddOrganizationLevel.Enabled = canBeGlobal;
        }

        private void PrivilegeLevelSelectionControlLoad(object sender, EventArgs e)
        {
            lvPrivileges.Columns[lvPrivileges.Columns.Count - 1].Width = lvPrivileges.Width - 24;
            listView1.Columns[listView1.Columns.Count - 1].Width = -2;

            fillPrivThread = new Thread(DoWork);
            fillPrivThread.Start();

            if (settings != null && settings.Actions.Count > 0)
            {
                foreach (PrivilegeAction pAction in settings.Actions)
                {
                    var prv = new Entity("privilege") { Id = pAction.PrivilegeId };
                    prv["name"] = pAction.PrivilegeName;

                    AddPrivilegeLevel(pAction.Level, pAction.LevelName, prv);
                }
            }
        }

        private void TextBox1TextChanged(object sender, EventArgs e)
        {
            if (fillPrivThread != null)
                fillPrivThread.Abort();

            ListViewDelegates.ClearItems(lvPrivileges);
            ListViewDelegates.ClearGroups(lvPrivileges);

            fillPrivThread = new Thread(DoWork);
            fillPrivThread.Start();
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;

            foreach (var role in settings.SelectedRoles)
            {
                try
                {
                    worker.ReportProgress(1, string.Format("Retrieving privileges set for role \"{0}\"...", role["name"]));
                    var rolePrivileges = rManager.GetPrivilegesForRole(role.Id);

                    foreach (var pAction in settings.Actions)
                    {
                        if (pAction.Level == PrivilegeLevel.None)
                        {
                            worker.ReportProgress(1, string.Format("Removing privilege \"{0}\" from role \"{1}\"...", pAction.PrivilegeName, role["name"]));
                            rManager.RemovePrivilegeFromRole(rolePrivileges, pAction.PrivilegeId);
                        }
                        else
                        {
                            worker.ReportProgress(1, string.Format("Adding/Updating privilege \"{0}\" in role \"{1}\"...", pAction.PrivilegeName, role["name"]));
                            rManager.AddPrivilegeToRole(rolePrivileges, pAction);
                        }
                    }

                    worker.ReportProgress(1, string.Format("Replacing privileges set for role \"{0}\"...", role["name"]));
                    rManager.ApplyChanges(rolePrivileges, role.Id);
                }
                catch (Exception error)
                {
                    CommonDelegates.DisplayMessageBox(ParentForm,
                                                      "Error while updating role " + role["name"] + ": " + error.Message,
                                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(infoPanel, e.UserState.ToString());
        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            if (SettingsApplied != null)
            {
                SettingsApplied(this, null);
            }
        }
    }
}