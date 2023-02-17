using System;
using System.Windows.Forms;

namespace XrmToolBox.Controls
{
    public partial class TextBoxSettingsControl<T> : UserControl
    {
        private Label lblTitle;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(521, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Settings name";
            // 
            // txtText
            // 
            this.txtText.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtText.Location = new System.Drawing.Point(10, 49);
            this.txtText.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(521, 29);
            this.txtText.TabIndex = 3;
            this.txtText.TextChanged += new System.EventHandler(this.txtText_TextChanged);
            this.txtText.Leave += new System.EventHandler(this.txtText_Leave);
            this.txtText.KeyDown += TxtText_KeyDown;
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(10, 78);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblDescription.Size = new System.Drawing.Size(521, 42);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "[Description]";
            // 
            // TextBoxSettingsControl
            // 
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TextBoxSettingsControl";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.Size = new System.Drawing.Size(541, 120);
            this.Load += new System.EventHandler(this.SwitchSettingsControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

     
        private TextBox txtText;
        private Label lblDescription;
    }
}