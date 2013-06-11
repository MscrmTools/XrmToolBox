// PROJECT : MsCrmTools.MetadataDocumentGenerator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.MetadataDocumentGenerator.Generation;
using MsCrmTools.MetadataDocumentGenerator.Helper;
using XrmToolBox;

namespace MsCrmTools.MetadataDocumentGenerator
{
    public partial class MainControl : UserControl, IMsCrmToolsPluginUserControl
    {
        #region Variables

        /// <summary>
        /// Microsoft Dynamics CRM 2011 Organization Service
        /// </summary>
        private IOrganizationService service;

        /// <summary>
        /// Panel used to display progress information
        /// </summary>
        private Panel infoPanel;

        private List<EntityMetadata> emdCache;

        private GenerationSettings settings;

        private bool loadSettingsFlag = false;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class <see cref="MainControl"/>
        /// </summary>
        public MainControl()
        {
            InitializeComponent();

            cbbOutputType.SelectedIndex = 0;
            cbbSelectionType.SelectedIndex = 0;

            settings = new GenerationSettings();
            settings.AttributesSelection = AttributeSelectionOption.AllAttributes;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the organization service used by the tool
        /// </summary>
        public IOrganizationService Service
        {
            get { return service; }
        }

        /// <summary>
        /// Gets the logo to display in the tools list
        /// </summary>
        public Image PluginLogo
        {
            get { return toolImageList.Images[0]; }
        }

        #endregion

        #region EventHandlers

        /// <summary>
        /// EventHandler to request a connection to an organization
        /// </summary>
        public event EventHandler OnRequestConnection;

        /// <summary>
        /// EventHandler to close the current tool
        /// </summary>
        public event EventHandler OnCloseTool;

        #endregion EventHandlers

        #region Methods

        /// <summary>
        /// Updates the organization service used by the tool
        /// </summary>
        /// <param name="newService">Organization service</param>
        /// <param name="actionName">Action that requested a service update</param>
        /// <param name="parameter">Parameter passed when requesting a service update</param>
        public void UpdateConnection(IOrganizationService newService, string actionName = "", object parameter = null)
        {
            service = newService;

            if (actionName == "Connect")
            {
                LoadEntitiesAndLanguages();
            }
        }

        private void TsbConnectClick(object sender, EventArgs e)
        {
            SetWorkingState(true);

            if (service == null)
            {
                if (OnRequestConnection != null)
                {
                    var args = new RequestConnectionEventArgs { ActionName = "Connect", Control = this, Parameter = null };
                    OnRequestConnection(this, args);
                }
            }
            else
            {
                LoadEntitiesAndLanguages();
            }
        }

        private void LoadEntitiesAndLanguages()
        {
            infoPanel = InformationPanel.GetInformationPanel(this, "Retrieving entities...", 340, 100);

            var worker = new BackgroundWorker();
            worker.DoWork += WorkerDoWork;
            worker.ProgressChanged += WorkerProgressChanged;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(infoPanel, e.UserState.ToString());
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            // TODO Clear settings cache
            emdCache = new List<EntityMetadata>();

            var request = new RetrieveAllEntitiesRequest();
            var response = (RetrieveAllEntitiesResponse)service.Execute(request);

            foreach (var emd in response.EntityMetadata)
            {
                emdCache.Add(emd);
            }
            
            ((BackgroundWorker)sender).ReportProgress(0, "Retrieving available languages");

            var lcidRequest = new RetrieveProvisionedLanguagesRequest();
            var lcidResponse = (RetrieveProvisionedLanguagesResponse)service.Execute(lcidRequest);

            e.Result =
                lcidResponse.RetrieveProvisionedLanguages.Select(
                    lcid => new LanguageCode {Lcid = lcid, Label = CultureInfo.GetCultureInfo(lcid).EnglishName});

        }

        private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var emd in emdCache)
            {
                var displayName = emd.DisplayName != null && emd.DisplayName.UserLocalizedLabel != null
                                      ? emd.DisplayName.UserLocalizedLabel.Label
                                      : "N/A";
                var name = emd.LogicalName;


                lvEntities.Items.Add(new ListViewItem
                                         {
                                             Text = displayName,
                                             SubItems =
                                                 {
                                                     name
                                                 },
                                             Tag = name
                                         });
            }

            foreach (var lc in (IEnumerable<LanguageCode>) e.Result)
            {
                cbbLcid.Items.Add(lc);
            }

            if (cbbLcid.Items.Count > 0)
            {
                cbbLcid.SelectedIndex = 0;
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
            SetWorkingState(false);
        }

        private void TsbCloseClick(object sender, EventArgs e)
        {
            if (OnCloseTool != null)
            {
                OnCloseTool(this, null);
            }
        }

