using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;
using XrmToolBox.New;
using XrmToolBox.PluginsStore;
using PluginUpdates = XrmToolBox.AppCode.PluginUpdates;

namespace XrmToolBox
{
    internal static class Program
    {
        private static int uninstalltries = 0;

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

            try
            {
                using (StreamReader reader = new StreamReader(updateFile))
                {
                    var pus = (PluginUpdates)XmlSerializerHelper.Deserialize(reader.ReadToEnd(),
                        typeof(PluginUpdates));

                    try
                    {
                        var oldProcess = Process.GetProcessById(pus.PreviousProcessId);
                        oldProcess.WaitForExit();
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
            catch (Exception error)
            {
                MessageBox.Show(
                    $@"An error occured when trying to update files: {error.Message}

Please start XrmToolBox again to fix this problem",
                    @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                if (Path.GetFullPath(currentPluginsPath).ToLowerInvariant() != Path.GetFullPath(Paths.PluginsPath).ToLowerInvariant())
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

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                var isc = new ItSecurityChecker();
                if (isc.IsDisabled())
                {
                    var message =
                        "IT department restricted the access to XrmToolBox.\r\n\r\nPlease contact your administrators if you need access to XrmToolBox";
                    MessageBox.Show(message, "XrmToolBox restriction detected!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

                RedirectAssembly("NuGet.Core");
                RedirectAssembly("Newtonsoft.Json");
                RedirectAssembly("McTools.Xrm.Connection");
                RedirectAssembly("McTools.Xrm.Connection.WinForms");
                RedirectAssembly("XrmToolBox.Extensibility");
                RedirectAssembly("XrmToolBox.PluginsStore");
                RedirectAssembly("Microsoft.Xrm.Sdk");
                RedirectAssembly("Microsoft.Xrm.Sdk.Workflow");
                RedirectAssembly("Microsoft.Crm.Sdk.Proxy");
                RedirectAssembly("Microsoft.Xrm.Tooling.Connector");
                RedirectAssembly("Microsoft.Xrm.Tooling.Ui.Styles");
                RedirectAssembly("Microsoft.Xrm.Tooling.CrmConnectControl");
                RedirectAssembly("Microsoft.IdentityModel.Clients.ActiveDirectory");
                RedirectAssembly("WeifenLuo.WinFormsUI.Docking");
                RedirectAssembly("WeifenLuo.WinFormsUI.Docking.ThemeVS2015");
                RedirectAssembly("ScintillaNET");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new NewForm(args));
            }
            catch (Exception error)
            {
                const string lockedMessage = "One reason can be that at least one file is locked by Windows. Please unblock each locked files or unlock XrmToolBox.zip before extracting its content";
                MessageBox.Show("An unexpected error occured: " + error + "\r\n\r\n" + lockedMessage, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private static void RemovePlugins()
        {
            uninstalltries++;
            var updateFile = Path.Combine(Paths.XrmToolBoxPath, "Deletion.xml");

            if (!File.Exists(updateFile))
                return;

            try
            {
                using (StreamReader reader = new StreamReader(updateFile))
                {
                    var pds = (PluginDeletions)XmlSerializerHelper.Deserialize(reader.ReadToEnd(), typeof(PluginDeletions));

                    try
                    {
                        var oldProcess = Process.GetProcessById(pds.PreviousProcessId);
                        oldProcess.WaitForExit();
                    }
                    catch { }

                    foreach (var pd in pds.Plugins)
                    {
                        foreach (var filePath in pd.Files)
                        {
                            var pathToDelete = Path.Combine(Paths.XrmToolBoxPath, filePath);

                            if (File.Exists(pathToDelete))
                            {
                                File.Delete(pathToDelete);
                            }
                            else
                            {
                                pathToDelete = Path.Combine(Paths.PluginsPath, filePath);
                                if (File.Exists(pathToDelete))
                                {
                                    File.Delete(pathToDelete);
                                }
                            }
                        }
                    }
                }

                File.Delete(updateFile);
            }
            catch (Exception error)
            {
                if (uninstalltries > 3)
                {
                    MessageBox.Show(
                                  $"An error occured when trying to delete some tools: {error.Message}.\n\nPlease start XrmToolBox again to fix this problem.",
                                  @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Thread.Sleep(500);
                    RemovePlugins();
                }
            }
        }
    }
}