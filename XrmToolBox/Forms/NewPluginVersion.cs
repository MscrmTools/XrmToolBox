using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.PluginsStore.DTO;

namespace XrmToolBox.Forms
{
    public partial class NewPluginVersion : Form
    {
        public NewPluginVersion(XtbPlugin plugin)
        {
            InitializeComponent();

            cbbReminder.SelectedIndex = 0;
            lblPluginTitle.Text = plugin.Name;
            lblNewVersion.Text = $"New version: {plugin.Version}";
            rtbReleaseNotes.Text = plugin.LatestReleaseNote;
            rtbReleaseNotes.BackColor = Color.White;
            rtbReleaseNotes.Padding = new Padding(10);
            pbLogo.Load(plugin.LogoUrl ?? "https://raw.githubusercontent.com/wiki/MscrmTools/XrmToolBox/Images/unknown.png");
        }

        public bool IsVersionSkipped { get; private set; }
        public int NumberOfDaysToSkip { get; private set; }

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

            Close();
        }

        private void llUpdateNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void NewPluginVersion_Resize(object sender, System.EventArgs e)
        {
            pbLogo.Location = new Point((Width - pbLogo.Width) / 2, 0);
        }
    }
}