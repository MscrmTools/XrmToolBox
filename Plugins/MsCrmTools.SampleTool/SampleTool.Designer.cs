namespace MsCrmTools.SampleTool
{
    partial class SampleTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampleTool));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbMultipleCalls = new System.Windows.Forms.CheckBox();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbWhoAmI = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDuplicate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.txtState = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.btnCheckMultiSample = new System.Windows.Forms.Button();
            this.btnDoSomethingWrong = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOpenNotif = new System.Windows.Forms.Button();
            this.gbToast = new System.Windows.Forms.GroupBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.gbErrorHandling = new System.Windows.Forms.GroupBox();
            this.btnShowNotif = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbNotification = new System.Windows.Forms.GroupBox();
            this.lblNotifType = new System.Windows.Forms.Label();
            this.cbbNotifType = new System.Windows.Forms.ComboBox();
            this.lblNotifMessage = new System.Windows.Forms.Label();
            this.txtNotifMessage = new System.Windows.Forms.TextBox();
            this.lblNotifAction = new System.Windows.Forms.Label();
            this.cbbNotifActions = new System.Windows.Forms.ComboBox();
            this.lblNotifButtonText = new System.Windows.Forms.Label();
            this.txtNotifButton = new System.Windows.Forms.TextBox();
            this.txtNotifLink = new System.Windows.Forms.TextBox();
            this.lblNotifLinkText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkNotifCanBeClosed = new System.Windows.Forms.CheckBox();
            this.notificationControl1 = new XrmToolBox.Extensibility.UserControls.NotificationControl();
            this.lblNotifAutoClose = new System.Windows.Forms.Label();
            this.nudNotif = new System.Windows.Forms.NumericUpDown();
            this.gbOptions.SuspendLayout();
            this.toolStripMenu.SuspendLayout();
            this.gbToast.SuspendLayout();
            this.gbErrorHandling.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbNotification.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNotif)).BeginInit();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbMultipleCalls);
            this.gbOptions.Location = new System.Drawing.Point(3, 7);
            this.gbOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbOptions.Size = new System.Drawing.Size(375, 60);
            this.gbOptions.TabIndex = 3;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // cbMultipleCalls
            // 
            this.cbMultipleCalls.AutoSize = true;
            this.cbMultipleCalls.Location = new System.Drawing.Point(18, 22);
            this.cbMultipleCalls.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbMultipleCalls.Name = "cbMultipleCalls";
            this.cbMultipleCalls.Size = new System.Drawing.Size(106, 20);
            this.cbMultipleCalls.TabIndex = 0;
            this.cbMultipleCalls.Text = "Multiple calls";
            this.cbMultipleCalls.UseVisualStyleBackColor = true;
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator2,
            this.tsbWhoAmI,
            this.toolStripSeparator1,
            this.tsbCancel,
            this.toolStripSeparator3,
            this.tsbDuplicate,
            this.toolStripSeparator4});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1238, 27);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(29, 24);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbWhoAmI
            // 
            this.tsbWhoAmI.Image = ((System.Drawing.Image)(resources.GetObject("tsbWhoAmI.Image")));
            this.tsbWhoAmI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWhoAmI.Name = "tsbWhoAmI";
            this.tsbWhoAmI.Size = new System.Drawing.Size(93, 24);
            this.tsbWhoAmI.Text = "Who am I";
            this.tsbWhoAmI.ToolTipText = "Perfomrs a Who I Am request";
            this.tsbWhoAmI.Click += new System.EventHandler(this.tsbWhoAmI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbCancel
            // 
            this.tsbCancel.Enabled = false;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(73, 24);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.ToolTipText = "Cancel the current request";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbDuplicate
            // 
            this.tsbDuplicate.Image = ((System.Drawing.Image)(resources.GetObject("tsbDuplicate.Image")));
            this.tsbDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDuplicate.Name = "tsbDuplicate";
            this.tsbDuplicate.Size = new System.Drawing.Size(93, 24);
            this.tsbDuplicate.Text = "Duplicate";
            this.tsbDuplicate.Click += new System.EventHandler(this.tsbDuplicate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(434, 95);
            this.txtState.Margin = new System.Windows.Forms.Padding(2);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(217, 22);
            this.txtState.TabIndex = 5;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(431, 71);
            this.lblState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(38, 16);
            this.lblState.TabIndex = 6;
            this.lblState.Text = "State";
            // 
            // btnCheckMultiSample
            // 
            this.btnCheckMultiSample.Location = new System.Drawing.Point(434, 19);
            this.btnCheckMultiSample.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCheckMultiSample.Name = "btnCheckMultiSample";
            this.btnCheckMultiSample.Size = new System.Drawing.Size(386, 30);
            this.btnCheckMultiSample.TabIndex = 7;
            this.btnCheckMultiSample.Text = "Is Sample Tool for multiconnection installed?";
            this.btnCheckMultiSample.UseVisualStyleBackColor = true;
            this.btnCheckMultiSample.Click += new System.EventHandler(this.btnCheckMultiSample_Click);
            // 
            // btnDoSomethingWrong
            // 
            this.btnDoSomethingWrong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDoSomethingWrong.Location = new System.Drawing.Point(6, 21);
            this.btnDoSomethingWrong.Margin = new System.Windows.Forms.Padding(4);
            this.btnDoSomethingWrong.Name = "btnDoSomethingWrong";
            this.btnDoSomethingWrong.Size = new System.Drawing.Size(363, 44);
            this.btnDoSomethingWrong.TabIndex = 8;
            this.btnDoSomethingWrong.Text = "Do something wrong";
            this.btnDoSomethingWrong.UseVisualStyleBackColor = true;
            this.btnDoSomethingWrong.Click += new System.EventHandler(this.btnDoSomethingWrong_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(6, 65);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(363, 48);
            this.button1.TabIndex = 9;
            this.button1.Text = "Do something wrong without catch";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOpenNotif
            // 
            this.btnOpenNotif.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnOpenNotif.Location = new System.Drawing.Point(6, 143);
            this.btnOpenNotif.Name = "btnOpenNotif";
            this.btnOpenNotif.Size = new System.Drawing.Size(363, 36);
            this.btnOpenNotif.TabIndex = 10;
            this.btnOpenNotif.Text = "Open Toast Notification";
            this.btnOpenNotif.UseVisualStyleBackColor = true;
            this.btnOpenNotif.Click += new System.EventHandler(this.btnOpenNotif_Click);
            // 
            // gbToast
            // 
            this.gbToast.Controls.Add(this.textBox1);
            this.gbToast.Controls.Add(this.btnOpenNotif);
            this.gbToast.Controls.Add(this.lblMessage);
            this.gbToast.Controls.Add(this.txtHeader);
            this.gbToast.Controls.Add(this.lblHeader);
            this.gbToast.Location = new System.Drawing.Point(3, 200);
            this.gbToast.Name = "gbToast";
            this.gbToast.Padding = new System.Windows.Forms.Padding(6);
            this.gbToast.Size = new System.Drawing.Size(375, 185);
            this.gbToast.TabIndex = 11;
            this.gbToast.TabStop = false;
            this.gbToast.Text = "Toast Notification";
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Location = new System.Drawing.Point(6, 21);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(363, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Header";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHeader
            // 
            this.txtHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtHeader.Location = new System.Drawing.Point(6, 51);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(363, 22);
            this.txtHeader.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(6, 103);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(363, 22);
            this.textBox1.TabIndex = 3;
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMessage.Location = new System.Drawing.Point(6, 73);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(363, 30);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbErrorHandling
            // 
            this.gbErrorHandling.Controls.Add(this.button1);
            this.gbErrorHandling.Controls.Add(this.btnDoSomethingWrong);
            this.gbErrorHandling.Location = new System.Drawing.Point(3, 74);
            this.gbErrorHandling.Name = "gbErrorHandling";
            this.gbErrorHandling.Padding = new System.Windows.Forms.Padding(6);
            this.gbErrorHandling.Size = new System.Drawing.Size(375, 123);
            this.gbErrorHandling.TabIndex = 12;
            this.gbErrorHandling.TabStop = false;
            this.gbErrorHandling.Text = "Error management";
            // 
            // btnShowNotif
            // 
            this.btnShowNotif.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnShowNotif.Location = new System.Drawing.Point(3, 566);
            this.btnShowNotif.Name = "btnShowNotif";
            this.btnShowNotif.Size = new System.Drawing.Size(357, 49);
            this.btnShowNotif.TabIndex = 14;
            this.btnShowNotif.Text = "Show Notification (New)";
            this.btnShowNotif.UseVisualStyleBackColor = true;
            this.btnShowNotif.Click += new System.EventHandler(this.btnShowNotif_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbNotification);
            this.panel1.Controls.Add(this.btnCheckMultiSample);
            this.panel1.Controls.Add(this.gbOptions);
            this.panel1.Controls.Add(this.txtState);
            this.panel1.Controls.Add(this.gbErrorHandling);
            this.panel1.Controls.Add(this.lblState);
            this.panel1.Controls.Add(this.gbToast);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1238, 682);
            this.panel1.TabIndex = 15;
            // 
            // gbNotification
            // 
            this.gbNotification.Controls.Add(this.nudNotif);
            this.gbNotification.Controls.Add(this.lblNotifAutoClose);
            this.gbNotification.Controls.Add(this.chkNotifCanBeClosed);
            this.gbNotification.Controls.Add(this.label1);
            this.gbNotification.Controls.Add(this.txtNotifLink);
            this.gbNotification.Controls.Add(this.lblNotifLinkText);
            this.gbNotification.Controls.Add(this.txtNotifButton);
            this.gbNotification.Controls.Add(this.lblNotifButtonText);
            this.gbNotification.Controls.Add(this.cbbNotifActions);
            this.gbNotification.Controls.Add(this.lblNotifAction);
            this.gbNotification.Controls.Add(this.txtNotifMessage);
            this.gbNotification.Controls.Add(this.lblNotifMessage);
            this.gbNotification.Controls.Add(this.cbbNotifType);
            this.gbNotification.Controls.Add(this.lblNotifType);
            this.gbNotification.Controls.Add(this.btnShowNotif);
            this.gbNotification.Location = new System.Drawing.Point(849, 7);
            this.gbNotification.Name = "gbNotification";
            this.gbNotification.Size = new System.Drawing.Size(363, 618);
            this.gbNotification.TabIndex = 15;
            this.gbNotification.TabStop = false;
            this.gbNotification.Text = "Notification";
            // 
            // lblNotifType
            // 
            this.lblNotifType.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotifType.Location = new System.Drawing.Point(3, 18);
            this.lblNotifType.Name = "lblNotifType";
            this.lblNotifType.Size = new System.Drawing.Size(357, 41);
            this.lblNotifType.TabIndex = 15;
            this.lblNotifType.Text = "Type";
            this.lblNotifType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbNotifType
            // 
            this.cbbNotifType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbNotifType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNotifType.FormattingEnabled = true;
            this.cbbNotifType.Location = new System.Drawing.Point(3, 59);
            this.cbbNotifType.Name = "cbbNotifType";
            this.cbbNotifType.Size = new System.Drawing.Size(357, 24);
            this.cbbNotifType.TabIndex = 16;
            // 
            // lblNotifMessage
            // 
            this.lblNotifMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotifMessage.Location = new System.Drawing.Point(3, 83);
            this.lblNotifMessage.Name = "lblNotifMessage";
            this.lblNotifMessage.Size = new System.Drawing.Size(357, 41);
            this.lblNotifMessage.TabIndex = 17;
            this.lblNotifMessage.Text = "Message";
            this.lblNotifMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNotifMessage
            // 
            this.txtNotifMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtNotifMessage.Location = new System.Drawing.Point(3, 124);
            this.txtNotifMessage.Multiline = true;
            this.txtNotifMessage.Name = "txtNotifMessage";
            this.txtNotifMessage.Size = new System.Drawing.Size(357, 66);
            this.txtNotifMessage.TabIndex = 18;
            this.txtNotifMessage.Text = "A sample notification";
            // 
            // lblNotifAction
            // 
            this.lblNotifAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotifAction.Location = new System.Drawing.Point(3, 190);
            this.lblNotifAction.Name = "lblNotifAction";
            this.lblNotifAction.Size = new System.Drawing.Size(357, 41);
            this.lblNotifAction.TabIndex = 19;
            this.lblNotifAction.Text = "Actions";
            this.lblNotifAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbNotifActions
            // 
            this.cbbNotifActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbNotifActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbNotifActions.FormattingEnabled = true;
            this.cbbNotifActions.Location = new System.Drawing.Point(3, 231);
            this.cbbNotifActions.Name = "cbbNotifActions";
            this.cbbNotifActions.Size = new System.Drawing.Size(357, 24);
            this.cbbNotifActions.TabIndex = 20;
            // 
            // lblNotifButtonText
            // 
            this.lblNotifButtonText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotifButtonText.Location = new System.Drawing.Point(3, 255);
            this.lblNotifButtonText.Name = "lblNotifButtonText";
            this.lblNotifButtonText.Size = new System.Drawing.Size(357, 41);
            this.lblNotifButtonText.TabIndex = 21;
            this.lblNotifButtonText.Text = "Button text";
            this.lblNotifButtonText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNotifButton
            // 
            this.txtNotifButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtNotifButton.Location = new System.Drawing.Point(3, 296);
            this.txtNotifButton.Name = "txtNotifButton";
            this.txtNotifButton.Size = new System.Drawing.Size(357, 22);
            this.txtNotifButton.TabIndex = 22;
            this.txtNotifButton.Text = "Click me";
            // 
            // txtNotifLink
            // 
            this.txtNotifLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtNotifLink.Location = new System.Drawing.Point(3, 359);
            this.txtNotifLink.Name = "txtNotifLink";
            this.txtNotifLink.Size = new System.Drawing.Size(357, 22);
            this.txtNotifLink.TabIndex = 24;
            this.txtNotifLink.Text = "Read more";
            // 
            // lblNotifLinkText
            // 
            this.lblNotifLinkText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotifLinkText.Location = new System.Drawing.Point(3, 318);
            this.lblNotifLinkText.Name = "lblNotifLinkText";
            this.lblNotifLinkText.Size = new System.Drawing.Size(357, 41);
            this.lblNotifLinkText.TabIndex = 23;
            this.lblNotifLinkText.Text = "Link text";
            this.lblNotifLinkText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 41);
            this.label1.TabIndex = 25;
            this.label1.Text = "Can be closed";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkNotifCanBeClosed
            // 
            this.chkNotifCanBeClosed.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkNotifCanBeClosed.Location = new System.Drawing.Point(3, 422);
            this.chkNotifCanBeClosed.Name = "chkNotifCanBeClosed";
            this.chkNotifCanBeClosed.Size = new System.Drawing.Size(357, 38);
            this.chkNotifCanBeClosed.TabIndex = 26;
            this.chkNotifCanBeClosed.UseVisualStyleBackColor = true;
            // 
            // notificationControl1
            // 
            this.notificationControl1.Action = XrmToolBox.Extensibility.UserControls.NotificationAction.None;
            this.notificationControl1.ButtonText = "Action";
            this.notificationControl1.CanBeClosed = true;
            this.notificationControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.notificationControl1.LinkText = "Learn more";
            this.notificationControl1.Location = new System.Drawing.Point(0, 27);
            this.notificationControl1.Message = "Message";
            this.notificationControl1.Name = "notificationControl1";
            this.notificationControl1.Padding = new System.Windows.Forms.Padding(4);
            this.notificationControl1.Size = new System.Drawing.Size(1238, 36);
            this.notificationControl1.TabIndex = 13;
            this.notificationControl1.Type = XrmToolBox.Extensibility.UserControls.NotificationType.Info;
            this.notificationControl1.Visible = false;
            // 
            // lblNotifAutoClose
            // 
            this.lblNotifAutoClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotifAutoClose.Location = new System.Drawing.Point(3, 460);
            this.lblNotifAutoClose.Name = "lblNotifAutoClose";
            this.lblNotifAutoClose.Size = new System.Drawing.Size(357, 41);
            this.lblNotifAutoClose.TabIndex = 27;
            this.lblNotifAutoClose.Text = "Delay before auto close (s)";
            this.lblNotifAutoClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudNotif
            // 
            this.nudNotif.Dock = System.Windows.Forms.DockStyle.Top;
            this.nudNotif.Location = new System.Drawing.Point(3, 501);
            this.nudNotif.Name = "nudNotif";
            this.nudNotif.Size = new System.Drawing.Size(357, 22);
            this.nudNotif.TabIndex = 28;
            this.nudNotif.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // SampleTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.notificationControl1);
            this.Controls.Add(this.toolStripMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SampleTool";
            this.PluginIcon = ((System.Drawing.Icon)(resources.GetObject("$this.PluginIcon")));
            this.Size = new System.Drawing.Size(1238, 745);
            this.Load += new System.EventHandler(this.SampleTool_Load);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.gbToast.ResumeLayout(false);
            this.gbToast.PerformLayout();
            this.gbErrorHandling.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbNotification.ResumeLayout(false);
            this.gbNotification.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNotif)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox cbMultipleCalls;
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbWhoAmI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.ToolStripButton tsbDuplicate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button btnCheckMultiSample;
        private System.Windows.Forms.Button btnDoSomethingWrong;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnOpenNotif;
        private System.Windows.Forms.GroupBox gbToast;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox gbErrorHandling;
        private XrmToolBox.Extensibility.UserControls.NotificationControl notificationControl1;
        private System.Windows.Forms.Button btnShowNotif;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbNotification;
        private System.Windows.Forms.ComboBox cbbNotifType;
        private System.Windows.Forms.Label lblNotifType;
        private System.Windows.Forms.ComboBox cbbNotifActions;
        private System.Windows.Forms.Label lblNotifAction;
        private System.Windows.Forms.TextBox txtNotifMessage;
        private System.Windows.Forms.Label lblNotifMessage;
        private System.Windows.Forms.TextBox txtNotifLink;
        private System.Windows.Forms.Label lblNotifLinkText;
        private System.Windows.Forms.TextBox txtNotifButton;
        private System.Windows.Forms.Label lblNotifButtonText;
        private System.Windows.Forms.CheckBox chkNotifCanBeClosed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudNotif;
        private System.Windows.Forms.Label lblNotifAutoClose;
    }
}
