using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace XrmToolBox.AppCode
{
	internal static class ThemeHelpers
	{
		public static void ApplyThemeAndWatch(Control control)
		{
			if (control == null || control.IsDisposed)
			{
				return;
			}

			CustomTheme.Instance.ApplyTheme(control);
			WatchControlTree(control);
		}

		private static void ControlAdded(object sender, ControlEventArgs e)
		{
			ApplyThemeAndWatch(e.Control);
		}

		private static void WatchControlTree(Control control)
		{
			// Removing the handler first makes registration idempotent when the
			// manual reapply button walks a tree that is already being watched.
			control.ControlAdded -= ControlAdded;
			control.ControlAdded += ControlAdded;

			foreach (Control child in control.Controls)
			{
				WatchControlTree(child);
			}
		}
	}
}
