using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;
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

            OnMouseLeave(this, new System.EventArgs());
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
            else if (e.ClickedItem == tsmiOpenWithConnection)
            {
                OpenFavoritePluginRequested?.Invoke(this, new OpenFavoritePluginEventArgs(item, true));
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
            if (CustomTheme.Instance.IsActive)
            {
                pbLogo.BackColor = CustomTheme.Instance.HighlightColor;
                lblPlugin.BackColor = CustomTheme.Instance.HighlightColor;
                BackColor = CustomTheme.Instance.HighlightColor;
                lblConnectionName.BackColor = CustomTheme.Instance.HighlightColor;
                lblConnectionName.ForeColor = CustomTheme.Instance.ForeColor5;
                lblPlugin.ForeColor = CustomTheme.Instance.ForeColor5;
            }
            else
            {
                lblConnectionName.BackColor = Color.AliceBlue;
                pbLogo.BackColor = Color.AliceBlue;
                lblPlugin.BackColor = Color.AliceBlue;
                BackColor = Color.AliceBlue;
                lblConnectionName.ForeColor = Color.Gray;
                lblPlugin.ForeColor = Color.Black;
            }
        }

        private void OnMouseLeave(object sender, System.EventArgs e)
        {
            pbLogo.BackColor = Color.Transparent;
            lblPlugin.BackColor = Color.Transparent;
            BackColor = Color.Transparent;
            lblConnectionName.BackColor = Color.Transparent;

            if (CustomTheme.Instance.IsActive)
            {
                lblConnectionName.ForeColor = CustomTheme.Instance.ForeColor1;
                lblPlugin.ForeColor = CustomTheme.Instance.ForeColor3;
            }
            else
            {
                lblConnectionName.ForeColor = Color.Gray;
                lblPlugin.ForeColor = SystemColors.ControlText;
            }
        }
    }
}