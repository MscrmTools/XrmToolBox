using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace XrmToolBox
{
    partial class MainForm
    {
        #region Events

        private void displayXrmToolBoxHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/MscrmTools/XrmToolBox/wiki");
        }

        private void donateDollarPluginMenuItem_Click(object sender, EventArgs e)
        {
            var plugin = tabControl1.SelectedTab.GetPaypalPlugin();
            Donate("EN", "USD", plugin.EmailAccount, plugin.DonationDescription);
        }

        private void donateEuroPluginMenuItem_Click(object sender, EventArgs e)
        {
            var plugin = tabControl1.SelectedTab.GetPaypalPlugin();
            Donate("EN", "EUR", plugin.EmailAccount, plugin.DonationDescription);
        }

        private void donateGbpPluginMenuItem_Click(object sender, EventArgs e)
        {
            var plugin = tabControl1.SelectedTab.GetPaypalPlugin();
            Donate("EN", "GBP", plugin.EmailAccount, plugin.DonationDescription);
        }

        private void donateInEuroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Donate("EN", "EUR", "tanguy92@hotmail.com", "Donation for MSCRM Tools - XrmToolBox");
        }

        private void donateInGBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Donate("EN", "GBP", "tanguy92@hotmail.com", "Donation for MSCRM Tools - XrmToolBox");
        }

        private void donateInUSDollarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Donate("EN", "USD", "tanguy92@hotmail.com", "Donation for MSCRM Tools - XrmToolBox");
        }

        private void TsbDiscussPluginClick(object sender, EventArgs e)
        {
            Process.Start(GetCodePlexUrl("Discussions"));
        }

        private void TsbRatePluginClick(object sender, EventArgs e)
        {
            Process.Start(GetCodePlexUrl("Releases"));
        }

        private void TsbReportBugPluginClick(object sender, EventArgs e)
        {
            Process.Start(GetCodePlexUrl("WorkItem/Create"));
        }

        #endregion Events

        #region Prepare Community items

        private void AssignHelpMenuItems(ToolStripItemCollection dropDownItems)
        {
            dropDownItems.AddRange(new ToolStripItem[] {
                displayXrmToolBoxHelpToolStripMenuItem});
        }

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
            if (tabControl1.SelectedIndex == 0) // Home Screen
            {
                tstxtFilterPlugin.Enabled = true;
                githubPluginMenuItem.Visible = false;
                GithubXrmToolBoxMenuItem.Visible = false;
                PaypalXrmToolBoxToolStripMenuItem.Visible = false;
                PayPalSelectedPluginToolStripMenuItem.Visible = false;
                HelpSelectedPluginToolStripMenuItem.Visible = false;
                displayXrmToolBoxHelpToolStripMenuItem.Visible = false;
                AssignPayPalMenuItems(donateToolStripMenuItem.DropDownItems);
                return;
            }

            // Disabling plugin search if not a home screen
            tstxtFilterPlugin.Enabled = false;
            var pluginName = tabControl1.SelectedTab.GetPluginName();

            var helpedPlugin = tabControl1.SelectedTab.GetHelpEnabledPlugin();
            if (helpedPlugin == null)
            {
                HelpSelectedPluginToolStripMenuItem.Visible = false;
                displayXrmToolBoxHelpToolStripMenuItem.Visible = false;
            }
            else
            {
                displayXrmToolBoxHelpToolStripMenuItem.Visible = true;
                HelpSelectedPluginToolStripMenuItem.Visible = true;
                HelpSelectedPluginToolStripMenuItem.Text =
                    string.Format(HelpSelectedPluginToolStripMenuItem.Tag.ToString(), pluginName);
                HelpSelectedPluginToolStripMenuItem.Image = (helpedPlugin as PluginControlBase)?.TabIcon;
            }

            var paypalPlugin = tabControl1.SelectedTab.GetPaypalPlugin();
            if (paypalPlugin == null)
            {
                PaypalXrmToolBoxToolStripMenuItem.Visible = false;
                PayPalSelectedPluginToolStripMenuItem.Visible = false;
                AssignPayPalMenuItems(donateToolStripMenuItem.DropDownItems);
            }
            else
            {
                PaypalXrmToolBoxToolStripMenuItem.Visible = true;
                PayPalSelectedPluginToolStripMenuItem.Visible = true;
                PayPalSelectedPluginToolStripMenuItem.Text = pluginName;
                PayPalSelectedPluginToolStripMenuItem.Image = (paypalPlugin as PluginControlBase)?.TabIcon;
                AssignPayPalMenuItems(PaypalXrmToolBoxToolStripMenuItem.DropDownItems);
            }

            var githubPlugin = tabControl1.SelectedTab.GetGithubPlugin();

            if (githubPlugin == null)
            {
                GithubXrmToolBoxMenuItem.Visible = false;
                githubPluginMenuItem.Visible = false;
            }
            else
            {
                GithubXrmToolBoxMenuItem.Visible = true;
                githubPluginMenuItem.Visible = true;
                githubPluginMenuItem.Text = pluginName;
                githubPluginMenuItem.Image = (githubPlugin as PluginControlBase)?.TabIcon;
            }
        }

        private void HelpSelectedPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(GetHelpUrl());
        }

        private void GithubXrmToolBoxMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/MscrmTools/XrmToolBox/issues/new");
        }

        private void githubPluginMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(GetGithubBaseUrl("issues/new"));
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                displayXrmToolBoxHelpToolStripMenuItem_Click(sender, e);
        }

        private void feedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
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

        private string GetCodePlexUrl(string page)
        {
            var plugin = tabControl1.SelectedTab.GetCodePlexPlugin();
            return String.Format("http://{0}.codeplex.com/{1}", plugin.CodePlexUrlName, page);
        }

        private string GetGithubBaseUrl(string page)
        {
            var plugin = tabControl1.SelectedTab.GetGithubPlugin();
            return String.Format("https://github.com/{0}/{1}/{2}", plugin.UserName, plugin.RepositoryName, page);
        }

        private string GetHelpUrl()
        {
            var plugin = tabControl1.SelectedTab.GetHelpEnabledPlugin();
            return plugin.HelpUrl;
        }
    }
}