using System;
using System.Linq;
using System.Reflection;

namespace XrmToolBox
{
    /// <summary>
    /// This class/hack was created to address issues with the Xrm Sdk not allowing the earlybound type assembly to be specified.
    /// https://github.com/MscrmTools/XrmToolBox/issues/353
    /// </summary>
    internal class AppDomainProxyTypeHack
    {
        public static void OverrideAppDomainKnownTypeInitialization()
        {
            InvokeAddingOfAssemblyLoadEventHandler();
            RemoveAssemblyLoadEventHandler();
        }

        private static void InvokeAddingOfAssemblyLoadEventHandler()
        {
            var proxyTypesProvider = typeof(Microsoft.Xrm.Sdk.IOrganizationService).Assembly.GetType("Microsoft.Xrm.Sdk.KnownProxyTypesProvider");
            if (proxyTypesProvider == null)
            {
                throw new Exception("AppDomainProxyTypeHack expected Microsoft.Xrm.Sdk.KnownProxyTypesProvider to exist in the same assembly as Microsoft.Xrm.Sdk.IOrganizationService, but it wasn't found!");
            }
            var getInstance = proxyTypesProvider.GetMethod("GetInstance", BindingFlags.NonPublic | BindingFlags.Static);
            if (getInstance == null)
            {
                throw new Exception("AppDomainProxyTypeHack expected Microsoft.Xrm.Sdk.KnownProxyTypesProvider to contain a GetInstance(bool) method, but it wasn't found!");
            }
            getInstance.Invoke(null, BindingFlags.Static, null, new object[] { false }, null);
        }

        private static void RemoveAssemblyLoadEventHandler()
        {
            var assemblyLoadField = AppDomain.CurrentDomain.GetType().GetField("AssemblyLoad", BindingFlags.Instance | BindingFlags.NonPublic);
            if (assemblyLoadField == null)
            {
                throw new Exception("AppDomainProxyTypeHack expected AppDomain to contain an AssemblyLoad field, and none was found!");
            }

            var listOfDelegates = (AssemblyLoadEventHandler) assemblyLoadField.GetValue(AppDomain.CurrentDomain);
            if (listOfDelegates == null)
            {
                throw new Exception("AppDomainProxyTypeHack expected Microsoft.Xrm.Sdk.KnownProxyTypesProvider.GetInstance(false) to add an Event handler to the AssemblyLoad Event, and none was found!");
            }
            var first = listOfDelegates.GetInvocationList().FirstOrDefault(i => i.Target.GetType().FullName == "Microsoft.Xrm.Sdk.AppDomainBasedKnownProxyTypesProvider");
            if (first == null)
            {
                throw new Exception("AppDomainProxyTypeHack expected Microsoft.Xrm.Sdk.KnownProxyTypesProvider.GetInstance(false) to add an Event handler of type Microsoft.Xrm.Sdk.AppDomainBasedKnownProxyTypesProvider to the AssemblyLoad Event, but none was found!");
            }
            // Remove Event Handler
            AppDomain.CurrentDomain.AssemblyLoad -= (AssemblyLoadEventHandler) first;
        }
    }
}
