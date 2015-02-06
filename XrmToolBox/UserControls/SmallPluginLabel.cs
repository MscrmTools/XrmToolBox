using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XrmToolBox.UserControls
{
    internal class SmallPluginLabel : Label
    {
        private Color primaryFontColor;
        private Color secondaryFontColor;
        public Color PrimaryFontColor {
            set { primaryFontColor = value; }
        }

        public Color SecondaryFontColor
        {
            set
            {
                secondaryFontColor = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Point drawPoint = new Point(0, 0);

            try
            {
                int indexOfBy = Text.IndexOf("by");
                int indexOfSeparator = Text.IndexOf(" - ", indexOfBy);

                string firstPart = Text.Substring(0, indexOfBy);
                string secondPart = Text.Substring(indexOfBy, indexOfSeparator - indexOfBy);
                string thirdPart = Text.Remove(0, indexOfSeparator);


                Font normalFont = this.Font;

                Font smallFont = new Font(normalFont.FontFamily, normalFont.Size - 4);

                Size normalSize1 = TextRenderer.MeasureText(firstPart, normalFont);
                Size smallSize = TextRenderer.MeasureText(secondPart, smallFont);
                Size normalSize2 = TextRenderer.MeasureText(thirdPart, normalFont);

                Rectangle normalRect1 = new Rectangle(drawPoint, normalSize1);
                Rectangle smallRect = new Rectangle(new Point(normalSize1.Width - 10,5), smallSize);
                Rectangle normalRect2 = new Rectangle(new Point(smallSize.Width + normalSize1.Width - 20, 0), normalSize2);

                TextRenderer.DrawText(e.Graphics, firstPart, normalFont, normalRect1, primaryFontColor); //ForeColor);
                TextRenderer.DrawText(e.Graphics, secondPart, smallFont, smallRect, secondaryFontColor);//Color.Gray);
                TextRenderer.DrawText(e.Graphics, thirdPart, normalFont, normalRect2, primaryFontColor); //ForeColor);
            }
            catch
            {
                TextRenderer.DrawText(e.Graphics, Text, Font, drawPoint, ForeColor);
            }
        }
    }
}