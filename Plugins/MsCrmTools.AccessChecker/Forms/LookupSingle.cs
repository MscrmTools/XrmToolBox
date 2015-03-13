using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.AccessChecker.Forms
{
    public partial class LookupSingle : Form
    {
        private readonly IOrganizationService service;
        private readonly string entityName;
        private EntityMetadata metadata;

        public LookupSingle(string entityName, IOrganizationService service)
        {
            InitializeComponent();

            this.entityName = entityName;
            this.service = service;
        }

        public Guid SelectedRecordId { get; private set; }
        public string SelectedRecordText { get; private set; }

        private void LookupSingleLoad(object sender, EventArgs e)
        {
            var request = new RetrieveEntityRequest {LogicalName = entityName, EntityFilters = EntityFilters.Attributes};
            metadata = ((RetrieveEntityResponse) service.Execute(request)).EntityMetadata;

            var qe = new QueryExpression("savedquery");
            qe.ColumnSet = new ColumnSet(true);
            qe.Criteria.AddCondition("returnedtypecode", ConditionOperator.Equal, entityName);
            qe.Criteria.AddCondition("querytype", ConditionOperator.Equal, 4);
            var records = service.RetrieveMultiple(qe);

            int index = 0;
            int defaultViewIndex = 0;

            foreach (var record in records.Entities)
            {
                if ((bool) record["isdefault"])
                    defaultViewIndex = index;

                var view = new ViewInfo();
                view.Entity = record;

                cbbViews.Items.Add(view);

                index++;
            }

            cbbViews.SelectedIndex = defaultViewIndex;
        }

        private void CbbViewsSelectedIndexChanged(object sender, EventArgs e)
        {
            lvResults.Columns.Clear();

            var view = ((ViewInfo) cbbViews.SelectedItem).Entity;
            var layout = new XmlDocument();
            layout.LoadXml(view["layoutxml"].ToString());

            foreach (XmlNode cell in layout.SelectNodes("//cell"))
            {
                var ch = new ColumnHeader();
                try
                {
                    ch.Text =
                        metadata.Attributes.First(a => a.LogicalName == cell.Attributes["name"].Value)
                            .DisplayName.UserLocalizedLabel.Label;
                    ch.Width = int.Parse(cell.Attributes["width"].Value);
                }
                catch
                {
                    ch.Text = cell.Attributes["name"].Value;
                }
                lvResults.Columns.Add(ch);
            }
        }

        private int GetIntPart(string text)
        {
            int returnedValue;
            if (int.TryParse(text, out returnedValue))
            {
                return returnedValue;
            }
            return -1;
        }

        private void BtnSearchClick(object sender, EventArgs e)
        {
            try
            {
                var view = ((ViewInfo) cbbViews.SelectedItem).Entity;
                var layout = new XmlDocument();
                layout.LoadXml(view["layoutxml"].ToString());

                string fetchXml = view["fetchxml"].ToString();
                if (txtSearch.Text.Length == 0) txtSearch.Text = "*";
                fetchXml = fetchXml.Replace("{0}", txtSearch.Text.Replace("*", "%"));
                fetchXml = fetchXml.Replace("{1}", GetIntPart(txtSearch.Text).ToString());

                var fetch = new XmlDocument();
                fetch.LoadXml(fetchXml);

                var fetchNode = fetch.SelectSingleNode("//fetch");
                if (fetchNode != null)
                {
                    if (fetchNode.Attributes["page"] == null)
                    {
                        XmlAttribute pageAttr = fetch.CreateAttribute("page");
                        fetchNode.Attributes.Append(pageAttr);
                    }

                    fetchNode.Attributes["page"].Value = "1";

                    if (fetchNode.Attributes["count"] == null)
                    {
                        XmlAttribute pageAttr = fetch.CreateAttribute("count");
                        fetchNode.Attributes.Append(pageAttr);
                    }

                    fetchNode.Attributes["count"].Value = "250";
                }

                var results =
                    ((ExecuteFetchResponse) service.Execute(new ExecuteFetchRequest {FetchXml = fetch.OuterXml}))
                        .FetchXmlResult;
                var resultsDoc = new XmlDocument();
                resultsDoc.LoadXml(results);

                
                foreach (XmlNode node in resultsDoc.SelectNodes("//result"))
                {
                    bool isFirstCell = true;
                    
                    var item = new ListViewItem();
                    item.Tag = node.SelectSingleNode(metadata.PrimaryIdAttribute).InnerText;

                    foreach (XmlNode cell in layout.SelectNodes("//cell"))
                    {
                        var attributeNode = node.SelectSingleNode(cell.Attributes["name"].Value);
                        if (attributeNode == null)
                        {
                            if (isFirstCell)
                            {
                                item.Text = "";
                                isFirstCell = false;
                            }
                            else
                            {
                                item.SubItems.Add("");
                            }
                        }
                        else
                        {
                            if (attributeNode.Attributes["name"] != null)
                            {
                                if (isFirstCell)
                                {
                                    item.Text = attributeNode.Attributes["name"].Value;
                                    isFirstCell = false;
                                }
                                else
                                {
                                    item.SubItems.Add(attributeNode.Attributes["name"].Value);
                                }
                            }
                            else
                            {
                                if (isFirstCell)
                                {
                                    item.Text = attributeNode.InnerText;
                                    isFirstCell = false;
                                }
                                else
                                {
                                    item.SubItems.Add(attributeNode.InnerText);
                                }
                            }
                        }
                    }

                    lvResults.Items.Add(item);
                }

                if (resultsDoc.SelectSingleNode("resultset").Attributes["morerecords"].Value == "1")
                {
                    MessageBox.Show(this,
                                    "There is more than 250 records that match your search! Please refine your search",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(this,
                                   "An error occured: " + error.Message,
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            SelectedRecordId = new Guid(lvResults.SelectedItems[0].Tag.ToString());
            SelectedRecordText = lvResults.SelectedItems[0].Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            SelectedRecordId = Guid.Empty;
            SelectedRecordText = string.Empty;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TxtSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                BtnSearchClick(null, null);
            }
        }

        private void LvResultsColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvResults.Sorting = lvResults.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvResults.ListViewItemSorter = new ListViewItemComparer(e.Column, lvResults.Sorting);
        }

        private void LvResultsDoubleClick(object sender, EventArgs e)
        {
            BtnOkClick(null, null);
        }
    }
}
