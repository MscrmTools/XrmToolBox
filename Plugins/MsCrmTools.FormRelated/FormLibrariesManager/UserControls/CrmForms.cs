using Microsoft.Xrm.Sdk;
using MsCrmTools.FormLibrariesManager.AppCode;
using MsCrmTools.FormLibrariesManager.Forms;
using MsCrmTools.FormRelated.FormLibrariesManager.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace MsCrmTools.FormLibrariesManager.UserControls
{
    public partial class CrmForms : UserControl
    {
        public CrmForms()
        {
            InitializeComponent();
            ScriptsByName = new Dictionary<string, Entity>();
        }

        public MainControl Plugin { get; set; }
        public Dictionary<string, Entity> ScriptsByName { get; private set; }
        public IOrganizationService Service { get; set; }

        public List<Entity> GetSelectedForms()
        {
            return lvForms.CheckedItems.Cast<ListViewItem>().Select(i => (Entity)i.Tag).ToList();
        }

        public void LoadForms(List<Entity> forms, List<Entity> scripts, MainControl plugin)
        {
            ScriptsByName.Clear();
            foreach (var script in scripts)
            {
                ScriptsByName.Add(script.GetAttributeValue<string>("name"), script);
            }

            Plugin = plugin;
            lvForms.Items.Clear();

            foreach (var form in forms)
            {
                var item = new ListViewItem(form.GetAttributeValue<string>("objecttypecode"));
                item.SubItems.Add(form.GetAttributeValue<string>("name"));
                item.Tag = form;

                lvForms.Items.Add(item);
            }
        }

        private void lvForms_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvForms.Sorting = lvForms.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvForms.ListViewItemSorter = new ListViewItemComparer(e.Column, lvForms.Sorting);
        }

        private void lvForms_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            if (lvForms.FocusedItem.Bounds.Contains(e.Location))
            {
                rightClickFormMenu.Show(Cursor.Position);
            }
        }

        #region Right Click Context Menu

        private void manageOnLoadEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEventsDialog fod;
            try
            {
                var form = (Entity)lvForms.FocusedItem.Tag;
                fod = new FormEventsDialog(form, "onload") { StartPosition = FormStartPosition.CenterParent };
                if (fod.ShowDialog(ParentForm) == DialogResult.Cancel)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Plugin.UpdateFormEventHanders(fod.Form, fod.EventName, fod.FormEvents.ToList());
        }

        private void manageOnSaveEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEventsDialog fod;
            try
            {
                var form = (Entity)lvForms.FocusedItem.Tag;
                fod = new FormEventsDialog(form, "onsave") { StartPosition = FormStartPosition.CenterParent };
                if (fod.ShowDialog(ParentForm) == DialogResult.Cancel)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Plugin.UpdateFormEventHanders(fod.Form, fod.EventName, fod.FormEvents.ToList());
        }

        private void viewLibrariesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = (Entity)lvForms.FocusedItem.Tag;
                var formLibrariesNode = FormManager.GetFormLibraries(form);
                if (formLibrariesNode == null || !formLibrariesNode.HasChildNodes)
                {
                    MessageBox.Show("No Libraries are currently registered for the form");
                    return;
                }
                var scripts = (from XmlNode l in formLibrariesNode
                               select ScriptsByName[l.Attributes["name"].Value]).ToList();

                var sod = new ScriptsOrderDialog(scripts, true) { StartPosition = FormStartPosition.CenterParent };
                if (sod.ShowDialog(ParentForm) == DialogResult.Cancel)
                {
                    return;
                }

                var libraryNodes = new List<XmlNode>();
                foreach (XmlNode node in formLibrariesNode.ChildNodes)
                {
                    libraryNodes.Add(node);
                }

                formLibrariesNode.RemoveAll();

                foreach (var script in sod.Scripts)
                {
                    foreach (var libraryNode in libraryNodes)
                    {
                        if (libraryNode.Attributes["name"].Value == script.GetAttributeValue<string>("name"))
                        {
                            formLibrariesNode.AppendChild(libraryNode);
                            break;
                        }
                    }
                }

                Plugin.UpdateFormLibraryOrder(form, formLibrariesNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion Right Click Context Menu
    }
}