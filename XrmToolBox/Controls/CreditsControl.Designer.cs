
namespace XrmToolBox.Controls
{
    partial class CreditsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreditsControl));
            this.rtbCredits = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbCredits
            // 
            this.rtbCredits.BackColor = System.Drawing.Color.White;
            this.rtbCredits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCredits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCredits.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCredits.Location = new System.Drawing.Point(0, 0);
            this.rtbCredits.Name = "rtbCredits";
            this.rtbCredits.ReadOnly = true;
            this.rtbCredits.Size = new System.Drawing.Size(977, 441);
            this.rtbCredits.TabIndex = 3;
            this.rtbCredits.Text = resources.GetString("rtbCredits.Text");
            this.rtbCredits.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbCredits_LinkClicked);
            // 
            // CreditsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbCredits);
            this.Name = "CreditsControl";
            this.Size = new System.Drawing.Size(977, 441);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbCredits;
    }
}
