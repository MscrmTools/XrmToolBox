namespace XrmToolBox.Extensibility.Forms
{
    partial class ErrorDetail
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
            this.panButton = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnIssue = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.panInfo = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlIcon = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panDetails = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtCallStack = new System.Windows.Forms.TextBox();
            this.lblCallStack = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblException = new System.Windows.Forms.Label();
            this.txtException = new System.Windows.Forms.TextBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.lblErrorCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtErrorCode = new System.Windows.Forms.TextBox();
            this.panButton.SuspendLayout();
            this.panInfo.SuspendLayout();
            this.pnlIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panButton
            // 
            this.panButton.Controls.Add(this.btnClose);
            this.panButton.Controls.Add(this.btnIssue);
            this.panButton.Controls.Add(this.btnCopy);
            this.panButton.Controls.Add(this.btnDetails);
            this.panButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panButton.Location = new System.Drawing.Point(0, 704);
            this.panButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panButton.Name = "panButton";
            this.panButton.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.panButton.Size = new System.Drawing.Size(1027, 68);
            this.panButton.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(755, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(262, 50);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // btnIssue
            // 
            this.btnIssue.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnIssue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIssue.Location = new System.Drawing.Point(346, 9);
            this.btnIssue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(168, 50);
            this.btnIssue.TabIndex = 5;
            this.btnIssue.Text = "Create Issue";
            this.btnIssue.UseVisualStyleBackColor = true;
            this.btnIssue.Visible = false;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCopy.Location = new System.Drawing.Point(178, 9);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(168, 50);
            this.btnCopy.TabIndex = 4;
            this.btnCopy.Text = "Copy Details";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Visible = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDetails.Location = new System.Drawing.Point(10, 9);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(4, 5, 10, 5);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(168, 50);
            this.btnDetails.TabIndex = 1;
            this.btnDetails.Text = "Show Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // panInfo
            // 
            this.panInfo.Controls.Add(this.lblHeader);
            this.panInfo.Controls.Add(this.pnlIcon);
            this.panInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panInfo.Location = new System.Drawing.Point(0, 0);
            this.panInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panInfo.Name = "panInfo";
            this.panInfo.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.panInfo.Size = new System.Drawing.Size(1027, 160);
            this.panInfo.TabIndex = 1;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(140, 9);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(877, 142);
            this.lblHeader.TabIndex = 4;
            this.lblHeader.Text = "lblHeader";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlIcon
            // 
            this.pnlIcon.Controls.Add(this.pictureBox1);
            this.pnlIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlIcon.Location = new System.Drawing.Point(10, 9);
            this.pnlIcon.Name = "pnlIcon";
            this.pnlIcon.Size = new System.Drawing.Size(130, 142);
            this.pnlIcon.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::XrmToolBox.Extensibility.Properties.Resources.error_52;
            this.pictureBox1.Location = new System.Drawing.Point(34, 42);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panDetails
            // 
            this.panDetails.Controls.Add(this.splitContainer1);
            this.panDetails.Controls.Add(this.panel1);
            this.panDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panDetails.Location = new System.Drawing.Point(0, 160);
            this.panDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panDetails.Name = "panDetails";
            this.panDetails.Size = new System.Drawing.Size(1027, 544);
            this.panDetails.TabIndex = 2;
            this.panDetails.Visible = false;
            this.panDetails.VisibleChanged += new System.EventHandler(this.panDetails_VisibleChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 152);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtMessage);
            this.splitContainer1.Panel1.Controls.Add(this.lblMessage);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10, 9, 10, 6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtCallStack);
            this.splitContainer1.Panel2.Controls.Add(this.lblCallStack);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10, 3, 10, 9);
            this.splitContainer1.Size = new System.Drawing.Size(1027, 392);
            this.splitContainer1.SplitterDistance = 88;
            this.splitContainer1.SplitterWidth = 12;
            this.splitContainer1.TabIndex = 8;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.SystemColors.Window;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(140, 9);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(877, 73);
            this.txtMessage.TabIndex = 6;
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMessage.Location = new System.Drawing.Point(10, 9);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblMessage.Size = new System.Drawing.Size(130, 73);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "Message";
            // 
            // txtCallStack
            // 
            this.txtCallStack.BackColor = System.Drawing.SystemColors.Window;
            this.txtCallStack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCallStack.Location = new System.Drawing.Point(140, 3);
            this.txtCallStack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCallStack.Multiline = true;
            this.txtCallStack.Name = "txtCallStack";
            this.txtCallStack.ReadOnly = true;
            this.txtCallStack.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCallStack.Size = new System.Drawing.Size(877, 280);
            this.txtCallStack.TabIndex = 6;
            this.txtCallStack.WordWrap = false;
            // 
            // lblCallStack
            // 
            this.lblCallStack.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCallStack.Location = new System.Drawing.Point(10, 3);
            this.lblCallStack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCallStack.Name = "lblCallStack";
            this.lblCallStack.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblCallStack.Size = new System.Drawing.Size(130, 280);
            this.lblCallStack.TabIndex = 0;
            this.lblCallStack.Text = "Call Stack";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblException);
            this.panel1.Controls.Add(this.txtException);
            this.panel1.Controls.Add(this.txtSource);
            this.panel1.Controls.Add(this.lblErrorCode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtErrorCode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1027, 152);
            this.panel1.TabIndex = 0;
            // 
            // lblException
            // 
            this.lblException.AutoSize = true;
            this.lblException.Location = new System.Drawing.Point(10, 28);
            this.lblException.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblException.Name = "lblException";
            this.lblException.Size = new System.Drawing.Size(79, 20);
            this.lblException.TabIndex = 0;
            this.lblException.Text = "Exception";
            // 
            // txtException
            // 
            this.txtException.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtException.BackColor = System.Drawing.SystemColors.Window;
            this.txtException.Location = new System.Drawing.Point(141, 23);
            this.txtException.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtException.Name = "txtException";
            this.txtException.ReadOnly = true;
            this.txtException.Size = new System.Drawing.Size(873, 26);
            this.txtException.TabIndex = 1;
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.BackColor = System.Drawing.SystemColors.Window;
            this.txtSource.Location = new System.Drawing.Point(141, 115);
            this.txtSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(873, 26);
            this.txtSource.TabIndex = 10;
            // 
            // lblErrorCode
            // 
            this.lblErrorCode.AutoSize = true;
            this.lblErrorCode.Location = new System.Drawing.Point(10, 74);
            this.lblErrorCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblErrorCode.Name = "lblErrorCode";
            this.lblErrorCode.Size = new System.Drawing.Size(86, 20);
            this.lblErrorCode.TabIndex = 2;
            this.lblErrorCode.Text = "Error Code";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Source";
            // 
            // txtErrorCode
            // 
            this.txtErrorCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtErrorCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtErrorCode.Location = new System.Drawing.Point(141, 69);
            this.txtErrorCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtErrorCode.Name = "txtErrorCode";
            this.txtErrorCode.ReadOnly = true;
            this.txtErrorCode.Size = new System.Drawing.Size(873, 26);
            this.txtErrorCode.TabIndex = 3;
            // 
            // ErrorDetail
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1027, 772);
            this.ControlBox = false;
            this.Controls.Add(this.panDetails);
            this.Controls.Add(this.panInfo);
            this.Controls.Add(this.panButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ErrorDetail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error Detail";
            this.TopMost = true;
            this.panButton.ResumeLayout(false);
            this.panInfo.ResumeLayout(false);
            this.pnlIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panDetails.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panButton;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panInfo;
        private System.Windows.Forms.Panel panDetails;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtErrorCode;
        private System.Windows.Forms.Label lblErrorCode;
        private System.Windows.Forms.TextBox txtException;
        private System.Windows.Forms.Label lblException;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtCallStack;
        private System.Windows.Forms.Label lblCallStack;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlIcon;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}