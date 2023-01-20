using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.Manifest;
using XrmToolBox.ToolLibrary.AppCode;
using XrmToolBox.ToolLibrary.Forms;

namespace XrmToolBox.ToolLibrary
{
    [Flags]
    public enum PackageInstallAction
    {
        None = 1,
        Install = 2,
        Update = 4,
        Unavailable = 8
    }

    public class ToolLibrary : IToolLibrary
    {
        #region variables

        public static readonly Version MinCompatibleVersion = new Version(1, 2015, 12, 20);
        private readonly PluginDeletions pendingDeletions;
        private readonly PluginUpdates pendingUpdates;
        private readonly IToolLibrarySettings settings;
        private PackageVersion connectionControlsPackage;
        private FileInfo[] plugins;
        public HttpClient HttpClient { get; private set; }

        #endregion variables

        public ToolLibrary(IToolLibrarySettings settings, Dictionary<string, string> repositories)
        {
            this.settings = settings;
            HttpClient = new HttpClient();
            Repositories = repositories;
            if (Repositories.Count == 0)
            {
                if (!string.IsNullOrEmpty(settings.RepositoryUrl))
                {
                    Repositories.Add("Default", settings.RepositoryUrl);
                }

                if (!string.IsNullOrEmpty(settings.AdditionalRepositories))
                {
                    foreach (var repository in settings.AdditionalRepositories.Split(Environment.NewLine.ToCharArray()))
                    {
                        var parts = repository.Split('|');
                        if (parts.Length != 2) continue;

                        Repositories.Add(parts[0], parts[1]);
                    }
                }
            }

            pendingUpdates = LoadPendingFile<PluginUpdates>("Update.xml") ?? new PluginUpdates();
            pendingUpdates.PreviousProcessId = Process.GetCurrentProcess().Id;

            pendingDeletions = LoadPendingFile<PluginDeletions>("Deletion.xml") ?? new PluginDeletions();
            pendingDeletions.PreviousProcessId = Process.GetCurrentProcess().Id;
        }

        #region Event Handlers

        public event EventHandler OnToolsMetadataRefreshRequested;

        public event EventHandler PluginsUpdated;

        #endregion Event Handlers

        #region Properties

