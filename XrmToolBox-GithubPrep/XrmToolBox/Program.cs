// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace XrmToolBox
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (!CheckRequiredAssemblies())
                {
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception error)
            {
                const string lockedMessage = "One reason can be that at least one file is locked by Windows. Please unlock each locked files or unlock XrmToolBox.zip before extracting its content";
                MessageBox.Show("An unexpected error occured: " + error + "\r\n\r\n" + lockedMessage, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        static bool CheckRequiredAssemblies()
        {
            try
            {
                Assembly.Load(
                    "Microsoft.IdentityModel, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35");
                return true;
            }
            catch
            {
                if (
                    MessageBox.Show(
                        "Unable to find Windows Identity Foundation 3.5 on this computer\r\n\r\nThis program cannot work properly.\r\n\r\nDo you want to display information on how to install this required component on your computer?",
                        "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    if (Environment.OSVersion.Version.Major > 6 ||
                        Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor > 1)
                    {
                        Process.Start("http://blogs.technet.com/b/tvishnun1/archive/2012/06/12/windows-identity-foundation-in-windows-8.aspx");
                    }
                    else
                    {
                        Process.Start("http://support.microsoft.com/kb/974405");
                    }
                }

                return false;
            }
        }
    }
}
