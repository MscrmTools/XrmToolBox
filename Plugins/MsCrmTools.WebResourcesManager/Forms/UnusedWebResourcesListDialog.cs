// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using MsCrmTools.WebResourcesManager.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.Forms
{
    public partial class UnusedWebResourcesListDialog : Form
    {
        private readonly IOrganizationService service;

        public UnusedWebResourcesListDialog(IEnumerable<Entity> unusedWebResources, IOrganizationService service)
        {
            InitializeComponent();

            this.service = service;

            foreach (var wr in unusedWebResources)
            {
                var item = new ListViewItem(wr["name"].ToString()) { Tag = wr };
                lvWebResources.Items.Add(item);
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnDeleteClick(object sender, EventArgs e)
        {
            if (lvWebResources.SelectedItems.Count == 0)
                return;

            if (DialogResult.No ==
                MessageBox.Show(this,
                                "Are your sure you want to delete selected web resources?\r\n\r\nEven web resources without any dependencies could be used by other web resources",
                                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                return;

            var list = (from ListViewItem item in lvWebResources.SelectedItems select (Entity)item.Tag).ToList();
            pbDelete.Visible = true;

            var bwDelete = new BackgroundWorker();
            bwDelete.DoWork += BwDeleteDoWork;
            bwDelete.ProgressChanged += BwDeleteProgressChanged;
            bwDelete.RunWorkerCompleted += BwDeleteRunWorkerCompleted;
            bwDelete.WorkerReportsProgress = true;
            bwDelete.RunWorkerAsync(list);
        }

        private void BwDeleteDoWork(object sender, DoWorkEventArgs e)
        {
            var bw = (BackgroundWorker)sender;
            var wrs = (List<Entity>)e.Argument;

            int i = 1;
            foreach (var wr in wrs)
            {
                bw.ReportProgress((i * 100) / wrs.Count, "Deleting web resource " + wr["name"] + "...");

                try
                {
                    service.Delete(wr.LogicalName, wr.Id);
                }
                catch
                { }

                i++;
            }
        }

        private void BwDeleteProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbDelete.Value = e.ProgressPercentage;
        }

        private void BwDeleteRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbDelete.Value = 0;
            pbDelete.Visible = false;
        }

        private void lvWebResources_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lvWebResources.Sorting == SortOrder.Ascending)
            {
                lvWebResources.Sorting = SortOrder.Descending;
            }
            else
            {
                lvWebResources.Sorting = SortOrder.Ascending;
            }

            lvWebResources.ListViewItemSorter = new ListViewItemComparer(e.Column, lvWebResources.Sorting);
        }
    }
}