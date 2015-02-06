namespace MsCrmTools.WebResourcesManager.UserControls
{
    partial class CodeControl
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
            this.tecCode = new ICSharpCode.TextEditor.TextEditorControl();
            this.SuspendLayout();
            // 
            // tecCode
            // 
            this.tecCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tecCode.IsReadOnly = false;
            this.tecCode.Location = new System.Drawing.Point(6, 3);
            this.tecCode.Name = "tecCode";
            this.tecCode.Size = new System.Drawing.Size(491, 394);
            this.tecCode.TabIndex = 1;
            this.tecCode.Text = "textEditorControl1";
            this.tecCode.TextChanged += new System.EventHandler(this.tecCode_TextChanged);
            // 
            // CodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tecCode);
            this.Name = "CodeControl";
            this.Size = new System.Drawing.Size(500, 400);
            this.Load += new System.EventHandler(this.CodeControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl tecCode;
    }
}
