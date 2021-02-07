using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.Forms
{
    public partial class DonationIntroForm : Form
    {
        private readonly string pluginName;

        public DonationIntroForm(string pluginName = null)
        {
            InitializeComponent();

            this.pluginName = pluginName;
        }

        private void DonationIntroForm_Load(object sender, EventArgs e)
        {
            var plugins = PluginManagerExtended.Instance.Plugins.Where(p => p.Value is IPayPalPlugin);

            cbbTools.Items.AddRange(plugins.Select(p => p.Metadata.Name).ToArray());

            if (pluginName != null)
            {
                cbbTools.SelectedItem = pluginName;
                cbbTools.Enabled = false;
            }
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            if (sender == btnXtbDollars)
            {
                OpenPayPal("tanguy92@hotmail.com", "Donation for XrmToolBox", "USD");
            }
            else if (sender == btnXtbEuros)
            {
                OpenPayPal("tanguy92@hotmail.com", "Donation for XrmToolBox", "EUR");
            }
            else if (sender == btnXtbPounds)
            {
                OpenPayPal("tanguy92@hotmail.com", "Donation for XrmToolBox", "GBP");
            }
            else
            {
                var item = cbbTools.SelectedItem;
                if (item == null)
                {
                    MessageBox.Show(this, @"Please Select a tool in the list", @"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var tool = PluginManagerExtended.Instance.Plugins.FirstOrDefault(
                    p => p.Metadata.Name == item.ToString());

                if (tool == null)
                {
                    MessageBox.Show(this, @"Cannot find PayPal information in the tool", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var paypalTool = (IPayPalPlugin)tool.Value;

                if (sender == btnToolDollars)
                {
                    OpenPayPal(paypalTool.EmailAccount, paypalTool.DonationDescription, "USD");
                }
                else if (sender == btnToolEuros)
                {
                    OpenPayPal(paypalTool.EmailAccount, paypalTool.DonationDescription, "EUR");
                }
                else if (sender == btnToolPounds)
                {
                    OpenPayPal(paypalTool.EmailAccount, paypalTool.DonationDescription, "GBP");
                }
            }
        }

        private void OpenPayPal(string emailAccount, string description, string currency)
        {
            var url = string.Format(
                "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business={0}&lc={1}&item_name={2}&currency_code={3}&bn=PP%2dDonationsBF",
                emailAccount,
                "EN",
                HttpUtility.UrlEncode(description),
                currency);

            Process.Start(url);
        }
    }
}