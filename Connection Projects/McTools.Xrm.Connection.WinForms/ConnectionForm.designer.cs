namespace McTools.Xrm.Connection.WinForms
{
    partial class ConnectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelName = new System.Windows.Forms.Label();
            this.bValidate = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.comboBoxOrganizations = new System.Windows.Forms.ComboBox();
            this.cbUseSsl = new System.Windows.Forms.CheckBox();
            this.rbAuthenticationIntegrated = new System.Windows.Forms.RadioButton();
            this.gbAuthentication = new System.Windows.Forms.GroupBox();
            this.chkSavePassword = new System.Windows.Forms.CheckBox();
            this.tbUserPassword = new System.Windows.Forms.TextBox();
            this.tbUserLogin = new System.Windows.Forms.TextBox();
            this.tbUserDomain = new System.Windows.Forms.TextBox();
            this.labelUserPassword = new System.Windows.Forms.Label();
            this.labelUserLogin = new System.Windows.Forms.Label();
            this.labelUserDomain = new System.Windows.Forms.Label();
            this.rbAuthenticationCustom = new System.Windows.Forms.RadioButton();
            this.gbServer = new System.Windows.Forms.GroupBox();
            this.tbServerUrl = new System.Windows.Forms.TextBox();
            this.lblServerUrl = new System.Windows.Forms.Label();
            this.cbbOnlineEnv = new System.Windows.Forms.ComboBox();
            this.tbHomeRealmUrl = new System.Windows.Forms.TextBox();
            this.lblHomeRealmUrl = new System.Windows.Forms.Label();
            this.cbUseOSDP = new System.Windows.Forms.CheckBox();
            this.cbUseOnline = new System.Windows.Forms.CheckBox();
            this.tbServerPort = new System.Windows.Forms.TextBox();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.tbServerName = new System.Windows.Forms.TextBox();
            this.labelServerName = new System.Windows.Forms.Label();
            this.cbUseIfd = new System.Windows.Forms.CheckBox();
            this.bGetOrganizations = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.gbOrgs = new System.Windows.Forms.GroupBox();
            this.tbTimeoutValue = new System.Windows.Forms.TextBox();
            this.lblTimeoutValue = new System.Windows.Forms.Label();
            this.gbAuthentication.SuspendLayout();
            this.gbServer.SuspendLayout();
            this.gbOrgs.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 15);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(90, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Connection name";
            // 
            // bValidate
            // 
            this.bValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bValidate.Location = new System.Drawing.Point(238, 441);
            this.bValidate.Name = "bValidate";
            this.bValidate.Size = new System.Drawing.Size(75, 23);
            this.bValidate.TabIndex = 7;
            this.bValidate.Text = "OK";
            this.bValidate.UseVisualStyleBackColor = true;
            this.bValidate.Click += new System.EventHandler(this.BValidateClick);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(108, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(283, 20);
            this.tbName.TabIndex = 1;
            // 
            // comboBoxOrganizations
            // 
            this.comboBoxOrganizations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOrganizations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrganizations.Enabled = false;
            this.comboBoxOrganizations.FormattingEnabled = true;
            this.comboBoxOrganizations.Location = new System.Drawing.Point(6, 19);
            this.comboBoxOrganizations.Name = "comboBoxOrganizations";
            this.comboBoxOrganizations.Size = new System.Drawing.Size(283, 21);
            this.comboBoxOrganizations.Sorted = true;
            this.comboBoxOrganizations.TabIndex = 0;
            this.comboBoxOrganizations.SelectedIndexChanged += new System.EventHandler(this.ComboBoxOrganizationsSelectedIndexChanged);
            this.comboBoxOrganizations.TextChanged += new System.EventHandler(this.ComboBoxOrganizationsTextChanged);
            // 
            // cbUseSsl
            // 
            this.cbUseSsl.AutoSize = true;
            this.cbUseSsl.Location = new System.Drawing.Point(9, 45);
            this.cbUseSsl.Name = "cbUseSsl";
            this.cbUseSsl.Size = new System.Drawing.Size(68, 17);
            this.cbUseSsl.TabIndex = 2;
            this.cbUseSsl.Text = "Use SSL";
            this.cbUseSsl.UseVisualStyleBackColor = true;
            this.cbUseSsl.CheckedChanged += new System.EventHandler(this.cbUseSsl_CheckedChanged);
            // 
            // rbAuthenticationIntegrated
            // 
            this.rbAuthenticationIntegrated.AutoSize = true;
            this.rbAuthenticationIntegrated.Checked = true;
            this.rbAuthenticationIntegrated.Location = new System.Drawing.Point(6, 19);
            this.rbAuthenticationIntegrated.Name = "rbAuthenticationIntegrated";
            this.rbAuthenticationIntegrated.Size = new System.Drawing.Size(191, 17);
            this.rbAuthenticationIntegrated.TabIndex = 0;
            this.rbAuthenticationIntegrated.TabStop = true;
            this.rbAuthenticationIntegrated.Text = "Windows Integrated Authentication";
            this.rbAuthenticationIntegrated.UseVisualStyleBackColor = true;
            this.rbAuthenticationIntegrated.CheckedChanged += new System.EventHandler(this.RbAuthenticationIntegratedCheckedChanged);
            // 
            // gbAuthentication
            // 
            this.gbAuthentication.Controls.Add(this.chkSavePassword);
            this.gbAuthentication.Controls.Add(this.tbUserPassword);
            this.gbAuthentication.Controls.Add(this.tbUserLogin);
            this.gbAuthentication.Controls.Add(this.tbUserDomain);
            this.gbAuthentication.Controls.Add(this.labelUserPassword);
            this.gbAuthentication.Controls.Add(this.labelUserLogin);
            this.gbAuthentication.Controls.Add(this.labelUserDomain);
            this.gbAuthentication.Controls.Add(this.rbAuthenticationCustom);
            this.gbAuthentication.Controls.Add(this.rbAuthenticationIntegrated);
            this.gbAuthentication.Location = new System.Drawing.Point(15, 192);
            this.gbAuthentication.Name = "gbAuthentication";
            this.gbAuthentication.Size = new System.Drawing.Size(379, 151);
            this.gbAuthentication.TabIndex = 3;
            this.gbAuthentication.TabStop = false;
            this.gbAuthentication.Text = "Authentication";
            // 
            // chkSavePassword
            // 
            this.chkSavePassword.AutoSize = true;
            this.chkSavePassword.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSavePassword.Location = new System.Drawing.Point(6, 120);
            this.chkSavePassword.Name = "chkSavePassword";
            this.chkSavePassword.Size = new System.Drawing.Size(344, 17);
            this.chkSavePassword.TabIndex = 8;
            this.chkSavePassword.Text = "Save password in configuration file (the password will be encrypted)";
            this.chkSavePassword.UseVisualStyleBackColor = true;
            // 
            // tbUserPassword
            // 
            this.tbUserPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUserPassword.Enabled = false;
            this.tbUserPassword.Location = new System.Drawing.Point(65, 94);
            this.tbUserPassword.Name = "tbUserPassword";
            this.tbUserPassword.PasswordChar = '*';
            this.tbUserPassword.Size = new System.Drawing.Size(308, 20);
            this.tbUserPassword.TabIndex = 7;
            this.tbUserPassword.Enter += new System.EventHandler(this.tbUserPassword_Enter);
            // 
            // tbUserLogin
            // 
            this.tbUserLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUserLogin.Enabled = false;
            this.tbUserLogin.Location = new System.Drawing.Point(65, 68);
            this.tbUserLogin.Name = "tbUserLogin";
            this.tbUserLogin.Size = new System.Drawing.Size(308, 20);
            this.tbUserLogin.TabIndex = 5;
            // 
            // tbUserDomain
            // 
            this.tbUserDomain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUserDomain.Enabled = false;
            this.tbUserDomain.Location = new System.Drawing.Point(65, 42);
            this.tbUserDomain.Name = "tbUserDomain";
            this.tbUserDomain.Size = new System.Drawing.Size(308, 20);
            this.tbUserDomain.TabIndex = 3;
            // 
            // labelUserPassword
            // 
            this.labelUserPassword.AutoSize = true;
            this.labelUserPassword.Location = new System.Drawing.Point(6, 97);
            this.labelUserPassword.Name = "labelUserPassword";
            this.labelUserPassword.Size = new System.Drawing.Size(53, 13);
            this.labelUserPassword.TabIndex = 6;
            this.labelUserPassword.Text = "Password";
            // 
            // labelUserLogin
            // 
            this.labelUserLogin.AutoSize = true;
            this.labelUserLogin.Location = new System.Drawing.Point(6, 71);
            this.labelUserLogin.Name = "labelUserLogin";
            this.labelUserLogin.Size = new System.Drawing.Size(33, 13);
            this.labelUserLogin.TabIndex = 4;
            this.labelUserLogin.Text = "Login";
            // 
            // labelUserDomain
            // 
            this.labelUserDomain.AutoSize = true;
            this.labelUserDomain.Location = new System.Drawing.Point(6, 45);
            this.labelUserDomain.Name = "labelUserDomain";
            this.labelUserDomain.Size = new System.Drawing.Size(43, 13);
            this.labelUserDomain.TabIndex = 2;
            this.labelUserDomain.Text = "Domain";
            // 
            // rbAuthenticationCustom
            // 
            this.rbAuthenticationCustom.AutoSize = true;
            this.rbAuthenticationCustom.Location = new System.Drawing.Point(203, 19);
            this.rbAuthenticationCustom.Name = "rbAuthenticationCustom";
            this.rbAuthenticationCustom.Size = new System.Drawing.Size(131, 17);
            this.rbAuthenticationCustom.TabIndex = 1;
            this.rbAuthenticationCustom.Text = "Custom Authentication";
            this.rbAuthenticationCustom.UseVisualStyleBackColor = true;
            // 
            // gbServer
            // 
            this.gbServer.Controls.Add(this.tbServerUrl);
            this.gbServer.Controls.Add(this.lblServerUrl);
            this.gbServer.Controls.Add(this.cbbOnlineEnv);
            this.gbServer.Controls.Add(this.tbHomeRealmUrl);
            this.gbServer.Controls.Add(this.lblHomeRealmUrl);
            this.gbServer.Controls.Add(this.cbUseOSDP);
            this.gbServer.Controls.Add(this.cbUseOnline);
            this.gbServer.Controls.Add(this.tbServerPort);
            this.gbServer.Controls.Add(this.labelServerPort);
            this.gbServer.Controls.Add(this.tbServerName);
            this.gbServer.Controls.Add(this.labelServerName);
            this.gbServer.Controls.Add(this.cbUseIfd);
            this.gbServer.Controls.Add(this.cbUseSsl);
            this.gbServer.Location = new System.Drawing.Point(12, 38);
            this.gbServer.Name = "gbServer";
            this.gbServer.Size = new System.Drawing.Size(379, 148);
            this.gbServer.TabIndex = 2;
            this.gbServer.TabStop = false;
            this.gbServer.Text = "Server Information";
            // 
            // tbServerUrl
            // 
            this.tbServerUrl.Location = new System.Drawing.Point(83, 18);
            this.tbServerUrl.Name = "tbServerUrl";
            this.tbServerUrl.Size = new System.Drawing.Size(290, 20);
            this.tbServerUrl.TabIndex = 1;
            this.tbServerUrl.Leave += new System.EventHandler(this.TbServerUrlLeave);
            // 
            // lblServerUrl
            // 
            this.lblServerUrl.AutoSize = true;
            this.lblServerUrl.Location = new System.Drawing.Point(6, 21);
            this.lblServerUrl.Name = "lblServerUrl";
            this.lblServerUrl.Size = new System.Drawing.Size(79, 13);
            this.lblServerUrl.TabIndex = 0;
            this.lblServerUrl.Text = "Discovery URL";
            // 
            // cbbOnlineEnv
            // 
            this.cbbOnlineEnv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbOnlineEnv.FormattingEnabled = true;
            this.cbbOnlineEnv.Items.AddRange(new object[] {
            "crm.dynamics.com",
            "crm2.dynamics.com",
            "crm4.dynamics.com",
            "crm5.dynamics.com",
            "crm6.dynamics.com",
            "crm7.dynamics.com",
            "crm9.dynamics.com"});
            this.cbbOnlineEnv.Location = new System.Drawing.Point(83, 68);
            this.cbbOnlineEnv.Name = "cbbOnlineEnv";
            this.cbbOnlineEnv.Size = new System.Drawing.Size(290, 21);
            this.cbbOnlineEnv.TabIndex = 7;
            this.cbbOnlineEnv.Visible = false;
            // 
            // tbHomeRealmUrl
            // 
            this.tbHomeRealmUrl.Enabled = false;
            this.tbHomeRealmUrl.Location = new System.Drawing.Point(83, 120);
            this.tbHomeRealmUrl.Name = "tbHomeRealmUrl";
            this.tbHomeRealmUrl.Size = new System.Drawing.Size(290, 20);
            this.tbHomeRealmUrl.TabIndex = 11;
            // 
            // lblHomeRealmUrl
            // 
            this.lblHomeRealmUrl.AutoSize = true;
            this.lblHomeRealmUrl.Location = new System.Drawing.Point(6, 123);
            this.lblHomeRealmUrl.Name = "lblHomeRealmUrl";
            this.lblHomeRealmUrl.Size = new System.Drawing.Size(77, 13);
            this.lblHomeRealmUrl.TabIndex = 10;
            this.lblHomeRealmUrl.Text = "Home realm url";
            // 
            // cbUseOSDP
            // 
            this.cbUseOSDP.AutoSize = true;
            this.cbUseOSDP.Location = new System.Drawing.Point(265, 45);
            this.cbUseOSDP.Name = "cbUseOSDP";
            this.cbUseOSDP.Size = new System.Drawing.Size(97, 17);
            this.cbUseOSDP.TabIndex = 5;
            this.cbUseOSDP.Text = "Use Office 365";
            this.cbUseOSDP.UseVisualStyleBackColor = true;
            this.cbUseOSDP.CheckedChanged += new System.EventHandler(this.CbUseOsdpCheckedChanged);
            // 
            // cbUseOnline
            // 
            this.cbUseOnline.AutoSize = true;
            this.cbUseOnline.Location = new System.Drawing.Point(154, 45);
            this.cbUseOnline.Name = "cbUseOnline";
            this.cbUseOnline.Size = new System.Drawing.Size(105, 17);
            this.cbUseOnline.TabIndex = 4;
            this.cbUseOnline.Text = "Use CRM Online";
            this.cbUseOnline.UseVisualStyleBackColor = true;
            this.cbUseOnline.CheckedChanged += new System.EventHandler(this.CbUseOnlineCheckedChanged);
            // 
            // tbServerPort
            // 
            this.tbServerPort.Location = new System.Drawing.Point(83, 94);
            this.tbServerPort.Name = "tbServerPort";
            this.tbServerPort.Size = new System.Drawing.Size(290, 20);
            this.tbServerPort.TabIndex = 9;
            this.tbServerPort.Text = "80";
            // 
            // labelServerPort
            // 
            this.labelServerPort.AutoSize = true;
            this.labelServerPort.Location = new System.Drawing.Point(6, 97);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(60, 13);
            this.labelServerPort.TabIndex = 8;
            this.labelServerPort.Text = "Server Port";
            // 
            // tbServerName
            // 
            this.tbServerName.Location = new System.Drawing.Point(83, 68);
            this.tbServerName.Name = "tbServerName";
            this.tbServerName.Size = new System.Drawing.Size(290, 20);
            this.tbServerName.TabIndex = 7;
            // 
            // labelServerName
            // 
            this.labelServerName.AutoSize = true;
            this.labelServerName.Location = new System.Drawing.Point(6, 71);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Size = new System.Drawing.Size(69, 13);
            this.labelServerName.TabIndex = 6;
            this.labelServerName.Text = "Server Name";
            // 
            // cbUseIfd
            // 
            this.cbUseIfd.AutoSize = true;
            this.cbUseIfd.Location = new System.Drawing.Point(83, 45);
            this.cbUseIfd.Name = "cbUseIfd";
            this.cbUseIfd.Size = new System.Drawing.Size(65, 17);
            this.cbUseIfd.TabIndex = 3;
            this.cbUseIfd.Text = "Use IFD";
            this.cbUseIfd.UseVisualStyleBackColor = true;
            this.cbUseIfd.CheckedChanged += new System.EventHandler(this.CbUseIfdCheckedChanged);
            // 
            // bGetOrganizations
            // 
            this.bGetOrganizations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bGetOrganizations.Location = new System.Drawing.Point(295, 19);
            this.bGetOrganizations.Name = "bGetOrganizations";
            this.bGetOrganizations.Size = new System.Drawing.Size(75, 23);
            this.bGetOrganizations.TabIndex = 1;
            this.bGetOrganizations.Text = "Get Orgs.";
            this.bGetOrganizations.UseVisualStyleBackColor = true;
            this.bGetOrganizations.Click += new System.EventHandler(this.BGetOrganizationsClick);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(319, 441);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 8;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.BCancelClick);
            // 
            // gbOrgs
            // 
            this.gbOrgs.Controls.Add(this.bGetOrganizations);
            this.gbOrgs.Controls.Add(this.comboBoxOrganizations);
            this.gbOrgs.Location = new System.Drawing.Point(15, 349);
            this.gbOrgs.Name = "gbOrgs";
            this.gbOrgs.Size = new System.Drawing.Size(379, 52);
            this.gbOrgs.TabIndex = 4;
            this.gbOrgs.TabStop = false;
            this.gbOrgs.Text = "Organization";
            // 
            // tbTimeoutValue
            // 
            this.tbTimeoutValue.Location = new System.Drawing.Point(95, 409);
            this.tbTimeoutValue.Name = "tbTimeoutValue";
            this.tbTimeoutValue.Size = new System.Drawing.Size(65, 20);
            this.tbTimeoutValue.TabIndex = 6;
            this.tbTimeoutValue.Text = "00:02:00";
            // 
            // lblTimeoutValue
            // 
            this.lblTimeoutValue.AutoSize = true;
            this.lblTimeoutValue.Location = new System.Drawing.Point(18, 411);
            this.lblTimeoutValue.Name = "lblTimeoutValue";
            this.lblTimeoutValue.Size = new System.Drawing.Size(74, 13);
            this.lblTimeoutValue.TabIndex = 5;
            this.lblTimeoutValue.Text = "Timeout value";
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(403, 470);
            this.Controls.Add(this.tbTimeoutValue);
            this.Controls.Add(this.lblTimeoutValue);
            this.Controls.Add(this.gbOrgs);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.gbServer);
            this.Controls.Add(this.gbAuthentication);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.bValidate);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConnectionForm";
            this.Text = "Connection dialog";
            this.gbAuthentication.ResumeLayout(false);
            this.gbAuthentication.PerformLayout();
            this.gbServer.ResumeLayout(false);
            this.gbServer.PerformLayout();
            this.gbOrgs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button bValidate;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.ComboBox comboBoxOrganizations;
        private System.Windows.Forms.CheckBox cbUseSsl;
        private System.Windows.Forms.RadioButton rbAuthenticationIntegrated;
        private System.Windows.Forms.GroupBox gbAuthentication;
        private System.Windows.Forms.RadioButton rbAuthenticationCustom;
        private System.Windows.Forms.TextBox tbUserPassword;
        private System.Windows.Forms.TextBox tbUserLogin;
        private System.Windows.Forms.TextBox tbUserDomain;
        private System.Windows.Forms.Label labelUserPassword;
        private System.Windows.Forms.Label labelUserLogin;
        private System.Windows.Forms.Label labelUserDomain;
        private System.Windows.Forms.GroupBox gbServer;
        private System.Windows.Forms.TextBox tbServerPort;
        private System.Windows.Forms.Label labelServerPort;
        private System.Windows.Forms.TextBox tbServerName;
        private System.Windows.Forms.Label labelServerName;
        private System.Windows.Forms.CheckBox cbUseIfd;
        private System.Windows.Forms.Button bGetOrganizations;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.CheckBox cbUseOnline;
        private System.Windows.Forms.GroupBox gbOrgs;
        private System.Windows.Forms.CheckBox cbUseOSDP;
        private System.Windows.Forms.TextBox tbHomeRealmUrl;
        private System.Windows.Forms.Label lblHomeRealmUrl;
        private System.Windows.Forms.CheckBox chkSavePassword;
        private System.Windows.Forms.ComboBox cbbOnlineEnv;
        private System.Windows.Forms.TextBox tbTimeoutValue;
        private System.Windows.Forms.Label lblTimeoutValue;
        private System.Windows.Forms.TextBox tbServerUrl;
        private System.Windows.Forms.Label lblServerUrl;
    }
}