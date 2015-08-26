// PROJECT : MsCrmTools.FormParameterManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using MsCrmTools.FormParameterManager.AppCode;
using MsCrmTools.FormParameterManager.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.FormParameterManager
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
     
        private void SetState(bool isRunning)
        {
            lvForms.Enabled = !isRunning;
            lvParameters.Enabled = !isRunning;
            toolStripMenu.Enabled = !isRunning;
        }

        #endregion Methods
      
        #region Load Forms

        private void tsbLoadForms_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadForms);
        }

        void LoadForms()
        {
            // Clear listviews
            lvForms.Items.Clear();
            lvParameters.Items.Clear();

            SetState(true);

            WorkAsync("Loading forms...",
                (bw, e) =>
                {
                    IEnumerable<CrmForm> forms = CrmForm.GetForms(Service, bw);
                    var items = new List<ListViewItem>();

                    foreach (var form in forms)
                    {
                        var item = new ListViewItem(form.Name);
                        item.SubItems.Add(form.EntityLogicalName);
                        item.SubItems.Add((form.Parameters.Count > 0).ToString());
                        item.Tag = form;

                        items.Add(item);
                    }

                    e.Result = items;
                },
                e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        lvForms.Items.AddRange(((List<ListViewItem>)e.Result).ToArray());
                    }

                    SetState(false);
                },
                e => SetWorkingMessage(e.UserState.ToString()));
        }

        #endregion LoadForms

        #region Create Parameters

        private void tsbCreateNewParameter_Click(object sender, EventArgs e)
        {
            if (lvForms.CheckedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select a form before creating a parameter", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pForm = new ParameterForm(Service);
            if (pForm.ShowDialog(this) == DialogResult.OK)
            {
                SetState(true);

                WorkAsync("Creating attribute...",
                    (bw, evt) =>
                    {
                        var parameter = (FormParameter)((object[])evt.Argument)[0];
                        var lvItems = (ListView.CheckedListViewItemCollection)((object[])evt.Argument)[1];
                        var formsUpdated = new List<CrmForm>();

                        foreach (ListViewItem item in lvItems)
                        {
                            var crmForm = (CrmForm)item.Tag;

                            // Creating attribute on form
                            crmForm.AddParameter(parameter);

                            // Updating form
                            crmForm.UpdateForm(Service);

                            item.Tag = crmForm;

                            formsUpdated.Add(crmForm);
                        }

                        // Publishing form
                        bw.ReportProgress(0,"Publishing form(s) ...");
                        CrmForm.PublishForms(Service, formsUpdated.Select(f => f.EntityLogicalName));

                        evt.Result = lvItems;
                    },
                    evt =>
                    {
                        if (evt.Error != null)
                        {
                            MessageBox.Show(this, evt.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            var lvItems = (ListView.CheckedListViewItemCollection)evt.Result;
                            foreach (ListViewItem item in lvItems)
                            {
                                item.SubItems[2].Text = (((CrmForm)item.Tag).Parameters.Count > 0).ToString();
                            }

                            lvForms_ItemChecked(null, null);
                        }

                        SetState(false);
                    },
                    evt=>SetWorkingMessage(evt.UserState.ToString()),
                    new object[] { pForm.Parameter, lvForms.CheckedItems });
            }
        }

        #endregion Create Parameters

        #region Delete Parameters

        private void tsbDeleteParameters_Click(object sender, EventArgs e)
        {
            if (lvParameters.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select a parameter", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetState(true);

            WorkAsync("Deleting attribute(s)...",
                (bw, evt) =>
                {
                    var lvItems = (ListView.SelectedListViewItemCollection) ((object[]) evt.Argument)[0];
                    var formsUpdated = new List<CrmForm>();


                    foreach (ListViewItem item in lvItems)
                    {
                        var parameter = (FormParameter) item.Tag;

                        // Deleting attribute on form
                        parameter.ParentForm.RemoveParameter(parameter);

                        if (!formsUpdated.Contains(parameter.ParentForm))
                        {
                            formsUpdated.Add(parameter.ParentForm);
                        }
                    }

                    // Updating form
                    foreach (var form in formsUpdated)
                    {
                        form.UpdateForm(Service);
                    }

                    bw.ReportProgress(0, "Publishing form(s)...");

                    // Publishing form
                    CrmForm.PublishForms(Service, formsUpdated.Select(f => f.EntityLogicalName));

                    evt.Result = formsUpdated;
                },
                evt =>
                {
                    if (evt.Error != null)
                    {
                        MessageBox.Show(this, evt.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        foreach (var form in (List<CrmForm>) evt.Result)
                        {
                            foreach (ListViewItem item in lvForms.Items)
                            {
                                if (((CrmForm) item.Tag).Id == form.Id)
                                {
                                    item.SubItems[2].Text = (form.Parameters.Count > 0).ToString();
                                }
                            }
                        }

                        lvForms_ItemChecked(null, null);
                    }

                    SetState(false);
                },
                evt => SetWorkingMessage(evt.UserState.ToString()),
                new object[] {lvParameters.SelectedItems});
        }

        #endregion Delete Parameters
      
        #region ListViews events

        private void lvForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvForms.Items)
            {
                item.Checked = item.Selected;
            }
        }

        private void lvForms_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvForms.Sorting = lvForms.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvForms.ListViewItemSorter = new ListViewItemComparer(e.Column, lvForms.Sorting);
        }

        private void lvForms_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvForms.CheckedItems.Count > 0)
            {
                if (lvParameters.Columns.Count == 2)
                {
                    lvParameters.Columns[0].Width = 100;
                    lvParameters.Columns[1].Width = 100;

                    var ch1 = new ColumnHeader {Text = "Entity", Width = 100};
                    var ch2 = new ColumnHeader {Text = "Form", Width = 100};

                    lvParameters.Columns.AddRange(new[] {ch1, ch2});
                }
            }
            else 
            {
                if (lvParameters.Columns.Count > 2)
                {
                    lvParameters.Columns.RemoveAt(3);
                    lvParameters.Columns.RemoveAt(2);
                }
            }

            lvParameters.Items.Clear();

            foreach (ListViewItem lvItem in lvForms.CheckedItems)
            {
                var form = (CrmForm) lvItem.Tag;

                foreach (var parameter in form.Parameters)
                {
                    var item = new ListViewItem(parameter.Name);
                    item.SubItems.Add(parameter.Type.ToString());

                    if (lvForms.CheckedItems.Count > 0)
                    {
                        item.SubItems.Add(parameter.ParentForm.EntityDisplayName);
                        item.SubItems.Add(parameter.ParentForm.Name);
                    }

                    item.Tag = parameter;

                    lvParameters.Items.Add(item);
                }
            }
        }

        private void lvParameters_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvParameters.Sorting = lvParameters.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvParameters.ListViewItemSorter = new ListViewItemComparer(e.Column, lvParameters.Sorting);
        }

        #endregion ListViews events
    }
}
