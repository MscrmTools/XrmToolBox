using ScintillaNET;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    public class CustomTheme
    {
        private static CustomTheme theme;

        public CustomTheme()
        {
        }

        public static CustomTheme Instance
        {
            get
            {
                if (theme == null)
                {
                    theme = new CustomTheme();
                }
                return theme;
            }
        }

        public Color Background1 { get; protected set; }
        public Color Background2 { get; protected set; }
        public Color Background3 { get; protected set; }
        public Color Background4 { get; protected set; }
        public Color Background5 { get; protected set; }
        public Color ForeColor1 { get; protected set; }
        public Color ForeColor2 { get; protected set; }
        public Color ForeColor3 { get; protected set; }
        public Color ForeColor4 { get; protected set; }
        public Color ForeColor5 { get; protected set; }
        public Color HighlightColor { get; protected set; }
        public bool IsActive { get; private set; }
        public ProfessionalColorTable MenuColorTable { get; protected set; }

        public void ApplyTheme(Control control)
        {
            if (!IsActive) return;

            UpdateChildControlsTheme(control);

            foreach (var ts in control.Controls.OfType<ToolStrip>())
            {
                ts.Renderer = new ToolStripProfessionalRenderer(MenuColorTable);

                UpdateDropdownItemsTheme(ts.Items);
            }
        }

        public void SetTheme(CustomTheme customTheme)
        {
            theme = customTheme;
            theme.IsActive = customTheme != null;
        }

        private void UpdateChildControlsTheme(Control control)
        {
            control.ForeColor = ForeColor1;
            control.BackColor = Background1;

            //Update plugins that have custom controls + extra logic
            UpdateSql4CDSTheme(control);
            UpdateFetchXmlBuilderTheme(control);
            UpdatePluginRegistrationTheme(control);
            //TODO - support more plugins here...

            if (control is TextBox
                || control is ComboBox
                || control is RichTextBox
                )
            {
                control.BackColor = Background2;
                control.ForeColor = ForeColor2;
            }
            else if (control is LinkLabel ll)
            {
                ll.ActiveLinkColor = HighlightColor;
                ll.DisabledLinkColor = ForeColor5;
                ll.ForeColor = HighlightColor;
                ll.LinkColor = HighlightColor;
            }
            else if (control is Button b)
            {
                if (b.FlatAppearance.BorderSize > 0)
                {
                    b.BackColor = Background2;
                    b.ForeColor = ForeColor2;
                    b.FlatStyle = FlatStyle.Flat;
                }
            }

            if (control is TextBox tb)
            {
                tb.BorderStyle = BorderStyle.FixedSingle;
            }

            if (control is RichTextBox rtb && rtb.ReadOnly)
            {
                control.BackColor = Background1;
                control.ForeColor = ForeColor1;
            }

            foreach (Control childControl in control.Controls)
            {
                UpdateChildControlsTheme(childControl);
            }
        }

        private void UpdateDropdownItemsTheme(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = ForeColor1;
                item.BackColor = Background1;

                if (item is ToolStripMenuItem tsmi)
                {
                    UpdateDropdownItemsTheme(tsmi.DropDownItems);
                }

                if (item is ToolStripTextBox tstb)
                {
                    tstb.TextBox.BackColor = Background2;
                    tstb.TextBox.ForeColor = ForeColor2;
                }

                if (item is ToolStripComboBox tstc)
                {
                    tstc.ComboBox.BackColor = Background2;
                    tstc.ComboBox.ForeColor = ForeColor2;
                }

                if (item is ToolStripDropDownButton tsddb)
                {
                    UpdateDropdownItemsTheme(tsddb.DropDownItems);
                }
            }
        }
        //TODO: Use more properties from subclass (ex: 'DarkTheme')
        private void UpdateSql4CDSTheme(Control control)
        {
            //No need to update theme if not in dark mode.
            //TODO: More checks for other types of themes?
            if (GetType() != typeof(DarkTheme))
            {
                return;
            }

            //Theme main editor
            if (control is Scintilla scintilla)
            {
                var darkGrey = Background1;
                var lightGrey = Color.FromArgb(255, 170, 170, 170);
                var lightGreen = Color.FromArgb(255, 84, 198, 82);
                var lightRed = Color.FromArgb(255, 235, 117, 120);
                var lightPurple = Color.FromArgb(255, 200, 120, 255);
                var lightBlueGreen = Color.FromArgb(255, 127, 219, 255);
                var orange = Color.FromArgb(255, 214, 93, 14);

                scintilla.StyleResetDefault();

                scintilla.CaretForeColor = Color.White;
                scintilla.CaretLineBackColor = darkGrey;

                scintilla.Styles[Style.Default].BackColor = darkGrey;
                scintilla.Styles[Style.Default].ForeColor = darkGrey;

                scintilla.Styles[Style.LineNumber].ForeColor = lightGrey;
                scintilla.Styles[Style.LineNumber].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.Default].BackColor = darkGrey;
                scintilla.Styles[Style.Sql.Default].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.Comment].ForeColor = lightGreen;
                scintilla.Styles[Style.Sql.Comment].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.CommentLine].ForeColor = lightGreen;
                scintilla.Styles[Style.Sql.CommentLine].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.CommentLineDoc].ForeColor = lightGreen;
                scintilla.Styles[Style.Sql.CommentLineDoc].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.Number].ForeColor = lightRed;
                scintilla.Styles[Style.Sql.Number].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.Word].ForeColor = lightPurple;
                scintilla.Styles[Style.Sql.Word].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.Word2].ForeColor = lightPurple;
                scintilla.Styles[Style.Sql.Word2].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.Identifier].ForeColor = Color.White;
                scintilla.Styles[Style.Sql.Identifier].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.User1].ForeColor = lightGrey;
                scintilla.Styles[Style.Sql.User1].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.User2].ForeColor = lightBlueGreen;
                scintilla.Styles[Style.Sql.User2].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.String].ForeColor = orange;
                scintilla.Styles[Style.Sql.String].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.Character].ForeColor = orange;
                scintilla.Styles[Style.Sql.Character].BackColor = darkGrey;

                scintilla.Styles[Style.Sql.Operator].ForeColor = lightBlueGreen;
                scintilla.Styles[Style.Sql.Operator].BackColor = darkGrey;
            }

            //TODO: Theme result set window
        }
        private void UpdateFetchXmlBuilderTheme(Control control)
        {
            //TODO
        }
        private void UpdatePluginRegistrationTheme(Control control)
        {
            //TODO
        }
    }
}