using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility.UserControls
{
    public partial class NotificationArea : UserControl
    {
        private Uri moreInfoUri;

        public NotificationArea()
        {
            InitializeComponent();


            AddBackcolorChangedEvent(this);
            AddForecolorChangedEvent(this);
        }

        private void AddBackcolorChangedEvent(Control control)
        {
            control.BackColorChanged += NotificationArea_BackColorChanged;
            foreach (Control childControl in control.Controls)
            {
                childControl.BackColorChanged += NotificationArea_BackColorChanged;
                AddBackcolorChangedEvent(childControl);
            }
        }

        private void AddForecolorChangedEvent(Control control)
        {
            control.ForeColorChanged += NotificationArea_ForeColorChanged;
            foreach (Control childControl in control.Controls)
            {
                childControl.ForeColorChanged += NotificationArea_ForeColorChanged;
                AddForecolorChangedEvent(childControl);
            }
        }

        private void NotificationArea_ForeColorChanged(object sender, EventArgs e)
        {
            var ctrl = (Control)sender;
            ctrl.ForeColorChanged -= NotificationArea_ForeColorChanged;
            ctrl.ForeColor = SystemColors.ControlText;
            ctrl.ForeColorChanged += NotificationArea_ForeColorChanged;
        }

        private void NotificationArea_BackColorChanged(object sender, EventArgs e)
        {
            var ctrl = (Control)sender;
            ctrl.BackColorChanged -= NotificationArea_BackColorChanged;
            ctrl.BackColor = SystemColors.Info;
            ctrl.BackColorChanged += NotificationArea_BackColorChanged;
        }

        private void Initialize(string message, Uri infoUri, int height = 30)
        {
            lblMessage.Text = message;
            Height = height;
            llLearMore.Visible = infoUri != null;
            moreInfoUri = infoUri;
            Visible = true;
        }

        public void ShowInfoNotification(string message, Uri infoUri, int height = 30)
        {
            Initialize(message, infoUri, height);

            pbNotif.Image = Icons.notif_icn_info16;
        }

        public void ShowWarningNotification(string message, Uri infoUri, int height = 30)
        {
            Initialize(message, infoUri, height);

            pbNotif.Image = Icons.notif_icn_alert16;
        }

        public void ShowErrorNotification(string message, Uri infoUri, int height = 30)
        {
            Initialize(message, infoUri, height);

            pbNotif.Image = Icons.notif_icn_crit16;
        }

        private void llDismiss_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Visible = false;
        }

        private void llLearnMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(moreInfoUri.AbsoluteUri); 
        }
    }
}
