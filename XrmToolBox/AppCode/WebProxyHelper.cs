﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using McTools.Xrm.Connection;

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
            }
            else
            {
                WebRequest.DefaultWebProxy = null;
            }
        }
    }
}
