using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XrmToolBox
{
    /// <summary>
    /// Class Attribute that defines that this type should not be displayed
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class IgnorePluginAttribute : Attribute
    {

    }
}
