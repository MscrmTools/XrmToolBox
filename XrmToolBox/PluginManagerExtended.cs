using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox
{
    public class PluginManagerExtended : MarshalByRefObject
    {
        private static readonly string PluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");
        private CompositionContainer container;
        private DirectoryCatalog directoryCatalog;
        private DateTime lastPluginsUpdate;

        public PluginManagerExtended(Form parentForm)
        {
            lastPluginsUpdate = DateTime.Now;

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

        public bool IsWatchingForNewPlugins { get; set; }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> Plugins { get; set; }

        public void Initialize()
        {
            try
            {
                directoryCatalog = new DirectoryCatalog(PluginPath);

                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(directoryCatalog);
                container = new CompositionContainer(catalog);
                container.ComposeParts(this);
            }
            catch (ReflectionTypeLoadException ex)
            {
                if (ex.LoaderExceptions.Length == 1)
                {
                    throw ex.LoaderExceptions[0];
                }
                var sb = new StringBuilder();
                var i = 1;
                sb.AppendLine("Multiple Exception Occured Attempting to Intialize the Plugin Manager");
                foreach (var exception in ex.LoaderExceptions)
                {
                    sb.AppendLine("Exception " + i++);
                    sb.AppendLine(exception.ToString());
                    sb.AppendLine();
                    sb.AppendLine();
                }

                throw new ReflectionTypeLoadException(ex.Types, ex.LoaderExceptions, sb.ToString());
            }
        }

        public void Recompose()
        {
            directoryCatalog.Refresh();
            container.ComposeParts(directoryCatalog.Parts);
        }

        private void watcher_EventRaised(object sender, FileSystemEventArgs e)
        {
            try
            {
                ((FileSystemWatcher)sender).EnableRaisingEvents = false;

                if (IsWatchingForNewPlugins)
                {
                    PluginsListUpdated(this, new EventArgs());
                }
            }
            finally
            {
                ((FileSystemWatcher)sender).EnableRaisingEvents = true;
            }
        }
    }
}