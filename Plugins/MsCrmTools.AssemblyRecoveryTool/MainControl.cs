// PROJECT : MsCrmTools.AssemblyRecoveryTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using MsCrmTools.AssemblyRecoveryTool.AppCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.AssemblyRecoveryTool
{
    public partial class MainControl : PluginControlBase
    {
        public AssemblyManager Manager
        {
            get;
            private set;
        }

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class <see cref="MainControl"/>
        /// </summary>
        public MainControl()
        {
            InitializeComponent();

            // Execute code automatically when plugin is loaded
            this.Enter += MainControl_Enter;
        }

        /// <summary>
        /// Will initiate loading of assemblies from currently connected server if current connection was updated in main application
        /// </summary>
        /// <param name="sender">Instance of class <see cref="MainControl"/></param>
        /// <param name="e">Event arguments</param>
        private void MainControl_ConnectionUpdated(object sender, PluginControlBase.ConnectionUpdatedEventArgs e)
        {
            ExecuteMethod(RetrieveAssemblies);
        }

        /// <summary>
        /// Will initiate loading of assemblies from currently connected server
        /// </summary>
        /// <param name="sender">Instance of class <see cref="MainControl"/></param>
        /// <param name="e">Event aguments</param>
        private void MainControl_Enter(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (sender is MainControl)
                {
                    var plugin = ((MainControl)sender);
                    // In case if connection updated on main application, update assemblies list inside the plugin
                    plugin.ConnectionUpdated += MainControl_ConnectionUpdated;

                    if (plugin.Service != null)
                    {
                        // Execute assemblies retrieve only if Service object is set for correct sender.
                        // This will help plugin act predicatable when it was loaded in offline mode;
                        // Plugin will not insist on connecting to server. Will scinetly obey instead.
                        ExecuteMethod(RetrieveAssemblies);
                    }
                }
            }
        }

        #endregion Constructor

        #region Methods

        private void TsbCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        #endregion Methods

        public void RetrieveAssemblies()
        {
            // Initalizing plugin wide AssemblyManager instanace
            Manager = new AssemblyManager(Service);

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading assemblies...",
                Work = (bw, e) =>
                {
                    e.Result = Manager.RetrieveAssemblies();
                },
                PostWorkCallBack = e =>
                {
                    listView_Assemblies.Items.Clear();

                    var list = (List<Entity>)e.Result;

                    foreach (Entity pAssembly in list)
                    {
                        var item = new ListViewItem(pAssembly["name"].ToString());
                        item.SubItems.Add(pAssembly["version"].ToString());
                        item.SubItems.Add(pAssembly["publickeytoken"].ToString());

                        item.Tag = pAssembly.Id;

                        listView_Assemblies.Items.Add(item);
                    }
                }
            });
        }

        private void tsbExportToDisk_Click(object sender, EventArgs e)
        {
            if (listView_Assemblies.CheckedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select at least one assembly in the list!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fbDialog = new FolderBrowserDialog
            {
                Description = "Select a location to recover the assemblies",
                ShowNewFolderButton = true
            };

            if (fbDialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(fbDialog.SelectedPath))
                {
                    foreach (ListViewItem item in listView_Assemblies.CheckedItems)
                    {
                        string filename = string.Format("{0}{1}.dll",
                            (fbDialog.SelectedPath.EndsWith("\\")
                            ? fbDialog.SelectedPath
                            : fbDialog.SelectedPath + "\\"),
                        item.Text);

                        byte[] buffer = Manager.RetrieveAssembly((Guid)item.Tag);

                        using (var writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                        {
                            writer.Write(buffer);
                        }
                    }

                    MessageBox.Show(this, "Assemblies recovered!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void tsbLoadAssemblies_Click(object sender, EventArgs e)
        {
            ExecuteMethod(RetrieveAssemblies);
        }
    }
}