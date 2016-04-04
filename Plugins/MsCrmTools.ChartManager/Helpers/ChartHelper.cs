﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.ChartManager.Helpers
{
    internal class ChartHelper
    {
        public static EntityCollection GetChartsByEntity(string entityLogicalName, IOrganizationService service)
        {
            return service.RetrieveMultiple(new QueryExpression("savedqueryvisualization")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("primaryentitytypecode", ConditionOperator.Equal, entityLogicalName)
                    }
                }
            });
        }

        public static List<ChartDefinition> AnalyzeFiles(List<string> filenames, IOrganizationService service)
        {
            var list = new List<ChartDefinition>();

            foreach (var fileName in filenames)
            {
                var doc = XDocument.Load(fileName);

                var cd = new ChartDefinition
                {
                    FileName = fileName,
                    Name = new FileInfo(fileName).Name,
                    IsValid = doc.Element("visualization") != null,
                    Errors = new List<string>()
                };

                if (!cd.IsValid)
                {
                    cd.Errors.Add("Not a chart definition");
                    list.Add(cd);
                    continue;
                }

                var chart = new Entity("savedqueryvisualization");

                var idElement = doc.Descendants("visualizationid").FirstOrDefault();
                if (idElement != null)
                {
                      chart.Id = new Guid(idElement.Value);
                }

                var nameElement = doc.Descendants("name").FirstOrDefault();
                if (nameElement == null)
                {
                    cd.Errors.Add("Missing 'name' node");
                }
                else
                {
                    chart["name"] = nameElement.Value;
                }

                var descriptionElement = doc.Descendants("description").FirstOrDefault();
                if (descriptionElement == null)
                {
                    cd.Errors.Add("Missing 'description' node");
                }
                else
                {
                    chart["description"] = descriptionElement.Value;
                }

                var primaryentitytypecodeElement = doc.Descendants("primaryentitytypecode").FirstOrDefault();
                if (primaryentitytypecodeElement == null)
                {
                    cd.Errors.Add("Missing 'primaryentitytypecode' node");
                }
                else
                {
                    chart["primaryentitytypecode"] = primaryentitytypecodeElement.Value;
                }

                var datadescriptionElement = doc.Descendants("datadescription").FirstOrDefault();
                if (datadescriptionElement == null)
                {
                    cd.Errors.Add("Missing 'datadescription' node");
                }
                else
                {
                    chart["datadescription"] = datadescriptionElement.FirstNode.ToString();
                }

                var presentationdescriptionElement = doc.Descendants("presentationdescription").FirstOrDefault();
                if (presentationdescriptionElement == null)
                {
                    cd.Errors.Add("Missing 'presentationdescription' node");
                }
                else
                {
                    chart["presentationdescription"] = presentationdescriptionElement.FirstNode.ToString();
                }

                var isdefaultElement = doc.Descendants("isdefault").FirstOrDefault();
                if (isdefaultElement == null)
                {
                    cd.Errors.Add("Missing 'isdefault' node");
                }
                else
                {
                    chart["isdefault"] = isdefaultElement.Value.ToLower() == "true";
                }

                cd.Entity = chart;

                if (chart.Id != Guid.Empty)
                {
                    cd.Exists = service.RetrieveMultiple(new QueryExpression("savedqueryvisualization")
                    {
                        Criteria = new FilterExpression
                        {
                            Conditions =
                            {
                                new ConditionExpression("savedqueryvisualizationid", ConditionOperator.Equal, chart.Id)
                            }
                        }
                    }).Entities.Count > 0;
                }

                list.Add(cd);
            }

            return list;
        }

        public static void ImportFiles(List<ChartDefinition> charts, IOrganizationService service)
        {
            foreach (var chart in charts)
            {
                if (chart.Exists)
                {
                    if (!chart.Overwrite)
                    {
                        chart.Entity["name"] = string.Format("{0}_{1}", chart.Entity.GetAttributeValue<string>("name"), DateTime.Now.ToShortTimeString());
                        chart.Entity.Attributes.Remove("savedqueryvisualizationid");
                        chart.Entity.Id = Guid.Empty;
                        
                        service.Create(chart.Entity);
                    }
                    else
                    {
                        service.Update(chart.Entity);
                    }
                }
                else
                {
                    service.Create(chart.Entity);
                }
            }
        }
    }

    public class ChartDefinition
    {
        public string FileName { get; set; }
        public bool IsValid { get; set; }
        public Entity Entity { get; set; }
        public bool Exists { get; set; }
        public List<string> Errors { get; set; }
        public string Name { get; set; }
        public bool Overwrite { get; set; }
    }
}
