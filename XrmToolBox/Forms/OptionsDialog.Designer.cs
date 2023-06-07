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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
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
            this.tpPaths = new System.Windows.Forms.TabPage();
            this.lblChangePathDescription = new System.Windows.Forms.Label();
            this.lblChangePathTitle = new System.Windows.Forms.Label();
            this.llOpenStorageFolder = new System.Windows.Forms.LinkLabel();
            this.llOpenRootFolder = new System.Windows.Forms.LinkLabel();
            this.tbDataCollect = new System.Windows.Forms.TabPage();
            this.chkOptinAI = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tpAssemblies = new System.Windows.Forms.TabPage();
            this.lvAssemblies = new System.Windows.Forms.ListView();
            this.chAssembly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbAppProtocol = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.llProtocolDoc = new System.Windows.Forms.LinkLabel();
            this.pnlAppProtocolButton = new System.Windows.Forms.Panel();
            this.btnAppProtocol = new System.Windows.Forms.Button();
            this.pnlAppProtocolPath = new System.Windows.Forms.Panel();
            this.lblAppProtocolPath = new System.Windows.Forms.Label();
            this.pnlAppProtocolStatus = new System.Windows.Forms.Panel();
            this.lblAppProtocolStatus = new System.Windows.Forms.Label();
            this.pnlAppProtocolTop = new System.Windows.Forms.Panel();
            this.lblAppProtocolSummary = new System.Windows.Forms.Label();
            this.tpCredits = new System.Windows.Forms.TabPage();
            this.rtbCredits = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tbProxy.SuspendLayout();
            this.gbCustomProxy.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tpPaths.SuspendLayout();
            this.tbDataCollect.SuspendLayout();
            this.tpAssemblies.SuspendLayout();
            this.tbAppProtocol.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlAppProtocolButton.SuspendLayout();
            this.pnlAppProtocolPath.SuspendLayout();
            this.pnlAppProtocolStatus.SuspendLayout();
            this.pnlAppProtocolTop.SuspendLayout();
            this.tpCredits.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 771);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(876, 53);
            this.panel1.TabIndex = 5;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(629, 13);
            this.btnOk.Margin = new System.Windows.Forms.Padding(5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(113, 35);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(751, 13);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 35);
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
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(876, 117);
            this.pnlHeader.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(770, 43);
            this.label2.TabIndex = 1;
            this.label2.Text = "This dialog helps you to control how tools are displayed in the application. You " +
    "can also define how to use XrmToolBox with a proxy";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblTitle.Location = new System.Drawing.Point(14, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(119, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Settings";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tbProxy);
            this.tabControl1.Controls.Add(this.tpPaths);
            this.tabControl1.Controls.Add(this.tbDataCollect);
            this.tabControl1.Controls.Add(this.tpAssemblies);
            this.tabControl1.Controls.Add(this.tbAppProtocol);
            this.tabControl1.Controls.Add(this.tpCredits);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 117);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(876, 654);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.propertyGrid1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(868, 621);
            this.tabPage2.TabIndex = 6;
            this.tabPage2.Text = "General";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(2, 2);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(864, 617);
            this.propertyGrid1.TabIndex = 0;
            // 
            // tbProxy
            // 
            this.tbProxy.Controls.Add(this.gbCustomProxy);
            this.tbProxy.Controls.Add(this.cbbProxyUsage);
            this.tbProxy.Location = new System.Drawing.Point(4, 29);
            this.tbProxy.Name = "tbProxy";
            this.tbProxy.Padding = new System.Windows.Forms.Padding(3);
            this.tbProxy.Size = new System.Drawing.Size(868, 621);
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
            this.gbCustomProxy.Location = new System.Drawing.Point(3, 31);
            this.gbCustomProxy.Name = "gbCustomProxy";
            this.gbCustomProxy.Size = new System.Drawing.Size(862, 215);
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
            this.panel3.Location = new System.Drawing.Point(0, 95);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(786, 67);
            this.panel3.TabIndex = 14;
            // 
            // txtProxyUser
            // 
            this.txtProxyUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProxyUser.Enabled = false;
            this.txtProxyUser.Location = new System.Drawing.Point(196, 3);
            this.txtProxyUser.Name = "txtProxyUser";
            this.txtProxyUser.Size = new System.Drawing.Size(586, 26);
            this.txtProxyUser.TabIndex = 5;
            // 
            // txtProxyPassword
            // 
            this.txtProxyPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProxyPassword.Enabled = false;
            this.txtProxyPassword.Location = new System.Drawing.Point(196, 35);
            this.txtProxyPassword.Name = "txtProxyPassword";
            this.txtProxyPassword.PasswordChar = '*';
            this.txtProxyPassword.Size = new System.Drawing.Size(586, 26);
            this.txtProxyPassword.TabIndex = 6;
            // 
            // lblProxyUser
            // 
            this.lblProxyUser.AutoSize = true;
            this.lblProxyUser.Location = new System.Drawing.Point(10, 6);
            this.lblProxyUser.Name = "lblProxyUser";
            this.lblProxyUser.Size = new System.Drawing.Size(43, 20);
            this.lblProxyUser.TabIndex = 7;
            this.lblProxyUser.Text = "User";
            // 
            // lblProxyPassword
            // 
            this.lblProxyPassword.AutoSize = true;
            this.lblProxyPassword.Location = new System.Drawing.Point(12, 38);
            this.lblProxyPassword.Name = "lblProxyPassword";
            this.lblProxyPassword.Size = new System.Drawing.Size(78, 20);
            this.lblProxyPassword.TabIndex = 8;
            this.lblProxyPassword.Text = "Password";
            // 
            // txtProxyAddress
            // 
            this.txtProxyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProxyAddress.Enabled = false;
            this.txtProxyAddress.Location = new System.Drawing.Point(194, 25);
            this.txtProxyAddress.Name = "txtProxyAddress";
            this.txtProxyAddress.Size = new System.Drawing.Size(668, 26);
            this.txtProxyAddress.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Bypass proxy on local";
            // 
            // lblProxyAddress
            // 
            this.lblProxyAddress.AutoSize = true;
            this.lblProxyAddress.Location = new System.Drawing.Point(6, 27);
            this.lblProxyAddress.Name = "lblProxyAddress";
            this.lblProxyAddress.Size = new System.Drawing.Size(68, 20);
            this.lblProxyAddress.TabIndex = 3;
            this.lblProxyAddress.Text = "Address";
            // 
            // chkByPassProxyOnLocal
            // 
            this.chkByPassProxyOnLocal.AutoSize = true;
            this.chkByPassProxyOnLocal.Enabled = false;
            this.chkByPassProxyOnLocal.Location = new System.Drawing.Point(194, 169);
            this.chkByPassProxyOnLocal.Name = "chkByPassProxyOnLocal";
            this.chkByPassProxyOnLocal.Size = new System.Drawing.Size(22, 21);
            this.chkByPassProxyOnLocal.TabIndex = 9;
            this.chkByPassProxyOnLocal.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbCustomAuthYes);
            this.panel2.Controls.Add(this.rbCustomAuthNo);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(0, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 33);
            this.panel2.TabIndex = 13;
            // 
            // rbCustomAuthYes
            // 
            this.rbCustomAuthYes.AutoSize = true;
            this.rbCustomAuthYes.Enabled = false;
            this.rbCustomAuthYes.Location = new System.Drawing.Point(194, 3);
            this.rbCustomAuthYes.Name = "rbCustomAuthYes";
            this.rbCustomAuthYes.Size = new System.Drawing.Size(62, 24);
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
            this.rbCustomAuthNo.Location = new System.Drawing.Point(261, 3);
            this.rbCustomAuthNo.Name = "rbCustomAuthNo";
            this.rbCustomAuthNo.Size = new System.Drawing.Size(54, 24);
            this.rbCustomAuthNo.TabIndex = 2;
            this.rbCustomAuthNo.TabStop = true;
            this.rbCustomAuthNo.Text = "No";
            this.rbCustomAuthNo.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 20);
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
            this.cbbProxyUsage.Location = new System.Drawing.Point(3, 3);
            this.cbbProxyUsage.Name = "cbbProxyUsage";
            this.cbbProxyUsage.Size = new System.Drawing.Size(862, 28);
            this.cbbProxyUsage.TabIndex = 14;
            this.cbbProxyUsage.SelectedIndexChanged += new System.EventHandler(this.cbbProxyUsage_SelectedIndexChanged);
            // 
            // tpPaths
            // 
            this.tpPaths.Controls.Add(this.lblChangePathDescription);
            this.tpPaths.Controls.Add(this.lblChangePathTitle);
            this.tpPaths.Controls.Add(this.llOpenStorageFolder);
            this.tpPaths.Controls.Add(this.llOpenRootFolder);
            this.tpPaths.Location = new System.Drawing.Point(4, 29);
            this.tpPaths.Margin = new System.Windows.Forms.Padding(5);
            this.tpPaths.Name = "tpPaths";
            this.tpPaths.Padding = new System.Windows.Forms.Padding(5);
            this.tpPaths.Size = new System.Drawing.Size(868, 621);
            this.tpPaths.TabIndex = 3;
            this.tpPaths.Text = "Paths";
            this.tpPaths.UseVisualStyleBackColor = true;
            // 
            // lblChangePathDescription
            // 
            this.lblChangePathDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChangePathDescription.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangePathDescription.Location = new System.Drawing.Point(5, 127);
            this.lblChangePathDescription.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblChangePathDescription.Name = "lblChangePathDescription";
            this.lblChangePathDescription.Size = new System.Drawing.Size(858, 489);
            this.lblChangePathDescription.TabIndex = 4;
            this.lblChangePathDescription.Text = resources.GetString("lblChangePathDescription.Text");
            // 
            // lblChangePathTitle
            // 
            this.lblChangePathTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChangePathTitle.Font = new System.Drawing.Font("Segoe UI Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangePathTitle.Location = new System.Drawing.Point(5, 85);
            this.lblChangePathTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblChangePathTitle.Name = "lblChangePathTitle";
            this.lblChangePathTitle.Size = new System.Drawing.Size(858, 42);
            this.lblChangePathTitle.TabIndex = 3;
            this.lblChangePathTitle.Text = "How to change XrmToolBox storage folder";
            // 
            // llOpenStorageFolder
            // 
            this.llOpenStorageFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.llOpenStorageFolder.Location = new System.Drawing.Point(5, 45);
            this.llOpenStorageFolder.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.llOpenStorageFolder.Name = "llOpenStorageFolder";
            this.llOpenStorageFolder.Size = new System.Drawing.Size(858, 40);
            this.llOpenStorageFolder.TabIndex = 1;
            this.llOpenStorageFolder.TabStop = true;
            this.llOpenStorageFolder.Text = "Open XrmToolBox storage folder";
            this.llOpenStorageFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOpenStorageFolder_LinkClicked);
            // 
            // llOpenRootFolder
            // 
            this.llOpenRootFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.llOpenRootFolder.Location = new System.Drawing.Point(5, 5);
            this.llOpenRootFolder.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.llOpenRootFolder.Name = "llOpenRootFolder";
            this.llOpenRootFolder.Size = new System.Drawing.Size(858, 40);
            this.llOpenRootFolder.TabIndex = 0;
            this.llOpenRootFolder.TabStop = true;
            this.llOpenRootFolder.Text = "Open XrmToolBox folder";
            this.llOpenRootFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOpenRootFolder_LinkClicked);
            // 
            // tbDataCollect
            // 
            this.tbDataCollect.Controls.Add(this.chkOptinAI);
            this.tbDataCollect.Controls.Add(this.label6);
            this.tbDataCollect.Location = new System.Drawing.Point(4, 29);
            this.tbDataCollect.Margin = new System.Windows.Forms.Padding(2);
            this.tbDataCollect.Name = "tbDataCollect";
            this.tbDataCollect.Padding = new System.Windows.Forms.Padding(2);
            this.tbDataCollect.Size = new System.Drawing.Size(868, 621);
            this.tbDataCollect.TabIndex = 5;
            this.tbDataCollect.Text = "Data collect";
            this.tbDataCollect.UseVisualStyleBackColor = true;
            // 
            // chkOptinAI
            // 
            this.chkOptinAI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOptinAI.AutoSize = true;
            this.chkOptinAI.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOptinAI.Checked = true;
            this.chkOptinAI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOptinAI.Location = new System.Drawing.Point(267, 585);
            this.chkOptinAI.Name = "chkOptinAI";
            this.chkOptinAI.Size = new System.Drawing.Size(517, 24);
            this.chkOptinAI.TabIndex = 2;
            this.chkOptinAI.Text = "I allow XrmToolBox to send anonymous data for statistics usage only";
            this.chkOptinAI.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(2, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(864, 617);
            this.label6.TabIndex = 1;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // tpAssemblies
            // 
            this.tpAssemblies.Controls.Add(this.lvAssemblies);
            this.tpAssemblies.Location = new System.Drawing.Point(4, 29);
            this.tpAssemblies.Name = "tpAssemblies";
            this.tpAssemblies.Padding = new System.Windows.Forms.Padding(3);
            this.tpAssemblies.Size = new System.Drawing.Size(868, 621);
            this.tpAssemblies.TabIndex = 7;
            this.tpAssemblies.Text = "Assemblies";
            this.tpAssemblies.UseVisualStyleBackColor = true;
            // 
            // lvAssemblies
            // 
            this.lvAssemblies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAssembly,
            this.chVersion});
            this.lvAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAssemblies.HideSelection = false;
            this.lvAssemblies.Location = new System.Drawing.Point(3, 3);
            this.lvAssemblies.Name = "lvAssemblies";
            this.lvAssemblies.Size = new System.Drawing.Size(862, 615);
            this.lvAssemblies.TabIndex = 0;
            this.lvAssemblies.UseCompatibleStateImageBehavior = false;
            this.lvAssemblies.View = System.Windows.Forms.View.Details;
            // 
            // chAssembly
            // 
            this.chAssembly.Text = "Assembly";
            this.chAssembly.Width = 400;
            // 
            // chVersion
            // 
            this.chVersion.Text = "Version";
            this.chVersion.Width = 150;
            // 
            // tbAppProtocol
            // 
            this.tbAppProtocol.Controls.Add(this.panel4);
            this.tbAppProtocol.Controls.Add(this.pnlAppProtocolButton);
            this.tbAppProtocol.Controls.Add(this.pnlAppProtocolPath);
            this.tbAppProtocol.Controls.Add(this.pnlAppProtocolStatus);
            this.tbAppProtocol.Controls.Add(this.pnlAppProtocolTop);
            this.tbAppProtocol.Location = new System.Drawing.Point(4, 29);
            this.tbAppProtocol.Name = "tbAppProtocol";
            this.tbAppProtocol.Padding = new System.Windows.Forms.Padding(3);
            this.tbAppProtocol.Size = new System.Drawing.Size(868, 621);
            this.tbAppProtocol.TabIndex = 8;
            this.tbAppProtocol.Text = "Application protocol";
            this.tbAppProtocol.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.llProtocolDoc);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 325);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10);
            this.panel4.Size = new System.Drawing.Size(862, 293);
            this.panel4.TabIndex = 3;
            // 
            // llProtocolDoc
            // 
            this.llProtocolDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llProtocolDoc.AutoSize = true;
            this.llProtocolDoc.Location = new System.Drawing.Point(296, 263);
            this.llProtocolDoc.Name = "llProtocolDoc";
            this.llProtocolDoc.Size = new System.Drawing.Size(556, 20);
            this.llProtocolDoc.TabIndex = 0;
            this.llProtocolDoc.TabStop = true;
            this.llProtocolDoc.Text = "Click here to read documentation about how to implement application protocol";
            this.llProtocolDoc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llProtocolDoc_LinkClicked);
            // 
            // pnlAppProtocolButton
            // 
            this.pnlAppProtocolButton.Controls.Add(this.btnAppProtocol);
            this.pnlAppProtocolButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAppProtocolButton.Location = new System.Drawing.Point(3, 177);
            this.pnlAppProtocolButton.Name = "pnlAppProtocolButton";
            this.pnlAppProtocolButton.Padding = new System.Windows.Forms.Padding(40);
            this.pnlAppProtocolButton.Size = new System.Drawing.Size(862, 148);
            this.pnlAppProtocolButton.TabIndex = 2;
            // 
            // btnAppProtocol
            // 
            this.btnAppProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAppProtocol.Location = new System.Drawing.Point(40, 40);
            this.btnAppProtocol.Name = "btnAppProtocol";
            this.btnAppProtocol.Size = new System.Drawing.Size(782, 68);
            this.btnAppProtocol.TabIndex = 0;
            this.btnAppProtocol.Text = "Enable";
            this.btnAppProtocol.UseVisualStyleBackColor = true;
            this.btnAppProtocol.Click += new System.EventHandler(this.btnAppProtocol_Click);
            // 
            // pnlAppProtocolPath
            // 
            this.pnlAppProtocolPath.Controls.Add(this.lblAppProtocolPath);
            this.pnlAppProtocolPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAppProtocolPath.Location = new System.Drawing.Point(3, 151);
            this.pnlAppProtocolPath.Name = "pnlAppProtocolPath";
            this.pnlAppProtocolPath.Padding = new System.Windows.Forms.Padding(27, 4, 27, 4);
            this.pnlAppProtocolPath.Size = new System.Drawing.Size(862, 26);
            this.pnlAppProtocolPath.TabIndex = 4;
            // 
            // lblAppProtocolPath
            // 
            this.lblAppProtocolPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppProtocolPath.Location = new System.Drawing.Point(27, 4);
            this.lblAppProtocolPath.Name = "lblAppProtocolPath";
            this.lblAppProtocolPath.Size = new System.Drawing.Size(808, 18);
            this.lblAppProtocolPath.TabIndex = 1;
            this.lblAppProtocolPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAppProtocolStatus
            // 
            this.pnlAppProtocolStatus.Controls.Add(this.lblAppProtocolStatus);
            this.pnlAppProtocolStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAppProtocolStatus.Location = new System.Drawing.Point(3, 97);
            this.pnlAppProtocolStatus.Name = "pnlAppProtocolStatus";
            this.pnlAppProtocolStatus.Padding = new System.Windows.Forms.Padding(10);
            this.pnlAppProtocolStatus.Size = new System.Drawing.Size(862, 54);
            this.pnlAppProtocolStatus.TabIndex = 1;
            // 
            // lblAppProtocolStatus
            // 
            this.lblAppProtocolStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppProtocolStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppProtocolStatus.Location = new System.Drawing.Point(10, 10);
            this.lblAppProtocolStatus.Name = "lblAppProtocolStatus";
            this.lblAppProtocolStatus.Size = new System.Drawing.Size(842, 34);
            this.lblAppProtocolStatus.TabIndex = 0;
            this.lblAppProtocolStatus.Tag = "Current Status : {0}";
            this.lblAppProtocolStatus.Text = "Current Status";
            this.lblAppProtocolStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlAppProtocolTop
            // 
            this.pnlAppProtocolTop.Controls.Add(this.lblAppProtocolSummary);
            this.pnlAppProtocolTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAppProtocolTop.Location = new System.Drawing.Point(3, 3);
            this.pnlAppProtocolTop.Name = "pnlAppProtocolTop";
            this.pnlAppProtocolTop.Padding = new System.Windows.Forms.Padding(10);
            this.pnlAppProtocolTop.Size = new System.Drawing.Size(862, 94);
            this.pnlAppProtocolTop.TabIndex = 0;
            // 
            // lblAppProtocolSummary
            // 
            this.lblAppProtocolSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAppProtocolSummary.Location = new System.Drawing.Point(10, 10);
            this.lblAppProtocolSummary.Name = "lblAppProtocolSummary";
            this.lblAppProtocolSummary.Size = new System.Drawing.Size(842, 68);
            this.lblAppProtocolSummary.TabIndex = 0;
            this.lblAppProtocolSummary.Text = resources.GetString("lblAppProtocolSummary.Text");
            // 
            // tpCredits
            // 
            this.tpCredits.Controls.Add(this.rtbCredits);
            this.tpCredits.Location = new System.Drawing.Point(4, 29);
            this.tpCredits.Name = "tpCredits";
            this.tpCredits.Padding = new System.Windows.Forms.Padding(3);
            this.tpCredits.Size = new System.Drawing.Size(868, 621);
            this.tpCredits.TabIndex = 9;
            this.tpCredits.Text = "Credits";
            this.tpCredits.UseVisualStyleBackColor = true;
            // 
            // rtbCredits
            // 
            this.rtbCredits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCredits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCredits.Location = new System.Drawing.Point(3, 3);
            this.rtbCredits.Name = "rtbCredits";
            this.rtbCredits.ReadOnly = true;
            this.rtbCredits.Size = new System.Drawing.Size(862, 615);
            this.rtbCredits.TabIndex = 0;
            this.rtbCredits.Text = resources.GetString("rtbCredits.Text");
            this.rtbCredits.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbCredits_LinkClicked);
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(876, 824);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tbProxy.ResumeLayout(false);
            this.gbCustomProxy.ResumeLayout(false);
            this.gbCustomProxy.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tpPaths.ResumeLayout(false);
            this.tbDataCollect.ResumeLayout(false);
            this.tbDataCollect.PerformLayout();
            this.tpAssemblies.ResumeLayout(false);
            this.tbAppProtocol.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlAppProtocolButton.ResumeLayout(false);
            this.pnlAppProtocolPath.ResumeLayout(false);
            this.pnlAppProtocolStatus.ResumeLayout(false);
            this.pnlAppProtocolTop.ResumeLayout(false);
            this.tpCredits.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tpPaths;
        private System.Windows.Forms.LinkLabel llOpenRootFolder;
        private System.Windows.Forms.Label lblChangePathDescription;
        private System.Windows.Forms.Label lblChangePathTitle;
        private System.Windows.Forms.LinkLabel llOpenStorageFolder;
        private System.Windows.Forms.TabPage tbDataCollect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.CheckBox chkOptinAI;
        private System.Windows.Forms.TabPage tpAssemblies;
        private System.Windows.Forms.ListView lvAssemblies;
        private System.Windows.Forms.ColumnHeader chAssembly;
        private System.Windows.Forms.ColumnHeader chVersion;
        private System.Windows.Forms.TabPage tbAppProtocol;
        private System.Windows.Forms.Panel pnlAppProtocolStatus;
        private System.Windows.Forms.Label lblAppProtocolStatus;
        private System.Windows.Forms.Panel pnlAppProtocolTop;
        private System.Windows.Forms.Label lblAppProtocolSummary;
        private System.Windows.Forms.Panel pnlAppProtocolButton;
        private System.Windows.Forms.Button btnAppProtocol;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblAppProtocolPath;
        private System.Windows.Forms.Panel pnlAppProtocolPath;
        private System.Windows.Forms.LinkLabel llProtocolDoc;
        private System.Windows.Forms.TabPage tpCredits;
        private System.Windows.Forms.RichTextBox rtbCredits;
    }
}