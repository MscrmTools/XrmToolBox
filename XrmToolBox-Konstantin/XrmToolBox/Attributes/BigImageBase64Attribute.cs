using System;
using System.Drawing;

namespace XrmToolBox.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class BigImageBase64Attribute : Attribute
    {
        /// <summary>
        /// Gets the name of the entity logical.
        /// </summary>
        /// <value>
        /// The name of the entity logical.
        /// </value>
        public string BigBase64Image{ get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BigImageBase64Attribute" /> class.
        /// </summary>
        /// <param name="base64Image"></param>
        public BigImageBase64Attribute(string base64Image)
        {
            BigBase64Image = base64Image;
        }
    }
}
