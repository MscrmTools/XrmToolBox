using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace McTools.Xrm.Connection.WinForms
{
    public class FormHelper
    {
        private readonly Form _innerAppForm;

        public FormHelper(Form innerAppForm)
        {
            _innerAppForm = innerAppForm;
        }

        /// <summary>
        /// Asks this manager to select a Crm connection to use
        /// </summary>
        /// <param name="connectionParameter">The connection parameter.</param>
        /// <param name="preConnectionRequestAction">The action to be performed before the async call to create the connection.  Useful to display a please wait message</param>
        /// <returns></returns>
        public bool AskForConnection(object connectionParameter, Action preConnectionRequestAction)
        {
            var cs = new ConnectionSelector
            {
                StartPosition = FormStartPosition.CenterParent,
            };

            if (cs.ShowDialog(_innerAppForm) == DialogResult.OK)
            {
                var connectionDetail = cs.SelectedConnections.First();
                if (connectionDetail.IsCustomAuth)
                {
                    if (connectionDetail.PasswordIsEmpty)
                    {
                        var pForm = new PasswordForm()
                        {
                            UserDomain = connectionDetail.UserDomain,
                            UserLogin = connectionDetail.UserName
                        };
                        if (pForm.ShowDialog(_innerAppForm) == DialogResult.OK)
                        {
                            connectionDetail.SetPassword(pForm.UserPassword);
                            connectionDetail.SavePassword = pForm.SavePassword;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (preConnectionRequestAction != null)
                {
                    preConnectionRequestAction();
                }

                ConnectionManager.Instance.ConnectToServer(connectionDetail, connectionParameter);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Asks this manager to select a Crm connection to use
        /// </summary>
        public bool AskForConnection(object connectionParameter)
        {
            return AskForConnection(connectionParameter, null);
        }

        /// <summary>
        /// Deletes a Crm connection from the connections list
        /// </summary>
        /// <param name="connectionToDelete">Details of the connection to delete</param>
        public void DeleteConnection(ConnectionDetail connectionToDelete)
        {
            ConnectionManager.Instance.ConnectionsList.Connections.Remove(connectionToDelete);
            ConnectionManager.Instance.SaveConnectionsFile();
        }

        public void DisplayConnectionsList(Form form)
        {
            var cs = new ConnectionSelector(true, false)
            {
                StartPosition = FormStartPosition.CenterParent,
            };

            cs.ShowDialog(form);
        }

        /// <summary>
        /// Creates or updates a Crm connection
        /// </summary>
        /// <param name="isCreation">Indicates if it is a connection creation</param>
        /// <param name="connectionToUpdate">Details of the connection to update</param>
        /// <returns>Created or updated connection</returns>
        public ConnectionDetail EditConnection(bool isCreation, ConnectionDetail connectionToUpdate)
        {
            var cForm = new ConnectionWizard(connectionToUpdate) { StartPosition = FormStartPosition.CenterParent };

            //var cForm = new ConnectionForm(isCreation) { StartPosition = FormStartPosition.CenterParent };

            //if (!isCreation)
            //{
            //    cForm.CrmConnectionDetail = connectionToUpdate;
            //}

            if (cForm.ShowDialog(_innerAppForm) == DialogResult.OK)
            {
                // TODO on garde?
                //if (cForm.DoConnect)
                //{
                //    ConnectionManager.Instance.ConnectToServer(cForm.CrmConnectionDetail);
                //}

                //if (!cForm.CrmConnectionDetail.PasswordIsEmpty && !cForm.CrmConnectionDetail.SavePassword)
                //{
                //    cForm.CrmConnectionDetail.ErasePassword();
                //}

                if (isCreation)
                {
                    if (ConnectionManager.Instance.ConnectionsList.Connections.FirstOrDefault(
                        d => d.ConnectionId == cForm.CrmConnectionDetail.ConnectionId) == null)
                    {
                        ConnectionManager.Instance.ConnectionsList.Connections.Add(cForm.CrmConnectionDetail);
                    }
                }
                else
                {
                    foreach (ConnectionDetail detail in ConnectionManager.Instance.ConnectionsList.Connections)
                    {
                        if (detail.ConnectionId == cForm.CrmConnectionDetail.ConnectionId)
                        {
                            detail.UpdateAfterEdit(cForm.CrmConnectionDetail);
                        }
                    }
                }

                ConnectionManager.Instance.SaveConnectionsFile();

                return cForm.CrmConnectionDetail;
            }

            return null;
        }

        /// <summary>
        /// Checks the existence of a user password and returns it
        /// </summary>
        /// <param name="detail">Details of the Crm connection</param>
        /// <returns>True if password defined</returns>
        public bool RequestPassword(ConnectionDetail detail)
        {
            if (!detail.PasswordIsEmpty)
                return true;

            bool returnValue = false;

            var pForm = new PasswordForm
            {
                UserLogin = detail.UserName,
                UserDomain = detail.UserDomain,
            };

            MethodInvoker mi = delegate
            {
                if (pForm.ShowDialog(_innerAppForm) == DialogResult.OK)
                {
                    detail.SetPassword(pForm.UserPassword);
                    detail.SavePassword = pForm.SavePassword;
                    returnValue = true;
                }
            };

            if (_innerAppForm.InvokeRequired)
            {
                _innerAppForm.Invoke(mi);
            }
            else
            {
                mi();
            }

            return returnValue;
        }

        public List<ConnectionDetail> SelectMultipleConnectionDetails()
        {
            var cs = new ConnectionSelector(true)
            {
                StartPosition = FormStartPosition.CenterParent,
            };

            if (cs.ShowDialog(_innerAppForm) == DialogResult.OK)
            {
                return cs.SelectedConnections;
            }

            return new List<ConnectionDetail>();
        }
    }
}