using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.TempNew.EventArgs;

namespace XrmToolBox.TempNew
{
    public partial class MostRecentlyUsedItemControl : UserControl
    {
        private readonly MostRecentlyUsedItem item;

        public MostRecentlyUsedItemControl(string base64Image, MostRecentlyUsedItem item)
        {
            InitializeComponent();

            this.item = item;

            lblPlugin.Text = item.PluginName;
            lblConnectionName.Text = item.ConnectionName;

            if (!string.IsNullOrEmpty(base64Image))
            {
                var bytes = Convert.FromBase64String(base64Image);
                var ms = new MemoryStream(bytes, 0, bytes.Length);
                ms.Write(bytes, 0, bytes.Length);
                pbLogo.Image = Image.FromStream(ms);
                ms.Close();
            }

            foreach (Control control in Controls)
            {
                control.MouseEnter += OnMouseEnter;
                control.MouseLeave += OnMouseLeave;
                control.Click += OnClick;
            }
        }

        public event EventHandler<OpenMruPluginEventArgs> OpenMruPluginRequested;

        private void OnMouseEnter(object sender, System.EventArgs e)
        {
            pbLogo.BackColor = Color.AliceBlue;
            lblPlugin.BackColor = Color.AliceBlue;
            lblConnectionName.BackColor = Color.AliceBlue;
        }

        private void OnMouseLeave(object sender, System.EventArgs e)
        {
            pbLogo.BackColor = Color.Transparent;
            lblPlugin.BackColor = Color.Transparent;
            lblConnectionName.BackColor = Color.Transparent;
        }

        private void OnClick(object sender, System.EventArgs e)
        {
            OpenMruPluginRequested?.Invoke(this, new OpenMruPluginEventArgs(item));
        }
    }
}