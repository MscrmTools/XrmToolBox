using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace XrmToolBox.AutoUpdater
{
    public partial class AutoUpdater : Form
    {
        private readonly string backupFolder;
        private readonly string extractedPackage;
        private readonly string packageFilePath;
        private readonly string packageFolder;
        private readonly string packageUrl;
        private string xrmtoolboxExecutablePath;

        public AutoUpdater(string packageUrl)
        {
            InitializeComponent();

            this.packageUrl = packageUrl;

            packageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "XrmToolBox");
            packageFilePath = Path.Combine(packageFolder, "update.zip");
            extractedPackage = Path.Combine(packageFolder, "Update");
            backupFolder = Path.Combine(packageFolder, "Backup_" + DateTime.Now.ToString("yyyyMMdd-HHmmss"));
        }

        private void AutoUpdater_Load(object sender, System.EventArgs e)
        {
            if (File.Exists(packageFilePath))
            {
                File.Delete(packageFilePath);
            }

            if (Directory.Exists(extractedPackage))
            {
                Directory.Delete(extractedPackage, true);
            }

            if (!Directory.Exists(packageFolder))
            {
                Directory.CreateDirectory(packageFolder);
            }

            using (var client = new WebClient())
            {
                lblProgress.Text = "Downloading new version of XrmToolBox...";
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.Headers.Add("user-agent", "XrmToolBox.AutoUpdater");
                client.DownloadFileAsync(new Uri(packageUrl), packageFilePath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(xrmtoolboxExecutablePath);
            Close();
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            pbDownloadFile.Value = 100;
            pbDownloadFile.Style = ProgressBarStyle.Marquee;

            var bw = new BackgroundWorker { WorkerReportsProgress = true };
            bw.DoWork += (b, evt) =>
            {
                var currentAssemby = new FileInfo(Assembly.GetExecutingAssembly().FullName);

                // Décompresser le répertoire
                ((BackgroundWorker)b).ReportProgress(0, "Extracting XrmToolBox package...");
                ZipFile.ExtractToDirectory(packageFilePath, extractedPackage);

                // Déplacer les anciens fichiers (backup)
                ((BackgroundWorker)b).ReportProgress(0, "Backing up existing XrmToolBox version...");
                if (!Directory.Exists(backupFolder))
                {
                    Directory.CreateDirectory(backupFolder);
                }
                CopyDirectoryContent(currentAssemby.DirectoryName, backupFolder, (BackgroundWorker)b, "Backing up");

                // Déplacer les nouveaux fichiers
                ((BackgroundWorker)b).ReportProgress(0, "Copying new files...");
                CopyDirectoryContent(extractedPackage, currentAssemby.DirectoryName, (BackgroundWorker)b, "Copying");
            };
            bw.ProgressChanged += (b, evt) =>
            {
                lblProgress.Text = evt.UserState.ToString();
            };
            bw.RunWorkerCompleted += (b, evt) =>
            {
                if (evt.Error != null)
                {
                    MessageBox.Show(this, "An error occured: " + evt.Error.ToString());
                }

                pbDownloadFile.Value = 100;
                pbDownloadFile.Style = ProgressBarStyle.Continuous;
                lblProgress.Text = "Operation completed";

                lblEndInstructions.Text = string.Format(lblEndInstructions.Text, backupFolder);
                Size = new Size(Size.Width, 150);
            };
            bw.RunWorkerAsync();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pbDownloadFile.Value = e.ProgressPercentage;
            lblProgress.Text = string.Format("Downloading new version of XrmToolBox... ({0}/{1})", e.BytesReceived, e.TotalBytesToReceive);
        }

        private void CopyDirectoryContent(string directoryPath, string destinationDirectoryPath, BackgroundWorker worker, string action)
        {
            var directory = new DirectoryInfo(directoryPath);
            foreach (var fi in directory.GetFiles())
            {
                worker.ReportProgress(0, action + " " + fi.Name);

                fi.CopyTo(Path.Combine(destinationDirectoryPath, fi.Name), true);

                if (fi.Name.ToLower() == "xrmtoolbox.exe")
                {
                    xrmtoolboxExecutablePath = fi.FullName;
                }
            }

            foreach (var di in directory.GetDirectories())
            {
                var newPath = Path.Combine(destinationDirectoryPath, di.Name);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                CopyDirectoryContent(di.FullName, newPath, worker, action);
            }
        }
    }
}