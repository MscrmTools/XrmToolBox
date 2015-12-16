using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.SyncFilterManager.AppCode;
using MscrmTools.SyncFilterManager.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MscrmTools.SyncFilterManager.Controls
{
    public partial class CrmSystemViewList : UserControl
    {
        #region Variables

        private ConnectionDetail connectionDetail;
        private string currentAttributeGroup;
        private string entityName;
        private List<ListViewGroup> groupsCache;
        private List<ListViewItem> itemsCache;
        private Panel loadingPanel;
        private IOrganizationService service;

        #endregion Variables

        #region Constructors

        public CrmSystemViewList()
        {
            InitializeComponent();
        }

        public CrmSystemViewList(IOrganizationService service, string entityName, ConnectionDetail connectionDetail)
        {
            this.service = service;
            this.entityName = entityName;
            this.connectionDetail = connectionDetail;

            InitializeComponent();
        }

        #endregion Constructors

        #region Properties

        public ConnectionDetail ConnectionDetail { set { connectionDetail = value; } }
        public bool DisplayOfflineFilter { get; set; }
        public bool DisplayOutlookFilter { get; set; }

        [Description("Display Rules Template"), Category("CRM")]
        public bool DisplayRulesTemplate { get; set; }

        [Description("Display System Rules"), Category("CRM")]
        public bool DisplaySystemRules { get; set; }

        [Description("Display System View"), Category("CRM")]
        public bool DisplaySystemView { get; set; }

        [Description("Entity to display"), Category("CRM")]
        public string EntityName { set { entityName = value; } get { return entityName; } }

        public IOrganizationService Service { set { service = value; } }

        #endregion Properties

        #region Load System views

        public void LoadSystemViews(List<Entity> users = null, string returnedTypeExpected = null)
        {
            if (!DisplaySystemView && !DisplayRulesTemplate && !DisplaySystemRules && !lvViews.Columns.ContainsKey("User"))
            {
                lvViews.Columns.Add(new ColumnHeader
                {
                    Text = "User",
                    Name = "User",
                    Width = 150
                });

                lvViews.Columns.Add(new ColumnHeader
                {
                    Text = "State",
                    Name = "State",
                    Width = 75
                });
            }

            if (DisplayRulesTemplate && !lvViews.Columns.ContainsKey("IsDefault"))
            {
                lvViews.Columns.Add(new ColumnHeader
                {
                    Text = "Is Default",
                    Name = "IsDefault",
                    Width = 75
                });
            }

            if (DisplaySystemView || DisplayRulesTemplate)
            {
                lvViews.ShowGroups = true;
            }

            lvViews.Items.Clear();

            loadingPanel = InformationPanel.GetInformationPanel(this, "Retrieving items...", 340, 120);

            var bw = new BackgroundWorker { WorkerReportsProgress = true };
            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync(new object[] { users, returnedTypeExpected });
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var arguments = (object[])e.Argument;
            var rm = new RuleManager(entityName, service, connectionDetail);

            if (DisplaySystemView)
            {
                var query = new QueryExpression("savedquery")
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression("querytype", ConditionOperator.Equal, 0),
                            new ConditionExpression("fetchxml", ConditionOperator.NotNull)
                        }
                    }
                };

                if (arguments.Length > 1 && arguments[1] != null)
                    query.Criteria.AddCondition("returnedtypecode", ConditionOperator.Equal, arguments[1].ToString());

                e.Result = service.RetrieveMultiple(query);
            }
            else
            {
                string expectedReturnTypeCode = arguments.Length > 1 && arguments[1] != null ? arguments[1].ToString() : string.Empty;

                if (DisplaySystemRules)
                {
                    e.Result = rm.GetRules(new[] { 16, 256 }, expectedReturnedType: expectedReturnTypeCode, worker: (BackgroundWorker)sender);
                }
                else if (DisplayRulesTemplate)
                {
                    e.Result = rm.GetRules(new[] { 131072, 8192 }, expectedReturnedType: expectedReturnTypeCode, worker: (BackgroundWorker)sender);
                }
                else
                {
                    e.Result = rm.GetRules(new[] { 16, 256 }, (List<Entity>)arguments[0], worker: (BackgroundWorker)sender);
                }
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(loadingPanel, e.UserState.ToString());
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while searching items: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var views = new List<ListViewItem>();
                var viewsForCache = new List<ListViewItem>();
                var groups = new List<ListViewGroup>();

                foreach (var view in ((EntityCollection)e.Result).Entities)
                {
                    var lvi = new ListViewItem(view.GetAttributeValue<string>("name")) { Tag = view };
                    lvi.SubItems.Add(GetNameByQueryType(view.GetAttributeValue<int>("querytype")));
                    lvi.SubItems.Add(view.GetAttributeValue<string>("returnedtypecode"));
                    lvi.SubItems.Add(view.GetAttributeValue<string>("description"));

                    if (!DisplayRulesTemplate && !DisplaySystemView && !DisplaySystemRules)
                    {
                        currentAttributeGroup = "ownerid";

                        var ownerName = view.GetAttributeValue<EntityReference>("ownerid").Name;

                        lvi.SubItems.Add(ownerName);
                        lvi.SubItems.Add(view.GetAttributeValue<OptionSetValue>("statecode").Value == 0
                            ? "Active"
                            : "Inactive");

                        var group = groups.FirstOrDefault(g => g.Name == ownerName);
                        if (group == null)
                        {
                            group = new ListViewGroup(ownerName, ownerName);
                            groups.Add(group);
                        }

                        lvi.Group = group;

                        if (view.GetAttributeValue<int>("querytype") == 16 && DisplayOfflineFilter
                            || view.GetAttributeValue<int>("querytype") == 256 && DisplayOutlookFilter)
                        {
                            views.Add(lvi);
                        }
                    }

                    if (DisplayRulesTemplate)
                    {
                        lvi.SubItems.Add(view.GetAttributeValue<bool>("isdefault").ToString());
                    }

                    if (DisplaySystemView || DisplayRulesTemplate || DisplaySystemRules)
                    {
                        currentAttributeGroup = "returnedtypecode";

                        var name = DisplaySystemView
                            ? view.GetAttributeValue<string>("returnedtypecode")
                            : GetNameByQueryType(view.GetAttributeValue<int>("querytype"));
                        var group = groups.FirstOrDefault(g => g.Name == name);
                        if (group == null)
                        {
                            group = new ListViewGroup(name, name);
                            groups.Add(group);
                        }

                        lvi.Group = group;
                        views.Add(lvi);
                    }

                    viewsForCache.Add(lvi);
                }

                groups.Sort(new ListViewGroupHeaderSorter(DisplaySystemView || !DisplayRulesTemplate && !DisplaySystemView && !DisplaySystemRules));

                if (!DisplayRulesTemplate && !DisplaySystemView && !DisplaySystemRules)
                {
                    itemsCache = new List<ListViewItem>();
                    itemsCache.AddRange(viewsForCache);

                    groupsCache = new List<ListViewGroup>();
                    groupsCache.AddRange(groups);
                    groupsCache.Sort(new ListViewGroupHeaderSorter(DisplaySystemView || !DisplayRulesTemplate && !DisplaySystemView && !DisplaySystemRules));
                }

                lvViews.Groups.AddRange(groups.ToArray());
                lvViews.Items.AddRange(views.ToArray());

                lvViews.Sorting = SortOrder.Ascending;
                lvViews.ListViewItemSorter = new ListViewItemComparer(2, lvViews.Sorting);
            }
        }

        #endregion Load System views

        #region Enable Selected Rules

        public void EnableSelectedRules()
        {
            loadingPanel = InformationPanel.GetInformationPanel(this, "Activating selected records...", 340, 120);

            var bwEnable = new BackgroundWorker();
            bwEnable.DoWork += bwEnable_DoWork;
            bwEnable.RunWorkerCompleted += bwEnable_RunWorkerCompleted;
            bwEnable.RunWorkerAsync(lvViews.SelectedItems);
        }

        private void bwEnable_DoWork(object sender, DoWorkEventArgs e)
        {
            var rm = new RuleManager(entityName, service, connectionDetail);

            var activatedItems = new List<ListViewItem>();

            foreach (ListViewItem item in (ListView.SelectedListViewItemCollection)e.Argument)
            {
                var rule = (Entity)item.Tag;

                rm.EnableRule(rule.Id);

                activatedItems.Add(item);
            }

            e.Result = activatedItems;
        }

        private void bwEnable_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while activating selected records: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (ListViewItem item in (List<ListViewItem>)e.Result)
            {
                item.SubItems[item.SubItems.Count - 1].Text = "Active";
            }
        }

        #endregion Enable Selected Rules

        #region Disable Selected Rules

        public void DisableSelectedRules()
        {
            loadingPanel = InformationPanel.GetInformationPanel(this, "Deactivating selected records...", 340, 120);

            var bwDisable = new BackgroundWorker();
            bwDisable.DoWork += bwDisable_DoWork;
            bwDisable.RunWorkerCompleted += bwDisable_RunWorkerCompleted;
            bwDisable.RunWorkerAsync(lvViews.SelectedItems);
        }

        private void bwDisable_DoWork(object sender, DoWorkEventArgs e)
        {
            var rm = new RuleManager(entityName, service, connectionDetail);

            var deactivatedItems = new List<ListViewItem>();

            foreach (ListViewItem item in (ListView.SelectedListViewItemCollection)e.Argument)
            {
                var rule = (Entity)item.Tag;

                rm.DisableRule(rule.Id);

                deactivatedItems.Add(item);
            }

            e.Result = deactivatedItems;
        }

        private void bwDisable_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while activating selected records: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (ListViewItem item in (List<ListViewItem>)e.Result)
            {
                item.SubItems[item.SubItems.Count - 1].Text = "Inactive";
            }
        }

        #endregion Disable Selected Rules

        #region Delete Selected Rules

        public void DeleteSelectedRules()
        {
            loadingPanel = InformationPanel.GetInformationPanel(this, "Deleting selected records...", 340, 120);

            var bwDelete = new BackgroundWorker();
            bwDelete.DoWork += bwDelete_DoWork;
            bwDelete.RunWorkerCompleted += bwDelete_RunWorkerCompleted;
            bwDelete.RunWorkerAsync(lvViews.SelectedItems);
        }

        private void bwDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            var deletedItems = new List<ListViewItem>();

            var rm = new RuleManager(entityName, service, connectionDetail);

            foreach (ListViewItem item in (ListView.SelectedListViewItemCollection)e.Argument)
            {
                var rule = (Entity)item.Tag;

                rm.DeleteRule(rule.Id);

                deletedItems.Add(item);
            }

            e.Result = deletedItems;
        }

        private void bwDelete_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while deleting selected records: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (ListViewItem item in (List<ListViewItem>)e.Result)
            {
                lvViews.Items.Remove(item);
            }
        }

        #endregion Delete Selected Rules

        #region Apply Selected Filters to Users

        internal void ApplySelectedFiltersToUsers(bool applyToActiveUsers = false)
        {
            if (lvViews.SelectedItems.Count == 0)
            {
                return;
            }

            var templates = new EntityReferenceCollection();
            foreach (ListViewItem item in lvViews.SelectedItems)
            {
                templates.Add(new EntityReference(entityName, ((Entity)item.Tag).Id));
            }

            List<Entity> users = null;

            if (!applyToActiveUsers)
            {
                var usDialog = new UserSelectionDialog(service);
                if (usDialog.ShowDialog(this) == DialogResult.OK)
                {
                    users = usDialog.SelectedUsers;
                }
                else
                {
                    return;
                }
            }

            if (users == null || users.Count > 0)
            {
                loadingPanel = InformationPanel.GetInformationPanel(this, "Processing...", 340, 120);

                var bwApplyFiltersToUsers = new BackgroundWorker { WorkerReportsProgress = true };
                bwApplyFiltersToUsers.DoWork += bwApplyFiltersToUsers_DoWork;
                bwApplyFiltersToUsers.ProgressChanged += bwApplyFiltersToUsers_ProgressChanged;
                bwApplyFiltersToUsers.RunWorkerCompleted += bwApplyFiltersToUsers_RunWorkerCompleted;
                bwApplyFiltersToUsers.RunWorkerAsync(new object[] { templates, users });
            }
        }

        private void bwApplyFiltersToUsers_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw = (BackgroundWorker)sender;
            var rm = new RuleManager("savedquery", service, connectionDetail);
            var templates = ((EntityReferenceCollection)((object[])e.Argument)[0]);
            var users = (List<Entity>)((object[])e.Argument)[1];
            if (users == null)
            {
                rm.ApplyRuleToActiveUsers(templates);
            }
            else
            {
                foreach (var user in users)
                {
                    bw.ReportProgress(0,
                        string.Format("Applying filter(s) for user {0}...", user.GetAttributeValue<string>("fullname")));

                    rm.ApplyRulesToUser(templates, user.Id);
                }
            }
        }

        private void bwApplyFiltersToUsers_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(loadingPanel, e.UserState.ToString());
        }

        private void bwApplyFiltersToUsers_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while applying filters to users: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Apply Selected Filters to Users

        #region Create Filter From view

        internal void CreateFilterFromView(bool isSystem)
        {
            if (lvViews.SelectedItems.Count == 0)
            {
                return;
            }

            bool disableOutlookTemplate = false;
            var l = (from ListViewItem item in lvViews.SelectedItems select ((Entity)item.Tag).GetAttributeValue<string>("returnedtypecode")).ToList();
            if (
                l.All(
                    s => s != "serviceappointment"
                        && s != "phonecall"
                        && s != "contact"
                        && s != "letter"
                        && s != "appointment"
                        && s != "recurringappointmentmaster"
                        && s != "task"
                        && s != "fax"))
            {
                disableOutlookTemplate = true;
            }
            var ttsDialog = new TemplateTypeSelection(disableOutlookTemplate, isSystem);
            if (ttsDialog.ShowDialog(this) == DialogResult.OK)
            {
                loadingPanel = InformationPanel.GetInformationPanel(this, "Processing...", 340, 120);

                var bwCreateFilterFromView = new BackgroundWorker { WorkerReportsProgress = true };
                bwCreateFilterFromView.DoWork += bwCreateFilterFromView_DoWork;
                bwCreateFilterFromView.ProgressChanged += bwCreateFilterFromView_ProgressChanged;
                bwCreateFilterFromView.RunWorkerCompleted += bwCreateFilterFromView_RunWorkerCompleted;
                bwCreateFilterFromView.RunWorkerAsync(new object[] { lvViews.SelectedItems, ttsDialog.TemplateType, isSystem });
            }
        }

        private void bwCreateFilterFromView_DoWork(object sender, DoWorkEventArgs e)
        {
            var argument = (object[])e.Argument;
            var worker = (BackgroundWorker)sender;
            var items = (ListView.SelectedListViewItemCollection)argument[0];
            var rm = new RuleManager("savedquery", service, connectionDetail);

            var views = (from ListViewItem item in items select (Entity)item.Tag).ToList();

            var rulesIds = rm.CreateRuleFromSystemView(views, (int)argument[1]);

            e.Result = 1;

            if (!(bool)argument[2])
            {
                ApplyTemplateToUsers(rulesIds, "Do you want to apply this new template to some users?", rm, worker);
            }
        }

        private void bwCreateFilterFromView_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(loadingPanel, e.UserState.ToString());
        }

        private void bwCreateFilterFromView_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                string message = "Error: ";
                switch ((int)e.Result)
                {
                    case 0:
                        message = "Error while creating new default rules: ";
                        break;

                    case 1:
                        message = "Error while applying new default rules to users: ";
                        break;
                }

                MessageBox.Show(this, message + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Create Filter From view

        #region Update Filter From View

        internal void UpdateFilterFromView(bool isSystem)
        {
            if (lvViews.SelectedItems.Count == 0)
            {
                return;
            }

            var view = GetSelectedSystemView().FirstOrDefault();

            if (view == null)
            {
                MessageBox.Show(this, "Please select a view", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var rsDialog = new RuleSelectionDialog(service, view.GetAttributeValue<string>("returnedtypecode"), isSystem);
            if (rsDialog.ShowDialog(this) == DialogResult.OK)
            {
                var rule = rsDialog.SelectedRule;

                loadingPanel = InformationPanel.GetInformationPanel(this, "Updating filter...", 340, 120);

                var bwUpdateRule = new BackgroundWorker { WorkerReportsProgress = true };
                bwUpdateRule.DoWork += bwUpdateRule_DoWork;
                bwUpdateRule.ProgressChanged += bwUpdateRule_ProgressChanged;
                bwUpdateRule.RunWorkerCompleted += bwUpdateRule_RunWorkerCompleted;
                bwUpdateRule.RunWorkerAsync(new object[] { rule, view, isSystem });
            }
        }

        private void bwUpdateRule_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = 0;

            var rule = (Entity)((object[])e.Argument)[0];
            var view = (Entity)((object[])e.Argument)[1];
            var isSystem = (bool)((object[])e.Argument)[2];

            var rm = new RuleManager("savedquery", service, connectionDetail);
            rm.UpdateRuleFromSystemView(view, rule, (BackgroundWorker)sender);

            e.Result = 1;

            if (!isSystem)
            {
                ApplyTemplateToUsers(
                    new List<Guid> { rule.Id },
                    "Do you want to apply this new template to some users?",
                    rm,
                    (BackgroundWorker)sender);
            }
        }

        private void bwUpdateRule_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(loadingPanel, e.UserState.ToString());
        }

        private void bwUpdateRule_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                string message = "Error: ";
                switch ((int)e.Result)
                {
                    case 0:
                        message = "Error while updating default rule: ";
                        break;

                    case 1:
                        message = "Error while applying default rule(s) to user(s): ";
                        break;
                }

                MessageBox.Show(this, message + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Update Filter From View

        #region Rename view

        public void RenameView()
        {
            if (lvViews.SelectedItems.Count == 1)
            {
                var item = lvViews.SelectedItems[0];
                var entity = (Entity)item.Tag;
                var rDialog = new ViewPropertiesDialog
                {
                    ViewName = entity.GetAttributeValue<string>("name"),
                    ViewDescription = entity.GetAttributeValue<string>("description")
                };
                if (rDialog.ShowDialog(ParentForm) == DialogResult.OK)
                {
                    entity["name"] = rDialog.ViewName;
                    entity["description"] = rDialog.ViewDescription;

                    loadingPanel = InformationPanel.GetInformationPanel(this, "Updating properties...", 340, 120);

                    var bwUpdateView = new BackgroundWorker { WorkerReportsProgress = true };
                    bwUpdateView.DoWork += bwUpdateView_DoWork;
                    bwUpdateView.ProgressChanged += bwUpdateView_ProgressChanged;
                    bwUpdateView.RunWorkerCompleted += bwUpdateView_RunWorkerCompleted;
                    bwUpdateView.RunWorkerAsync(entity);
                }
            }
        }

        private void bwUpdateView_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw = (BackgroundWorker)sender;

            service.Update((Entity)e.Argument);

            bw.ReportProgress(0, "Publishing...");

            var request = new PublishXmlRequest { ParameterXml = String.Format("<importexportxml><entities><entity>{0}</entity></entities></importexportxml>", ((Entity)e.Argument).GetAttributeValue<string>("returnedtypecode")) };
            service.Execute(request);

            e.Result = e.Argument;
        }

        private void bwUpdateView_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(loadingPanel, e.UserState.ToString());
        }

        private void bwUpdateView_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            var item = lvViews.SelectedItems[0];

            item.Text = ((Entity)e.Result).GetAttributeValue<string>("name");
            item.SubItems[3].Text = ((Entity)e.Result).GetAttributeValue<string>("description");

            if (DisplayRulesTemplate)
            {
                if (DialogResult.Yes == MessageBox.Show(
                    ParentForm,
                    "Do you want to apply this change to active users?",
                    "Question",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question))
                {
                    ApplySelectedFiltersToUsers(true);
                }
            }
        }

        #endregion Rename view

        public List<Entity> GetSelectedSystemView()
        {
            return (from ListViewItem lvi in lvViews.SelectedItems select (Entity)lvi.Tag).ToList();
        }

        internal void DefineAsDefault()
        {
            var currentItem = GetSelectedSystemView().FirstOrDefault();

            if (currentItem != null)
            {
                var associatedItems = (from ListViewItem lvi in lvViews.Items select (Entity)lvi.Tag).ToList().Where(e => e.GetAttributeValue<string>("returnedtypecode") == currentItem.GetAttributeValue<string>("returnedtypecode"));

                if (!currentItem.GetAttributeValue<bool>("isdefault"))
                {
                    loadingPanel = InformationPanel.GetInformationPanel(this, "Removing default status to previous default record", 340, 120);

                    var bwApplyDefault = new BackgroundWorker { WorkerReportsProgress = true };
                    bwApplyDefault.DoWork += bwApplyDefault_DoWork;
                    bwApplyDefault.ProgressChanged += bwApplyDefault_ProgressChanged;
                    bwApplyDefault.RunWorkerCompleted += bwApplyDefault_RunWorkerCompleted;
                    bwApplyDefault.RunWorkerAsync(new object[] { currentItem, associatedItems });
                }
            }
        }

        internal void DisplayViews(bool displayOutlookFilters, bool displayOfflineFilters)
        {
            if (!DisplayRulesTemplate && !DisplaySystemView && itemsCache != null)
            {
                DisplayOutlookFilter = displayOutlookFilters;
                DisplayOfflineFilter = displayOfflineFilters;

                lvViews.Items.Clear();

                lvViews.Items.AddRange(itemsCache.Where(x =>
                                    ((Entity)x.Tag).GetAttributeValue<int>("querytype") == 16 && displayOfflineFilters
                                    || ((Entity)x.Tag).GetAttributeValue<int>("querytype") == 256 && displayOutlookFilters
                                    ).ToArray());

                GroupBy(currentAttributeGroup);
            }
        }

        internal void GroupBy(string attribute)
        {
            currentAttributeGroup = attribute;
            var newGroups = new List<ListViewGroup>();
            var items = new List<ListViewItem>();

            var currentItems = lvViews.Items;

            lvViews.Groups.Clear();

            foreach (ListViewItem item in currentItems)
            {
                lvViews.Items.Remove(item);

                var rule = (Entity)item.Tag;
                string groupCriteria;
                if (rule[attribute] is EntityReference)
                {
                    groupCriteria = rule.GetAttributeValue<EntityReference>(attribute).Name;
                }
                else
                {
                    groupCriteria = rule.GetAttributeValue<string>(attribute);
                }

                var newGroup = newGroups.FirstOrDefault(g => g.Name == groupCriteria);
                if (newGroup == null)
                {
                    newGroup = new ListViewGroup(groupCriteria, groupCriteria);
                    newGroups.Add(newGroup);
                }

                item.Group = newGroup;
                items.Add(item);
            }

            newGroups.Sort(new ListViewGroupHeaderSorter(DisplaySystemView || !DisplayRulesTemplate && !DisplaySystemView));

            lvViews.Groups.AddRange(newGroups.ToArray());
            lvViews.Items.AddRange(items.ToArray());
        }

        private void ApplyTemplateToUsers(List<Guid> rulesIds, string question, RuleManager rm, BackgroundWorker worker)
        {
            worker.ReportProgress(0, "Waiting for input...");

            if (
               MessageBox.Show(this, question, "Question",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var usDialog = new UserSelectionDialog(service);
                if (usDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (var user in usDialog.SelectedUsers)
                    {
                        worker.ReportProgress(0, string.Format("Applying rule(s) to user(s) '{0}'", user.GetAttributeValue<string>("fullname")));

                        var erc = new EntityReferenceCollection();
                        foreach (var ruleId in rulesIds)
                        {
                            erc.Add(new EntityReference("savedquery", ruleId));
                        }

                        rm.ApplyRulesToUser(erc, user.Id);
                    }
                }
            }
        }

        private void bwApplyDefault_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            var arguments = (object[])e.Argument;
            var currentItem = (Entity)arguments[0];
            var associatedItems = (IEnumerable<Entity>)arguments[1];

            if (associatedItems.Any())
            {
                foreach (var record in associatedItems.Where(r => r.Id != currentItem.Id && r.GetAttributeValue<bool>("isdefault")))
                {
                    var recordToUpdate = new Entity(record.LogicalName) { Id = record.Id };
                    recordToUpdate["isdefault"] = false;

                    service.Update(recordToUpdate);
                }
            }

            worker.ReportProgress(0, "Applying default status to selected record...");

            var currentRecordToUpdate = new Entity(currentItem.LogicalName) { Id = currentItem.Id };
            currentRecordToUpdate["isdefault"] = true;

            service.Update(currentRecordToUpdate);
        }

        private void bwApplyDefault_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(loadingPanel, e.UserState.ToString());
        }

        private void bwApplyDefault_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(loadingPanel);
            loadingPanel.Dispose();

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while defining default status: " + e.Error.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (lvViews.SelectedItems.Count == 1)
                {
                    ListViewItem currentItem = lvViews.SelectedItems[0];

                    foreach (ListViewItem item in lvViews.Items)
                    {
                        if (((Entity)item.Tag).GetAttributeValue<string>("returnedtypecode") == ((Entity)currentItem.Tag).GetAttributeValue<string>("returnedtypecode"))
                            item.SubItems[item.SubItems.Count - 1].Text = item == currentItem ? true.ToString() : false.ToString();
                        ((Entity)item.Tag)["isdefault"] = item == currentItem;
                    }
                }
            }
        }

        private string GetNameByQueryType(int queryType)
        {
            switch (queryType)
            {
                case 16:
                    return "Offline filter";

                case 256:
                    return "Outlook filter";

                case 8192:
                    return "Offline template";

                case 131072:
                    return "Outlook template";
            }

            return string.Empty;
        }

        private void lvViews_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvViews.Sorting = lvViews.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvViews.ListViewItemSorter = new ListViewItemComparer(e.Column, lvViews.Sorting);
        }
    }
}