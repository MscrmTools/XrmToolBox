using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
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

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IXrmToolBoxPlugin, IPluginMetadata>> Plugins { get; set; }

        public void Initialize()
        {
            directoryCatalog = new DirectoryCatalog(PluginPath);

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(directoryCatalog);
            container = new CompositionContainer(catalog);
            container.ComposeParts(this);
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

                PluginsListUpdated(this, new EventArgs());
            }
            finally
            {
                ((FileSystemWatcher)sender).EnableRaisingEvents = true;
            }
        }
    }
}