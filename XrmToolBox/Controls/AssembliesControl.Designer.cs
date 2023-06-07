
namespace XrmToolBox.Controls
{
    partial class AssembliesControl
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
            this.lvAssemblies = new System.Windows.Forms.ListView();
            this.chAssembly = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvAssemblies
            // 
            this.lvAssemblies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAssembly,
            this.chVersion});
            this.lvAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAssemblies.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvAssemblies.HideSelection = false;
            this.lvAssemblies.Location = new System.Drawing.Point(0, 0);
            this.lvAssemblies.Name = "lvAssemblies";
            this.lvAssemblies.Size = new System.Drawing.Size(1202, 712);
            this.lvAssemblies.TabIndex = 2;
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
            // AssembliesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvAssemblies);
            this.Name = "AssembliesControl";
            this.Size = new System.Drawing.Size(1202, 712);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvAssemblies;
        private System.Windows.Forms.ColumnHeader chAssembly;
        private System.Windows.Forms.ColumnHeader chVersion;
    }
}
