using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security;
using System.ServiceModel.Description;
using System.Xml.Linq;

namespace McTools.Xrm.Connection
{
    /// <summary>
    /// Stores data regarding a specific connection to Crm server
    /// </summary>
    public class ConnectionDetail : IComparable, ICloneable
    {
        private string userPassword;

        #region Propriétés

        private CrmServiceClient crmSvc;
        public AuthenticationProviderType AuthType { get; set; }

        /// <summary>
        /// Gets or sets the connection unique identifier
        /// </summary>
        public Guid? ConnectionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the connection
        /// </summary>
        public string ConnectionName { get; set; }

        /// <summary>
        /// Get or set the Crm Ticket
        /// </summary>
        public string CrmTicket { get; set; }

        /// <summary>
        /// Gets or sets custom information for use by consuming application
        /// </summary>
        public Dictionary<string, string> CustomInformation { get; set; }

        /// <summary>
        /// Gets or sets the Home realm url for ADFS authentication
        /// </summary>
        public string HomeRealmUrl { get; set; }

        /// <summary>
        /// Get or set flag to know if custom authentication
        /// </summary>
        public bool IsCustomAuth { get; set; }

        public DateTime LastUsedOn { get; set; }

        /// <summary>
        /// Get or set the organization name
        /// </summary>
        public string Organization { get; set; }

        public string OrganizationDataServiceUrl { get; set; }

        /// <summary>
        /// Get or set the organization friendly name
        /// </summary>
        public string OrganizationFriendlyName { get; set; }

        public int OrganizationMajorVersion
        {
            get
            {
                return OrganizationVersion != null ? int.Parse(OrganizationVersion.Split('.')[0]) : -1;
            }
        }

        public int OrganizationMinorVersion
        {
            get
            {
                return OrganizationVersion != null ? int.Parse(OrganizationVersion.Split('.')[1]) : -1;
            }
        }

        /// <summary>
        /// Gets or sets the Crm Service Url
        /// </summary>
        public string OrganizationServiceUrl { get; set; }

        /// <summary>
        /// Get or set the organization name
        /// </summary>
        public string OrganizationUrlName { get; set; }

        public string OrganizationVersion { get; set; }
        public string OriginalUrl { get; set; }

        /// <summary>
        /// Gets an information if the password is empty
        /// </summary>
        public bool PasswordIsEmpty { get { return string.IsNullOrEmpty(userPassword); } }

        /// <summary>
        /// Gets or sets the information if the password must be saved
        /// </summary>
        public bool SavePassword { get; set; }

        /// <summary>
        /// Get or set the server name
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Get or set the server port
        /// </summary>
        [DefaultValue(80)]
        public int ServerPort { get; set; }

        public CrmServiceClient ServiceClient
        {
            get { return GetCrmServiceClient(); }
            set { crmSvc = value; }
        }

        public TimeSpan Timeout { get; set; }

        public long TimeoutTicks
        {
            get { return Timeout.Ticks; }
            set { Timeout = new TimeSpan(value); }
        }

        /// <summary>
        /// Get or set flag to know if we use IFD
        /// </summary>
        public bool UseIfd { get; set; }

        /// <summary>
        /// Get or set flag to know if we use CRM Online
        /// </summary>
        public bool UseOnline { get; set; }

        /// <summary>
        /// Get or set flag to know if we use Online Services
        /// </summary>
        public bool UseOsdp { get; set; }

        /// <summary>
        /// Get or set the user domain name
        /// </summary>
        public string UserDomain { get; set; }

        /// <summary>
        /// Get or set user login
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Get or set the use of SSL connection
        /// </summary>
        public bool UseSsl { get; set; }

        public string WebApplicationUrl { get; set; }

        #endregion Propriétés

        #region Méthodes

        public void ErasePassword()
        {
            userPassword = null;
        }

