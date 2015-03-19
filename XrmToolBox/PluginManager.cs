// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XrmToolBox
{
    public class PluginManager
    {
        #region Variables

        /// <summary>
        /// List of loaded plugins
        /// </summary>
        public List<Type> Plugins { get; private set; }

        /// <summary>
        /// List of plugins user controls
        /// </summary>
        public List<UserControl> PluginsControls { get; private set; }

        // Not sure of a better way to do this, but I figure that the IOrganizationService isn't going anywhere anytime soon
        private AssemblyName _xrmSdkAssemblyName = typeof (Microsoft.Xrm.Sdk.IOrganizationService).Assembly.GetName();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class PluginManager
        /// </summary>
        public PluginManager()
        {
            Plugins = new List<Type>();
            PluginsControls = new List<UserControl>();
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

                Parallel.ForEach(files, file =>
                {
                    try
                    {
                        var assembly = Assembly.UnsafeLoadFrom(file);

                        AssertAssemblyReferencesCorrectXrmSdkVersion(assembly, file);
                        foreach (var type in assembly.GetTypes())
                        {
                            if (type.IsPublic && !type.IsAbstract)
                            {
                                if ((type.Attributes & TypeAttributes.Abstract) != TypeAttributes.Abstract &&
                                    !type.GetCustomAttributes(typeof(IgnorePluginAttribute), false).Any())
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
                    catch (InvalidXrmSdkReferenceException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        if (System.Diagnostics.Debugger.IsAttached)
                        {
                            throw;
                        }// else eat it
                    }
                });
            }
        }

        private void AssertAssemblyReferencesCorrectXrmSdkVersion(Assembly assembly, string filePath)
        {
            var xrmSdkAssembly = assembly.GetReferencedAssemblies().FirstOrDefault(a => a.Name == _xrmSdkAssemblyName.Name);
            if (xrmSdkAssembly != null && xrmSdkAssembly.Version.Major != _xrmSdkAssemblyName.Version.Major)
            {
                throw new InvalidXrmSdkReferenceException("Assembly '{0}' references '{1}' which is incompatible with this version of the XrmToolbox which references '{2}'.  Please remove the dll from the folder and restart the XrmToolbox!",
                    filePath, xrmSdkAssembly.FullName, _xrmSdkAssemblyName.FullName);
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
