using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            option.HiddenPlugins =
                lvPlugins.Items.Cast<ListViewItem>().Where(i => i.Checked == false).Select(i => i.Text).ToList();

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

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            var pManager = new PluginManager();
            pManager.LoadPlugins();

            foreach (var plugin in pManager.Plugins)
            {
                var title = ((AssemblyTitleAttribute) GetAssemblyAttribute(plugin.Assembly, typeof (AssemblyTitleAttribute))).Title;
                var author = ((AssemblyCompanyAttribute) GetAssemblyAttribute(plugin.Assembly, typeof (AssemblyCompanyAttribute))).Company;
                var version = plugin.Assembly.GetName().Version.ToString();

                var item = new ListViewItem(title);
                item.SubItems.Add(author);
                item.SubItems.Add(version);
                item.Checked = option.HiddenPlugins == null || !option.HiddenPlugins.Contains(title);

                lvPlugins.Items.Add(item);
            }
        }

        private object GetAssemblyAttribute(Assembly assembly, Type attributeType)
        {
            return assembly.GetCustomAttributes(attributeType, true)[0];
        }
    }
}
