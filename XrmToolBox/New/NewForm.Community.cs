using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.New
{
    partial class NewForm
    {
        #region Events

        private void displayXrmToolBoxHelpToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Process.Start("https://github.com/MscrmTools/XrmToolBox/wiki");
        }

        private void donateDollarPluginMenuItem_Click(object sender, System.EventArgs e)
        {
            if (((PluginForm)dpMain.ActiveContent).Control is IPayPalPlugin payPal)
            {
                Donate("EN", "USD", payPal.EmailAccount, payPal.DonationDescription);
            }
        }

        private void donateEuroPluginMenuItem_Click(object sender, System.EventArgs e)
        {
            if (((PluginForm)dpMain.ActiveContent).Control is IPayPalPlugin payPal)
            {
                Donate("EN", "EUR", payPal.EmailAccount, payPal.DonationDescription);
            }
        }

        private void donateGbpPluginMenuItem_Click(object sender, System.EventArgs e)
        {
            if (((PluginForm)dpMain.ActiveContent).Control is IPayPalPlugin payPal)
            {
                Donate("EN", "GBP", payPal.EmailAccount, payPal.DonationDescription);
            }
        }

        private void donateInEuroToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Donate("EN", "EUR", "tanguy92@hotmail.com", "Donation for MSCRM Tools - XrmToolBox");
        }

        private void donateInGBPToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Donate("EN", "GBP", "tanguy92@hotmail.com", "Donation for MSCRM Tools - XrmToolBox");
        }

        private void donateInUSDollarsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Donate("EN", "USD", "tanguy92@hotmail.com", "Donation for MSCRM Tools - XrmToolBox");
        }

        #endregion Events

        #region Prepare Community items

        private void AssignPayPalMenuItems(ToolStripItemCollection dropDownItems)
        {
            dropDownItems.AddRange(new ToolStripItem[]
            {
                donateInUSDollarsToolStripMenuItem,
                donateInEuroToolStripMenuItem,
                donateInGBPToolStripMenuItem
            });
        }

        private void ProcessMenuItemsForPlugin()
        {
            if (!(dpMain.ActiveContent is PluginForm)) // Home Screen
            {
                githubPluginMenuItem.Visible = false;
                GithubXrmToolBoxMenuItem.Visible = false;
                PaypalXrmToolBoxToolStripMenuItem.Visible = false;
                PayPalSelectedPluginToolStripMenuItem.Visible = false;
                HelpSelectedPluginToolStripMenuItem.Visible = false;
                displayXrmToolBoxHelpToolStripMenuItem.Visible = false;
                AssignPayPalMenuItems(donateToolStripMenuItem.DropDownItems);
                return;
            }

            var pluginName = ((PluginForm)dpMain.ActiveContent).PluginTitle;

            if (((PluginForm)dpMain.ActiveContent).Control is IHelpPlugin help)
            {
                displayXrmToolBoxHelpToolStripMenuItem.Visible = true;
                HelpSelectedPluginToolStripMenuItem.Visible = true;
                HelpSelectedPluginToolStripMenuItem.Text =
                    string.Format(HelpSelectedPluginToolStripMenuItem.Tag.ToString(), pluginName);
                HelpSelectedPluginToolStripMenuItem.Image = (help as PluginControlBase)?.TabIcon;
            }
            else
            {
                HelpSelectedPluginToolStripMenuItem.Visible = false;
                displayXrmToolBoxHelpToolStripMenuItem.Visible = false;
            }

            if (((PluginForm)dpMain.ActiveContent).Control is IPayPalPlugin paypal)
            {
                PaypalXrmToolBoxToolStripMenuItem.Visible = true;
                PayPalSelectedPluginToolStripMenuItem.Visible = true;
                PayPalSelectedPluginToolStripMenuItem.Text = pluginName;
                PayPalSelectedPluginToolStripMenuItem.Image = (paypal as PluginControlBase)?.TabIcon;
                AssignPayPalMenuItems(PaypalXrmToolBoxToolStripMenuItem.DropDownItems);
            }
            else
            {
                PaypalXrmToolBoxToolStripMenuItem.Visible = false;
                PayPalSelectedPluginToolStripMenuItem.Visible = false;
                AssignPayPalMenuItems(donateToolStripMenuItem.DropDownItems);
            }

            if (((PluginForm)dpMain.ActiveContent).Control is IGitHubPlugin github)
            {
                GithubXrmToolBoxMenuItem.Visible = true;
                githubPluginMenuItem.Visible = true;
                githubPluginMenuItem.Text = pluginName;
                githubPluginMenuItem.Image = (github as PluginControlBase)?.TabIcon;
            }
            else
            {
                GithubXrmToolBoxMenuItem.Visible = false;
                githubPluginMenuItem.Visible = false;
            }
        }

        private void HelpSelectedPluginToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Process.Start(GetHelpUrl());
        }

        private void GithubXrmToolBoxMenuItem_Click(object sender, System.EventArgs e)
        {
            Process.Start("https://github.com/MscrmTools/XrmToolBox/issues/new");
        }

        private void githubPluginMenuItem_Click(object sender, System.EventArgs e)
        {
            Process.Start(GetGithubBaseUrl("issues/new"));
        }

        private void helpToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!(dpMain.ActiveContent is PluginForm)) // Home Screen
                displayXrmToolBoxHelpToolStripMenuItem_Click(sender, e);
        }

        private void feedbackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!(dpMain.ActiveContent is PluginForm)) // Home Screen
                GithubXrmToolBoxMenuItem_Click(sender, e);
        }

        #endregion Prepare Community items

        private void Donate(string language, string currency, string emailAccount, string description)
        {
            var url =
               string.Format(
                   "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business={0}&lc={1}&item_name={2}&currency_code={3}&bn=PP%2dDonationsBF",
                   emailAccount,
                   language,
                   HttpUtility.UrlEncode(description),
                   currency);

            Process.Start(url);
        }

        private string GetGithubBaseUrl(string page)
        {
            var plugin = (IGitHubPlugin)((PluginForm)dpMain.ActiveContent).Control;
            return $"https://github.com/{plugin.UserName}/{plugin.RepositoryName}/{page}";
        }

        private string GetHelpUrl()
        {
            var plugin = (IHelpPlugin)((PluginForm)dpMain.ActiveContent).Control;
            return plugin.HelpUrl;
        }
    }
}