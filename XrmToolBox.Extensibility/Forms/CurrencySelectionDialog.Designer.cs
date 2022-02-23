
namespace XrmToolBox.Extensibility.Forms
{
    partial class CurrencySelectionDialog
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEuro = new System.Windows.Forms.Button();
            this.btnDollar = new System.Windows.Forms.Button();
            this.btnPound = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(655, 42);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Please select a currency";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 42);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(655, 159);
            this.pnlMain.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.Controls.Add(this.btnEuro, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDollar, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPound, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(655, 159);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnEuro
            // 
            this.btnEuro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEuro.Image = global::XrmToolBox.Extensibility.Properties.Resources.money_euro;
            this.btnEuro.Location = new System.Drawing.Point(222, 13);
            this.btnEuro.Name = "btnEuro";
            this.btnEuro.Size = new System.Drawing.Size(209, 133);
            this.btnEuro.TabIndex = 1;
            this.btnEuro.Text = "Euro";
            this.btnEuro.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEuro.UseVisualStyleBackColor = true;
            this.btnEuro.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnDollar
            // 
            this.btnDollar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDollar.Image = global::XrmToolBox.Extensibility.Properties.Resources.money_dollar;
            this.btnDollar.Location = new System.Drawing.Point(13, 13);
            this.btnDollar.Name = "btnDollar";
            this.btnDollar.Size = new System.Drawing.Size(203, 133);
            this.btnDollar.TabIndex = 0;
            this.btnDollar.Text = "US Dollar";
            this.btnDollar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDollar.UseVisualStyleBackColor = true;
            this.btnDollar.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnPound
            // 
            this.btnPound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPound.Image = global::XrmToolBox.Extensibility.Properties.Resources.money_pound;
            this.btnPound.Location = new System.Drawing.Point(437, 13);
            this.btnPound.Name = "btnPound";
            this.btnPound.Size = new System.Drawing.Size(205, 133);
            this.btnPound.TabIndex = 2;
            this.btnPound.Text = "Great Britain Pound";
            this.btnPound.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPound.UseVisualStyleBackColor = true;
            this.btnPound.Click += new System.EventHandler(this.btn_Click);
            // 
            // CurrencySelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 201);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CurrencySelectionDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.pnlMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnEuro;
        private System.Windows.Forms.Button btnDollar;
        private System.Windows.Forms.Button btnPound;
    }
}