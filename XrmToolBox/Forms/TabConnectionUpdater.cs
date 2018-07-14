// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using McTools.Xrm.Connection;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XrmToolBox.New;

namespace XrmToolBox.Forms
{
    public partial class TabConnectionUpdater : Form
    {
        private readonly List<PluginForm> pluginForms;

        public TabConnectionUpdater(List<PluginForm> pluginForms, ConnectionDetail detail)
        {
            InitializeComponent();

            this.pluginForms = pluginForms;
            SelectedPluginForms = new List<PluginForm>();

            lblHeaderDesc.Text = string.Format(lblHeaderDesc.Tag.ToString(), detail.ConnectionName);
        }

        public List<PluginForm> SelectedPluginForms { get; }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvTabs.Items)
            {
                item.Checked = true;
            }
            btnOK.PerformClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvTabs.CheckedItems)
            {
                SelectedPluginForms.Add((PluginForm)item.Tag);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void EnableButtons()
        {
            btnAll.Enabled = lvTabs.Items.Count > 0;
            btnOK.Enabled = lvTabs.CheckedItems.Count > 0;
        }

        private void lvTabs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            EnableButtons();
        }

        private void TabConnectionUpdaterLoad(object sender, EventArgs e)
        {
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
    }
}