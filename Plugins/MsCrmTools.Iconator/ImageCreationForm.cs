// PROJECT : MsCrmTools.Iconator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.Iconator.AppCode;
using MsCrmTools.Iconator.Delegates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace MsCrmTools.Iconator
{
    public partial class ImageCreationForm : Form
    {
        public List<Entity> WebResourcesCreated;
        private readonly List<FileInfo> _selectedFiles;
        private readonly IOrganizationService _service;
        private Panel infoPanel;

        public ImageCreationForm(IOrganizationService service, string path = null)
        {
            InitializeComponent();

            _service = service;
            _selectedFiles = new List<FileInfo>();
            WebResourcesCreated = new List<Entity>();

            if (!string.IsNullOrEmpty(path))
            {
                var fi = new FileInfo(path);
                _selectedFiles.Add(fi);

                var item = new ListViewItem(fi.Name);
                item.SubItems.Add(fi.FullName);
                item.Tag = fi;

                lvFiles.Items.Add(item);
            }
        }

        private void BtnAddFilesClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
                {
                    Filter = "Image files|*.jpg;*.png;*.gif;*.jpeg",
                    Multiselect = true,
                    Title = "Select image files to add as web resource"
                };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in dialog.FileNames)
                {
                    var fi = new FileInfo(filePath);
                    _selectedFiles.Add(fi);

                    var item = new ListViewItem(fi.Name);
                    item.SubItems.Add(fi.FullName);
                    item.Tag = fi;

                    lvFiles.Items.Add(item);
                }
            }
        }

        private void BtnCreateWebResourcesClick(object sender, EventArgs e)
        {
            if (lvFiles.Items.Count > 0)
            {
                infoPanel = InformationPanel.GetInformationPanel(this, "Creating web resource(s)...", 340, 150);

                var bWorker = new BackgroundWorker();
                bWorker.RunWorkerCompleted += BWorkerRunWorkerCompleted;
                bWorker.DoWork += BWorkerDoWork;
                bWorker.RunWorkerAsync();
            }
        }

        private void BWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            foreach (ListViewItem item in ListViewDelegates.GetItems(lvFiles))
            {
                var fi = (FileInfo)item.Tag;
                var extension = fi.Extension.Remove(0, 1).ToLower();

                var webR = new Entity("webresource");
                webR["displayname"] = fi.Name;
                webR["webresourcetype"] = extension == "png"
                                              ? new OptionSetValue(5)
                                              : extension == "jpg" || extension == "jpeg"
                                                    ? new OptionSetValue(6)
                                                    : new OptionSetValue(7);

                webR["name"] = (txtPrefixToUse.Text + fi.Name).Replace(" ", "");

                var fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read);
                var binaryData = new byte[fs.Length];
                fs.Read(binaryData, 0, (int)fs.Length);
                fs.Close();
                webR["content"] = Convert.ToBase64String(binaryData, 0, binaryData.Length);

                bool processed = false;
                if (CheckBoxDelegates.IsChecked(chkOverwriteExistingImages))
                {
                    var existingWr = _service.RetrieveMultiple(new QueryByAttribute
                    {
                        EntityName = "webresource",
                        Attributes = { "name" },
                        Values = { webR["name"] }
                    }).Entities.ToList().FirstOrDefault();

                    if (existingWr != null)
                    {
                        webR.Id = existingWr.Id;
                        _service.Update(webR);
                        processed = true;
                    }
                }

                if (!processed)
                {
                    Guid wrId = _service.Create(webR);
                    webR.Id = wrId;
                }

                if (!CheckBoxDelegates.IsChecked(chkAddToDefaultSolution))
                {
                    var request = new AddSolutionComponentRequest
                                      {
                                          ComponentType = 61,
                                          SolutionUniqueName = cbbSolutions.SelectedItem.ToString(),
                                          ComponentId = webR.Id
                                      };

                    _service.Execute(request);
                }

                WebResourcesCreated.Add(webR);
            }
        }

        private void BWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Controls.Remove(infoPanel);
                infoPanel.Dispose();

                MessageBox.Show(this, "Error while creating or updating web resources: " + e.Error.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Close();
            }
        }

        private void CbbFilesSelectedIndexChanged(object sender, EventArgs e)
        {
            // Retrieve publisher prefix
            var publisher = _service.Retrieve("publisher",
                                              ((EntityReference)
                                               ((SolutionObject)cbbSolutions.SelectedItem).Solution["publisherid"]).Id,
                                              new ColumnSet("customizationprefix"));

            txtPrefixToUse.Text = publisher["customizationprefix"] + "_";
        }

        private void ChkAddToDefaultSolutionCheckedChanged(object sender, EventArgs e)
        {
            cbbSolutions.Enabled = !chkAddToDefaultSolution.Checked;

            if (!chkAddToDefaultSolution.Checked)
            {
                cbbSolutions.Items.Clear();

                // Traitement pour récupérer les solutions qui nous intéresse
                var qe = new QueryExpression("solution");
                qe.Distinct = true;
                qe.ColumnSet = new ColumnSet(true);
                qe.Criteria = new FilterExpression();
                qe.Criteria.AddCondition(new ConditionExpression("ismanaged", ConditionOperator.Equal, false));
                qe.Criteria.AddCondition(new ConditionExpression("isvisible", ConditionOperator.Equal, true));

                var solutions = _service.RetrieveMultiple(qe);

                foreach (var solution in solutions.Entities)
                {
                    cbbSolutions.Items.Add(new SolutionObject { Solution = solution });
                }
            }
        }

        private void ChkUseSolutionPrefixCheckedChanged(object sender, EventArgs e)
        {
            txtPrefixToUse.Enabled = !chkUseSolutionPrefix.Checked;

            if (chkUseSolutionPrefix.Checked && cbbSolutions.SelectedItem != null)
            {
                // Retrieve publisher prefix
                var publisher = _service.Retrieve("publisher",
                                                  ((EntityReference)
                                                   ((SolutionObject)cbbSolutions.SelectedItem).Solution["publisherid"]).Id,
                                                  new ColumnSet("customizationprefix"));

                txtPrefixToUse.Text = publisher["customizationprefix"] + "_";
            }
        }
    }
}