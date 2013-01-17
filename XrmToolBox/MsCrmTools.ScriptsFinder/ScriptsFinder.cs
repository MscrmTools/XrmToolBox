// PROJECT : MsCrmTools.ScriptsFinder
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using XrmToolBox;

namespace MsCrmTools.ScriptsFinder
{
    public partial class ScriptsFinder : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        private IOrganizationService service;

        private Panel infoPanel;

        #endregion Variables

        #region Constructor

        public ScriptsFinder()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Properties

        public IOrganizationService Service
        {
            get { return service; }
        }

        public Image PluginLogo
        {
            get { return imageList1.Images[0]; }
        }

        #endregion Properties

        private void TsbMainFindScriptsClick(object sender, EventArgs e)
        {
            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var arg = new RequestConnectionEventArgs { Control = this, ActionName = "FindScripts" };
                    OnRequestConnection(this, arg);
                }
            }
            else
            {
                FindScripts();
            }
        }

        private void FindScripts()
        {
            lvScripts.Items.Clear();
            tsbMainFindScripts.Enabled = false;
            tsbExportToCsv.Enabled = false;

            infoPanel = InformationPanel.GetInformationPanel(this, "Loading scripts (this can take a while...)", 340,
                                                             100);

            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var lScripts = new List<ListViewItem>();

            var sManager = new ScriptsManager(service);
            sManager.Find();

            foreach (var script in sManager.Scripts)
            {
                var item = new ListViewItem(script.EntityName);
                item.SubItems.Add(script.EntityLogicalName);
                item.SubItems.Add(script.Form);
                item.SubItems.Add(script.Event);
                item.SubItems.Add(script.Attribute);
                item.SubItems.Add(script.AttributeLogicalName);
                item.SubItems.Add(script.ScriptLocation);
                item.SubItems.Add(script.MethodCalled);

                lScripts.Add(item);
            }

            e.Result = lScripts;
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var item in (List<ListViewItem>) e.Result)
            {
                lvScripts.Items.Add(item);
            }

            tsbMainFindScripts.Enabled = true;
            tsbExportToCsv.Enabled = true;
            infoPanel.Dispose();
            Controls.Remove(infoPanel);
        }

        private void LvScriptsColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvScripts.Sorting = lvScripts.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvScripts.ListViewItemSorter = new ListViewItemComparer(e.Column, lvScripts.Sorting);
        }

        private void TsbCloseThisTabClick(object sender, EventArgs e)
        {
            const string message = "Are your sure you want to close this tab?";
            if (MessageBox.Show(message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                OnCloseTool(this, null);
        }


        public event EventHandler OnRequestConnection;
        public event EventHandler OnCloseTool;

        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;

            if(actionName == "FindScripts")
            {
                FindScripts(); 
            }
        }

        private void TsbExportToCsvClick(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
                          {
                              Filter = "CSV file (*.csv)|*.csv",
                              Title = "Select a file where to save the list of scripts"
                          };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var writer = new StreamWriter(sfd.FileName, false))
                {
                    writer.WriteLine("Entity Display Name,Entity Logical Name,Form,Event,Attribute Display Name,Attribute Logical Name,Script Location,Method Called");

                    foreach (ListViewItem item in lvScripts.Items)
                    {
                        writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}",
                            item.SubItems[0].Text,
                            item.SubItems[1].Text,
                            item.SubItems[2].Text,
                            item.SubItems[3].Text,
                            item.SubItems[4].Text,
                            item.SubItems[5].Text,
                            item.SubItems[6].Text,
                            item.SubItems[7].Text);
                    }

                    MessageBox.Show(this, string.Format("File saved to '{0}'!", sfd.FileName), "Information",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
