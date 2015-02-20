using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Xml;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

#if NO_GEMBOX
using OfficeOpenXml;
#else
using GemBox.Spreadsheet;
#endif

namespace MsCrmTools.Translator.AppCode
{
    public class SiteMapTranslation
    {
        private Entity siteMap;

        /// <summary>
        /// 
        /// </summary>
        /// <example>
        /// viewId;entityLogicalName;viewName;ViewType;Type;LCID1;LCID2;...;LCODX
        /// </example>
        /// <param name="languages"></param>
        /// <param name="file"></param>
        /// <param name="service"></param>
#if NO_GEMBOX
        public void Export(List<int> languages, ExcelWorkbook file, IOrganizationService service)
#else
        public void Export(List<int> languages, ExcelFile file, IOrganizationService service)
#endif
        {
            var line = 1;

            var siteMap = GetSiteMap(service);
            var siteMapDoc = new XmlDocument();
            siteMapDoc.LoadXml(siteMap["sitemapxml"].ToString());

            var crmSiteMapAreas = new List<CrmSiteMapArea>();
            var crmSiteMapGroups = new List<CrmSiteMapGroup>();
            var crmSiteMapSubAreas = new List<CrmSiteMapSubArea>();

            #region Export Area

            var areaNodes = siteMapDoc.SelectNodes("SiteMap/Area");
            foreach (XmlNode areaNode in areaNodes)
            {
                var area = new CrmSiteMapArea {Id = areaNode.Attributes["Id"].Value};
                foreach (XmlNode titleNode in areaNode.SelectNodes("Titles/Title"))
                {
                    area.Titles.Add(int.Parse(titleNode.Attributes["LCID"].Value), titleNode.Attributes["Title"].Value);
                }
                foreach (XmlNode titleNode in areaNode.SelectNodes("Descriptions/Description"))
                {
                    area.Descriptions.Add(int.Parse(titleNode.Attributes["LCID"].Value), titleNode.Attributes["Description"].Value);
                }

                crmSiteMapAreas.Add(area);

                #region Export Groups

                var groupNodes = areaNode.SelectNodes("Group");
                foreach (XmlNode groupNode in groupNodes)
                {
                    var group = new CrmSiteMapGroup
                    {
                        Id = groupNode.Attributes["Id"].Value,
                        AreaId = areaNode.Attributes["Id"].Value
                    };
                    foreach (XmlNode titleNode in groupNode.SelectNodes("Titles/Title"))
                    {
                        group.Titles.Add(int.Parse(titleNode.Attributes["LCID"].Value), titleNode.Attributes["Title"].Value);
                    }
                    foreach (XmlNode titleNode in groupNode.SelectNodes("Descriptions/Description"))
                    {
                        group.Descriptions.Add(int.Parse(titleNode.Attributes["LCID"].Value), titleNode.Attributes["Description"].Value);
                    }

                    crmSiteMapGroups.Add(group);

                    #region Export SubArea

                    var subAreaNodes = groupNode.SelectNodes("SubArea");
                    foreach (XmlNode subAreaNode in subAreaNodes)
                    {
                        var subArea = new CrmSiteMapSubArea()
                        {
                            Id = subAreaNode.Attributes["Id"].Value,
                            GroupId = groupNode.Attributes["Id"].Value,
                            AreaId = areaNode.Attributes["Id"].Value
                        };
                        foreach (XmlNode titleNode in subAreaNode.SelectNodes("Titles/Title"))
                        {
                            subArea.Titles.Add(int.Parse(titleNode.Attributes["LCID"].Value), titleNode.Attributes["Title"].Value);
                        }
                        foreach (XmlNode titleNode in groupNode.SelectNodes("Descriptions/Description"))
                        {
                            subArea.Descriptions.Add(int.Parse(titleNode.Attributes["LCID"].Value), titleNode.Attributes["Description"].Value);
                        }

                        crmSiteMapSubAreas.Add(subArea);
                    }

                    #endregion
                }

                #endregion
            }

            #endregion

            #region Area sheet

            var areaSheet = file.Worksheets.Add("SiteMap Areas");
            AddAreaHeader(areaSheet, languages);
            foreach (var crmArea in crmSiteMapAreas)
            {
                var cell = 0;
                areaSheet.Cells[line, cell++].Value = crmArea.Id;
                areaSheet.Cells[line, cell++].Value = "Title";

                foreach (var lcid in languages)
                {
                    areaSheet.Cells[line, cell++].Value = crmArea.Titles.FirstOrDefault(n => n.Key == lcid).Value;
                }

                line++;
                cell = 0;
                areaSheet.Cells[line, cell++].Value = crmArea.Id;
                areaSheet.Cells[line, cell++].Value = "Description";

                foreach (var lcid in languages)
                {
                    areaSheet.Cells[line, cell++].Value = crmArea.Descriptions.FirstOrDefault(n => n.Key == lcid).Value;
                }
                line++;
            }

            #endregion

            #region Group sheet

            line = 1;
            var groupSheet = file.Worksheets.Add("SiteMap Groups");
            AddGroupHeader(groupSheet, languages);
            foreach (var crmGroup in crmSiteMapGroups)
            {
                var cell = 0;
                groupSheet.Cells[line, cell++].Value = crmGroup.AreaId;
                groupSheet.Cells[line, cell++].Value = crmGroup.Id;
                groupSheet.Cells[line, cell++].Value = "Title";

                foreach (var lcid in languages)
                {
                    groupSheet.Cells[line, cell++].Value = crmGroup.Titles.FirstOrDefault(n => n.Key == lcid).Value;
                }

                line++;
                cell = 0;
                groupSheet.Cells[line, cell++].Value = crmGroup.AreaId;
                groupSheet.Cells[line, cell++].Value = crmGroup.Id;
                groupSheet.Cells[line, cell++].Value = "Description";

                foreach (var lcid in languages)
                {
                    groupSheet.Cells[line, cell++].Value = crmGroup.Descriptions.FirstOrDefault(n => n.Key == lcid).Value;
                }
                line++;
            }

            #endregion

            #region SubArea sheet

            line = 1;
            var subAreaSheet = file.Worksheets.Add("SiteMap SubAreas");
            AddSubAreaHeader(subAreaSheet, languages);
            foreach (var crmSubArea in crmSiteMapSubAreas)
            {
                var cell = 0;
                subAreaSheet.Cells[line, cell++].Value = crmSubArea.AreaId;
                subAreaSheet.Cells[line, cell++].Value = crmSubArea.GroupId;
                subAreaSheet.Cells[line, cell++].Value = crmSubArea.Id;
                subAreaSheet.Cells[line, cell++].Value = "Title";

                foreach (var lcid in languages)
                {
                    subAreaSheet.Cells[line, cell++].Value = crmSubArea.Titles.FirstOrDefault(n => n.Key == lcid).Value;
                }

                line++;
                cell = 0;
                subAreaSheet.Cells[line, cell++].Value = crmSubArea.AreaId;
                subAreaSheet.Cells[line, cell++].Value = crmSubArea.GroupId;
                subAreaSheet.Cells[line, cell++].Value = crmSubArea.Id;
                subAreaSheet.Cells[line, cell++].Value = "Description";

                foreach (var lcid in languages)
                {
                    subAreaSheet.Cells[line, cell++].Value = crmSubArea.Descriptions.FirstOrDefault(n => n.Key == lcid).Value;
                }
                line++;
            }

            #endregion
            
            // Applying style to cells
            for (int i = 0; i < (2 + languages.Count); i++)
            {
                StyleMutator.SetCellColorAndFontWeight(areaSheet.Cells[0, i].Style, Color.PowderBlue, isBold:true);
            }
            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    StyleMutator.SetCellColorAndFontWeight(areaSheet.Cells[i, j].Style, Color.AliceBlue);
                }
            }

