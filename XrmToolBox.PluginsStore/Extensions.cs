using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.PluginsStore
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
            try
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
            catch (Exception error)
            {
                var lm = new LogManager(typeof(Store));
                lm.LogError($"Unable to check if {fi.Name} is implementing interface IXrmToolBoxPlugin: {error.Message}");
                return false;
            }
        }

        public static void ReportProgress(this BackgroundWorker worker, int progress, string message)
        {
            if (worker != null && worker.WorkerReportsProgress)
            {
                worker.ReportProgress(progress, message);
            }
        }
    }
}