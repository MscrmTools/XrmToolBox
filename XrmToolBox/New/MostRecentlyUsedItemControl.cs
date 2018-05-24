using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.New.EventArgs;
using Timer = System.Timers.Timer;

namespace XrmToolBox.New
{
    public partial class MostRecentlyUsedItemControl : UserControl
    {
        private readonly string base64Image;
        private readonly MostRecentlyUsedItem item;
        private Thread clickThread;

        public MostRecentlyUsedItemControl(string base64Image, MostRecentlyUsedItem item)
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            this.item = item;
            this.base64Image = base64Image;
        }

        public event EventHandler ClearMruRequested;

        public event EventHandler<OpenMruPluginEventArgs> OpenMruPluginRequested;

        public event EventHandler RemoveMruRequested;

        public MostRecentlyUsedItem Item => item;

        private void cmsMru_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsmiRemove)
            {
                RemoveMruRequested?.Invoke(this, new System.EventArgs());
            }
            else if (e.ClickedItem == tsmiRemoveAll)
            {
                ClearMruRequested?.Invoke(this, new System.EventArgs());
            }
        }

        private void MostRecentlyUsedItemControl_Load(object sender, System.EventArgs e)
        {
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

            var tooltiptext = item.Date.ToString();
            toolTip.SetToolTip(lblPlugin, tooltiptext);
            toolTip.SetToolTip(lblConnectionName, tooltiptext);
            toolTip.SetToolTip(pbLogo, tooltiptext);

            var timer = new Timer { Interval = 500 };
            timer.Elapsed += (s, evt) =>
            {
                ((Timer)s).Stop();

                MouseEnter += OnMouseEnter;
                MouseLeave += OnMouseLeave;
                MouseClick += OnMouseClick;
                foreach (Control ctrl in Controls)
                {
                    ctrl.MouseEnter += OnMouseEnter;
                    ctrl.MouseLeave += OnMouseLeave;
                    ctrl.MouseClick += OnMouseClick;
                }
            };
            timer.Start();
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OpenMruPluginRequested?.Invoke(this, new OpenMruPluginEventArgs(item));
            }
        }

        private void OnMouseEnter(object sender, System.EventArgs e)
        {
            pbLogo.BackColor = Color.AliceBlue;
            lblPlugin.BackColor = Color.AliceBlue;
            lblConnectionName.BackColor = Color.AliceBlue;
            BackColor = Color.AliceBlue;
        }

        private void OnMouseLeave(object sender, System.EventArgs e)
        {
            pbLogo.BackColor = Color.Transparent;
            lblPlugin.BackColor = Color.Transparent;
            lblConnectionName.BackColor = Color.Transparent;
            BackColor = Color.Transparent;
        }
    }
}