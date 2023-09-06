
namespace XrmToolBox.Controls
{
    partial class HiddenToolsControl
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
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvAssemblies
            // 
            this.lvAssemblies.CheckBoxes = true;
            this.lvAssemblies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName});
            this.lvAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAssemblies.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvAssemblies.HideSelection = false;
            this.lvAssemblies.Location = new System.Drawing.Point(0, 0);
            this.lvAssemblies.Name = "lvAssemblies";
            this.lvAssemblies.Size = new System.Drawing.Size(1202, 712);
            this.lvAssemblies.TabIndex = 2;
            this.lvAssemblies.UseCompatibleStateImageBehavior = false;
            this.lvAssemblies.View = System.Windows.Forms.View.Details;
            this.lvAssemblies.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvAssemblies_ItemChecked);
            // 
            // chName
            // 
            this.chName.Text = "Tool";
            this.chName.Width = 400;
            // 
            // HiddenToolsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvAssemblies);
            this.Name = "HiddenToolsControl";
            this.Size = new System.Drawing.Size(1202, 712);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HiddenToolsControl_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvAssemblies;
        private System.Windows.Forms.ColumnHeader chName;
    }
}
