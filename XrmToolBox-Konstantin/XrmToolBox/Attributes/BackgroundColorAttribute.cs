using System;
using System.Drawing;

namespace XrmToolBox.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class BackgroundColorAttribute : Attribute
    {
        public string Color { get; private set; }

        public int ColorR { get; private set; }
        public int ColorG { get; private set; }
        public int ColorB { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundColorAttribute" /> class.
        /// </summary>
        /// <param name="color"></param>
        public BackgroundColorAttribute(string color)
        {
            Color = color;

            if (string.IsNullOrEmpty(color))
            {
                ColorR = 230;
                ColorG = 230;
                ColorB = 250;
            }
        }

        public BackgroundColorAttribute(int r, int g, int b)
        {
            ColorR = r;
            ColorG = g;
            ColorB = b;
        }
    }
}
