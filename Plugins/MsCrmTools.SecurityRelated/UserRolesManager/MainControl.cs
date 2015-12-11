using Microsoft.Xrm.Sdk;
using MsCrmTools.UserRolesManager.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.UserRolesManager
{
    public partial class MainControl : PluginControlBase
    {
        private List<Entity> allRoles;
        private Guid currentUserId;

        public MainControl()
        {
            InitializeComponent();
        }

        private void LoadCrmItems()
        {
            principalSelector1.Service = Service;
            roleSelector1.Service = Service;

            principalSelector1.LoadViews();
            roleSelector1.LoadRoles();

            allRoles = roleSelector1.AllRoles;

            currentUserId = (new SystemUserManager(Service)).GetCurrentUserId();
        }

        private void TsbCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbLoadCrmItems_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadCrmItems);
        }

        #region Action 1 : Add roles to principals

        private void action1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ra = new RoleAction
            {
                ActionType = 1,
                Roles = roleSelector1.SelectedRoles,
                Principals = principalSelector1.SelectedItems
            };

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Adding roles to principal(s)...",
                AsyncArgument = ra,
                Work = (bw, evt) =>
                {
                    var action = (RoleAction)evt.Argument;

                    var rManager = new RoleManager(Service);
                    rManager.AddRolesToPrincipals(action.Roles, action.Principals, allRoles, bw);
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + evt.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                },
                ProgressChanged = evt => { SetWorkingMessage(string.Format(evt.UserState.ToString(), evt.ProgressPercentage)); }
            });
        }

        #endregion Action 1 : Add roles to principals

        #region Action 2 : Remove roles from principals

        private void action2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ra = new RoleAction
            {
                ActionType = 2,
                Roles = roleSelector1.SelectedRoles,
                Principals = principalSelector1.SelectedItems
            };

            if (ra.Principals.Any(p => p.Id == currentUserId))
            {
                MessageBox.Show(this,
                    "You can't remove roles from your own profile. Your profile will be removed from the principals list",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                ra.Principals.Remove(ra.Principals.First(r => r.Id == currentUserId));
            }

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Removing roles to principal(s)...",
                AsyncArgument = ra,
                Work = (bw, evt) =>
                {
                    var action = (RoleAction)evt.Argument;

                    var rManager = new RoleManager(Service);
                    rManager.RemoveRolesFromPrincipals(action.Roles, action.Principals, allRoles, bw);
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + evt.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                },
                ProgressChanged = evt => SetWorkingMessage(string.Format(evt.UserState.ToString(), evt.ProgressPercentage))
            });
        }

        #endregion Action 2 : Remove roles from principals

        #region Action 3 : Remove then Add roles to principals

        private void action3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ra = new RoleAction
            {
                ActionType = 3,
                Roles = roleSelector1.SelectedRoles,
                Principals = principalSelector1.SelectedItems
            };

            if (ra.Principals.Any(p => p.Id == currentUserId))
            {
                MessageBox.Show(this,
                    "You can't remove roles from your own profile. Your profile will be removed from the principals list",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                ra.Principals.Remove(ra.Principals.First(r => r.Id == currentUserId));
            }

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Removing roles from principal(s)...",
                AsyncArgument = ra,
                Work = (bw, evt) =>
                {
                    var action = (RoleAction)evt.Argument;

                    var rManager = new RoleManager(Service);
                    rManager.RemoveExistingRolesFromPrincipals(action.Principals);

                    bw.ReportProgress(0, "Adding roles to principals ({0} %)...");

                    rManager.AddRolesToPrincipals(action.Roles, action.Principals, allRoles, bw);
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + evt.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                },
                ProgressChanged = evt => { SetWorkingMessage(string.Format(evt.UserState.ToString(), evt.ProgressPercentage)); }
            });
        }

        #endregion Action 3 : Remove then Add roles to principals
    }
}