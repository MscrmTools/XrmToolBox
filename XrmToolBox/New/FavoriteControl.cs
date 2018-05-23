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
    public partial class FavoriteControl : UserControl
    {
        private readonly string base64Image;
        private readonly Favorite item;
        private Thread clickThread;

        public FavoriteControl(string base64Image, Favorite item)
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            this.item = item;
            this.base64Image = base64Image;
        }

        public event EventHandler<OpenFavoritePluginEventArgs> OpenFavoritePluginRequested;

        public event EventHandler RemoveFavoriteRequested;

        public Favorite Item => item;

        private void cmsMru_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsmiRemove)
            {
                RemoveFavoriteRequested?.Invoke(this, new System.EventArgs());
            }
        }

        private void MostRecentlyUsedItemControl_Load(object sender, System.EventArgs e)
        {
            lblPlugin.Text = item.PluginName;

            if (!string.IsNullOrEmpty(base64Image))
            {
                var bytes = Convert.FromBase64String(base64Image);
                var ms = new MemoryStream(bytes, 0, bytes.Length);
                ms.Write(bytes, 0, bytes.Length);
                pbLogo.Image = Image.FromStream(ms);
                ms.Close();
            }

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
                OpenFavoritePluginRequested?.Invoke(this, new OpenFavoritePluginEventArgs(item));
            }
        }

        private void OnMouseEnter(object sender, System.EventArgs e)
        {
            pbLogo.BackColor = Color.AliceBlue;
            lblPlugin.BackColor = Color.AliceBlue;
            BackColor = Color.AliceBlue;
        }

        private void OnMouseLeave(object sender, System.EventArgs e)
        {
            pbLogo.BackColor = Color.Transparent;
            lblPlugin.BackColor = Color.Transparent;
            BackColor = Color.Transparent;
        }
    }
}