        private void SetWorkingState(bool isWorking)
        {
            gbOutput.Enabled = !isWorking;
            gbAttributeSelection.Enabled = !isWorking;
            gbOptions.Enabled = !isWorking;

            tsbConnect.Enabled = !isWorking;
            tsbGenerate.Enabled = !isWorking;

            settingsToolStripDropDownButton.Enabled = !isWorking;
        }

        #endregion Methods

        private void CbbSelectionTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (settings == null || cbbSelectionType.SelectedIndex == (int)settings.AttributesSelection)
                return;

            if (settings.EntitiesToProceed.Count != 0)
            {
                if (MessageBox.Show(this,
                                    "If you change selection type, all previous selected attributes or forms will be cleared from current selection. \r\n\r\nDo you want to continue?",
                                    "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    cbbSelectionType.SelectedIndex = (int) settings.AttributesSelection;
                    return;
                }
            }

            foreach (var item in settings.EntitiesToProceed)
            {
                item.Attributes.Clear();
                item.Forms.Clear();
            }

            DisplaySubSelectionComponents();
        }

        private void DisplaySubSelectionComponents()
        {
           
            lvAttributes.Items.Clear();
            lvForms.Items.Clear();

            settings.AttributesSelection = (AttributeSelectionOption) cbbSelectionType.SelectedIndex;

            switch (cbbSelectionType.SelectedIndex)
            {
                case (int) AttributeSelectionOption.AllAttributes:
                case (int) AttributeSelectionOption.AttributesOptionSet:
                    {
                        lvAttributes.Visible = false;
                        lvForms.Visible = false;
                        lblSubSelect.Visible = false;
                    }
                    break;
                case (int) AttributeSelectionOption.AttributesOnForm:
                    {
                        lvAttributes.Visible = false;
                        lvForms.Visible = true;
                        lblSubSelect.Visible = true;
                        lblSubSelect.Text = "Forms";
                    }
                    break;
                case (int) AttributeSelectionOption.AttributeManualySelected:
                    {
                        lvAttributes.Visible = true;
                        lvForms.Visible = false;
                        lblSubSelect.Visible = true;
                        lblSubSelect.Text = "Attributes";
                        LvEntitiesSelectedIndexChanged(null, null);
                    }
                    break;
            }
        }

        private void BtnBrowseFilePathClick(object sender, EventArgs e)
        {
            var sfDialog = new SaveFileDialog
                               {
                                   Filter =
                                       cbbOutputType.SelectedIndex == 0
                                           ? "Excel workbook|*.xlsx"
                                           : "Word Document|*.docx",
                                   Title = "Select a location for the file generated"
                               };

            if (sfDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtOutputFilePath.Text = sfDialog.FileName;
            }
        }

        private void LvEntitiesItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                if (settings.EntitiesToProceed.Any(x => x.Name == e.Item.Tag.ToString()))
                    return;

