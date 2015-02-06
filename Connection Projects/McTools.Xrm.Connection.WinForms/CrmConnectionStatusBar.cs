using System;
using System.Windows.Forms;

namespace McTools.Xrm.Connection.WinForms
{
    /// <summary>
    /// StatusStrip child object allowing to list, create, upadate and connect
    /// to Crm server.
    /// </summary>
    public partial class CrmConnectionStatusBar : StatusStrip
    {
        #region Variables

        /// <summary>
        /// Crm connection manager
        /// </summary>
        ConnectionManager cManager;

        private FormHelper _formHelper;

        /// <summary>
        /// Resources manager
        /// </summary>
        System.ComponentModel.ComponentResourceManager resources;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class CrmConnectionStatusBar
        /// </summary>
        /// <param name="connectionManager">Connection manager to use</param>
        public CrmConnectionStatusBar(ConnectionManager connectionManager, FormHelper formHelper)
        {
            resources = new System.ComponentModel.ComponentResourceManager(typeof(CrmConnectionStatusBar));


            this.cManager = connectionManager;
            _formHelper = formHelper;

            // Build connection control
            this.BuildConnectionControl();

            // Add label that will display information about connection
            ToolStripLabel informationLabel = new ToolStripLabel();
            this.Items.Add(informationLabel);
            base.RenderMode = ToolStripRenderMode.Professional;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds the ToolStripDropDownButton that will manage connections
        /// </summary>
        private void BuildConnectionControl()
        {
            ToolStripDropDownButton connexionManager = new ToolStripDropDownButton();
            connexionManager.Text = "Not connected";
            connexionManager.Image = ((System.Drawing.Image)(resources.GetObject("server")));
            //connexionManager.Click += new EventHandler(connexionManager_Click);

            this.AddActionsList(connexionManager);

            this.Items.Add(connexionManager);
        }

        public void RebuildConnectionList()
        {
            AddActionsList((ToolStripDropDownButton)Items[0]);
        }

        /// <summary>
        /// Adds the ToolStripMenuItems representing connections to the 
        /// ToolStripDropDownButton
        /// </summary>
        /// <param name="btn">ToolStripDropDownButton where to add connections</param>
        private void AddActionsList(ToolStripDropDownButton btn)
        {
            // Clearing existing connections
            btn.DropDownItems.Clear();

            if (this.cManager != null && this.cManager.ConnectionsList != null && this.cManager.ConnectionsList.Connections.Count > 0)
            {
                this.cManager.ConnectionsList.Connections.Sort();

                foreach (ConnectionDetail cDetail in this.cManager.ConnectionsList.Connections)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = cDetail.ConnectionName;
                    item.Tag = cDetail;

                    if (cDetail.UseOnline)
                    {
                        item.Image = RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.CRMOnlineLive_16.png");
                    }
                    else if (cDetail.UseOsdp)
                    {
                        item.Image = RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.CRMOnlineLive_16.png");
                    }
                    else if (cDetail.UseIfd)
                    {
                        item.Image = RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.server_key.png");
                    }
                    else
                    {
                        item.Image = RessourceManager.GetImage("McTools.Xrm.Connection.WinForms.Resources.server.png");
                    }

                    this.BuildActionItems(item);

                    btn.DropDownItems.Add(item);
                }

                if (this.cManager.ConnectionsList.Connections.Count > 0)
                {
                    ToolStripSeparator separator = new ToolStripSeparator();
                    btn.DropDownItems.Add(separator);
                }
            }

            ToolStripMenuItem newConnectionItem = new ToolStripMenuItem();
            newConnectionItem.Text = "Create new connection";
            newConnectionItem.Image = ((System.Drawing.Image)(resources.GetObject("server_add")));
            newConnectionItem.Click += new EventHandler(newConnectionItem_Click);
            btn.DropDownItems.Add(newConnectionItem);
        }

