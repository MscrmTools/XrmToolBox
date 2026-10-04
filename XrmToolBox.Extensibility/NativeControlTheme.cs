using ScintillaNET;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    /// <summary>
    /// Applies Windows-native styling to elements such as scrollbars that do not
    /// respond to WinForms BackColor and ForeColor. Keeps native API calls and
    /// window-handle lifecycle management separate from ordinary control styling,
    /// reapplying the theme when handles are recreated without plugin-specific rules.
    /// </summary>
    internal static class NativeControlTheme
    {
        private static readonly ConditionalWeakTable<Control, WindowThemeState> States =
            new ConditionalWeakTable<Control, WindowThemeState>();

        public static void Apply(Control control, CustomTheme theme)
        {
            if (!(control is ScrollBar || control is Scintilla || control is ScrollableControl ||
                control is TextBoxBase || control is TreeView || control is ListView ||
                control is ListBox || control is MdiClient))
            {
                return;
            }

            control.HandleCreated -= HandleCreated;
            control.HandleCreated += HandleCreated;
            control.HandleDestroyed -= HandleDestroyed;
            control.HandleDestroyed += HandleDestroyed;
            ApplyToHandle(control, theme);
        }

        private static void HandleCreated(object sender, EventArgs e)
        {
            ApplyToHandle((Control)sender, CustomTheme.Instance);
        }

        private static void HandleDestroyed(object sender, EventArgs e)
        {
            States.Remove((Control)sender);
        }

        private static void ApplyToHandle(Control control, CustomTheme theme)
        {
            // Do not create HWNDs while walking a plugin's partially built control tree.
            if (!control.IsHandleCreated || control.IsDisposed)
            {
                return;
            }

            var dark = theme.IsActive && theme.Background1.GetBrightness() < 0.5f &&
                !SystemInformation.HighContrast;
            var state = States.GetValue(control, _ => new WindowThemeState());
            if (state.Handle == control.Handle && state.Dark == dark)
            {
                return;
            }

            // SetWindowTheme sends WM_THEMECHANGED synchronously. Record the state first.
            state.Handle = control.Handle;
            state.Dark = dark;
            var scrollbarOnly = control is ScrollBar || control is Scintilla ||
                control is ScrollableControl || control is MdiClient;
            try
            {
                // Windows supplies the native scrollbar artwork; older visual styles
                // may not provide DarkMode_Explorer and keep their existing appearance.
                SetWindowTheme(control.Handle, dark ? "DarkMode_Explorer" : null,
                    dark && scrollbarOnly ? "ScrollBar" : null);
            }
            catch (DllNotFoundException) { }
            catch (EntryPointNotFoundException) { }
        }

        private sealed class WindowThemeState
        {
            public IntPtr Handle;
            public bool Dark;
        }

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int SetWindowTheme(IntPtr window, string application, string classes);
    }
}
