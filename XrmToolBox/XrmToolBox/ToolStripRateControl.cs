using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace XrmToolBox
{
    internal class ToolStripRateControl:ToolStripControlHost
    {
        public ToolStripRateControl(RateControl rateControl)
            : base(rateControl)
        {
            
        }
    }

    internal class RateControl : UserControl
    {
        public RateControl(string sRate)
        {
            Height = 29;
            Width = 80;
            BackColor = Color.Transparent;

            decimal rate;

            if (decimal.TryParse(sRate ?? "0", out rate))
            {

                int i = 0;

                do
                {
                    var p = new PictureBox {Left = i*17, Top = 3, Width = 16, Height = 16};
                    var t = new ToolTip();
                    t.SetToolTip(p, "XrmToolBox latest release rating on CodePlex: " + sRate + "/5\r\n\r\nGo to XrmToolBox CodePlex page to confirm or change this rate");

                    if (rate > i)
                    {
                        if (rate - i >= 1)
                        {
                            using (
                                Stream myStream =
                                    Assembly.GetExecutingAssembly()
                                        .GetManifestResourceStream("XrmToolBox.Images.fullstar.png"))
                            {
                                if (myStream == null) return;
                                p.Image = new Bitmap(myStream);
                            }
                        }
                        else
                        {
                            using (
                                Stream myStream =
                                    Assembly.GetExecutingAssembly()
                                        .GetManifestResourceStream("XrmToolBox.Images.mediumstar.png"))
                            {
                                if (myStream == null) return;
                                p.Image = new Bitmap(myStream);
                            }
                        }
                    }
                    else
                    {
                        using (
                            Stream myStream =
                                Assembly.GetExecutingAssembly()
                                    .GetManifestResourceStream("XrmToolBox.Images.emptystar.png"))
                        {
                            if (myStream == null) return;
                            p.Image = new Bitmap(myStream);
                        }
                    }

                    Controls.Add(p);
                    i++;
                } while (i < 5);
            }
            else
            {
                var l = new Label{ Left = 0, Top = 3, Height = 16, Text = sRate };
                  Controls.Add(l); 
            }
        }
    }
}
