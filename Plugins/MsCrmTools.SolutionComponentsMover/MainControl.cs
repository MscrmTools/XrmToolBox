using Microsoft.Xrm.Sdk;
using MsCrmTools.SolutionComponentsMover.AppCode;
using MsCrmTools.SolutionComponentsMover.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.SolutionComponentsMover
{
    public partial class MainControl : PluginControlBase
    {
        private SolutionManager sManager;

        public MainControl()
        {
            InitializeComponent();
        }

        public void LoadSolutions()
        {
            sManager = new SolutionManager(Service);

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading solutions...",
                Work = (bw, e) =>
                {
                    e.Result = sManager.RetrieveSolutions();
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error == null)
                    {
                        sourceSolutionPicker.LoadSolutions((IEnumerable<Entity>)e.Result);
                        targetSolutionPicker.LoadSolutions((IEnumerable<Entity>)e.Result);
                    }
                    else
                    {
                        MessageBox.Show(ParentForm, "An error occured: " + e.Error.Message, "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                },
            });
        }

        private void tsbCloseThisTab_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbCopyComponents_Click(object sender, EventArgs e)
        {
            var settings = new CopySettings
            {
                SourceSolutions = sourceSolutionPicker.SelectedSolutions,
                TargetSolutions = targetSolutionPicker.SelectedSolutions
            };

            var csForm = new ComponentTypeSelector();
            if (csForm.ShowDialog(ParentForm) == DialogResult.OK)
            {
                settings.ComponentsTypes = csForm.SelectedComponents;
            }
            else
            {
                return;
            }

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Starting copy...",
                AsyncArgument = settings,
                Work = (bw, evt) =>
                {
                    sManager.CopyComponents((CopySettings)evt.Argument, bw);
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(ParentForm, "An error occured: " + evt.Error.Message, "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                },
                ProgressChanged = evt => { SetWorkingMessage(evt.UserState.ToString()); }
            });
        }

        private void tsbLoadSolutions_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadSolutions);
        }
    }
}