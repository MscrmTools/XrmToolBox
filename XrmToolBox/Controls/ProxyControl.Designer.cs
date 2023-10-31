
namespace XrmToolBox.Controls
{
    partial class ProxyControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.sscBypassLocal = new XrmToolBox.Controls.SwitchSettingsControl();
            this.txtscPassword = new XrmToolBox.Controls.TextBoxSettingsControl();
            this.txtscUserName = new XrmToolBox.Controls.TextBoxSettingsControl();
            this.sscCustomAuth = new XrmToolBox.Controls.SwitchSettingsControl();
            this.txtscCustomProxyAddress = new XrmToolBox.Controls.TextBoxSettingsControl();
            this.ddscSelection = new XrmToolBox.Controls.DropdownSettingsControl();
            this.btnApply = new System.Windows.Forms.Button();
            this.LblConnectivityTest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sscBypassLocal
            // 
            this.sscBypassLocal.Checked = false;
            this.sscBypassLocal.Description = null;
            this.sscBypassLocal.Dock = System.Windows.Forms.DockStyle.Top;
            this.sscBypassLocal.Enabled = false;
            this.sscBypassLocal.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sscBypassLocal.Location = new System.Drawing.Point(10, 482);
            this.sscBypassLocal.Name = "sscBypassLocal";
            this.sscBypassLocal.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.sscBypassLocal.PropertyName = null;
            this.sscBypassLocal.Size = new System.Drawing.Size(1347, 112);
            this.sscBypassLocal.TabIndex = 17;
            this.sscBypassLocal.Title = "By pass proxy for local address";
            // 
            // txtscPassword
            // 
            this.txtscPassword.Description = null;
            this.txtscPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtscPassword.Enabled = false;
            this.txtscPassword.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtscPassword.IsPassword = true;
            this.txtscPassword.Location = new System.Drawing.Point(10, 391);
            this.txtscPassword.Multiline = false;
            this.txtscPassword.Name = "txtscPassword";
            this.txtscPassword.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.txtscPassword.PropertyName = null;
            this.txtscPassword.Readonly = false;
            this.txtscPassword.Size = new System.Drawing.Size(1347, 91);
            this.txtscPassword.TabIndex = 16;
            this.txtscPassword.Title = "Password";
            this.txtscPassword.ValidationRegEx = null;
            // 
            // txtscUserName
            // 
            this.txtscUserName.Description = null;
            this.txtscUserName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtscUserName.Enabled = false;
            this.txtscUserName.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtscUserName.IsPassword = false;
            this.txtscUserName.Location = new System.Drawing.Point(10, 300);
            this.txtscUserName.Multiline = false;
            this.txtscUserName.Name = "txtscUserName";
            this.txtscUserName.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.txtscUserName.PropertyName = null;
            this.txtscUserName.Readonly = false;
            this.txtscUserName.Size = new System.Drawing.Size(1347, 91);
            this.txtscUserName.TabIndex = 15;
            this.txtscUserName.Title = "User name";
            this.txtscUserName.ValidationRegEx = null;
            // 
            // sscCustomAuth
            // 
            this.sscCustomAuth.Checked = false;
            this.sscCustomAuth.Description = null;
            this.sscCustomAuth.Dock = System.Windows.Forms.DockStyle.Top;
            this.sscCustomAuth.Enabled = false;
            this.sscCustomAuth.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sscCustomAuth.Location = new System.Drawing.Point(10, 188);
            this.sscCustomAuth.Name = "sscCustomAuth";
            this.sscCustomAuth.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.sscCustomAuth.PropertyName = null;
            this.sscCustomAuth.Size = new System.Drawing.Size(1347, 112);
            this.sscCustomAuth.TabIndex = 14;
            this.sscCustomAuth.Title = "Custom authentication";
            this.sscCustomAuth.OnSettingsPropertyChanged += new System.EventHandler<XrmToolBox.AppCode.SettingsPropertyEventArgs>(this.sscCustomAuth_OnSettingsPropertyChanged);
            // 
            // txtscCustomProxyAddress
            // 
            this.txtscCustomProxyAddress.Description = null;
            this.txtscCustomProxyAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtscCustomProxyAddress.Enabled = false;
            this.txtscCustomProxyAddress.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtscCustomProxyAddress.IsPassword = false;
            this.txtscCustomProxyAddress.Location = new System.Drawing.Point(10, 97);
            this.txtscCustomProxyAddress.Multiline = false;
            this.txtscCustomProxyAddress.Name = "txtscCustomProxyAddress";
            this.txtscCustomProxyAddress.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.txtscCustomProxyAddress.PropertyName = null;
            this.txtscCustomProxyAddress.Readonly = false;
            this.txtscCustomProxyAddress.Size = new System.Drawing.Size(1347, 91);
            this.txtscCustomProxyAddress.TabIndex = 13;
            this.txtscCustomProxyAddress.Title = "Custom proxy address";
            this.txtscCustomProxyAddress.ValidationRegEx = null;
            // 
            // ddscSelection
            // 
            this.ddscSelection.Description = null;
            this.ddscSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.ddscSelection.EnumType = null;
            this.ddscSelection.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddscSelection.Location = new System.Drawing.Point(10, 10);
            this.ddscSelection.Name = "ddscSelection";
            this.ddscSelection.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.ddscSelection.PropertyName = null;
            this.ddscSelection.Size = new System.Drawing.Size(1347, 87);
            this.ddscSelection.TabIndex = 12;
            this.ddscSelection.Title = "Selection";
            this.ddscSelection.Value = null;
            this.ddscSelection.OnSettingsPropertyChanged += new System.EventHandler<XrmToolBox.AppCode.SettingsPropertyEventArgs>(this.ddscSelection_OnSettingsPropertyChanged);
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnApply.Location = new System.Drawing.Point(10, 594);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(1347, 89);
            this.btnApply.TabIndex = 18;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // LblConnectivityTest
            // 
            this.LblConnectivityTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.LblConnectivityTest.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblConnectivityTest.Location = new System.Drawing.Point(10, 683);
            this.LblConnectivityTest.Name = "LblConnectivityTest";
            this.LblConnectivityTest.Size = new System.Drawing.Size(1347, 42);
            this.LblConnectivityTest.TabIndex = 19;
            this.LblConnectivityTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProxyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LblConnectivityTest);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.sscBypassLocal);
            this.Controls.Add(this.txtscPassword);
            this.Controls.Add(this.txtscUserName);
            this.Controls.Add(this.sscCustomAuth);
            this.Controls.Add(this.txtscCustomProxyAddress);
            this.Controls.Add(this.ddscSelection);
            this.Name = "ProxyControl";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(1367, 942);
            this.ResumeLayout(false);

        }

        #endregion

        private SwitchSettingsControl sscBypassLocal;
        private TextBoxSettingsControl txtscPassword;
        private TextBoxSettingsControl txtscUserName;
        private SwitchSettingsControl sscCustomAuth;
        private TextBoxSettingsControl txtscCustomProxyAddress;
        private DropdownSettingsControl ddscSelection;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label LblConnectivityTest;
    }
}
