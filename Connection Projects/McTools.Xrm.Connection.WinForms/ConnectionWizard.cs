using McTools.Xrm.Connection.WinForms.Properties;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace McTools.Xrm.Connection.WinForms
{
    public partial class ConnectionWizard : Form
    {
        private const string SpecifyPasswordText = "Please specify the password";
        private readonly ConnectionDetail originalDetail;
        private readonly List<string> visitedPath;
        private CrmServiceClient serviceClient;
        private ConnectionDetail updatedDetail;

        public ConnectionWizard(ConnectionDetail detail = null)
        {
            InitializeComponent();

            originalDetail = (ConnectionDetail)detail?.Clone();
            updatedDetail = new ConnectionDetail(true);

            visitedPath = new List<string> { pnlConnectUrl.Name };

            if (detail != null)
            {
                txtOrganizationUrl.Text = string.IsNullOrEmpty(detail.OriginalUrl) ? detail.WebApplicationUrl : detail.OriginalUrl;
                txtDomain.Text = detail.UserDomain;
                txtUsername.Text = detail.UserName;
                txtConnectionName.Text = detail.ConnectionName;
                chkSavePassword.Checked = detail.SavePassword;
                if (detail.PasswordIsEmpty || detail.SavePassword == false)
                {
                    txtPassword.PasswordChar = (char)0;
                    txtPassword.UseSystemPasswordChar = false;
                    txtPassword.Text = SpecifyPasswordText;
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

                updatedDetail = (ConnectionDetail)originalDetail.Clone();

                lblHeader.Text = "Edit connection";
            }
        }

        public ConnectionDetail CrmConnectionDetail { get { return updatedDetail; } }

        private void btnBack_Click(object sender, EventArgs e)
        {
            visitedPath.Remove(visitedPath.Last());

            if (visitedPath.Count == 0)
            {
                visitedPath.Add(pnlConnectUrl.Name);
                DisplayPanel(pnlConnectUrl);
                return;
            }

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
            if (serviceClient != null)
            {
                // This happens when the connection is created. When updating
                // a connection, the service client is not instanciated
                updatedDetail.Organization = serviceClient.ConnectedOrgUniqueName;
                updatedDetail.OrganizationFriendlyName = serviceClient.ConnectedOrgFriendlyName;
                updatedDetail.OrganizationUrlName = serviceClient.ConnectedOrgUniqueName;
                updatedDetail.OrganizationVersion = serviceClient.ConnectedOrgVersion.ToString();
                updatedDetail.OrganizationDataServiceUrl = serviceClient.ConnectedOrgPublishedEndpoints[EndpointType.OrganizationDataService];
                updatedDetail.OrganizationServiceUrl = serviceClient.ConnectedOrgPublishedEndpoints[EndpointType.OrganizationService];
            }

            updatedDetail.ConnectionName = txtConnectionName.Text;
            updatedDetail.ServiceClient = serviceClient;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            Uri uri;
            if (!Uri.TryCreate(txtOrganizationUrl.Text, UriKind.Absolute, out uri))
            {
                MessageBox.Show(Resources.ConnectionWizard_InvalidUrl);
                return;
            }

            txtOrganizationUrl.Text = txtOrganizationUrl.Text.Trim();
            txtOrganizationUrl.Text = txtOrganizationUrl.Text.EndsWith("/")
                ? txtOrganizationUrl.Text.Remove(txtOrganizationUrl.Text.Length - 1, 1)
                : txtOrganizationUrl.Text;

            updatedDetail.OriginalUrl = txtOrganizationUrl.Text.ToLower();

            TimeSpan timeOut;
            if (!TimeSpan.TryParse(txtTimeout.Text, CultureInfo.InvariantCulture, out timeOut))
            {
                MessageBox.Show(this, Resources.ConnectionWizard_InvalidTimeoutValue, Resources.ConnectionWizard_WarningTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            updatedDetail.Timeout = timeOut;
            updatedDetail.UseSsl = updatedDetail.OriginalUrl.StartsWith("https");

            var urlWithoutProtocol = updatedDetail.OriginalUrl.Remove(0, updatedDetail.UseSsl ? 8 : 7);
            var urlParts = urlWithoutProtocol.Split('/');
            var host = urlParts[0];
            var hostParts = host.Split(':');

            updatedDetail.ServerName = hostParts[0];
            updatedDetail.ServerPort = hostParts.Length == 2 ? int.Parse(hostParts[1]) : new int?();
            updatedDetail.OrganizationUrlName = urlParts.Length > 1 && !urlParts[1].ToLower().StartsWith("main.aspx") ? urlParts[1] : null;
            updatedDetail.IsCustomAuth = !chkUseIntegratedAuthentication.Checked;

            txtDomain.Enabled = true;

            if (updatedDetail.OrganizationUrlName == null)
            {
                IPAddress ipa;
                if (!IPAddress.TryParse(updatedDetail.ServerName, out ipa))
                {
                    //orga = hostName.Split('.')[0];
                    updatedDetail.OrganizationUrlName = updatedDetail.ServerName.Split('.')[0];
                }

                if (updatedDetail.ServerName.IndexOf("dynamics.com", StringComparison.Ordinal) > 0)
                {
                    // MFA test
                    //var t = new Task(() =>
                    //{
                    //    var svc1 = new CrmServiceClient("AuthType=OAuth;Username=user1@crm2016rtm.onmicrosoft.com; Password=pass@word1;Url=https://crm2016rtm.crm4.dynamics.com;AppId=7d770fd6-f087-40f0-b41f-e55bf556cdac;RedirectUri=http://www.xrmtoolbox.com;TokenCacheStorePath=c:\\temp;LoginPrompt=Always");
                    //    //var svc1 = new CrmServiceClient("AuthType=Office365;Username=tanguy@crm2016rtm.onmicrosoft.com; Password=Emeline18*;Url=https://crm2016rtm.crm4.dynamics.com;");
                    //});

                    //t.Start();

                    // return;

                    updatedDetail.UseOnline = true;
                    updatedDetail.UseOsdp = true;

                    txtDomain.Enabled = false;
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

                    lblDescription.Text = Resources.ConnectionWizard_IfdSelectionHeaderDescription;
                    DisplayPanel(pnlConnectMoreActiveDirectoryInfo);
                    rbIfdNo.Focus();
                }
            }
            else
            {
                if (chkUseIntegratedAuthentication.Checked)
                {
                    if (updatedDetail.IsConnectionBrokenWithUpdatedData(originalDetail))
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
            updatedDetail = new ConnectionDetail();
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
            updatedDetail.UseIfd = rbIfdYes.Checked;

            if (updatedDetail.OrganizationUrlName == null || updatedDetail.OrganizationUrlName == updatedDetail.ServerName)
            {
                lblDescription.Text = Resources.ConnectionWizard_EnterUrlHeaderDescription;

                if (!updatedDetail.UseIfd)
                {
                    MessageBox.Show(this,
                        Resources.ConnectionWizard_UnableToDetermineOrganization,
                        Resources.ConnectionWizard_WarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtOrganizationUrl.Focus();
                    DisplayPanel(pnlConnectUrl);
                    return;
                }

                updatedDetail.OrganizationUrlName = updatedDetail.ServerName.Split('.')[0];

                if (updatedDetail.OrganizationUrlName == updatedDetail.ServerName)
                {
                    MessageBox.Show(this,
                        Resources.ConnectionWizard_InvalidIfUrl,
                        Resources.ConnectionWizard_WarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtOrganizationUrl.Focus();
                    DisplayPanel(pnlConnectUrl);
                    return;
                }
            }

            if (updatedDetail.UseIfd || updatedDetail.IsCustomAuth)
            {
                visitedPath.Add(pnlConnectAuthentication.Name);
                lblDescription.Text = Resources.ConnectionWizard_CredentialsHeaderDescription;
                txtDomain.Enabled = !updatedDetail.UseIfd;
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

            var bw = new BackgroundWorker();
            bw.DoWork += (bwSender, evt) =>
            {
                var detail = (ConnectionDetail)evt.Argument;
                evt.Result = detail.GetCrmServiceClient(true);
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

                if (!crmSvc.IsReady)
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
            bw.RunWorkerAsync(updatedDetail);
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

            updatedDetail.UserDomain = txtDomain.Text;
            updatedDetail.UserName = txtUsername.Text;
            updatedDetail.SavePassword = chkSavePassword.Checked;
            updatedDetail.SetPassword(txtPassword.Text);

            if (originalDetail == null)
            {
                lblDescription.Text = Resources.ConnectionWizard_ConnectingHeaderDescription;
                DisplayPanel(pnlWaiting);

                Connect();
            }
            else if (updatedDetail.IsConnectionBrokenWithUpdatedData(originalDetail))
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

        //private CrmServiceClient ConnectOnline(bool isOffice365, ConnectionSettings settings)
        //{
        //    var securePassword = new SecureString();
        //    foreach (char c in settings.Password)
        //        securePassword.AppendChar(c);
        //    securePassword.MakeReadOnly();

        //    return new CrmServiceClient(settings.Username, securePassword, GetOnlineRegion(hostName), orga, true, useSsl, isOffice365: isOffice365);
        //}

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

        //private void FillConnectionDetailFromControls(ConnectionDetail detail)
        //{
        //    detail.ConnectionName = txtConnectionName.Text;
        //    detail.OriginalUrl = txtOrganizationUrl.Text;
        //    detail.UserDomain = txtDomain.Text;
        //    detail.UserName = txtUsername.Text;
        //    detail.SavePassword = chkSavePassword.Checked;
        //    detail.IsCustomAuth = !chkUseIntegratedAuthentication.Checked;
        //    detail.UseIfd = rbIfdYes.Checked;
        //    detail.ConnectionId = Guid.NewGuid();
        //    detail.UseOsdp = isOffice365 || (originalDetail != null && originalDetail.UseOsdp);
        //    detail.UseOnline = isOnline;
        //    detail.UseSsl = txtOrganizationUrl.Text.ToLower().StartsWith("https");
        //    detail.ServerName = hostName;
        //    detail.Timeout = TimeSpan.Parse(txtTimeout.Text);

        //    if (txtPassword.Text != "@@PASSWORD@@" && txtPassword.Text != SpecifyPasswordText)
        //    {
        //        detail.SetPassword(txtPassword.Text);
        //    }

        //    if (string.IsNullOrEmpty(hostPort))
        //    {
        //        if (useSsl)
        //        {
        //            detail.ServerPort = 443;
        //        }
        //        else
        //        {
        //            detail.ServerPort = 80;
        //        }
        //    }
        //    else
        //    {
        //        detail.ServerPort = int.Parse(hostPort);
        //    }

        //    if (isOnline)
        //    {
        //        detail.AuthType = detail.UseOsdp ? AuthenticationProviderType.OnlineFederation : AuthenticationProviderType.LiveId;
        //    }
        //    else
        //    {
        //        detail.AuthType = isIfd ? AuthenticationProviderType.Federation : AuthenticationProviderType.ActiveDirectory;
        //    }
        //}

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

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.ForeColor == Color.DarkGray && txtPassword.Text == SpecifyPasswordText)
            {
                txtPassword.Text = string.Empty;
                txtPassword.ForeColor = Color.Black;
                txtPassword.PasswordChar = '•';
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}