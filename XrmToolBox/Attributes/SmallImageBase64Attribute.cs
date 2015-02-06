using System;
using System.Drawing;

namespace XrmToolBox.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class SmallImageBase64Attribute : Attribute
    {
        /// <summary>
        /// Gets the name of the entity logical.
        /// </summary>
        /// <value>
        /// The name of the entity logical.
        /// </value>
        public string SmallBase64Image{ get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SmallImageBase64Attribute" /> class.
        /// </summary>
        /// <param name="base64Image"></param>
        public SmallImageBase64Attribute(string base64Image)
        {
            SmallBase64Image = base64Image;
        }
    }
}
