// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;

namespace XrmToolBox.Extensibility.Interfaces
{
    public interface IXrmToolBoxPluginControl
    {
        /// <summary>
        /// EventHandler to close the current tool
        /// </summary>
        event EventHandler OnCloseTool;

        /// <summary>
        /// EventHandler to request a connection to an organization
        /// </summary>
        event EventHandler OnRequestConnection;

        /// <summary>
        /// EventHandler when a work async is started
        /// </summary>
        event EventHandler OnWorkAsync;

        /// <summary>
        /// Gets the organization service used by the tool
        /// </summary>
        IOrganizationService Service { get; }

        /// <summary>
        /// Method to allow plugin to Cancel a closing event, or perform any save events required before closing.
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