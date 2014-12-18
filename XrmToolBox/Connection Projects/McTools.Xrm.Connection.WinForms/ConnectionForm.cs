using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;

namespace McTools.Xrm.Connection.WinForms
{
    /// <summary>
    /// Formulaire de création et d'édition d'une connexion
    /// à un serveur Crm.
    /// </summary>
    public partial class ConnectionForm : Form
    {
        #region Variables

        /// <summary>
        /// Détail de connexion courant
        /// </summary>
        ConnectionDetail detail;

        /// <summary>
        /// List of Crm server organizations
        /// </summary>
        List<OrganizationDetail> organizations;

        /// <summary>
        /// Indique si l'utilisateur a demandé à se connecter
        /// au serveur
        /// </summary>
        bool doConnect;

        private bool proposeToConnect;
        public CrmConnections ConnectionList { get; set; }

        #endregion

        #region Propriétés

        /// <summary>
        /// Obtient la valeur qui définit si l'utilisateur a demandé 
        /// à se connecter au serveur
        /// </summary>
        public bool DoConnect
        {
            get { return doConnect; }
        }

        /// <summary>
        /// Obtient ou définit le détail de la connexion courante
        /// </summary>
        public ConnectionDetail CrmConnectionDetail
        {
            get { return detail; }
            set { detail = value; }
        }

        readonly bool isCreationMode;

        #endregion

        #region Constructeur

