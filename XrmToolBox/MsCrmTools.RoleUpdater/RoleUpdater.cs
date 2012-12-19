// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.RoleUpdater.Controls;
using MsCrmTools.RoleUpdater.DelegatesHelpers;
using XrmToolBox;

namespace MsCrmTools.RoleUpdater
{
    public partial class RoleUpdater : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Crm organization service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Manager to process actions on roles
        /// </summary>
        RoleManager rManager;

        /// <summary>
        /// Wizard current step
        /// </summary>
        private int currentStep = 1;

        /// <summary>
        /// List of all entities metadata
        /// </summary>
        List<EntityMetadata> entities;

        /// <summary>
        /// Wizard settings
        /// </summary>
        private UpdateSettings settings = new UpdateSettings();

        /// <summary>
        /// Panel for displaying information
        /// </summary>
        private Panel infoPanel;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class RoleUpdater
        /// </summary>
        public RoleUpdater()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the organization service
        /// </summary>
        public IOrganizationService Service
        {
            get { return service; }
        }

        /// <summary>
        /// Gets the logo for this tool
        /// </summary>
        public Image PluginLogo
        {
            get { return imageList1.Images[0]; }
        }

        #endregion Properties

        #region Events

        public event EventHandler OnRequestConnection;
        public event EventHandler OnCloseTool;

        #endregion Events

        #region Methods

        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;
            rManager = new RoleManager(service);
            entities = MetadataHelper.GetEntitiesMetadata(service, EntityFilters.Privileges);

