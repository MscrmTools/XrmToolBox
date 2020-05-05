using System.Drawing;
using System.Windows.Forms;

namespace XrmToolBox.Forms
{
    public partial class NewConnectionVersion : Form
    {
        public NewConnectionVersion(string version, string releaseNotes)
        {
            InitializeComponent();

            cbbReminder.SelectedIndex = 0;
            lblNewVersion.Text = $@"New version: {version}";
            rtbReleaseNotes.Text = releaseNotes;
            rtbReleaseNotes.BackColor = Color.White;
            rtbReleaseNotes.Padding = new Padding(10);
        }

        public bool IsVersionSkipped { get; private set; }
        public int NumberOfDaysToSkip { get; private set; }
        public bool OnNextRestart { get; private set; }

        private void llDoNotUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (cbbReminder.SelectedIndex)
            {
                case 0:
                    NumberOfDaysToSkip = 1;
                    break;

                case 1:
                    NumberOfDaysToSkip = 2;
                    break;

                case 2:
                    NumberOfDaysToSkip = 3;
                    break;

                case 3:
                    IsVersionSkipped = true;
                    break;
            }

            DialogResult = DialogResult.No;
            Close();
        }

        private void llDoNotUpdate_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle(0, 0, ((LinkLabel)sender).Width - 2, ((LinkLabel)sender).Height - 2));
        }

        private void llUpdateNextStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult = DialogResult.Yes;
            OnNextRestart = true;
            Close();
        }

        private void llUpdateNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void llUpdateNow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green), new Rectangle(0, 0, ((LinkLabel)sender).Width - 2, ((LinkLabel)sender).Height - 2));
        }

        private void NewPluginVersion_Resize(object sender, System.EventArgs e)
        {
            pbLogo.Location = new Point((Width - pbLogo.Width) / 2, 0);
        }

        private void rtbReleaseNotes_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}