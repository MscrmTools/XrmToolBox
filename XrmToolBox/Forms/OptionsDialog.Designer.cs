namespace XrmToolBox.Forms
{
    partial class OptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbDisplay = new System.Windows.Forms.TabPage();
            this.chkMergeConnectionFiles = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbToolsListSmall = new System.Windows.Forms.RadioButton();
            this.rdbToolsListLarge = new System.Windows.Forms.RadioButton();
            this.chkDisplayMuFirst = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvPlugins = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbProxy = new System.Windows.Forms.TabPage();
            this.gbCustomProxy = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtProxyUser = new System.Windows.Forms.TextBox();
            this.txtProxyPassword = new System.Windows.Forms.TextBox();
            this.lblProxyUser = new System.Windows.Forms.Label();
            this.lblProxyPassword = new System.Windows.Forms.Label();
            this.txtProxyAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblProxyAddress = new System.Windows.Forms.Label();
            this.chkByPassProxyOnLocal = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbCustomAuthYes = new System.Windows.Forms.RadioButton();
            this.rbCustomAuthNo = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbProxyUsage = new System.Windows.Forms.ComboBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkDisplayPluginsStoreOnlyIfUpdates = new System.Windows.Forms.CheckBox();
            this.chkDoNotCheckForUpdate = new System.Windows.Forms.CheckBox();
            this.chkCloseEachPluginSilently = new System.Windows.Forms.CheckBox();
            this.chkDisplayPluginsStoreOnStartup = new System.Windows.Forms.CheckBox();
            this.chkClosePluginsSilently = new System.Windows.Forms.CheckBox();
            this.chkAllowUsageStatistics = new System.Windows.Forms.CheckBox();
            this.tpPaths = new System.Windows.Forms.TabPage();
            this.lblChangePathDescription = new System.Windows.Forms.Label();
            this.lblChangePathTitle = new System.Windows.Forms.Label();
            this.llOpenStorageFolder = new System.Windows.Forms.LinkLabel();
            this.llOpenRootFolder = new System.Windows.Forms.LinkLabel();
            this.chkRememberSession = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbDisplay.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tbProxy.SuspendLayout();
            this.gbCustomProxy.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tpPaths.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 982);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1415, 81);
            this.panel1.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(978, 19);
            this.btnOk.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(199, 54);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1195, 19);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(199, 54);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1415, 181);
            this.pnlHeader.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(27, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1369, 67);
            this.label2.TabIndex = 1;
            this.label2.Text = "This dialog helps you to control how plugins are displayed in the application. Yo" +
    "u can also define how to use XrmToolBox with a proxy";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblTitle.Location = new System.Drawing.Point(25, 19);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(192, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Settings";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbDisplay);
            this.tabControl1.Controls.Add(this.tbProxy);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tpPaths);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 181);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1415, 801);
            this.tabControl1.TabIndex = 7;
            // 
            // tbDisplay
            // 
            this.tbDisplay.Controls.Add(this.chkMergeConnectionFiles);
            this.tbDisplay.Controls.Add(this.panel4);
            this.tbDisplay.Controls.Add(this.chkDisplayMuFirst);
            this.tbDisplay.Controls.Add(this.groupBox3);
            this.tbDisplay.Location = new System.Drawing.Point(10, 48);
            this.tbDisplay.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbDisplay.Name = "tbDisplay";
            this.tbDisplay.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbDisplay.Size = new System.Drawing.Size(1395, 743);
            this.tbDisplay.TabIndex = 0;
            this.tbDisplay.Text = "Display";
            this.tbDisplay.UseVisualStyleBackColor = true;
            // 
            // chkMergeConnectionFiles
            // 
            this.chkMergeConnectionFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMergeConnectionFiles.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMergeConnectionFiles.Location = new System.Drawing.Point(11, 115);
            this.chkMergeConnectionFiles.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.chkMergeConnectionFiles.Name = "chkMergeConnectionFiles";
            this.chkMergeConnectionFiles.Size = new System.Drawing.Size(1364, 73);
            this.chkMergeConnectionFiles.TabIndex = 6;
            this.chkMergeConnectionFiles.Text = "Do not display connections files in the bottom left connection control, show me o" +
    "nly connections (requires to restart XrmToolBox)";
            this.chkMergeConnectionFiles.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.rdbToolsListSmall);
            this.panel4.Controls.Add(this.rdbToolsListLarge);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1374, 54);
            this.panel4.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tools list";
            // 
            // rdbToolsListSmall
            // 
            this.rdbToolsListSmall.AutoSize = true;
            this.rdbToolsListSmall.Location = new System.Drawing.Point(391, 9);
            this.rdbToolsListSmall.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.rdbToolsListSmall.Name = "rdbToolsListSmall";
            this.rdbToolsListSmall.Size = new System.Drawing.Size(198, 36);
            this.rdbToolsListSmall.TabIndex = 1;
            this.rdbToolsListSmall.Text = "Small icons";
            this.rdbToolsListSmall.UseVisualStyleBackColor = true;
            // 
            // rdbToolsListLarge
            // 
            this.rdbToolsListLarge.AutoSize = true;
            this.rdbToolsListLarge.Checked = true;
            this.rdbToolsListLarge.Location = new System.Drawing.Point(160, 9);
            this.rdbToolsListLarge.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.rdbToolsListLarge.Name = "rdbToolsListLarge";
            this.rdbToolsListLarge.Size = new System.Drawing.Size(199, 36);
            this.rdbToolsListLarge.TabIndex = 0;
            this.rdbToolsListLarge.TabStop = true;
            this.rdbToolsListLarge.Text = "Large icons";
            this.rdbToolsListLarge.UseVisualStyleBackColor = true;
            // 
            // chkDisplayMuFirst
            // 
            this.chkDisplayMuFirst.AutoSize = true;
            this.chkDisplayMuFirst.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDisplayMuFirst.Location = new System.Drawing.Point(11, 62);
            this.chkDisplayMuFirst.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.chkDisplayMuFirst.Name = "chkDisplayMuFirst";
            this.chkDisplayMuFirst.Size = new System.Drawing.Size(405, 36);
            this.chkDisplayMuFirst.TabIndex = 0;
            this.chkDisplayMuFirst.Text = "Display most used tools first";
            this.chkDisplayMuFirst.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lvPlugins);
            this.groupBox3.Location = new System.Drawing.Point(11, 282);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox3.Size = new System.Drawing.Size(1376, 426);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tools list (Unchecked plugins are not displayed in XrmToolBox application)";
            // 
            // lvPlugins
            // 
            this.lvPlugins.CheckBoxes = true;
            this.lvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPlugins.FullRowSelect = true;
            this.lvPlugins.Location = new System.Drawing.Point(5, 36);
            this.lvPlugins.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.lvPlugins.Name = "lvPlugins";
            this.lvPlugins.Size = new System.Drawing.Size(1366, 385);
            this.lvPlugins.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPlugins.TabIndex = 0;
            this.lvPlugins.UseCompatibleStateImageBehavior = false;
            this.lvPlugins.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Plugin name";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Author";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Version";
            this.columnHeader3.Width = 100;
            // 
            // tbProxy
            // 
            this.tbProxy.Controls.Add(this.gbCustomProxy);
            this.tbProxy.Controls.Add(this.cbbProxyUsage);
            this.tbProxy.Location = new System.Drawing.Point(10, 48);
            this.tbProxy.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbProxy.Name = "tbProxy";
            this.tbProxy.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tbProxy.Size = new System.Drawing.Size(1395, 743);
            this.tbProxy.TabIndex = 1;
            this.tbProxy.Text = "Proxy";
            this.tbProxy.UseVisualStyleBackColor = true;
            // 
            // gbCustomProxy
            // 
            this.gbCustomProxy.Controls.Add(this.panel3);
            this.gbCustomProxy.Controls.Add(this.txtProxyAddress);
            this.gbCustomProxy.Controls.Add(this.label4);
            this.gbCustomProxy.Controls.Add(this.lblProxyAddress);
            this.gbCustomProxy.Controls.Add(this.chkByPassProxyOnLocal);
            this.gbCustomProxy.Controls.Add(this.panel2);
            this.gbCustomProxy.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCustomProxy.Location = new System.Drawing.Point(5, 44);
            this.gbCustomProxy.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.gbCustomProxy.Name = "gbCustomProxy";
            this.gbCustomProxy.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.gbCustomProxy.Size = new System.Drawing.Size(1385, 333);
            this.gbCustomProxy.TabIndex = 15;
            this.gbCustomProxy.TabStop = false;
            this.gbCustomProxy.Text = "Custom proxy";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtProxyUser);
            this.panel3.Controls.Add(this.txtProxyPassword);
            this.panel3.Controls.Add(this.lblProxyUser);
            this.panel3.Controls.Add(this.lblProxyPassword);
            this.panel3.Location = new System.Drawing.Point(0, 147);
            this.panel3.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1397, 105);
            this.panel3.TabIndex = 14;
            // 
            // txtProxyUser
            // 
            this.txtProxyUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProxyUser.Enabled = false;
            this.txtProxyUser.Location = new System.Drawing.Point(348, 5);
            this.txtProxyUser.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtProxyUser.Name = "txtProxyUser";
            this.txtProxyUser.Size = new System.Drawing.Size(1039, 38);
            this.txtProxyUser.TabIndex = 5;
            // 
            // txtProxyPassword
            // 
            this.txtProxyPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProxyPassword.Enabled = false;
            this.txtProxyPassword.Location = new System.Drawing.Point(348, 54);
            this.txtProxyPassword.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtProxyPassword.Name = "txtProxyPassword";
            this.txtProxyPassword.PasswordChar = '*';
            this.txtProxyPassword.Size = new System.Drawing.Size(1039, 38);
            this.txtProxyPassword.TabIndex = 6;
            // 
            // lblProxyUser
            // 
            this.lblProxyUser.AutoSize = true;
            this.lblProxyUser.Location = new System.Drawing.Point(18, 9);
            this.lblProxyUser.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblProxyUser.Name = "lblProxyUser";
            this.lblProxyUser.Size = new System.Drawing.Size(74, 32);
            this.lblProxyUser.TabIndex = 7;
            this.lblProxyUser.Text = "User";
            // 
            // lblProxyPassword
            // 
            this.lblProxyPassword.AutoSize = true;
            this.lblProxyPassword.Location = new System.Drawing.Point(21, 59);
            this.lblProxyPassword.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblProxyPassword.Name = "lblProxyPassword";
            this.lblProxyPassword.Size = new System.Drawing.Size(139, 32);
            this.lblProxyPassword.TabIndex = 8;
            this.lblProxyPassword.Text = "Password";
            // 
            // txtProxyAddress
            // 
            this.txtProxyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProxyAddress.Enabled = false;
            this.txtProxyAddress.Location = new System.Drawing.Point(345, 39);
            this.txtProxyAddress.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.txtProxyAddress.Name = "txtProxyAddress";
            this.txtProxyAddress.Size = new System.Drawing.Size(1037, 38);
            this.txtProxyAddress.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 260);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(290, 32);
            this.label4.TabIndex = 10;
            this.label4.Text = "Bypass proxy on local";
            // 
            // lblProxyAddress
            // 
            this.lblProxyAddress.AutoSize = true;
            this.lblProxyAddress.Location = new System.Drawing.Point(11, 43);
            this.lblProxyAddress.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblProxyAddress.Name = "lblProxyAddress";
            this.lblProxyAddress.Size = new System.Drawing.Size(119, 32);
            this.lblProxyAddress.TabIndex = 3;
            this.lblProxyAddress.Text = "Address";
            // 
            // chkByPassProxyOnLocal
            // 
            this.chkByPassProxyOnLocal.AutoSize = true;
            this.chkByPassProxyOnLocal.Enabled = false;
            this.chkByPassProxyOnLocal.Location = new System.Drawing.Point(345, 262);
            this.chkByPassProxyOnLocal.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkByPassProxyOnLocal.Name = "chkByPassProxyOnLocal";
            this.chkByPassProxyOnLocal.Size = new System.Drawing.Size(34, 33);
            this.chkByPassProxyOnLocal.TabIndex = 9;
            this.chkByPassProxyOnLocal.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbCustomAuthYes);
            this.panel2.Controls.Add(this.rbCustomAuthNo);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(0, 88);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1397, 50);
            this.panel2.TabIndex = 13;
            // 
            // rbCustomAuthYes
            // 
            this.rbCustomAuthYes.AutoSize = true;
            this.rbCustomAuthYes.Enabled = false;
            this.rbCustomAuthYes.Location = new System.Drawing.Point(345, 5);
            this.rbCustomAuthYes.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.rbCustomAuthYes.Name = "rbCustomAuthYes";
            this.rbCustomAuthYes.Size = new System.Drawing.Size(101, 36);
            this.rbCustomAuthYes.TabIndex = 0;
            this.rbCustomAuthYes.Text = "Yes";
            this.rbCustomAuthYes.UseVisualStyleBackColor = true;
            this.rbCustomAuthYes.CheckedChanged += new System.EventHandler(this.rbCustomAuthYes_CheckedChanged);
            // 
            // rbCustomAuthNo
            // 
            this.rbCustomAuthNo.AutoSize = true;
            this.rbCustomAuthNo.Checked = true;
            this.rbCustomAuthNo.Enabled = false;
            this.rbCustomAuthNo.Location = new System.Drawing.Point(464, 5);
            this.rbCustomAuthNo.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.rbCustomAuthNo.Name = "rbCustomAuthNo";
            this.rbCustomAuthNo.Size = new System.Drawing.Size(88, 36);
            this.rbCustomAuthNo.TabIndex = 2;
            this.rbCustomAuthNo.TabStop = true;
            this.rbCustomAuthNo.Text = "No";
            this.rbCustomAuthNo.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(299, 32);
            this.label5.TabIndex = 12;
            this.label5.Text = "Custom authentication";
            // 
            // cbbProxyUsage
            // 
            this.cbbProxyUsage.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbProxyUsage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbProxyUsage.FormattingEnabled = true;
            this.cbbProxyUsage.Items.AddRange(new object[] {
            "No proxy",
            "Use Internet Explorer configured proxy",
            "Use custom proxy"});
            this.cbbProxyUsage.Location = new System.Drawing.Point(5, 5);
            this.cbbProxyUsage.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.cbbProxyUsage.Name = "cbbProxyUsage";
            this.cbbProxyUsage.Size = new System.Drawing.Size(1385, 39);
            this.cbbProxyUsage.TabIndex = 14;
            this.cbbProxyUsage.SelectedIndexChanged += new System.EventHandler(this.cbbProxyUsage_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkRememberSession);
            this.tabPage1.Controls.Add(this.chkDisplayPluginsStoreOnlyIfUpdates);
            this.tabPage1.Controls.Add(this.chkDoNotCheckForUpdate);
            this.tabPage1.Controls.Add(this.chkCloseEachPluginSilently);
            this.tabPage1.Controls.Add(this.chkDisplayPluginsStoreOnStartup);
            this.tabPage1.Controls.Add(this.chkClosePluginsSilently);
            this.tabPage1.Controls.Add(this.chkAllowUsageStatistics);
            this.tabPage1.Location = new System.Drawing.Point(10, 48);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tabPage1.Size = new System.Drawing.Size(1395, 743);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Miscellaneous";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkDisplayPluginsStoreOnlyIfUpdates
            // 
            this.chkDisplayPluginsStoreOnlyIfUpdates.AutoSize = true;
            this.chkDisplayPluginsStoreOnlyIfUpdates.Enabled = false;
            this.chkDisplayPluginsStoreOnlyIfUpdates.Location = new System.Drawing.Point(75, 225);
            this.chkDisplayPluginsStoreOnlyIfUpdates.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkDisplayPluginsStoreOnlyIfUpdates.Name = "chkDisplayPluginsStoreOnlyIfUpdates";
            this.chkDisplayPluginsStoreOnlyIfUpdates.Size = new System.Drawing.Size(498, 36);
            this.chkDisplayPluginsStoreOnlyIfUpdates.TabIndex = 6;
            this.chkDisplayPluginsStoreOnlyIfUpdates.Text = "Only if plugin updates are available";
            this.chkDisplayPluginsStoreOnlyIfUpdates.UseVisualStyleBackColor = true;
            // 
            // chkDoNotCheckForUpdate
            // 
            this.chkDoNotCheckForUpdate.AutoSize = true;
            this.chkDoNotCheckForUpdate.Location = new System.Drawing.Point(28, 270);
            this.chkDoNotCheckForUpdate.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkDoNotCheckForUpdate.Name = "chkDoNotCheckForUpdate";
            this.chkDoNotCheckForUpdate.Size = new System.Drawing.Size(366, 36);
            this.chkDoNotCheckForUpdate.TabIndex = 5;
            this.chkDoNotCheckForUpdate.Text = "Do not check for updates";
            this.chkDoNotCheckForUpdate.UseVisualStyleBackColor = true;
            // 
            // chkCloseEachPluginSilently
            // 
            this.chkCloseEachPluginSilently.AutoSize = true;
            this.chkCloseEachPluginSilently.Location = new System.Drawing.Point(27, 81);
            this.chkCloseEachPluginSilently.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkCloseEachPluginSilently.Name = "chkCloseEachPluginSilently";
            this.chkCloseEachPluginSilently.Size = new System.Drawing.Size(502, 36);
            this.chkCloseEachPluginSilently.TabIndex = 2;
            this.chkCloseEachPluginSilently.Text = "Do not prompt when closing plugins";
            this.chkCloseEachPluginSilently.UseVisualStyleBackColor = true;
            this.chkCloseEachPluginSilently.CheckedChanged += new System.EventHandler(this.chkCloseEachPluginSilently_CheckedChanged);
            // 
            // chkDisplayPluginsStoreOnStartup
            // 
            this.chkDisplayPluginsStoreOnStartup.AutoSize = true;
            this.chkDisplayPluginsStoreOnStartup.Location = new System.Drawing.Point(27, 177);
            this.chkDisplayPluginsStoreOnStartup.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkDisplayPluginsStoreOnStartup.Name = "chkDisplayPluginsStoreOnStartup";
            this.chkDisplayPluginsStoreOnStartup.Size = new System.Drawing.Size(612, 36);
            this.chkDisplayPluginsStoreOnStartup.TabIndex = 4;
            this.chkDisplayPluginsStoreOnStartup.Text = "Display plugins store on XrmToolBox startup";
            this.chkDisplayPluginsStoreOnStartup.UseVisualStyleBackColor = true;
            this.chkDisplayPluginsStoreOnStartup.CheckedChanged += new System.EventHandler(this.chkDisplayPluginsStoreOnStartup_CheckedChanged);
            // 
            // chkClosePluginsSilently
            // 
            this.chkClosePluginsSilently.AutoSize = true;
            this.chkClosePluginsSilently.Location = new System.Drawing.Point(27, 129);
            this.chkClosePluginsSilently.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkClosePluginsSilently.Name = "chkClosePluginsSilently";
            this.chkClosePluginsSilently.Size = new System.Drawing.Size(924, 36);
            this.chkClosePluginsSilently.TabIndex = 3;
            this.chkClosePluginsSilently.Text = "Do not prompt on exit when plugins are opened, close plugins silently";
            this.chkClosePluginsSilently.UseVisualStyleBackColor = true;
            // 
            // chkAllowUsageStatistics
            // 
            this.chkAllowUsageStatistics.AutoSize = true;
            this.chkAllowUsageStatistics.Location = new System.Drawing.Point(27, 34);
            this.chkAllowUsageStatistics.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.chkAllowUsageStatistics.Name = "chkAllowUsageStatistics";
            this.chkAllowUsageStatistics.Size = new System.Drawing.Size(981, 36);
            this.chkAllowUsageStatistics.TabIndex = 1;
            this.chkAllowUsageStatistics.Text = "Allow to send usage statistics (only anonymous data : plugin usage count)";
            this.chkAllowUsageStatistics.UseVisualStyleBackColor = true;
            // 
            // tpPaths
            // 
            this.tpPaths.Controls.Add(this.lblChangePathDescription);
            this.tpPaths.Controls.Add(this.lblChangePathTitle);
            this.tpPaths.Controls.Add(this.llOpenStorageFolder);
            this.tpPaths.Controls.Add(this.llOpenRootFolder);
            this.tpPaths.Location = new System.Drawing.Point(10, 48);
            this.tpPaths.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.tpPaths.Name = "tpPaths";
            this.tpPaths.Padding = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.tpPaths.Size = new System.Drawing.Size(1395, 743);
            this.tpPaths.TabIndex = 3;
            this.tpPaths.Text = "Paths";
            this.tpPaths.UseVisualStyleBackColor = true;
            // 
            // lblChangePathDescription
            // 
            this.lblChangePathDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChangePathDescription.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangePathDescription.Location = new System.Drawing.Point(7, 197);
            this.lblChangePathDescription.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblChangePathDescription.Name = "lblChangePathDescription";
            this.lblChangePathDescription.Size = new System.Drawing.Size(1381, 538);
            this.lblChangePathDescription.TabIndex = 4;
            this.lblChangePathDescription.Text = resources.GetString("lblChangePathDescription.Text");
            // 
            // lblChangePathTitle
            // 
            this.lblChangePathTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChangePathTitle.Font = new System.Drawing.Font("Segoe UI Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangePathTitle.Location = new System.Drawing.Point(7, 132);
            this.lblChangePathTitle.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblChangePathTitle.Name = "lblChangePathTitle";
            this.lblChangePathTitle.Size = new System.Drawing.Size(1381, 65);
            this.lblChangePathTitle.TabIndex = 3;
            this.lblChangePathTitle.Text = "How to change XrmToolBox storage folder";
            // 
            // llOpenStorageFolder
            // 
            this.llOpenStorageFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.llOpenStorageFolder.Location = new System.Drawing.Point(7, 70);
            this.llOpenStorageFolder.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.llOpenStorageFolder.Name = "llOpenStorageFolder";
            this.llOpenStorageFolder.Size = new System.Drawing.Size(1381, 62);
            this.llOpenStorageFolder.TabIndex = 1;
            this.llOpenStorageFolder.TabStop = true;
            this.llOpenStorageFolder.Text = "Open XrmToolBox storage folder";
            this.llOpenStorageFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOpenStorageFolder_LinkClicked);
            // 
            // llOpenRootFolder
            // 
            this.llOpenRootFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.llOpenRootFolder.Location = new System.Drawing.Point(7, 8);
            this.llOpenRootFolder.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.llOpenRootFolder.Name = "llOpenRootFolder";
            this.llOpenRootFolder.Size = new System.Drawing.Size(1381, 62);
            this.llOpenRootFolder.TabIndex = 0;
            this.llOpenRootFolder.TabStop = true;
            this.llOpenRootFolder.Text = "Open XrmToolBox folder";
            this.llOpenRootFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOpenRootFolder_LinkClicked);
            // 
            // chkRememberSession
            // 
            this.chkRememberSession.AutoSize = true;
            this.chkRememberSession.Location = new System.Drawing.Point(28, 325);
            this.chkRememberSession.Name = "chkRememberSession";
            this.chkRememberSession.Size = new System.Drawing.Size(296, 36);
            this.chkRememberSession.TabIndex = 7;
            this.chkRememberSession.Text = "Remember session";
            this.chkRememberSession.UseVisualStyleBackColor = true;
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1415, 1063);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.OptionsDialog_Load);
            this.panel1.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbDisplay.ResumeLayout(false);
            this.tbDisplay.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tbProxy.ResumeLayout(false);
            this.gbCustomProxy.ResumeLayout(false);
            this.gbCustomProxy.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tpPaths.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdbToolsListSmall;
        private System.Windows.Forms.RadioButton rdbToolsListLarge;
        private System.Windows.Forms.CheckBox chkDisplayMuFirst;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvPlugins;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tbProxy;
        private System.Windows.Forms.Label lblProxyPassword;
        private System.Windows.Forms.Label lblProxyUser;
        private System.Windows.Forms.TextBox txtProxyPassword;
        private System.Windows.Forms.TextBox txtProxyUser;
        private System.Windows.Forms.TextBox txtProxyAddress;
        private System.Windows.Forms.Label lblProxyAddress;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbCustomAuthYes;
        private System.Windows.Forms.RadioButton rbCustomAuthNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkByPassProxyOnLocal;
        private System.Windows.Forms.ComboBox cbbProxyUsage;
        private System.Windows.Forms.GroupBox gbCustomProxy;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox chkAllowUsageStatistics;
        private System.Windows.Forms.CheckBox chkClosePluginsSilently;
        private System.Windows.Forms.CheckBox chkDisplayPluginsStoreOnStartup;
        private System.Windows.Forms.CheckBox chkCloseEachPluginSilently;
        private System.Windows.Forms.CheckBox chkDoNotCheckForUpdate;
        private System.Windows.Forms.CheckBox chkDisplayPluginsStoreOnlyIfUpdates;
        private System.Windows.Forms.TabPage tpPaths;
        private System.Windows.Forms.LinkLabel llOpenRootFolder;
        private System.Windows.Forms.Label lblChangePathDescription;
        private System.Windows.Forms.Label lblChangePathTitle;
        private System.Windows.Forms.LinkLabel llOpenStorageFolder;
        private System.Windows.Forms.CheckBox chkMergeConnectionFiles;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkRememberSession;
    }
}