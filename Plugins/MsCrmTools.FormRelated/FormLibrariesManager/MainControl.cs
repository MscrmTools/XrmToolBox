// PROJECT : MsCrmTools.FormLibrariesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MsCrmTools.FormLibrariesManager.AppCode;
using MsCrmTools.FormLibrariesManager.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.FormLibrariesManager
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

        private void tsbLoadLibrariesAndScripts_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadLibrariesAndForms);
        }

        private void LoadLibrariesAndForms()
        {
            WorkAsync("Loading libraries...",
                (bw, e) =>
                {
                    var sManager = new ScriptManager(Service);
                    var scripts = sManager.GetAllScripts();

                    bw.ReportProgress(0, "Loading forms...");

                    var fManager = new FormManager(Service);
                    var forms = fManager.GetAllForms();

                    e.Result = new[] { scripts, forms };

                    bw.ReportProgress(0, "Filling controls...");
                },
                e =>
                {
                    var scripts = (List<Entity>)((object[])e.Result)[0];
                    var forms = (List<Entity>)((object[])e.Result)[1];

                    crmScriptsTreeView1.LoadScripts(scripts);
                    crmForms1.LoadForms(forms);

                    if (e.Error != null)
                    {
                        MessageBox.Show(ParentForm, "An error occured:" + e.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                },
                e => SetWorkingMessage(e.UserState.ToString()));
        }

        private void tsbAddCheckedScripts_Click(object sender, EventArgs e)
        {
            lvLogs.Items.Clear();
            var scripts = crmScriptsTreeView1.GetSelectedScripts();
            var forms = crmForms1.GetSelectedForms();

            if (scripts.Count == 0)
            {
                MessageBox.Show(ParentForm, "Please select at least one script", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (forms.Count == 0)
            {
                MessageBox.Show(ParentForm, "Please select at least one form", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var sod = new ScriptsOrderDialog(scripts){StartPosition = FormStartPosition.CenterParent};
            if (sod.ShowDialog(ParentForm) == DialogResult.Cancel)
            {
                return;
            }

            WorkAsync("Updating forms...",
              (bw, evt) =>
              {
                  var scriptsArg = (List<Entity>)((object[])evt.Argument)[0];
                  var addFirstArg = (bool)((object[])evt.Argument)[1];
                  var formsArg = (List<Entity>)((object[])evt.Argument)[2];

                  if (addFirstArg)
                  {
                      scripts.Reverse();
                  }

                  var fManager = new FormManager(Service);

                  bool atLeastOneSuccess = false;

                  foreach (var form in formsArg)
                  {
                      bw.ReportProgress(0, string.Format("Updating form '{0}' ({1})...", form.GetAttributeValue<string>("name"), form.GetAttributeValue<string>("objecttypecode")));

                      foreach (var script in scriptsArg)
                      {
                          try
                          {
                              fManager.AddLibrary(form, script.GetAttributeValue<string>("name"), addFirstArg);

                              atLeastOneSuccess = true;

                              ListViewDelegates.AddItem(lvLogs,
                                 new ListViewItem
                                 {
                                     Text = form.GetAttributeValue<string>("objecttypecode"),
                                     SubItems =
                                {
                                    new ListViewItem.ListViewSubItem {Text = form.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = script.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = "Added!"}
                                },
                                     ForeColor = Color.Green
                                 });
                          }
                          catch (Exception error)
                          {
                              ListViewDelegates.AddItem(lvLogs,
                                  new ListViewItem
                                  {
                                      Text = form.GetAttributeValue<string>("objecttypecode"),
                                      SubItems =
                                {
                                    new ListViewItem.ListViewSubItem {Text = form.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = script.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = error.Message}
                                },
                                      ForeColor = Color.Red
                                  });
                          }
                      }

                      fManager.UpdateForm(form);
                  }

                  if (!atLeastOneSuccess)
                  {
                      return;
                  }

                  foreach (var entitiesToPublish in formsArg.Select(f => f.GetAttributeValue<string>("objecttypecode")).Distinct())
                  {
                      bw.ReportProgress(0, string.Format("Publishing entity '{0}'...", entitiesToPublish));
                      fManager.PublishForm(entitiesToPublish);
                  }
              },
              evt =>
              {
                  if (evt.Error != null)
                  {
                      MessageBox.Show(ParentForm, "An error occured:" + evt.Error.Message, "Error", MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
                  }
              },
              evt => SetWorkingMessage(evt.UserState.ToString()),
              new object[] { sod.Scripts, sod.AddFirst, forms });
        }

        private void tsbRemoveCheckedScripts_Click(object sender, EventArgs e)
        {
            lvLogs.Items.Clear();
            var scriptsParam = crmScriptsTreeView1.GetSelectedScripts();
            var formsParam = crmForms1.GetSelectedForms();

            if (scriptsParam.Count == 0)
            {
                MessageBox.Show(ParentForm, "Please select at least one script", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (formsParam.Count == 0)
            {
                MessageBox.Show(ParentForm, "Please select at least one form", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            WorkAsync("Updating forms...",
                (bw, evt) =>
                {
                    var scripts = (List<Entity>)((object[])evt.Argument)[0];
                    var forms = (List<Entity>)((object[])evt.Argument)[1];

                    var fManager = new FormManager(Service);

                    bool atLeastOneSuccess = false;
                    bool atLeastOneSuccessForForm = false;

                    foreach (var form in forms)
                    {
                        bw.ReportProgress(0, string.Format("Updating form '{0}' ({1})...", form.GetAttributeValue<string>("name"), form.GetAttributeValue<string>("objecttypecode")));

                        foreach (var script in scripts)
                        {
                            try
                            {
                                if (fManager.RemoveLibrary(form, script.GetAttributeValue<string>("name"), ParentForm))
                                {
                                    atLeastOneSuccessForForm = true;

                                    ListViewDelegates.AddItem(lvLogs,
                                   new ListViewItem
                                   {
                                       Text = form.GetAttributeValue<string>("objecttypecode"),
                                       SubItems =
                                {
                                    new ListViewItem.ListViewSubItem {Text = form.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = script.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = "Removed!"}
                                },
                                       ForeColor = Color.Green
                                   });
                                }
                                else
                                {
                                    ListViewDelegates.AddItem(lvLogs,
                                   new ListViewItem
                                   {
                                       Text = form.GetAttributeValue<string>("objecttypecode"),
                                       SubItems =
                                {
                                    new ListViewItem.ListViewSubItem {Text = form.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = script.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = "Canceled!"}
                                },
                                       ForeColor = Color.DarkOrange
                                   });
                                }

                            }
                            catch (Exception error)
                            {
                                ListViewDelegates.AddItem(lvLogs,
                                    new ListViewItem
                                    {
                                        Text = form.GetAttributeValue<string>("objecttypecode"),
                                        SubItems =
                                {
                                    new ListViewItem.ListViewSubItem {Text = form.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = script.GetAttributeValue<string>("name")},
                                    new ListViewItem.ListViewSubItem {Text = error.Message}
                                },
                                        ForeColor = Color.Red
                                    });
                            }
                        }

                        if (atLeastOneSuccessForForm)
                        {
                            fManager.UpdateForm(form);
                            atLeastOneSuccess = true;
                        }
                    }

                    if (!atLeastOneSuccess)
                    {
                        return;
                    }

                    foreach (var entitiesToPublish in forms.Select(f => f.GetAttributeValue<string>("objecttypecode")).Distinct())
                    {
                        bw.ReportProgress(0, string.Format("Publishing entity '{0}'...", entitiesToPublish));
                        fManager.PublishForm(entitiesToPublish);
                    }
                },
                evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(ParentForm, "An error occured:" + evt.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                },
                evt => SetWorkingMessage(evt.UserState.ToString()),
                new[] { scriptsParam, formsParam });
        }
    }
}
