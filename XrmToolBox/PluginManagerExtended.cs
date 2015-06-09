using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.UserControls;

namespace XrmToolBox
{
    public class PluginManagerExtended : MarshalByRefObject
    {
        private DateTime lastPluginsUpdate;

        public PluginManagerExtended(Form parentForm)
        {
            lastPluginsUpdate = DateTime.Now;
            PluginsControls = new List<PluginModel>();

            var watcher = new FileSystemWatcher(PluginPath)
            {
                EnableRaisingEvents = true,
                Filter = "*.dll",
                NotifyFilter = NotifyFilters.FileName,
                SynchronizingObject = parentForm
            };
            watcher.Created += watcher_EventRaised;
        }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IMsCrmToolsPluginUserControl, IPluginMetadata>> Plugins { get; set; }

        /// <summary>
        /// List of plugins user controls
        /// </summary>
        public List<PluginModel> PluginsControls { get; private set; }


        private CompositionContainer container;
        private DirectoryCatalog directoryCatalog;
        private static readonly string PluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"Plugins");

        public event EventHandler PluginsListUpdated;

        public void Initialize()
        {
            directoryCatalog = new DirectoryCatalog(PluginPath);

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(directoryCatalog);
            container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        void watcher_EventRaised(object sender, FileSystemEventArgs e)
        {
            try
            {
                ((FileSystemWatcher) sender).EnableRaisingEvents = false;

                PluginsListUpdated(this, new EventArgs());
            }
            finally
            {
                ((FileSystemWatcher) sender).EnableRaisingEvents = true;
            }
        }
        
        public void Recompose()
        {
            directoryCatalog.Refresh();
            container.ComposeParts(directoryCatalog.Parts);
        }
    }
}
