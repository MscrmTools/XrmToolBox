using System;
using System.Drawing;

namespace XrmToolBox.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class SecondaryFontColorAttribute : Attribute
    {
        public string Color { get; private set; }

        public int ColorR { get; private set; }
        public int ColorG { get; private set; }
        public int ColorB { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecondaryFontColorAttribute" /> class.
        /// </summary>
        /// <param name="color"></param>
        public SecondaryFontColorAttribute(string color)
        {
            Color = color;

            if (string.IsNullOrEmpty(color))
            {
                ColorR = 190;
                ColorG = 190;
                ColorB = 190;
            }
        }

        public SecondaryFontColorAttribute(int r, int g, int b)
        {
            ColorR = r;
            ColorG = g;
            ColorB = b;
        }
    }
}
