using System;
using System.Windows.Forms;

namespace XrmToolBox.Controls
{
    public partial class SwitchSettingsControl : UserControl
    {
        private Label lblDescription;
        private Label lblTitle;
        private SwitchControl switchControl1;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.switchControl1 = new XrmToolBox.Controls.SwitchControl();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(792, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Settings name";
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(10, 88);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(792, 89);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "[Description]";
            // 
            // switchControl1
            // 
            this.switchControl1.Checked = false;
            this.switchControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.switchControl1.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switchControl1.Location = new System.Drawing.Point(10, 49);
            this.switchControl1.Margin = new System.Windows.Forms.Padding(4);
            this.switchControl1.Name = "switchControl1";
            this.switchControl1.Size = new System.Drawing.Size(792, 39);
            this.switchControl1.TabIndex = 1;
            // 
            // SwitchSettingsControl
            // 
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.switchControl1);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SwitchSettingsControl";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.Size = new System.Drawing.Size(812, 161);
            this.Load += new System.EventHandler(this.SwitchSettingsControl_Load);
            this.EnabledChanged += new System.EventHandler(this.SwitchSettingsControl_EnabledChanged);
            this.ResumeLayout(false);

        }
    }
}