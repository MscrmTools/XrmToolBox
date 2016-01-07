// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using MsCrmTools.SiteMapEditor.AppCode;
using MsCrmTools.SiteMapEditor.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SiteMapEditor.Controls
{
    public partial class TitleControl : UserControl, ISiteMapSavable
    {
        private readonly Dictionary<string, string> collec;

        private string initialLCID = "";
        private string initialTitle = "";
        private ToolTip tip;

        #region Delegates

        public delegate void SaveEventHandler(object sender, SaveEventArgs e);

        #endregion Delegates

        #region Event Handlers

        public event SaveEventHandler Saved;

        #endregion Event Handlers

        public TitleControl()
        {
            InitializeComponent();

            collec = new Dictionary<string, string>();

            tip = new ToolTip();
            tip.ToolTipTitle = "Information";
            tip.SetToolTip(txtTitleLCID, "A four or five digit Locale ID for the title.");
            tip.SetToolTip(txtTitleTitle, "Text to be displayed.");
        }

        public TitleControl(Dictionary<string, string> collection)
            : this()
        {
            if (collection != null)
                collec = collection;

            FillControls();
        }

        public void Save()
        {
            int result;
            if (txtTitleLCID.Text.Length != 4 && txtTitleLCID.Text.Length != 5 && !int.TryParse(txtTitleLCID.Text, out result))
            {
                MessageBox.Show(this, "LCID accepts only 4 or 5 digits figure", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtTitleLCID.Text.Length == 0)
            {
                MessageBox.Show(this, "Title can't be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Dictionary<string, string> collection = new Dictionary<string, string>();
                if (txtTitleLCID.Text.Length > 0)
                    collection.Add("LCID", txtTitleLCID.Text);
                if (txtTitleTitle.Text.Length > 0)
                    collection.Add("Title", txtTitleTitle.Text);

                if (collec.ContainsKey("_disabled"))
                {
                    collection.Add("_disabled", collec["_disabled"]);
                }

                initialLCID = txtTitleLCID.Text;
                initialTitle = txtTitleTitle.Text;

                SendSaveMessage(collection);
            }
        }

        private void FillControls()
        {
            txtTitleLCID.Text = collec.ContainsKey("LCID") ? collec["LCID"] : "";
            txtTitleTitle.Text = collec.ContainsKey("Title") ? collec["Title"] : "";

            initialLCID = txtTitleLCID.Text;
            initialTitle = txtTitleTitle.Text;
        }

        private void SiteMapControl_Leave(object sender, EventArgs e)
        {
            if (initialLCID != txtTitleLCID.Text ||
                initialTitle != txtTitleTitle.Text)
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