using System;
using System.Drawing;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    public class DarkProfessionalColors : ProfessionalColorTable
    {
        private Color Background;
        private Color LightColor;
        private Color MenuBackground;
        private Color MenuItemBackground;
        private Color MenuItemSelectedBackground;

        public DarkProfessionalColors(CustomTheme theme)
        {
            Background = theme.Background1;
            MenuBackground = theme.Background1;
            MenuItemBackground = theme.Background1;
            MenuItemSelectedBackground = theme.HighlightColor;
            LightColor = theme.ForeColor1;
        }

        public override Color ButtonCheckedGradientBegin => MenuBackground;
        public override Color ButtonCheckedGradientEnd => MenuBackground;
        public override Color ButtonCheckedGradientMiddle => MenuBackground;
        public override Color ButtonCheckedHighlight => MenuBackground;
        public override Color ButtonCheckedHighlightBorder => MenuBackground;
        public override Color ButtonPressedBorder => MenuBackground;
        public override Color ButtonPressedGradientBegin => MenuBackground;
        public override Color ButtonPressedGradientEnd => MenuBackground;
        public override Color ButtonPressedGradientMiddle => MenuBackground;
        public override Color ButtonPressedHighlight => MenuBackground;
        public override Color ButtonPressedHighlightBorder => MenuBackground;
        public override Color ButtonSelectedBorder => MenuBackground;
        public override Color ButtonSelectedGradientBegin => MenuBackground;
        public override Color ButtonSelectedGradientEnd => MenuBackground;
        public override Color ButtonSelectedGradientMiddle => MenuBackground;
        public override Color ButtonSelectedHighlight => MenuBackground;
        public override Color ButtonSelectedHighlightBorder => MenuBackground;
        public override Color CheckBackground => MenuBackground;
        public override Color CheckPressedBackground => MenuBackground;
        public override Color CheckSelectedBackground => MenuBackground;
        public override Color GripDark => MenuBackground;
        public override Color GripLight => MenuBackground;
        public override Color ImageMarginGradientBegin => MenuItemBackground;

        public override Color ImageMarginGradientEnd => MenuItemBackground;

        public override Color ImageMarginGradientMiddle => MenuItemBackground;

        public override Color ImageMarginRevealedGradientBegin => MenuItemBackground;

        public override Color ImageMarginRevealedGradientEnd => MenuItemBackground;

        public override Color ImageMarginRevealedGradientMiddle => MenuItemBackground;

        public override Color MenuBorder => MenuItemBackground;

        public override Color MenuItemBorder => MenuItemBackground;

        public override Color MenuItemPressedGradientBegin => MenuItemBackground;

        public override Color MenuItemPressedGradientEnd => MenuItemBackground;

        public override Color MenuItemPressedGradientMiddle => MenuItemBackground;

        public override Color MenuItemSelected => MenuItemSelectedBackground;

        public override Color MenuItemSelectedGradientBegin => MenuItemBackground;

        public override Color MenuItemSelectedGradientEnd => MenuItemBackground;

        public override Color MenuStripGradientBegin => MenuItemBackground;

        public override Color MenuStripGradientEnd => MenuItemBackground;

        public override Color OverflowButtonGradientBegin => MenuBackground;
        public override Color OverflowButtonGradientEnd => MenuBackground;
        public override Color OverflowButtonGradientMiddle => MenuBackground;
        public override Color RaftingContainerGradientBegin => MenuBackground;
        public override Color RaftingContainerGradientEnd => MenuBackground;
        public override Color SeparatorDark => LightColor;
        public override Color SeparatorLight => MenuBackground;
        public override Color StatusStripGradientBegin => MenuBackground;
        public override Color StatusStripGradientEnd => MenuBackground;
        public override Color ToolStripBorder => Background;

        public override Color ToolStripContentPanelGradientBegin => MenuBackground;

        public override Color ToolStripContentPanelGradientEnd => MenuBackground;

        public override Color ToolStripDropDownBackground => MenuItemBackground;

        public override Color ToolStripGradientBegin => MenuBackground;

        public override Color ToolStripGradientEnd => MenuBackground;

        public override Color ToolStripGradientMiddle => MenuBackground;

        public override Color ToolStripPanelGradientBegin => MenuBackground;
        public override Color ToolStripPanelGradientEnd => MenuBackground;
    }

    public class DarkTheme : CustomTheme
    {
        public DarkTheme()
        {
            Background1 = ColorTranslator.FromHtml("#212121");
            Background2 = ColorTranslator.FromHtml("#424242");
            Background3 = ColorTranslator.FromHtml("#616161");
            Background4 = ColorTranslator.FromHtml("#757575");
            Background5 = ColorTranslator.FromHtml("#9E9E9E");

            ForeColor1 = ColorTranslator.FromHtml("#BDBDBD");
            ForeColor2 = ColorTranslator.FromHtml("#E0E0E0");
            ForeColor3 = ColorTranslator.FromHtml("#EEEEEE");
            ForeColor4 = ColorTranslator.FromHtml("#F5F5F5");
            ForeColor5 = ColorTranslator.FromHtml("#FAFAFA");

            HighlightColor = ColorTranslator.FromHtml("#007ACC");

            MenuColorTable = new DarkProfessionalColors(this);
        }
    }
}