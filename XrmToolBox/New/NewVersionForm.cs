using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace XrmToolBox.New
{
    public partial class NewVersionForm : DockContent
    {
        private readonly string version;
        private readonly Uri downloadUrl;

        public NewVersionForm(string version, Uri downloadUrl)
        {
            InitializeComponent();

            this.version = version;
            this.downloadUrl = downloadUrl;

            pictureBox1.BackColor = Color.Transparent;
        }

        private void NewVersionForm_Load(object sender, System.EventArgs e)
        {
            webBrowser1.Url = new Uri($"https://www.xrmtoolbox.com/new-version/?version={version}", UriKind.Absolute);
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, System.EventArgs e)
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

				if (!updaterFile.Equals(destinationFile, StringComparison.OrdinalIgnoreCase)) 
				{
					File.Copy(updaterFile, destinationFile, true);
				}

                var args = Environment.GetCommandLineArgs().ToList();

                Process.Start(destinationFile, $"{downloadUrl} {string.Join(" ", args)}");

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