using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace McTools.Xrm.Connection.WinForms
{
    /// <summary>
    /// Formulaire de sélection d'une connexion à un serveur
    /// Crm dans une liste de connexions existantes.
    /// </summary>
    public partial class ConnectionSelector : Form
    {
        #region Variables

        private readonly bool allowMultipleSelection;
        private readonly bool isConnectionSelection;
        private bool hadCreatedNewConnection;

        /// <summary>
        /// Connexion sélectionnée
        /// </summary>
        private List<ConnectionDetail> selectedConnections;

        /// <summary>
        /// Obtient la connexion sélectionnée
        /// </summary>
        public List<ConnectionDetail> SelectedConnections
        {
            get { return selectedConnections; }
        }

        #endregion Variables

        #region Constructeur

        /// <summary>
        /// Créé une nouvelle instance de la classe ConnectionSelector
        /// </summary>
        public ConnectionSelector(bool allowMultipleSelection = false, bool isConnectionSelection = true)
        {
            InitializeComponent();

            tsb_UseMru.Checked = ConnectionManager.Instance.ConnectionsList.UseMruDisplay;
            tsb_UseMru.CheckedChanged += tsb_UseMru_CheckedChanged;

            this.isConnectionSelection = isConnectionSelection;
            this.allowMultipleSelection = allowMultipleSelection;
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

            DisplayConnections(allowMultipleSelection);
        }

        private void DisplayConnections(bool allowMultipleSelection)
        {
            lvConnections.Items.Clear();
            lvConnections.Groups.Clear();

            ConnectionManager.Instance.ConnectionsList.Connections.Sort();

            lvConnections.MultiSelect = allowMultipleSelection;

            LoadImages();

            var details = ConnectionManager.Instance.ConnectionsList.Connections;
            if (ConnectionManager.Instance.ConnectionsList.UseMruDisplay)
            {
                details = ConnectionManager.Instance.ConnectionsList.Connections.OrderByDescending(c => c.LastUsedOn).ThenBy(c => c.ConnectionName).ToList();
            }

            foreach (ConnectionDetail detail in details)
            {
                var item = new ListViewItem(detail.ConnectionName);
                item.SubItems.Add(detail.ServerName);
                item.SubItems.Add(detail.Organization);
                item.SubItems.Add(detail.OrganizationVersion);
                item.Tag = detail;
                item.ImageIndex = GetImageIndex(detail);

                if (!ConnectionManager.Instance.ConnectionsList.UseMruDisplay)
                {
                    item.Group = GetGroup(detail.AuthType);
                }

                lvConnections.Items.Add(item);
            }

            if (!ConnectionManager.Instance.ConnectionsList.UseMruDisplay)
            {
                var groups = new ListViewGroup[lvConnections.Groups.Count];

                lvConnections.Groups.CopyTo(groups, 0);

                Array.Sort(groups, new GroupComparer());

                lvConnections.BeginUpdate();
                lvConnections.Groups.Clear();
                lvConnections.Groups.AddRange(groups);
                lvConnections.EndUpdate();
            }
        }

        private void LoadImages()
        {
            lvConnections.SmallImageList = new ImageList();
            lvConnections.SmallImageList.Images.Add(RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.server.png"));
            lvConnections.SmallImageList.Images.Add(RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.server_key.png"));
            lvConnections.SmallImageList.Images.Add(RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.CRMOnlineLive_16.png"));
        }

        #endregion Constructeur

        #region Properties

        public bool HadCreatedNewConnection
        {
            get { return hadCreatedNewConnection; }
        }

        #endregion Properties

        #region Méthodes

        private void BCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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

        private void LvConnectionsColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvConnections.Sorting = lvConnections.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvConnections.ListViewItemSorter = new ListViewItemComparer(e.Column, lvConnections.Sorting);
        }

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

        #endregion Méthodes

        private void ConnectionSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                tsbNewConnection_Click(null, null);
            }

            if (e.Control && e.KeyCode == Keys.U)
            {
                tsbUpdateConnection_Click(null, null);
            }

            if (e.Control && e.KeyCode == Keys.D)
            {
                tsbDeleteConnection_Click(null, null);
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

        private void lvConnections_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || lvConnections.SelectedItems.Count == 0)
                return;

            BValidateClick(null, null);
        }

        private void tsb_UseMru_CheckedChanged(object sender, EventArgs e)
        {
            var tsb = (ToolStripButton)sender;
            ConnectionManager.Instance.ConnectionsList.UseMruDisplay = tsb_UseMru.Checked;

            DisplayConnections(allowMultipleSelection);
        }

        private void tsbDeleteConnection_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to delete selected connection(s)?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            foreach (ListViewItem connectionItem in lvConnections.SelectedItems)
            {
                var detailToRemove = (ConnectionDetail)connectionItem.Tag;

                lvConnections.Items.Remove(lvConnections.SelectedItems[0]);

                ConnectionManager.Instance.ConnectionsList.Connections.RemoveAll(d => d.ConnectionId == detailToRemove.ConnectionId);
            }

            ConnectionManager.Instance.SaveConnectionsFile();
        }

        private void tsbNewConnection_Click(object sender, EventArgs e)
        {
            var cForm = new Form1
            {
                StartPosition = FormStartPosition.CenterParent
            };

            //var cForm = new ConnectionForm(true, false)
            //{
            //    StartPosition = FormStartPosition.CenterParent
            //};

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

                ConnectionManager.Instance.SaveConnectionsFile();
            }
        }

        private void tsbUpdateConnection_Click(object sender, EventArgs e)
        {
            if (lvConnections.SelectedItems.Count == 1)
            {
                ListViewItem item = lvConnections.SelectedItems[0];

                var cForm = new Form1
                {
                    StartPosition = FormStartPosition.CenterParent
                };

                //var cForm = new ConnectionForm(false, false)
                //{
                //    CrmConnectionDetail = (ConnectionDetail)item.Tag,
                //    StartPosition = FormStartPosition.CenterParent
                //};

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

                    ConnectionManager.Instance.SaveConnectionsFile();
                }
            }
        }
    }
}