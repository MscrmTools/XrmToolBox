using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;

namespace McTools.Xrm.Connection
{
    /// <summary>
    /// Stores data regarding a specific connection to Crm server
    /// </summary>
    public class ConnectionDetail : IComparable, ICloneable
    {
        private string userPassword;
        
        #region Propriétés
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
        /// Get or set flag to know if custom authentication
        /// </summary>
        public bool IsCustomAuth { get; set; }

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
        /// Get or set the Crm Ticket
        /// </summary>
        public string CrmTicket { get; set; }

        /// <summary>
        /// Get or set the user domain name
        /// </summary>
        public string UserDomain { get; set; }

        /// <summary>
        /// Get or set user login
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets an information if the password is empty
        /// </summary>
        public bool PasswordIsEmpty { get { return string.IsNullOrEmpty(userPassword); } }

        /// <summary>
        /// Gets or sets the information if the password must be saved
        /// </summary>
        public bool SavePassword { get; set; }

        /// <summary>
        /// Get or set the use of SSL connection
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Get or set the server name
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Get or set the server port
        /// </summary>
        [DefaultValue(80)]
        public int ServerPort { get; set; }

        /// <summary>
        /// Get or set the organization name
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Get or set the organization name
        /// </summary>
        public string OrganizationUrlName { get; set; }

        /// <summary>
        /// Get or set the organization friendly name
        /// </summary>
        public string OrganizationFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the Crm Service Url
        /// </summary>
        public string OrganizationServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the Home realm url for ADFS authentication
        /// </summary>
        public string HomeRealmUrl { get; set; }

        public string OrganizationVersion { get; set; }

        public TimeSpan Timeout { get; set; }

        public long TimeoutTicks
        {
            get { return Timeout.Ticks; }
            set { Timeout = new TimeSpan(value); }
        }

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

        public string WebApplicationUrl { get; set; }

        public DateTime LastUsedOn { get; set; }

        #endregion

        #region Méthodes

        /// <summary>
        /// Retourne le nom de la connexion
        /// </summary>
        /// <returns>Nom de la connexion</returns>
        public override string ToString()
        {
            return ConnectionName;
        }

        private string GetDiscoveryCrmConnectionString()
        {
            var connectionString = string.Format("Url={0}://{1}:{2};",
                UseSsl ? "https" : "http",
                UseIfd ? ServerName : UseOsdp ? "disco." + ServerName : UseOnline ? "dev." + ServerName : ServerName,
                ServerPort == 0 ? (UseSsl ? 443 : 80) : ServerPort);

            if (IsCustomAuth)
            {
                if (!UseIfd)
                {
                    if (!string.IsNullOrEmpty(UserDomain))
                    {
                        connectionString += string.Format("Domain={0};", UserDomain);
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

                connectionString += string.Format("Username={0};Password={1};", username, decryptedPassword);
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

                connectionString += string.Format("DeviceID={0};DevicePassword={1};",
                                                  deviceCredentials.UserName.UserName,
                                                  deviceCredentials.UserName.Password);
            }

            if (UseIfd && !string.IsNullOrEmpty(HomeRealmUrl))
            {
                connectionString += string.Format("HomeRealmUri={0};", HomeRealmUrl);
            }

            return connectionString;
        }

        private string GetOrganizationCrmConnectionString()
        {
            var connectionString = string.Format("Url={0};", OrganizationServiceUrl.Replace("/XRMServices/2011/Organization.svc", ""));

            if (IsCustomAuth)
            {
                if (!UseIfd)
                {
                    if (!string.IsNullOrEmpty(UserDomain))
                    {
                        connectionString += string.Format("Domain={0};", UserDomain);
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

                connectionString += string.Format("Username={0};Password={1};", username, decryptedPassword);
            }

            if (UseOnline)
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

                connectionString += string.Format("DeviceID={0};DevicePassword={1};",
                                                  deviceCredentials.UserName.UserName,
                                                  deviceCredentials.UserName.Password);
            }

            if (UseIfd && !string.IsNullOrEmpty(HomeRealmUrl))
            {
                connectionString += string.Format("HomeRealmUri={0};", HomeRealmUrl);
            }

            //append timeout in seconds to connectionstring
            connectionString += string.Format("Timeout={0};", Timeout.ToString(@"hh\:mm\:ss"));
            return connectionString;
        }

        public void UpdateAfterEdit(ConnectionDetail editedConnection)
        {
            ConnectionName = editedConnection.ConnectionName;
            OrganizationServiceUrl = editedConnection.OrganizationServiceUrl;
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

        public IOrganizationService GetOrganizationService()
        {
            return new OrganizationService(CrmConnection.Parse(GetOrganizationCrmConnectionString()));
        }

        public IDiscoveryService GetDiscoveryService()
        {
            return new DiscoveryService(CrmConnection.Parse(GetDiscoveryCrmConnectionString()));
        }

        public void ErasePassword()
        {
            userPassword = null;
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

        #endregion

        public bool IsConnectionBrokenWithUpdatedData(ConnectionDetail updatedDetail)
        {
            if (updatedDetail.AuthType != AuthType
               || updatedDetail.CrmTicket != CrmTicket
               || updatedDetail.HomeRealmUrl != HomeRealmUrl
               || updatedDetail.IsCustomAuth != IsCustomAuth
               || updatedDetail.Organization != Organization
               || updatedDetail.OrganizationFriendlyName != OrganizationFriendlyName
               || updatedDetail.OrganizationServiceUrl != OrganizationServiceUrl
               || updatedDetail.OrganizationUrlName != OrganizationUrlName
               || updatedDetail.ServerName != ServerName
               || updatedDetail.ServerPort != ServerPort
               || updatedDetail.UseIfd != UseIfd
               || updatedDetail.UseOnline != UseOnline
               || updatedDetail.UseOsdp != UseOsdp
               || updatedDetail.UseSsl != UseSsl
               || updatedDetail.UserDomain != UserDomain
               || updatedDetail.UserName != UserName
               || (SavePassword && updatedDetail.userPassword != userPassword)
               || (!SavePassword && !string.IsNullOrEmpty(updatedDetail.userPassword) && updatedDetail.userPassword != userPassword))
            {
                return true;
            }

            return false;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var detail = (ConnectionDetail) obj;

            return String.Compare(ConnectionName, detail.ConnectionName, StringComparison.Ordinal);
        }

        #endregion

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
                WebApplicationUrl = WebApplicationUrl
            };
        }

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
                    new XElement("Organization", Organization),
                    new XElement("OrganizationUrlName", OrganizationUrlName),
                    new XElement("OrganizationFriendlyName", OrganizationFriendlyName),
                    new XElement("OrganizationServiceUrl", OrganizationServiceUrl),
                    new XElement("OrganizationVersion", OrganizationVersion),
                    new XElement("Timeout", TimeoutTicks),
                    new XElement("WebApplicationUrl", WebApplicationUrl),
                    new XElement("LastUsedOn", LastUsedOn.ToString(CultureInfo.InvariantCulture.DateTimeFormat)));
        }

        public void CopyPasswordTo(ConnectionDetail detail)
        {
            detail.userPassword = userPassword;
        }
    }
}