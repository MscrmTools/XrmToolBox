using System.Diagnostics;
using System.Windows.Forms;

namespace XrmToolBox.Announcement
{
    public partial class AnnouncementDialog : Form
    {
        private readonly AnnouncementItem item;

        public AnnouncementDialog(AnnouncementItem item)
        {
            InitializeComponent();

            try
            {
                this.item = item;
                int titleHeight = RectangleToScreen(ClientRectangle).Top - Top;

                var image = item.Image;
                Height = image.Height + pnlFooter.Height + titleHeight;
                Width = image.Width;

                pbImage.Image = image;
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;

                if (AnnouncementSettings.Instance.LastShown.Contains(item.Id))
                {
                    AnnouncementSettings.Instance.LastShown.Remove(item.Id);
                }

                AnnouncementSettings.Instance.LastShown.Add(item.Id);
            }
            catch
            {
                IsInvalid = true;
            }
        }

        public bool IsInvalid { get; }

        private void llForget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!AnnouncementSettings.Instance.ToHide.Contains(item.Id))
                AnnouncementSettings.Instance.ToHide.Add(item.Id);
            AnnouncementSettings.Instance.Save();
            Close();
        }

        private void llMoreInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(item.Url);
            Close();
        }

        private void pbImage_Click(object sender, System.EventArgs e)
        {
            Process.Start(item.Url);
            Close();
        }
    }
}