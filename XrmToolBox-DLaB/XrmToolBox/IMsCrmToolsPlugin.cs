// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;

namespace XrmToolBox
{
    public interface IMsCrmToolsPluginUserControl
    {
        /// <summary>
        /// Gets the organization service used by the tool
        /// </summary>
        IOrganizationService Service { get; }

        /// <summary>
        /// Gets the logo to display in the tools list
        /// </summary>
        Image PluginLogo { get; }

        /// <summary>
        /// EventHandler to request a connection to an organization
        /// </summary>
        event EventHandler OnRequestConnection;

        /// <summary>
        /// EventHandler to close the current tool
        /// </summary>
        event EventHandler OnCloseTool;

        /// <summary>
        /// M
        /// </summary>
        /// <param name="info"></param>
        void ClosingPlugin(PluginCloseInfo info);

        /// <summary>
        /// Updates the organization service used by the tool
        /// </summary>
        /// <param name="newService">Organization service</param>
        /// <param name="connectionDetail">Details of the connection</param>
        /// <param name="actionName">Action that requested a service update</param>
        /// <param name="parameter">Parameter passed when requesting a service update</param>
        void UpdateConnection(IOrganizationService newService, ConnectionDetail connectionDetail, string actionName = "", object parameter = null);
    }
}
