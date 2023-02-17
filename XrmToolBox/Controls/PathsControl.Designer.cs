
namespace XrmToolBox.Controls
{
    partial class PathsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathsControl));
            this.lblChangePathDescription = new System.Windows.Forms.Label();
            this.lblChangePathTitle = new System.Windows.Forms.Label();
            this.llOpenStorageFolder = new System.Windows.Forms.LinkLabel();
            this.llOpenRootFolder = new System.Windows.Forms.LinkLabel();
            this.pnlStatisticsMain = new System.Windows.Forms.Panel();
            this.pnlStorageGraphics = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlLegend = new System.Windows.Forms.Panel();
            this.pnlStatisticsMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblChangePathDescription
            // 
            this.lblChangePathDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChangePathDescription.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F);
            this.lblChangePathDescription.Location = new System.Drawing.Point(0, 148);
            this.lblChangePathDescription.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblChangePathDescription.Name = "lblChangePathDescription";
            this.lblChangePathDescription.Size = new System.Drawing.Size(1121, 372);
            this.lblChangePathDescription.TabIndex = 18;
            this.lblChangePathDescription.Text = resources.GetString("lblChangePathDescription.Text");
            // 
            // lblChangePathTitle
            // 
            this.lblChangePathTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChangePathTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F);
            this.lblChangePathTitle.Location = new System.Drawing.Point(0, 85);
            this.lblChangePathTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblChangePathTitle.Name = "lblChangePathTitle";
            this.lblChangePathTitle.Size = new System.Drawing.Size(1121, 63);
            this.lblChangePathTitle.TabIndex = 16;
            this.lblChangePathTitle.Text = "How to change XrmToolBox storage folder";
            this.lblChangePathTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llOpenStorageFolder
            // 
            this.llOpenStorageFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.llOpenStorageFolder.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llOpenStorageFolder.Location = new System.Drawing.Point(0, 43);
            this.llOpenStorageFolder.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.llOpenStorageFolder.Name = "llOpenStorageFolder";
            this.llOpenStorageFolder.Size = new System.Drawing.Size(1121, 42);
            this.llOpenStorageFolder.TabIndex = 15;
            this.llOpenStorageFolder.TabStop = true;
            this.llOpenStorageFolder.Text = "Open XrmToolBox storage folder";
            this.llOpenStorageFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOpenStorageFolder_LinkClicked);
            // 
            // llOpenRootFolder
            // 
            this.llOpenRootFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.llOpenRootFolder.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.llOpenRootFolder.Location = new System.Drawing.Point(0, 0);
            this.llOpenRootFolder.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.llOpenRootFolder.Name = "llOpenRootFolder";
            this.llOpenRootFolder.Size = new System.Drawing.Size(1121, 43);
            this.llOpenRootFolder.TabIndex = 14;
            this.llOpenRootFolder.TabStop = true;
            this.llOpenRootFolder.Text = "Open XrmToolBox folder";
            this.llOpenRootFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llOpenRootFolder_LinkClicked);
            // 
            // pnlStatisticsMain
            // 
            this.pnlStatisticsMain.Controls.Add(this.panel1);
            this.pnlStatisticsMain.Controls.Add(this.pnlStorageGraphics);
            this.pnlStatisticsMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatisticsMain.Location = new System.Drawing.Point(0, 689);
            this.pnlStatisticsMain.Name = "pnlStatisticsMain";
            this.pnlStatisticsMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlStatisticsMain.Size = new System.Drawing.Size(1121, 150);
            this.pnlStatisticsMain.TabIndex = 19;
            // 
            // pnlStorageGraphics
            // 
            this.pnlStorageGraphics.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStorageGraphics.Location = new System.Drawing.Point(20, 20);
            this.pnlStorageGraphics.Name = "pnlStorageGraphics";
            this.pnlStorageGraphics.Size = new System.Drawing.Size(1081, 42);
            this.pnlStorageGraphics.TabIndex = 18;
            this.pnlStorageGraphics.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStorageGraphics_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlLegend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1081, 42);
            this.panel1.TabIndex = 19;
            // 
            // pnlLegend
            // 
            this.pnlLegend.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLegend.Location = new System.Drawing.Point(0, 0);
            this.pnlLegend.Name = "pnlLegend";
            this.pnlLegend.Size = new System.Drawing.Size(1081, 42);
            this.pnlLegend.TabIndex = 19;
            this.pnlLegend.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLegend_Paint);
            // 
            // PathsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlStatisticsMain);
            this.Controls.Add(this.lblChangePathDescription);
            this.Controls.Add(this.lblChangePathTitle);
            this.Controls.Add(this.llOpenStorageFolder);
            this.Controls.Add(this.llOpenRootFolder);
            this.Name = "PathsControl";
            this.Size = new System.Drawing.Size(1121, 839);
            this.Resize += new System.EventHandler(this.PathsControl_Resize);
            this.pnlStatisticsMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblChangePathDescription;
        private System.Windows.Forms.Label lblChangePathTitle;
        private System.Windows.Forms.LinkLabel llOpenStorageFolder;
        private System.Windows.Forms.LinkLabel llOpenRootFolder;
        private System.Windows.Forms.Panel pnlStatisticsMain;
        private System.Windows.Forms.Panel pnlStorageGraphics;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlLegend;
    }
}
