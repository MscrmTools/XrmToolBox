// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XrmToolBox.TempNew;

namespace XrmToolBox.Forms
{
    public partial class TabConnectionUpdater : Form
    {
        private readonly List<TabPage> tabs;

        private readonly List<PluginForm> pluginForms;

        public TabConnectionUpdater(List<TabPage> tabs)
        {
            InitializeComponent();

            this.tabs = tabs;
            SelectedTabs = new List<TabPage>();
        }

        public TabConnectionUpdater(List<PluginForm> pluginForms)
        {
            InitializeComponent();

            this.pluginForms = pluginForms;
            SelectedPluginForms = new List<PluginForm>();
        }

        public List<TabPage> SelectedTabs { get; private set; }
        public List<PluginForm> SelectedPluginForms { get; private set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvTabs.CheckedItems)
            {
                if (item.Tag is TabPage t)
                {
                    SelectedTabs.Add(t);
                }
                else
                {
                    SelectedPluginForms.Add((PluginForm)item.Tag);
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void TabConnectionUpdaterLoad(object sender, EventArgs e)
        {
            if (tabs != null)
            {
                foreach (var tab in tabs)
                {
                    string[] nameParts = tab.Text.Split('(');

                    var item = new ListViewItem(nameParts[0]);
                    item.SubItems.Add(nameParts[1].Replace(")", ""));
                    item.Tag = tab;

                    lvTabs.Items.Add(item);
                }
            }

            if (pluginForms != null)
            {
                foreach (var pluginForm in pluginForms)
                {
                    string[] nameParts = pluginForm.Text.Split('(');

                    var item = new ListViewItem(nameParts[0]);
                    item.SubItems.Add(nameParts[1].Replace(")", ""));
                    item.Tag = pluginForm;

                    lvTabs.Items.Add(item);
                }
            }

            EnableButtons();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvTabs.Items)
            {
                item.Checked = true;
            }
            btnOK.PerformClick();
        }

        private void lvTabs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            EnableButtons();
        }

        private void EnableButtons()
        {
            btnAll.Enabled = lvTabs.Items.Count > 0;
            btnOK.Enabled = lvTabs.CheckedItems.Count > 0;
        }
    }
}