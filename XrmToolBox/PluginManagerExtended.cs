using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.New;

namespace XrmToolBox
{
    public class PluginManagerExtended : MarshalByRefObject
    {
        //private static readonly string PluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
        private static readonly string PluginPath = Paths.PluginsPath;

        private CompositionContainer container;
        private DirectoryCatalog directoryCatalog;

        public PluginManagerExtended(Form parentForm)
        {
            if (!Directory.Exists(PluginPath))
            {
                Directory.CreateDirectory(PluginPath);
            }

            var watcher = new FileSystemWatcher(PluginPath)
            {
                EnableRaisingEvents = true,
                Filter = "*.dll",
                NotifyFilter = NotifyFilters.FileName,
                SynchronizingObject = parentForm
            };
            watcher.Created += watcher_EventRaised;
        }

        public event EventHandler PluginsListUpdated;

        public Dictionary<string, string> ValidationErrors { get; set; }

        public bool IsWatchingForNewPlugins { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> Plugins { get; set; }

        public IEnumerable<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> ValidatedPlugins
        {
            get { return Plugins?.Where(p => !ValidationErrors.ContainsKey(p.Metadata.Name)); }
        }

        public void Initialize()
        {
            ValidationErrors = new Dictionary<string, string>();

            try
            {
                directoryCatalog = new DirectoryCatalog(PluginPath);

                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(directoryCatalog);
                container = new CompositionContainer(catalog);
                container.ComposeParts(this);
                ValidatePlugins();
            }
            catch (ReflectionTypeLoadException ex)
            {
                ValidationErrors = new Dictionary<string, string>();

                foreach (var exception in ex.LoaderExceptions)
                {
                    var assemblies = ex.Types.Select(t => t?.Assembly.FullName ?? "").Distinct();
                    foreach (var assembly in assemblies)
                    {
                        ValidationErrors.Add(assembly, exception.Message);
                    }
                }

                var ipForm = new InvalidPluginsForm(ValidationErrors);
                ipForm.TopMost = true;
                ipForm.ShowDialog();

                throw;
            }
        }

        public void Recompose()
        {
            try
            {
                directoryCatalog.Refresh();
                container.ComposeParts(directoryCatalog.Parts);
                ValidatePlugins();
            }
            catch (ReflectionTypeLoadException e)
            {
                foreach (var exception in e.LoaderExceptions)
                {
                    var source = exception.Source;
                }
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
                    var value = plugin.Value;
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