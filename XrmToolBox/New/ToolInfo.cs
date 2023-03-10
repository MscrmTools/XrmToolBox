using System;
using System.Collections.Generic;
using System.Drawing;

namespace XrmToolBox.New
{
    internal class ToolInfo
    {
        public string Author { get; set; }
        public Color BackColor { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public Color Color { get; set; }
        public string Description { get; set; }
        public bool hasPayPal { get; set; }
        public Image Image { get; set; }
        public string Name { get; set; }
        public int NumberOfUse { get; set; }
        public Color SecondaryColor { get; set; }
        public string Version { get; set; }
    }
}