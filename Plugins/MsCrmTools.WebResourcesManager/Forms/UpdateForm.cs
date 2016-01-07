// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Xrm.Sdk;
using MsCrmTools.WebResourcesManager.AppCode;
using System;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.Forms
{
    internal partial class UpdateForm : Form
    {
        #region Variables

        /// <summary>
        /// Current script
        /// </summary>
        private readonly WebResource currentWebResource;

        /// <summary>
        /// Current Prefix for names depending on selected solution
        /// </summary>
        private string currentPrefix = string.Empty;

        /// <summary>
        /// Xrm Organization Service
        /// </summary>
        private IOrganizationService innerService;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class UpdateForm
        /// </summary>
        /// <param name="script">Script to display or to create</param>
        /// <param name="service">Xrm Organization Service</param>
        public UpdateForm(WebResource script, IOrganizationService service)
        {
            InitializeComponent();

            innerService = service;
            currentWebResource = script;

            FillControls();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Script to update
        /// </summary>
        internal WebResource WebRessource
        {
            get
            {
                return currentWebResource;
            }
        }

        #endregion Properties

        #region Methods

        private void FillControls()
        {
            if (!string.IsNullOrEmpty(currentWebResource.FilePath))
                txtPath.Text = currentWebResource.FilePath;

            if (currentWebResource.Entity.Contains("name"))
                txtName.Text = currentWebResource.Entity["name"].ToString();

            if (currentWebResource.Entity.Contains("displayname"))
                txtDisplayName.Text = currentWebResource.Entity["displayname"].ToString();

            if (currentWebResource.Entity.Contains("description"))
                txtDescription.Text = currentWebResource.Entity["description"].ToString();
        }

        #endregion Methods

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            currentWebResource.Entity["displayname"] = txtDisplayName.Text;
            currentWebResource.Entity["description"] = txtDescription.Text;

            DialogResult = DialogResult.OK;
        }
    }
}