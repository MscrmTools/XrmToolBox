using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace XrmToolBox.Forms
{
    public partial class NewVersionForm : Form
    {
        private const string style = "<style>*{font-family:Segoe UI;}</style>";
        private readonly Uri downloadUrl;

        public NewVersionForm(string currentVersion, string newVersion, string description, string userName, string repositoryName, Uri downloadUrl = null)
        {
            InitializeComponent();

            lblCurrentVersion.Text = string.Format(lblCurrentVersion.Text, currentVersion);
            lblNewVersion.Text = string.Format(lblNewVersion.Text, newVersion);
            webBrowser1.DocumentText = style + description;
            webBrowser1.ScriptErrorsSuppressed = true;

            if (downloadUrl == null)
            {
                this.downloadUrl = new Uri(string.Format("https://github.com/{0}/{1}/releases", userName, repositoryName));
            }
            else
            {
                this.downloadUrl = downloadUrl;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            var currentAssemblyFolder = new FileInfo(Assembly.GetExecutingAssembly().FullName).DirectoryName;
            var updaterFile = Path.Combine(currentAssemblyFolder, "XrmToolBox.AutoUpdater.exe");
            var urlIsZip = downloadUrl.ToString().ToLowerInvariant().EndsWith(".zip");

            if (File.Exists(updaterFile) && urlIsZip)
            {
                var destinationFolder =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                        "XrmToolBox");
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }

                var destinationFile = Path.Combine(destinationFolder, "XrmToolBox.AutoUpdater.exe");

                File.Copy(updaterFile, destinationFile, true);

                Process.Start(destinationFile, downloadUrl.ToString());
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                if (urlIsZip)
                {
                    MessageBox.Show(this,
                        "Auto updater has not been found! The new version will be downloaded only. Please install it manually");
                }
                Process.Start(downloadUrl.ToString());
            }
        }
    }
}