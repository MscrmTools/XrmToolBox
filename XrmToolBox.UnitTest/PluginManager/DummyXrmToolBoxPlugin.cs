namespace XrmToolBox.UnitTest
{
    using System;
    using System.Drawing;
    using McTools.Xrm.Connection;
    using Microsoft.Xrm.Sdk;

    /// <summary>The dummy xrm tool box plugin.</summary>
    public class DummyXrmToolBoxPlugin : IMsCrmToolsPluginUserControl
    {
        /// <summary>The on request connection.</summary>
        public event EventHandler OnRequestConnection;

        /// <summary>The on close tool.</summary>
        public event EventHandler OnCloseTool;

        /// <summary>Gets the service.</summary>
        public IOrganizationService Service { get; private set; }

        /// <summary>Gets the plugin logo.</summary>
        public Image PluginLogo { get; private set; }

        /// <summary>The closing plugin.</summary>
        /// <param name="info">The info.</param>
        public void ClosingPlugin(PluginCloseInfo info)
        {
            throw new NotImplementedException();
        }

        /// <summary>The update connection.</summary>
        /// <param name="newService">The new service.</param>
        /// <param name="connectionDetail">The connection detail.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="parameter">The parameter.</param>
        public void UpdateConnection(IOrganizationService newService, ConnectionDetail connectionDetail, string actionName = "", object parameter = null)
        {
            throw new NotImplementedException();
        }
    }
}
