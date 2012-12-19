// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Windows.Forms;

namespace XrmToolBox.Forms
{
    public partial class WelcomeScreen : Form
    {
        public WelcomeScreen()
        {
            InitializeComponent();
        }

        private void WelcomeScreenLoad(object sender, EventArgs e)
        {
            var timer = new Timer();
            timer.Tick += TimerTick;
            timer.Interval = 3000;
            timer.Start();
        }

        void TimerTick(object sender, EventArgs e)
        {
            ((Timer) sender).Stop();
            Close();
        }
    }
}
