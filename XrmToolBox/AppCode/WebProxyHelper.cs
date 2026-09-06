using System;
using System.Net;

namespace XrmToolBox.AppCode
{
    internal class WebProxyHelper
    {
        public static void ApplyProxy()
        {
            try
            {
                WebRequest.DefaultWebProxy = null;

                if (Options.Instance.UseCustomProxy)
                {
                    WebRequest.DefaultWebProxy = new WebProxy
                    {
                        Address = new Uri(Options.Instance.ProxyAddress, UriKind.Absolute),
                        BypassProxyOnLocal = Options.Instance.ByPassProxyOnLocal,
                        UseDefaultCredentials = Options.Instance.UseDefaultCredentials,
                    };

                    if (!((WebProxy)WebRequest.DefaultWebProxy).UseDefaultCredentials)
                    {
                        var userNamePart = Options.Instance.UserName.Split('\\');

                        WebRequest.DefaultWebProxy.Credentials = new NetworkCredential
                        {
                            Domain = userNamePart.Length == 2 ? userNamePart[0] : null,
                            UserName = userNamePart.Length == 2 ? userNamePart[1] : userNamePart[0],
                            Password = Options.Instance.Password
                        };
                    }
                }
                else if (Options.Instance.UseInternetExplorerProxy)
                {
                    WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();
                    //Use default credentials if no proxy credentials
                    if (WebRequest.DefaultWebProxy.Credentials == null)
                        WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
                }
                else
                {
                    WebRequest.DefaultWebProxy = null;
                }
            }
            catch
            {
                // Ignore any error
            }
        }
    }
}