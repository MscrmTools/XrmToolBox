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

            pnlSupport.Visible = false;
            panel2.Visible = true;
        }

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

                                label7.Text = string.Format(label7.Text,
                                    userName,
                                    orgName.Length > 0 && orgName != userName ? string.Format(" ({0})", orgName) : "");

                                pnlSupport.Visible = true;
                                panel2.Visible = false;
                                //lblSupport.Text = string.Format("Thank you for your support {0}{1}!",
                                //    userName,
                                //    orgName.Length > 0 && orgName != userName ? string.Format(" ({0})", orgName) : "");
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
    }
}
