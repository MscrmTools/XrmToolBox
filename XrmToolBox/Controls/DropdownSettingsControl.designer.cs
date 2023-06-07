using System;
using System.Windows.Forms;

namespace XrmToolBox.Controls
{
    public partial class DropdownSettingsControl : UserControl
    {
        private Label lblTitle;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.cbbList = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(527, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Settings name";
            // 
            // cbbList
            // 
            this.cbbList.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbList.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbList.FormattingEnabled = true;
            this.cbbList.Location = new System.Drawing.Point(10, 49);
            this.cbbList.Name = "cbbList";
            this.cbbList.Size = new System.Drawing.Size(527, 35);
            this.cbbList.TabIndex = 5;
            this.cbbList.SelectedIndexChanged += new System.EventHandler(this.cbbList_SelectedIndexChanged);
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(10, 84);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(527, 39);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "[Description]";
            // 
            // DropdownSettingsControl
            // 
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.cbbList);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DropdownSettingsControl";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.Size = new System.Drawing.Size(547, 139);
            this.Load += new System.EventHandler(this.SwitchSettingsControl_Load);
            this.ResumeLayout(false);

        }

        private ComboBox cbbList;
        private Label lblDescription;
    }
}