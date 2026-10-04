using System.Drawing;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    public class CustomTheme
    {
        private static CustomTheme theme;

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

        // Semantic colors used by plugin-specific editor rules.
        public Color AttributeColor { get; protected set; }
        public Color CommentColor { get; protected set; }
        public Color KeywordColor { get; protected set; }
        public Color NumberColor { get; protected set; }
        public Color OperatorColor { get; protected set; }
        public Color StringColor { get; protected set; }
        public Color TagColor { get; protected set; }

        public bool IsActive { get; private set; }
        public ProfessionalColorTable MenuColorTable { get; protected set; }

        public void ApplyTheme(Control control)
        {
            if (!IsActive || control == null)
            {
                return;
            }

            UpdateControlTree(control);
        }

        public void SetTheme(CustomTheme customTheme)
        {
            theme = customTheme ?? new CustomTheme();
            theme.IsActive = customTheme != null;
        }

        private void UpdateControlTree(Control control)
        {
            var originalBackColor = control.BackColor;
            control.ForeColor = ForeColor1;
            if (!(control is StatusStrip))
            {
                control.BackColor = Background1;
            }

            // Specialized rules fill the gaps left by standard WinForms colors.
            ThemePluginRules.Apply(control, this, originalBackColor);

            if (control is TextBox || control is ComboBox || control is RichTextBox)
            {
                control.BackColor = Background2;
                control.ForeColor = ForeColor2;
            }
            else if (control is LinkLabel linkLabel)
            {
                linkLabel.ActiveLinkColor = HighlightColor;
                linkLabel.DisabledLinkColor = ForeColor5;
                linkLabel.ForeColor = HighlightColor;
                linkLabel.LinkColor = HighlightColor;
            }
            else if (control is Button button && button.FlatAppearance.BorderSize > 0)
            {
                button.BackColor = Background2;
                button.ForeColor = ForeColor2;
                button.FlatStyle = FlatStyle.Flat;
            }

            if (control is TextBox textBox)
            {
                textBox.BorderStyle = BorderStyle.FixedSingle;
            }

            if (control is RichTextBox richTextBox && richTextBox.ReadOnly)
            {
                richTextBox.BackColor = Background1;
                richTextBox.ForeColor = ForeColor1;
            }

            if (control is TabPage tabPage)
            {
                tabPage.UseVisualStyleBackColor = false;
            }

            if (control is ToolStrip toolStrip)
            {
                ApplyToolStripTheme(toolStrip);
            }

            NativeControlTheme.Apply(control, this);

            foreach (Control childControl in control.Controls)
            {
                UpdateControlTree(childControl);
            }
        }

        private void ApplyToolStripTheme(ToolStrip strip)
        {
            strip.BackColor = strip is StatusStrip ? Background2 : Background1;
            strip.ForeColor = ForeColor2;
            if (!(strip.Renderer is ThemedToolStripRenderer renderer) || !renderer.UsesPalette(this))
            {
                strip.Renderer = new ThemedToolStripRenderer(this);
            }

            // Connection menus rebuild their items on opening, outside ControlAdded.
            strip.ItemAdded -= ToolStripItemAdded;
            strip.ItemAdded += ToolStripItemAdded;
            foreach (ToolStripItem item in strip.Items)
            {
                UpdateDropdownItemTheme(item);
            }
        }

        private static void ToolStripItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (Instance.IsActive)
            {
                Instance.UpdateDropdownItemTheme(e.Item);
            }
        }

        private void UpdateDropdownItemTheme(ToolStripItem item)
        {
            item.ForeColor = ForeColor1;
            item.BackColor = item.Owner is StatusStrip ? Background2 : Background1;

            if (item is ToolStripTextBox textBox)
            {
                textBox.TextBox.BackColor = Background2;
                textBox.TextBox.ForeColor = ForeColor2;
            }

            if (item is ToolStripComboBox comboBox)
            {
                comboBox.ComboBox.BackColor = Background2;
                comboBox.ComboBox.ForeColor = ForeColor2;
            }

            if (item is ToolStripDropDownItem dropDownItem)
            {
                // Accessing DropDown creates it. Theme only the menu being opened,
                // rather than constructing every nested menu during ItemAdded/layout.
                dropDownItem.DropDownOpened -= ToolStripDropDownOpened;
                dropDownItem.DropDownOpened += ToolStripDropDownOpened;
            }
        }

        private static void ToolStripDropDownOpened(object sender, System.EventArgs e)
        {
            if (Instance.IsActive && sender is ToolStripDropDownItem item)
            {
                Instance.ApplyToolStripTheme(item.DropDown);
            }
        }

        private sealed class ThemedToolStripRenderer : ToolStripProfessionalRenderer
        {
            private readonly CustomTheme palette;

            public ThemedToolStripRenderer(CustomTheme palette) : base(palette.MenuColorTable)
            {
                this.palette = palette;
            }

            public bool UsesPalette(CustomTheme candidate)
            {
                return ReferenceEquals(palette, candidate);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = e.Item.Selected || e.Item.Pressed ? palette.ForeColor5 : palette.ForeColor1;
                base.OnRenderItemText(e);
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = e.Item.Enabled ? palette.ForeColor1 : palette.Background5;
                base.OnRenderArrow(e);
            }
        }
    }
}
