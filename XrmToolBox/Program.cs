// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.AppCode;

namespace XrmToolBox
{
    internal static class Program
    {
        private static readonly string[] rootFiles =
        {
            "McTools.Xrm.Connection.dll",
            "McTools.Xrm.Connection.WinForms.dll",
            "Microsoft.IdentityModel.dll",
            "Microsoft.Crm.Sdk.Proxy.dll",
            "Microsoft.Xrm.Sdk.Deployment.dll",
            "Microsoft.Xrm.Sdk.dll",
            "Microsoft.Xrm.Sdk.Workflow.dll",
            "Microsoft.Xrm.Tooling.Connector.dll",
            "Microsoft.Xrm.Tooling.CrmConnectControl.dll",
            "Microsoft.IdentityModel.Clients.ActiveDirectory.dll",
            "Microsoft.IdentityModel.Clients.ActiveDirectory.WindowsForms.dll",
            "XrmToolBox.Extensibility.dll",
            "McTools.StopAdvertisement.dll",
            "NuGet.Core.dll",
            "Microsoft.Web.XmlTransform.dll"
        };

        private static bool CheckRequiredAssemblies()
        {
            try
            {
                Assembly.Load(
                    "Microsoft.IdentityModel, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35");
                return true;
            }
            catch
            {
                if (
                    MessageBox.Show(
                        "Unable to find Windows Identity Foundation 3.5 on this computer\r\n\r\nThis program cannot work properly.\r\n\r\nDo you want to display information on how to install this required component on your computer?",
                        "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    if (Environment.OSVersion.Version.Major > 6 ||
                        Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor > 1)
                    {
                        Process.Start("http://blogs.technet.com/b/tvishnun1/archive/2012/06/12/windows-identity-foundation-in-windows-8.aspx");
                    }
                    else
                    {
                        Process.Start("http://support.microsoft.com/kb/974405");
                    }
                }

                return false;
            }
        }

        private static void CopyUpdatedPlugins()
        {
            var updateFile = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, "Update.xml");

            if (!File.Exists(updateFile))
                return;

           

            using (StreamReader reader = new StreamReader(updateFile))
            {
                var pus = (PluginUpdates)XmlSerializerHelper.Deserialize(reader.ReadToEnd(), typeof(PluginUpdates));

                try
                {
                    var oldProcess = Process.GetProcessById(pus.PreviousProcessId);
                    if (oldProcess != null)
                    {
                        oldProcess.WaitForExit(1000);
                    }
                }
                catch { }

                foreach (var pu in pus.Plugins)
                {
                    var destinationDirectory = Path.GetDirectoryName(pu.Destination);
                    if (!Directory.Exists(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }
                    File.Copy(pu.Source, pu.Destination, true);
                }
            }

            File.Delete(updateFile);
        }

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                if (!CheckRequiredAssemblies())
                {
                    return;
                }

                SearchAndDestroyPluginsInRootFolder();

                CopyUpdatedPlugins();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(args));
            }
            catch (Exception error)
            {
                const string lockedMessage = "One reason can be that at least one file is locked by Windows. Please unlock each locked files or unlock XrmToolBox.zip before extracting its content";
                MessageBox.Show("An unexpected error occured: " + error + "\r\n\r\n" + lockedMessage, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private static void SearchAndDestroyPluginsInRootFolder()
        {
            var currentDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

            if (currentDirectory == null)
                return;

            var unwantedFiles = currentDirectory.GetFiles("*.dll").Where(f => !rootFiles.Contains(f.Name)).ToList();

            if (unwantedFiles.Any())
            {
                if (DialogResult.Yes == MessageBox.Show(
                    string.Format(
                        "The following unknown file(s) currently exist in the XrmToolBox root folder:\r\n{0}\r\n\r\nThese files could result in unexpected behaviors like preventing the latest version of plugins to load. Would you like to delete these files?",
                        String.Join("\r\n", unwantedFiles.Select(f => "- " + f))), "Warning", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning))
                {
                    foreach (var file in unwantedFiles)
                    {
                        File.Delete(file.FullName);
                    }
                }
            }
        }
    }
}