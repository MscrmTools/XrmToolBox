using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
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

        private static PluginManagerExtended instance;
        private CompositionContainer container;
        private DirectoryCatalog directoryCatalog;

        private PluginManagerExtended()
        {
            if (!Directory.Exists(PluginPath))
            {
                Directory.CreateDirectory(PluginPath);
            }
        }

        public event EventHandler PluginsListUpdated;

        public static PluginManagerExtended Instance => instance ?? (instance = new PluginManagerExtended());
        public bool IsWatchingForNewPlugins { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IXrmToolBoxPlugin, IPluginComposedMetadata>> PluginsX { get; set; }

        public Manifest Manifest { get; set; }
        public IReadOnlyCollection<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> Plugins { get; set; }

        public IEnumerable<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> ValidatedPlugins
        {
            get { return Plugins?.Where(p => !ValidationErrors.ContainsKey(p.Metadata.Name)); }
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

                var ipForm = new InvalidPluginsForm(ValidationErrors);
                ipForm.TopMost = true;
                ipForm.ShowDialog();

                throw;
            }
        }

	    private void RescanIfRequired()
	    {
		    if (Manifest == null)
		    {
			    Manifest = ManifestLoader.LoadDefaultManifest();
			    Plugins = ManifestLoader.LoadPlugins(Manifest);
		    }

		    directoryCatalog.Refresh();

		    var loadedFiles = directoryCatalog.LoadedFiles;
		    var scannedFiles = Manifest?.ScannedAssemblies ?? Array.Empty<AssemblyInfo>();
		    var isRequireScan = loadedFiles.Count != scannedFiles.Length;

		    if (!isRequireScan)
		    {
			    for (var i = 0; i < loadedFiles.Count; i++)
			    {
				    var loadedFileName = loadedFiles[i].ToLower();
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
	    }

	    private void LoadParts(bool isRetry = false)
	    {
			try
			{
				container.ComposeParts(this);

				Manifest = ManifestLoader.CreateManifest(PluginsX.ToArray(), directoryCatalog);
				ManifestLoader.SaveManifest(Manifest);
				Plugins = ManifestLoader.LoadPlugins(Manifest);

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

            foreach (var plugin in Plugins)
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

        private void watcher_EventRaised(object sender, FileSystemEventArgs e)
        {
            try
            {
                ((FileSystemWatcher)sender).EnableRaisingEvents = false;

                if (IsWatchingForNewPlugins)
                {
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