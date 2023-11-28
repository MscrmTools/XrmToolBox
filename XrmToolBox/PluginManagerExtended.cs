using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.Manifest;
using XrmToolBox.New;

namespace XrmToolBox
{
    public class PluginManagerExtended : MarshalByRefObject
    {
        //private static readonly string PluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
        private static readonly string PluginPath = Paths.PluginsPath;
        private Dictionary<string, string> AssemblyTypesByPath { get; set; }

        private static PluginManagerExtended instance;
        private CompositionContainer container;
        private DirectoryCatalog directoryCatalog;
        
        private PluginManagerExtended()
        {
            AppDomain.CurrentDomain.AssemblyResolve += LookupAssembly;
            if (!Directory.Exists(PluginPath))
            {
                Directory.CreateDirectory(PluginPath);
            }
        }

        public event EventHandler PluginsListUpdated;

        public static PluginManagerExtended Instance => instance ?? (instance = new PluginManagerExtended());
        public bool IsWatchingForNewPlugins { get; set; }

        /// <summary>
        /// Contains all the info of plugins installed and the list of assemblies in the Plugins folder.
        /// </summary>
        public Manifest Manifest { get; set; }

        /// <summary>
        /// [DEPRECATED] This property should not be used anymore; it was replace by <see cref="PluginsExt"/>.
        /// </summary>
        [Obsolete("This property should not be used anymore; it was replace by PluginsExt", true)]
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> Plugins { get; set; }

        /// <summary>
        /// Contains all the info of plugins installed and a factory method to create each plugin when needed.
        /// </summary>
        public IReadOnlyCollection<Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>> PluginsExt { get; set; }

        /// <summary>
        /// [DEPRECATED] This property should not be used anymore; it was replace by <see cref="ValidatedPluginsExt"/>.
        /// </summary>
        [Obsolete("This property should not be used anymore; it was replace by ValidatedPluginsExt", true)]
        public IEnumerable<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> ValidatedPlugins
        {
            get { return Plugins?.Where(p => !ValidationErrors.ContainsKey(p.Metadata.Name)); }
        }

        public IEnumerable<Lazy<IXrmToolBoxPlugin, IPluginMetadataExt>> ValidatedPluginsExt
        {
            get { return PluginsExt?.Where(p => !ValidationErrors.ContainsKey(p.Metadata.Name)); }
        }

        public Dictionary<string, string> ValidationErrors { get; set; }

        public void Initialize(Form parentForm)
        {
            ValidationErrors = new Dictionary<string, string>();

            var watcher = new FileSystemWatcher(PluginPath)
            {
                EnableRaisingEvents = true,
                Filter = "*.dll",
                NotifyFilter = NotifyFilters.FileName,
                SynchronizingObject = parentForm
            };
            watcher.Created += watcher_EventRaised;

            directoryCatalog = new DirectoryCatalog(PluginPath);
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(directoryCatalog);
            container = new CompositionContainer(catalog);

            try
            {
                RescanIfRequired();
            }
            catch (ReflectionTypeLoadException ex)
            {
                ValidationErrors = new Dictionary<string, string>();

                foreach (var exception in ex.LoaderExceptions)
                {
                    var assemblies = ex.Types.Select(t => t?.Assembly.FullName ?? "").Distinct();
                    foreach (var assembly in assemblies.Select(a => string.IsNullOrWhiteSpace(a) ? "unknown" : a))
                    {
                        if (ValidationErrors.ContainsKey(assembly) && ValidationErrors[assembly] == exception.Message)
                        {   // Don't repeat exact same assembly/error
                            continue;
                        }
                        var i = 0;
                        var assemblykey = assembly;
                        while (ValidationErrors.ContainsKey(assemblykey))
                        {   // Don't try to add same assembly name, dictionary will explode
                            assemblykey = $"{assembly} ({++i})";
                        }
                        ValidationErrors.Add(assemblykey, exception.Message);
                    }
                }

                using (var ipForm = new InvalidPluginsForm(ValidationErrors))
                {
                    ipForm.TopMost = true;
                    ipForm.ShowDialog();
                }
                throw;
            }
        }

