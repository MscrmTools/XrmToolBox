
namespace XrmToolBox.ToolLibrary.UserControls
{
    partial class ProgressStepControl
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
            this.lblAction = new System.Windows.Forms.Label();
            this.pnlState = new System.Windows.Forms.Panel();
            this.pbState = new System.Windows.Forms.PictureBox();
            this.pnlAction = new System.Windows.Forms.Panel();
            this.pbAction = new System.Windows.Forms.PictureBox();
            this.pnlState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbState)).BeginInit();
            this.pnlAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAction)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAction
            // 
            this.lblAction.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAction.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(66, 0);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(379, 60);
            this.lblAction.TabIndex = 2;
            this.lblAction.Text = "Action";
            this.lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlState
            // 
            this.pnlState.Controls.Add(this.pbState);
            this.pnlState.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlState.Location = new System.Drawing.Point(445, 0);
            this.pnlState.Name = "pnlState";
            this.pnlState.Padding = new System.Windows.Forms.Padding(10);
            this.pnlState.Size = new System.Drawing.Size(66, 60);
            this.pnlState.TabIndex = 4;
            // 
            // pbState
            // 
            this.pbState.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbState.Image = global::XrmToolBox.ToolLibrary.Resource.progress;
            this.pbState.Location = new System.Drawing.Point(10, 10);
            this.pbState.Name = "pbState";
            this.pbState.Size = new System.Drawing.Size(48, 60);
            this.pbState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbState.TabIndex = 4;
            this.pbState.TabStop = false;
            // 
            // pnlAction
            // 
            this.pnlAction.Controls.Add(this.pbAction);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAction.Location = new System.Drawing.Point(0, 0);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Padding = new System.Windows.Forms.Padding(10);
            this.pnlAction.Size = new System.Drawing.Size(66, 60);
            this.pnlAction.TabIndex = 5;
            // 
            // pbAction
            // 
            this.pbAction.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbAction.Image = global::XrmToolBox.ToolLibrary.Resource.Download32;
            this.pbAction.Location = new System.Drawing.Point(10, 10);
            this.pbAction.Name = "pbAction";
            this.pbAction.Size = new System.Drawing.Size(768, 60);
            this.pbAction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbAction.TabIndex = 1;
            this.pbAction.TabStop = false;
            // 
            // ProgressStepControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlState);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.pnlAction);
            this.Name = "ProgressStepControl";
            this.Size = new System.Drawing.Size(1046, 60);
            this.pnlState.ResumeLayout(false);
            this.pnlState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbState)).EndInit();
            this.pnlAction.ResumeLayout(false);
            this.pnlAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Panel pnlState;
        private System.Windows.Forms.PictureBox pbState;
        private System.Windows.Forms.Panel pnlAction;
        private System.Windows.Forms.PictureBox pbAction;
    }
}