        /// <summary>
        /// Créé une nouvelle instance de la classe ConnectionForm
        /// </summary>
        public ConnectionForm(bool isCreation, bool proposeToConnect = true)
        {
            InitializeComponent();
            isCreationMode = isCreation;
            this.proposeToConnect = proposeToConnect;
            cbbOnlineEnv.SelectedIndex = 0;

            var tip = new ToolTip { ToolTipTitle = "Information" };
            tip.SetToolTip(tbServerName, "For CRM Online or Office 365, use:\r\ncrm.dynamics.com for North America\r\ncrm2.dynamics.com for LATAM\r\ncrm4.dynamics.com for EMEA\r\ncrm5.dynamics.com for Asia Pacific\r\n\r\nFor OnPremise:\r\nUse the server name\r\n\r\nFor IFD:\r\nUse <discovery_name>.<domain>.<extension>");
            tip.SetToolTip(tbServerPort, "Specify port only if different from 80 or 443 (SSL)");
            tip.SetToolTip(tbHomeRealmUrl, "(Optional) In specific case, you should need to specify the home realm url to authenticate through ADFS");
            // allows for validation of SSL conversations
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

        #region Méthodes

        protected override void OnLoad(EventArgs e)
        {
            if (detail != null)
            {
                FillValues();
            }

            if (proposeToConnect == false && isCreationMode == false)
            {
                bValidate.Enabled = true;
            }

            base.OnLoad(e);
        }

        private void BValidateClick(object sender, EventArgs e)
        {
            if (tbName.Text.Length == 0)
            {
                MessageBox.Show(this, "You must define a name for this connection!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (proposeToConnect && comboBoxOrganizations.Text.Length == 0 && comboBoxOrganizations.SelectedItem == null &&
                !(cbUseIfd.Checked || cbUseOSDP.Checked))
            {
                MessageBox.Show(this, "You must select an organization!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (tbUserPassword.Text.Length == 0 && (cbUseIfd.Checked || rbAuthenticationCustom.Checked))
            {
                MessageBox.Show(this, "You must define a password!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (detail == null)
                detail = new ConnectionDetail();

            // Save connection details in structure
            detail.ConnectionName = tbName.Text;
            detail.IsCustomAuth = rbAuthenticationCustom.Checked;
            detail.UseSsl = cbUseSsl.Checked;
            detail.UseOnline = cbUseOnline.Checked;
            detail.UseOsdp = cbUseOSDP.Checked;
            detail.ServerName = (cbUseOSDP.Checked || cbUseOnline.Checked)
                ? cbbOnlineEnv.SelectedItem.ToString()
                : tbServerName.Text;
            detail.ServerPort = tbServerPort.Text;
            detail.UserDomain = tbUserDomain.Text;
            detail.UserName = tbUserLogin.Text;
            detail.UserPassword = tbUserPassword.Text;
            detail.SavePassword = chkSavePassword.Checked;
            detail.UseIfd = cbUseIfd.Checked;
            detail.HomeRealmUrl = (tbHomeRealmUrl.Text.Length > 0 ? tbHomeRealmUrl.Text : null);

            TimeSpan timeOut;
            if (!TimeSpan.TryParse(tbTimeoutValue.Text, CultureInfo.InvariantCulture, out timeOut))
            {
                MessageBox.Show(this, "Wrong timeout value!\r\n\r\nExpected format : HH:mm:ss", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            detail.Timeout = timeOut;

            OrganizationDetail selectedOrganization = comboBoxOrganizations.SelectedItem != null
                ? ((Organization) comboBoxOrganizations.SelectedItem).OrganizationDetail
                : null;
            if (selectedOrganization != null)
            {
                detail.OrganizationServiceUrl = selectedOrganization.Endpoints[EndpointType.OrganizationService];
                detail.Organization = selectedOrganization.UniqueName;
                detail.OrganizationUrlName = selectedOrganization.UrlName;
                detail.OrganizationFriendlyName = selectedOrganization.FriendlyName;
                detail.OrganizationVersion = selectedOrganization.OrganizationVersion;
            }

            try
            {
                if (proposeToConnect || isCreationMode)
                {
                    FillDetails();

                    if (proposeToConnect &&
                        MessageBox.Show(this, "Do you want to connect now to this server?", "Question",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        doConnect = true;
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                if (detail.OrganizationServiceUrl != null && detail.OrganizationServiceUrl.IndexOf(detail.ServerName, StringComparison.Ordinal) < 0)
                {
                    var uri = new Uri(detail.OrganizationServiceUrl);
                    var hostName = uri.Host;

                    const string format = "The server name you provided ({0}) is not the same as the one defined in deployment manager ({1}). Please make sure that the server name defined in deployment manager is reachable from you computer.\r\n\r\nError:\r\n{2}";
                    MessageBox.Show(this, string.Format(format, detail.ServerName, hostName,error.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(this, error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ComboBoxOrganizationsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOrganizations.Text.Length > 0 || comboBoxOrganizations.SelectedItem != null)
            {
                bValidate.Enabled = true;
            }
            else
            {
                bValidate.Enabled = false;
            }
        }

        private void ComboBoxOrganizationsTextChanged(object sender, EventArgs e)
        {
            if (comboBoxOrganizations.Text.Length > 0 || comboBoxOrganizations.SelectedItem != null)
            {
                bValidate.Enabled = true;
            }
            else
            {
                bValidate.Enabled = false;
            }
        }

        private void CbUseIfdCheckedChanged(object sender, EventArgs e)
        {
            if (cbUseIfd.Checked)
            {
                cbUseOnline.Checked = false;
                cbUseOSDP.Checked = false;
            }

            bValidate.Enabled = cbUseIfd.Checked;

            rbAuthenticationCustom.Checked = cbUseIfd.Checked;
            rbAuthenticationIntegrated.Enabled = !cbUseIfd.Checked;
            rbAuthenticationIntegrated.Checked = !cbUseIfd.Checked;

            tbUserDomain.Enabled = cbUseIfd.Checked;
            tbUserLogin.Enabled = cbUseIfd.Checked;
            tbUserPassword.Enabled = cbUseIfd.Checked;

            tbServerName.Visible = !cbUseOnline.Checked;
            cbbOnlineEnv.Visible = cbUseOnline.Checked;
            tbHomeRealmUrl.Enabled = cbUseIfd.Checked;

            cbUseSsl.Checked = cbUseIfd.Checked;
            cbUseSsl.Enabled = !cbUseIfd.Checked;
        }

        private void CbUseOnlineCheckedChanged(object sender, EventArgs e)
        {
            if (cbUseOnline.Checked)
            {
                cbUseIfd.Checked = false;
                cbUseOSDP.Checked = true;

                rbAuthenticationCustom.Checked = true;
                rbAuthenticationIntegrated.Enabled = false;
                rbAuthenticationIntegrated.Checked = false;

                tbUserDomain.Text = string.Empty;

                tbUserDomain.Enabled = false;
                tbUserLogin.Enabled = true;
                tbUserPassword.Enabled = true;

                tbServerName.Visible = !cbUseOnline.Checked;
                cbbOnlineEnv.Visible = cbUseOnline.Checked;
                tbServerPort.Text = string.Empty;
                tbHomeRealmUrl.Text = string.Empty;

                cbUseSsl.Checked = true;
                cbUseSsl.Enabled = false;
                tbServerPort.Enabled = false;
                tbHomeRealmUrl.Enabled = false;
            }
            else
            {
                rbAuthenticationCustom.Checked = false;
                rbAuthenticationIntegrated.Enabled = true;
                rbAuthenticationIntegrated.Checked = true;

                tbUserDomain.Enabled = false;
                tbUserLogin.Enabled = false;
                tbUserPassword.Enabled = false;

                cbUseSsl.Checked = false;
                cbUseSsl.Enabled = true;
                cbUseOSDP.Checked = false;
                tbServerPort.Enabled = true;

                tbServerName.Visible = true;
                cbbOnlineEnv.Visible = false;
            }
        }

        private void CbUseOsdpCheckedChanged(object sender, EventArgs e)
        {
            if (cbUseOSDP.Checked)
            {
                cbUseIfd.Checked = false;
                cbUseOnline.Checked = true;
                //cbUseOnline.Enabled = false;

                rbAuthenticationCustom.Checked = true;
                rbAuthenticationIntegrated.Enabled = false;
                rbAuthenticationIntegrated.Checked = false;

                tbUserDomain.Text = string.Empty;

                tbUserDomain.Enabled = false;
                tbUserLogin.Enabled = true;
                tbUserPassword.Enabled = true;

                tbServerName.Visible = !cbUseOnline.Checked;
                cbbOnlineEnv.Visible = cbUseOnline.Checked;
                tbServerPort.Text = string.Empty;
                tbHomeRealmUrl.Text = string.Empty;

                cbUseSsl.Checked = true;
                cbUseSsl.Enabled = false;
                tbServerPort.Enabled = false;
                tbHomeRealmUrl.Enabled = false;
            }
            else
            {
                rbAuthenticationCustom.Checked = cbUseOnline.Checked;
                rbAuthenticationIntegrated.Enabled = !cbUseOnline.Checked;
                //cbUseOnline.Enabled = true;

                tbUserDomain.Enabled = false;
                tbUserLogin.Enabled = cbUseOnline.Checked;
                tbUserPassword.Enabled = cbUseOnline.Checked;

                cbUseSsl.Checked = cbUseOnline.Checked;
                cbUseSsl.Enabled = !cbUseOnline.Checked;
                tbServerPort.Enabled = !cbUseOnline.Checked;
            }
        }

        private void RbAuthenticationIntegratedCheckedChanged(object sender, EventArgs e)
        {
            if (rbAuthenticationIntegrated.Checked)
            {
                tbUserDomain.Text = string.Empty;
                tbUserLogin.Text = string.Empty;
                tbUserPassword.Text = string.Empty;
            }

            tbUserDomain.Enabled = rbAuthenticationCustom.Checked;
            tbUserLogin.Enabled = rbAuthenticationCustom.Checked;
            tbUserPassword.Enabled = rbAuthenticationCustom.Checked;
        }

        private void BGetOrganizationsClick(object sender, EventArgs e)
        {
            string warningMessage = string.Empty;
            bool goodAuthenticationData = false;
            bool goodServerData = false;

            // Check data filled by user
            if (rbAuthenticationIntegrated.Checked ||
                (
                rbAuthenticationCustom.Checked &&
                (tbUserDomain.Text.Length > 0 || cbUseIfd.Checked || cbUseOSDP.Checked) &&
                tbUserLogin.Text.Length > 0 &&
                tbUserPassword.Text.Length > 0
                )
                ||
                    (cbUseOnline.Checked && !string.IsNullOrEmpty(tbUserLogin.Text) &&
                    !string.IsNullOrEmpty(tbUserPassword.Text)))
                goodAuthenticationData = true;

            if (tbServerName.Text.Length > 0 || cbbOnlineEnv.SelectedIndex >= 0)
                goodServerData = true;

            if (!goodServerData)
            {
                warningMessage += "Please provide server name\r\n";
            }
            if (!goodAuthenticationData)
            {
                warningMessage += "Please fill all authentication textboxes\r\n";
            }

            if (warningMessage.Length > 0)
            {
                MessageBox.Show(this, warningMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Save connection details in structure
                if (isCreationMode)
                {
                    detail = new ConnectionDetail();
                }

                detail.ConnectionName = tbName.Text;
                detail.IsCustomAuth = rbAuthenticationCustom.Checked;
                detail.UseSsl = cbUseSsl.Checked;
                detail.ServerName = (cbUseOSDP.Checked || cbUseOnline.Checked) ? cbbOnlineEnv.SelectedItem.ToString() : tbServerName.Text;
                detail.ServerPort = tbServerPort.Text;
                detail.UserDomain = tbUserDomain.Text;
                detail.UserName = tbUserLogin.Text;
                detail.UserPassword = tbUserPassword.Text;
                detail.UseIfd = cbUseIfd.Checked;
                detail.UseOnline = cbUseOnline.Checked;
                detail.UseOsdp = cbUseOSDP.Checked;
                detail.HomeRealmUrl = (tbHomeRealmUrl.Text.Length > 0 ? tbHomeRealmUrl.Text : null);

                detail.AuthType = AuthenticationProviderType.ActiveDirectory;
                if (cbUseIfd.Checked)
                {
                    detail.AuthType = AuthenticationProviderType.Federation;
                }
                else if (cbUseOSDP.Checked)
                {
                    detail.AuthType = AuthenticationProviderType.OnlineFederation;
                }
                else if (cbUseOnline.Checked)
                {
                    detail.AuthType = AuthenticationProviderType.LiveId;
                }

                // Launch organization retrieval
                comboBoxOrganizations.Items.Clear();
                organizations = new List<OrganizationDetail>();
                Cursor = Cursors.WaitCursor;

                var bw = new BackgroundWorker();
                bw.DoWork += BwDoWork;
                bw.RunWorkerCompleted += BwRunWorkerCompleted;
                bw.RunWorkerAsync();
            }
        }

        void BwDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = RetrieveOrganizations(detail);
        }

        void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured while retrieving organizations: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (OrganizationDetail orgDetail in (OrganizationDetailCollection)e.Result)
                {
                    organizations.Add(orgDetail);

                    comboBoxOrganizations.Items.Add(new Organization { OrganizationDetail = orgDetail });
                    comboBoxOrganizations.SelectedIndex = 0;
                    comboBoxOrganizations.Enabled = true;
                }
            }

            Cursor = Cursors.Default;
        }

        private OrganizationDetailCollection RetrieveOrganizations(ConnectionDetail currentDetail)
        {
            WebRequest.GetSystemWebProxy();

            var connection = CrmConnection.Parse(currentDetail.GetDiscoveryCrmConnectionString());
            var service = new DiscoveryService(connection);

            var request = new RetrieveOrganizationsRequest();
            var response = (RetrieveOrganizationsResponse)service.Execute(request);
            return response.Details;
        }

        /// <summary>
        /// Remplit les contrôles du formulaire avec les données
        /// du détail de connexion courant
        /// </summary>
        private void FillValues()
        {
            rbAuthenticationCustom.Checked = detail.IsCustomAuth;
            rbAuthenticationIntegrated.Checked = !detail.IsCustomAuth;

            //rbAuthenticationIntegrated.CheckedChanged += new EventHandler(rbAuthenticationIntegrated_CheckedChanged);

            tbName.Text = detail.ConnectionName;
            tbServerPort.Text = detail.ServerPort;
            tbUserDomain.Text = detail.UserDomain;
            tbUserLogin.Text = detail.UserName;
            tbUserPassword.Text = detail.UserPassword;
            chkSavePassword.Checked = detail.SavePassword;
            comboBoxOrganizations.Text = detail.OrganizationFriendlyName;
            cbUseIfd.Checked = detail.UseIfd;
            cbUseOSDP.Checked = detail.UseOsdp;
            cbUseOnline.Checked = detail.UseOnline;
            cbUseSsl.Checked = detail.UseSsl;
            tbHomeRealmUrl.Text = detail.HomeRealmUrl;
            tbTimeoutValue.Text = detail.Timeout.ToString(@"hh\:mm\:ss");

            tbHomeRealmUrl.Enabled = detail.UseIfd;

            if (detail.UseOnline || detail.UseOsdp)
            {
                tbServerName.Visible = false;
                cbbOnlineEnv.Visible = true;

                cbbOnlineEnv.SelectedItem = detail.ServerName;
            }
            else
            {
                tbServerName.Visible = true;
                cbbOnlineEnv.Visible = false;

                tbServerName.Text = detail.ServerName;
            }
        }

        /// <summary>
        /// Remplit le détail de connexion avec le contenu 
        /// des contrôles du formulaire
        /// </summary>
        /// <returns></returns>
        private void FillDetails()
        {
            bool hasFoundOrg = false;

            OrganizationDetail selectedOrganization = comboBoxOrganizations.SelectedItem != null ? ((Organization)comboBoxOrganizations.SelectedItem).OrganizationDetail : null;

            if (organizations == null || organizations.Count == 0)
            {
                var orgs = RetrieveOrganizations(detail);
                foreach (OrganizationDetail orgDetail in orgs)
                {
                    if (organizations == null)
                        organizations = new List<OrganizationDetail>();

                    organizations.Add(orgDetail);

                    comboBoxOrganizations.Items.Add(new Organization { OrganizationDetail = orgDetail });
                    comboBoxOrganizations.SelectedIndex = 0;
                    comboBoxOrganizations.Enabled = true;
                }
            }

            if (organizations == null)
            {
                MessageBox.Show(this, "Organizations list is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (OrganizationDetail organization in organizations)
            {
                if (organization.UniqueName == selectedOrganization.UniqueName)
                {
                    detail.OrganizationServiceUrl = organization.Endpoints[EndpointType.OrganizationService];
                    detail.Organization = organization.UniqueName;
                    detail.OrganizationUrlName = organization.UrlName;
                    detail.OrganizationFriendlyName = organization.FriendlyName;
                    detail.OrganizationVersion = organization.OrganizationVersion;

                    detail.ConnectionName = tbName.Text;

                    if (isCreationMode)
                    {
                        detail.ConnectionId = Guid.NewGuid();
                    }

                    hasFoundOrg = true;

                    break;
                }
            }

            if (!hasFoundOrg)
            {
                throw new Exception("Unable to match selected organization with list of organizations in this deployment");
            }
        }

        #endregion
    }
}