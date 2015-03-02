using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xrm.Client.Windows.Controls.ConnectionDialog;
using Microsoft.Xrm.Sdk.Client;

namespace McTools.Xrm.Connection
{
    /// <summary>
    /// Stores the list of Crm connections
    /// </summary>
    public class CrmConnections
    {
        #region Variables

        /// <summary>
        /// Liste de connexions
        /// </summary>
        List<ConnectionDetail> _connections;

        string _proxyAddress;

        string _proxyPort;

        string _userName;

        string _password;

        bool _useCustomProxy;

        #endregion

        public CrmConnections()
        {
            Connections = new List<ConnectionDetail>();
        }

        #region Propriétés

        /// <summary>
        /// Obtient ou définit la liste des connexions
        /// </summary>
        public List<ConnectionDetail> Connections
        {
            get { return _connections; }
            set { _connections = value; }
        }

        public string ProxyAddress
        {
            get { return _proxyAddress; }
            set { _proxyAddress = value; }
        }


        public string ProxyPort
        {
            get { return _proxyPort; }
            set { _proxyPort = value; }
        }


        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }


        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }


        public bool UseCustomProxy
        {
            get { return _useCustomProxy; }
            set { _useCustomProxy = value; }
        }

        #endregion

        #region methods

        public void SerializeToFile(string filePath)
        {
            var listElement = new XElement("Connections");
                
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

                foreach (var elt in doc.Descendants("ConnectionDetail"))
                {
                    var cd = new ConnectionDetail();

                    var authElement = elt.Element("AuthType");
                    if (authElement != null)
                    {
                        cd.AuthType =
                            (AuthenticationProviderType)
                                Enum.Parse(typeof (AuthenticationProviderType), authElement.Value);
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
                        cd.ServerPort = int.Parse(serverPortElement.Value);
                    }

                    var timeOutElement = elt.Element("Timeout");
                    if (timeOutElement != null)
                    {
                        cd.TimeoutTicks = int.Parse(timeOutElement.Value);
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
                        cd.SetPassword(userPasswordElement.Value);
                    }

                    var webApplicationUrlElement = elt.Element("WebApplicationUrl");
                    if (webApplicationUrlElement != null)
                    {
                        cd.WebApplicationUrl = webApplicationUrlElement.Value;
                    }

                    crmConnections.Connections.Add(cd);
                }
            }

            return crmConnections;
        }

        #endregion
    }
}