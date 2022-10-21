using McTools.Xrm.Connection.WinForms;
using NuGet.Configuration;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace XrmToolBox.PluginsStore
{
    public class ProxySettingsHelper
    {
        private const string KEY_HTTP_PROXY = "http_proxy";
        private const string KEY_HTTP_PROXY_PASSWORD = "http_proxy.password";
        private const string KEY_HTTP_PROXY_USER = "http_proxy.user";

        public static void registerSettings(Form parentForm)
        {
            ProxySettingsForm frmProxy = new ProxySettingsForm
            {
                StartPosition = FormStartPosition.CenterParent
            };

            var settings = Settings.LoadDefaultSettings("c:\\temp");
            var proxyInfos = SettingsUtility.GetConfigValue(settings, KEY_HTTP_PROXY);

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
                    SettingsUtility.SetConfigValue(settings, KEY_HTTP_PROXY, string.Format("http://{0}:{1}", frmProxy.proxyAddress, frmProxy.proxyPort));
                }
                else
                {
                    SettingsUtility.DeleteConfigValue(settings, KEY_HTTP_PROXY);
                    SettingsUtility.DeleteConfigValue(settings, KEY_HTTP_PROXY_USER);
                    SettingsUtility.DeleteConfigValue(settings, KEY_HTTP_PROXY_PASSWORD);
                }
                MessageBox.Show("You need to restart XrmToolbox for changes to take effect");
            }
        }
    }
}