using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;

namespace McTools.Xrm.Connection
{
    #region Event Args Class Definition

    public class ConnectionSucceedEventArgs : EventArgs
    {
        public IOrganizationService OrganizationService { get; set; }
        public ConnectionDetail ConnectionDetail { get; set; }
        public object Parameter { get; set; }
    }

    public class ConnectionFailedEventArgs : EventArgs
    {
        public string FailureReason { get; set; }
    }

    public class StepChangedEventArgs : EventArgs
    {
        public string CurrentStep { get; set; }
    }

    public class RequestPasswordEventArgs : EventArgs
    {
        public ConnectionDetail ConnectionDetail { get; set; }

        public RequestPasswordEventArgs(ConnectionDetail connectionDetail)
        {
            ConnectionDetail = connectionDetail;
        }
    }

    public class UseProxyEventArgs : EventArgs
    {
        public IWebProxy Proxy { get; set; }
    }

    public class EditConnectEventArgs : EventArgs
    {
    }

    public class DeleteConnectionEventArgs : EventArgs
    {
    }

    #endregion

    /// <summary>
    /// Manager that handles all connection operations
    /// </summary>
    public class ConnectionManager
    {
        #region Delegates

        public delegate void ConnectionSucceedEventHandler(object sender, ConnectionSucceedEventArgs e);
        public delegate void ConnectionFailedEventHandler(object sender, ConnectionFailedEventArgs e);
        public delegate void StepChangedEventHandler(object sender, StepChangedEventArgs e);
        public delegate bool RequestPasswordEventHandler(object sender, RequestPasswordEventArgs e);
        public delegate void UseProxyEventHandler(object sender, UseProxyEventArgs e);


        #endregion

        #region Event Handlers

        public event ConnectionSucceedEventHandler ConnectionSucceed;
        public event ConnectionFailedEventHandler ConnectionFailed;
        public event StepChangedEventHandler StepChanged;
        public event RequestPasswordEventHandler RequestPassword;
        public event UseProxyEventHandler UseProxy;

        #endregion

        #region Constants

        const string ConfigFileName = "mscrmtools2011.config";
        const string CryptoPassPhrase = "MsCrmTools";
        const string CryptoSaltValue = "Tanguy 92*";
        const string CryptoInitVector = "ahC3@bCa2Didfc3d";
        const string CryptoHashAlgorythm = "SHA1";
        const int CryptoPasswordIterations = 2;
        const int CryptoKeySize = 256;

        #endregion Constants

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class ConnectionManager
        /// </summary>
        public ConnectionManager()
        {
            ConnectionsList = LoadConnectionsList();

            ServicePointManager.ServerCertificateValidationCallback += ValidateRemoteCertificate;

        }
        // callback used to validate the certificate in an SSL conversation
        private static bool ValidateRemoteCertificate(
        object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors policyErrors
        )
        {
            return true;
        }
        #endregion

        #region Properties

        /// <summary>
        /// List of Crm connections
        /// </summary>
        public CrmConnections ConnectionsList { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Launch the Crm connection process 
        /// </summary>
        /// <param name="detail">Details of the Crm connection</param>
        /// <param name="connectionParameter">A parameter to retrieve after connection</param>
        public void ConnectToServer(ConnectionDetail detail, object connectionParameter)
        {
            var parameters = new List<object> { detail, connectionParameter };

            // Runs the connection asynchronously
            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.RunWorkerAsync(parameters);
        }

        /// <summary>
        /// Launch the Crm connection process 
        /// </summary>
        /// <param name="detail">Details of the Crm connection</param>
        public void ConnectToServer(ConnectionDetail detail)
        {
            ConnectToServer(detail, null);
        }

        /// <summary>
        /// Working process
        /// </summary>
        /// <param name="sender">BackgroundWorker object</param>
        /// <param name="e">BackgroundWorker object parameters</param>
        void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            object result = Connect((List<object>)e.Argument);
            e.Result = e.Argument;
            ((List<object>)e.Result).Add(result);
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var parameters = (List<object>)e.Result;

            if (parameters.Count == 3)
            {
                var error = parameters[2] as Exception;
                if (error != null)
                {
                    SendFailureMessage(CrmExceptionHelper.GetErrorMessage(error, false));
                }
                else
                {
                    var service = parameters[2] as IOrganizationService;
                    if (service != null)
                    {
                        SendSuccessMessage(service, parameters);
                    }
                }
            }
        }

