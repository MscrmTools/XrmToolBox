﻿// PROJECT : MscrmTools.SyncFilterManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MscrmTools.SyncFilterManager.AppCode;
using MscrmTools.SyncFilterManager.Controls;
using MscrmTools.SyncFilterManager.Forms;
using XrmToolBox;
using XrmToolBox.Attributes;

// If empty strings are left, default values are used
[assembly: BackgroundColor("")]
[assembly: PrimaryFontColor("")]
[assembly: SecondaryFontColor("Gray")]
[assembly: SmallImageBase64("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")]
[assembly: BigImageBase64("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")]

namespace MscrmTools.SyncFilterManager
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

            crmUserList1.Service = service;
            crmSystemViewsList.Service = service;
            usersLocalDataRulesView.Service = service;
            defaultLocalDataRulesView.Service = service;
            systemRulesListView.Service = service;

            switch (actionName)
            {
                case "crmSystemViewsList.LoadSystemViews":
                    crmSystemViewsList.LoadSystemViews();
                    break;
                case "defaultLocalDataRulesView.LoadSystemViews":
                    defaultLocalDataRulesView.LoadSystemViews();
                    break;
                case "usersLocalDataRulesView.LoadSystemViews":
                    LoadUserLocalDataRules();
                    break;
                case "crmUserList1.Search":
                    ((CrmUserList) parameter).Search();
                    break;
                case "LoadUserLocalDataRulesForUsers":
                    LoadUserLocalDataRulesForUsers();
                    break;
                case "LoadSystemRules":
                    LoadSystemRules();
                    break;
            }
        }

        private void TsbCloseClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }

        #endregion Methods

        #region Users

        private void tsbResetUsersFiltersToDefault_Click(object sender, EventArgs e)
        {
            var selectedUsers = crmUserList1.GetSelectedUsers();

            if (selectedUsers.Count == 0)
            {
                return;
            }

            if (DialogResult.Yes ==
                MessageBox.Show(this,
                "This action will replace all synchronization rules for the selected user(s) with the organization’s default local data rules. Any user-created rules will be removed.\r\n\r\n(Note: Although there can be multiple filters per entity listed in the Default Local Data Rules, CRM only allows one rule per entity to have the “Is Default” attribute set to “True” - Only Default Local Data Rules where the “Is Default” attribute is “True” will be copied to the selected user.)\r\n\r\nDo you want to continue?",
                //                    "This action will replace existing rule(s) for selected user(s) with default rule(s) defined as default\r\n\r\nDo you want to continue?",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                tabCtrl.Enabled = false;

                infoPanel = InformationPanel.GetInformationPanel(this, "Reseting users local data rules...", 340, 120);

                var bwResetUsersFilter = new BackgroundWorker {WorkerReportsProgress = true};
                bwResetUsersFilter.DoWork += bwResetUsersFilter_DoWork;
                bwResetUsersFilter.ProgressChanged += bwResetUsersFilter_ProgressChanged;
                bwResetUsersFilter.RunWorkerCompleted += bwResetUsersFilter_RunWorkerCompleted;
                bwResetUsersFilter.RunWorkerAsync(selectedUsers);
            }
        }

        void bwResetUsersFilter_DoWork(object sender, DoWorkEventArgs e)
        {
            var rm = new RuleManager("userquery", service);
            rm.ResetUsersRulesFromDefault((List<Entity>)e.Argument, (BackgroundWorker)sender);
        }

        void bwResetUsersFilter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(infoPanel, e.UserState.ToString());
        }

        void bwResetUsersFilter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Controls.Remove(infoPanel);
            infoPanel.Dispose();

            if (e.Error != null)
            {
                MessageBox.Show(this, "Error while updating users Local Data Rules: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tabCtrl.Enabled = true;
        }

        private void crmUserList1_OnRequestConnection(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs
                {
                    ActionName = "crmUserList1.Search",
                    Control = this,
                    Parameter = sender
                };
                OnRequestConnection(this, args);
            }
            else
            {
                MessageBox.Show(this, "OnRequestConnection event not registered!", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion Users

        #region Users Rules

        private void tsbLoadUsersLocalDataRules_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                    {
                        ActionName = "usersLocalDataRulesView.LoadSystemViews",
                        Control = this
                    };
                    OnRequestConnection(this, args);
                }
                else
                {
                    MessageBox.Show(this, "OnRequestConnection event not registered!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                LoadUserLocalDataRules();
            }
        }

        private void LoadUserLocalDataRules()
        {
            if (DialogResult.OK ==
                MessageBox.Show(this,
                    "Depending on the number of users in your organization, it can take time to retrieve all users local data rules",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information))
            {
                usersLocalDataRulesView.DisplayOfflineFilter = chkDisplayOfflineFilters.Checked;
                usersLocalDataRulesView.DisplayOutlookFilter = chkDisplayOutlookFilters.Checked;

                usersLocalDataRulesView.LoadSystemViews();
            }
        }

        private void forSpecificUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                    {
                        ActionName = "LoadUserLocalDataRulesForUsers",
                        Control = this
                    };
                    OnRequestConnection(this, args);
                }
                else
                {
                    MessageBox.Show(this, "OnRequestConnection event not registered!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                LoadUserLocalDataRulesForUsers();
            }
        }

        private void LoadUserLocalDataRulesForUsers()
        {
            var usDialog = new UserSelectionDialog(service);
            if (usDialog.ShowDialog(this) == DialogResult.OK)
            {
                usersLocalDataRulesView.DisplayOfflineFilter = chkDisplayOfflineFilters.Checked;
                usersLocalDataRulesView.DisplayOutlookFilter = chkDisplayOutlookFilters.Checked;
                usersLocalDataRulesView.LoadSystemViews(usDialog.SelectedUsers);
            }
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

        private void tsbDeleteUserRule_Click(object sender, EventArgs e)
        {
            if (usersLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            if (MessageBox.Show(this, "Are you sure you want to delete this synchonization rule?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                usersLocalDataRulesView.DeleteSelectedRules();
        }

        private void tsmiGroupByRule_Click(object sender, EventArgs e)
        {
            usersLocalDataRulesView.GroupBy("name");
        }

        private void tsmiGroupByReturnedType_Click(object sender, EventArgs e)
        {
            usersLocalDataRulesView.GroupBy("returnedtypecode");
        }

        private void tsmiGroupByUser_Click(object sender, EventArgs e)
        {
            usersLocalDataRulesView.GroupBy("ownerid");
        }

        #endregion Users Rules

        #region System Rules

        private void tsbLoadSystemSynchronizationFilter_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                    {
                        ActionName = "LoadSystemRules",
                        Control = this
                    };
                    OnRequestConnection(this, args);
                }
                else
                {
                    MessageBox.Show(this, "OnRequestConnection event not registered!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                LoadSystemRules();
            }
        }

        private void LoadSystemRules()
        {
            systemRulesListView.LoadSystemViews();
        }

        #endregion

        #region Default Rules

        private void tsbLoadDefaultLocalDataRules_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                    {
                        ActionName = "defaultLocalDataRulesView.LoadSystemViews",
                        Control = this
                    };
                    OnRequestConnection(this, args);
                }
                else
                {
                    MessageBox.Show(this, "OnRequestConnection event not registered!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                defaultLocalDataRulesView.LoadSystemViews();

                chkDisplayOfflineFilters.Enabled = true;
                chkDisplayOutlookFilters.Enabled = true;
            }
        }

        private void tsbDefaultDelete_Click(object sender, EventArgs e)
        {
            if (defaultLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            if(MessageBox.Show(this,"Are you sure you want to delete this synchonization rule?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
            defaultLocalDataRulesView.DeleteSelectedRules();
        }

        private void tsbApplyToUsers_Click(object sender, EventArgs e)
        {
            if (defaultLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            defaultLocalDataRulesView.ApplySelectedFiltersToUsers();
        }

        #endregion Default Rules

        #region System Views

        private void tsbLoadSystemViews_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                    {
                        ActionName = "crmSystemViewsList.LoadSystemViews",
                        Control = this
                    };
                    OnRequestConnection(this, args);
                }
                else
                {
                    MessageBox.Show(this, "OnRequestConnection event not registered!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                crmSystemViewsList.LoadSystemViews();
            }
        }

        private void tsmiCreateSystemFilterFromView_Click(object sender, EventArgs e)
        {
            crmSystemViewsList.CreateFilterFromView(true);
        }

        private void tsmiCreateFilterTemplateFromView_Click(object sender, EventArgs e)
        {
            crmSystemViewsList.CreateFilterFromView(false);
        }

        private void tsmiUpdateSystemFilter_Click(object sender, EventArgs e)
        {
            crmSystemViewsList.UpdateFilterFromView(true);
        }

        private void tsmiUpdateFilterTemplate_Click(object sender, EventArgs e)
        {
            crmSystemViewsList.UpdateFilterFromView(false);
        }

        #endregion System Views

        private void tsbCopyToNewDefautRule_Click(object sender, EventArgs e)
        {
            if (crmSystemViewsList.GetSelectedSystemView().Count == 0) return;
            crmSystemViewsList.CreateFilterFromView(false);
        }

        private void tsbCopyToExistingDefaultRule_Click(object sender, EventArgs e)
        {
            if (crmSystemViewsList.GetSelectedSystemView().Count == 0) return;
            crmSystemViewsList.UpdateFilterFromView(false);
        }

        private void tsbShowFetchXmlDefault_Click(object sender, EventArgs e)
        {
            if (defaultLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            ShowFetchXml(defaultLocalDataRulesView.GetSelectedSystemView().FirstOrDefault());
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

        private void tsbShowFetchXmlSystemRules_Click(object sender, EventArgs e)
        {
            ShowFetchXml(systemRulesListView.GetSelectedSystemView().FirstOrDefault());
        }

        private void ShowFetchXml(Entity entity)
        {
            if (entity != null)
            {
                var fxDialog = new XmlContentDisplayDialog(entity.GetAttributeValue<string>("fetchxml"));
                fxDialog.ShowDialog(this);
            }
        }

        private void tsbDefineAsDefault_Click(object sender, EventArgs e)
        {
            if (defaultLocalDataRulesView.GetSelectedSystemView().Count == 0) return;
            defaultLocalDataRulesView.DefineAsDefault();
        }

        private void chkDisplayOutlookFilters_CheckedChanged(object sender, EventArgs e)
        {
            usersLocalDataRulesView.DisplayViews(chkDisplayOutlookFilters.Checked, chkDisplayOfflineFilters.Checked);
        }

        private void chkDisplayOfflineFilters_CheckedChanged(object sender, EventArgs e)
        {
            usersLocalDataRulesView.DisplayViews(chkDisplayOutlookFilters.Checked, chkDisplayOfflineFilters.Checked);
        }

        private void tsbSystemRuleDelete_Click(object sender, EventArgs e)
        {
            systemRulesListView.DeleteSelectedRules();
        }

       

        
    }
}