        /// <summary>
        /// Updates the connection status displayed on the main ToolStripDropDownButton
        /// </summary>
        /// <param name="isConnected">Indicates if the status is 'Connected'</param>
        /// <param name="detail">Connection details</param>
        public void SetConnectionStatus(bool isConnected, ConnectionDetail detail)
        {
            ToolStripDropDownButton btn = (ToolStripDropDownButton)this.Items[0];

            if (isConnected)
            {
                this.SetMessage("Connected!");
                btn.Text = string.Format("Connected to '{0} ({1})'",
                       detail.ServerName,
                       detail.OrganizationFriendlyName);
                btn.Image = (System.Drawing.Image)(resources.GetObject("server_lightning"));
            }
            else
            {
                btn.Text = "Not connected";
                btn.Image = (System.Drawing.Image)(resources.GetObject("server"));
            }
        }

        /// <summary>
        /// Displays a message about the connection
        /// </summary>
        /// <param name="message">Message to display</param>
        public void SetMessage(string message)
        {
            ToolStripLabel label = (ToolStripLabel)this.Items[1];

            MethodInvoker mi = delegate
            {
                label.Text = message;
            };

            if (this.InvokeRequired)
            {
                this.Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        /// <summary>
        /// Creates the three action menus for a connection
        /// </summary>
        /// <param name="item">Menu where to add the actions</param>
        private void BuildActionItems(ToolStripMenuItem item)
        {
            ToolStripMenuItem cItem = new ToolStripMenuItem();
            cItem.Click += new EventHandler(actionItem_Click);
            cItem.Text = "Connect";
            cItem.Image = ((System.Drawing.Image)(resources.GetObject("server_connect")));
            item.DropDownItems.Add(cItem);

            ToolStripMenuItem eItem = new ToolStripMenuItem();
            eItem.Click += new EventHandler(actionItem_Click);
            eItem.Text = "Edit";
            eItem.Image = ((System.Drawing.Image)(resources.GetObject("server_edit")));
            item.DropDownItems.Add(eItem);

            ToolStripMenuItem dItem = new ToolStripMenuItem();
            dItem.Click += new EventHandler(actionItem_Click);
            dItem.Text = "Delete";
            dItem.Image = ((System.Drawing.Image)(resources.GetObject("server_delete")));
            item.DropDownItems.Add(dItem);
        }

        #endregion

        #region Events

        void connexionManager_Click(object sender, EventArgs e)
        {
            // On main ToolStripDropDownButton button click, we rebuild the list
            // of crm connections
            this.AddActionsList((ToolStripDropDownButton)sender);
        }

        void newConnectionItem_Click(object sender, EventArgs e)
        {
            ConnectionDetail detail = _formHelper.EditConnection(true, null);

            if (detail != null)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = detail.ConnectionName;
                item.Tag = detail;

                BuildActionItems(item);

                ToolStripDropDownButton connexionManager = (ToolStripDropDownButton)this.Items[0];

                if (connexionManager.DropDownItems.Count == 1)
                {
                    connexionManager.DropDownItems.Insert(0, new ToolStripSeparator());
                    connexionManager.DropDownItems.Insert(0, item);
                }
                else
                {
                    connexionManager.DropDownItems.Insert(connexionManager.DropDownItems.Count - 2, item);
                }

                MessageBox.Show(this, "Connection Created Successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void actionItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStripMenuItem parentItem = (ToolStripMenuItem)clickedItem.OwnerItem;
            ConnectionDetail currentConnection = (ConnectionDetail)parentItem.Tag;
            ToolStripDropDownButton connexionManager = (ToolStripDropDownButton)parentItem.OwnerItem;

            switch (clickedItem.Text)
            {
                case "Connect":
                    this.cManager.ConnectToServer(currentConnection);
                    break;
                case "Edit":
                    currentConnection = _formHelper.EditConnection(false, currentConnection);

                    if (currentConnection != null && parentItem.Text != currentConnection.ConnectionName)
                    {
                        parentItem.Text = currentConnection.ConnectionName;
                        parentItem.Tag = currentConnection;
                    }

                    break;
                case "Delete":
                    connexionManager.DropDownItems.Remove(parentItem);

                    if (connexionManager.DropDownItems.Count == 2)
                    {
                        connexionManager.DropDownItems.RemoveAt(0);
                    }

                    _formHelper.DeleteConnection(currentConnection);

                    break;
            }
        }

        #endregion
    }
}