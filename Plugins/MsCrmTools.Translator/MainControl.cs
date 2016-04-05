// PROJECT : MsCrmTools.Translator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using CrmExceptionHelper = XrmToolBox.CrmExceptionHelper;

namespace MsCrmTools.Translator
{
    public partial class MainControl : PluginControlBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of class <see cref="MainControl"/>
        /// </summary>
        public MainControl()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Methods

        private void TsbCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        #endregion Methods

        private void BtnBrowseImportFileClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Title = "Select translation file",
                Filter = "Excel Workbook|*.xlsx"
            };

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        private void BtnCheckAllClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvEntities.Items)
                item.Checked = true;
        }

        private void BtnClearAllClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvEntities.Items)
                item.Checked = false;
        }

        private void BtnExportTranslationsClick(object sender, EventArgs e)
        {
            if (lvEntities.CheckedItems.Count > 0 || chkExportGlobalOptSet.Checked || chkExportSiteMap.Checked || chkExportDashboards.Checked)
            {
                var entities = (from ListViewItem item in lvEntities.CheckedItems select ((EntityMetadata)item.Tag).LogicalName).ToList();

                var sfd = new SaveFileDialog { Filter = "Excel workbook|*.xlsx", Title = "Select file destination" };
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    var settings = new ExportSettings
                    {
                        ExportAttributes = chkExportAttributes.Checked,
                        ExportBooleans = chkExportBooleans.Checked,
                        ExportEntities = chkExportEntity.Checked,
                        ExportForms = chkExportForms.Checked,
                        ExportFormFields = chkExportFormsFields.Checked,
                        ExportFormSections = chkExportFormsSections.Checked,
                        ExportFormTabs = chkExportFormsTabs.Checked,
                        ExportGlobalOptionSet = chkExportGlobalOptSet.Checked,
                        ExportOptionSet = chkExportPicklists.Checked,
                        ExportViews = chkExportViews.Checked,
                        ExportCharts = chkExportCharts.Checked,
                        ExportCustomizedRelationships = chkExportCustomizedRelationships.Checked,
                        ExportSiteMap = chkExportSiteMap.Checked,
                        ExportDashboards = chkExportDashboards.Checked,
                        FilePath = sfd.FileName,
                        Entities = entities
                    };

                    SetState(true);

                    WorkAsync(new WorkAsyncInfo
                    {
                        Message = "Exporting Translations...",
                        AsyncArgument = settings,
                        Work = (bw, evt) =>
                        {
                            var engine = new Engine();
                            engine.Export((ExportSettings)evt.Argument, Service, bw);
                        },
                        PostWorkCallBack = evt =>
                        {
                            SetState(false);

                            if (evt.Error != null)
                            {
                                string errorMessage = CrmExceptionHelper.GetErrorMessage(evt.Error, true);
                                MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        },
                        ProgressChanged = evt => { SetWorkingMessage(evt.UserState.ToString()); }
                    });
                }
            }
        }

        private void BtnImportTranslationsClick(object sender, EventArgs e)
        {
            if (txtFilePath.Text.Length == 0)
            {
                MessageBox.Show(this, "Please select a file to import", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (tabControl1.SelectedIndex != 1)
                    tabControl1.SelectedIndex = 1;

                return;
            }

            ExecuteMethod(ImportTranslations);
        }

        private void ImportTranslations()
        {
            SetState(false);

            WorkAsync(new WorkAsyncInfo
            {
                Message = "",
                AsyncArgument = txtFilePath.Text,
                Work = (bw, e) =>
                {
                    var engine = new Engine();
                    engine.Import(e.Argument.ToString(), Service, bw);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                        MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    SetState(false);
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private void LoadEntities()
        {
            lvEntities.Items.Clear();

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading entities...",
                Work = (bw, e) =>
                {
                    List<EntityMetadata> entities = MetadataHelper.RetrieveEntities(Service);
                    e.Result = entities;
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(e.Error, true);
                        MessageBox.Show(this, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        foreach (EntityMetadata emd in (List<EntityMetadata>)e.Result)
                        {
                            var item = new ListViewItem { Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd };
                            item.SubItems.Add(emd.LogicalName);
                            lvEntities.Items.Add(item);
                        }
                    }
                }
            });
        }

        private void LvEntitiesColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvEntities.Sorting = lvEntities.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvEntities.ListViewItemSorter = new ListViewItemComparer(e.Column, lvEntities.Sorting);
        }

        private void SetState(bool isRunning)
        {
            tabPage1.Enabled = !isRunning;
            tabPage2.Enabled = !isRunning;
            tsbLoadEntities.Enabled = !isRunning;
        }

        private void TsbLoadEntitiesClick(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEntities);
        }
    }
}