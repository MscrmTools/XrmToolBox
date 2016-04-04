// PROJECT : MscrmTools.SyncFilterManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using MscrmTools.SyncFilterManager.AppCode;
using MscrmTools.SyncFilterManager.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MscrmTools.SyncFilterManager
{
    public partial class MainControl : PluginControlBase
    {
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

        private void TsbCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        #endregion Methods

        #region Users

        private void crmUserList1_OnRequestConnection(object sender, EventArgs e)
        {
            ExecuteMethod(SearchUsers);
        }

        private void ResetUsersFiltersToDefault()
        {
            crmUserList1.Service = Service;
            crmUserList1.ConnectionDetail = ConnectionDetail;

            var selectedUsers = crmUserList1.GetSelectedUsers();

            if (selectedUsers.Count == 0)
            {
                return;
            }

            if (DialogResult.Yes ==
                MessageBox.Show(this,
                "This action will replace all synchronization rules for the selected user(s) with the organization’s default local data rules. Any user-created rules will be removed.\r\n\r\n(Note: Although there can be multiple filters per entity listed in the Default Local Data Rules, CRM only allows one rule per entity to have the “Is Default” attribute set to “True” - Only Default Local Data Rules where the “Is Default” attribute is “True” will be copied to the selected user.)\r\n\r\nDo you want to continue?",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                tabCtrl.Enabled = false;

                WorkAsync("Reseting users local data rules...",
                   (bw, e) =>
                   {
                       var rm = new RuleManager("userquery", Service, ConnectionDetail);
                       rm.ResetUsersRulesFromDefault((List<Entity>)e.Argument, bw);
                   },
                   e =>
                   {
                       if (e.Error != null)
                       {
                           MessageBox.Show(this, "Error while updating users Local Data Rules: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       }

                       tabCtrl.Enabled = true;
                   },
                   e => SetWorkingMessage(e.UserState.ToString()),
                   selectedUsers);
            }
        }

        private void SearchUsers()
        {
            crmUserList1.Service = Service;
            crmUserList1.ConnectionDetail = ConnectionDetail;
            crmUserList1.Search();
        }

        private void tsbResetUsersFiltersToDefault_Click(object sender, EventArgs e)
        {
            ExecuteMethod(ResetUsersFiltersToDefault);
        }

        #endregion Users

        #region Users Rules

        private void forSpecificUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadUserLocalDataRulesForUsers);
        }

        private void LoadUserLocalDataRules()
        {
            if (DialogResult.OK ==
                MessageBox.Show(this,
                    "Depending on the number of users in your organization, it can take time to retrieve all users local data rules",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information))
            {
                usersLocalDataRulesView.Service = Service;
                usersLocalDataRulesView.DisplayOfflineFilter = chkDisplayOfflineFilters.Checked;
                usersLocalDataRulesView.DisplayOutlookFilter = chkDisplayOutlookFilters.Checked;
                usersLocalDataRulesView.LoadSystemViews();
            }
        }

        private void LoadUserLocalDataRulesForUsers()
        {
            var usDialog = new UserSelectionDialog(Service);
            if (usDialog.ShowDialog(this) == DialogResult.OK)
            {
                usersLocalDataRulesView.Service = Service;
                usersLocalDataRulesView.DisplayOfflineFilter = chkDisplayOfflineFilters.Checked;
                usersLocalDataRulesView.DisplayOutlookFilter = chkDisplayOutlookFilters.Checked;
                usersLocalDataRulesView.LoadSystemViews(usDialog.SelectedUsers);
            }
        }

        private void tsbDeleteUserRule_Click(object sender, EventArgs e)
        {
            if (usersLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            if (MessageBox.Show(this, "Are you sure you want to delete this synchonization rule?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                usersLocalDataRulesView.DeleteSelectedRules();
        }

        private void tsbLoadUsersLocalDataRules_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadUserLocalDataRules);
        }

        private void tsbUserRuleDisable_Click(object sender, EventArgs e)
        {
            if (usersLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            usersLocalDataRulesView.DisableSelectedRules();
        }

        private void tsbUserRuleEnable_Click(object sender, EventArgs e)
        {
            if (usersLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            usersLocalDataRulesView.EnableSelectedRules();
        }

        private void tsmiGroupByReturnedType_Click(object sender, EventArgs e)
        {
            usersLocalDataRulesView.GroupBy("returnedtypecode");
        }

        private void tsmiGroupByRule_Click(object sender, EventArgs e)
        {
            usersLocalDataRulesView.GroupBy("name");
        }

        private void tsmiGroupByUser_Click(object sender, EventArgs e)
        {
            usersLocalDataRulesView.GroupBy("ownerid");
        }

        #endregion Users Rules

        #region System Rules

        private void LoadSystemRules()
        {
            systemRulesListView.Service = Service;
            systemRulesListView.LoadSystemViews();
        }

        private void tsbLoadSystemSynchronizationFilter_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadSystemRules);
        }

        #endregion System Rules

        #region Default Rules

        private void LoadDefaultLocalDataRules()
        {
            defaultLocalDataRulesView.Service = Service;
            defaultLocalDataRulesView.LoadSystemViews();

            chkDisplayOfflineFilters.Enabled = true;
            chkDisplayOutlookFilters.Enabled = true;
        }

        private void tsbApplyToUsers_Click(object sender, EventArgs e)
        {
            if (defaultLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            defaultLocalDataRulesView.ApplySelectedFiltersToUsers();
        }

        private void tsbDefaultDelete_Click(object sender, EventArgs e)
        {
            if (defaultLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            if (MessageBox.Show(this, "Are you sure you want to delete this synchonization rule?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                defaultLocalDataRulesView.DeleteSelectedRules();
        }

        private void tsbLoadDefaultLocalDataRules_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadDefaultLocalDataRules);
        }

        #endregion Default Rules

        #region System Views

        private void LoadSystemViews()
        {
            crmSystemViewsList.Service = Service;
            crmSystemViewsList.ConnectionDetail = ConnectionDetail;
            crmSystemViewsList.LoadSystemViews();
        }

        private void tsbLoadSystemViews_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadSystemViews);
        }

        private void tsmiCreateFilterTemplateFromView_Click(object sender, EventArgs e)
        {
            crmSystemViewsList.CreateFilterFromView(false);
        }

        private void tsmiCreateSystemFilterFromView_Click(object sender, EventArgs e)
        {
            crmSystemViewsList.CreateFilterFromView(true);
        }

        private void tsmiUpdateFilterTemplate_Click(object sender, EventArgs e)
        {
            crmSystemViewsList.UpdateFilterFromView(false);
        }

        private void tsmiUpdateSystemFilter_Click(object sender, EventArgs e)
        {
            crmSystemViewsList.UpdateFilterFromView(true);
        }

        #endregion System Views

        private void chkDisplayOfflineFilters_CheckedChanged(object sender, EventArgs e)
        {
            usersLocalDataRulesView.DisplayViews(chkDisplayOutlookFilters.Checked, chkDisplayOfflineFilters.Checked);
        }

        private void chkDisplayOutlookFilters_CheckedChanged(object sender, EventArgs e)
        {
            usersLocalDataRulesView.DisplayViews(chkDisplayOutlookFilters.Checked, chkDisplayOfflineFilters.Checked);
        }

        private void ShowFetchXml(Entity entity)
        {
            if (entity != null)
            {
                var fxDialog = new XmlContentDisplayDialog(entity.GetAttributeValue<string>("fetchxml"));
                fxDialog.ShowDialog(this);
            }
        }

        private void tsbCopyToExistingDefaultRule_Click(object sender, EventArgs e)
        {
            if (crmSystemViewsList.GetSelectedSystemView().Count == 0) return;
            crmSystemViewsList.UpdateFilterFromView(false);
        }

        private void tsbCopyToNewDefautRule_Click(object sender, EventArgs e)
        {
            if (crmSystemViewsList.GetSelectedSystemView().Count == 0) return;
            crmSystemViewsList.CreateFilterFromView(false);
        }

        private void tsbCopyUserFiltersToUser_Click(object sender, EventArgs e)
        {
            crmUserList1.ReplaceUserFilters();
        }

        private void tsbDefineAsDefault_Click(object sender, EventArgs e)
        {
            if (defaultLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            defaultLocalDataRulesView.DefineAsDefault();
        }

        private void tsbShowFetchXmlDefault_Click(object sender, EventArgs e)
        {
            if (defaultLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            ShowFetchXml(defaultLocalDataRulesView.GetSelectedSystemView().FirstOrDefault());
        }

        private void tsbShowFetchXmlSystemRules_Click(object sender, EventArgs e)
        {
            ShowFetchXml(systemRulesListView.GetSelectedSystemView().FirstOrDefault());
        }

        private void tsbShowFetchXmlUser_Click(object sender, EventArgs e)
        {
            if (usersLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            ShowFetchXml(usersLocalDataRulesView.GetSelectedSystemView().FirstOrDefault());
        }

        private void tsbShowFetchXmlView_Click(object sender, EventArgs e)
        {
            ShowFetchXml(crmSystemViewsList.GetSelectedSystemView().FirstOrDefault());
        }

        private void tsbSystemFilterProperties_Click(object sender, EventArgs e)
        {
            systemRulesListView.RenameView();
        }

        private void tsbSystemRuleDelete_Click(object sender, EventArgs e)
        {
            systemRulesListView.DeleteSelectedRules();
        }

        private void tsbTemplateFilterProperties_Click(object sender, EventArgs e)
        {
            defaultLocalDataRulesView.RenameView();
        }
    }
}