using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility.UserControls
{
    public partial class NotificationArea : UserControl
    {
        private Uri moreInfoUri;

        public NotificationArea()
        {
            InitializeComponent();
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
