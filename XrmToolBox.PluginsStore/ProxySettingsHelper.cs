using System.Text.RegularExpressions;
using System.Windows.Forms;
using McTools.Xrm.Connection.WinForms;

namespace XrmToolBox.PluginsStore
{
    public class ProxySettingsHelper
    {

        private const string SECTION_CONFIG = "config";
        private const string KEY_HTTP_PROXY = "http_proxy";
        private const string KEY_HTTP_PROXY_USER = "http_proxy.user";
        private const string KEY_HTTP_PROXY_PASSWORD = "http_proxy.password";

        public static void registerSettings(Form parentForm)
        {
            ProxySettingsForm frmProxy = new ProxySettingsForm
            {
                StartPosition = FormStartPosition.CenterParent
            };

            var settings = NuGet.Settings.LoadDefaultSettings(null, null, null);

            string proxyInfos = settings.GetValue(SECTION_CONFIG, KEY_HTTP_PROXY, false);

            if (proxyInfos == null)
            {
                proxyInfos = "";
            }


            Match m = Regex.Match(proxyInfos, @"http://(.*)?:(.*)", RegexOptions.IgnoreCase);

            if (m.Success)
            {

                frmProxy.UseCustomProxy = true;

                frmProxy.proxyAddress = m.Groups[1].Value;

                frmProxy.proxyPort = m.Groups[2].Value;


            }
            else
            {

                frmProxy.UseCustomProxy = false;

                frmProxy.proxyAddress = "";

                frmProxy.proxyPort = "";
            }

            if (frmProxy.ShowDialog(parentForm) == DialogResult.OK)
            {

                if (frmProxy.UseCustomProxy)
                {
                    settings.SetValue(SECTION_CONFIG, KEY_HTTP_PROXY, string.Format("http://{0}:{1}", frmProxy.proxyAddress, frmProxy.proxyPort));

                }
                else
                {
                    settings.DeleteValue(SECTION_CONFIG, KEY_HTTP_PROXY);
                    settings.DeleteValue(SECTION_CONFIG, KEY_HTTP_PROXY_USER);
                    settings.DeleteValue(SECTION_CONFIG, KEY_HTTP_PROXY_PASSWORD);
                }
                MessageBox.Show("You need to restart XrmToolbox for changes to take effect");


            }
        }

    }
}