namespace MsCrmTools.WebResourcesManager.Forms
{
	partial class FindAndReplaceForm
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
            this.cbWholeWord = new System.Windows.Forms.CheckBox();
            this.cbCaseSensitive = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbSearchUp = new System.Windows.Forms.CheckBox();
            this.cbWildcards = new System.Windows.Forms.CheckBox();
            this.cbRegex = new System.Windows.Forms.CheckBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpFind = new System.Windows.Forms.TabPage();
            this.btnFindNext = new System.Windows.Forms.Button();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpReplace = new System.Windows.Forms.TabPage();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.btnFindNext2 = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.txtFind2 = new System.Windows.Forms.TextBox();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tpFind.SuspendLayout();
            this.tpReplace.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbWholeWord
            // 
            this.cbWholeWord.AutoSize = true;
            this.cbWholeWord.Location = new System.Drawing.Point(17, 42);
            this.cbWholeWord.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbWholeWord.Name = "cbWholeWord";
            this.cbWholeWord.Size = new System.Drawing.Size(162, 24);
            this.cbWholeWord.TabIndex = 5;
            this.cbWholeWord.Text = "Match whole word";
            this.cbWholeWord.UseVisualStyleBackColor = true;
            // 
            // cbCaseSensitive
            // 
            this.cbCaseSensitive.AutoSize = true;
            this.cbCaseSensitive.Location = new System.Drawing.Point(17, 8);
            this.cbCaseSensitive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCaseSensitive.Name = "cbCaseSensitive";
            this.cbCaseSensitive.Size = new System.Drawing.Size(117, 24);
            this.cbCaseSensitive.TabIndex = 4;
            this.cbCaseSensitive.Text = "Match case";
            this.cbCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbSearchUp);
            this.panel1.Controls.Add(this.cbWildcards);
            this.panel1.Controls.Add(this.cbRegex);
            this.panel1.Controls.Add(this.cbWholeWord);
            this.panel1.Controls.Add(this.cbCaseSensitive);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 221);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 120);
            this.panel1.TabIndex = 11;
            // 
            // cbSearchUp
            // 
            this.cbSearchUp.AutoSize = true;
            this.cbSearchUp.Location = new System.Drawing.Point(283, 76);
            this.cbSearchUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSearchUp.Name = "cbSearchUp";
            this.cbSearchUp.Size = new System.Drawing.Size(108, 24);
            this.cbSearchUp.TabIndex = 8;
            this.cbSearchUp.Text = "Search up";
            this.cbSearchUp.UseVisualStyleBackColor = true;
            // 
            // cbWildcards
            // 
            this.cbWildcards.AutoSize = true;
            this.cbWildcards.Location = new System.Drawing.Point(283, 42);
            this.cbWildcards.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbWildcards.Name = "cbWildcards";
            this.cbWildcards.Size = new System.Drawing.Size(104, 24);
            this.cbWildcards.TabIndex = 7;
            this.cbWildcards.Text = "Wildcards";
            this.cbWildcards.UseVisualStyleBackColor = true;
            // 
            // cbRegex
            // 
            this.cbRegex.AutoSize = true;
            this.cbRegex.Location = new System.Drawing.Point(283, 8);
            this.cbRegex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbRegex.Name = "cbRegex";
            this.cbRegex.Size = new System.Drawing.Size(173, 24);
            this.cbRegex.TabIndex = 6;
            this.cbRegex.Text = "Regular Expression";
            this.cbRegex.UseVisualStyleBackColor = true;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpFind);
            this.tabMain.Controls.Add(this.tpReplace);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(533, 221);
            this.tabMain.TabIndex = 12;
            // 
            // tpFind
            // 
            this.tpFind.Controls.Add(this.btnFindNext);
            this.tpFind.Controls.Add(this.txtFind);
            this.tpFind.Controls.Add(this.label1);
            this.tpFind.Location = new System.Drawing.Point(4, 29);
            this.tpFind.Name = "tpFind";
            this.tpFind.Padding = new System.Windows.Forms.Padding(3);
            this.tpFind.Size = new System.Drawing.Size(525, 188);
            this.tpFind.TabIndex = 0;
            this.tpFind.Text = "Find";
            this.tpFind.UseVisualStyleBackColor = true;
            // 
            // btnFindNext
            // 
            this.btnFindNext.Location = new System.Drawing.Point(407, 147);
            this.btnFindNext.Name = "btnFindNext";
            this.btnFindNext.Size = new System.Drawing.Size(112, 35);
            this.btnFindNext.TabIndex = 13;
            this.btnFindNext.Text = "Find Next";
            this.btnFindNext.UseVisualStyleBackColor = true;
            this.btnFindNext.Click += new System.EventHandler(this.FindNextClick);
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.Location = new System.Drawing.Point(13, 28);
            this.txtFind.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(503, 26);
            this.txtFind.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Text to Find:";
            // 
            // tpReplace
            // 
            this.tpReplace.Controls.Add(this.btnReplaceAll);
            this.tpReplace.Controls.Add(this.btnFindNext2);
            this.tpReplace.Controls.Add(this.btnReplace);
            this.tpReplace.Controls.Add(this.txtFind2);
            this.tpReplace.Controls.Add(this.txtReplace);
            this.tpReplace.Controls.Add(this.label2);
            this.tpReplace.Controls.Add(this.label3);
            this.tpReplace.Location = new System.Drawing.Point(4, 29);
            this.tpReplace.Name = "tpReplace";
            this.tpReplace.Padding = new System.Windows.Forms.Padding(3);
            this.tpReplace.Size = new System.Drawing.Size(525, 188);
            this.tpReplace.TabIndex = 1;
            this.tpReplace.Text = "Replace";
            this.tpReplace.UseVisualStyleBackColor = true;
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(410, 147);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(112, 35);
            this.btnReplaceAll.TabIndex = 13;
            this.btnReplaceAll.Text = "Replace All";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.ReplaceAllClick);
            // 
            // btnFindNext2
            // 
            this.btnFindNext2.Location = new System.Drawing.Point(174, 147);
            this.btnFindNext2.Name = "btnFindNext2";
            this.btnFindNext2.Size = new System.Drawing.Size(112, 35);
            this.btnFindNext2.TabIndex = 12;
            this.btnFindNext2.Text = "Find Next";
            this.btnFindNext2.UseVisualStyleBackColor = true;
            this.btnFindNext2.Click += new System.EventHandler(this.FindNext2Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(292, 147);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(112, 35);
            this.btnReplace.TabIndex = 11;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.ReplaceClick);
            // 
            // txtFind2
            // 
            this.txtFind2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind2.Location = new System.Drawing.Point(13, 28);
            this.txtFind2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFind2.Name = "txtFind2";
            this.txtFind2.Size = new System.Drawing.Size(503, 26);
            this.txtFind2.TabIndex = 10;
            // 
            // txtReplace
            // 
            this.txtReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplace.Location = new System.Drawing.Point(13, 84);
            this.txtReplace.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(503, 26);
            this.txtReplace.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Replace with:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Text to Find:";
            // 
            // FindAndReplaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 341);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindAndReplaceForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find and replace";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FindAndReplaceForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tpFind.ResumeLayout(false);
            this.tpFind.PerformLayout();
            this.tpReplace.ResumeLayout(false);
            this.tpReplace.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.CheckBox cbWholeWord;
        private System.Windows.Forms.CheckBox cbCaseSensitive;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpFind;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tpReplace;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.Button btnFindNext2;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.TextBox txtFind2;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbSearchUp;
        private System.Windows.Forms.CheckBox cbWildcards;
        private System.Windows.Forms.CheckBox cbRegex;
	}
}