using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace McTools.Xrm.Connection
{
    /// <summary>
    /// Stores the list of Crm connections
    /// </summary>
    public class CrmConnections
    {
        #region Variables

        private string _password;
        private string _proxyAddress;

        private bool _useCustomProxy;
        private string _userName;

        #endregion Variables

        public CrmConnections()
        {
            Connections = new List<ConnectionDetail>();
        }

        #region Propriétés

        public bool ByPassProxyOnLocal { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des connexions
        /// </summary>
        public List<ConnectionDetail> Connections { get; set; }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string ProxyAddress
        {
            get { return _proxyAddress; }
            set { _proxyAddress = value; }
        }

        public bool UseCustomProxy
        {
            get { return _useCustomProxy; }
            set { _useCustomProxy = value; }
        }

        public bool UseDefaultCredentials { get; set; }

        public bool UseInternetExplorerProxy { get; set; }

        public bool UseMruDisplay { get; set; }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        #endregion Propriétés

        #region methods

        public static CrmConnections LoadFromFile(string filePath)
        {
            var crmConnections = new CrmConnections();

            if (!File.Exists(filePath))
            {
                return crmConnections;
            }

            using (var fStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (fStream.Length == 0)
                {
                    return crmConnections;
                }

                var doc = XDocument.Load(fStream);
                var crmConnectionsElt = doc.Element("CrmConnections");
                if (crmConnectionsElt == null)
                    return crmConnections;

                var connectionsElt = crmConnectionsElt.Element("Connections");
                if (connectionsElt == null)
                    return crmConnections;

                var useMruDisplayElt = connectionsElt.Element("UseMruDisplay");
                if (useMruDisplayElt != null)
                {
                    crmConnections.UseMruDisplay = useMruDisplayElt.Value == "true";
                }

                var proxyElt = connectionsElt.Element("Proxy");
                if (proxyElt != null)
                {
                    var useCustomProxyElt = proxyElt.Element("UseCustomProxy");
                    if (useCustomProxyElt != null)
                        crmConnections.UseCustomProxy = useCustomProxyElt.Value == "true";
                    var useInternetExplorerProxyElt = proxyElt.Element("UseInternetExplorerProxy");
                    if (useInternetExplorerProxyElt != null)
                        crmConnections.UseInternetExplorerProxy = useInternetExplorerProxyElt.Value == "true";
                    var addressElt = proxyElt.Element("Address");
                    if (addressElt != null)
                        crmConnections.ProxyAddress = addressElt.Value;
                    var usernameElt = proxyElt.Element("Username");
                    if (usernameElt != null)
                        crmConnections.UserName = usernameElt.Value;
                    var passwordElt = proxyElt.Element("Password");
                    if (passwordElt != null)
                        crmConnections.Password = passwordElt.Value;
                    var byPassProxyOnLocalElt = proxyElt.Element("ByPassProxyOnLocal");
                    if (byPassProxyOnLocalElt != null)
                        crmConnections.ByPassProxyOnLocal = byPassProxyOnLocalElt.Value == "true";
                    var useDefaultCredentialsElt = proxyElt.Element("UseDefaultCredentials");
                    if (useDefaultCredentialsElt != null)
                        crmConnections.UseDefaultCredentials = useDefaultCredentialsElt.Value == "true";
                }

                foreach (var elt in doc.Descendants("ConnectionDetail"))
                {
                    var cd = new ConnectionDetail();

                    var authElement = elt.Element("AuthType");
                    if (authElement != null)
                    {
                        cd.AuthType =
                            (AuthenticationProviderType)
                                Enum.Parse(typeof(AuthenticationProviderType), authElement.Value);
                    }

                    var connectionIdElement = elt.Element("ConnectionId");
                    cd.ConnectionId = connectionIdElement != null ? new Guid(connectionIdElement.Value) : Guid.NewGuid();

                    var connectionNameElement = elt.Element("ConnectionName");
                    cd.ConnectionName = connectionNameElement != null ? connectionNameElement.Value : null;

                    var homeRealmUrlElement = elt.Element("HomeRealmUrl");
                    cd.HomeRealmUrl = homeRealmUrlElement != null ? homeRealmUrlElement.Value : null;

                    var isCustomAuthElement = elt.Element("IsCustomAuth");
                    cd.IsCustomAuth = isCustomAuthElement != null && isCustomAuthElement.Value == "true";

                    var organizationElement = elt.Element("Organization");
                    if (organizationElement != null)
                    {
                        cd.Organization = organizationElement.Value;
                    }

                    var originalUrlElement = elt.Element("OriginalUrl");
                    if (originalUrlElement != null)
                    {
                        cd.OriginalUrl = originalUrlElement.Value;
                    }

                    var organizationFriendlyNameElement = elt.Element("OrganizationFriendlyName");
                    if (organizationFriendlyNameElement != null)
                    {
                        cd.OrganizationFriendlyName = organizationFriendlyNameElement.Value;
                    }

                    var organizationServiceUrlElement = elt.Element("OrganizationServiceUrl");
                    if (organizationServiceUrlElement != null)
                    {
                        cd.OrganizationServiceUrl = organizationServiceUrlElement.Value;
                    }

                    var organizationDataServiceUrlElement = elt.Element("OrganizationDataServiceUrl");
                    if (organizationDataServiceUrlElement != null)
                    {
                        cd.OrganizationDataServiceUrl = organizationDataServiceUrlElement.Value;
                    }

                    var organizationUrlNameElement = elt.Element("OrganizationUrlName");
                    if (organizationUrlNameElement != null)
                    {
                        cd.OrganizationUrlName = organizationUrlNameElement.Value;
                    }

                    var organizationVersionElement = elt.Element("OrganizationVersion");
                    if (organizationVersionElement != null)
                    {
                        cd.OrganizationVersion = organizationVersionElement.Value;
                    }

                    var savePasswordElement = elt.Element("SavePassword");
                    cd.SavePassword = savePasswordElement != null && savePasswordElement.Value == "true";

                    var serverNameElement = elt.Element("ServerName");
                    if (serverNameElement != null)
                    {
                        cd.ServerName = serverNameElement.Value;
                    }

                    var serverPortElement = elt.Element("ServerPort");
                    if (serverPortElement != null)
                    {
                        int serverPort = string.IsNullOrEmpty(serverPortElement.Value)
                            ? 80
                            : int.Parse(serverPortElement.Value);

                        cd.ServerPort = serverPort;
                    }

                    var timeOutElement = elt.Element("Timeout");
                    if (timeOutElement != null)
                    {
                        long timeoutValue = string.IsNullOrEmpty(timeOutElement.Value)
                           ? 1200000000
                           : long.Parse(timeOutElement.Value);

                        cd.TimeoutTicks = timeoutValue;
                    }

                    var useIfdElement = elt.Element("UseIfd");
                    cd.UseIfd = useIfdElement != null && useIfdElement.Value == "true";
                    var useOnlineElement = elt.Element("UseOnline");
                    cd.UseOnline = useOnlineElement != null && useOnlineElement.Value == "true";
                    var useOsdpElement = elt.Element("UseOsdp");
                    cd.UseOsdp = useOsdpElement != null && useOsdpElement.Value == "true";
                    var useSslElement = elt.Element("UseSsl");
                    cd.UseSsl = useSslElement != null && useSslElement.Value == "true";

                    var userDomainElement = elt.Element("UserDomain");
                    if (timeOutElement != null)
                    {
                        cd.UserDomain = userDomainElement.Value;
                    }

                    var userNameElement = elt.Element("UserName");
                    if (userNameElement != null)
                    {
                        cd.UserName = userNameElement.Value;
                    }

                    var userPasswordElement = elt.Element("UserPassword");
                    if (userPasswordElement != null)
                    {
                        cd.SetPassword(userPasswordElement.Value, true);
                    }

                    var webApplicationUrlElement = elt.Element("WebApplicationUrl");
                    if (webApplicationUrlElement != null)
                    {
                        cd.WebApplicationUrl = webApplicationUrlElement.Value;
                    }

                    var lastUsedOnElt = elt.Element("LastUsedOn");
                    if (lastUsedOnElt != null)
                    {
                        cd.LastUsedOn = DateTime.Parse(lastUsedOnElt.Value, CultureInfo.InvariantCulture.DateTimeFormat);
                    }

                    var customInfo = elt.Element("CustomInformation");
                    if (customInfo != null)
                    {
                        cd.CustomInformation = new Dictionary<string, string>();
                        foreach (var custel in customInfo.Elements())
                        {
                            cd.CustomInformation.Add(custel.Name.LocalName, custel.Value);
                        }
                    }

                    crmConnections.Connections.Add(cd);
                }
            }

            return crmConnections;
        }

        public void SerializeToFile(string filePath)
        {
            var listElement = new XElement("Connections",
                new XElement("Proxy",
                    new XElement("UseCustomProxy", _useCustomProxy),
                    new XElement("UseInternetExplorerProxy", UseInternetExplorerProxy),
                    new XElement("Address", _proxyAddress),
                    new XElement("Username", _userName),
                    new XElement("Password", _password),
                    new XElement("UseDefaultCredentials", UseDefaultCredentials),
                    new XElement("ByPassProxyOnLocal", ByPassProxyOnLocal)),
                new XElement("UseMruDisplay", UseMruDisplay));

            foreach (var connection in Connections)
            {
                listElement.Add(connection.GetXElement());
            }

            var doc = new XDocument(new XElement("CrmConnections", listElement));

            using (var fStream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                fStream.SetLength(0);
            }

            using (var fStream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                XmlWriter writer = XmlWriter.Create(fStream, new XmlWriterSettings { Indent = true });
                doc.WriteTo(writer);
                writer.Close();
            }
        }

        #endregion methods
    }
}