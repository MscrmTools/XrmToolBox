using System.Collections.Generic;

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
    }
}