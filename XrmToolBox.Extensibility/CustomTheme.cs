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
    }
}