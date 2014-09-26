using System;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Client;

namespace McTools.Xrm.Connection
{
    /// <summary>
    /// Stores data regarding a specific connection to Crm server
    /// </summary>
    public class ConnectionDetail : IComparable
    {
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
        [System.Xml.Serialization.XmlIgnore]
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
        /// Get or set the user password
        /// </summary>
        //[System.Xml.Serialization.XmlIgnore]
        public string UserPassword { get; set; }

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
        public string ServerPort { get; set; }

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

        public int OrganizationMajorVersion {get
        {
            return OrganizationVersion != null ? int.Parse(OrganizationVersion.Split('.')[0]) : -1;
        }}

        public int OrganizationMinorVersion
        {
            get
            {
                return OrganizationVersion != null ? int.Parse(OrganizationVersion.Split('.')[1]) : -1;
            }
        }

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

        public string GetDiscoveryCrmConnectionString()
        {
            var connectionString = string.Format("Url={0}://{1}:{2};",
                UseSsl ? "https" : "http",
                UseIfd ? ServerName : UseOsdp ? "disco." + ServerName : UseOnline ? "dev." + ServerName : ServerName,
                ServerPort.Length == 0 ? (UseSsl ? 443 : 80) : int.Parse(ServerPort));

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

                connectionString += string.Format("Username={0};Password={1};", username, UserPassword);
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

        public string GetOrganizationCrmConnectionString()
        {
            var currentServerName = string.Empty;

            if (UseOsdp || UseOnline)
            {
                currentServerName = string.Format("{0}.{1}", OrganizationUrlName, ServerName);
            }
            else if (UseIfd)
            {
                var serverNameParts = ServerName.Split('.');
                
                serverNameParts[0] = OrganizationUrlName;
                

                currentServerName = string.Format("{0}:{1}",
                                                  string.Join(".", serverNameParts),
                                                  ServerPort.Length == 0 ? (UseSsl ? 443 : 80) : int.Parse(ServerPort));
            }
            else
            {
                currentServerName = string.Format("{0}:{1}/{2}",
                                                  ServerName,
                                                  ServerPort.Length == 0 ? (UseSsl ? 443 : 80) : int.Parse(ServerPort),
                                                  Organization);
            }

            //var connectionString = string.Format("Url={0}://{1};",
            //                                     UseSsl ? "https" : "http",
            //                                     currentServerName);

            var connectionString = string.Format("Url={0};", OrganizationServiceUrl.Replace("/XRMServices/2011/Organization.svc",""));

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

                connectionString += string.Format("Username={0};Password={1};", username, UserPassword);
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

            return connectionString;
        }


        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return String.CompareOrdinal(ConnectionName, ((ConnectionDetail)obj).ConnectionName);
        }

        #endregion
    }
}