        /// <summary>
        /// Connects to a Crm server
        /// </summary>
        /// <param name="parameters">List of parameters</param>
        /// <returns>An exception or an IOrganizationService</returns>
        private object Connect(List<object> parameters)
        {
            WebRequest.DefaultWebProxy = WebRequest.GetSystemWebProxy();

            var detail = (ConnectionDetail)parameters[0];
            SendStepChange("Creating Organization service proxy...");

            // Connecting to Crm server
            try
            {
                var connection = CrmConnection.Parse(detail.GetOrganizationCrmConnectionString());
                var service = new OrganizationService(connection);

                TestConnection(service);

                var vRequest = new RetrieveVersionRequest();
                var vResponse = (RetrieveVersionResponse) service.Execute(vRequest);

                detail.OrganizationVersion = vResponse.Version;

                var currentConnection = ConnectionsList.Connections.FirstOrDefault(x => x.ConnectionId == detail.ConnectionId);
                if (currentConnection != null)
                {
                    currentConnection.OrganizationVersion = vResponse.Version;
                    currentConnection.SavePassword = detail.SavePassword;
                    currentConnection.UserPassword = detail.UserPassword;
                }

                SaveConnectionsFile(ConnectionsList);

                return service;
            }
            catch (Exception error)
            {
                return error;
            }
        }

        /// <summary>
        /// Restore Crm connections list from the file
        /// </summary>
        /// <returns>List of Crm connections</returns>
        public CrmConnections LoadConnectionsList()
        {
            CrmConnections crmConnections;
            try
            {
                if (File.Exists(ConfigFileName))
                {
                    using (var configReader = new StreamReader(ConfigFileName))
                    {
                        crmConnections = (CrmConnections)XmlSerializerHelper.Deserialize(configReader.ReadToEnd(), typeof(CrmConnections));
                    }

                    if (!string.IsNullOrEmpty(crmConnections.Password))
                    {
                        crmConnections.Password = CryptoManager.Decrypt(crmConnections.Password,
                        CryptoPassPhrase,
                        CryptoSaltValue,
                        CryptoHashAlgorythm,
                        CryptoPasswordIterations,
                        CryptoInitVector,
                        CryptoKeySize);
                    }

                    foreach (var detail in crmConnections.Connections)
                    {
                        if (!string.IsNullOrEmpty(detail.UserPassword))
                        {
                            detail.UserPassword = CryptoManager.Decrypt(detail.UserPassword,
                                                                        CryptoPassPhrase,
                                                                        CryptoSaltValue,
                                                                        CryptoHashAlgorythm,
                                                                        CryptoPasswordIterations,
                                                                        CryptoInitVector,
                                                                        CryptoKeySize);
                        }

                        // Fix for new connection code
                        if (string.IsNullOrEmpty(detail.OrganizationUrlName))
                        {
                            if (detail.UseIfd || detail.UseOnline || detail.UseOsdp)
                            {
                                var uri = new Uri(detail.OrganizationServiceUrl);
                                detail.OrganizationUrlName = uri.Host.Split('.')[0];
                            }
                            else
                            {
                                detail.OrganizationUrlName = detail.Organization;
                            }
                        }

                        // Fix old connection for TimeOut
                        if (detail.Timeout == TimeSpan.Zero)
                        {
                            detail.Timeout = new TimeSpan(1200000000);
                        }
                    }
                }
                else
                {
                    crmConnections = new CrmConnections
                    {
                        Connections = new List<ConnectionDetail>()
                    };
                }

                return crmConnections;
            }
            catch (Exception error)
            {
                throw new Exception("Error while deserializing configuration file. Details: " + error.Message);
            }
        }

