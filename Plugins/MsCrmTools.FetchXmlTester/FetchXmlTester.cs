// PROJECT : MsCrmTools.FetchXmlTester
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using CSRichTextBoxSyntaxHighlighting;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;

namespace MsCrmTools.FetchXmlTester
{
    public partial class FetchXmlTester : PluginControlBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of class <see cref="FetchXmlTester"/>
        /// </summary>
        public FetchXmlTester()
        {
            InitializeComponent();

            XMLViewerSettings viewerSetting = new XMLViewerSettings
            {
                AttributeKey = Color.Red,
                AttributeValue = Color.Blue,
                Tag = Color.Blue,
                Element = Color.DarkRed,
                Value = Color.Black,
            };

            //viewer.Settings = viewerSetting;
        }

        #endregion Constructor

        #region Methods

        private string IndentXMLString(string xml)
        {
            var ms = new MemoryStream();
            var xtw = new XmlTextWriter(ms, Encoding.Unicode);
            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(xml);

                xtw.Formatting = Formatting.Indented;
                doc.WriteContentTo(xtw);
                xtw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                var sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void ProcessFetchXml()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Executing request...",
                AsyncArgument = txtRequest.Text,
                Work = (bw, e) =>
                {
                    var request = new ExecuteFetchRequest { FetchXml = e.Argument.ToString() };
                    var response = (ExecuteFetchResponse)Service.Execute(request);

                    e.Result = response.FetchXmlResult;
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error == null)
                    {
                        txtResponse.Text = IndentXMLString(e.Result.ToString());
                        tabControl1.SelectedTab = tabPage2;
                    }
                    else
                    {
                        MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            });
        }

        private void TsbCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void TsbExecuteClick(object sender, EventArgs e)
        {
            if (txtRequest.Text.Length == 0)
            {
                MessageBox.Show(this, "Please provide a fetchXml query before trying to execute it!", "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ExecuteMethod(ProcessFetchXml);
        }

        #endregion Methods

        private void ToolStripButton1Click(object sender, EventArgs e)
        {
            try
            {
                if ((tabControl1.SelectedTab.Controls[0]).Text.Length == 0)
                {
                    MessageBox.Show(this, "Please provide a valid Xml before trying to format it!", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ((XMLViewer)tabControl1.SelectedTab.Controls[0]).Process(true);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "An error occured: " + error.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}