            if (actionName.ToLower() == "loadroles")
            {
                LoadRolesAndPrivileges();
                BtnResetClick(null, null);
            }
        }
        
        private void LoadRolesAndPrivileges()
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs
                                   {
                                       ActionName = "LoadRoles",
                                       Control = this,
                                       Parameter = null
                                   };

                    OnRequestConnection(this, args);
                }
            }
            else
            {
                CommonDelegates.SetCursor(this, Cursors.WaitCursor);

                infoPanel = InformationPanel.GetInformationPanel(this, "Loading roles...", 340, 100);

                var bwFillRolesAndPrivileges = new BackgroundWorker();
                bwFillRolesAndPrivileges.DoWork += BwFillRolesAndPrivilegesDoWork;
                bwFillRolesAndPrivileges.ProgressChanged += BwFillRolesAndPrivilegesRunWorkerProgressChanged;
                bwFillRolesAndPrivileges.RunWorkerCompleted += BwFillRolesAndPrivilegesRunWorkerCompleted;
                bwFillRolesAndPrivileges.WorkerReportsProgress = true;
                bwFillRolesAndPrivileges.RunWorkerAsync();
            }
        }

        private void BwFillRolesAndPrivilegesDoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;

            // Retrieve all roles without parent role
            rManager.LoadRootRoles();

            worker.ReportProgress(1, "Loading privileges...");

            // Retrieve all privileges
            rManager.LoadPrivileges();
        }

        private void BwFillRolesAndPrivilegesRunWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(infoPanel, e.UserState.ToString());
        }

        private void BwFillRolesAndPrivilegesRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            infoPanel.Dispose();
            Controls.Remove(infoPanel);

            if (e.Error != null)
            {
                var errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                MessageBox.Show(this, "An error occured: " + errorMessage, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                BtnNextClick(null, null);

                pnlSteps.Visible = true;
                btnPrevious.Visible = false;
                btnReset.Visible = false;
                btnNext.Visible = true;
            }

            CommonDelegates.SetCursor(this, Cursors.Default);
        }

        private void BtnConnectClick(object sender, EventArgs e)
        {
            LoadRolesAndPrivileges();
        }

        private void BtnNextClick(object sender, EventArgs e)
        {
            switch (currentStep)
            {
                case 1:
                    {
                        pnlSteps.Visible = true;
                        btnPrevious.Visible = true;
                        btnReset.Visible = true;
                        btnNext.Visible = true;

                        // Adds the role selection control in main form
                        var ctrl = new RoleSelectionControl(rManager.Roles, settings)
                                       {
                                           Width = Width,
                                           Height = Height - 70,
                                           Anchor =
                                               AnchorStyles.Bottom |
                                               AnchorStyles.Left |
                                               AnchorStyles.Right |
                                               AnchorStyles.Top
                                       };
                        pnlSteps.Controls.Add(ctrl);
                        currentStep = 2;
                    }
                    break;
                case 2:
                    {
                        settings.SelectedRoles = ((RoleSelectionControl)pnlSteps.Controls[0]).SelectedRoles;

                        if (settings.SelectedRoles == null)
                            return;

                        pnlSteps.Visible = true;
                        btnPrevious.Visible = true;
                        btnReset.Visible = true;

                        pnlSteps.Controls[0].Dispose();
                        pnlSteps.Controls.Clear();

                        var ctrl = new PrivilegeLevelSelectionControl(rManager, entities, settings)
                        {
                            Width = Width,
                            Height = Height - 50,
                            Anchor =
                                AnchorStyles.Bottom |
                                AnchorStyles.Left |
                                AnchorStyles.Right |
                                AnchorStyles.Top
                        };
                        pnlSteps.Controls.Add(ctrl);
                        currentStep = 3;
                    }
                    break;
                case 3:
                    {
                        pnlSteps.Visible = true;
                        btnNext.Enabled = false;
                        btnPrevious.Visible = true;
                        btnReset.Visible = true;

                        ((PrivilegeLevelSelectionControl) pnlSteps.Controls[0]).ApplyChanges();

                        //settings.Actions = ((PrivilegeLevelSelectionControl)pnlSteps.Controls[0]).Actions;
                        //pnlSteps.Controls.Clear();

                        //var ctrl = new UpdateControl(rManager, settings);
                        //pnlSteps.Controls.Add(ctrl);
                        //currentStep = 4;
                    }
                    break;
            }
        }

        private void BtnPreviousClick(object sender, EventArgs e)
        {
            switch (currentStep)
            {
                case 2:
                    {
                        pnlSteps.Visible = false;
                        pnlSteps.Controls.Clear();
                        currentStep = 1;

                        btnNext.Visible = false;
                        btnPrevious.Visible = false;
                        btnReset.Visible = false;
                    }
                    break;
                case 3:
                    {
                        pnlSteps.Visible = true;

                        btnNext.Visible = true;
                        btnPrevious.Visible = true;
                        btnReset.Visible = true;

                        // Adds the role selection control in main form
                        pnlSteps.Controls[0].Dispose();
                        pnlSteps.Controls.Clear();
                        var ctrl = new RoleSelectionControl(rManager.Roles, settings)
                        {
                            Width = Width,
                            Height = Height - 50,
                            Anchor =
                                AnchorStyles.Bottom |
                                AnchorStyles.Left |
                                AnchorStyles.Right |
                                AnchorStyles.Top
                        };
                        pnlSteps.Controls.Add(ctrl);
                        currentStep = 2;
                    }
                    break;
                case 4:
                    {
                        pnlSteps.Visible = true;

                        btnNext.Visible = true;
                        btnNext.Enabled = true;
                        btnPrevious.Visible = true;
                        btnReset.Visible = true;

                        pnlSteps.Controls[0].Dispose();
                        pnlSteps.Controls.Clear();
                        var ctrl = new PrivilegeLevelSelectionControl(rManager, entities, settings)
                        {
                            Width = Width,
                            Height = Height - 50,
                            Anchor =
                                AnchorStyles.Bottom |
                                AnchorStyles.Left |
                                AnchorStyles.Right |
                                AnchorStyles.Top
                        };
                        pnlSteps.Controls.Add(ctrl);
                        currentStep = 3;
                    }
                    break;
            }
        }

        private void BtnResetClick(object sender, EventArgs e)
        {
            pnlSteps.Controls.Clear();
            settings = new UpdateSettings();
            var ctrl = new RoleSelectionControl(rManager.Roles, settings);
            pnlSteps.Controls.Add(ctrl);

            btnPrevious.Visible = false;
            btnReset.Visible = false;
            btnNext.Enabled = true;

            currentStep = 2;
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }

        #endregion
    }
}
