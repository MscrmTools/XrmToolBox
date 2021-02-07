
namespace XrmToolBox.Forms
{
    partial class DonationIntroForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DonationIntroForm));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbTools = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnXtbDollars = new System.Windows.Forms.Button();
            this.btnXtbEuros = new System.Windows.Forms.Button();
            this.btnXtbPounds = new System.Windows.Forms.Button();
            this.btnToolDollars = new System.Windows.Forms.Button();
            this.btnToolEuros = new System.Windows.Forms.Button();
            this.btnToolPounds = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(686, 39);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderTitle.Location = new System.Drawing.Point(0, 0);
            this.lblHeaderTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(686, 38);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Thank you for wanting to donate!";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 418);
            this.pnlFooter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(686, 40);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(601, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Controls.Add(this.lblDescription);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 39);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pnlMain.Size = new System.Drawing.Size(686, 379);
            this.pnlMain.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbTools, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnXtbDollars, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnXtbEuros, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnXtbPounds, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnToolDollars, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnToolEuros, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnToolPounds, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 105);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.94048F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.76191F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(672, 268);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label2.Location = new System.Drawing.Point(338, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Donate for a specific tool";
            // 
            // cbbTools
            // 
            this.cbbTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbTools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTools.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbTools.FormattingEnabled = true;
            this.cbbTools.Location = new System.Drawing.Point(338, 34);
            this.cbbTools.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbbTools.Name = "cbbTools";
            this.cbbTools.Size = new System.Drawing.Size(332, 33);
            this.cbbTools.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Donate for XrmToolBox";
            // 
            // btnXtbDollars
            // 
            this.btnXtbDollars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXtbDollars.Image = global::XrmToolBox.Properties.Resources.money_dollar;
            this.btnXtbDollars.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXtbDollars.Location = new System.Drawing.Point(2, 81);
            this.btnXtbDollars.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXtbDollars.Name = "btnXtbDollars";
            this.btnXtbDollars.Size = new System.Drawing.Size(332, 66);
            this.btnXtbDollars.TabIndex = 6;
            this.btnXtbDollars.Text = "In US Dollars";
            this.btnXtbDollars.UseVisualStyleBackColor = true;
            this.btnXtbDollars.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // btnXtbEuros
            // 
            this.btnXtbEuros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXtbEuros.Image = global::XrmToolBox.Properties.Resources.money_euro;
            this.btnXtbEuros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXtbEuros.Location = new System.Drawing.Point(2, 151);
            this.btnXtbEuros.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXtbEuros.Name = "btnXtbEuros";
            this.btnXtbEuros.Size = new System.Drawing.Size(332, 55);
            this.btnXtbEuros.TabIndex = 7;
            this.btnXtbEuros.Text = "In Euros";
            this.btnXtbEuros.UseVisualStyleBackColor = true;
            this.btnXtbEuros.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // btnXtbPounds
            // 
            this.btnXtbPounds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXtbPounds.Image = global::XrmToolBox.Properties.Resources.money_pound;
            this.btnXtbPounds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXtbPounds.Location = new System.Drawing.Point(2, 210);
            this.btnXtbPounds.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXtbPounds.Name = "btnXtbPounds";
            this.btnXtbPounds.Size = new System.Drawing.Size(332, 56);
            this.btnXtbPounds.TabIndex = 8;
            this.btnXtbPounds.Text = "In Pounds Sterling";
            this.btnXtbPounds.UseVisualStyleBackColor = true;
            this.btnXtbPounds.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // btnToolDollars
            // 
            this.btnToolDollars.Image = global::XrmToolBox.Properties.Resources.money_dollar;
            this.btnToolDollars.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnToolDollars.Location = new System.Drawing.Point(338, 81);
            this.btnToolDollars.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnToolDollars.Name = "btnToolDollars";
            this.btnToolDollars.Size = new System.Drawing.Size(332, 66);
            this.btnToolDollars.TabIndex = 9;
            this.btnToolDollars.Text = "In US Dollars";
            this.btnToolDollars.UseVisualStyleBackColor = true;
            this.btnToolDollars.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // btnToolEuros
            // 
            this.btnToolEuros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToolEuros.Image = global::XrmToolBox.Properties.Resources.money_euro;
            this.btnToolEuros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnToolEuros.Location = new System.Drawing.Point(338, 151);
            this.btnToolEuros.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnToolEuros.Name = "btnToolEuros";
            this.btnToolEuros.Size = new System.Drawing.Size(332, 55);
            this.btnToolEuros.TabIndex = 10;
            this.btnToolEuros.Text = "In Euros";
            this.btnToolEuros.UseVisualStyleBackColor = true;
            this.btnToolEuros.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // btnToolPounds
            // 
            this.btnToolPounds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToolPounds.Image = global::XrmToolBox.Properties.Resources.money_pound;
            this.btnToolPounds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnToolPounds.Location = new System.Drawing.Point(338, 210);
            this.btnToolPounds.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnToolPounds.Name = "btnToolPounds";
            this.btnToolPounds.Size = new System.Drawing.Size(332, 56);
            this.btnToolPounds.TabIndex = 11;
            this.btnToolPounds.Text = "In Pounds Sterling";
            this.btnToolPounds.UseVisualStyleBackColor = true;
            this.btnToolPounds.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(7, 6);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(672, 99);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = resources.GetString("lblDescription.Text");
            // 
            // DonationIntroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(686, 458);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DonationIntroForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.DonationIntroForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbbTools;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnXtbDollars;
        private System.Windows.Forms.Button btnXtbEuros;
        private System.Windows.Forms.Button btnXtbPounds;
        private System.Windows.Forms.Button btnToolDollars;
        private System.Windows.Forms.Button btnToolEuros;
        private System.Windows.Forms.Button btnToolPounds;
    }
}