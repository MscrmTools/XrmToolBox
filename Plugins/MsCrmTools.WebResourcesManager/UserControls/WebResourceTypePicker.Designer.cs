namespace MsCrmTools.WebResourcesManager.UserControls
{
    partial class WebResourceTypePicker
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkIco = new System.Windows.Forms.CheckBox();
            this.chkXsl = new System.Windows.Forms.CheckBox();
            this.chkXap = new System.Windows.Forms.CheckBox();
            this.chkGif = new System.Windows.Forms.CheckBox();
            this.chkJpeg = new System.Windows.Forms.CheckBox();
            this.chkPng = new System.Windows.Forms.CheckBox();
            this.chkXml = new System.Windows.Forms.CheckBox();
            this.chkJavaScript = new System.Windows.Forms.CheckBox();
            this.chkCss = new System.Windows.Forms.CheckBox();
            this.chkHtml = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkAll);
            this.groupBox2.Controls.Add(this.chkIco);
            this.groupBox2.Controls.Add(this.chkXsl);
            this.groupBox2.Controls.Add(this.chkXap);
            this.groupBox2.Controls.Add(this.chkGif);
            this.groupBox2.Controls.Add(this.chkJpeg);
            this.groupBox2.Controls.Add(this.chkPng);
            this.groupBox2.Controls.Add(this.chkXml);
            this.groupBox2.Controls.Add(this.chkJavaScript);
            this.groupBox2.Controls.Add(this.chkCss);
            this.groupBox2.Controls.Add(this.chkHtml);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(606, 188);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Load only these file types";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Checked = true;
            this.chkAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAll.Location = new System.Drawing.Point(8, 38);
            this.chkAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(52, 24);
            this.chkAll.TabIndex = 10;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkIco
            // 
            this.chkIco.AutoSize = true;
            this.chkIco.Checked = true;
            this.chkIco.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIco.Enabled = false;
            this.chkIco.Location = new System.Drawing.Point(507, 72);
            this.chkIco.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkIco.Name = "chkIco";
            this.chkIco.Size = new System.Drawing.Size(63, 24);
            this.chkIco.TabIndex = 9;
            this.chkIco.Tag = ".ico";
            this.chkIco.Text = "ICO";
            this.chkIco.UseVisualStyleBackColor = true;
            // 
            // chkXsl
            // 
            this.chkXsl.AutoSize = true;
            this.chkXsl.Checked = true;
            this.chkXsl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkXsl.Enabled = false;
            this.chkXsl.Location = new System.Drawing.Point(363, 144);
            this.chkXsl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkXsl.Name = "chkXsl";
            this.chkXsl.Size = new System.Drawing.Size(66, 24);
            this.chkXsl.TabIndex = 8;
            this.chkXsl.Tag = ".xsl|.xslt";
            this.chkXsl.Text = "XSL";
            this.chkXsl.UseVisualStyleBackColor = true;
            // 
            // chkXap
            // 
            this.chkXap.AutoSize = true;
            this.chkXap.Checked = true;
            this.chkXap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkXap.Enabled = false;
            this.chkXap.Location = new System.Drawing.Point(363, 109);
            this.chkXap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkXap.Name = "chkXap";
            this.chkXap.Size = new System.Drawing.Size(67, 24);
            this.chkXap.TabIndex = 7;
            this.chkXap.Tag = ".xap";
            this.chkXap.Text = "XAP";
            this.chkXap.UseVisualStyleBackColor = true;
            // 
            // chkGif
            // 
            this.chkGif.AutoSize = true;
            this.chkGif.Checked = true;
            this.chkGif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGif.Enabled = false;
            this.chkGif.Location = new System.Drawing.Point(363, 72);
            this.chkGif.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkGif.Name = "chkGif";
            this.chkGif.Size = new System.Drawing.Size(63, 24);
            this.chkGif.TabIndex = 6;
            this.chkGif.Tag = ".gif";
            this.chkGif.Text = "GIF";
            this.chkGif.UseVisualStyleBackColor = true;
            // 
            // chkJpeg
            // 
            this.chkJpeg.AutoSize = true;
            this.chkJpeg.Checked = true;
            this.chkJpeg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJpeg.Enabled = false;
            this.chkJpeg.Location = new System.Drawing.Point(174, 144);
            this.chkJpeg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkJpeg.Name = "chkJpeg";
            this.chkJpeg.Size = new System.Drawing.Size(112, 24);
            this.chkJpeg.TabIndex = 5;
            this.chkJpeg.Tag = ".jpg|.jpeg";
            this.chkJpeg.Text = "JPG/JPEG";
            this.chkJpeg.UseVisualStyleBackColor = true;
            // 
            // chkPng
            // 
            this.chkPng.AutoSize = true;
            this.chkPng.Checked = true;
            this.chkPng.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPng.Enabled = false;
            this.chkPng.Location = new System.Drawing.Point(174, 109);
            this.chkPng.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkPng.Name = "chkPng";
            this.chkPng.Size = new System.Drawing.Size(69, 24);
            this.chkPng.TabIndex = 4;
            this.chkPng.Tag = ".png";
            this.chkPng.Text = "PNG";
            this.chkPng.UseVisualStyleBackColor = true;
            // 
            // chkXml
            // 
            this.chkXml.AutoSize = true;
            this.chkXml.Checked = true;
            this.chkXml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkXml.Enabled = false;
            this.chkXml.Location = new System.Drawing.Point(174, 72);
            this.chkXml.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkXml.Name = "chkXml";
            this.chkXml.Size = new System.Drawing.Size(68, 24);
            this.chkXml.TabIndex = 3;
            this.chkXml.Tag = ".xml";
            this.chkXml.Text = "XML";
            this.chkXml.UseVisualStyleBackColor = true;
            // 
            // chkJavaScript
            // 
            this.chkJavaScript.AutoSize = true;
            this.chkJavaScript.Checked = true;
            this.chkJavaScript.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJavaScript.Enabled = false;
            this.chkJavaScript.Location = new System.Drawing.Point(9, 144);
            this.chkJavaScript.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkJavaScript.Name = "chkJavaScript";
            this.chkJavaScript.Size = new System.Drawing.Size(54, 24);
            this.chkJavaScript.TabIndex = 2;
            this.chkJavaScript.Tag = ".js";
            this.chkJavaScript.Text = "JS";
            this.chkJavaScript.UseVisualStyleBackColor = true;
            // 
            // chkCss
            // 
            this.chkCss.AutoSize = true;
            this.chkCss.Checked = true;
            this.chkCss.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCss.Enabled = false;
            this.chkCss.Location = new System.Drawing.Point(9, 109);
            this.chkCss.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkCss.Name = "chkCss";
            this.chkCss.Size = new System.Drawing.Size(68, 24);
            this.chkCss.TabIndex = 1;
            this.chkCss.Tag = ".css";
            this.chkCss.Text = "CSS";
            this.chkCss.UseVisualStyleBackColor = true;
            // 
            // chkHtml
            // 
            this.chkHtml.AutoSize = true;
            this.chkHtml.Checked = true;
            this.chkHtml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHtml.Enabled = false;
            this.chkHtml.Location = new System.Drawing.Point(9, 72);
            this.chkHtml.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkHtml.Name = "chkHtml";
            this.chkHtml.Size = new System.Drawing.Size(78, 24);
            this.chkHtml.TabIndex = 0;
            this.chkHtml.Tag = ".html|.htm";
            this.chkHtml.Text = "HTML";
            this.chkHtml.UseVisualStyleBackColor = true;
            // 
            // WebResourceTypePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "WebResourceTypePicker";
            this.Size = new System.Drawing.Size(606, 188);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkIco;
        private System.Windows.Forms.CheckBox chkXsl;
        private System.Windows.Forms.CheckBox chkXap;
        private System.Windows.Forms.CheckBox chkGif;
        private System.Windows.Forms.CheckBox chkJpeg;
        private System.Windows.Forms.CheckBox chkPng;
        private System.Windows.Forms.CheckBox chkXml;
        private System.Windows.Forms.CheckBox chkJavaScript;
        private System.Windows.Forms.CheckBox chkCss;
        private System.Windows.Forms.CheckBox chkHtml;
        private System.Windows.Forms.CheckBox chkAll;

    }
}
