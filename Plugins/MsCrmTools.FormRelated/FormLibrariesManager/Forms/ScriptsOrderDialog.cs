using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.FormLibrariesManager.Forms
{
    public partial class ScriptsOrderDialog : Form
    {

        public ScriptsOrderDialog(List<Entity> scripts, bool reorderOnly)
        {
            InitializeComponent();
            Scripts = scripts;
            AddFirst = false;
            ReorderOnly = reorderOnly;
            if (reorderOnly)
            {
                rdbAddScriptsToBegining.Visible = false;
                rdbAddScriptsToEnd.Visible = false;
            }
        }

        public bool AddFirst { get; private set; }
        public bool ReorderOnly { get; private set; }
        public List<Entity> Scripts { get; private set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Scripts = lvScripts.Items.Cast<ListViewItem>().Select(i => (Entity)i.Tag).ToList();
            AddFirst = rdbAddScriptsToBegining.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void ScriptsOrderDialog_Load(object sender, EventArgs e)
        {
            foreach (var script in Scripts)
            {
                var item = new ListViewItem(script.GetAttributeValue<string>("name"));
                item.SubItems.Add(script.GetAttributeValue<string>("displayname"));
                item.SubItems.Add(script.GetAttributeValue<string>("description"));
                item.Tag = script;

                lvScripts.Items.Add(item);
            }
        }

        private void tsbDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvScripts.SelectedItems.Count > 0)
                {
                    ListViewItem selected = lvScripts.SelectedItems[0];
                    int indx = selected.Index;
                    int totl = lvScripts.Items.Count;

                    if (indx == totl - 1)
                    {
                        lvScripts.Items.Remove(selected);
                        lvScripts.Items.Insert(0, selected);
                    }
                    else
                    {
                        lvScripts.Items.Remove(selected);
                        lvScripts.Items.Insert(indx + 1, selected);
                    }
                }
                else
                {
                    MessageBox.Show("You can only move one item at a time. Please select only one item and try again.",
                        "Item Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch
            {
            }
        }

        private void tsbUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvScripts.SelectedItems.Count > 0)
                {
                    ListViewItem selected = lvScripts.SelectedItems[0];
                    int indx = selected.Index;
                    int totl = lvScripts.Items.Count;

                    if (indx == 0)
                    {
                        lvScripts.Items.Remove(selected);
                        lvScripts.Items.Insert(totl - 1, selected);
                    }
                    else
                    {
                        lvScripts.Items.Remove(selected);
                        lvScripts.Items.Insert(indx - 1, selected);
                    }
                }
                else
                {
                    MessageBox.Show("You can only move one item at a time. Please select only one item and try again.",
                        "Item Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch
            {
            }
        }
    }
}