            for (int i = 0; i < (3 + languages.Count); i++)
            {
                StyleMutator.SetCellColorAndFontWeight(groupSheet.Cells[0, i].Style, Color.PowderBlue, isBold: true);
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    StyleMutator.SetCellColorAndFontWeight(groupSheet.Cells[i, j].Style, Color.AliceBlue);

                }
            }

            for (int i = 0; i < (4 + languages.Count); i++)
            {
                StyleMutator.SetCellColorAndFontWeight(subAreaSheet.Cells[0, i].Style, Color.PowderBlue, isBold: true);
            }

            for (int i = 1; i < line; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    StyleMutator.SetCellColorAndFontWeight(subAreaSheet.Cells[i, j].Style, Color.AliceBlue);
                }
            }
        }

        public Entity GetSiteMap(IOrganizationService service)
        {
            EntityCollection ec = service.RetrieveMultiple(new QueryExpression("sitemap") {ColumnSet = new ColumnSet(true)});

            var siteMap = ec[0];
            return siteMap;
        }

#if NO_GEMBOX
        public void PrepareAreas(ExcelWorksheet sheet, IOrganizationService service)
        {
            if (siteMap == null)
            {
                siteMap = GetSiteMap(service);
            }

            var siteMapDoc = new XmlDocument();
            siteMapDoc.LoadXml(siteMap["sitemapxml"].ToString());

            var rowsCount = sheet.Dimension.Rows;

            for (var rowI = 1; rowI < rowsCount; rowI++)
            {
                if (sheet.Cells[rowI, 0].Value == null) break;

                var areaId = sheet.Cells[rowI, 0].Value.ToString();
                var areaNode = siteMapDoc.SelectSingleNode("SiteMap/Area[@Id='" + areaId + "']");
                if (areaNode == null)
                {
                    throw new Exception("Unable to find area with id " + areaId);
                }

                var columnIndex = 2;
                while (sheet.Cells[rowI, columnIndex].Value != null)
                {
                    if (sheet.Cells[rowI, 1].Value.ToString() == "Title")
                    {
                        UpdateXmlNode(areaNode, "Titles", "Title", sheet.Cells[0, columnIndex].Value.ToString(),
                            sheet.Cells[rowI, columnIndex].Value.ToString());
                    }
                    else
                    {
                        UpdateXmlNode(areaNode, "Descriptions", "Description", sheet.Cells[0, columnIndex].Value.ToString(),
                         sheet.Cells[rowI, columnIndex].Value.ToString());
                    }
                    columnIndex++;
                }

            }

            siteMap["sitemapxml"] = siteMapDoc.OuterXml;
        }

        public void PrepareGroups(ExcelWorksheet sheet, IOrganizationService service)
        {
            if (siteMap == null)
            {
                siteMap = GetSiteMap(service);
            }

            var siteMapDoc = new XmlDocument();
            siteMapDoc.LoadXml(siteMap["sitemapxml"].ToString());

            var rowsCount = sheet.Dimension.Rows;

            for (var rowI = 1; rowI < rowsCount; rowI++)
            {
                if (sheet.Cells[rowI, 0].Value == null) break;

                var areaId = sheet.Cells[rowI, 0].Value.ToString();
                var groupId = sheet.Cells[rowI, 1].Value.ToString();
                var groupNode = siteMapDoc.SelectSingleNode("SiteMap/Area[@Id='" + areaId + "']/Group[@Id='" + groupId + "']");
                if (groupNode == null)
                {
                    throw new Exception("Unable to find group with id " + groupId + " in area " + areaId);
                }

                var columnIndex = 3;
                while (sheet.Cells[rowI, columnIndex].Value != null)
                {
                    if (sheet.Cells[rowI, 1].Value.ToString() == "Title")
                    {
                        UpdateXmlNode(groupNode, "Titles", "Title", sheet.Cells[0, columnIndex].Value.ToString(),
                            sheet.Cells[rowI, columnIndex].Value.ToString());
                    }
                    else
                    {
                        UpdateXmlNode(groupNode, "Descriptions", "Description", sheet.Cells[0, columnIndex].Value.ToString(),
                         sheet.Cells[rowI, columnIndex].Value.ToString());
                    }
                    columnIndex++;
                }

            }

            siteMap["sitemapxml"] = siteMapDoc.OuterXml;
        }

        public void PrepareSubAreas(ExcelWorksheet sheet, IOrganizationService service)
        {
            if (siteMap == null)
            {
                siteMap = GetSiteMap(service);
            }

            var siteMapDoc = new XmlDocument();
            siteMapDoc.LoadXml(siteMap["sitemapxml"].ToString());

            var rowsCount = sheet.Dimension.Rows;

            for (var rowI = 1; rowI < rowsCount; rowI++)
            {
                if (sheet.Cells[rowI, 0].Value == null) break;

                var areaId = sheet.Cells[rowI, 0].Value.ToString();
                var groupId = sheet.Cells[rowI, 1].Value.ToString();
                var subAreaId = sheet.Cells[rowI, 2].Value.ToString();
                var subAreaNode = siteMapDoc.SelectSingleNode("SiteMap/Area[@Id='" + areaId + "']/Group[@Id='" + groupId + "']/SubArea[@Id='" + subAreaId + "']");
                if (subAreaNode == null)
                {
                    throw new Exception("Unable to find group with id " + groupId + " in area " + areaId);
                }

                var columnIndex = 4;
                while (sheet.Cells[rowI, columnIndex].Value != null)
                {
                    if (sheet.Cells[rowI, 1].Value.ToString() == "Title")
                    {
                        UpdateXmlNode(subAreaNode, "Titles", "Title", sheet.Cells[0, columnIndex].Value.ToString(),
                            sheet.Cells[rowI, columnIndex].Value.ToString());
                    }
                    else
                    {
                        UpdateXmlNode(subAreaNode, "Descriptions", "Description", sheet.Cells[0, columnIndex].Value.ToString(),
                         sheet.Cells[rowI, columnIndex].Value.ToString());
                    }
                    columnIndex++;
                }
            }

            siteMap["sitemapxml"] = siteMapDoc.OuterXml;
        }
