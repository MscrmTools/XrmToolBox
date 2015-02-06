namespace MsCrmTools.FetchXmlBuilder.UserControls
{
    partial class FetchControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMapping = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAggregate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkDistinct = new System.Windows.Forms.CheckBox();
            this.nudPage = new System.Windows.Forms.NumericUpDown();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.nudTop = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mapping";
            // 
            // txtMapping
            // 
            this.txtMapping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMapping.Location = new System.Drawing.Point(176, 3);
            this.txtMapping.Name = "txtMapping";
            this.txtMapping.ReadOnly = true;
            this.txtMapping.Size = new System.Drawing.Size(271, 20);
            this.txtMapping.TabIndex = 1;
            this.txtMapping.Text = "logical";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Count";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Page";
            // 
            // chkAggregate
            // 
            this.chkAggregate.AutoSize = true;
            this.chkAggregate.Location = new System.Drawing.Point(176, 82);
            this.chkAggregate.Name = "chkAggregate";
            this.chkAggregate.Size = new System.Drawing.Size(15, 14);
            this.chkAggregate.TabIndex = 6;
            this.chkAggregate.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Aggregate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Top";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Distinct";
            // 
            // chkDistinct
            // 
            this.chkDistinct.AutoSize = true;
            this.chkDistinct.Location = new System.Drawing.Point(176, 131);
            this.chkDistinct.Name = "chkDistinct";
            this.chkDistinct.Size = new System.Drawing.Size(15, 14);
            this.chkDistinct.TabIndex = 10;
            this.chkDistinct.UseVisualStyleBackColor = true;
            // 
            // nudPage
            // 
            this.nudPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPage.Location = new System.Drawing.Point(176, 30);
            this.nudPage.Name = "nudPage";
            this.nudPage.Size = new System.Drawing.Size(271, 20);
            this.nudPage.TabIndex = 12;
            // 
            // nudCount
            // 
            this.nudCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudCount.Location = new System.Drawing.Point(176, 56);
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(271, 20);
            this.nudCount.TabIndex = 13;
            // 
            // nudTop
            // 
            this.nudTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTop.Location = new System.Drawing.Point(176, 105);
            this.nudTop.Name = "nudTop";
            this.nudTop.Size = new System.Drawing.Size(271, 20);
            this.nudTop.TabIndex = 14;
            // 
            // FetchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudTop);
            this.Controls.Add(this.nudCount);
            this.Controls.Add(this.nudPage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkDistinct);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkAggregate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMapping);
            this.Controls.Add(this.label1);
            this.Name = "FetchControl";
            this.Size = new System.Drawing.Size(450, 400);
            this.Load += new System.EventHandler(this.FetchControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.nudPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMapping;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkAggregate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDistinct;
        private System.Windows.Forms.NumericUpDown nudPage;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.NumericUpDown nudTop;
    }
}
