// PROJECT : MsCrmTools.AttributeBulkUpdater
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
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Tanguy.WinForm.Utilities.DelegatesHelpers;
using XrmToolBox;

namespace MsCrmTools.AttributeBulkUpdater.Forms
{
    /// <summary>
    /// Updates the changed attributes
    /// </summary>
    public partial class UpdateAttributesForm : Form
    {
        #region Variables
    
        /// <summary>
        /// Crm Organization service
        /// </summary>
        readonly IOrganizationService innerService;

        /// <summary>
        /// Background worker for attributes update
        /// </summary>
        BackgroundWorker bwUpdateAttributes;

        /// <summary>
        /// Item update result
        /// </summary>
        enum Result
        {
            Success,
            Warning,
            Error
        }

        /// <summary>
        /// Flag to close automatically this window
        /// </summary>
        bool closeWindow;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class UpdateAttributesForm
        /// </summary>
        /// <param name="service">Crm Organization service</param>
        /// <param name="us">Settings for update operation</param>
        public UpdateAttributesForm(IOrganizationService service, UpdateSettings us)
        {
            InitializeComponent();

            innerService = service;
            FillImagesInListView();

            UpdateAttributes(us);
        }

        #endregion

        #region Handlers

        void bwUpdateAttributes_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bwUpdateAttributes = (BackgroundWorker)sender;
            UpdateSettings us = (UpdateSettings)e.Argument;
            int attributesProcessed = 0;

            // We need to process only items that have their IsValidForAdvancedFind
            // property updated
            List<ListViewItem> itemsToManage = new List<ListViewItem>();

            foreach (ListViewItem item in us.Items)
            {
                AttributeMetadata amd = (AttributeMetadata)item.Tag;

                if (amd.IsValidForAdvancedFind.Value != item.Checked && us.UpdateValidForAdvancedFind
                    || amd.IsAuditEnabled.Value != item.Checked && us.UpdateAuditIsEnabled)
                {
                    itemsToManage.Add(item);
                }
            }

            if (itemsToManage.Count == 0)
            {
                CommonDelegates.DisplayMessageBox(this, "No attributes need to be updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                closeWindow = true;
            }
            else
            {
                // We process the items
                foreach (ListViewItem item in itemsToManage)
                {
                    AttributeMetadata amd = (AttributeMetadata)item.Tag;

                    try
                    {
                        attributesProcessed++;

                        if (amd.IsCustomizable.Value)
                        {
                            if (us.UpdateValidForAdvancedFind)
                            {
                                amd.IsValidForAdvancedFind.Value = item.Checked;
                            }

                            if (us.UpdateAuditIsEnabled)
                            {
                                amd.IsAuditEnabled.Value = item.Checked;
                            }

                            UpdateAttributeRequest request = new UpdateAttributeRequest
                                                                 {
                                Attribute = amd,
                                EntityName = amd.EntityLogicalName
                            };

                            innerService.Execute(request);

                            AddItemToInformationList(amd.DisplayName.UserLocalizedLabel.Label, null, Result.Success);
                        }
                        else
                        {
                            AddItemToInformationList(amd.DisplayName.UserLocalizedLabel.Label, "Attribute not customizable!", Result.Warning);
                        }
                    }
                    catch (Exception error)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                        AddItemToInformationList(amd.DisplayName.UserLocalizedLabel.Label, errorMessage, Result.Error);
                    }

                    bwUpdateAttributes.ReportProgress(Convert.ToInt32(attributesProcessed * 100 / itemsToManage.Count));
                }
            }
        }

        void bwUpdateAttributes_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbUpdate.Value = e.ProgressPercentage;
        }

        void bwUpdateAttributes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnClose.Enabled = true;

            if (closeWindow)
            {
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (bwUpdateAttributes != null)
            {
                bwUpdateAttributes.CancelAsync();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Launch process to update attributes
        /// </summary>
        /// <param name="us">Settings for update operation</param>
        public void UpdateAttributes(UpdateSettings us)
        {
            bwUpdateAttributes = new BackgroundWorker();
            bwUpdateAttributes.RunWorkerCompleted += bwUpdateAttributes_RunWorkerCompleted;
            bwUpdateAttributes.ProgressChanged += bwUpdateAttributes_ProgressChanged;
            bwUpdateAttributes.DoWork += bwUpdateAttributes_DoWork;
            bwUpdateAttributes.WorkerReportsProgress = true;
            bwUpdateAttributes.WorkerSupportsCancellation = true;
            bwUpdateAttributes.RunWorkerAsync(us);
        }

        /// <summary>
        /// Adds an information in the listbox
        /// </summary>
        /// <param name="attributeName">Attribute display name</param>
        /// <param name="message">Message to display</param>
        /// <param name="result">Result of the update for this attribute</param>
        private void AddItemToInformationList(string attributeName, string message, Result result)
        {
            ListViewItem item = new ListViewItem();
            item.Text = attributeName;
            item.SubItems.Add(message);

            switch (result)
            {
                case Result.Success:
                    {
                        item.ImageIndex = 0;
                    }
                    break;
                case Result.Warning:
                    {
                        item.ImageIndex = 1;
                    }
                    break;
                case Result.Error:
                    {
                        item.ImageIndex = 2;
                    }
                    break;
            }

            ListViewDelegates.AddItem(lvInformation, item);
        }

        /// <summary>
        /// Attaches result images to lisbox
        /// </summary>
        private void FillImagesInListView()
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            Bitmap successImage;
            Bitmap warningImage;
            Bitmap errorImage;

            using (Stream myStream = myAssembly.GetManifestResourceStream("AttributeBulkUpdater.Images.solidgreentick.gif"))
            {
                successImage = new Bitmap(myStream);
                if (successImage != null)
                {
                    ListViewDelegates.AddImageToImageList(lvInformation, successImage);
                }
            }

            using (Stream myStream = myAssembly.GetManifestResourceStream("AttributeBulkUpdater.Images.notif_icn_warn16.png"))
            {
                warningImage = new Bitmap(myStream);
                if (warningImage != null)
                {
                    ListViewDelegates.AddImageToImageList(lvInformation, warningImage);
                }
            }
            using (Stream myStream = myAssembly.GetManifestResourceStream("AttributeBulkUpdater.Images.notif_icn_crit16.png"))
            {
                errorImage = new Bitmap(myStream);
                if (errorImage != null)
                {
                    ListViewDelegates.AddImageToImageList(lvInformation, errorImage);
                }
            }
        }

        #endregion
    }
}