#else
        public void PrepareAreas(ExcelWorksheet sheet, IOrganizationService service)
        {
            if (siteMap == null)
            {
                siteMap = GetSiteMap(service);
            }

            var siteMapDoc = new XmlDocument();
            siteMapDoc.LoadXml(siteMap["sitemapxml"].ToString());

            foreach (var row in sheet.Rows.OrderBy(r=>r.Index))
            {
                if (row.Index == 0) continue;
                if (row.Cells[0].Value == null) break;

                var areaId = row.Cells[0].Value.ToString();
                var areaNode = siteMapDoc.SelectSingleNode("SiteMap/Area[@Id='" + areaId + "']");
                if (areaNode == null)
                {
                    throw new Exception("Unable to find area with id " + areaId);
                }

                var columnIndex = 2;
                while (row.Cells[columnIndex].Value != null)
                {
                    if (row.Cells[1].Value.ToString() == "Title")
                    {
                        UpdateXmlNode(areaNode, "Titles", "Title", sheet.Cells[0, columnIndex].Value.ToString(),
                            row.Cells[columnIndex].Value.ToString());
                    }
                    else
                    {
                        UpdateXmlNode(areaNode, "Descriptions", "Description", sheet.Cells[0, columnIndex].Value.ToString(),
                         row.Cells[columnIndex].Value.ToString());
                    }
                    columnIndex++;
                }

            }

            siteMap["sitemapxml"] = siteMapDoc.OuterXml;
        }

        public void PrepareGroups(ExcelWorksheet sheet, IOrganizationService service)
        {
            if (siteMap == null)
            {
                siteMap = GetSiteMap(service);
            } 
            
            var siteMapDoc = new XmlDocument();
            siteMapDoc.LoadXml(siteMap["sitemapxml"].ToString());

            foreach (var row in sheet.Rows)
            {
                if (row.Index == 0) continue;
                if (row.Cells[0].Value == null) break;

                var areaId = row.Cells[0].Value.ToString();
                var groupId = row.Cells[1].Value.ToString();
                var groupNode = siteMapDoc.SelectSingleNode("SiteMap/Area[@Id='" + areaId + "']/Group[@Id='"+groupId+"']");
                if (groupNode == null)
                {
                    throw new Exception("Unable to find group with id " + groupId + " in area " + areaId);
                }

                var columnIndex = 3;
                while (row.Cells[columnIndex].Value != null)
                {
                    if (row.Cells[1].Value.ToString() == "Title")
                    {
                        UpdateXmlNode(groupNode, "Titles", "Title", sheet.Cells[0, columnIndex].Value.ToString(),
                            row.Cells[columnIndex].Value.ToString());
                    }
                    else
                    {
                        UpdateXmlNode(groupNode, "Descriptions", "Description", sheet.Cells[0, columnIndex].Value.ToString(),
                         row.Cells[columnIndex].Value.ToString());
                    }
                    columnIndex++;
                }

            }

            siteMap["sitemapxml"] = siteMapDoc.OuterXml;
        }

        public void PrepareSubAreas(ExcelWorksheet sheet, IOrganizationService service)
        {
            if (siteMap == null)
            {
                siteMap = GetSiteMap(service);
            }

            var siteMapDoc = new XmlDocument();
            siteMapDoc.LoadXml(siteMap["sitemapxml"].ToString());

            foreach (var row in sheet.Rows)
            {
                if (row.Index == 0) continue;
                if (row.Cells[0].Value == null) break;

                var areaId = row.Cells[0].Value.ToString();
                var groupId = row.Cells[1].Value.ToString();
                var subAreaId = row.Cells[2].Value.ToString();
                var subAreaNode = siteMapDoc.SelectSingleNode("SiteMap/Area[@Id='" + areaId + "']/Group[@Id='" + groupId + "']/SubArea[@Id='" + subAreaId + "']");
                if (subAreaNode == null)
                {
                    throw new Exception("Unable to find group with id " + groupId + " in area " + areaId);
                }

                var columnIndex = 4;
                while (row.Cells[columnIndex].Value != null)
                {
                    if (row.Cells[1].Value.ToString() == "Title")
                    {
                        UpdateXmlNode(subAreaNode, "Titles", "Title", sheet.Cells[0, columnIndex].Value.ToString(),
                            row.Cells[columnIndex].Value.ToString());
                    }
                    else
                    {
                        UpdateXmlNode(subAreaNode, "Descriptions", "Description", sheet.Cells[0, columnIndex].Value.ToString(),
                         row.Cells[columnIndex].Value.ToString());
                    }
                    columnIndex++;
                }
            }

            siteMap["sitemapxml"] = siteMapDoc.OuterXml;
        }