        public CrmServiceClient GetCrmServiceClient(bool forceNewService = false)
        {
            if (forceNewService == false && crmSvc != null)
            {
                return crmSvc;
            }

            return new CrmServiceClient(GetOrganizationCrmConnectionString());

            if (UseOnline)
            {
                crmSvc = ConnectOnline(UseOsdp);
            }
            else if (UseIfd)
            {
                var password = CryptoManager.Decrypt(userPassword, ConnectionManager.CryptoPassPhrase,
                   ConnectionManager.CryptoSaltValue,
                   ConnectionManager.CryptoHashAlgorythm,
                   ConnectionManager.CryptoPasswordIterations,
                   ConnectionManager.CryptoInitVector,
                   ConnectionManager.CryptoKeySize);

                crmSvc = new CrmServiceClient(new NetworkCredential(UserName, password, UserDomain), AuthenticationType.IFD, ServerName, ServerPort.ToString(),
                   OrganizationUrlName, true, UseSsl);
            }
            else
            {
                NetworkCredential credential;
                if (!IsCustomAuth)
                {
                    credential = CredentialCache.DefaultNetworkCredentials;
                }
                else
                {
                    var password = CryptoManager.Decrypt(userPassword, ConnectionManager.CryptoPassPhrase,
                  ConnectionManager.CryptoSaltValue,
                  ConnectionManager.CryptoHashAlgorythm,
                  ConnectionManager.CryptoPasswordIterations,
                  ConnectionManager.CryptoInitVector,
                  ConnectionManager.CryptoKeySize);

                    credential = new NetworkCredential(UserName, password, UserDomain);
                }
                crmSvc = new CrmServiceClient(credential, AuthenticationType.AD, ServerName, ServerPort.ToString(), OrganizationUrlName, true, UseSsl);
            }

            if (!crmSvc.IsReady)
            {
                var error = crmSvc.LastCrmError;
                crmSvc = null;
                throw new Exception(error);
            }

            return crmSvc;
        }

