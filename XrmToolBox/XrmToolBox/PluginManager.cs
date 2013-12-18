// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace XrmToolBox
{
    public class PluginManager
    {
        #region Variables

        /// <summary>
        /// List of loaded plugins
        /// </summary>
        public List<Type> Plugins { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class PluginManager
        /// </summary>
        public PluginManager()
        {
            Plugins = new List<Type>();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Loads plugins
        /// <remarks>
        /// Only assemblies located in the same directory than this program are loaded
        /// </remarks>
        /// </summary>
        public void LoadPlugins()
        {
            var directoryInfo = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

            if (directoryInfo != null)
            {
                var files = Directory.GetFileSystemEntries(directoryInfo.FullName, "*.dll");

                foreach (var file in files)
                {
                    try
                    {
                        var assembly = Assembly.UnsafeLoadFrom(file);

                        foreach (var type in assembly.GetTypes())
                        {
                            if (type.IsPublic)
                            {
                                if ((type.Attributes & TypeAttributes.Abstract) != TypeAttributes.Abstract)
                                {
                                    var iConnector = type.GetInterface("IMsCrmToolsPluginUserControl", true);

                                    if (iConnector != null)
                                    {
                                        try
                                        {
                                            Plugins.Add(type);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Create an instance of the class included in the plugin
        /// </summary>
        /// <param name="assemblyPath">Path of the assembly</param>
        /// <param name="fullName">Fullname of the type to instanciate</param>
        /// <param name="args">Arguments to pass to the constructor</param>
        /// <returns>Instanciated object</returns>
        public static object CreateInstance(string assemblyPath, string fullName, object[] args = null)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);

            return assembly.CreateInstance(fullName, false, BindingFlags.CreateInstance, null, args, null, null);
        }

        #endregion Methods
    }
}
