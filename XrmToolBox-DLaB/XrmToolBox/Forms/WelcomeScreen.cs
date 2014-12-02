// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace XrmToolBox.Forms
{
    public partial class WelcomeScreen : Form
    {
        public WelcomeScreen()
        {
            InitializeComponent();
        }

        public Form MyParentForm { get; set; }

        private void WelcomeScreenLoad(object sender, EventArgs e)
        {
            ManageLicense();

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

        private void ManageLicense()
        {
            try
            {
                var assembly =
                    Assembly.LoadFile(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory +
                                      "\\McTools.StopAdvertisement.dll");
                if (assembly != null)
                {
                    Type type = assembly.GetType("McTools.StopAdvertisement.LicenseManager");
                    if (type != null)
                    {
                        MethodInfo methodInfo = type.GetMethod("IsValid");
                        if (methodInfo != null)
                        {
                            object classInstance = Activator.CreateInstance(type, null);

                            if ((bool) methodInfo.Invoke(classInstance, null))
                            {
                                PropertyInfo userNameInfo = type.GetProperty("UserName");
                                PropertyInfo orgNameInfo = type.GetProperty("OrganizationName");

                                var userName = userNameInfo.GetValue(classInstance, null).ToString();
                                var orgName = orgNameInfo.GetValue(classInstance, null).ToString();

                                lblSupport.Text = string.Format(lblSupport.Text,
                                    userName,
                                    orgName.Length > 0 && orgName != userName ? string.Format(" ({0})", orgName) : "");

                                lblSupport.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
            finally
            {
                //pnlSupport.Visible = false;
                //panel2.Visible = true;
            }
        }

        private void linkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }
    }
}
