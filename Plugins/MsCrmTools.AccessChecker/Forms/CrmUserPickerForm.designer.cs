namespace MsCrmTools.AccessChecker.Forms
{
    partial class CrmUserPickerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrmUserPickerForm));
            this.button_Validate = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvUsers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtSearchFilter = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Validate
            // 
            this.button_Validate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Validate.Location = new System.Drawing.Point(364, 620);
            this.button_Validate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Validate.Name = "button_Validate";
            this.button_Validate.Size = new System.Drawing.Size(112, 35);
            this.button_Validate.TabIndex = 1;
            this.button_Validate.Text = "OK";
            this.button_Validate.UseVisualStyleBackColor = true;
            this.button_Validate.Click += new System.EventHandler(this.ButtonValidateClick);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(486, 620);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(112, 35);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 588);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(507, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "You can search with lastname, firstname or user ID (wildcard * allowed)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Search:";
            // 
            // lvUsers
            // 
            this.lvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvUsers.FullRowSelect = true;
            this.lvUsers.Location = new System.Drawing.Point(18, 62);
            this.lvUsers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvUsers.MultiSelect = false;
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.Size = new System.Drawing.Size(578, 519);
            this.lvUsers.SmallImageList = this.imageList1;
            this.lvUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvUsers.TabIndex = 7;
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = System.Windows.Forms.View.Details;
            this.lvUsers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvUsersColumnClick);
            this.lvUsers.SelectedIndexChanged += new System.EventHandler(this.ListViewUsersSelectedIndexChanged);
            this.lvUsers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewUsersMouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Last name";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "First name";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "User Name";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Business Unit";
            this.columnHeader4.Width = 130;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ico_16_8.gif");
            // 
            // txtSearchFilter
            // 
            this.txtSearchFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchFilter.Location = new System.Drawing.Point(93, 22);
            this.txtSearchFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSearchFilter.Name = "txtSearchFilter";
            this.txtSearchFilter.Size = new System.Drawing.Size(433, 26);
            this.txtSearchFilter.TabIndex = 6;
            this.txtSearchFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearchFilterKeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(537, 18);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(62, 35);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Go";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.ButtonSearchClick);
            // 
            // CrmUserPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 674);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lvUsers);
            this.Controls.Add(this.txtSearchFilter);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Validate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CrmUserPickerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Validate;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvUsers;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox txtSearchFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}