        public bool AllowConnectionControlPreRelease { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> Categories { get; set; }
        public PluginDeletions PendingDeletions => pendingDeletions;
        public PluginUpdates PendingUpdates => pendingUpdates;
        public int PluginsCount => XrmToolBoxPlugins?.Plugins.Count ?? 0;
        public int PluginsUpdatesCount => XrmToolBoxPlugins?.Plugins.Count(p => p.Action == PackageInstallAction.Update) ?? 0;
        public Dictionary<string, string> Repositories { get; private set; }
        public List<IXrmToolBoxLibraryTool> Tools => XrmToolBoxPlugins.Plugins.Select(p => (IXrmToolBoxLibraryTool)p).ToList();

        public XtbPlugins XrmToolBoxPlugins { get; set; }

        #endregion Properties

        #region Methods

        public void AnalyzePackage(XtbPlugin plugin, Version targetVersion = null)
        {
            plugins = new DirectoryInfo(Paths.PluginsPath).GetFiles();
            OnToolsMetadataRefreshRequested?.Invoke(this, new EventArgs());
            var files = plugin.Files;

            bool install = false, update = false, otherFilesFound = false;

            Version version;

            if (string.IsNullOrEmpty(plugin.MinimalXrmToolBoxVersion) || !Version.TryParse(plugin.MinimalXrmToolBoxVersion, out version))
            {
                plugin.Compatibilty = CompatibleState.Other;
            }
            else
            {
                plugin.Compatibilty = IsPluginDependencyCompatible(version);
            }

            var currentVersion = new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
            var currentVersionFound = false;

            var manifest = ManifestLoader.LoadDefaultManifest();

            foreach (var file in files)
            {
                if (Path.GetFileName(file).Length == 0)
                    continue;

                var directoryName = Path.GetDirectoryName(file);
                if (directoryName == null)
                {
                    continue;
                }

                if (directoryName.ToLower().EndsWith("plugins"))
                {
                    // Only check version of files in the Plugins folder
                    var existingPluginFile = plugins.FirstOrDefault(p => file.ToLower().EndsWith(p.Name.ToLower()));
                    if (existingPluginFile == null)
                    {
                        install = true;
                    }
                    else
                    {
                        var pluginInfo = manifest.PluginMetadata
                            .FirstOrDefault(p => p.AssemblyFilename.ToLower() == existingPluginFile.FullName.ToLower());

                        // If a file is found, we check version only if the file
                        // contains classes that implement IXrmToolBoxPlugin
                        if (pluginInfo == null)
                        {
                            otherFilesFound = true;
                            continue;
                        }

                        var existingFileVersion = new Version(pluginInfo.Version);

                        if (existingFileVersion < currentVersion)
                        {
                            currentVersion = existingFileVersion;
                            currentVersionFound = true;
                        }

                        if (targetVersion == null && !existingFileVersion.Equals(new Version(plugin.Version))
                            || targetVersion != null && !existingFileVersion.Equals(targetVersion))
                        {
                            update = true;
                        }
                    }
                }
            }

            if (currentVersionFound)
            {
                plugin.CurrentVersion = currentVersion;
            }

            if (otherFilesFound || update)
            {
                plugin.RequiresXtbRestart = true;
            }

            if (plugin.Compatibilty != CompatibleState.Compatible)
            {
                plugin.Action = PackageInstallAction.Unavailable;
            }
            else if (update)
            {
                plugin.Action = PackageInstallAction.Update;
            }
            else if (install)
            {
                plugin.Action = PackageInstallAction.Install;
            }
            else
            {
                plugin.Action = PackageInstallAction.None;
            }
        }

        public void DownloadPackage(ToolOperationEventArgs e, PluginUpdates pus)
        {
            var cachePackagePath = Path.Combine(Paths.XrmToolBoxPath, "NugetPlugins", $"{e.Plugin.NugetId}.{e.Version}");

            string fullPath, destinationFile;
            if (!Directory.Exists(cachePackagePath))
            {
                Directory.CreateDirectory(cachePackagePath);

                Uri pathUri = new Uri(e.Plugin.DownloadUrl);
                byte[] packageBytes;
                if (pathUri.Scheme == "http" || pathUri.Scheme == "https")
                {
                    packageBytes = HttpClient.GetByteArrayAsync(e.DownloadUrl).GetAwaiter().GetResult();
                }
                else if (pathUri.Scheme == "file")
                {
                    packageBytes = File.ReadAllBytes(e.DownloadUrl);
                }
                else
                {
                    throw new Exception($"Unsupported file path scheme {pathUri.Scheme}");
                }

                using (var ms = new MemoryStream())
                {
                    ms.Write(packageBytes, 0, packageBytes.Length);
                    var package = Package.Open(ms);

                    foreach (var part in package.GetParts())
                    {
                        if (part.Uri.ToString().ToLower().IndexOf("/plugins/") < 0) continue;

                        var fileName = part.Uri.ToString().Split(new string[] { "/Plugins/", "/plugins/" }, StringSplitOptions.RemoveEmptyEntries).Last();
                        fullPath = Path.Combine(cachePackagePath, fileName);
                        destinationFile = Path.Combine(Paths.PluginsPath, fileName);

                        var directory = Path.GetDirectoryName(fullPath);
                        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                        using (var partStream = part.GetStream())
                        {
                            using (var fileStream = File.Create(fullPath))
                            {
                                partStream.Seek(0, SeekOrigin.Begin);
                                partStream.CopyTo(fileStream);
                            }
                        }

                        // XrmToolBox restart is required when a plugin has to be
                        // updated or when a new plugin shares files with other
                        // plugin(s) already installed
                        if (e.Plugin.RequiresXtbRestart || e.Plugin.Action == PackageInstallAction.Install)
                        {
                            pus.Plugins.Add(new PluginUpdate
                            {
                                Name = e.Plugin.Name,
                                Source = fullPath,
                                Destination = destinationFile,
                                RequireRestart = e.Plugin.RequiresXtbRestart
                            });
                        }
                    }
                }
            }
            else
            {
                AddFilesToUpdate(cachePackagePath, pus, e.Plugin);
            }
        }

        public async Task<PackageVersion> GetPackageVersion(string packageName)
        {
            var response = await HttpClient.GetAsync($"https://api-v2v3search-0.nuget.org/query?q={packageName}").ConfigureAwait(false);
            var data = await response.Content.ReadAsStringAsync();

            var jo = JObject.Parse(data);

            var cc = ((JArray)jo["data"]).FirstOrDefault(d => d["id"].ToString() == packageName);

            var rResponse = await HttpClient.GetAsync(cc["registration"].ToString()).ConfigureAwait(false);
            var rData = await rResponse.Content.ReadAsStringAsync();

            var jor = JObject.Parse(rData);

            var latestVersion = ((JArray)((JArray)jor["items"]).Last()["items"]).Last();
            var content = await HttpClient.GetByteArrayAsync(latestVersion["packageContent"].ToString()).ConfigureAwait(false);

            var versionInfoResponse = await HttpClient.GetAsync(latestVersion["@id"].ToString()).ConfigureAwait(false);
            var versionInfoData = await versionInfoResponse.Content.ReadAsStringAsync();
            var jov = JObject.Parse(versionInfoData);

            var ceUrl = jov["catalogEntry"].ToString();
            var ceResponse = await HttpClient.GetAsync(ceUrl).ConfigureAwait(false);
            var ceData = await ceResponse.Content.ReadAsStringAsync();
            var ceo = JObject.Parse(ceData);

            var fullVersion = ceo["version"].ToString();

            var nugetVersion = new Version(fullVersion.Split('-')[0]);
            var release = fullVersion.IndexOf("-") > 0 ? fullVersion.Split('-')[1] : "";
            var isPreRelease = (bool)ceo["isPrerelease"];
            var releasenotes = ceo["releaseNotes"].ToString();

            string version = fullVersion;

            return new PackageVersion(packageName, version, releasenotes, content);
        }

        public XtbPlugin GetPluginByFileName(string filename)
        {
            if (XrmToolBoxPlugins == null)
            {
                LoadTools().Wait();
            }

            return XrmToolBoxPlugins.Plugins.FirstOrDefault(p => p.Files.Any(f => f.ToLower().IndexOf(filename.ToLower(), StringComparison.Ordinal) >= 0));
        }

        public string GetPluginProjectUrlByFileName(string fileName)
        {
            XtbPlugin plugin = GetPluginByFileName(fileName);
            return plugin?.ProjectUrl;
        }

        public IXrmToolBoxLibraryTool GetPluginUpdateByFile(string location)
        {
            var fi = new FileInfo(location);
            var plugin = XrmToolBoxPlugins.Plugins.FirstOrDefault(p => p.Files.Any(f => f.ToLower().Contains(fi.Name.ToLower())));
            if (plugin != null && plugin.Action == PackageInstallAction.Update)
            {
                return plugin;
            }

            return null;
        }

        public void InstallOneToolUpdate(IXrmToolBoxLibraryTool tool, bool onNextRestart, Form form)
        {
            DownloadPackage(new ToolOperationEventArgs(true, (XtbPlugin)tool, new Version(tool.Version), ((XtbPlugin)tool).DownloadUrl), pendingUpdates);

            XmlSerializerHelper.SerializeToFile(pendingUpdates, Path.Combine(Paths.XrmToolBoxPath, "Update.xml"));

            if (!onNextRestart)
            {
                if (DialogResult.Yes == MessageBox.Show(form,
                        @"This application needs to restart to install updated tools (or new tools that share some files with already installed tools). Click Yes to restart this application now",
                        @"Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Application.Restart();
                }
            }
        }

        public async Task<IConnectionControlUpdateSettings> IsConnectionControlsUpdateAvailable(string currentStoredVersion)
        {
            var ca = typeof(McTools.Xrm.Connection.ConnectionDetail).Assembly;
            connectionControlsPackage = await GetPackageVersion("MscrmTools.Xrm.Connection");

            // initial run, curr stored version may be null
            var currStoredVer = currentStoredVersion != null ? new Version(currentStoredVersion.Split('-')[0]) : null;

            bool isNewVersion = ca.GetName().Version < connectionControlsPackage.Version
                   || ca.GetName().Version == connectionControlsPackage.Version &&
                   connectionControlsPackage.Version == currStoredVer &&
                   connectionControlsPackage.ToString() != currentStoredVersion;

            return new ConnectionControlsUpdateSettings
            {
                NewVersion = isNewVersion,
                Version = connectionControlsPackage.ToString(),
                ReleaseNotes = connectionControlsPackage.ReleaseNotes
            };
        }

        public T LoadPendingFile<T>(string fileName)
        {
            if (File.Exists(Path.Combine(Paths.XrmToolBoxPath, fileName)))
            {
                var doc = new XmlDocument();
                doc.Load(Path.Combine(Paths.XrmToolBoxPath, fileName));

                return (T)XmlSerializerHelper.Deserialize(doc.OuterXml, typeof(T));
            }

            var constructorInfo = typeof(T).GetConstructor(Type.EmptyTypes);
            if (constructorInfo != null)
            {
                return (T)constructorInfo.Invoke(null);
            }

            return default(T);
        }

        public async Task LoadTools(bool fromStorePortal = true)
        {
            XrmToolBoxPlugins = new XtbPlugins();

            foreach (var repository in Repositories.Keys)
            {
                string url = Repositories[repository];
                bool isCustomRepo = !url.StartsWith("https://www.xrmtoolbox.com/") && !url.StartsWith("https://xrmtoolboxdev.microsoftcrmportals.com/");

                Uri pathUri = new Uri(url);
                if (pathUri.Scheme == "http" || pathUri.Scheme == "https")
                {
                    await Task.Run(() =>
                    {
                        do
                        {
                            var tmpPlugins = GetContent<XtbPlugins>(url, true);
                            foreach (var plugin in tmpPlugins.Plugins)
                            {
                                plugin.SourceRepositoryName = repository;
                            }
                            XrmToolBoxPlugins.Plugins.AddRange(tmpPlugins.Plugins);
                            url = tmpPlugins.NextLink;
                        }
                        while (url != null);
                    });
                }
                else if (pathUri.Scheme == "file")
                {
                    var serializer = new DataContractJsonSerializer(typeof(XtbPlugins),
                                new DataContractJsonSerializerSettings
                                {
                                    UseSimpleDictionaryFormat = true,
                                    DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss", new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd'T'HH:mm:ss" })
                                });

                    using (StreamReader reader = new StreamReader(url))
                    {
                        var tmpPlugins = (XtbPlugins)serializer.ReadObject(reader.BaseStream);
                        foreach (var plugin in tmpPlugins.Plugins)
                        {
                            plugin.SourceRepositoryName = repository;
                        }
                        XrmToolBoxPlugins.Plugins.AddRange(tmpPlugins.Plugins);
                    }
                }
                else
                {
                    throw new Exception($"Unsupported file path scheme {pathUri.Scheme} for repository");
                }

                if (isCustomRepo)
                {
                    foreach (var plugin in XrmToolBoxPlugins.Plugins)
                    {
                        plugin.IsFromCustomRepo = true;
                    }
                }
            }

            var regex = new Regex("(^[^a-zA-Z]*)");
            foreach (var plugin in XrmToolBoxPlugins.Plugins)
            {
                plugin.CleanedName = regex.Replace(plugin.Name, "");
            }

            plugins = new DirectoryInfo(Paths.PluginsPath).GetFiles();

            Categories = XrmToolBoxPlugins.Plugins
               .Where(p => p.CategoriesList != null)
               .SelectMany(p => p.CategoriesList?.Split(','))
               .Distinct()
               .OrderByDescending(s => s)
               .ToList();

            Parallel.ForEach(XrmToolBoxPlugins.Plugins,
               plugin =>
               {
                   AnalyzePackage(plugin);
               });
        }

