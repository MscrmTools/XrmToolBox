using McTools.Xrm.Connection;
using System;
using System.Net;

namespace XrmToolBox.AppCode
{
    internal class WebProxyHelper
    {
        public static void ApplyProxy()
        {
            if (ConnectionManager.Instance.ConnectionsList.UseCustomProxy)
            {
                WebRequest.DefaultWebProxy = new WebProxy
                {
                    Address = new Uri(ConnectionManager.Instance.ConnectionsList.ProxyAddress, UriKind.Absolute),
                    BypassProxyOnLocal = ConnectionManager.Instance.ConnectionsList.ByPassProxyOnLocal,
                    UseDefaultCredentials = ConnectionManager.Instance.ConnectionsList.UseDefaultCredentials,
                };

                if (!((WebProxy)WebRequest.DefaultWebProxy).UseDefaultCredentials)
                {
                    var userNamePart = ConnectionManager.Instance.ConnectionsList.UserName.Split('\\');

                    WebRequest.DefaultWebProxy.Credentials = new NetworkCredential
                    {
                        Domain = userNamePart[0],
                        UserName = userNamePart[1],
                        Password = ConnectionManager.Instance.ConnectionsList.Password
                    };
                }
            }
            else if (ConnectionManager.Instance.ConnectionsList.UseInternetExplorerProxy)
            {
                WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();
                //Use default credentials if no proxy credentials
                if (WebRequest.DefaultWebProxy.Credentials == null)
                    WebRequest.DefaultWebProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }
            else
            {
                WebRequest.DefaultWebProxy = null;
            }
        }
    }
}
