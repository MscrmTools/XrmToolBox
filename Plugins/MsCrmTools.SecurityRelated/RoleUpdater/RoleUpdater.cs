// PROJECT : MsCrmTools.RoleUpdater
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.RoleUpdater.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;

namespace MsCrmTools.RoleUpdater
{
    public partial class RoleUpdater : PluginControlBase
    {
        #region Variables

        /// <summary>
        /// Wizard current step
        /// </summary>
        private int currentStep = 1;

        /// <summary>
        /// List of all entities metadata
        /// </summary>
        private List<EntityMetadata> entities;

        /// <summary>
        /// Manager to process actions on roles
        /// </summary>
        private RoleManager rManager;

        /// <summary>
        /// Wizard settings
        /// </summary>
        private UpdateSettings settings = new UpdateSettings();

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

        #region Methods

        private void BtnConnectClick(object sender, EventArgs e)
        {
            ExecuteMethod(LoadRolesAndPrivileges);
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
                        ctrl.SettingsApplied += CtrlSettingsApplied;
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

                        ((PrivilegeLevelSelectionControl)pnlSteps.Controls[0]).ApplyChanges();
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

            btnPrevious.Visible = false;
            btnReset.Visible = false;
            btnNext.Enabled = true;

            currentStep = 2;
        }

        private void CtrlSettingsApplied(object sender, EventArgs e)
        {
            btnNext.Enabled = true;
        }

        private void LoadRolesAndPrivileges()
        {
            rManager = new RoleManager(Service);

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading roles...",
                Work = (bw, e) =>
                {
                    rManager.LoadRootRoles();

                    bw.ReportProgress(1, "Loading privileges...");
                    rManager.LoadPrivileges();

                    bw.ReportProgress(2, "Loading Entities privileges...");
                    entities = MetadataHelper.GetEntitiesMetadata(Service, EntityFilters.Privileges);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        var errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                        MessageBox.Show(this, "An error occured: " + errorMessage, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        pnlSteps.Visible = true;
                        btnPrevious.Visible = false;
                        btnReset.Visible = false;
                        btnNext.Visible = true;
                        BtnResetClick(null, null);
                    }
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        #endregion Methods
    }
}