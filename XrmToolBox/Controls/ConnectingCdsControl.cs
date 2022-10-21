using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.Properties;

namespace XrmToolBox.Controls
{
    public partial class ConnectingCdsControl : UserControl
    {
        private int currentFrame = 0;

        private Thread pictureThread;

        private System.Timers.Timer t;

        public ConnectingCdsControl()
        {
            AutoScaleMode = AutoScaleMode.Font;

            InitializeComponent();
        }

        private void AnimatePicture()
        {
            t = new System.Timers.Timer(300);
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void ConnectingCdsControl_Load(object sender, System.EventArgs e)
        {
            pictureThread = new Thread(AnimatePicture);
            pictureThread.Start();
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Image newImage = null;
            switch (currentFrame)
            {
                case 0:
                    newImage = Resources.loading_dv1;
                    currentFrame++;
                    break;

                case 1:
                    newImage = Resources.loading_dv2;
                    currentFrame++;
                    break;

                case 2:
                    newImage = Resources.loading_dv3;
                    currentFrame++;
                    break;

                case 3:
                    newImage = Resources.loading_dv4;
                    currentFrame++;
                    break;

                case 4:
                    newImage = Resources.loading_dv0;
                    currentFrame = 0;
                    break;
            }

            Invoke(new Action(() =>
            {
                pbConnectionLoading.Image = newImage;
            }));
        }
    }
}