        public bool PerformInstallation(PluginUpdates updates, Form form)
        {
            for (int i = updates.Plugins.Count - 1; i >= 0; i--)
            {
                try
                {
                    var pu = updates.Plugins[i];

                    if (pu.RequireRestart) continue;

                    // Can install plugin directly
                    var destinationDirectory = Path.GetDirectoryName(pu.Destination);
                    if (destinationDirectory == null)
                    {
                        continue;
                    }

                    if (!Directory.Exists(destinationDirectory))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }
                    File.Copy(pu.Source, pu.Destination, true);

                    updates.Plugins.RemoveAt(i);
                }
                catch (Exception error)
                {
                    throw new Exception("An error occured while copying files: " + error.Message);
                }
            }

            PluginsUpdated?.Invoke(this, new EventArgs());

            if (updates.Plugins.Any(p => p.RequireRestart))
            {
                XmlSerializerHelper.SerializeToFile(updates, Path.Combine(Paths.XrmToolBoxPath, "Update.xml"));

                if (form is ToolLibraryForm storeForm)
                {
                    form.Invoke(new Action(() =>
                    {
                        if (DialogResult.Yes == MessageBox.Show(form,
                                @"This application needs to restart to install updated tools (or new tools that share some files with already installed tools). Click Yes to restart this application now",
                                @"Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            storeForm.AskForPluginsClosing();

                            Application.Restart();
                        }
                    }));
                }

                return false;
            }

            return true;
        }

