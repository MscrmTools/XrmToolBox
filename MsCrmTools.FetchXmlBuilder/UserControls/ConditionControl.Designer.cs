namespace MsCrmTools.FetchXmlBuilder.UserControls
{
    partial class ConditionControl
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
            this.txtAttribute = new System.Windows.Forms.TextBox();
            this.txtOperator = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblAttribute = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtAttribute
            // 
            this.txtAttribute.Location = new System.Drawing.Point(166, 57);
            this.txtAttribute.Name = "txtAttribute";
            this.txtAttribute.Size = new System.Drawing.Size(193, 20);
            this.txtAttribute.TabIndex = 0;
            // 
            // txtOperator
            // 
            this.txtOperator.Location = new System.Drawing.Point(166, 84);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(193, 20);
            this.txtOperator.TabIndex = 1;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(166, 111);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(193, 20);
            this.txtValue.TabIndex = 2;
            // 
            // lblAttribute
            // 
            this.lblAttribute.AutoSize = true;
            this.lblAttribute.Location = new System.Drawing.Point(19, 57);
            this.lblAttribute.Name = "lblAttribute";
            this.lblAttribute.Size = new System.Drawing.Size(46, 13);
            this.lblAttribute.TabIndex = 3;
            this.lblAttribute.Text = "Attribute";
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(19, 87);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(48, 13);
            this.lblOperator.TabIndex = 4;
            this.lblOperator.Text = "Operator";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(19, 111);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 5;
            this.lblValue.Text = "Value";
            // 
            // ConditionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblOperator);
            this.Controls.Add(this.lblAttribute);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtOperator);
            this.Controls.Add(this.txtAttribute);
            this.Name = "ConditionControl";
            this.Size = new System.Drawing.Size(476, 398);
            this.Load += new System.EventHandler(this.ConditionControlLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAttribute;
        private System.Windows.Forms.TextBox txtOperator;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblAttribute;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Label lblValue;
    }
}
