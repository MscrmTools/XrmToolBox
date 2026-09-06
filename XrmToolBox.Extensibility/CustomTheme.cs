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
            control.ForeColor = ForeColor1;
            control.BackColor = Background1;

            ThemePluginRules.Apply(control, this);

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

            if (control is ToolStrip toolStrip)
            {
                toolStrip.Renderer = new ToolStripProfessionalRenderer(MenuColorTable);
                UpdateDropdownItemsTheme(toolStrip.Items);
            }

            foreach (Control childControl in control.Controls)
            {
                UpdateControlTree(childControl);
            }
        }

        private void UpdateDropdownItemsTheme(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = ForeColor1;
                item.BackColor = Background1;

                if (item is ToolStripMenuItem menuItem)
                {
                    UpdateDropdownItemsTheme(menuItem.DropDownItems);
                }

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

                if (item is ToolStripDropDownButton dropDownButton)
                {
                    UpdateDropdownItemsTheme(dropDownButton.DropDownItems);
                }
            }
        }
    }
}
