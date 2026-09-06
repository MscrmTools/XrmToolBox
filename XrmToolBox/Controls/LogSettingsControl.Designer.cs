namespace XrmToolBox.Controls
{
    partial class LogSettingsControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClearLogs = new System.Windows.Forms.Button();
            this.btnOpenLogFolder = new System.Windows.Forms.Button();
            this.textBoxSettingsControl1 = new XrmToolBox.Controls.TextBoxSettingsControl();
            this.ddscLogsLevel = new XrmToolBox.Controls.DropdownSettingsControl();
            this.SuspendLayout();
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClearLogs.Location = new System.Drawing.Point(10, 10);
            this.btnClearLogs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(863, 61);
            this.btnClearLogs.TabIndex = 19;
            this.btnClearLogs.Text = "Clear all logs";
            this.btnClearLogs.UseVisualStyleBackColor = true;
            this.btnClearLogs.Click += new System.EventHandler(this.btnClearLogs_Click);
            // 
            // btnOpenLogFolder
            // 
            this.btnOpenLogFolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOpenLogFolder.Location = new System.Drawing.Point(10, 71);
            this.btnOpenLogFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenLogFolder.Name = "btnOpenLogFolder";
            this.btnOpenLogFolder.Size = new System.Drawing.Size(863, 61);
            this.btnOpenLogFolder.TabIndex = 22;
            this.btnOpenLogFolder.Text = "Open Log folder";
            this.btnOpenLogFolder.UseVisualStyleBackColor = true;
            this.btnOpenLogFolder.Click += new System.EventHandler(this.btnOpenLogFolder_Click);
            // 
            // textBoxSettingsControl1
            // 
            this.textBoxSettingsControl1.Description = "Number of days of retention for log files. 0 means no automatic deletion";
            this.textBoxSettingsControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxSettingsControl1.Font = new System.Drawing.Font("Segoe UI Variable Text", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSettingsControl1.IsPassword = false;
            this.textBoxSettingsControl1.Location = new System.Drawing.Point(10, 239);
            this.textBoxSettingsControl1.Multiline = false;
            this.textBoxSettingsControl1.Name = "textBoxSettingsControl1";
            this.textBoxSettingsControl1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.textBoxSettingsControl1.PropertyName = null;
            this.textBoxSettingsControl1.Readonly = false;
            this.textBoxSettingsControl1.Size = new System.Drawing.Size(863, 131);
            this.textBoxSettingsControl1.TabIndex = 24;
            this.textBoxSettingsControl1.Title = "Retention duration for log files (in days)";
            this.textBoxSettingsControl1.ValidationRegEx = "^[0-9]*$";
            this.textBoxSettingsControl1.OnSettingsPropertyChanged += new System.EventHandler<XrmToolBox.AppCode.SettingsPropertyEventArgs>(this.textBoxSettingsControl1_OnSettingsPropertyChanged);
            // 
            // ddscLogsLevel
            // 
            this.ddscLogsLevel.Description = null;
            this.ddscLogsLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ddscLogsLevel.EnumType = null;
            this.ddscLogsLevel.Font = new System.Drawing.Font("Segoe UI Variable Text", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddscLogsLevel.Location = new System.Drawing.Point(10, 132);
            this.ddscLogsLevel.Name = "ddscLogsLevel";
            this.ddscLogsLevel.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.ddscLogsLevel.PropertyName = null;
            this.ddscLogsLevel.Size = new System.Drawing.Size(863, 107);
            this.ddscLogsLevel.TabIndex = 23;
            this.ddscLogsLevel.Title = "Logs level";
            this.ddscLogsLevel.Value = null;
            this.ddscLogsLevel.OnSettingsPropertyChanged += new System.EventHandler<XrmToolBox.AppCode.SettingsPropertyEventArgs>(this.ddscLogsLevel_OnSettingsPropertyChanged);
            // 
            // LogSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxSettingsControl1);
            this.Controls.Add(this.ddscLogsLevel);
            this.Controls.Add(this.btnOpenLogFolder);
            this.Controls.Add(this.btnClearLogs);
            this.Name = "LogSettingsControl";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(883, 484);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.Button btnOpenLogFolder;
        private DropdownSettingsControl ddscLogsLevel;
        private TextBoxSettingsControl textBoxSettingsControl1;
    }
}