#endif
        public void Import(IOrganizationService service)
        {
           service.Update(siteMap);
        }

        private void AddAreaHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            sheet.Cells[0, cell++].Value = "Area Id";
            sheet.Cells[0, cell++].Value = "Type";

            foreach (var lcid in languages)
            {
                sheet.Cells[0, cell++].Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void AddGroupHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            sheet.Cells[0, cell++].Value = "Area Id";
            sheet.Cells[0, cell++].Value = "Group Id";
            sheet.Cells[0, cell++].Value = "Type";

            foreach (var lcid in languages)
            {
                sheet.Cells[0, cell++].Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void AddSubAreaHeader(ExcelWorksheet sheet, IEnumerable<int> languages)
        {
            var cell = 0;

            sheet.Cells[0, cell++].Value = "Area Id";
            sheet.Cells[0, cell++].Value = "Group Id";
            sheet.Cells[0, cell++].Value = "SubArea Id";
            sheet.Cells[0, cell++].Value = "Type";

            foreach (var lcid in languages)
            {
                sheet.Cells[0, cell++].Value = lcid.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void UpdateXmlNode(XmlNode node,string collectionName, string itemName, string lcid, string description)
        {
            XmlNode refNode;
            switch (node.Name)
            {
                case "Area":
                    refNode = node.SelectSingleNode("Group");
                    break;
                case "Group":
                    refNode = node.SelectSingleNode("SubArea");
                    break;
                case "SubArea":
                    refNode = node.SelectSingleNode("Privilege");
                    break;
                default:
                    throw new Exception("Unexpected node name");
            }

            var labelsNode = node.SelectSingleNode(collectionName);
            if (labelsNode == null)
            {
                labelsNode = node.OwnerDocument.CreateElement(collectionName);
                if (refNode != null)
                {
                    node.InsertBefore(labelsNode, refNode);
                }
                else
                {
                    node.AppendChild(labelsNode);
                }
            }

            var labelNode = labelsNode.SelectSingleNode(string.Format(itemName + "[@LCID='{0}']", lcid));
            if (labelNode == null)
            {
                labelNode = node.OwnerDocument.CreateElement(itemName);
                labelsNode.AppendChild(labelNode);

                var languageAttr = node.OwnerDocument.CreateAttribute("LCID");
                languageAttr.Value = lcid;
                labelNode.Attributes.Append(languageAttr);
                var descriptionAttr = node.OwnerDocument.CreateAttribute(itemName);
                labelNode.Attributes.Append(descriptionAttr);
            }

            labelNode.Attributes[itemName].Value = description;
        }
    }
}
