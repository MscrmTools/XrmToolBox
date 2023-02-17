
namespace XrmToolBox.Controls
{
    partial class AdvancedControl
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTabsOrder = new System.Windows.Forms.Panel();
            this.lbTabs = new System.Windows.Forms.ListBox();
            this.pnlTabsOrderLeft = new System.Windows.Forms.Panel();
            this.btnSort = new System.Windows.Forms.Button();
            this.bnApply = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.lblTabsOrderTitle = new System.Windows.Forms.Label();
            this.pnlTabsOrder.SuspendLayout();
            this.pnlTabsOrderLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1535, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Advanced";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlTabsOrder
            // 
            this.pnlTabsOrder.Controls.Add(this.lbTabs);
            this.pnlTabsOrder.Controls.Add(this.pnlTabsOrderLeft);
            this.pnlTabsOrder.Controls.Add(this.lblTabsOrderTitle);
            this.pnlTabsOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTabsOrder.Location = new System.Drawing.Point(0, 41);
            this.pnlTabsOrder.Name = "pnlTabsOrder";
            this.pnlTabsOrder.Size = new System.Drawing.Size(1535, 341);
            this.pnlTabsOrder.TabIndex = 1;
            // 
            // lbTabs
            // 
            this.lbTabs.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbTabs.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTabs.FormattingEnabled = true;
            this.lbTabs.ItemHeight = 27;
            this.lbTabs.Location = new System.Drawing.Point(91, 35);
            this.lbTabs.Name = "lbTabs";
            this.lbTabs.Size = new System.Drawing.Size(311, 306);
            this.lbTabs.TabIndex = 4;
            // 
            // pnlTabsOrderLeft
            // 
            this.pnlTabsOrderLeft.Controls.Add(this.btnSort);
            this.pnlTabsOrderLeft.Controls.Add(this.bnApply);
            this.pnlTabsOrderLeft.Controls.Add(this.btnDown);
            this.pnlTabsOrderLeft.Controls.Add(this.btnUp);
            this.pnlTabsOrderLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTabsOrderLeft.Location = new System.Drawing.Point(0, 35);
            this.pnlTabsOrderLeft.Name = "pnlTabsOrderLeft";
            this.pnlTabsOrderLeft.Size = new System.Drawing.Size(91, 306);
            this.pnlTabsOrderLeft.TabIndex = 3;
            // 
            // btnSort
            // 
            this.btnSort.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSort.Location = new System.Drawing.Point(10, 3);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 65);
            this.btnSort.TabIndex = 3;
            this.btnSort.Text = "Sort alpha.";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // bnApply
            // 
            this.bnApply.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnApply.Location = new System.Drawing.Point(10, 216);
            this.bnApply.Name = "bnApply";
            this.bnApply.Size = new System.Drawing.Size(75, 65);
            this.bnApply.TabIndex = 2;
            this.bnApply.Text = "Apply";
            this.bnApply.UseVisualStyleBackColor = true;
            this.bnApply.Click += new System.EventHandler(this.bnApply_Click);
            // 
            // btnDown
            // 
            this.btnDown.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Location = new System.Drawing.Point(10, 145);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 65);
            this.btnDown.TabIndex = 1;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(10, 74);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 65);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // lblTabsOrderTitle
            // 
            this.lblTabsOrderTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTabsOrderTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTabsOrderTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTabsOrderTitle.Name = "lblTabsOrderTitle";
            this.lblTabsOrderTitle.Size = new System.Drawing.Size(1535, 35);
            this.lblTabsOrderTitle.TabIndex = 2;
            this.lblTabsOrderTitle.Text = "Navigation tabs order";
            // 
            // AdvancedControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlTabsOrder);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AdvancedControl";
            this.Size = new System.Drawing.Size(1535, 716);
            this.Load += new System.EventHandler(this.AdvancedControl_Load);
            this.pnlTabsOrder.ResumeLayout(false);
            this.pnlTabsOrderLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlTabsOrder;
        private System.Windows.Forms.ListBox lbTabs;
        private System.Windows.Forms.Panel pnlTabsOrderLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Label lblTabsOrderTitle;
        private System.Windows.Forms.Button bnApply;
        private System.Windows.Forms.Button btnSort;
    }
}
