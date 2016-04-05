// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using MsCrmTools.SiteMapEditor.AppCode;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MsCrmTools.SiteMapEditor.Controls
{
    public partial class DescriptionControl : UserControl, ISiteMapSavable
    {
        private readonly Dictionary<string, string> collec;

        private string initialDescription = "";
        private string initialLCID = "";
        private ToolTip tip;

        #region Delegates

        public delegate void SaveEventHandler(object sender, SaveEventArgs e);

        #endregion Delegates

        #region Event Handlers

        public event SaveEventHandler Saved;

        #endregion Event Handlers

        public DescriptionControl()
        {
            InitializeComponent();

            collec = new Dictionary<string, string>();

            tip = new ToolTip();
            tip.ToolTipTitle = "Information";
            tip.SetToolTip(txtDescriptionLCID, "A four or five digit locale ID for the Description.");
            tip.SetToolTip(txtDescriptionDescription, "Text to be displayed.");
        }

        public DescriptionControl(Dictionary<string, string> collection)
            : this()
        {
            if (collection != null)
                collec = collection;

            FillControls();
        }

        public void Save()
        {
            int result;
            if (txtDescriptionLCID.Text.Length != 4 && txtDescriptionLCID.Text.Length != 5 && !int.TryParse(txtDescriptionLCID.Text, out result))
            {
                MessageBox.Show(this, "LCID accepts only 4 or 5 digits figure", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtDescriptionDescription.Text.Length == 0)
            {
                MessageBox.Show(this, "Description can't be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Dictionary<string, string> collection = new Dictionary<string, string>();
                if (txtDescriptionLCID.Text.Length > 0)
                    collection.Add("LCID", txtDescriptionLCID.Text);
                if (txtDescriptionDescription.Text.Length > 0)
                    collection.Add("Description", txtDescriptionDescription.Text);

                if (collec.ContainsKey("_disabled"))
                {
                    collection.Add("_disabled", collec["_disabled"]);
                }

                initialLCID = txtDescriptionLCID.Text;
                initialDescription = txtDescriptionDescription.Text;

                SendSaveMessage(collection);
            }
        }

        private void FillControls()
        {
            txtDescriptionLCID.Text = collec.ContainsKey("LCID") ? collec["LCID"] : "";
            txtDescriptionDescription.Text = collec.ContainsKey("Description") ? collec["Description"] : "";

            initialLCID = txtDescriptionLCID.Text;
            initialDescription = txtDescriptionDescription.Text;
        }

        private void SiteMapControl_Leave(object sender, EventArgs e)
        {
            if (initialLCID != txtDescriptionLCID.Text ||
                initialDescription != txtDescriptionDescription.Text)
            {
                if (MessageBox.Show("You didn't save your changes! Do you want to save them now?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Save();
                }
            }
        }

        #region Send Events

        /// <summary>
        /// Sends a connection success message
        /// </summary>
        /// <param name="service">IOrganizationService generated</param>
        /// <param name="parameters">Lsit of parameter</param>
        private void SendSaveMessage(Dictionary<string, string> collection)
        {
            SaveEventArgs sea = new SaveEventArgs { AttributeCollection = collection };

            if (Saved != null)
            {
                Saved(this, sea);
            }
        }

        #endregion Send Events
    }
}