                settings.EntitiesToProceed.Add(new EntityItem
                                                   {
                                                       Attributes = new List<string>(), 
                                                       Name = e.Item.Tag.ToString(),
                                                       Forms = new List<Guid>()
                                                   });
            }
            else
            {
                var item = settings.EntitiesToProceed.FirstOrDefault(x => x.Name == e.Item.Tag.ToString());
                if (item != null)
                {
                    settings.EntitiesToProceed.Remove(item);
                }
            }
        }

        private void LvEntitiesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count == 0)
            {
                return;
            }

            var currentEntity = settings.EntitiesToProceed.FirstOrDefault(x => x.Name == lvEntities.SelectedItems[0].Tag.ToString());
            if (currentEntity == null)
            {
                lvEntities.SelectedItems[0].Checked = true;
            }

            if (cbbSelectionType.SelectedIndex == (int)AttributeSelectionOption.AttributeManualySelected)
            {
                lvAttributes.Items.Clear();

                var entityName = lvEntities.SelectedItems[0].Tag.ToString();

                infoPanel = InformationPanel.GetInformationPanel(this, "Retrieving attributes...", 340, 100);
                SetWorkingState(true);

                var worker = new BackgroundWorker();
                worker.DoWork += RetrieveAttributeWorkerDoWork;
                worker.RunWorkerCompleted += RetrieveAttributeWorkerRunWorkerCompleted;
                worker.RunWorkerAsync(entityName);
            }
            else if (cbbSelectionType.SelectedIndex == (int) AttributeSelectionOption.AttributesOnForm
                || cbbSelectionType.SelectedIndex == (int)AttributeSelectionOption.AttributesNotOnForm)
            {
                lvForms.Items.Clear();
                
                var entityName = lvEntities.SelectedItems[0].Tag.ToString();

                infoPanel = InformationPanel.GetInformationPanel(this, "Retrieving forms...", 340, 100);
                SetWorkingState(true);

                var worker = new BackgroundWorker();
                worker.DoWork += RetrieveFormsWorkerDoWork;
                worker.RunWorkerCompleted += RetrieveFormsWorkerRunWorkerCompleted;
                worker.RunWorkerAsync(entityName);
            }
        }

        private void RetrieveFormsWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var qba = new QueryByAttribute("systemform");
            qba.Attributes.AddRange("objecttypecode", "type");
            qba.Values.AddRange(e.Argument.ToString(), 2);
            qba.ColumnSet = new ColumnSet(true);

            e.Result = service.RetrieveMultiple(qba).Entities;
        }

        private void RetrieveFormsWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var currentEntity = settings.EntitiesToProceed.FirstOrDefault(x => x.Name == lvEntities.SelectedItems[0].Tag.ToString());

            foreach (var form in (DataCollection<Entity>)e.Result)
            {
                var item = new ListViewItem(form.GetAttributeValue<string>("name")) {Tag = form};

                if (currentEntity != null && currentEntity.Forms.Contains(form.Id))
                {
                    item.Checked = true;
                }

                lvForms.Items.Add(item);
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
            SetWorkingState(false);
        }

        private void LvFormsItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvEntities.SelectedItems.Count == 0 || lvForms.CheckedItems.Count == 0)
                return;

            var currentEntityName = lvEntities.SelectedItems[0].Tag.ToString();

            var currentEntity = settings.EntitiesToProceed.FirstOrDefault(x => x.Name == currentEntityName);
            if (currentEntity == null)
            {
                lvEntities.SelectedItems[0].Checked = true;
                currentEntity = settings.EntitiesToProceed.FirstOrDefault(x => x.Name == currentEntityName);
            }

            var form = (Entity) lvForms.CheckedItems[0].Tag;

            if (e.Item.Checked)
            {
                if (!currentEntity.Forms.Contains(form.Id))
                    currentEntity.Forms.Add(form.Id);
            }
            else
            {
                if (currentEntity.Forms.Contains(form.Id) && loadSettingsFlag == false)
                {
                    currentEntity.Forms.Remove(form.Id);
                }
            }
        }

        private void RetrieveAttributeWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var entityName = e.Argument.ToString();

            var request = new RetrieveEntityRequest {EntityFilters = EntityFilters.Attributes, LogicalName = entityName};
            var response = (RetrieveEntityResponse)service.Execute(request);

            e.Result = response.EntityMetadata;
        }

        private void RetrieveAttributeWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var currentEntity = settings.EntitiesToProceed.FirstOrDefault(x => x.Name == lvEntities.SelectedItems[0].Tag.ToString());

            foreach (var amd in ((EntityMetadata) e.Result).Attributes)
            {
                var displayName = amd.DisplayName != null && amd.DisplayName.UserLocalizedLabel != null
                                      ? amd.DisplayName.UserLocalizedLabel.Label
                                      : "N/A";

                var item = new ListViewItem(displayName);
                item.SubItems.Add(amd.LogicalName);
                item.Tag = amd.LogicalName;

                if (currentEntity != null && currentEntity.Attributes.Contains(amd.LogicalName))
                {
                    item.Checked = true;
                }

                lvAttributes.Items.Add(item);
            }

            infoPanel.Dispose();
            Controls.Remove(infoPanel);
            SetWorkingState(false);
        }

        private void LvAttributesItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvEntities.SelectedItems.Count == 0)
                return;

            var currentEntityName = lvEntities.SelectedItems[0].Tag.ToString();

            var currentEntity = settings.EntitiesToProceed.FirstOrDefault(x => x.Name == currentEntityName);
            if (currentEntity == null)
            {
                lvEntities.SelectedItems[0].Checked = true;
                currentEntity = settings.EntitiesToProceed.FirstOrDefault(x => x.Name == currentEntityName);
            }

            if (e.Item.Checked)
            {
                currentEntity.Attributes.Add(lvAttributes.CheckedItems[0].Tag.ToString());
            }
            else
            {
                var item = currentEntity.Attributes.FirstOrDefault(x => x == e.Item.Tag.ToString());
                if (item != null)
                {
                    currentEntity.Attributes.Remove(item);
                }
            }
        }

        private void TsbGenerateClick(object sender, EventArgs e)
        {
            if (settings.EntitiesToProceed.Count == 0)
            {
                MessageBox.Show(this, "No entity has been selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtOutputFilePath.Text.Length == 0)
            {
                MessageBox.Show(this, "Please select a destination file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            settings.AddAuditInformation = chkAddAudit.Checked;
            settings.AddEntitiesSummary = chkDisplayEntityList.Checked;
            settings.AddFieldSecureInformation = chkAddFls.Checked;
            settings.AddRequiredLevelInformation = chkAddRequiredLevel.Checked;
            settings.AddValidForAdvancedFind = chkAddValidForAf.Checked;
            settings.AddFormLocation = chkAddFormLocation.Checked;

            settings.DisplayNamesLangugageCode = ((LanguageCode) cbbLcid.SelectedItem).Lcid;
            settings.FilePath = txtOutputFilePath.Text;
            settings.OutputDocumentType = cbbOutputType.SelectedIndex == 0 ? Output.Excel : Output.Word;
            settings.AttributesSelection = (AttributeSelectionOption)cbbSelectionType.SelectedIndex;
            settings.IncludeOnlyAttributesOnForms = cbbSelectionType.SelectedIndex == (int)AttributeSelectionOption.AttributesOnForm;

            infoPanel = InformationPanel.GetInformationPanel(this, "Generating document...", 340, 140);
            SetWorkingState(true);

            var bwGenerate = new BackgroundWorker {WorkerReportsProgress = true};
            bwGenerate.DoWork += BwGenerateDoWork;
            bwGenerate.ProgressChanged += BwGenerateProgressChanged;
            bwGenerate.RunWorkerCompleted += BwGenerateRunWorkerCompleted;
            bwGenerate.RunWorkerAsync();

        }

        void BwGenerateDoWork(object sender, DoWorkEventArgs e)
        {
            IDocument docGenerator;

            if (cbbOutputType.SelectedItem.ToString() == "Excel Workbook")
            {
                docGenerator = new ExcelDocument();
            }
            else
            {
                docGenerator = new WordDocument();
            }

            docGenerator.Worker = (BackgroundWorker) sender;
            docGenerator.Settings = settings;
            docGenerator.Generate(service);
        }

        void BwGenerateProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            InformationPanel.ChangeInformationPanelMessage(infoPanel, string.Format("{0}%\r\n{1}", e.ProgressPercentage, e.UserState));
        }

        void BwGenerateRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            infoPanel.Dispose();
            Controls.Remove(infoPanel);
            SetWorkingState(false);

            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured while generating document: " + e.Error.ToString(), "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (DialogResult.Yes == MessageBox.Show(this, "Do you want to open generated document?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    Process.Start(settings.FilePath);
                }
            }
        }

        private void CbbOutputTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            txtOutputFilePath.Text = string.Empty;
        }

        private void ListViewsColumnClick(object sender, ColumnClickEventArgs e)
        {
            var lv = (ListView) sender;

            lv.Sorting = lv.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lv.ListViewItemSorter = new ListViewItemComparer(e.Column, lv.Sorting);
            lv.Sort();
        }

        #region Settings

        private void SaveCurrentSettingsToolStripMenuItemClick(object sender, EventArgs e)
        {
            settings.OutputDocumentType = (Output) cbbOutputType.SelectedIndex;
            settings.AttributesSelection = (AttributeSelectionOption) cbbSelectionType.SelectedIndex;
            settings.FilePath = txtOutputFilePath.Text;
            settings.AddAuditInformation = chkAddAudit.Checked;
            settings.AddFieldSecureInformation = chkAddFls.Checked;
            settings.AddFormLocation = chkAddFormLocation.Checked;
            settings.AddRequiredLevelInformation = chkAddRequiredLevel.Checked;
            settings.AddValidForAdvancedFind = chkAddValidForAf.Checked;
            settings.AddEntitiesSummary = chkDisplayEntityList.Checked;

            settings.SaveToFile();
        }

        private void LoadSettingsToolStripMenuItemClick(object sender, EventArgs e)
        {
            loadSettingsFlag = true;
            settings = GenerationSettings.CreateFromFile();

            cbbOutputType.SelectedIndex = settings.OutputDocumentType == Output.Excel ? 0 : 1;
            cbbSelectionType.SelectedIndex = (int)settings.AttributesSelection;

            DisplaySubSelectionComponents();

            txtOutputFilePath.Text = settings.FilePath;
            chkAddAudit.Checked = settings.AddAuditInformation;
            chkAddFls.Checked = settings.AddFieldSecureInformation;
            chkAddFormLocation.Checked = settings.AddFormLocation;
            chkAddRequiredLevel.Checked = settings.AddRequiredLevelInformation;
            chkAddValidForAf.Checked = settings.AddValidForAdvancedFind;
            chkDisplayEntityList.Checked = settings.AddEntitiesSummary;

            foreach (ListViewItem item in lvEntities.Items)
            {
                var eItem = settings.EntitiesToProceed.FirstOrDefault(x => x.Name == item.Tag.ToString());
                item.Checked = eItem != null;
            }

            loadSettingsFlag = false;
        }

        #endregion Settings
    }
}
