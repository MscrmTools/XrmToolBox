using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XrmToolBox.Forms
{
    public partial class OptionsDialog : Form
    {
        private readonly Options option;

        public OptionsDialog(Options option)
        {
            InitializeComponent();

            this.option = (Options)option.Clone();

            rdbToolsListLarge.Checked = option.DisplayLargeIcons;
            rdbToolsListSmall.Checked = !option.DisplayLargeIcons;
            chkDisplayMuFirst.Checked = option.DisplayMostUsedFirst;
        }

        public Options Option { get { return option; } }

        private void BtnOkClick(object sender, EventArgs e)
        {
            option.DisplayLargeIcons = rdbToolsListLarge.Checked;
            option.DisplayMostUsedFirst = chkDisplayMuFirst.Checked;

            if (!option.DisplayMostUsedFirst && option.MostUsedList.Count > 0)
                option.MostUsedList.Clear();

            option.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnResetMuListClick(object sender, EventArgs e)
        {
            option.MostUsedList = new List<PluginUseCount>();
        }
    }
}
