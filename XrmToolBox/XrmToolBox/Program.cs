// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Reflection;
using System.Windows.Forms;

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
                MessageBox.Show("An unexpected error occured: " + error, "Error", MessageBoxButtons.OK,
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
                        "Unable to find Windows Identity Foundation 3.5 on this computer\r\n\r\nThis program may not work properly.\r\n\r\nDo you want to continue?",
                        "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
