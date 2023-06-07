namespace XrmToolBox.ToolLibrary.UserControls
{
    partial class ToolPackageCtrl
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblToolName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbOpenTool = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.lblTitleReleaseNotes = new System.Windows.Forms.Label();
            this.rtbReleaseNotes = new System.Windows.Forms.RichTextBox();
            this.lblTitleDesc = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitleVersion = new System.Windows.Forms.Label();
            this.lblTitleAuthors = new System.Windows.Forms.Label();
            this.lblTitleFirstRelease = new System.Windows.Forms.Label();
            this.lblTitleLatestRelease = new System.Windows.Forms.Label();
            this.lblDownload = new System.Windows.Forms.Label();
            this.lblTitlePRojectUrl = new System.Windows.Forms.Label();
            this.lblTitleRating = new System.Windows.Forms.Label();
            this.lblToolAuthors = new System.Windows.Forms.Label();
            this.lblToolFirstRelease = new System.Windows.Forms.Label();
            this.lblToolLastRelease = new System.Windows.Forms.Label();
            this.lblToolDownload = new System.Windows.Forms.Label();
            this.llToolProjectUrl = new System.Windows.Forms.LinkLabel();
            this.pnlRating = new System.Windows.Forms.Panel();
            this.llRateThisTool = new System.Windows.Forms.LinkLabel();
            this.lblToolRating = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbbVersions = new System.Windows.Forms.ComboBox();
            this.pnlSeparator1 = new System.Windows.Forms.Panel();
            this.lblIncompatibleReason = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.tlpActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenTool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.pnlRating.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlSeparator1.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.tlpActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblToolName);
            this.pnlTop.Controls.Add(this.panel1);
            this.pnlTop.Controls.Add(this.pbLogo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(30, 31);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1628, 92);
            this.pnlTop.TabIndex = 0;
            // 
            // lblToolName
            // 
            this.lblToolName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolName.Location = new System.Drawing.Point(90, 0);
            this.lblToolName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToolName.Name = "lblToolName";
            this.lblToolName.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.lblToolName.Size = new System.Drawing.Size(1506, 92);
            this.lblToolName.TabIndex = 5;
            this.lblToolName.Text = "[Tool name]";
            this.lblToolName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbOpenTool);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1596, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(32, 92);
            this.panel1.TabIndex = 4;
            // 
            // pbOpenTool
            // 
            this.pbOpenTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbOpenTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbOpenTool.Image = global::XrmToolBox.ToolLibrary.Resource.Open16;
            this.pbOpenTool.Location = new System.Drawing.Point(0, 0);
            this.pbOpenTool.Name = "pbOpenTool";
            this.pbOpenTool.Size = new System.Drawing.Size(32, 32);
            this.pbOpenTool.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOpenTool.TabIndex = 3;
            this.pbOpenTool.TabStop = false;
            this.pbOpenTool.Click += new System.EventHandler(this.pbOpenTool_Click);
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(90, 92);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.rtbDescription);
            this.pnlMain.Controls.Add(this.lblTitleReleaseNotes);
            this.pnlMain.Controls.Add(this.rtbReleaseNotes);
            this.pnlMain.Controls.Add(this.lblTitleDesc);
            this.pnlMain.Controls.Add(this.tlpMain);
            this.pnlMain.Controls.Add(this.pnlSeparator1);
            this.pnlMain.Controls.Add(this.pnlActions);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(30, 123);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 31, 4, 5);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1628, 1152);
            this.pnlMain.TabIndex = 1;
            // 
            // rtbDescription
            // 
            this.rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDescription.Location = new System.Drawing.Point(0, 549);
            this.rtbDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.ReadOnly = true;
            this.rtbDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbDescription.Size = new System.Drawing.Size(1628, 347);
            this.rtbDescription.TabIndex = 8;
            this.rtbDescription.Text = "";
            this.rtbDescription.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.rtbDescription_ContentsResized);
            // 
            // lblTitleReleaseNotes
            // 
            this.lblTitleReleaseNotes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTitleReleaseNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleReleaseNotes.Location = new System.Drawing.Point(0, 896);
            this.lblTitleReleaseNotes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleReleaseNotes.Name = "lblTitleReleaseNotes";
            this.lblTitleReleaseNotes.Size = new System.Drawing.Size(1628, 50);
            this.lblTitleReleaseNotes.TabIndex = 5;
            this.lblTitleReleaseNotes.Text = "Release notes";
            this.lblTitleReleaseNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rtbReleaseNotes
            // 
            this.rtbReleaseNotes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbReleaseNotes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbReleaseNotes.Location = new System.Drawing.Point(0, 946);
            this.rtbReleaseNotes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rtbReleaseNotes.Name = "rtbReleaseNotes";
            this.rtbReleaseNotes.ReadOnly = true;
            this.rtbReleaseNotes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbReleaseNotes.Size = new System.Drawing.Size(1628, 206);
            this.rtbReleaseNotes.TabIndex = 6;
            this.rtbReleaseNotes.Text = "";
            this.rtbReleaseNotes.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbReleaseNotes_LinkClicked);
            // 
            // lblTitleDesc
            // 
            this.lblTitleDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitleDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleDesc.Location = new System.Drawing.Point(0, 477);
            this.lblTitleDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleDesc.Name = "lblTitleDesc";
            this.lblTitleDesc.Size = new System.Drawing.Size(1628, 72);
            this.lblTitleDesc.TabIndex = 0;
            this.lblTitleDesc.Text = "Description";
            this.lblTitleDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.83908F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.16092F));
            this.tlpMain.Controls.Add(this.lblTitleVersion, 0, 0);
            this.tlpMain.Controls.Add(this.lblTitleAuthors, 0, 1);
            this.tlpMain.Controls.Add(this.lblTitleFirstRelease, 0, 2);
            this.tlpMain.Controls.Add(this.lblTitleLatestRelease, 0, 3);
            this.tlpMain.Controls.Add(this.lblDownload, 0, 4);
            this.tlpMain.Controls.Add(this.lblTitlePRojectUrl, 0, 5);
            this.tlpMain.Controls.Add(this.lblTitleRating, 0, 6);
            this.tlpMain.Controls.Add(this.lblToolAuthors, 1, 1);
            this.tlpMain.Controls.Add(this.lblToolFirstRelease, 1, 2);
            this.tlpMain.Controls.Add(this.lblToolLastRelease, 1, 3);
            this.tlpMain.Controls.Add(this.lblToolDownload, 1, 4);
            this.tlpMain.Controls.Add(this.llToolProjectUrl, 1, 5);
            this.tlpMain.Controls.Add(this.pnlRating, 1, 6);
            this.tlpMain.Controls.Add(this.panel2, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpMain.Location = new System.Drawing.Point(0, 117);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 8;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(1628, 360);
            this.tlpMain.TabIndex = 3;
            // 
            // lblTitleVersion
            // 
            this.lblTitleVersion.AutoSize = true;
            this.lblTitleVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitleVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleVersion.Location = new System.Drawing.Point(4, 0);
            this.lblTitleVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleVersion.Name = "lblTitleVersion";
            this.lblTitleVersion.Size = new System.Drawing.Size(347, 49);
            this.lblTitleVersion.TabIndex = 0;
            this.lblTitleVersion.Text = "Version : ";
            this.lblTitleVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitleAuthors
            // 
            this.lblTitleAuthors.AutoSize = true;
            this.lblTitleAuthors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitleAuthors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleAuthors.Location = new System.Drawing.Point(4, 49);
            this.lblTitleAuthors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleAuthors.Name = "lblTitleAuthors";
            this.lblTitleAuthors.Size = new System.Drawing.Size(347, 49);
            this.lblTitleAuthors.TabIndex = 1;
            this.lblTitleAuthors.Text = "Author(s) : ";
            this.lblTitleAuthors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitleFirstRelease
            // 
            this.lblTitleFirstRelease.AutoSize = true;
            this.lblTitleFirstRelease.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitleFirstRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleFirstRelease.Location = new System.Drawing.Point(4, 98);
            this.lblTitleFirstRelease.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleFirstRelease.Name = "lblTitleFirstRelease";
            this.lblTitleFirstRelease.Size = new System.Drawing.Size(347, 49);
            this.lblTitleFirstRelease.TabIndex = 2;
            this.lblTitleFirstRelease.Text = "First release : ";
            this.lblTitleFirstRelease.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitleLatestRelease
            // 
            this.lblTitleLatestRelease.AutoSize = true;
            this.lblTitleLatestRelease.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitleLatestRelease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleLatestRelease.Location = new System.Drawing.Point(4, 147);
            this.lblTitleLatestRelease.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleLatestRelease.Name = "lblTitleLatestRelease";
            this.lblTitleLatestRelease.Size = new System.Drawing.Size(347, 49);
            this.lblTitleLatestRelease.TabIndex = 3;
            this.lblTitleLatestRelease.Text = "Latest release : ";
            this.lblTitleLatestRelease.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDownload
            // 
            this.lblDownload.AutoSize = true;
            this.lblDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDownload.Location = new System.Drawing.Point(4, 196);
            this.lblDownload.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(347, 49);
            this.lblDownload.TabIndex = 4;
            this.lblDownload.Text = "Downloads : ";
            this.lblDownload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitlePRojectUrl
            // 
            this.lblTitlePRojectUrl.AutoSize = true;
            this.lblTitlePRojectUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitlePRojectUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitlePRojectUrl.Location = new System.Drawing.Point(4, 245);
            this.lblTitlePRojectUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitlePRojectUrl.Name = "lblTitlePRojectUrl";
            this.lblTitlePRojectUrl.Size = new System.Drawing.Size(347, 49);
            this.lblTitlePRojectUrl.TabIndex = 5;
            this.lblTitlePRojectUrl.Text = "Project url :";
            this.lblTitlePRojectUrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitleRating
            // 
            this.lblTitleRating.AutoSize = true;
            this.lblTitleRating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitleRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleRating.Location = new System.Drawing.Point(4, 294);
            this.lblTitleRating.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitleRating.Name = "lblTitleRating";
            this.lblTitleRating.Size = new System.Drawing.Size(347, 49);
            this.lblTitleRating.TabIndex = 6;
            this.lblTitleRating.Text = "Rating :";
            this.lblTitleRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblToolAuthors
            // 
            this.lblToolAuthors.AutoSize = true;
            this.lblToolAuthors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolAuthors.Location = new System.Drawing.Point(359, 49);
            this.lblToolAuthors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToolAuthors.Name = "lblToolAuthors";
            this.lblToolAuthors.Size = new System.Drawing.Size(1265, 49);
            this.lblToolAuthors.TabIndex = 8;
            this.lblToolAuthors.Text = "[Authors]";
            this.lblToolAuthors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblToolFirstRelease
            // 
            this.lblToolFirstRelease.AutoSize = true;
            this.lblToolFirstRelease.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolFirstRelease.Location = new System.Drawing.Point(359, 98);
            this.lblToolFirstRelease.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToolFirstRelease.Name = "lblToolFirstRelease";
            this.lblToolFirstRelease.Size = new System.Drawing.Size(1265, 49);
            this.lblToolFirstRelease.TabIndex = 9;
            this.lblToolFirstRelease.Text = "[First release]";
            this.lblToolFirstRelease.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblToolLastRelease
            // 
            this.lblToolLastRelease.AutoSize = true;
            this.lblToolLastRelease.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolLastRelease.Location = new System.Drawing.Point(359, 147);
            this.lblToolLastRelease.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToolLastRelease.Name = "lblToolLastRelease";
            this.lblToolLastRelease.Size = new System.Drawing.Size(1265, 49);
            this.lblToolLastRelease.TabIndex = 10;
            this.lblToolLastRelease.Text = "[Last release]";
            this.lblToolLastRelease.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblToolDownload
            // 
            this.lblToolDownload.AutoSize = true;
            this.lblToolDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolDownload.Location = new System.Drawing.Point(359, 196);
            this.lblToolDownload.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToolDownload.Name = "lblToolDownload";
            this.lblToolDownload.Size = new System.Drawing.Size(1265, 49);
            this.lblToolDownload.TabIndex = 11;
            this.lblToolDownload.Text = "[Download]";
            this.lblToolDownload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llToolProjectUrl
            // 
            this.llToolProjectUrl.AutoSize = true;
            this.llToolProjectUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.llToolProjectUrl.Location = new System.Drawing.Point(359, 245);
            this.llToolProjectUrl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llToolProjectUrl.Name = "llToolProjectUrl";
            this.llToolProjectUrl.Size = new System.Drawing.Size(1265, 49);
            this.llToolProjectUrl.TabIndex = 12;
            this.llToolProjectUrl.TabStop = true;
            this.llToolProjectUrl.Text = "Open";
            this.llToolProjectUrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llToolProjectUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llToolProjectUrl_LinkClicked);
            // 
            // pnlRating
            // 
            this.pnlRating.Controls.Add(this.llRateThisTool);
            this.pnlRating.Controls.Add(this.lblToolRating);
            this.pnlRating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRating.Location = new System.Drawing.Point(359, 299);
            this.pnlRating.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlRating.Name = "pnlRating";
            this.pnlRating.Size = new System.Drawing.Size(1265, 39);
            this.pnlRating.TabIndex = 15;
            // 
            // llRateThisTool
            // 
            this.llRateThisTool.Dock = System.Windows.Forms.DockStyle.Left;
            this.llRateThisTool.Location = new System.Drawing.Point(198, 0);
            this.llRateThisTool.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llRateThisTool.Name = "llRateThisTool";
            this.llRateThisTool.Size = new System.Drawing.Size(465, 39);
            this.llRateThisTool.TabIndex = 20;
            this.llRateThisTool.TabStop = true;
            this.llRateThisTool.Text = "Rate this tool";
            this.llRateThisTool.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llRateThisTool.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRateThisTool_LinkClicked);
            // 
            // lblToolRating
            // 
            this.lblToolRating.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblToolRating.Location = new System.Drawing.Point(0, 0);
            this.lblToolRating.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToolRating.Name = "lblToolRating";
            this.lblToolRating.Size = new System.Drawing.Size(198, 39);
            this.lblToolRating.TabIndex = 19;
            this.lblToolRating.Text = "[Rating]";
            this.lblToolRating.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbbVersions);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(358, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1267, 43);
            this.panel2.TabIndex = 16;
            // 
            // cbbVersions
            // 
            this.cbbVersions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbVersions.Enabled = false;
            this.cbbVersions.FormattingEnabled = true;
            this.cbbVersions.Location = new System.Drawing.Point(0, 6);
            this.cbbVersions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbVersions.Name = "cbbVersions";
            this.cbbVersions.Size = new System.Drawing.Size(1253, 28);
            this.cbbVersions.TabIndex = 15;
            this.cbbVersions.SelectedIndexChanged += new System.EventHandler(this.cbbVersions_SelectedIndexChanged);
            // 
            // pnlSeparator1
            // 
            this.pnlSeparator1.Controls.Add(this.lblIncompatibleReason);
            this.pnlSeparator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSeparator1.Location = new System.Drawing.Point(0, 74);
            this.pnlSeparator1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSeparator1.Name = "pnlSeparator1";
            this.pnlSeparator1.Size = new System.Drawing.Size(1628, 43);
            this.pnlSeparator1.TabIndex = 2;
            // 
            // lblIncompatibleReason
            // 
            this.lblIncompatibleReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIncompatibleReason.ForeColor = System.Drawing.Color.Red;
            this.lblIncompatibleReason.Location = new System.Drawing.Point(0, 0);
            this.lblIncompatibleReason.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIncompatibleReason.Name = "lblIncompatibleReason";
            this.lblIncompatibleReason.Size = new System.Drawing.Size(1628, 43);
            this.lblIncompatibleReason.TabIndex = 0;
            this.lblIncompatibleReason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.tlpActions);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Location = new System.Drawing.Point(0, 0);
            this.pnlActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1628, 74);
            this.pnlActions.TabIndex = 7;
            // 
            // tlpActions
            // 
            this.tlpActions.ColumnCount = 2;
            this.tlpActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpActions.Controls.Add(this.btnInstall, 0, 0);
            this.tlpActions.Controls.Add(this.btnDelete, 1, 0);
            this.tlpActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpActions.Location = new System.Drawing.Point(0, 0);
            this.tlpActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tlpActions.Name = "tlpActions";
            this.tlpActions.RowCount = 1;
            this.tlpActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tlpActions.Size = new System.Drawing.Size(1628, 74);
            this.tlpActions.TabIndex = 0;
            // 
            // btnInstall
            // 
            this.btnInstall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInstall.Location = new System.Drawing.Point(4, 5);
            this.btnInstall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(806, 64);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Location = new System.Drawing.Point(818, 5);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(806, 64);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Uninstall";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ToolPackageCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ToolPackageCtrl";
            this.Padding = new System.Windows.Forms.Padding(30, 31, 30, 31);
            this.Size = new System.Drawing.Size(1688, 1306);
            this.pnlTop.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOpenTool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.pnlRating.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlSeparator1.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.tlpActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblTitleVersion;
        private System.Windows.Forms.Label lblTitleAuthors;
        private System.Windows.Forms.Label lblTitleFirstRelease;
        private System.Windows.Forms.Label lblTitleLatestRelease;
        private System.Windows.Forms.Label lblDownload;
        private System.Windows.Forms.Label lblTitlePRojectUrl;
        private System.Windows.Forms.Label lblTitleRating;
        private System.Windows.Forms.Panel pnlSeparator1;
        private System.Windows.Forms.Label lblTitleDesc;
        private System.Windows.Forms.Label lblTitleReleaseNotes;
        private System.Windows.Forms.Label lblToolAuthors;
        private System.Windows.Forms.Label lblToolFirstRelease;
        private System.Windows.Forms.Label lblToolLastRelease;
        private System.Windows.Forms.Label lblToolDownload;
        private System.Windows.Forms.LinkLabel llToolProjectUrl;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.TableLayoutPanel tlpActions;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Label lblIncompatibleReason;
        private System.Windows.Forms.Panel pnlRating;
        private System.Windows.Forms.LinkLabel llRateThisTool;
        private System.Windows.Forms.Label lblToolRating;
        private System.Windows.Forms.Label lblToolName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbOpenTool;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbbVersions;
        private System.Windows.Forms.RichTextBox rtbReleaseNotes;
    }
}
