using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.TempNew.EventArgs;
using Timer = System.Timers.Timer;

namespace XrmToolBox.TempNew
{
    public partial class MostRecentlyUsedItemControl : UserControl
    {
        private readonly MostRecentlyUsedItem item;
        private readonly string base64Image;
        private Thread clickThread;

        public MostRecentlyUsedItemControl(string base64Image, MostRecentlyUsedItem item)
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            this.item = item;
            this.base64Image = base64Image;
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
                Click += OnClick;

                foreach (Control ctrl in Controls)
                {
                    ctrl.MouseEnter += OnMouseEnter;
                    ctrl.MouseLeave += OnMouseLeave;
                    ctrl.Click += OnClick;
                }
            };
            timer.Start();
        }
    }
}