using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using Timer = System.Windows.Forms.Timer;

namespace XrmToolBox.Forms
{
    public partial class WelcomeDialog : Form
    {
        private static WelcomeDialog innerForm;
        private static Thread thread;
        private static string currentStatus;
        private static Timer timer;

        #region Static methods to control Splashscreen

        public static void ShowForm()
        {
            innerForm = new WelcomeDialog();
            Application.Run(innerForm);
        }

        public static void CloseForm()
        {
            if (innerForm.InvokeRequired)
            {
                innerForm.Invoke(new Action(() => { innerForm.Close(); }));
            }
            else
            {
                innerForm.Close();
            }
            timer.Stop();

            thread = null;
            innerForm = null;
        }

        public static void ShowSplashScreen()
        {
            // Make sure it is only launched once.
            if (innerForm != null)
                return;
            thread = new Thread(ShowForm) { IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        // A static method to set the status.
        public static void SetStatus(string newStatus)
        {
            if (innerForm == null)
                return;

            currentStatus = newStatus;
        }

        #endregion Static methods to control Splashscreen

        public WelcomeDialog(bool closeWindow = true)
        {
            InitializeComponent();

            // Set drawing optimizations
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            lblVersion.Text = $@"version: {version}";

            timer = new Timer { Interval = 100 };
            timer.Tick += (sender, e) =>
            {
                lblWorkingState.Text = currentStatus;
            };
            timer.Start();

            ManageLicense();

            if (!closeWindow)
            {
                linkClose.Visible = true;
                lblWorkingState.Text = string.Empty;
                lblWorkingState.Visible = false;
            }
        }

        private void linkClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ManageLicense()
        {
            try
            {
                var stopAdvertisementLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "McTools.StopAdvertisement.dll");

                if (File.Exists(stopAdvertisementLocation))
                {
                    var type = Assembly.LoadFile(stopAdvertisementLocation).GetType("McTools.StopAdvertisement.LicenseManager");
                    if (type == null)
                    {
                        return;
                    }

                    var methodInfo = type.GetMethod("IsValid");
                    if (methodInfo == null) { return; }

                    object classInstance = Activator.CreateInstance(type, null);

                    if ((bool)methodInfo.Invoke(classInstance, null))
                    {
                        var userNameInfo = type.GetProperty("UserName");
                        var orgNameInfo = type.GetProperty("OrganizationName");

                        var userName = userNameInfo.GetValue(classInstance, null).ToString();
                        var orgName = orgNameInfo.GetValue(classInstance, null).ToString();

                        lblSupport.Text = string.Format(lblSupport.Text,
                            userName,
                            orgName.Length > 0 && orgName != userName ? string.Format(" ({0})", orgName) : "");

                        pnlSupport.Visible = true;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                // Nothing to do as if the file is missing, it's not big deal
            }
            catch (NotSupportedException)
            {
                MessageBox.Show(this,
                    "It seems you maybe forgot to unblock XrmToolBox.zip before extracting it. XrmToolBox can't work as expected until you unblocked all files. To do so, display XrmToolBox.zip properties and unblock the file before extracting it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}