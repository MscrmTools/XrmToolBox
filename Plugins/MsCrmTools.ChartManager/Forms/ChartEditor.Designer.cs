namespace MsCrmTools.ChartManager.Forms
{
    partial class ChartEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblDataDescription = new System.Windows.Forms.Label();
            this.tecDataDescription = new ICSharpCode.TextEditor.TextEditorControl();
            this.lblVisualizationDescription = new System.Windows.Forms.Label();
            this.tecVisualizationDescription = new ICSharpCode.TextEditor.TextEditorControl();
            this.pnlTitle.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.White;
            this.pnlTitle.Controls.Add(this.label1);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1058, 100);
            this.pnlTitle.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chart Editor";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 790);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1058, 51);
            this.panel2.TabIndex = 1;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(821, 5);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(112, 35);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(942, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 35);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(159, 106);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(887, 26);
            this.txtName.TabIndex = 2;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(159, 138);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(887, 26);
            this.txtDescription.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 109);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(51, 20);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Name";
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(12, 141);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(89, 20);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "Description";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 170);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblDataDescription);
            this.splitContainer1.Panel1.Controls.Add(this.tecDataDescription);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblVisualizationDescription);
            this.splitContainer1.Panel2.Controls.Add(this.tecVisualizationDescription);
            this.splitContainer1.Size = new System.Drawing.Size(1058, 614);
            this.splitContainer1.SplitterDistance = 307;
            this.splitContainer1.TabIndex = 6;
            // 
            // lblDataDescription
            // 
            this.lblDataDescription.AutoSize = true;
            this.lblDataDescription.Location = new System.Drawing.Point(12, 0);
            this.lblDataDescription.Name = "lblDataDescription";
            this.lblDataDescription.Size = new System.Drawing.Size(128, 20);
            this.lblDataDescription.TabIndex = 6;
            this.lblDataDescription.Text = "Data Description";
            // 
            // tecDataDescription
            // 
            this.tecDataDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tecDataDescription.IsReadOnly = false;
            this.tecDataDescription.Location = new System.Drawing.Point(159, 3);
            this.tecDataDescription.Name = "tecDataDescription";
            this.tecDataDescription.Size = new System.Drawing.Size(887, 301);
            this.tecDataDescription.TabIndex = 0;
            // 
            // lblVisualizationDescription
            // 
            this.lblVisualizationDescription.AutoSize = true;
            this.lblVisualizationDescription.Location = new System.Drawing.Point(12, 9);
            this.lblVisualizationDescription.Name = "lblVisualizationDescription";
            this.lblVisualizationDescription.Size = new System.Drawing.Size(144, 20);
            this.lblVisualizationDescription.TabIndex = 8;
            this.lblVisualizationDescription.Text = "Presentation Desc.";
            // 
            // tecVisualizationDescription
            // 
            this.tecVisualizationDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tecVisualizationDescription.IsReadOnly = false;
            this.tecVisualizationDescription.Location = new System.Drawing.Point(159, -1);
            this.tecVisualizationDescription.Name = "tecVisualizationDescription";
            this.tecVisualizationDescription.Size = new System.Drawing.Size(887, 304);
            this.tecVisualizationDescription.TabIndex = 7;
            // 
            // ChartEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 841);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlTitle);
            this.Name = "ChartEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chart Editor";
            this.Load += new System.EventHandler(this.ChartEditor_Load);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private ICSharpCode.TextEditor.TextEditorControl tecDataDescription;
        private System.Windows.Forms.Label lblDataDescription;
        private System.Windows.Forms.Label lblVisualizationDescription;
        private ICSharpCode.TextEditor.TextEditorControl tecVisualizationDescription;
    }
}