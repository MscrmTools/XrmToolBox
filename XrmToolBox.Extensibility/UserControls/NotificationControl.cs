using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.Extensibility.Properties;

namespace XrmToolBox.Extensibility.UserControls
{
    public enum NotificationAction
    {
        None,
        Button,
        Link,
        Both
    }

    public enum NotificationType
    {
        Success,
        Error,
        Warning,
        Info
    }

    public partial class NotificationControl : UserControl
    {
        private NotificationAction action = NotificationAction.None;
        private string buttonText = "Action";
        private bool canBeClosed = true;
        private string linkText = "Learn more";
        private string message = "Message";
        private NotificationType type = NotificationType.Info;

        public NotificationControl()
        {
            InitializeComponent();

            lLabel.Paint -= NotificationControl_Paint;
        }

        public event EventHandler ButtonClicked;

        public event LinkLabelLinkClickedEventHandler LinkClicked;

        public NotificationAction Action
        {
            get => action;
            set
            {
                action = value;
                btnAction.Visible = action == NotificationAction.Button || action == NotificationAction.Both;
                lLabel.Visible = action == NotificationAction.Link || action == NotificationAction.Both;
            }
        }

        public string ButtonText
        {
            get => buttonText;
            set
            {
                buttonText = value;
                btnAction.Text = buttonText;

                var size = TextRenderer.MeasureText(btnAction.Text, btnAction.Font);
                btnAction.Width = size.Width + 10;
            }
        }

        public bool CanBeClosed
        {
            get => canBeClosed;
            set
            {
                canBeClosed = value;
                lblClose.Visible = canBeClosed;
            }
        }

        public string LinkText
        {
            get => linkText;
            set
            {
                linkText = value;
                lLabel.Text = linkText;

                var size = TextRenderer.MeasureText(lLabel.Text, lLabel.Font);
                lLabel.Width = size.Width + 10;
            }
        }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                Invalidate();
            }
        }

        public NotificationType Type
        {
            get => type;
            set
            {
                type = value;

                Invalidate();
            }
        }

        public void SetVisible(bool visible, int delayBeforeAutoHide = 0)
        {
            this.Visible = visible;

            if (visible && delayBeforeAutoHide > 0)
            {
                Timer timer = new Timer();
                timer.Interval = delayBeforeAutoHide;
                timer.Tick += (s, e) =>
                {
                    this.Visible = false;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }

        private void btnAction_Click(object sender, System.EventArgs e)
        {
            ButtonClicked?.Invoke(this, e);
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void lLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkClicked?.Invoke(this, e);
        }

        private void NotificationControl_Load(object sender, System.EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(this, Message);
        }

        private void NotificationControl_Paint(object sender, PaintEventArgs e)
        {
            if (sender != this)
                return;

            Debug.Print(((Control)sender).Name + " : " + e.ToString());

            Color borderColor = Color.LightGray;
            Color backgroundColor = SystemColors.Info;
            Color foreColor = Color.FromArgb(36, 36, 36);
            Image img = Resources.info;

            switch (Type)
            {
                case NotificationType.Success:
                    {
                        borderColor = Color.FromArgb(159, 216, 159);
                        backgroundColor = Color.FromArgb(241, 250, 241);
                        img = Resources.Success;
                        break;
                    }
                case NotificationType.Error:
                    {
                        borderColor = Color.FromArgb(238, 172, 178);
                        backgroundColor = Color.FromArgb(253, 243, 244);
                        img = Resources.Error32;
                        break;
                    }
                case NotificationType.Warning:
                    {
                        borderColor = Color.FromArgb(253, 207, 180);
                        backgroundColor = Color.FromArgb(255, 249, 245);
                        img = Resources.warning;
                        break;
                    }
            }

            if (e.ClipRectangle.X == lLabel.Left)
            {
                e.Graphics.FillRectangle(new SolidBrush(backgroundColor), e.ClipRectangle);
                return;
            }

            if (e.ClipRectangle.X == lblClose.Left)
            {
                e.Graphics.FillRectangle(new SolidBrush(backgroundColor), e.ClipRectangle);
                return;
            }

            e.Graphics.FillRoundedRectangle(new SolidBrush(backgroundColor), e.ClipRectangle, 6);
            e.Graphics.DrawRoundedRectangle(new Pen(new SolidBrush(borderColor)), e.ClipRectangle, 6);
            e.Graphics.DrawImage(img, new Rectangle(10, e.ClipRectangle.Height / 2 - 8, 16, 16));

            var size = TextRenderer.MeasureText(Message, Font);

            this.Height = size.Height + 20;

            TextRenderer.DrawText(e.Graphics, Message, Font,
   new Rectangle(36, e.ClipRectangle.Height / 2 - size.Height / 2, e.ClipRectangle.Width - 50 - (btnAction.Visible ? btnAction.Width : 0) - (lLabel.Visible ? lLabel.Width : 0) - (CanBeClosed ? lblClose.Width : 0), e.ClipRectangle.Height), Color.FromArgb(36, 36, 36), Color.Transparent,
   TextFormatFlags.EndEllipsis);
        }

        private void NotificationControl_Resize(object sender, System.EventArgs e)
        {
            Invalidate();
        }
    }
}