using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
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
			if (control is DockPanel panel && CustomTheme.Instance.IsActive)
			{
				ApplyDockPalette(panel, CustomTheme.Instance);
			}

			// Removing the handler first makes registration idempotent when the
			// manual reapply button walks a tree that is already being watched.
			control.ControlAdded -= ControlAdded;
			control.ControlAdded += ControlAdded;

			foreach (Control child in control.Controls)
			{
				WatchControlTree(child);
			}
		}

		private static void ApplyDockPalette(DockPanel panel, CustomTheme theme)
		{
			var colors = panel.Theme?.ColorPalette;
			if (colors == null)
			{
				return;
			}

			// Existing panes can be live and docked. Update their palette without
			// replacing the theme's factories or rebuilding the docking layout.
			panel.DockBackColor = theme.Background1;
			colors.MainWindowActive.Background = theme.Background1;
			colors.ToolWindowBorder = theme.Background3;
			colors.ToolWindowSeparator = theme.Background2;
			colors.TabUnselected.Background = theme.Background1;
			colors.TabUnselected.Text = theme.ForeColor1;
			colors.TabSelectedActive.Background = theme.HighlightColor;
			colors.TabSelectedActive.Text = theme.ForeColor5;
			colors.TabSelectedActive.Button = theme.ForeColor5;
			colors.TabSelectedInactive.Background = theme.Background2;
			colors.TabSelectedInactive.Text = theme.ForeColor2;
			colors.TabSelectedInactive.Button = theme.ForeColor2;
			colors.TabUnselectedHovered.Background = theme.Background3;
			colors.TabUnselectedHovered.Text = theme.ForeColor5;
			colors.TabUnselectedHovered.Button = theme.ForeColor5;
			colors.ToolWindowCaptionActive.Background = theme.HighlightColor;
			colors.ToolWindowCaptionActive.Text = theme.ForeColor5;
			colors.ToolWindowCaptionActive.Button = theme.ForeColor5;
			colors.ToolWindowCaptionInactive.Background = theme.Background2;
			colors.ToolWindowCaptionInactive.Text = theme.ForeColor2;
			colors.ToolWindowCaptionInactive.Button = theme.ForeColor2;
			colors.ToolWindowTabSelectedActive.Background = theme.Background2;
			colors.ToolWindowTabSelectedActive.Text = theme.ForeColor5;
			colors.ToolWindowTabSelectedInactive.Background = theme.Background2;
			colors.ToolWindowTabSelectedInactive.Text = theme.ForeColor2;
			colors.ToolWindowTabUnselected.Background = theme.Background1;
			colors.ToolWindowTabUnselected.Text = theme.ForeColor1;
			colors.ToolWindowTabUnselectedHovered.Background = theme.Background3;
			colors.ToolWindowTabUnselectedHovered.Text = theme.ForeColor5;
			colors.AutoHideStripDefault.Background = theme.Background1;
			colors.AutoHideStripDefault.Text = theme.ForeColor1;
			colors.AutoHideStripDefault.Border = theme.Background3;
			colors.AutoHideStripHovered.Background = theme.Background2;
			colors.AutoHideStripHovered.Text = theme.ForeColor5;
			colors.AutoHideStripHovered.Border = theme.HighlightColor;
			panel.Invalidate(true);
		}
	}
}
