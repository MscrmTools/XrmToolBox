// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.SampleTool
{
    /// <summary>
    /// This class describe how to write a plugin for XrmToolBox
    /// </summary>
    /// <remarks>
    /// A plugin is exposed to XrmToolBox through a class decorated with attribute
    /// [Export(typeof(IMsCrmToolsPluginUserControl))]
    /// All ExportMetadata described in this plugin are mandatory
    /// Name and Description are not more used from Assembly information
    /// For image Metadata, paste your base64 encoded image or null to use "No logo" logo
    /// For color Metadata, you can use color name or hexadecimal code
    /// 
    /// If this is not obvious, it is possible to include multiple plugins in one assembly
    /// as plugins are retrieved by class and not by assembly anymore
    /// 
    /// Required references:
    /// - XrmToolBox.Extensions
    /// - System.ComponentModel.Composition
    /// - Microsoft Dynamics CRM SDK libraries
    /// </remarks>
    [Export(typeof(IMsCrmToolsPluginUserControl)),
    ExportMetadata("Name", "A Sample Tool"),
    ExportMetadata("Description", "This is a tool to learn XrmToolBox developement"),
    ExportMetadata("SmallImageBase64", null),
    ExportMetadata("BigImageBase64", null),
    ExportMetadata("BackgroundColor", "Lavender"),
    ExportMetadata("PrimaryFontColor", "#000000"),
    ExportMetadata("SecondaryFontColor", "DarkGray")]
    public partial class SampleTool : PluginBase, IGitHubPlugin, ICodePlexPlugin, IPayPalPlugin
    {
        #region Base tool implementation

        public SampleTool()
        {
            InitializeComponent();
        }

        private void BtnWhoAmIClick(object sender, EventArgs e)
        {
            ExecuteMethod(ProcessWhoAmI);
        }

        public void ProcessWhoAmI()
        {
            WorkAsync(null, (w, e) =>
            {
                var request = new WhoAmIRequest();
                var response = (WhoAmIResponse) Service.Execute(request);

                e.Result = response.UserId;
            },
                e =>
                {
                    MessageBox.Show(string.Format("You are {0}", (Guid) e.Result));
                },
                e =>
                {
                    // If progress has to be notified to user, use the following method:
                    //SetWorkingMessage("Message to display");
                },
                "Retrieving your user id...",
                340,
                150);
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        #endregion Base tool implementation

        #region Github implementation

        public string UserName
        {
            get { return "GithubUserName"; }
        }

        public string RepositoryName
        {
            get { return "GithubRepositoryName"; }
        }

        #endregion Github implementation

        #region CodePlex implementation

        public string CodePlexUrlName
        {
            get { return "CodePlex"; }
        }

        #endregion CodePlex implementation

        #region PayPal implementation
        
        public string EmailAccount
        {
            get { return "paypal@paypal.com"; }
        }

        public string DonationDescription
        {
            get { return "paypal description"; }
        }

        #endregion PayPal implementation
    }
}