        /// <summary>
        /// Saves Crm connections list to file
        /// </summary>
        public void SaveConnectionsFile(CrmConnections connectionsList)
        {
            if (!string.IsNullOrEmpty(connectionsList.Password))
            {
                connectionsList.Password = CryptoManager.Encrypt(connectionsList.Password,
                    CryptoPassPhrase,
                    CryptoSaltValue,
                    CryptoHashAlgorythm,
                    CryptoPasswordIterations,
                    CryptoInitVector,
                    CryptoKeySize);
            }

            var cache = new Dictionary<Guid, string>();

            foreach (var detail in connectionsList.Connections)
            {
                if (!detail.ConnectionId.HasValue)
                    continue;

                cache.Add(detail.ConnectionId.Value, detail.UserPassword);

                if (detail.SavePassword)
                {
                    if (!string.IsNullOrEmpty(detail.UserPassword))
                    {

                        detail.UserPassword = CryptoManager.Encrypt(detail.UserPassword,
                                                                    CryptoPassPhrase,
                                                                    CryptoSaltValue,
                                                                    CryptoHashAlgorythm,
                                                                    CryptoPasswordIterations,
                                                                    CryptoInitVector,
                                                                    CryptoKeySize);
                    }
                }
                else
                {
                    detail.UserPassword = null;
                }
            }

            XmlSerializerHelper.SerializeToFile(connectionsList, ConfigFileName);

            foreach (var detail in connectionsList.Connections)
            {
                if (!detail.ConnectionId.HasValue)
                    continue;

                if (detail.UserPassword == null)
                {
                    detail.UserPassword = cache[detail.ConnectionId.Value];
                    continue;
                }

                if (!string.IsNullOrEmpty(detail.UserPassword))
                {
                    detail.UserPassword = CryptoManager.Decrypt(detail.UserPassword,
                                                                CryptoPassPhrase,
                                                                CryptoSaltValue,
                                                                CryptoHashAlgorythm,
                                                                CryptoPasswordIterations,
                                                                CryptoInitVector,
                                                                CryptoKeySize);
                }
            }
        }

        /// <summary>
        /// Tests the specified connection
        /// </summary>
        /// <param name="service">Organization service</param>
        public void TestConnection(IOrganizationService service)
        {
            try
            {
                SendStepChange("Testing connection...");

                var request = new WhoAmIRequest();
                service.Execute(request);
            }
            catch (Exception error)
            {
                throw new Exception("Test connection failed: " + CrmExceptionHelper.GetErrorMessage(error, false));
            }
        }

        #endregion

        #region Send Events

        /// <summary>
        /// Sends a connection success message 
        /// </summary>
        /// <param name="service">IOrganizationService generated</param>
        /// <param name="parameters">Lsit of parameter</param>
        private void SendSuccessMessage(IOrganizationService service, List<object> parameters)
        {
            if (ConnectionSucceed != null)
            {
                var args = new ConnectionSucceedEventArgs
                {
                    OrganizationService = service,
                    ConnectionDetail = (ConnectionDetail)parameters[0],
                    Parameter = parameters[1]
                };

                ConnectionSucceed(this, args);
            }
        }

        /// <summary>
        /// Sends a connection failure message
        /// </summary>
        /// <param name="failureReason">Reason of the failure</param>
        private void SendFailureMessage(string failureReason)
        {
            if (ConnectionFailed != null)
            {
                var args = new ConnectionFailedEventArgs
                {
                    FailureReason = failureReason
                };

                ConnectionFailed(this, args);
            }
        }

        /// <summary>
        /// Sends a step change message
        /// </summary>
        /// <param name="step">New step</param>
        private void SendStepChange(string step)
        {
            var args = new StepChangedEventArgs
            {
                CurrentStep = step
            };

            if (StepChanged != null)
            {
                StepChanged(this, args);
            }
        }

        #endregion
    }
}
