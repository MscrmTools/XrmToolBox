using System;
using System.Collections.Generic;
using System.Drawing;
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

        private FormHelper _formHelper;

        /// <summary>
        /// Resources manager
        /// </summary>
        private System.ComponentModel.ComponentResourceManager resources;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class CrmConnectionStatusBar
        /// </summary>
        public CrmConnectionStatusBar(FormHelper formHelper)
        {
            resources = new System.ComponentModel.ComponentResourceManager(typeof(CrmConnectionStatusBar));

            ConnectionManager.Instance.ConnectionListUpdated += cManager_ConnectionListUpdated;
            _formHelper = formHelper;

            // Build connection control
            this.BuildConnectionControl();

            // Add label that will display information about connection
            ToolStripStatusLabel informationLabel = new ToolStripStatusLabel
            {
                Spring = true,
                TextAlign = ContentAlignment.MiddleRight
            };

            this.Items.Add(informationLabel);

            ToolStripProgressBar progress = new ToolStripProgressBar
            {
                Minimum = 0,
                Maximum = 100,
                Visible = false
            };
            this.Items.Add(progress);

            base.RenderMode = ToolStripRenderMode.Professional;
        }

        private void cManager_ConnectionListUpdated(object sender, EventArgs e)
        {
            RebuildConnectionList();
        }

        #endregion Constructor

        #region Methods

        public void RebuildConnectionList()
        {
            AddActionsList((ToolStripDropDownButton)Items[0]);
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
            ToolStripStatusLabel label = (ToolStripStatusLabel)this.Items[1];

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

        public void SetProgress(int? percent)
        {
            ToolStripProgressBar progress = (ToolStripProgressBar)this.Items[2];

            MethodInvoker mi = delegate
            {
                if (percent.HasValue)
                {
                    progress.Value = percent.Value;
                    progress.Visible = true;
                }
                else
                {
                    progress.Value = 0;
                    progress.Visible = false;
                }
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
        /// Adds the ToolStripMenuItems representing connections to the
        /// ToolStripDropDownButton
        /// </summary>
        /// <param name="btn">ToolStripDropDownButton where to add connections</param>
        private void AddActionsList(ToolStripDropDownButton btn)
        {
            var list = new List<ToolStripItem>();

            if (ConnectionManager.Instance.ConnectionsList.Connections.Count > 0)
            {
                ConnectionManager.Instance.ConnectionsList.Connections.Sort();

                foreach (ConnectionDetail cDetail in ConnectionManager.Instance.ConnectionsList.Connections)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = cDetail.ConnectionName;
                    item.Tag = cDetail;

                    if (cDetail.UseOnline)
                    {
                        item.Image =
                            RessourceManager.GetImage(
                                "McTools.Xrm.Connection.WinForms.Resources.CRMOnlineLive_16.png");
                    }
                    else if (cDetail.UseOsdp)
                    {
                        item.Image =
                            RessourceManager.GetImage(
                                "McTools.Xrm.Connection.WinForms.Resources.CRMOnlineLive_16.png");
                    }
                    else if (cDetail.UseIfd)
                    {
                        item.Image =
                            RessourceManager.GetImage(
                                "McTools.Xrm.Connection.WinForms.Resources.server_key.png");
                    }
                    else
                    {
                        item.Image =
                            RessourceManager.GetImage(
                                "McTools.Xrm.Connection.WinForms.Resources.server.png");
                    }

                    BuildActionItems(item);
                    list.Add(item);
                }

                list.Add(new ToolStripSeparator());
            }

            var newConnectionItem = new ToolStripMenuItem();
            newConnectionItem.Text = "Create new connection";
            newConnectionItem.Image = ((System.Drawing.Image)(resources.GetObject("server_add")));
            newConnectionItem.Click += newConnectionItem_Click;
            list.Add(newConnectionItem);

            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    btn.DropDownItems.Clear();
                    btn.DropDownItems.AddRange(list.ToArray());
                }));
            }
            else
            {
                btn.DropDownItems.Clear();
                btn.DropDownItems.AddRange(list.ToArray());
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

        /// <summary>
        /// Builds the ToolStripDropDownButton that will manage connections
        /// </summary>
        private void BuildConnectionControl()
        {
            ToolStripDropDownButton connexionManager = new ToolStripDropDownButton();
            connexionManager.Text = "Not connected";
            connexionManager.Image = ((System.Drawing.Image)(resources.GetObject("server")));

            this.AddActionsList(connexionManager);

            this.Items.Add(connexionManager);
        }

        #endregion Methods

        #region Events

        private void actionItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            ToolStripMenuItem parentItem = (ToolStripMenuItem)clickedItem.OwnerItem;
            ConnectionDetail currentConnection = (ConnectionDetail)parentItem.Tag;
            ToolStripDropDownButton connexionManager = (ToolStripDropDownButton)parentItem.OwnerItem;

            switch (clickedItem.Text)
            {
                case "Connect":

                    if (currentConnection.IsCustomAuth)
                    {
                        if (_formHelper.RequestPassword(currentConnection))
                        {
                            ConnectionManager.Instance.ConnectToServer(currentConnection);
                        }
                    }
                    else
                    {
                        ConnectionManager.Instance.ConnectToServer(currentConnection);
                    }
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
                    if (MessageBox.Show(this, "Are you sure you want to delete selected connection(s)?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    connexionManager.DropDownItems.Remove(parentItem);

                    if (connexionManager.DropDownItems.Count == 2)
                    {
                        connexionManager.DropDownItems.RemoveAt(0);
                    }

                    _formHelper.DeleteConnection(currentConnection);

                    break;
            }
        }

        private void connexionManager_Click(object sender, EventArgs e)
        {
            // On main ToolStripDropDownButton button click, we rebuild the list
            // of crm connections
            this.AddActionsList((ToolStripDropDownButton)sender);
        }

        private void newConnectionItem_Click(object sender, EventArgs e)
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

        #endregion Events
    }
}