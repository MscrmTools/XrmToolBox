using System.Diagnostics;
using System.Web;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Forms;

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
                tsmiDonateUsdXrmToolBox,
                tsmiDonateEurXrmToolBox,
                tsmiDonateGbpXrmToolBox
            });
        }

        private void feedbackToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            GithubXrmToolBoxMenuItem_Click(sender, e);
        }

        private void githubPluginMenuItem_Click(object sender, System.EventArgs e)
        {
            Process.Start(GetGithubBaseUrl("issues/new"));
        }

        private void GithubXrmToolBoxMenuItem_Click(object sender, System.EventArgs e)
        {
            Process.Start("https://github.com/MscrmTools/XrmToolBox/issues/new");
        }

        private void HelpSelectedPluginToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Process.Start(GetHelpUrl());
        }

        private void helpToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!(dpMain.ActiveContent is PluginForm)) // Home Screen
                displayXrmToolBoxHelpToolStripMenuItem_Click(sender, e);
        }

        private void ProcessMenuItemsForPlugin()
        {
            if (!(dpMain.ActiveContent is PluginForm)) // Home Screen
            {
                tsmiFeedbackXrmToolBox.Visible = false;
                tsmiFeedbackSelectedPlugin.Visible = false;
                tsmiDonateXrmToolBox.Visible = false;
                tsmiDonateSelectedPlugin.Visible = false;
                tsmiHelpXrmToolBox.Visible = false;
                tsmiHelpSelectedPlugin.Visible = false;
                tsmiAboutXrmToolBox.Visible = false;
                tsmiAboutSelectedPlugin.Visible = false;
                AssignPayPalMenuItems(tsmiDonate.DropDownItems);
                return;
            }

            var pluginName = ((PluginForm)dpMain.ActiveContent).PluginTitle;

            if (((PluginForm)dpMain.ActiveContent).Control is IHelpPlugin help)
            {
                tsmiHelpXrmToolBox.Visible = true;
                tsmiHelpSelectedPlugin.Visible = true;
                tsmiHelpSelectedPlugin.Text =
                    string.Format(tsmiHelpSelectedPlugin.Tag.ToString(), pluginName);
                tsmiHelpSelectedPlugin.Image = (help as PluginControlBase)?.TabIcon;
                tsmiHelp.Click -= displayXrmToolBoxHelpToolStripMenuItem_Click;
            }
            else
            {
                tsmiHelpXrmToolBox.Visible = false;
                tsmiHelpSelectedPlugin.Visible = false;
                tsmiHelp.Click -= displayXrmToolBoxHelpToolStripMenuItem_Click;
                tsmiHelp.Click += displayXrmToolBoxHelpToolStripMenuItem_Click;
            }

            if (((PluginForm)dpMain.ActiveContent).Control is IPayPalPlugin paypal)
            {
                tsmiDonateXrmToolBox.Visible = true;
                tsmiDonateSelectedPlugin.Visible = true;
                tsmiDonateSelectedPlugin.Text = pluginName;
                tsmiDonateSelectedPlugin.Image = (paypal as PluginControlBase)?.TabIcon;
                AssignPayPalMenuItems(tsmiDonateXrmToolBox.DropDownItems);
            }
            else
            {
                tsmiDonateXrmToolBox.Visible = false;
                tsmiDonateSelectedPlugin.Visible = false;
                AssignPayPalMenuItems(tsmiDonate.DropDownItems);
            }

            if (((PluginForm)dpMain.ActiveContent).Control is IGitHubPlugin github)
            {
                tsmiFeedbackXrmToolBox.Visible = true;
                tsmiFeedbackSelectedPlugin.Visible = true;
                tsmiFeedbackSelectedPlugin.Text = pluginName;
                tsmiFeedbackSelectedPlugin.Image = (github as PluginControlBase)?.TabIcon;
                tsmiFeedback.Click -= feedbackToolStripMenuItem_Click;
            }
            else
            {
                tsmiFeedbackXrmToolBox.Visible = false;
                tsmiFeedbackSelectedPlugin.Visible = false;
                tsmiFeedback.Click -= feedbackToolStripMenuItem_Click;
                tsmiFeedback.Click += feedbackToolStripMenuItem_Click;
            }

            if (((PluginForm)dpMain.ActiveContent).Control is IAboutPlugin aboutPlugin)
            {
                tsmiAboutXrmToolBox.Visible = true;
                tsmiAboutSelectedPlugin.Visible = true;
                tsmiAboutSelectedPlugin.Text = pluginName;
                tsmiAboutSelectedPlugin.Image = (aboutPlugin as PluginControlBase)?.TabIcon;
                tsmiAbout.Click -= tsmiAbout_Click;
            }
            else
            {
                tsmiAboutXrmToolBox.Visible = false;
                tsmiAboutSelectedPlugin.Visible = false;
                tsmiAbout.Click -= tsmiAbout_Click;
                tsmiAbout.Click += tsmiAbout_Click;
            }
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

        private void tsmiAbout_Click(object sender, System.EventArgs e)
        {
            var aForm = new WelcomeDialog(false) { StartPosition = FormStartPosition.CenterParent };
            aForm.ShowDialog(this);
        }

        private void tsmiAboutSelectedPlugin_Click(object sender, System.EventArgs e)
        {
            var plugin = (IAboutPlugin)((PluginForm)dpMain.ActiveContent).Control;
            plugin.ShowAboutDialog();
        }

        private void tsmiAboutXrmToolBox_Click(object sender, System.EventArgs e)
        {
            tsmiAbout_Click(sender, e);
        }
    }
}