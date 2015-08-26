using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;

namespace MsCrmTools.ChartManager.Forms
{
    public partial class ChartEditor : Form
    {
        private Panel infoPanel;

        private readonly Entity chart;

        private readonly IOrganizationService service;

        public ChartEditor(Entity chart, IOrganizationService service)
        {
            InitializeComponent();
            this.chart = chart;
            this.service = service;
        }

        public bool HasUpdatedContent { get; private set; }

        private void ChartEditor_Load(object sender, EventArgs e)
        {
            txtName.Text = chart.GetAttributeValue<string>("name");
            txtDescription.Text = chart.GetAttributeValue<string>("description");
            tecDataDescription.Text = chart.GetAttributeValue<string>("datadescription");
            tecVisualizationDescription.Text = chart.GetAttributeValue<string>("presentationdescription");

            tecDataDescription.Text = IndentXmlString(tecDataDescription.Text);
            tecVisualizationDescription.Text = IndentXmlString(tecVisualizationDescription.Text);

            tecDataDescription.SetHighlighting("XML");
            tecVisualizationDescription.SetHighlighting("XML");

            // Fix to make Visualization Description text editor content using
            // all width
            Size = new Size(Size.Width + 1, Size.Height);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            chart["name"] = txtName.Text;
            chart["description"] = txtDescription.Text;
            chart["datadescription"] = tecDataDescription.Text;
            chart["presentationdescription"] = tecVisualizationDescription.Text;


            infoPanel = InformationPanel.GetInformationPanel(this, "Updating chart...", 350, 150);

            var worker = new BackgroundWorker {WorkerReportsProgress = true};
            worker.DoWork += (w, evt) =>
            {
                service.Update((Entity)evt.Argument);

                ((BackgroundWorker)w).ReportProgress(0,"Publishing entity...");

                service.Execute(new PublishXmlRequest
                {
                    ParameterXml = string.Format("<importexportxml><entities><entity>{0}</entity></entities><nodes/><securityroles/><settings/><workflows/></importexportxml>", chart.GetAttributeValue<string>("primaryentitytypecode"))
                });
            };
            worker.ProgressChanged += (w, evt) =>
            {
                InformationPanel.ChangeInformationPanelMessage(infoPanel, evt.UserState.ToString());
            };
            worker.RunWorkerCompleted += (w, evt) =>
            {
                if (evt.Error != null)
                {
                    MessageBox.Show(this, evt.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    HasUpdatedContent = true;
                }

                Controls.Remove(infoPanel);
                infoPanel.Dispose();
            };
            worker.RunWorkerAsync(chart);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string IndentXmlString(string xml)
        {
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.Unicode);
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(xml);

                xtw.Formatting = Formatting.Indented;
                doc.WriteContentTo(xtw);
                xtw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
            catch (Exception error)
            {
                MessageBox.Show(string.Format("An error occured while identing XML: {0}", error));
                return null;
            }
        }
    }
}
