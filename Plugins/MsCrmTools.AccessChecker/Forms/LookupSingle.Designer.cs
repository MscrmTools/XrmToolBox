namespace MsCrmTools.AccessChecker.Forms
{
    partial class LookupSingle
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
            this.lblViews = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cbbViews = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lvResults = new System.Windows.Forms.ListView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblViews
            // 
            this.lblViews.AutoSize = true;
            this.lblViews.Location = new System.Drawing.Point(12, 15);
            this.lblViews.Name = "lblViews";
            this.lblViews.Size = new System.Drawing.Size(35, 13);
            this.lblViews.TabIndex = 0;
            this.lblViews.Text = "Views";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 42);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search";
            // 
            // cbbViews
            // 
            this.cbbViews.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbViews.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbViews.FormattingEnabled = true;
            this.cbbViews.Location = new System.Drawing.Point(108, 12);
            this.cbbViews.Name = "cbbViews";
            this.cbbViews.Size = new System.Drawing.Size(481, 21);
            this.cbbViews.TabIndex = 2;
            this.cbbViews.SelectedIndexChanged += new System.EventHandler(this.CbbViewsSelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(108, 39);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(428, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearchKeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(542, 37);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(47, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Go";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // lvResults
            // 
            this.lvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvResults.FullRowSelect = true;
            this.lvResults.Location = new System.Drawing.Point(15, 65);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(574, 495);
            this.lvResults.TabIndex = 5;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            this.lvResults.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvResultsColumnClick);
            this.lvResults.DoubleClick += new System.EventHandler(this.LvResultsDoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(514, 566);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(433, 566);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // LookupSingle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 601);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvResults);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cbbViews);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblViews);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupSingle";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lookup record";
            this.Load += new System.EventHandler(this.LookupSingleLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblViews;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cbbViews;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}