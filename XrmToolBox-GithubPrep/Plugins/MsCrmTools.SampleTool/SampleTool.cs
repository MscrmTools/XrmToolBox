// PROJECT : MsCrmTools.SampleTool
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Query;
using XrmToolBox;

namespace MsCrmTools.SampleTool
{
    public partial class SampleTool : PluginBase
    {
        private IOrganizationService service;

        private Panel infoPanel;

        public SampleTool()
        {
            InitializeComponent();
        }

        private void BtnWhoAmIClick(object sender, EventArgs e)
        {
            ExecuteMethod(ProcessWhoAmI);
        }

        private void ProcessWhoAmI()
        {
            WorkAsync(null, (w, e) =>
            {
                var request = new WhoAmIRequest();
                var response = (WhoAmIResponse) service.Execute(request);

                e.Result = response.UserId;
            },
                e =>
                {
                    MessageBox.Show(string.Format("You are {0}", (Guid) e.Result));
                },
                e =>
                {

                },
                "Retrieving your user id...",
                340,
                150);
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }
    }
}
