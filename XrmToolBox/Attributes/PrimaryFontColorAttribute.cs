using System;
using System.Drawing;

namespace XrmToolBox.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class PrimaryFontColorAttribute : Attribute
    {
        public string Color { get; private set; }

        public int ColorR { get; private set; }
        public int ColorG { get; private set; }
        public int ColorB { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryFontColorAttribute" /> class.
        /// </summary>
        /// <param name="color"></param>
        public PrimaryFontColorAttribute(string color)
        {
            Color = color;

            if (string.IsNullOrEmpty(color))
            {
                ColorR = 0;
                ColorG = 0;
                ColorB = 0;
            }
        }

        public PrimaryFontColorAttribute(int r, int g, int b)
        {
            ColorR = r;
            ColorG = g;
            ColorB = b;
        }
    }
}
