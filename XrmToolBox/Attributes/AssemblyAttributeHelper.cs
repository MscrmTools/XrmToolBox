using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace XrmToolBox.Attributes
{
    class AssemblyAttributeHelper
    {
        public static string GetStringAttributeValue(Assembly assembly, string attributeName)
        {
            foreach (Attribute untypedAttribute in assembly.GetCustomAttributes(false))
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(untypedAttribute))
                {
                    if (property.Name == attributeName)
                    {
                        return property.GetValue(untypedAttribute).ToString();
                    }
                }
            }

            return string.Empty; 
        }

        public static Color GetColor(Assembly assembly, Type type)
        {
            string colorName = string.Empty;
            int colorR = 0;
            int colorG = 0;
            int colorB = 0;

            if (type == typeof (BackgroundColorAttribute))
            {
                colorR = 230;
                colorG = 230;
                colorB = 250;
            }

            if (type == typeof(SecondaryFontColorAttribute))
            {
                colorR = 190;
                colorG = 190;
                colorB = 190;
            }

            foreach (Attribute untypedAttribute in assembly.GetCustomAttributes(type, false))
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(untypedAttribute))
                {
                    switch (property.Name)
                    {
                        case "Color":
                            colorName = property.GetValue(untypedAttribute) != null ? property.GetValue(untypedAttribute).ToString() : "";
                            break;
                        case "ColorR":
                            colorR = (int)property.GetValue(untypedAttribute);
                            break;
                        case "ColorG":
                            colorG = (int)property.GetValue(untypedAttribute);
                            break;
                        case "ColorB":
                            colorB = (int)property.GetValue(untypedAttribute);
                            break;
                    }
                }
            }

            if (string.IsNullOrEmpty(colorName))
            {
                return Color.FromArgb(colorR, colorG, colorB);
            }

            return Color.FromName(colorName);
        }
    }
}
