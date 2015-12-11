using Microsoft.Xrm.Sdk;
using MsCrmTools.FormRelated.FormLibrariesManager.POCO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace MsCrmTools.FormRelated.FormLibrariesManager.Forms
{
    public partial class FormEventsDialog : Form
    {
        public FormEventsDialog(Entity form, string eventName)
        {
            InitializeComponent();
            Form = form;
            EventName = eventName;

            InitializeFormEvents();
            bsScript.DataSource = Scripts;
            bsFormEvent.DataSource = FormEvents;
        }

        public string EventName { get; private set; }
        public Entity Form { get; private set; }
        public ObservableCollection<FormEvent> FormEvents { get; set; }
        public ObservableCollection<Script> Scripts { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void FormEventsDialog_Load(object sender, EventArgs e)
        {
        }

        private void gridLineDataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // Default Enabled and Pass Execution Context to true
            //e.Row.Cells["Enabled"]
            //formEvent.Enabled = true;
            //formEvent.PassExecutionContext = true;
            e.Row.Cells[scriptDataGridViewTextBoxColumn.Name].Value = Scripts.Last().Name;
            e.Row.Cells[IsEnabled.Name].Value = true;
            e.Row.Cells[PassExecutionContext.Name].Value = true;
        }

        private void InitializeFormEvents()
        {
            var formXml = Form.GetAttributeValue<string>("formxml");
            var formDoc = new XmlDocument();
            formDoc.LoadXml(formXml);

            var formNode = formDoc.SelectSingleNode("form");
            if (formNode == null)
            {
                throw new Exception("Expected node \"formNode\" was not found");
            }

            var formLibrariesNode = formNode.SelectSingleNode("formLibraries");
            if (formLibrariesNode == null)
            {
                throw new Exception("Expected node \"formLibraries\" was not found!  Form must have a Library attached before adding an event.");
            }
            Scripts = new ObservableCollection<Script>();
            foreach (XmlNode node in formLibrariesNode.ChildNodes)
            {
                Scripts.Add(new Script { Name = node.Attributes["name"].Value });
            }

            var nodes = formNode.SelectNodes(string.Format("events/event[@name='{0}']/Handlers/Handler", EventName));
            if (nodes == null)
            {
                return;
            }

            FormEvents = new ObservableCollection<FormEvent>(from XmlNode node in nodes
                                                             select new FormEvent
                                                             {
                                                                 Script = node.Attributes["libraryName"].Value,
                                                                 Function = node.Attributes["functionName"].Value,
                                                                 Enabled = bool.Parse(node.Attributes["enabled"].Value),
                                                                 Parameters = node.Attributes["parameters"].Value,
                                                                 PassExecutionContext = bool.Parse(node.Attributes["passExecutionContext"].Value),
                                                             });
        }

        private void tsbDown_Click(object sender, EventArgs e)
        {
        }

        private void tsbUp_Click(object sender, EventArgs e)
        {
            var a = gridLineDataGridView1.SelectedRows[0];
        }
    }
}