        public void SetPassword(string password, bool isEncrypted = false)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (isEncrypted)
                {
                    userPassword = password;
                }
                else
                {
                    userPassword = CryptoManager.Encrypt(password, ConnectionManager.CryptoPassPhrase,
                        ConnectionManager.CryptoSaltValue,
                        ConnectionManager.CryptoHashAlgorythm,
                        ConnectionManager.CryptoPasswordIterations,
                        ConnectionManager.CryptoInitVector,
                        ConnectionManager.CryptoKeySize);
                }
            }
        }

        //public IOrganizationService GetOrganizationService()
        //{
        //    return new OrganizationService(CrmConnection.Parse(GetOrganizationCrmConnectionString()));
        //}
        /// <summary>
        /// Retourne le nom de la connexion
        /// </summary>
        /// <returns>Nom de la connexion</returns>
        public override string ToString()
        {
            return ConnectionName;
        }

        //public IDiscoveryService GetDiscoveryService()
        //{
        //    return new DiscoveryService(CrmConnection.Parse(GetDiscoveryCrmConnectionString()));
        //}
        public void UpdateAfterEdit(ConnectionDetail editedConnection)
        {
            ConnectionName = editedConnection.ConnectionName;
            OrganizationServiceUrl = editedConnection.OrganizationServiceUrl;
            OrganizationDataServiceUrl = editedConnection.OrganizationDataServiceUrl;
            CrmTicket = editedConnection.CrmTicket;
            IsCustomAuth = editedConnection.IsCustomAuth;
            Organization = editedConnection.Organization;
            OrganizationFriendlyName = editedConnection.OrganizationFriendlyName;
            ServerName = editedConnection.ServerName;
            ServerPort = editedConnection.ServerPort;
            UseIfd = editedConnection.UseIfd;
            UseOnline = editedConnection.UseOnline;
            UseOsdp = editedConnection.UseOsdp;
            UserDomain = editedConnection.UserDomain;
            UserName = editedConnection.UserName;
            userPassword = editedConnection.userPassword;
            UseSsl = editedConnection.UseSsl;
            HomeRealmUrl = editedConnection.HomeRealmUrl;
        }

        private CrmServiceClient ConnectOnline(bool isOffice365)
        {
            var password = CryptoManager.Decrypt(userPassword, ConnectionManager.CryptoPassPhrase,
                 ConnectionManager.CryptoSaltValue,
                 ConnectionManager.CryptoHashAlgorythm,
                 ConnectionManager.CryptoPasswordIterations,
                 ConnectionManager.CryptoInitVector,
                 ConnectionManager.CryptoKeySize);

            var securePassword = new SecureString();
            foreach (char c in password)
                securePassword.AppendChar(c);
            securePassword.MakeReadOnly();

            return new CrmServiceClient(UserName, securePassword, GetOnlineRegion(ServerName), OrganizationUrlName, true, UseSsl, isOffice365: isOffice365);
        }

        private string GetDiscoveryCrmConnectionString()
        {
            DbConnectionStringBuilder dbcb = new DbConnectionStringBuilder();
            dbcb.Add("Url", string.Format("{0}://{1}:{2}",
                UseSsl ? "https" : "http",
                UseIfd ? ServerName : UseOsdp ? "disco." + ServerName : UseOnline ? "dev." + ServerName : ServerName,
                ServerPort == 0 ? (UseSsl ? 443 : 80) : ServerPort));

            if (IsCustomAuth)
            {
                if (!UseIfd)
                {
                    if (!string.IsNullOrEmpty(UserDomain))
                    {
                        dbcb.Add("Domain", UserDomain);
                    }
                }

                string username = UserName;
                if (UseIfd)
                {
                    if (!string.IsNullOrEmpty(UserDomain))
                    {
                        username = string.Format("{0}\\{1}", UserDomain, UserName);
                    }
                }

                if (string.IsNullOrEmpty(userPassword))
                {
                    throw new Exception("User password cannot be null. If the user password is not stored in configuration file, you should request it from the end user");
                }

                var decryptedPassword = CryptoManager.Decrypt(userPassword, ConnectionManager.CryptoPassPhrase,
                    ConnectionManager.CryptoSaltValue,
                    ConnectionManager.CryptoHashAlgorythm,
                    ConnectionManager.CryptoPasswordIterations,
                    ConnectionManager.CryptoInitVector,
                    ConnectionManager.CryptoKeySize);

                dbcb.Add("Username", username);
                dbcb.Add("Password", decryptedPassword);
            }

            if (UseOnline && !UseOsdp)
            {
                ClientCredentials deviceCredentials;

                do
                {
                    deviceCredentials = DeviceIdManager.LoadDeviceCredentials() ??
                                        DeviceIdManager.RegisterDevice();
                } while (deviceCredentials.UserName.Password.Contains(";")
                         || deviceCredentials.UserName.Password.Contains("=")
                         || deviceCredentials.UserName.Password.Contains(" ")
                         || deviceCredentials.UserName.UserName.Contains(";")
                         || deviceCredentials.UserName.UserName.Contains("=")
                         || deviceCredentials.UserName.UserName.Contains(" "));

                dbcb.Add("DeviceID", deviceCredentials.UserName.UserName);
                dbcb.Add("DevicePassword", deviceCredentials.UserName.Password);
            }

            if (UseIfd && !string.IsNullOrEmpty(HomeRealmUrl))
            {
                dbcb.Add("HomeRealmUri", HomeRealmUrl);
            }

            return dbcb.ToString();
        }

        private string GetOnlineRegion(string hostname)
        {
            var prefix = hostname.Split('.')[1];
            var region = string.Empty;
            switch (prefix)
            {
                case "crm":
                    region = "NorthAmerica";
                    break;

                case "crm2":
                    region = "SouthAmerica";
                    break;

                case "crm3":
                    region = "Canada";
                    break;

                case "crm4":
                    region = "EMEA";
                    break;

                case "crm5":
                    region = "APAC";
                    break;

                case "crm6":
                    region = "Oceania";
                    break;

                case "crm7":
                    region = "Japan";
                    break;

                case "crm8":
                    region = "India";
                    break;

                case "crm9":
                    region = "NorthAmerica2";
                    break;
            }

            return region;
        }

        private string GetOrganizationCrmConnectionString()
        {
            DbConnectionStringBuilder dbcb = new DbConnectionStringBuilder();
            //dbcb.Add("Url", OrganizationServiceUrl.Replace("/XRMServices/2011/Organization.svc", ""));
            dbcb.Add("Url", !string.IsNullOrEmpty(OriginalUrl) ? OriginalUrl : WebApplicationUrl);

            if (IsCustomAuth)
            {
                if (!UseIfd)
                {
                    if (!string.IsNullOrEmpty(UserDomain))
                    {
                        dbcb.Add("Domain", UserDomain);
                    }
                }

                string username = UserName;
                if (UseIfd)
                {
                    if (!string.IsNullOrEmpty(UserDomain))
                    {
                        username = string.Format("{0}\\{1}", UserDomain, UserName);
                    }
                }

                if (string.IsNullOrEmpty(userPassword))
                {
                    throw new Exception("User password cannot be null. If the user password is not stored in configuration file, you should request it from the end user");
                }

                var decryptedPassword = CryptoManager.Decrypt(userPassword, ConnectionManager.CryptoPassPhrase,
                   ConnectionManager.CryptoSaltValue,
                   ConnectionManager.CryptoHashAlgorythm,
                   ConnectionManager.CryptoPasswordIterations,
                   ConnectionManager.CryptoInitVector,
                   ConnectionManager.CryptoKeySize);

                dbcb.Add("Username", username);
                dbcb.Add("Password", decryptedPassword);
            }

            // Online CTP is deprecated
            //if (UseOnline)
            //{
            //    ClientCredentials deviceCredentials;

            //    do
            //    {
            //        deviceCredentials = DeviceIdManager.LoadDeviceCredentials() ??
            //                            DeviceIdManager.RegisterDevice();
            //    } while (deviceCredentials.UserName.Password.Contains(";")
            //             || deviceCredentials.UserName.Password.Contains("=")
            //             || deviceCredentials.UserName.Password.Contains(" ")
            //             || deviceCredentials.UserName.UserName.Contains(";")
            //             || deviceCredentials.UserName.UserName.Contains("=")
            //             || deviceCredentials.UserName.UserName.Contains(" "));

            //    dbcb.Add("DeviceID", deviceCredentials.UserName.UserName);
            //    dbcb.Add("DevicePassword", deviceCredentials.UserName.Password);
            //}

            if (UseIfd && !string.IsNullOrEmpty(HomeRealmUrl))
            {
                dbcb.Add("HomeRealmUri", HomeRealmUrl);
            }

            //append timeout in seconds to connectionstring
            dbcb.Add("Timeout", Timeout.ToString(@"hh\:mm\:ss"));

            dbcb.Add("AuthType", UseOsdp ? "Office365" : (UseIfd ? "IFD" : "AD"));

            return dbcb.ToString();
        }

        #endregion Méthodes

        public object Clone()
        {
            return new ConnectionDetail
            {
                AuthType = AuthType,
                ConnectionId = ConnectionId,
                ConnectionName = ConnectionName,
                CrmTicket = CrmTicket,
                HomeRealmUrl = HomeRealmUrl,
                IsCustomAuth = IsCustomAuth,
                Organization = Organization,
                OrganizationFriendlyName = OrganizationFriendlyName,
                OrganizationServiceUrl = OrganizationServiceUrl,
                OrganizationDataServiceUrl = OrganizationDataServiceUrl,
                OrganizationUrlName = OrganizationUrlName,
                OrganizationVersion = OrganizationVersion,
                SavePassword = SavePassword,
                ServerName = ServerName,
                ServerPort = ServerPort,
                TimeoutTicks = TimeoutTicks,
                UseIfd = UseIfd,
                UseOnline = UseOnline,
                UseOsdp = UseOsdp,
                UseSsl = UseSsl,
                UserDomain = UserDomain,
                UserName = UserName,
                userPassword = userPassword,
                WebApplicationUrl = WebApplicationUrl,
                OriginalUrl = OriginalUrl,
                Timeout = Timeout
            };
        }

        public void CopyPasswordTo(ConnectionDetail detail)
        {
            detail.userPassword = userPassword;
        }

        public bool IsConnectionBrokenWithUpdatedData(ConnectionDetail originalDetail)
        {
            if (originalDetail.HomeRealmUrl != HomeRealmUrl
               || originalDetail.IsCustomAuth != IsCustomAuth
               || originalDetail.Organization != Organization
               || originalDetail.ServerName.ToLower() != ServerName.ToLower()
               || originalDetail.ServerPort != ServerPort
               || originalDetail.UseIfd != UseIfd
               || originalDetail.UseOnline != UseOnline
               || originalDetail.UseOsdp != UseOsdp
               || originalDetail.UseSsl != UseSsl
               || originalDetail.UserDomain.ToLower() != UserDomain.ToLower()
               || originalDetail.UserName.ToLower() != UserName.ToLower()
               || (SavePassword && !string.IsNullOrEmpty(userPassword) && originalDetail.userPassword != userPassword))
            {
                return true;
            }

            return false;
        }

        public bool PasswordIsDifferent(string password)
        {
            return password != userPassword;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var detail = (ConnectionDetail)obj;

            return String.Compare(ConnectionName, detail.ConnectionName, StringComparison.Ordinal);
        }

        #endregion IComparable Members

        internal XElement GetXElement()
        {
            return new XElement("ConnectionDetail",
                    new XElement("AuthType", AuthType),
                    new XElement("ConnectionId", ConnectionId),
                    new XElement("ConnectionName", ConnectionName),
                    new XElement("IsCustomAuth", IsCustomAuth),
                    new XElement("UseIfd", UseIfd),
                    new XElement("UseOnline", UseOnline),
                    new XElement("UseOsdp", UseOsdp),
                    new XElement("UserDomain", UserDomain),
                    new XElement("UserName", UserName),
                    new XElement("UserPassword", SavePassword ? userPassword : string.Empty),
                    new XElement("SavePassword", SavePassword),
                    new XElement("UseSsl", UseSsl),
                    new XElement("ServerName", ServerName),
                    new XElement("ServerPort", ServerPort),
                    new XElement("OriginalUrl", OriginalUrl),
                    new XElement("Organization", Organization),
                    new XElement("OrganizationUrlName", OrganizationUrlName),
                    new XElement("OrganizationFriendlyName", OrganizationFriendlyName),
                    new XElement("OrganizationServiceUrl", OrganizationServiceUrl),
                    new XElement("OrganizationDataServiceUrl", OrganizationDataServiceUrl),
                    new XElement("OrganizationVersion", OrganizationVersion),
                    new XElement("HomeRealmUrl", HomeRealmUrl),
                    new XElement("Timeout", TimeoutTicks),
                    new XElement("WebApplicationUrl", WebApplicationUrl),
                    new XElement("LastUsedOn", LastUsedOn.ToString(CultureInfo.InvariantCulture.DateTimeFormat)),
                    GetCustomInfoXElement());
        }

        private XElement GetCustomInfoXElement()
        {
            if (CustomInformation == null)
            {
                return null;
            }
            return new XElement("CustomInformation", CustomInformation.Select(i => new XElement(i.Key, i.Value)));
        }
    }
}