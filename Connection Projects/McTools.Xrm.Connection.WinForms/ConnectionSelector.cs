using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk.Client;

namespace McTools.Xrm.Connection.WinForms
{
    /// <summary>
    /// Formulaire de sélection d'une connexion à un serveur
    /// Crm dans une liste de connexions existantes.
    /// </summary>
    public partial class ConnectionSelector : Form
    {
        #region Variables

        private bool requiresSavingConnectionsFile;

        /// <summary>
        /// Connexion sélectionnée
        /// </summary>
        List<ConnectionDetail> selectedConnections;

        /// <summary>
        /// Obtient la connexion sélectionnée
        /// </summary>
        public List<ConnectionDetail> SelectedConnections
        {
            get { return selectedConnections; }
        }

        private bool hadCreatedNewConnection;

        private readonly bool isConnectionSelection;

        #endregion

        #region Constructeur

        /// <summary>
        /// Créé une nouvelle instance de la classe ConnectionSelector
        /// </summary>
        public ConnectionSelector(bool allowMultipleSelection = false, bool isConnectionSelection = true)
        {
            InitializeComponent();
            this.isConnectionSelection = isConnectionSelection;
            if (isConnectionSelection)
            {
                Text = "Select a connection";
                tsbDeleteConnection.Visible = false;
                tsbUpdateConnection.Visible = false;
                bCancel.Text = "Cancel";
                bValidate.Visible = true;
            }
            else
            {
                Text = "Connections list";
                tsbDeleteConnection.Visible = true;
                tsbUpdateConnection.Visible = true;
                bCancel.Text = "Close";
                bValidate.Visible = false;
            }

            ConnectionManager.Instance.ConnectionsList.Connections.Sort();

            lvConnections.MultiSelect = allowMultipleSelection;

            LoadImages();

            foreach (ConnectionDetail detail in ConnectionManager.Instance.ConnectionsList.Connections)
            {
                var item = new ListViewItem(detail.ConnectionName);
                item.SubItems.Add(detail.ServerName);
                item.SubItems.Add(detail.Organization);
                item.SubItems.Add(detail.OrganizationVersion);
                item.Tag = detail;
                item.Group = GetGroup(detail.AuthType);
                item.ImageIndex = GetImageIndex(detail);

                lvConnections.Items.Add(item);
            }

            var groups = new ListViewGroup[lvConnections.Groups.Count];

            lvConnections.Groups.CopyTo(groups, 0);

            Array.Sort(groups, new GroupComparer());

            lvConnections.BeginUpdate();
            lvConnections.Groups.Clear();
            lvConnections.Groups.AddRange(groups);
            lvConnections.EndUpdate();
        }

       
        private void LoadImages()
        {
            lvConnections.SmallImageList = new ImageList();
            lvConnections.SmallImageList.Images.Add(RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.server.png"));
            lvConnections.SmallImageList.Images.Add(RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.server_key.png"));
            lvConnections.SmallImageList.Images.Add(RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.CRMOnlineLive_16.png"));
          }

        #endregion

        #region Properties

        public bool HadCreatedNewConnection
        {
            get { return hadCreatedNewConnection; }
        }

        #endregion

        #region Méthodes

        private void LvConnectionsMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (isConnectionSelection)
            {
                BValidateClick(sender, e);
            }
            else
            {
                tsbUpdateConnection_Click(sender, null);
            }
        }
        
        private void BValidateClick(object sender, EventArgs e)
        {
            if (lvConnections.SelectedItems.Count > 0)
            {
                selectedConnections = new List<ConnectionDetail>();

                foreach (ListViewItem item in lvConnections.SelectedItems)
                {
                    selectedConnections.Add(item.Tag as ConnectionDetail);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void BCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LvConnectionsColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvConnections.Sorting = lvConnections.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvConnections.ListViewItemSorter = new ListViewItemComparer(e.Column, lvConnections.Sorting);
        }

        #endregion

        private void tsbNewConnection_Click(object sender, EventArgs e)
        {
            var cForm = new ConnectionForm(true, false)
            {
               StartPosition = FormStartPosition.CenterParent
            };

            if (cForm.ShowDialog(this) == DialogResult.OK)
            {
                var newConnection = cForm.CrmConnectionDetail;
                hadCreatedNewConnection = true;

                var item = new ListViewItem(newConnection.ConnectionName);
                item.SubItems.Add(newConnection.ServerName);
                item.SubItems.Add(newConnection.Organization);
                item.SubItems.Add(newConnection.OrganizationVersion);
                item.Tag = newConnection;
                item.Group = GetGroup(newConnection.AuthType);
                item.ImageIndex = GetImageIndex(newConnection);

                lvConnections.Items.Add(item);
                lvConnections.SelectedItems.Clear();
                item.Selected = true;

                lvConnections.Sort();

                if (isConnectionSelection)
                {
                    BValidateClick(sender, e);
                }

                if (ConnectionManager.Instance.ConnectionsList.Connections.FirstOrDefault(
                             d => d.ConnectionId == newConnection.ConnectionId) == null)
                {
                    ConnectionManager.Instance.ConnectionsList.Connections.Add(newConnection);
                }

                requiresSavingConnectionsFile = true;
            }
        }

       private void tsbUpdateConnection_Click(object sender, EventArgs e)
        {
            if (lvConnections.SelectedItems.Count == 1)
            {
                ListViewItem item = lvConnections.SelectedItems[0];

                var cForm = new ConnectionForm(false, false)
                {
                    CrmConnectionDetail = (ConnectionDetail)item.Tag,
                    StartPosition = FormStartPosition.CenterParent
                };

                if (cForm.ShowDialog(this) == DialogResult.OK)
                {
                    item.SubItems[0].Text = cForm.CrmConnectionDetail.ConnectionName;
                    item.SubItems[1].Text = cForm.CrmConnectionDetail.ServerName;
                    item.SubItems[2].Text = cForm.CrmConnectionDetail.Organization;
                    if (item.SubItems.Count == 4)
                    {
                        item.SubItems[3].Text = cForm.CrmConnectionDetail.OrganizationVersion;
                    }
                    else
                    {
                        item.SubItems.Add(cForm.CrmConnectionDetail.OrganizationVersion);
                    }
                    item.Group = GetGroup(cForm.CrmConnectionDetail.AuthType);

                    lvConnections.Refresh();

                    requiresSavingConnectionsFile = true;
                }
            }
        }

        private void tsbDeleteConnection_Click(object sender, EventArgs e)
        {
            if (lvConnections.SelectedItems.Count > 0)
            {
                var selectedItem = lvConnections.SelectedItems[0];
                var detailToRemove = (ConnectionDetail)selectedItem.Tag;
                
                lvConnections.Items.Remove(lvConnections.SelectedItems[0]);
               
                var realItemToDelete =
                    ConnectionManager.Instance.ConnectionsList.Connections.FirstOrDefault(
                        c => c.ConnectionId == detailToRemove.ConnectionId);

                if (realItemToDelete != null)
                {
                    ConnectionManager.Instance.ConnectionsList.Connections.Remove(detailToRemove);
                    requiresSavingConnectionsFile = true; 
                }
            }
        }

        private ListViewGroup GetGroup(AuthenticationProviderType type)
        {
            string groupName = string.Empty;

            switch (type)
            {
                case AuthenticationProviderType.ActiveDirectory:
                    groupName = "OnPremise";
                    break;
                case AuthenticationProviderType.OnlineFederation:
                    groupName = "CRM Online - Office 365";
                    break;
                case AuthenticationProviderType.LiveId:
                    groupName = "CRM Online - CTP";
                    break;
                case AuthenticationProviderType.Federation:
                    groupName = "Claims authentication - Internet Facing Deployment";
                    break;
            }

            var group = lvConnections.Groups.Cast<ListViewGroup>().FirstOrDefault(g => g.Name == groupName);
            if (group == null)
            {
                group = new ListViewGroup(groupName, groupName);
                lvConnections.Groups.Add(group);
            }

            return group;
        }

        private int GetImageIndex(ConnectionDetail detail)
        {
            if (detail.UseOnline)
            {
                return 2;
            }

            if (detail.UseOsdp)
            {
                return 2;
            }

            if (detail.UseIfd)
            {
                return 1;
            }

            return 0;
        }

        private void ConnectionSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectionManager.Instance.SaveConnectionsFile();
        }
    }
}