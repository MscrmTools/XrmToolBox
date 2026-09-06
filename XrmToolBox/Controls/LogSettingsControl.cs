using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace XrmToolBox.Controls
{
    public partial class LogSettingsControl : UserControl
    {
        public LogSettingsControl()
        {
            InitializeComponent();

            ddscLogsLevel.EnumType = typeof(LogManager.Level);
            ddscLogsLevel.Value = Options.Instance.LogLevel;

            textBoxSettingsControl1.Text = Options.Instance.LogRetentionInDays.ToString(CultureInfo.InvariantCulture);
        }

        private void btnClearLogs_Click(object sender, System.EventArgs e)
        {
            var rootPath = Paths.LogsPath;
            if (System.IO.Directory.Exists(rootPath))
            {
                foreach (var file in System.IO.Directory.GetFiles(rootPath, "*.log"))
                {
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"Error deleting log file {file}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                MessageBox.Show(this, rootPath + " has been cleared.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Log folder does not exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOpenLogFolder_Click(object sender, System.EventArgs e)
        {
            if (Directory.Exists(Paths.LogsPath) == false)
            {
                Directory.CreateDirectory(Paths.LogsPath);
            }

            Process.Start($"\"{Paths.LogsPath}\"");
        }

        private void ddscLogsLevel_OnSettingsPropertyChanged(object sender, AppCode.SettingsPropertyEventArgs e)
        {
            Options.Instance.LogLevel = e.Value as Extensibility.LogManager.Level? ?? Extensibility.LogManager.Level.Warning;
            Options.Instance.ReportSettingsChange(e);

            LogManager.LogLevel = Options.Instance.LogLevel;
            McTools.Xrm.Connection.AppCode.LogManager.LogLevel = (McTools.Xrm.Connection.AppCode.LogManager.Level)Options.Instance.LogLevel;
        }

        private void textBoxSettingsControl1_OnSettingsPropertyChanged(object sender, AppCode.SettingsPropertyEventArgs e)
        {
            Options.Instance.LogRetentionInDays = int.Parse(e.Value?.ToString() ?? "0");
            Options.Instance.ReportSettingsChange(e);
        }
    }
}