using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
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

        public PluginForm(UserControl control, string name)
        {
            InitializeComponent();

            Tag = control.Tag;
            Text = name;

            control.Dock = DockStyle.Fill;
            Controls.Add(control);
            Controls.SetChildIndex(control, 0);
            pluginControlBase = (PluginControlBase)control;
            Icon = pluginControlBase.PluginIcon;

            if (pluginControlBase is IStatusBarMessenger statusBarMessenger)
            {
                statusBarMessenger.SendMessageToStatusBar += StatusBarMessager_SendMessageToStatusBar;
            }
        }

        public event EventHandler CloseRequested;

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public IXrmToolBoxPluginControl Control => pluginControlBase;

        public string PluginName { get; internal set; }

        public string PluginTitle => pluginControlBase.GetType().GetTitle();

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
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
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.Tab:

                    if (TabIndex == DockPanel.Documents.OfType<DockContent>().Count() - 1)
                    {
                        DockPanel.Documents.OfType<DockContent>().First(d => d.TabIndex == 0).Activate();
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
            CloseRequested?.Invoke(this, new System.EventArgs());
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