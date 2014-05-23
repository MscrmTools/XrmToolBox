using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.MetadataDocumentGenerator.Forms
{
    public partial class PublisherSelector : Form
    {
        private readonly IOrganizationService service;

        public List<string> SelectedPrefixes { get; private set; }

        public PublisherSelector(IOrganizationService service, string selectedPrefixes)
        {
            InitializeComponent();

            this.service = service;
            this.SelectedPrefixes = selectedPrefixes.Split(';').ToList();
        }

        private void PublisherSelector_Load(object sender, EventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = service.RetrieveMultiple(new QueryExpression("publisher") {ColumnSet = new ColumnSet(true)});
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured while retrieving publishers: " + e.Error.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var results = (EntityCollection) e.Result;
                foreach (var result in results.Entities)
                {
                    var prefix = result.GetAttributeValue<string>("customizationprefix");

                    var item = new ListViewItem(result.GetAttributeValue<string>("friendlyname"));
                    item.SubItems.Add(prefix);
                    item.Tag = prefix;
                    item.Checked = SelectedPrefixes.Contains(prefix);

                    listView1.Items.Add(item);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedPrefixes = listView1.CheckedItems.Cast<ListViewItem>().Select(i => i.Tag.ToString()).ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
