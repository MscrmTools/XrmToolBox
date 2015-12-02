using McTools.Xrm.Connection.WinForms.Properties;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace McTools.Xrm.Connection.WinForms
{
    public partial class Form1 : Form
    {
        private readonly List<string> visitedPath;

        private bool changesRequireConnectionTesting;
        private ConnectionDetail crmConnectionDetail;
        private string hostName;
        private string hostPort;
        private bool isIfd;
        private bool isOffice365;
        private bool isOnline;
        private string orga;
        private CrmServiceClient serviceClient;
        private ConnectionDetail updatedConnectionDetail;
        private bool useSsl;

        public Form1(ConnectionDetail detail = null)
        {
            InitializeComponent();

            crmConnectionDetail = detail;

            visitedPath = new List<string> { pnlConnectUrl.Name };

            if (detail != null)
            {
                txtOrganizationUrl.Text = detail.OriginalUrl;
                txtDomain.Text = detail.UserDomain;
                txtUsername.Text = detail.UserName;
                txtConnectionName.Text = detail.ConnectionName;
                chkSavePassword.Checked = detail.SavePassword;
                if (detail.PasswordIsEmpty || detail.SavePassword == false)
                {
                    txtPassword.PasswordChar = (char)0;
                    txtPassword.UseSystemPasswordChar = false;
                    txtPassword.Text = "Please specify the password";
                    txtPassword.ForeColor = Color.DarkGray;
                }
                else
                {
                    txtPassword.PasswordChar = '•';
                    txtPassword.UseSystemPasswordChar = true;
                    txtPassword.Text = "@@PASSWORD@@";
                    txtPassword.ForeColor = Color.Black;
                }

                txtHomeRealm.Text = detail.HomeRealmUrl;
                chkUseIntegratedAuthentication.Checked = !detail.IsCustomAuth;
                rbIfdYes.Checked = detail.UseIfd;

                updatedConnectionDetail = (ConnectionDetail)crmConnectionDetail.Clone();

                lblHeader.Text = "Edit connection";
            }
        }

        public ConnectionDetail CrmConnectionDetail { get { return crmConnectionDetail; } }

        private void btnBack_Click(object sender, EventArgs e)
        {
            visitedPath.Remove(visitedPath.Last());

            foreach (var ctrl in Controls)
            {
                var pnl = ctrl as Panel;
                if (pnl != null && pnl != pnlHeader)
                {
                    pnl.Visible = pnl.Name == visitedPath.Last();
                }
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            // Saving information to a connection detail
            if (crmConnectionDetail == null)
            {
                crmConnectionDetail = new ConnectionDetail();
            }

            if (serviceClient != null)
            {
                // This happens when the connection is created. When updating
                // a connection, the service client is not instanciated
                crmConnectionDetail.Organization = serviceClient.ConnectedOrgUniqueName;
                crmConnectionDetail.OrganizationFriendlyName = serviceClient.ConnectedOrgFriendlyName;
                crmConnectionDetail.OrganizationUrlName = serviceClient.ConnectedOrgUniqueName;
                crmConnectionDetail.OrganizationVersion = serviceClient.ConnectedOrgVersion.ToString();
                crmConnectionDetail.OrganizationDataServiceUrl = serviceClient.ConnectedOrgPublishedEndpoints[EndpointType.OrganizationDataService];
                crmConnectionDetail.OrganizationServiceUrl = serviceClient.ConnectedOrgPublishedEndpoints[EndpointType.OrganizationService];
            }

            if (crmConnectionDetail == null)
            {
                crmConnectionDetail = new ConnectionDetail();
            }
            FillConnectionDetailFromControls(crmConnectionDetail);

            crmConnectionDetail.ServiceClient = serviceClient;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            var url = txtOrganizationUrl.Text;

            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                MessageBox.Show(Resources.ConnectionWizard_InvalidUrl);
                return;
            }

            TimeSpan timeOut;
            if (!TimeSpan.TryParse(txtTimeout.Text, CultureInfo.InvariantCulture, out timeOut))
            {
                MessageBox.Show(this, Resources.ConnectionWizard_InvalidTimeoutValue, Resources.ConnectionWizard_WarningTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            useSsl = url.StartsWith("https");
            var urlWithoutProtocol = url.Remove(0, useSsl ? 8 : 7);
            var urlParts = urlWithoutProtocol.Split('/');
            var host = urlParts[0];
            var hostParts = host.Split(':');
            hostName = hostParts[0];
            hostPort = hostParts.Length == 2 ? hostParts[1] : null;
            orga = urlParts.Length > 1 && !urlParts[1].ToLower().StartsWith("main.aspx") ? urlParts[1] : null;
            txtDomain.Enabled = true;

            if (orga == null)
            {
                IPAddress ipa;
                if (!IPAddress.TryParse(hostName, out ipa))
                {
                    orga = hostName.Split('.')[0];
                }

                if (hostName.IndexOf("dynamics.com", StringComparison.Ordinal) > 0)
                {
                    // MFA test
                    //var t = new Task(() =>
                    //{
                    //    var svc1 = new CrmServiceClient("AuthType=OAuth;Username=user1@crm2016rtm.onmicrosoft.com; Password=pass@word1;Url=https://crm2016rtm.crm4.dynamics.com;AppId=7d770fd6-f087-40f0-b41f-e55bf556cdac;RedirectUri=http://www.xrmtoolbox.com;TokenCacheStorePath=c:\\temp;LoginPrompt=Always");
                    //    //var svc1 = new CrmServiceClient("AuthType=Office365;Username=tanguy@crm2016rtm.onmicrosoft.com; Password=Emeline18*;Url=https://crm2016rtm.crm4.dynamics.com;");
                    //});

                    //t.Start();

                    // return;

                    isOnline = true;
                    txtDomain.Enabled = false;

                    if (updatedConnectionDetail == null)
                    {
                        updatedConnectionDetail = new ConnectionDetail();
                    }
                    FillConnectionDetailFromControls(updatedConnectionDetail);

                    lblDescription.Text = Resources.ConnectionWizard_CredentialsHeaderDescription;
                    DisplayPanel(pnlConnectAuthentication);

                    if (txtDomain.Enabled)
                    {
                        txtDomain.Focus();
                    }
                    else
                    {
                        txtUsername.Focus();
                    }
                }
                else
                {
                    // IFD or AD??
                    // Requires additional information
                    visitedPath.Add(pnlConnectMoreActiveDirectoryInfo.Name);

                    if (updatedConnectionDetail == null)
                    {
                        updatedConnectionDetail = new ConnectionDetail();
                    }
                    FillConnectionDetailFromControls(updatedConnectionDetail);

                    lblDescription.Text = Resources.ConnectionWizard_IfdSelectionHeaderDescription;
                    DisplayPanel(pnlConnectMoreActiveDirectoryInfo);
                    rbIfdNo.Focus();
                }
            }
            else
            {
                if (chkUseIntegratedAuthentication.Checked)
                {
                    FillConnectionDetailFromControls(updatedConnectionDetail);
                    if (updatedConnectionDetail.IsConnectionBrokenWithUpdatedData(crmConnectionDetail))
                    {
                        lblDescription.Text = Resources.ConnectionWizard_ConnectingHeaderDescription;
                        DisplayPanel(pnlWaiting);
                        Connect();
                    }
                    else
                    {
                        lblDescription.Text = Resources.ConnectionWizard_SuccessHeaderDescription;
                        DisplayPanel(pnlConnected);
                        txtConnectionName.Focus();
                    }
                }
                else
                {
                    visitedPath.Add(pnlConnectAuthentication.Name);
                    lblDescription.Text = Resources.ConnectionWizard_CredentialsHeaderDescription;
                    DisplayPanel(pnlConnectAuthentication);
                    if (txtDomain.Enabled)
                    {
                        txtDomain.Focus();
                    }
                    else
                    {
                        txtUsername.Focus();
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            hostName = string.Empty;
            hostPort = string.Empty;
            orga = string.Empty;
            isIfd = false;
            isOnline = false;
            useSsl = false;
            txtOrganizationUrl.Text = string.Empty;
            txtHomeRealm.Text = string.Empty;
            txtDomain.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;

            visitedPath.Clear();
            visitedPath.Add(pnlConnectUrl.Name);

            lblDescription.Text = Resources.ConnectionWizard_EnterUrlHeaderDescription;
            txtOrganizationUrl.Focus();
            DisplayPanel(pnlConnectUrl);
        }

        private void btnValidaIfdInfo_Click(object sender, EventArgs e)
        {
            isIfd = rbIfdYes.Checked;

            if (orga == null || orga == hostName)
            {
                lblDescription.Text = Resources.ConnectionWizard_EnterUrlHeaderDescription;

                if (!isIfd)
                {
                    MessageBox.Show(this,
                        Resources.ConnectionWizard_UnableToDetermineOrganization,
                        Resources.ConnectionWizard_WarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtOrganizationUrl.Focus();
                    DisplayPanel(pnlConnectUrl);
                    return;
                }

                orga = hostName.Split('.')[0];

                if (orga == hostName)
                {
                    MessageBox.Show(this,
                        Resources.ConnectionWizard_InvalidIfUrl,
                        Resources.ConnectionWizard_WarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtOrganizationUrl.Focus();
                    DisplayPanel(pnlConnectUrl);
                    return;
                }
            }

            if (isIfd || !chkUseIntegratedAuthentication.Checked)
            {
                visitedPath.Add(pnlConnectAuthentication.Name);
                lblDescription.Text = Resources.ConnectionWizard_CredentialsHeaderDescription;
                if (txtDomain.Enabled)
                {
                    txtDomain.Focus();
                }
                else
                {
                    txtUsername.Focus();
                }

                DisplayPanel(pnlConnectAuthentication);
                pnlConnectUrl.Visible = false;
            }
            else
            {
                lblDescription.Text = Resources.ConnectionWizard_ConnectingHeaderDescription;
                DisplayPanel(pnlWaiting);

                Connect();
            }
        }

        private void chkUseIntegratedAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            btnGo.Text = chkUseIntegratedAuthentication.Checked ? "Connect" : "Go";
        }

        private void Connect()
        {
            visitedPath.Add(pnlWaiting.Name);

            var settings = new ConnectionSettings
            {
                Domain = txtDomain.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
            };

            if (settings.Password == "@@PASSWORD@@")
            {
            }

            var bw = new BackgroundWorker();
            bw.DoWork += (bwSender, evt) =>
            {
                var bwSettings = (ConnectionSettings)evt.Argument;
                CrmServiceClient crmSvc;

                if (isOnline)
                {
                    Task<CrmServiceClient>[] taskArray =
                {
                    Task<CrmServiceClient>.Factory.StartNew(() => ConnectOnline(true, bwSettings)),
                    Task<CrmServiceClient>.Factory.StartNew(() => ConnectOnline(false, bwSettings)),
                };

                    taskArray[0].Wait();
                    taskArray[1].Wait();

                    var goodResult = taskArray.FirstOrDefault(t => string.IsNullOrEmpty(t.Result.LastCrmError));
                    if (goodResult != null)
                    {
                        crmSvc = goodResult.Result;

                        isOffice365 = taskArray[0].Result.LastCrmError == null;
                    }
                    else
                    {
                        crmSvc = taskArray.First().Result;
                    }
                }
                else if (isIfd)
                {
                    crmSvc = new CrmServiceClient(
                       new NetworkCredential(bwSettings.Username, bwSettings.Password, bwSettings.Domain), AuthenticationType.IFD, hostName, hostPort,
                       orga, true, useSsl);
                }
                else
                {
                    if (orga == null)
                    {
                        MessageBox.Show(this, Resources.ConnectionWizard_UnableToDetermineOrganizationFromUrl, Resources.ConnectionWizard_WarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }

                    NetworkCredential credential;
                    if (chkUseIntegratedAuthentication.Checked)
                    {
                        credential = CredentialCache.DefaultNetworkCredentials;
                    }
                    else
                    {
                        credential = new NetworkCredential(bwSettings.Username, bwSettings.Password, bwSettings.Domain);
                    }
                    crmSvc = new CrmServiceClient(credential, AuthenticationType.AD, hostName, hostPort, orga, true, useSsl);
                }

                evt.Result = crmSvc;
            };
            bw.RunWorkerCompleted += (bwSender, evt) =>
            {
                if (evt.Error != null)
                {
                    lblDescription.Text = Resources.ConnectionWizard_ErrorHeaderDescription;

                    lblError.Text = evt.Error.Message;
                    DisplayPanel(pnlError);

                    return;
                }

                CrmServiceClient crmSvc = (CrmServiceClient)evt.Result;

                if (!string.IsNullOrEmpty(crmSvc.LastCrmError))
                {
                    lblDescription.Text = Resources.ConnectionWizard_ErrorHeaderDescription;

                    lblError.Text = crmSvc.LastCrmError;
                    DisplayPanel(pnlError);

                    return;
                }

                lblDescription.Text = Resources.ConnectionWizard_SuccessHeaderDescription;

                DisplayPanel(pnlConnected);
                txtConnectionName.Focus();

                serviceClient = crmSvc;
            };
            bw.RunWorkerAsync(settings);
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            // Check data if authentication panel is the current displayed one
            if (pnlConnectAuthentication.Visible)
            {
                if (string.IsNullOrEmpty(txtDomain.Text) && txtDomain.Enabled
                    || string.IsNullOrEmpty(txtUsername.Text)
                    || string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show(this, Resources.ConnectionWizard_PleaseEnterCredentials,
                        Resources.ConnectionWizard_WarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            }

            if (updatedConnectionDetail == null)
            {
                updatedConnectionDetail = new ConnectionDetail();
            }
            FillConnectionDetailFromControls(updatedConnectionDetail);
            if (crmConnectionDetail == null)
            {
                lblDescription.Text = Resources.ConnectionWizard_ConnectingHeaderDescription;
                DisplayPanel(pnlWaiting);

                Connect();
            }
            else if (updatedConnectionDetail.IsConnectionBrokenWithUpdatedData(crmConnectionDetail))
            {
                if (DialogResult.Yes == MessageBox.Show(this, Resources.ConnectionWizard_NeedToTestConnectionAgain,
                        Resources.ConnectionWizard_QuestionHeaderTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    lblDescription.Text = Resources.ConnectionWizard_ConnectingHeaderDescription;
                    DisplayPanel(pnlWaiting);

                    Connect();
                }
            }
            else
            {
                lblDescription.Text = Resources.ConnectionWizard_SuccessHeaderDescription;
                DisplayPanel(pnlConnected);
                txtConnectionName.Focus();
            }
        }

        private CrmServiceClient ConnectOnline(bool isOffice365, ConnectionSettings settings)
        {
            var securePassword = new SecureString();
            foreach (char c in settings.Password)
                securePassword.AppendChar(c);
            securePassword.MakeReadOnly();

            return new CrmServiceClient(settings.Username, securePassword, GetOnlineRegion(hostName), orga, true, useSsl, isOffice365: isOffice365);
        }

        private void DisplayPanel(Panel panel)
        {
            foreach (var ctrl in Controls)
            {
                var pnl = ctrl as Panel;
                if (pnl != null && pnl != pnlHeader)
                {
                    pnl.Visible = pnl == panel;
                }
            }
        }

        private void FillConnectionDetailFromControls(ConnectionDetail detail)
        {
            detail.ConnectionName = txtConnectionName.Text;
            detail.OriginalUrl = txtOrganizationUrl.Text;
            detail.UserDomain = txtDomain.Text;
            detail.UserName = txtUsername.Text;
            detail.SavePassword = chkSavePassword.Checked;
            detail.IsCustomAuth = !chkUseIntegratedAuthentication.Checked;
            detail.UseIfd = rbIfdYes.Checked;
            detail.ConnectionId = Guid.NewGuid();
            detail.UseOsdp = isOffice365;
            detail.UseOnline = isOnline;
            detail.UseSsl = txtOrganizationUrl.Text.ToLower().StartsWith("https");
            detail.ServerName = hostName;
            detail.Timeout = TimeSpan.Parse(txtTimeout.Text);

            if (txtPassword.Text != "@@PASSWORD@@" && txtPassword.Text != "Please specify the password")
            {
                detail.SetPassword(txtPassword.Text);
            }

            if (string.IsNullOrEmpty(hostPort))
            {
                if (useSsl)
                {
                    detail.ServerPort = 443;
                }
                else
                {
                    detail.ServerPort = 80;
                }
            }
            else
            {
                detail.ServerPort = int.Parse(hostPort);
            }

            if (isOnline)
            {
                detail.AuthType = isOffice365 ? AuthenticationProviderType.OnlineFederation : AuthenticationProviderType.LiveId;
            }
            else
            {
                detail.AuthType = isIfd ? AuthenticationProviderType.Federation : AuthenticationProviderType.ActiveDirectory;
            }
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

                case "crm9":
                    region = "NorthAmerica2";
                    break;
            }

            return region;
        }

        private void rbIfdYes_CheckedChanged(object sender, EventArgs e)
        {
            txtHomeRealm.Enabled = rbIfdYes.Checked;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFinish_Click(null, null);
            }
        }

        private void txtOrganizationUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGo_Click(null, null);
            }
        }
    }
}