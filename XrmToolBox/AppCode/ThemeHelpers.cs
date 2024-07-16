using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace XrmToolBox.AppCode
{
	internal static class ThemeHelpers
	{
        static void RecursiveThemeCallback(object sender, ControlEventArgs e)
		{
			Debug.WriteLine($"recursive theme called: {DateTime.Now}");

			CustomTheme.Instance.ApplyTheme(e.Control);

			e.Control.ControlAdded += RecursiveThemeCallback;

			foreach (Control c in e.Control.Controls)
			{
				c.ControlAdded += RecursiveThemeCallback;
			}
		}
		static void RecursiveThemeAddCallback(Control control)
		{
			foreach (Control c in control.Controls)
			{
				c.ControlAdded += RecursiveThemeCallback;

				RecursiveThemeAddCallback(c);
			}
		}
        public static void ApplyThemeCallbacks(List<Control> controls, Control page)
        {
            foreach (Control c in controls)
			{
				c.ControlAdded += RecursiveThemeCallback;

				RecursiveThemeAddCallback(c);
			}

            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                page.Invoke((MethodInvoker)delegate
                {
					var updatedControls = page.Controls;
                    foreach (Control c in updatedControls )
                    {
                        c.ControlAdded += RecursiveThemeCallback;

                        RecursiveThemeAddCallback(c);
                    }
                });
            });
        }
	}
}
