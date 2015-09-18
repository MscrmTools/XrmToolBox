using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Xrm.Sdk;
using MsCrmTools.FormLibrariesManager;
using MsCrmTools.FormRelated.FormLibrariesManager.POCO;

namespace MsCrmTools.FormRelated.FormLibrariesManager.Forms
{
    public partial class FormEventsDialog : Form
    {
        public Entity Form { get; }
        public string EventName { get; }
        public ObservableCollection<Script> Scripts { get; set; }
        public ObservableCollection<FormEvent> FormEvents { get; set; }

        public FormEventsDialog(Entity form, string eventName)
        {
            InitializeComponent();
            Form = form;
            EventName = eventName;

            InitializeFormEvents();
            bsScript.DataSource = Scripts;
            bsFormEvent.DataSource = FormEvents;
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
                Scripts.Add(new Script {Name = node.Attributes["name"].Value});
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

        private void FormEventsDialog_Load(object sender, EventArgs e)
        {
            
            

        }


        private void tsbUp_Click(object sender, EventArgs e)
        {
            var a = gridLineDataGridView1.SelectedRows[0];
        }

        private void tsbDown_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
