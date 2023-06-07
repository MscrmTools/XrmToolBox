using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace XrmToolBox.Controls
{
    public partial class AdvancedControl : UserControl
    {
        public AdvancedControl()
        {
            InitializeComponent();
        }

        public event EventHandler OnTabsOrderChanged;

        private void AdvancedControl_Load(object sender, EventArgs e)
        {
            if (Options.Instance.OrderForSettingsTab.Count > 0)
            {
                foreach (var item in Options.Instance.OrderForSettingsTab)
                {
                    if (!lbTabs.Items.Contains(item))
                    {
                        lbTabs.Items.Add(item);
                    }
                }

                return;
            }

            var type = typeof(Options);
            foreach (var pi in type.GetProperties())
            {
                var a = pi.GetCustomAttributes(typeof(CategoryAttribute), true).FirstOrDefault() as CategoryAttribute;
                if (a != null)
                {
                    if (!lbTabs.Items.Contains(a.Category))
                    {
                        lbTabs.Items.Add(a.Category);
                    }
                }
            }

            lbTabs.Items.AddRange(new[] { "Proxy", "Paths", "Credits", "Application Protocol", "Assemblies" });
        }

        private void bnApply_Click(object sender, EventArgs e)
        {
            Options.Instance.OrderForSettingsTab = lbTabs.Items.Cast<string>().ToList();
            Options.Instance.ReportSettingsChange(new AppCode.SettingsPropertyEventArgs(nameof(Options.Instance.OrderForSettingsTab), Options.Instance.OrderForSettingsTab));
            OnTabsOrderChanged?.Invoke(this, e);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lbTabs.SelectedItem == null) return;
            if (lbTabs.SelectedIndex == lbTabs.Items.Count - 1) return;

            lbTabs.Sorted = false;

            var index = lbTabs.SelectedIndex;
            var item = lbTabs.SelectedItem.ToString();

            lbTabs.Items.RemoveAt(index);
            lbTabs.Items.Insert(index + 1, item);
            lbTabs.SelectedIndex = index + 1;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            lbTabs.Sorted = true;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lbTabs.SelectedItem == null) return;
            if (lbTabs.SelectedIndex == 0) return;

            lbTabs.Sorted = false;

            var index = lbTabs.SelectedIndex;
            var item = lbTabs.SelectedItem.ToString();

            lbTabs.Items.RemoveAt(index);
            lbTabs.Items.Insert(index - 1, item);
            lbTabs.SelectedIndex = index - 1;
        }
    }
}