        public void PerformUninstallation(PluginDeletions deletions)
        {
            string filePath = Path.Combine(Paths.XrmToolBoxPath, "Deletion.xml");

            XmlSerializerHelper.SerializeToFile(deletions, filePath);

            if (DialogResult.Yes == MessageBox.Show(
                "This application needs to restart to remove tools. Click Yes to restart this application now",
                "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                Application.Restart();
            }
        }

        public async Task<IConnectionControlUpdateSettings> PrepareConnectionControlsUpdate(Form form, bool restart)
        {
            var updates = new PluginUpdates { PreviousProcessId = Process.GetCurrentProcess().Id };

            await AddPackageToInstall(connectionControlsPackage, updates);
            await AddPackageToInstall(await GetPackageVersion("Microsoft.CrmSdk.XrmTooling.CoreAssembly"), updates);
            await AddPackageToInstall(await GetPackageVersion("Microsoft.CrmSdk.XrmTooling.WpfControls"), updates);

            XmlSerializerHelper.SerializeToFile(updates, Path.Combine(Paths.XrmToolBoxPath, "Update.xml"));

            bool returnedValue = false;
            form.Invoke(new Action(() =>
            {
                if (!restart && DialogResult.Yes == MessageBox.Show(form,
                        @"This application needs to restart to install new connection controls. Click Yes to restart this application now",
                        @"Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    returnedValue = true;
                }
            }));

            return new ConnectionControlsUpdateSettings
            {
                RestartNow = returnedValue,
                Version = connectionControlsPackage.ToString()
            };
        }

        public void PrepareUninstallPlugins(List<XtbPlugin> pluginsTodelete, PluginDeletions pds)
        {
            // Get list of files to delete
            foreach (var plugin in pluginsTodelete)
            {
                var conflicts = XrmToolBoxPlugins.Plugins.Where(p =>
                    p.Id != plugin.Id
                    && (p.Action == PackageInstallAction.None || p.Action == PackageInstallAction.Update)
                    && plugin.Files.Any(ff => p.Files.Contains(ff))).ToList();

                if (conflicts.Any())
                {
                    var conflictedFiles = conflicts.SelectMany(c => c.Files).Select(f => f);

                    pds.Plugins.Add(new PluginDeletion
                    {
                        Name = plugin.Name,
                        Conflict = true,
                        Files = plugin.Files.Where(f => !conflictedFiles.Contains(f)).Select(f => Path.GetFileName(f)).ToList()
                    });
                }
                else
                {
                    pds.Plugins.Add(new PluginDeletion
                    {
                        Name = plugin.Name,
                        Files = plugin.Files.Select(f => Path.GetFileName(f)).ToList()
                    });
                }
            }
        }

        public void UninstallByFileName(string fileName)
        {
            var plugin = GetPluginByFileName(fileName.ToLower());

            if (plugin != null)
            {
                PrepareUninstallPlugins(new List<XtbPlugin> { plugin }, pendingDeletions);
                PerformUninstallation(pendingDeletions);
            }
        }

        private void AddFilesToUpdate(string folderPath, PluginUpdates pus, XtbPlugin tool)
        {
            foreach (var file in Directory.GetFiles(folderPath))
            {
                var fileName = new FileInfo(file).Name;
                var fullPath = Path.Combine(folderPath, fileName);
                var destinationFile = Path.Combine(Paths.PluginsPath, fileName);

                // XrmToolBox restart is required when a plugin has to be
                // updated or when a new plugin shares files with other
                // plugin(s) already installed
                if (tool.RequiresXtbRestart || tool.Action == PackageInstallAction.Install)
                {
                    pus.Plugins.Add(new PluginUpdate
                    {
                        Name = tool.Name,
                        Source = fullPath,
                        Destination = destinationFile,
                        RequireRestart = tool.RequiresXtbRestart
                    });
                }
            }

            foreach (var directory in Directory.GetDirectories(folderPath))
            {
                AddFilesToUpdate(directory, pus, tool);
            }
        }

        private async Task AddPackageToInstall(PackageVersion pv, PluginUpdates updates)
        {
            var packageFolder = Path.Combine(Paths.XrmToolBoxPath, "NugetPlugins", $"{pv.PackageName}.{pv}");
            var currentLocation = Assembly.GetExecutingAssembly().Location;
            var folder = Path.GetDirectoryName(currentLocation);

            if (!Directory.Exists(packageFolder))
            {
                Directory.CreateDirectory(packageFolder);
            }

            using (var ms = new MemoryStream(pv.Content))
            using (var package = Package.Open(ms))
            {
                foreach (var part in package.GetParts())
                {
                    if (!part.Uri.ToString().EndsWith(".dll")) continue;

                    var fileName = Path.GetFileName(part.Uri.ToString());

                    using (var fileStream = File.OpenWrite(Path.Combine(packageFolder, fileName)))
                    using (var stream = part.GetStream())
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    updates.Plugins.Add(new PluginUpdate
                    {
                        Source = Path.Combine(packageFolder, fileName),
                        Destination = Path.Combine(folder, fileName),
                        RequireRestart = true
                    });
                }
            }
        }

        private T GetContent<T>(string url, bool fromStoreFromPortalForm) where T : new()
        {
            try
            {
                var request = WebRequest.CreateHttp(url);
                var response = request.GetResponse();
                using (Stream dataStream = response.GetResponseStream())
                {
                    if (dataStream != null)
                    {
                        var serializer = new DataContractJsonSerializer(typeof(T),
                            new DataContractJsonSerializerSettings
                            {
                                UseSimpleDictionaryFormat = true,
                                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss", new DateTimeFormatInfo { FullDateTimePattern = "yyyy-MM-dd'T'HH:mm:ss" })
                            });

                        return (T)serializer.ReadObject(dataStream);
                    }
                }
            }
            catch (WebException error)
            {
                if (fromStoreFromPortalForm &&
                    (error.Status == WebExceptionStatus.ConnectFailure
                    || error.Status == WebExceptionStatus.NameResolutionFailure
                    || error.Status == WebExceptionStatus.ProxyNameResolutionFailure))
                {
                    throw new Exception($"Unable to connect to {url}. Please check your network settings", error);
                }
            }
            return new T();
        }

        private CompatibleState IsPluginDependencyCompatible(Version xtbDependencyVersion)
        {
            if (xtbDependencyVersion >= MinCompatibleVersion
                && xtbDependencyVersion <= Assembly.GetEntryAssembly().GetName().Version)
            {
                return CompatibleState.Compatible;
            }

            if (xtbDependencyVersion < MinCompatibleVersion)
            {
                return CompatibleState.DoesntFitMinimumVersion;
            }

            if (xtbDependencyVersion > Assembly.GetEntryAssembly().GetName().Version)
            {
                return CompatibleState.RequireNewVersionOfXtb;
            }

            return CompatibleState.Other;
        }

        #endregion Methods
    }
}