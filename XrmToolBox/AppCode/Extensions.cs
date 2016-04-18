using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.AppCode
{
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Checks if the current file contains types that implement 
        /// IXrmToolBoxPlugin interface
        /// </summary>
        /// <param name="fi">Current file info</param>
        /// <returns>Value that indicates if the current file contains types that implement 
        /// IXrmToolBoxPlugin interface</returns>
        public static bool ImplementsXrmToolBoxPlugin(this FileInfo fi)
        {
            var assembly = Assembly.LoadFile(fi.FullName);

            foreach (var type in assembly.GetTypes())
            {
                if (type.GetInterfaces().Contains(typeof(IXrmToolBoxPlugin)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Return the assembly version from a file
        /// </summary>
        /// <param name="fi">Current file info</param>
        /// <returns>Assembly version</returns>
        public static Version GetAssemblyVersion(this FileInfo fi)
        {
            var assembly = Assembly.LoadFile(fi.FullName);

           return assembly.GetName().Version;
        }
    }
}
