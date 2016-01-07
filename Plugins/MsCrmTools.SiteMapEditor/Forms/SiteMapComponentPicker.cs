// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace MsCrmTools.SiteMapEditor.Forms
{
    public partial class SiteMapComponentPicker : Form
    {
        public SiteMapComponentPicker(string componentName, string version)
        {
            InitializeComponent();

            XmlDocument doc = new XmlDocument();

            Assembly myAssembly = Assembly.GetExecutingAssembly();
            using (StreamReader reader = new StreamReader(myAssembly.GetManifestResourceStream("MsCrmTools.SiteMapEditor.Resources.sitemap" + version + ".xml")))
            {
                doc.LoadXml(reader.ReadToEnd());
            }

            FillList(lvCrm2011SiteMap, doc, componentName);

            ToolTip tip = new ToolTip();
            tip.ToolTipTitle = "Information";
            tip.SetToolTip(chkAddChildComponents, "Check this control if you want to add components under the one you select (ie. Area with all child Groups and SubArea or just Area)");
        }

        public XmlNode SelectedNode { get; set; }

        private void btnComponentPickerCancel_Click(object sender, EventArgs e)
        {
            SelectedNode = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnComponentPickerValidate_Click(object sender, EventArgs e)
        {
            if (lvCrm2011SiteMap.SelectedItems.Count > 0)
            {
                XmlNode selectedXmlNode = (XmlNode)lvCrm2011SiteMap.SelectedItems[0].Tag;

                if (!chkAddChildComponents.Checked)
                {
                    for (int i = selectedXmlNode.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        selectedXmlNode.RemoveChild(selectedXmlNode.ChildNodes[i]);
                    }
                }

                SelectedNode = selectedXmlNode;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Please select a component!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FillList(ListView lv, XmlDocument doc, string componentName)
        {
            try
            {
                XmlNodeList list = doc.SelectNodes("//" + componentName);

                foreach (XmlNode node in list)
                {
                    if (!lv.Items.ContainsKey(node.Attributes["Id"].Value))
                    {
                        ListViewItem item = new ListViewItem(node.Attributes["Id"].Value);
                        item.SubItems.Add(node.Attributes["Entity"] != null ? node.Attributes["Entity"].Value : "-");
                        item.SubItems.Add(node.Attributes["ResourceId"] != null ? node.Attributes["ResourceId"].Value : "-");
                        item.Tag = node;

                        if (node.ParentNode != null)
                        {
                            string groupName = node.ParentNode.Name;

                            if (node.ParentNode.Attributes["Id"] != null)
                            {
                                groupName += " (" + node.ParentNode.Attributes["Id"].Value + ")";
                            }

                            ListViewGroup group = lv.Groups[groupName.Replace(" ", "")];

                            if (group == null)
                            {
                                group = new ListViewGroup(groupName);
                                group.Name = groupName.Replace(" ", "");
                                lv.Groups.Add(group);
                            }

                            item.Group = group;
                        }

                        lv.Items.Add(item);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstComponents_DoubleClick(object sender, EventArgs e)
        {
            btnComponentPickerValidate_Click(sender, e);
        }
    }
}