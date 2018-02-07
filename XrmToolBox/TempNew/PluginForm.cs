using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.TempNew
{
    public partial class PluginForm : DockContent
    {
        private readonly PluginControlBase pluginControlBase;

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

        private void StatusBarMessager_SendMessageToStatusBar(object sender, Extensibility.Args.StatusBarMessageEventArgs e)
        {
            void Mi()
            {
                if (string.IsNullOrEmpty(e.Message) && e.Progress == null)
                {
                    statusStrip.Visible = false;
                }
                else if (!string.IsNullOrEmpty(e.Message) && e.Progress != null)
                {
                    toolStripProgressBar.Value = e.Progress ?? 0;
                    toolStripStatusLabel.Text = e.Message;
                    toolStripProgressBar.Visible = true;
                    toolStripStatusLabel.Visible = true;
                    statusStrip.Visible = true;
                }
                else if (!string.IsNullOrEmpty(e.Message))
                {
                    toolStripStatusLabel.Text = e.Message;
                    toolStripStatusLabel.Visible = true;
                    toolStripProgressBar.Visible = false;
                    statusStrip.Visible = true;
                }
                else if (e.Progress != null)
                {
                    toolStripProgressBar.Value = e.Progress.Value;
                    toolStripProgressBar.Visible = true;
                    toolStripStatusLabel.Visible = false;
                    statusStrip.Visible = true;
                }
            }

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)Mi);
            }
            else
            {
                Mi();
            }
        }

        public void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            pluginControlBase.UpdateConnection(newService, detail, actionName, parameter);
        }

        public void SendIncomingBrokerMessage(MessageBusEventArgs message)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (pluginControlBase is IMessageBusHost host)
            {
                host.OnIncomingMessage(message);
            }
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public string PluginTitle => pluginControlBase.GetType().GetTitle();

        public IXrmToolBoxPluginControl Control => pluginControlBase;

        private void PluginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseRequested?.Invoke(this, new System.EventArgs());
            //if (Options.Instance.CloseEachPluginSilently) return;

            //if (e.CloseReason != CloseReason.UserClosing) return;

            //var closeInfo = new PluginCloseInfo(ToolBoxCloseReason.PluginRequest);

            //pluginControlBase.ClosingPlugin(closeInfo);

            //e.Cancel = closeInfo.Cancel;
        }

        private void PluginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pluginControlBase.Dispose();
        }
    }
}