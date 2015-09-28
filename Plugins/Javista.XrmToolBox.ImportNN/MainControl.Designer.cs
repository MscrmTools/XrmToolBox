    using System.Windows.Forms;
    using Microsoft.Xrm.Sdk;

namespace Javista.XrmToolBox.ImportNN
{
    public partial class MainControl
    {
        private ComboBox cbbFirstEntity;
        private ComboBox cbbRelationship;
        private ComboBox cbbSecondEntity;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbLoadMetadata;
        private GroupBox gbFirst;
        private ComboBox cbbFirstEntityAttribute;
        private RadioButton rdbFirstAttribute;
        private RadioButton rdbFirstGuid;
        private System.Windows.Forms.Label label2;
        private GroupBox gbRelationship;
        private GroupBox gbSecond;
        private ComboBox cbbSecondEntityAttribute;
        private RadioButton rdbSecondAttribute;
        private RadioButton rdbSecondGuid;
        private System.Windows.Forms.Label label1;
        private GroupBox gbImportFile;
        private Button btnBrowse;
        private TextBox txtFilePath;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbImportNN;
        private GroupBox gbLog;
        private ListBox listLog;

        /// <summary> 
        /// Variable n�cessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilis�es.
        /// </summary>
        /// <param name="disposing">true si les ressources manag�es doivent �tre supprim�es�; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainControl));
            this.cbbFirstEntity = new System.Windows.Forms.ComboBox();
            this.cbbRelationship = new System.Windows.Forms.ComboBox();
            this.cbbSecondEntity = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbLoadMetadata = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImportNN = new System.Windows.Forms.ToolStripButton();
            this.gbFirst = new System.Windows.Forms.GroupBox();
            this.cbbFirstEntityAttribute = new System.Windows.Forms.ComboBox();
            this.rdbFirstAttribute = new System.Windows.Forms.RadioButton();
            this.rdbFirstGuid = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.gbRelationship = new System.Windows.Forms.GroupBox();
            this.gbSecond = new System.Windows.Forms.GroupBox();
            this.cbbSecondEntityAttribute = new System.Windows.Forms.ComboBox();
            this.rdbSecondAttribute = new System.Windows.Forms.RadioButton();
            this.rdbSecondGuid = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gbImportFile = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.listLog = new System.Windows.Forms.ListBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExport = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.gbFirst.SuspendLayout();
            this.gbRelationship.SuspendLayout();
            this.gbSecond.SuspendLayout();
            this.gbImportFile.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbFirstEntity
            // 
            this.cbbFirstEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbFirstEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFirstEntity.FormattingEnabled = true;
            this.cbbFirstEntity.Location = new System.Drawing.Point(6, 19);
            this.cbbFirstEntity.Name = "cbbFirstEntity";
            this.cbbFirstEntity.Size = new System.Drawing.Size(653, 21);
            this.cbbFirstEntity.Sorted = true;
            this.cbbFirstEntity.TabIndex = 0;
            this.cbbFirstEntity.SelectedIndexChanged += new System.EventHandler(this.cbbFirstEntity_SelectedIndexChanged);
            // 
            // cbbRelationship
            // 
            this.cbbRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbRelationship.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRelationship.FormattingEnabled = true;
            this.cbbRelationship.Location = new System.Drawing.Point(9, 19);
            this.cbbRelationship.Name = "cbbRelationship";
            this.cbbRelationship.Size = new System.Drawing.Size(650, 21);
            this.cbbRelationship.Sorted = true;
            this.cbbRelationship.TabIndex = 1;
            this.cbbRelationship.SelectedIndexChanged += new System.EventHandler(this.cbbRelationship_SelectedIndexChanged);
            // 
            // cbbSecondEntity
            // 
            this.cbbSecondEntity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbSecondEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSecondEntity.FormattingEnabled = true;
            this.cbbSecondEntity.Location = new System.Drawing.Point(9, 20);
            this.cbbSecondEntity.Name = "cbbSecondEntity";
            this.cbbSecondEntity.Size = new System.Drawing.Size(650, 21);
            this.cbbSecondEntity.Sorted = true;
            this.cbbSecondEntity.TabIndex = 2;
            this.cbbSecondEntity.SelectedIndexChanged += new System.EventHandler(this.cbbSecondEntity_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.toolStripSeparator3,
            this.tsbLoadMetadata,
            this.toolStripSeparator1,
            this.tsbImportNN,
            this.toolStripSeparator2,
            this.tsbExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(671, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbLoadMetadata
            // 
            this.tsbLoadMetadata.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadMetadata.Image")));
            this.tsbLoadMetadata.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadMetadata.Name = "tsbLoadMetadata";
            this.tsbLoadMetadata.Size = new System.Drawing.Size(106, 22);
            this.tsbLoadMetadata.Text = "Load Metadata";
            this.tsbLoadMetadata.Click += new System.EventHandler(this.tsbLoadMetadata_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbImportNN
            // 
            this.tsbImportNN.Enabled = false;
            this.tsbImportNN.Image = ((System.Drawing.Image)(resources.GetObject("tsbImportNN.Image")));
            this.tsbImportNN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImportNN.Name = "tsbImportNN";
            this.tsbImportNN.Size = new System.Drawing.Size(154, 22);
            this.tsbImportNN.Text = "Import NN relationships";
            this.tsbImportNN.Click += new System.EventHandler(this.tsbImportNN_Click);
            // 
            // gbFirst
            // 
            this.gbFirst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFirst.Controls.Add(this.cbbFirstEntityAttribute);
            this.gbFirst.Controls.Add(this.rdbFirstAttribute);
            this.gbFirst.Controls.Add(this.rdbFirstGuid);
            this.gbFirst.Controls.Add(this.label2);
            this.gbFirst.Controls.Add(this.cbbFirstEntity);
            this.gbFirst.Location = new System.Drawing.Point(3, 28);
            this.gbFirst.Name = "gbFirst";
            this.gbFirst.Size = new System.Drawing.Size(665, 80);
            this.gbFirst.TabIndex = 5;
            this.gbFirst.TabStop = false;
            this.gbFirst.Text = "First Entity";
            // 
            // cbbFirstEntityAttribute
            // 
            this.cbbFirstEntityAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbFirstEntityAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFirstEntityAttribute.Enabled = false;
            this.cbbFirstEntityAttribute.FormattingEnabled = true;
            this.cbbFirstEntityAttribute.Location = new System.Drawing.Point(353, 46);
            this.cbbFirstEntityAttribute.Name = "cbbFirstEntityAttribute";
            this.cbbFirstEntityAttribute.Size = new System.Drawing.Size(306, 21);
            this.cbbFirstEntityAttribute.Sorted = true;
            this.cbbFirstEntityAttribute.TabIndex = 4;
            // 
            // rdbFirstAttribute
            // 
            this.rdbFirstAttribute.AutoSize = true;
            this.rdbFirstAttribute.Location = new System.Drawing.Point(243, 47);
            this.rdbFirstAttribute.Name = "rdbFirstAttribute";
            this.rdbFirstAttribute.Size = new System.Drawing.Size(104, 17);
            this.rdbFirstAttribute.TabIndex = 3;
            this.rdbFirstAttribute.Text = "Specific attribute";
            this.rdbFirstAttribute.UseVisualStyleBackColor = true;
            // 
            // rdbFirstGuid
            // 
            this.rdbFirstGuid.AutoSize = true;
            this.rdbFirstGuid.Checked = true;
            this.rdbFirstGuid.Location = new System.Drawing.Point(135, 47);
            this.rdbFirstGuid.Name = "rdbFirstGuid";
            this.rdbFirstGuid.Size = new System.Drawing.Size(102, 17);
            this.rdbFirstGuid.TabIndex = 2;
            this.rdbFirstGuid.TabStop = true;
            this.rdbFirstGuid.Text = "Unique Identifier";
            this.rdbFirstGuid.UseVisualStyleBackColor = true;
            this.rdbFirstGuid.CheckedChanged += new System.EventHandler(this.rdbFirstGuid_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mapping attribute";
            // 
            // gbRelationship
            // 
            this.gbRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRelationship.Controls.Add(this.cbbRelationship);
            this.gbRelationship.Location = new System.Drawing.Point(3, 114);
            this.gbRelationship.Name = "gbRelationship";
            this.gbRelationship.Size = new System.Drawing.Size(665, 54);
            this.gbRelationship.TabIndex = 6;
            this.gbRelationship.TabStop = false;
            this.gbRelationship.Text = "Relationship";
            // 
            // gbSecond
            // 
            this.gbSecond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSecond.Controls.Add(this.cbbSecondEntityAttribute);
            this.gbSecond.Controls.Add(this.rdbSecondAttribute);
            this.gbSecond.Controls.Add(this.rdbSecondGuid);
            this.gbSecond.Controls.Add(this.label1);
            this.gbSecond.Controls.Add(this.cbbSecondEntity);
            this.gbSecond.Location = new System.Drawing.Point(3, 174);
            this.gbSecond.Name = "gbSecond";
            this.gbSecond.Size = new System.Drawing.Size(665, 80);
            this.gbSecond.TabIndex = 6;
            this.gbSecond.TabStop = false;
            this.gbSecond.Text = "Second Entity";
            // 
            // cbbSecondEntityAttribute
            // 
            this.cbbSecondEntityAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbSecondEntityAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSecondEntityAttribute.Enabled = false;
            this.cbbSecondEntityAttribute.FormattingEnabled = true;
            this.cbbSecondEntityAttribute.Location = new System.Drawing.Point(353, 46);
            this.cbbSecondEntityAttribute.Name = "cbbSecondEntityAttribute";
            this.cbbSecondEntityAttribute.Size = new System.Drawing.Size(306, 21);
            this.cbbSecondEntityAttribute.Sorted = true;
            this.cbbSecondEntityAttribute.TabIndex = 4;
            // 
            // rdbSecondAttribute
            // 
            this.rdbSecondAttribute.AutoSize = true;
            this.rdbSecondAttribute.Location = new System.Drawing.Point(243, 47);
            this.rdbSecondAttribute.Name = "rdbSecondAttribute";
            this.rdbSecondAttribute.Size = new System.Drawing.Size(104, 17);
            this.rdbSecondAttribute.TabIndex = 3;
            this.rdbSecondAttribute.Text = "Specific attribute";
            this.rdbSecondAttribute.UseVisualStyleBackColor = true;
            // 
            // rdbSecondGuid
            // 
            this.rdbSecondGuid.AutoSize = true;
            this.rdbSecondGuid.Checked = true;
            this.rdbSecondGuid.Location = new System.Drawing.Point(135, 47);
            this.rdbSecondGuid.Name = "rdbSecondGuid";
            this.rdbSecondGuid.Size = new System.Drawing.Size(102, 17);
            this.rdbSecondGuid.TabIndex = 2;
            this.rdbSecondGuid.TabStop = true;
            this.rdbSecondGuid.Text = "Unique Identifier";
            this.rdbSecondGuid.UseVisualStyleBackColor = true;
            this.rdbSecondGuid.CheckedChanged += new System.EventHandler(this.rdbSecondGuid_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mapping attribute";
            // 
            // gbImportFile
            // 
            this.gbImportFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbImportFile.Controls.Add(this.btnBrowse);
            this.gbImportFile.Controls.Add(this.txtFilePath);
            this.gbImportFile.Location = new System.Drawing.Point(3, 260);
            this.gbImportFile.Name = "gbImportFile";
            this.gbImportFile.Size = new System.Drawing.Size(665, 56);
            this.gbImportFile.TabIndex = 7;
            this.gbImportFile.TabStop = false;
            this.gbImportFile.Text = "Import file";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(584, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(9, 21);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(569, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // gbLog
            // 
            this.gbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLog.Controls.Add(this.listLog);
            this.gbLog.Location = new System.Drawing.Point(3, 322);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(665, 174);
            this.gbLog.TabIndex = 8;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // listLog
            // 
            this.listLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listLog.FormattingEnabled = true;
            this.listLog.Location = new System.Drawing.Point(9, 19);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(650, 147);
            this.listLog.TabIndex = 0;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbExport
            // 
            this.tsbExport.Enabled = false;
            this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
            this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExport.Name = "tsbExport";
            this.tsbExport.Size = new System.Drawing.Size(151, 22);
            this.tsbExport.Text = "Export NN relationships";
            this.tsbExport.Click += new System.EventHandler(this.tsbExport_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(102, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // MainControl
            // 
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.gbImportFile);
            this.Controls.Add(this.gbSecond);
            this.Controls.Add(this.gbRelationship);
            this.Controls.Add(this.gbFirst);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(671, 499);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbFirst.ResumeLayout(false);
            this.gbFirst.PerformLayout();
            this.gbRelationship.ResumeLayout(false);
            this.gbSecond.ResumeLayout(false);
            this.gbSecond.PerformLayout();
            this.gbImportFile.ResumeLayout(false);
            this.gbImportFile.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbExport;
        private ToolStripButton tsbClose;
        private ToolStripSeparator toolStripSeparator3;

    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  