        public void Recompose()
        {
            try
            {
                RescanIfRequired();
            }
            catch (ReflectionTypeLoadException e)
            {
                ValidationErrors = new Dictionary<string, string>();

                foreach (var exception in e.LoaderExceptions)
                {
                    var assemblies = e.Types.Select(t => t?.Assembly.FullName ?? "").Distinct();
                    foreach (var assembly in assemblies)
                    {
                        if (!ValidationErrors.ContainsKey(assembly))
                            ValidationErrors.Add(assembly, exception.Message);
                    }
                }

                var ipForm = new InvalidPluginsForm(ValidationErrors) { TopMost = true };
                ipForm.ShowDialog();
            }
        }

        public void ValidatePlugins()
        {
            ValidationErrors = new Dictionary<string, string>();

            foreach (var plugin in PluginsExt)
            {
                try
                {
                    // ReSharper disable once UnusedVariable
                    //var value = plugin.Value; // TODO add validation somewhere!
                }
                catch (Exception e)
                {
                    ValidationErrors.Add(plugin.Metadata.Name, e.Message);
                }
            }
        }

        private void LoadParts(bool isRetry = false)
        {
            try
            {
                container.ComposeParts(this);

                Manifest = ManifestLoader.CreateManifest(PluginsExt.ToArray(), directoryCatalog);
                ManifestLoader.SaveManifest(Manifest);
                PluginsExt = ManifestLoader.LoadPlugins(Manifest);

                ValidatePlugins();
            }
            catch
            {
                if (isRetry)
                {
                    throw;
                }

                // rarely, an 'empty stack' error is thrown; let's rescan
                LoadParts(true);
            }
        }

        private void RescanIfRequired()
        {
            if (Manifest == null)
            {
                Manifest = ManifestLoader.LoadDefaultManifest();
                PluginsExt = ManifestLoader.LoadPlugins(Manifest);
            }

            directoryCatalog.Refresh();

            var loadedFiles = directoryCatalog.LoadedFiles;
            var scannedFiles = Manifest?.ScannedAssemblies ?? Array.Empty<AssemblyInfo>();
            var isRequireScan = loadedFiles.Count != scannedFiles.Length;

            if (!isRequireScan)
            {
                for (var i = 0; i < loadedFiles.Count; i++)
                {
                    var loadedFileName = loadedFiles[i].ToLower(CultureInfo.InvariantCulture);
                    var scannedFile = scannedFiles[i];

                    var loadedVersion = AssemblyName.GetAssemblyName(loadedFileName).Version.ToString();
                    var scannedVersion = scannedFile.Version;

                    if (loadedFileName != scannedFile.Name.ToLower() || loadedVersion != scannedVersion)
                    {
                        isRequireScan = true;
                        break;
                    }
                }
            }

            if (isRequireScan)
            {
                LoadParts();
            }

            if (PluginsExt == null)
            {
                PluginsExt = ManifestLoader.LoadPlugins(Manifest);
            }
        }

        private Assembly LookupAssembly(object sender, ResolveEventArgs args)
        {
            if (AssemblyTypesByPath == null)
            {
                AssemblyTypesByPath = LoadAssemblyTypesByPath();
            }

            return AssemblyTypesByPath.TryGetValue(args.Name, out var path) ? Assembly.LoadFrom(path) : null;
        }

        private Dictionary<string, string> LoadAssemblyTypesByPath()
        {
            var assemblyTypesByPath = new Dictionary<string, string>();
            var pluginDirectory = new DirectoryInfo(Paths.PluginsPath);
            foreach (var subDirectory in pluginDirectory.EnumerateDirectories())
            {
                foreach (var file in subDirectory.GetFiles("*.dll"))
                {
                    try
                    {
                        var possibleAssembly = Assembly.ReflectionOnlyLoadFrom(file.FullName);
                        assemblyTypesByPath[possibleAssembly.FullName] = file.FullName;
                    }
                    catch
                    {
                        // Not sure how to tell if the assembly is loaded.  Eat it for now.
                    }
                }
            }
            return assemblyTypesByPath;
        }

        private void watcher_EventRaised(object sender, FileSystemEventArgs e)
        {
            try
            {
                ((FileSystemWatcher)sender).EnableRaisingEvents = false;

                if (IsWatchingForNewPlugins)
                {
                    // Reset to load new plugins
                    AssemblyTypesByPath = null;
                    PluginsListUpdated?.Invoke(this, new EventArgs());
                }
            }
            finally
            {
                ((FileSystemWatcher)sender).EnableRaisingEvents = true;
            }
        }
    }
}