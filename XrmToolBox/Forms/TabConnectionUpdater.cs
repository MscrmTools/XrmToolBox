// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XrmToolBox.Forms
{
    public partial class TabConnectionUpdater : Form
    {
        private readonly List<TabPage> tabs;

        public TabConnectionUpdater(List<TabPage> tabs)
        {
            InitializeComponent();

            this.tabs = tabs;
        }

        public List<TabPage> SelectedTabs { get; private set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            SelectedTabs = new List<TabPage>();

            foreach (ListViewItem item in lvTabs.CheckedItems)
            {
                SelectedTabs.Add((TabPage)item.Tag);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void TabConnectionUpdaterLoad(object sender, EventArgs e)
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
    }
}