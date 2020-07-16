using McTools.Xrm.Connection;
using McTools.Xrm.Connection.AppCode;
using Microsoft.Xrm.Sdk;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.New
{
    public partial class PluginForm : DockContent, IStatusBarMessenger
    {
        private readonly PluginControlBase pluginControlBase;
        private StatusBarMessageEventArgs lastStatusEventArgs;

        public PluginForm(UserControl control, string name, string pluginName)
        {
            InitializeComponent();

            Tag = control.Tag;
            Text = name;
            PluginTitle = pluginName;

            control.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(control);
            pnlMain.Controls.SetChildIndex(control, 0);
            pluginControlBase = (PluginControlBase)control;
            pluginControlBase.OnCloseTool += PluginControlBase_OnCloseTool;
            Icon = pluginControlBase.PluginIcon;

            DisplayHighlight(pluginControlBase.ConnectionDetail);

            if (pluginControlBase is IStatusBarMessenger statusBarMessenger)
            {
                statusBarMessenger.SendMessageToStatusBar += StatusBarMessager_SendMessageToStatusBar;
            }

            if (pluginControlBase.ConnectionDetail != null)
            {
                pluginControlBase.ConnectionDetail.OnImpersonate += Detail_OnImpersonate;
                if (pluginControlBase.ConnectionDetail.ImpersonatedUserId != Guid.Empty)
                {
                    Detail_OnImpersonate(pluginControlBase.ConnectionDetail,
                        new ImpersonationEventArgs(pluginControlBase.ConnectionDetail.ImpersonatedUserId,
                            pluginControlBase.ConnectionDetail.ImpersonatedUserName));
                }
            }
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public sealed override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        public IXrmToolBoxPluginControl Control => pluginControlBase;

        public string PluginName { get; internal set; }

        public string PluginTitle { get; }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public bool CloseWithReason(ToolBoxCloseReason reason, bool forceSilent = false)
        {
            PluginCloseInfo info = new PluginCloseInfo(reason);
            if (Options.Instance.CloseEachPluginSilently || forceSilent)
            {
                info.Silent = true;
            }

            pluginControlBase?.ClosingPlugin(info);
            if (info.Cancel) return false;

            FormClosing -= PluginForm_FormClosing;
            Close();
            return true;
        }

        public void SendIncomingBrokerMessage(MessageBusEventArgs message)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (pluginControlBase is IMessageBusHost host)
            {
                host.OnIncomingMessage(message);
            }
        }

        public void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            pluginControlBase.UpdateConnection(newService, detail, actionName, parameter);
            detail.OnImpersonate += Detail_OnImpersonate;

            if (detail.ImpersonatedUserId != Guid.Empty)
            {
                Detail_OnImpersonate(detail, new ImpersonationEventArgs(detail.ImpersonatedUserId, detail.ImpersonatedUserName));
            }

            if (actionName != "AdditionalOrganization")
                DisplayHighlight(detail);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.Tab:

                    if (TabIndex == DockPanel.Documents.OfType<DockContent>().Count() - 1)
                    {
                        DockPanel.Documents.OfType<DockContent>().FirstOrDefault(d => d.TabIndex == 0)?.Activate();
                        return true;
                    }

                    foreach (var document in DockPanel.Documents.OfType<DockContent>())
                    {
                        if (document.TabIndex == TabIndex + 1)
                        {
                            document.Activate();
                            return true;
                        }
                    }
                    break;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            return true;
        }

        private void btnResetImpersonate_Click(object sender, System.EventArgs e)
        {
            pluginControlBase.ConnectionDetail.RemoveImpersonation();
        }

        private void Detail_OnImpersonate(object sender, ImpersonationEventArgs e)
        {
            if (e.UserId == Guid.Empty)
            {
                pnlImpersonate.Visible = false;
            }
            else
            {
                lblImpersonation.Text = string.Format(lblImpersonation.Tag.ToString(), e.UserName, e.UserId);
                pnlImpersonate.Visible = true;
            }
        }

        private void DisplayHighlight(ConnectionDetail detail)
        {
            if (detail?.IsEnvironmentHighlightSet ?? false)
            {
                BackColor = detail.EnvironmentHighlightingInfo?.Color ?? DefaultBackColor;
                lblEnvInfo.ForeColor = detail.EnvironmentHighlightingInfo?.TextColor ?? DefaultForeColor;
                lblEnvInfo.Text = detail.EnvironmentHighlightingInfo?.Text ?? "";
                lblEnvInfo.Visible = true;
                Padding = new Padding(10, 0, 10, 10);
            }
            else
            {
                Padding = new Padding(0, 0, 0, 0);
                lblEnvInfo.Visible = false;
            }

            Invalidate();
        }

        private void PluginControlBase_OnCloseTool(object sender, System.EventArgs e)
        {
            Close();
        }

        private void PluginForm_DockStateChanged(object sender, System.EventArgs e)
        {
            if (lastStatusEventArgs != null)
            {
                if (DockState == DockState.Float || DockState == DockState.Hidden)
                {
                    SendMessageToStatusBar?.Invoke(Control, new StatusBarMessageEventArgs(null, null));
                }

                var timer = new Timer { Interval = 100 };
                timer.Tick += (s, evt) =>
                {
                    timer.Stop();
                    StatusBarMessager_SendMessageToStatusBar(Control, lastStatusEventArgs);
                };
                timer.Start();
            }
        }

        private void PluginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pluginControlBase.Dispose();
        }

        private void PluginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.MdiFormClosing && Options.Instance.ClosePluginsSilentlyOnWindowsShutdown) return;

            e.Cancel = !CloseWithReason(ToolBoxCloseReason.CloseCurrent);
        }

        private void StatusBarMessager_SendMessageToStatusBar(object sender, StatusBarMessageEventArgs e)
        {
            void Mi()
            {
                lastStatusEventArgs = e;

                if (DockState != DockState.Float)
                {
                    statusStrip1.Visible = false;
                    SendMessageToStatusBar?.Invoke(sender, e);
                    return;
                }

                SendMessageToStatusBar?.Invoke(sender, new StatusBarMessageEventArgs(null, null));

                statusStrip1.Visible = true;

                if (string.IsNullOrEmpty(e.Message) && e.Progress == null)
                {
                    statusStrip1.Visible = false;
                }
                else if (!string.IsNullOrEmpty(e.Message) && e.Progress != null)
                {
                    toolStripProgressBar.Value = e.Progress.Value;
                    toolStripStatusLabel.Text = e.Message;
                    toolStripProgressBar.Visible = true;
                    toolStripStatusLabel.Visible = true;
                    statusStrip1.Visible = true;
                }
                else if (!string.IsNullOrEmpty(e.Message))
                {
                    toolStripStatusLabel.Text = e.Message;
                    toolStripStatusLabel.Visible = true;
                    toolStripProgressBar.Visible = false;
                    statusStrip1.Visible = true;
                }
                else if (e.Progress != null)
                {
                    toolStripProgressBar.Value = e.Progress.Value;
                    toolStripProgressBar.Visible = true;
                    toolStripStatusLabel.Visible = false;
                    statusStrip1.Visible = true;
                }
            }

            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke((MethodInvoker)Mi);
            }
            else
            {
                Mi();
            }
        }
    }
}