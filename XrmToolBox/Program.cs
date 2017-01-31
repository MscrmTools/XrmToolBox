using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.PluginsStore;
using PluginUpdates = XrmToolBox.AppCode.PluginUpdates;

namespace XrmToolBox
{
    internal static class Program
    {
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

                foreach (var arg in args)
                {
                    if (arg.ToLower().StartsWith("/overridepath:"))
                    {
                        var parts = arg.Split(':');
                        Paths.OverrideRootPath(string.Join(":", parts.Skip(1)));
                    }
                }

                InitializePluginsFolder();
                CopyUpdatedPlugins();
                RemovePlugins();

                RedirectAssembly("McTools.Xrm.Connection");
                RedirectAssembly("McTools.Xrm.Connection.WinForms");
                RedirectAssembly("XrmToolBox.Extensibility");
                RedirectAssembly("XrmToolBox.PluginsStore");
                RedirectAssembly("Microsoft.Xrm.Tooling.Connector");
                RedirectAssembly("Microsoft.IdentityModel.Clients.ActiveDirectory");
                RedirectAssembly("Microsoft.IdentityModel.Clients.ActiveDirectory.WindowsForms");

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

        public static void RedirectAssembly(string shortName)
        {
            var targetAssemblyName = Assembly.Load(shortName).GetName();
            var targetVersion = targetAssemblyName.Version;
            var targetPublicKeyToken = targetAssemblyName.GetPublicKeyToken();

            ResolveEventHandler handler = (sender, args) =>
            {
                var requestedAssembly = new AssemblyName(args.Name);
                if (requestedAssembly.Name != shortName)
                {
                    return null;
                }

                var alreadyLoadedAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.GetName().Name == requestedAssembly.Name);

                if (alreadyLoadedAssembly != null)
                {
                    return alreadyLoadedAssembly;
                }

                requestedAssembly.Version = targetVersion;
                requestedAssembly.SetPublicKeyToken(targetPublicKeyToken);
                requestedAssembly.CultureInfo = CultureInfo.InvariantCulture;


                return Assembly.Load(requestedAssembly);
            };

            AppDomain.CurrentDomain.AssemblyResolve += handler;
        }

        /// <summary>
        /// Performs checks to ensure that Windows Identity Foundation is available
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Updates plugin that need to be updated after having use Plugins Store
        /// </summary>
        private static void CopyUpdatedPlugins()
        {
            var updateFile = Path.Combine(Paths.XrmToolBoxPath, "Update.xml");

            if (!File.Exists(updateFile))
                return;

            using (StreamReader reader = new StreamReader(updateFile))
            {
                var pus = (PluginUpdates)XmlSerializerHelper.Deserialize(reader.ReadToEnd(), typeof(PluginUpdates));

                try
                {
                    var oldProcess = Process.GetProcessById(pus.PreviousProcessId);
                    oldProcess.WaitForExit(1000);
                }
                catch { }

                foreach (var pu in pus.Plugins)
                {
                    if (!Directory.Exists(Path.GetDirectoryName(pu.Destination)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(pu.Destination));
                    }

                    File.Copy(pu.Source, pu.Destination, true);
                }
            }

            File.Delete(updateFile);
        }

        private static void RemovePlugins()
        {
            var updateFile = Path.Combine(Paths.XrmToolBoxPath, "Deletion.xml");

            if (!File.Exists(updateFile))
                return;

            using (StreamReader reader = new StreamReader(updateFile))
            {
                var pds = (PluginDeletions) XmlSerializerHelper.Deserialize(reader.ReadToEnd(), typeof(PluginDeletions));
                foreach (var pd in pds.Plugins)
                {
                    foreach (var filePath in pd.Files)
                    {
                        var pathToDelete = Path.Combine(Paths.XrmToolBoxPath, filePath);

                        if (File.Exists(pathToDelete))
                        {
                            File.Delete(pathToDelete);
                        }
                    }
                }
            }

            File.Delete(updateFile);
        }

        /// <summary>
        /// Ensure that plugins are deployed to roaming user profile
        /// </summary>
        private static void InitializePluginsFolder()
        {
            if (!Directory.Exists(Paths.PluginsPath))
            {
                Directory.CreateDirectory(Paths.PluginsPath);
            }

            // Find archive that contains plugins to deploy
            var assembly = Assembly.GetExecutingAssembly();
            if (assembly.Location == null)
            {
                throw new NullReferenceException("Executing assembly is null!");
            }

            var currentDirectory = new FileInfo(assembly.Location).DirectoryName;
            if (currentDirectory == null)
            {
                throw new NullReferenceException("Current folder is null!");
            }

            // Check if previous installation contains a "Plugins" folder
            var currentPluginsPath = Path.Combine(currentDirectory, "Plugins");
            if (Directory.Exists(currentPluginsPath))
            {
                if (Path.GetFullPath(currentPluginsPath) != Path.GetFullPath(Paths.PluginsPath))
                {
                    foreach (FileInfo fi in new DirectoryInfo(currentPluginsPath).GetFiles())
                    {
                        using (FileStream sourceStream = new FileStream(fi.FullName, FileMode.Open))
                        {
                            using (
                                FileStream destStream = new FileStream(Path.Combine(Paths.PluginsPath, fi.Name),
                                    FileMode.Create))
                            {
                                destStream.Lock(0, sourceStream.Length);
                                sourceStream.CopyTo(destStream);
                                destStream.Unlock(0, sourceStream.Length);
                            }
                        }
                    }

                    Directory.Delete(currentPluginsPath, true);
                }
            }

            // Then updates plugins with latest version of plugins (zipped)
            var pluginsZipFilePath = Path.Combine(currentDirectory, "Plugins.zip");

            if (!File.Exists(pluginsZipFilePath))
            {
                return;
            }

            // Extract content of plugins archive to a temporary folder
            var tempPath = string.Format("{0}_Temp", Paths.PluginsPath);

            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }

            ZipFile.ExtractToDirectory(pluginsZipFilePath, tempPath);

            // Moves all plugins to appropriate folder if version is greater
            // to the version in place
            foreach (var fi in new DirectoryInfo(tempPath).GetFiles())
            {
                var targetFile = Path.Combine(Paths.PluginsPath, fi.Name);

                if (File.Exists(targetFile))
                {
                    if (fi.Extension.ToLower() != ".dll")
                    {
                        File.Copy(fi.FullName, targetFile, true);
                        continue;
                    }

                    var fiSourceVersion = new Version(FileVersionInfo.GetVersionInfo(fi.FullName).FileVersion);
                    var fiTargetVersion = new Version(FileVersionInfo.GetVersionInfo(targetFile).FileVersion);

                    if (fiSourceVersion > fiTargetVersion)
                    {
                        // If version to deploy is newer than current version
                        // Delete current version and copy the new one
                        File.Copy(fi.FullName, targetFile, true);
                    }
                }
                else
                {
                    File.Move(fi.FullName, targetFile);
                }
            }

            // Delete temporary folder
            Directory.Delete(tempPath, true);

            // Delete zip file
            File.Delete(pluginsZipFilePath);
        }
    }
}