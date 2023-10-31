namespace XrmToolBox.Forms
{
    partial class SettingsForm
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlNavMain = new System.Windows.Forms.Panel();
            this.pnlAdvanced = new System.Windows.Forms.Panel();
            this.advancedControl1 = new XrmToolBox.Controls.AdvancedControl();
            this.pnlAssemblies = new System.Windows.Forms.Panel();
            this.assembliesControl1 = new XrmToolBox.Controls.AssembliesControl();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlAppProtocol = new System.Windows.Forms.Panel();
            this.applicationProtocolControl1 = new XrmToolBox.Controls.ApplicationProtocolControl();
            this.lblAppProtocolTitle = new System.Windows.Forms.Label();
            this.pnlCredits = new System.Windows.Forms.Panel();
            this.creditsControl1 = new XrmToolBox.Controls.CreditsControl();
            this.lblCreditsTitle = new System.Windows.Forms.Label();
            this.pnlPaths = new System.Windows.Forms.Panel();
            this.pathsControl1 = new XrmToolBox.Controls.PathsControl();
            this.lblPathsTitle = new System.Windows.Forms.Label();
            this.pnlProxy = new System.Windows.Forms.Panel();
            this.proxyControl1 = new XrmToolBox.Controls.ProxyControl();
            this.lblProxyTitle = new System.Windows.Forms.Label();
            this.pnlNavLeft = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHiddenTools = new System.Windows.Forms.Label();
            this.hiddenToolsControl1 = new XrmToolBox.Controls.HiddenToolsControl();
            this.pnlNavMain.SuspendLayout();
            this.pnlAdvanced.SuspendLayout();
            this.pnlAssemblies.SuspendLayout();
            this.pnlAppProtocol.SuspendLayout();
            this.pnlCredits.SuspendLayout();
            this.pnlPaths.SuspendLayout();
            this.pnlProxy.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNavMain
            // 
            this.pnlNavMain.BackColor = System.Drawing.Color.White;
            this.pnlNavMain.Controls.Add(this.panel1);
            this.pnlNavMain.Controls.Add(this.pnlAdvanced);
            this.pnlNavMain.Controls.Add(this.pnlAssemblies);
            this.pnlNavMain.Controls.Add(this.pnlAppProtocol);
            this.pnlNavMain.Controls.Add(this.pnlCredits);
            this.pnlNavMain.Controls.Add(this.pnlPaths);
            this.pnlNavMain.Controls.Add(this.pnlProxy);
            this.pnlNavMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNavMain.Location = new System.Drawing.Point(308, 0);
            this.pnlNavMain.Name = "pnlNavMain";
            this.pnlNavMain.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.pnlNavMain.Size = new System.Drawing.Size(1488, 1022);
            this.pnlNavMain.TabIndex = 3;
            // 
            // pnlAdvanced
            // 
            this.pnlAdvanced.Controls.Add(this.advancedControl1);
            this.pnlAdvanced.Location = new System.Drawing.Point(684, 806);
            this.pnlAdvanced.Name = "pnlAdvanced";
            this.pnlAdvanced.Size = new System.Drawing.Size(332, 190);
            this.pnlAdvanced.TabIndex = 6;
            this.pnlAdvanced.Tag = "Advanced";
            // 
            // advancedControl1
            // 
            this.advancedControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedControl1.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.advancedControl1.Location = new System.Drawing.Point(0, 0);
            this.advancedControl1.Name = "advancedControl1";
            this.advancedControl1.Size = new System.Drawing.Size(332, 190);
            this.advancedControl1.TabIndex = 0;
            this.advancedControl1.OnTabsOrderChanged += new System.EventHandler(this.advancedControl1_OnTabsOrderChanged);
            // 
            // pnlAssemblies
            // 
            this.pnlAssemblies.Controls.Add(this.assembliesControl1);
            this.pnlAssemblies.Controls.Add(this.label1);
            this.pnlAssemblies.Location = new System.Drawing.Point(1073, 767);
            this.pnlAssemblies.Name = "pnlAssemblies";
            this.pnlAssemblies.Size = new System.Drawing.Size(403, 237);
            this.pnlAssemblies.TabIndex = 5;
            this.pnlAssemblies.Tag = "Assemblies";
            // 
            // assembliesControl1
            // 
            this.assembliesControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.assembliesControl1.Location = new System.Drawing.Point(0, 48);
            this.assembliesControl1.Name = "assembliesControl1";
            this.assembliesControl1.Size = new System.Drawing.Size(403, 189);
            this.assembliesControl1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Text", 16F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Assemblies";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlAppProtocol
            // 
            this.pnlAppProtocol.Controls.Add(this.applicationProtocolControl1);
            this.pnlAppProtocol.Controls.Add(this.lblAppProtocolTitle);
            this.pnlAppProtocol.Location = new System.Drawing.Point(50, 713);
            this.pnlAppProtocol.Name = "pnlAppProtocol";
            this.pnlAppProtocol.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.pnlAppProtocol.Size = new System.Drawing.Size(603, 291);
            this.pnlAppProtocol.TabIndex = 4;
            this.pnlAppProtocol.Tag = "Application Protocol";
            // 
            // applicationProtocolControl1
            // 
            this.applicationProtocolControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationProtocolControl1.Location = new System.Drawing.Point(10, 40);
            this.applicationProtocolControl1.Name = "applicationProtocolControl1";
            this.applicationProtocolControl1.Size = new System.Drawing.Size(583, 251);
            this.applicationProtocolControl1.TabIndex = 2;
            // 
            // lblAppProtocolTitle
            // 
            this.lblAppProtocolTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAppProtocolTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppProtocolTitle.Location = new System.Drawing.Point(10, 0);
            this.lblAppProtocolTitle.Name = "lblAppProtocolTitle";
            this.lblAppProtocolTitle.Size = new System.Drawing.Size(583, 40);
            this.lblAppProtocolTitle.TabIndex = 1;
            this.lblAppProtocolTitle.Text = "Application protocol";
            this.lblAppProtocolTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCredits
            // 
            this.pnlCredits.Controls.Add(this.creditsControl1);
            this.pnlCredits.Controls.Add(this.lblCreditsTitle);
            this.pnlCredits.Location = new System.Drawing.Point(1012, 45);
            this.pnlCredits.Name = "pnlCredits";
            this.pnlCredits.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.pnlCredits.Size = new System.Drawing.Size(391, 186);
            this.pnlCredits.TabIndex = 3;
            this.pnlCredits.Tag = "Credits";
            // 
            // creditsControl1
            // 
            this.creditsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.creditsControl1.Location = new System.Drawing.Point(10, 41);
            this.creditsControl1.Name = "creditsControl1";
            this.creditsControl1.Size = new System.Drawing.Size(371, 145);
            this.creditsControl1.TabIndex = 2;
            // 
            // lblCreditsTitle
            // 
            this.lblCreditsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCreditsTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditsTitle.Location = new System.Drawing.Point(10, 0);
            this.lblCreditsTitle.Name = "lblCreditsTitle";
            this.lblCreditsTitle.Size = new System.Drawing.Size(371, 41);
            this.lblCreditsTitle.TabIndex = 1;
            this.lblCreditsTitle.Text = "Credits";
            this.lblCreditsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlPaths
            // 
            this.pnlPaths.Controls.Add(this.pathsControl1);
            this.pnlPaths.Controls.Add(this.lblPathsTitle);
            this.pnlPaths.Location = new System.Drawing.Point(92, 62);
            this.pnlPaths.Name = "pnlPaths";
            this.pnlPaths.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.pnlPaths.Size = new System.Drawing.Size(808, 470);
            this.pnlPaths.TabIndex = 1;
            this.pnlPaths.Tag = "Paths";
            // 
            // pathsControl1
            // 
            this.pathsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pathsControl1.Location = new System.Drawing.Point(10, 40);
            this.pathsControl1.Name = "pathsControl1";
            this.pathsControl1.Size = new System.Drawing.Size(788, 430);
            this.pathsControl1.TabIndex = 1;
            // 
            // lblPathsTitle
            // 
            this.lblPathsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPathsTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPathsTitle.Location = new System.Drawing.Point(10, 0);
            this.lblPathsTitle.Name = "lblPathsTitle";
            this.lblPathsTitle.Size = new System.Drawing.Size(788, 40);
            this.lblPathsTitle.TabIndex = 0;
            this.lblPathsTitle.Text = "Paths";
            this.lblPathsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlProxy
            // 
            this.pnlProxy.Controls.Add(this.proxyControl1);
            this.pnlProxy.Controls.Add(this.lblProxyTitle);
            this.pnlProxy.Location = new System.Drawing.Point(906, 265);
            this.pnlProxy.Name = "pnlProxy";
            this.pnlProxy.Padding = new System.Windows.Forms.Padding(10);
            this.pnlProxy.Size = new System.Drawing.Size(522, 457);
            this.pnlProxy.TabIndex = 0;
            this.pnlProxy.Tag = "Proxy";
            // 
            // proxyControl1
            // 
            this.proxyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.proxyControl1.Location = new System.Drawing.Point(10, 60);
            this.proxyControl1.Name = "proxyControl1";
            this.proxyControl1.Padding = new System.Windows.Forms.Padding(10);
            this.proxyControl1.Size = new System.Drawing.Size(502, 387);
            this.proxyControl1.TabIndex = 4;
            this.proxyControl1.OnProxySettingsChanged += ProxyControl1_OnProxySettingsChanged;
            // 
            // lblProxyTitle
            // 
            this.lblProxyTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProxyTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProxyTitle.Location = new System.Drawing.Point(10, 10);
            this.lblProxyTitle.Name = "lblProxyTitle";
            this.lblProxyTitle.Size = new System.Drawing.Size(502, 50);
            this.lblProxyTitle.TabIndex = 3;
            this.lblProxyTitle.Text = "Proxy";
            this.lblProxyTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlNavLeft
            // 
            this.pnlNavLeft.AutoScroll = true;
            this.pnlNavLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNavLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlNavLeft.Name = "pnlNavLeft";
            this.pnlNavLeft.Size = new System.Drawing.Size(308, 1022);
            this.pnlNavLeft.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.hiddenToolsControl1);
            this.panel1.Controls.Add(this.lblHiddenTools);
            this.panel1.Location = new System.Drawing.Point(37, 565);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panel1.Size = new System.Drawing.Size(603, 291);
            this.panel1.TabIndex = 7;
            this.panel1.Tag = "Hidden Tools";
            // 
            // lblHiddenTools
            // 
            this.lblHiddenTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHiddenTools.Font = new System.Drawing.Font("Segoe UI Variable Text", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHiddenTools.Location = new System.Drawing.Point(10, 0);
            this.lblHiddenTools.Name = "lblHiddenTools";
            this.lblHiddenTools.Size = new System.Drawing.Size(583, 40);
            this.lblHiddenTools.TabIndex = 1;
            this.lblHiddenTools.Text = "Hidden Tools";
            this.lblHiddenTools.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hiddenToolsControl1
            // 
            this.hiddenToolsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hiddenToolsControl1.Location = new System.Drawing.Point(15, 60);
            this.hiddenToolsControl1.Name = "hiddenToolsControl1";
            this.hiddenToolsControl1.Size = new System.Drawing.Size(875, 377);
            this.hiddenToolsControl1.TabIndex = 2;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1796, 1022);
            this.Controls.Add(this.pnlNavMain);
            this.Controls.Add(this.pnlNavLeft);
            this.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.pnlNavMain.ResumeLayout(false);
            this.pnlAdvanced.ResumeLayout(false);
            this.pnlAssemblies.ResumeLayout(false);
            this.pnlAppProtocol.ResumeLayout(false);
            this.pnlCredits.ResumeLayout(false);
            this.pnlPaths.ResumeLayout(false);
            this.pnlProxy.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

    

        #endregion

        private System.Windows.Forms.Panel pnlNavMain;
        private System.Windows.Forms.Panel pnlNavLeft;
        private System.Windows.Forms.Panel pnlProxy;
        private System.Windows.Forms.Panel pnlPaths;
        private System.Windows.Forms.Panel pnlCredits;
        private System.Windows.Forms.Panel pnlAssemblies;
        private System.Windows.Forms.Panel pnlAppProtocol;
        private System.Windows.Forms.Label lblPathsTitle;
        private System.Windows.Forms.Label lblCreditsTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlAdvanced;
        private Controls.AdvancedControl advancedControl1;
        private Controls.ApplicationProtocolControl applicationProtocolControl1;
        private System.Windows.Forms.Label lblAppProtocolTitle;
        private Controls.AssembliesControl assembliesControl1;
        private Controls.PathsControl pathsControl1;
        private Controls.CreditsControl creditsControl1;
        private System.Windows.Forms.Label lblProxyTitle;
        private Controls.ProxyControl proxyControl1;
        private System.Windows.Forms.Panel panel1;
        private Controls.HiddenToolsControl hiddenToolsControl1;
        private System.Windows.Forms.Label lblHiddenTools;
    }
}

