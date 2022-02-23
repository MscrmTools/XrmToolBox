using System;
using System.Diagnostics;
using System.Web;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.Extensibility.Forms
{
    public partial class CurrencySelectionDialog : Form
    {
        private IPayPalPlugin pp;

        public CurrencySelectionDialog(IPayPalPlugin pp)
        {
            InitializeComponent();

            this.pp = pp;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string currency = "USD";
            if (sender == btnEuro)
            {
                currency = "EUR";
            }
            else if (sender == btnPound)
            {
                currency = "GBP";
            }

            var url = string.Format(
              "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business={0}&lc={1}&item_name={2}&currency_code={3}&bn=PP%2dDonationsBF",
              pp.EmailAccount,
              "EN",
              HttpUtility.UrlEncode(pp.DonationDescription),
              currency);

            Process.Start(url);
            Close();
        }
    }
}