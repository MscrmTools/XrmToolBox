// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.ViewLayoutReplicator.Helpers
{
    /// <summary>
    /// Helps to interact with Crm views
    /// </summary>
    class ViewHelper
    {
        #region Constants

        public const int VIEW_BASIC = 0;
        public const int VIEW_ADVANCEDFIND = 1;
        public const int VIEW_ASSOCIATED = 2;
        public const int VIEW_QUICKFIND = 4;
        public const int VIEW_SEARCH = 64;

        #endregion

        /// <summary>
        /// Retrieve the list of views for a specific entity
        /// </summary>
        /// <param name="entityDisplayName">Logical name of the entity</param>
        /// <param name="entitiesCache">Entities cache</param>
        /// <param name="service">Organization Service</param>
        /// <returns>List of views</returns>
        public static List<Entity> RetrieveViews(string entityLogicalName, List<EntityMetadata> entitiesCache, IOrganizationService service)
        {
            try
            {
                EntityMetadata currentEmd = entitiesCache.Find(delegate(EntityMetadata emd) { return emd.LogicalName == entityLogicalName; });

                QueryByAttribute qba = new QueryByAttribute
                                           {
                    EntityName = "savedquery",
                    ColumnSet = new ColumnSet(true)
                };

                qba.Attributes.Add("returnedtypecode");
                qba.Values.Add(currentEmd.ObjectTypeCode.Value);

                EntityCollection views = service.RetrieveMultiple(qba);

                List<Entity> viewsList = new List<Entity>();

                foreach (Entity entity in views.Entities)
                {
                    viewsList.Add(entity);
                }

                return viewsList;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving views: " + errorMessage);
            }
        }

        internal static IEnumerable<Entity> RetrieveUserViews(string entityLogicalName, List<EntityMetadata> entitiesCache, IOrganizationService service)
        {
            try
            {
                EntityMetadata currentEmd = entitiesCache.Find(e => e.LogicalName == entityLogicalName);

                QueryByAttribute qba = new QueryByAttribute
                {
                    EntityName = "userquery",
                    ColumnSet = new ColumnSet(true)
                };

                qba.Attributes.AddRange("returnedtypecode", "querytype");
                qba.Values.AddRange(currentEmd.ObjectTypeCode.Value, 0);

                EntityCollection views = service.RetrieveMultiple(qba);

                List<Entity> viewsList = new List<Entity>();

                foreach (Entity entity in views.Entities)
                {
                    viewsList.Add(entity);
                }

                return viewsList;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving user views: " + errorMessage);
            }
        }

        /// <summary>
        /// Copy view layout form source view to all specified target views
        /// </summary>
        /// <param name="sourceView">Source view</param>
        /// <param name="targetViews">List of target views</param>
        /// <param name="service">Crm organization service</param>
        /// <returns>Indicates if all views have been updated</returns>
        public static List<Tuple<string, string>> PropagateLayout(Entity sourceView, List<Entity> targetViews, IOrganizationService service)
        {
            var errors = new List<Tuple<string, string>>();
            string multiObjectAttribute = string.Empty;

            try
            {
                foreach (Entity targetView in targetViews)
                {
                    if (targetView.Id != sourceView.Id)
                    {
                        bool canUpdateLinkedAttributes = true;

                        if (sourceView["layoutxml"].ToString().Contains(".") && (int)targetView["querytype"] == VIEW_ASSOCIATED)
                        {
                            errors.Add(new Tuple<string, string>(targetView["name"].ToString(), "The associated view has not been updated because of related attributes"));
                            //canUpdateLinkedAttributes = false;
                            continue;
                        }

                        // Update grid cells
                        XmlDocument targetLayout = new XmlDocument();
                        targetLayout.LoadXml(targetView["layoutxml"].ToString());

                        XmlAttribute multiAttr = targetLayout.SelectSingleNode("grid/row").Attributes["multiobjectidfield"];
                        if (multiAttr != null)
                            multiObjectAttribute = multiAttr.Value;

                        // We empty the existing cells
                        for (int i = targetLayout.SelectSingleNode("grid/row").ChildNodes.Count; i > 0; i--)
                        {
                            XmlNode toDelete = targetLayout.SelectSingleNode("grid/row").ChildNodes[i - 1];
                            targetLayout.SelectSingleNode("grid/row").RemoveChild(toDelete);
                        }

                        XmlDocument sourceLayout = new XmlDocument();
                        sourceLayout.LoadXml(sourceView["layoutxml"].ToString());

                        XmlNodeList sourceCellNodes = sourceLayout.SelectNodes("grid/row/cell");

                        var cells = new List<string>();

                        foreach (XmlNode cellNode in sourceCellNodes)
                        {
                            if (!cellNode.Attributes["name"].Value.Contains(".") || (int)targetView["querytype"] != VIEW_ASSOCIATED)
                            {
                                cells.Add(cellNode.Attributes["name"].Value);

                                XmlNode nodeDest = targetLayout.ImportNode(cellNode.Clone(), true);
                                targetLayout.SelectSingleNode("grid/row").AppendChild(nodeDest);
                            }
                        }

                        targetView["layoutxml"] = targetLayout.OuterXml;

                        // Retrieve target fetch data
                        if (targetView.Contains("fetchxml"))
                        {
                            XmlDocument targetFetchDoc = new XmlDocument();
                            targetFetchDoc.LoadXml(targetView["fetchxml"].ToString());

                            // Apply same sorting
                            XmlNodeList targetSortNodes = targetFetchDoc.SelectNodes("fetch/entity/order");

                            // Removes existing sorting
                            for (int i = targetSortNodes.Count; i > 0; i--)
                            {
                                XmlNode toDelete = targetSortNodes[i - 1];
                                targetSortNodes[i - 1].ParentNode.RemoveChild(toDelete);
                            }

                            XmlDocument sourceFetchDoc = new XmlDocument();
                            sourceFetchDoc.LoadXml(sourceView["fetchxml"].ToString());

                            XmlNodeList sourceAttrNodes = sourceFetchDoc.SelectNodes("fetch/entity/attribute");

                            foreach (XmlNode attrNode in sourceAttrNodes)
                            {
                                if (targetFetchDoc.SelectSingleNode("fetch/entity/attribute[@name='" + attrNode.Attributes["name"].Value + "']") == null)
                                {
                                    XmlNode attrNodeToAdd = targetFetchDoc.ImportNode(attrNode, true);
                                    targetFetchDoc.SelectSingleNode("fetch/entity").AppendChild(attrNodeToAdd);
                                }
                            }

                            foreach (XmlNode cellNode in sourceCellNodes)
                            {
                                string name = cellNode.Attributes["name"].Value;
                                if (!name.Contains(".") && targetFetchDoc.SelectSingleNode("fetch/entity/attribute[@name='" + name + "']") == null)
                                {
                                    XmlElement attrNodeToAdd = targetFetchDoc.CreateElement("attribute");
                                    attrNodeToAdd.SetAttribute("name", name);
                                    targetFetchDoc.SelectSingleNode("fetch/entity").AppendChild(attrNodeToAdd);
                                }
                            }

                            XmlNodeList sourceSortNodes = sourceFetchDoc.SelectNodes("fetch/entity/order");

                            // Removes existing sorting
                            foreach (XmlNode orderNode in sourceSortNodes)
                            {
                                XmlNode orderNodeToAdd = targetFetchDoc.ImportNode(orderNode, true);
                                targetFetchDoc.SelectSingleNode("fetch/entity").AppendChild(orderNodeToAdd);
                            }

                            if (canUpdateLinkedAttributes)
                            {
                                // Retrieve source fetch data
                                if (sourceView.Contains("fetchxml"))
                                {
                                    //XmlDocument sourceFetchDoc = new XmlDocument();
                                    //sourceFetchDoc.LoadXml(sourceView["fetchxml"].ToString());

                                    XmlNodeList linkNodes = sourceFetchDoc.SelectNodes("fetch/entity/link-entity");

                                    foreach (XmlNode sourceLinkNode in linkNodes)
                                    {
                                        var alias = sourceLinkNode.Attributes["alias"].Value;

                                        if (cells.FirstOrDefault(c => c.StartsWith(alias + ".")) == null)
                                            continue;

                                        XmlNode targetLinkNode = targetFetchDoc.SelectSingleNode("fetch/entity/link-entity[@alias=\"" + alias + "\"]");

                                        // Adds the missing link-entity node
                                        if (targetLinkNode == null)
                                        {
                                            XmlNode nodeDest = targetFetchDoc.ImportNode(sourceLinkNode.Clone(), true);
                                            XmlAttribute typeAttr = nodeDest.Attributes["link-type"];
                                            if (typeAttr == null)
                                            {
                                                typeAttr = targetFetchDoc.CreateAttribute("link-type");
                                                typeAttr.Value = "outer";
                                                nodeDest.Attributes.Append(typeAttr);
                                            }
                                            else
                                            {
                                                typeAttr.Value = "outer";
                                            }

                                            targetFetchDoc.SelectSingleNode("fetch/entity").AppendChild(nodeDest);
                                        }

                                        // Retrieves node again (if it was added)
                                        targetLinkNode = targetFetchDoc.SelectSingleNode("fetch/entity/link-entity[@alias=\"" + alias + "\"]");

                                        // Removes existing attributes
                                        for (int i = targetLinkNode.ChildNodes.Count; i > 0; i--)
                                        {
                                            if (targetLinkNode.ChildNodes[i - 1].Name == "attribute")
                                            {
                                                XmlNode toDelete = targetLinkNode.ChildNodes[i - 1];
                                                targetLinkNode.RemoveChild(toDelete);
                                            }
                                        }

                                        // Adds the attribute nodes from the source node
                                        foreach (XmlNode node in sourceLinkNode.ChildNodes)
                                        {
                                            if (node.Name == "attribute")
                                            {
                                                XmlNode attributeNode = targetLinkNode.SelectSingleNode("attribute[@name='" + node.Attributes["name"].Value + "']");

                                                if (attributeNode == null)
                                                {
                                                    XmlNode nodeDest = targetFetchDoc.ImportNode(node.Clone(), true);
                                                    targetLinkNode.AppendChild(nodeDest);
                                                }
                                            }
                                        }
                                    }

                                }

                                // Suppression des éléments Attribute inutiles dans la requête
                                List<string> attributesToRemove = new List<string>();

                                foreach (XmlNode attributeNode in targetFetchDoc.SelectNodes("//attribute"))
                                {

                                    if (attributeNode.Attributes["name"].Value == multiObjectAttribute)
                                        break;

                                    bool isFoundInCell = false;

                                    foreach (XmlNode cellNode in sourceLayout.SelectNodes("grid/row/cell"))
                                    {
                                        if (attributeNode.ParentNode.Name == "link-entity")
                                        {
                                            if (cellNode.Attributes["name"].Value == attributeNode.ParentNode.Attributes["alias"].Value + "." + attributeNode.Attributes["name"].Value)
                                            {
                                                isFoundInCell = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (attributeNode.Attributes["name"].Value == (attributeNode.ParentNode.Attributes["name"].Value + "id") || cellNode.Attributes["name"].Value == attributeNode.Attributes["name"].Value)
                                            {
                                                isFoundInCell = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (!isFoundInCell)
                                    {
                                        if (attributeNode.ParentNode.Name == "link-entity")
                                        {
                                            attributesToRemove.Add(attributeNode.ParentNode.Attributes["alias"].Value + "." + attributeNode.Attributes["name"].Value);
                                        }
                                        else
                                        {
                                            attributesToRemove.Add(attributeNode.Attributes["name"].Value);
                                        }
                                    }
                                }

                                foreach (string attributeName in attributesToRemove)
                                {
                                    XmlNode node = null;

                                    if (attributeName.Contains("."))
                                    {
                                        node = targetFetchDoc.SelectSingleNode("fetch/entity/link-entity[@alias='" + attributeName.Split('.')[0] + "']/attribute[@name='" + attributeName.Split('.')[1] + "']");
                                        targetFetchDoc.SelectSingleNode("fetch/entity/link-entity[@alias='" + node.ParentNode.Attributes["alias"].Value + "']").RemoveChild(node);
                                    }
                                    else
                                    {
                                        node = targetFetchDoc.SelectSingleNode("fetch/entity/attribute[@name='" + attributeName + "']");
                                        targetFetchDoc.SelectSingleNode("fetch/entity").RemoveChild(node);
                                    }
                                }

                                foreach (XmlNode linkentityNode in targetFetchDoc.SelectNodes("fetch/entity/link-entity"))
                                {
                                    if (linkentityNode != null && linkentityNode.ChildNodes.Count == 0)
                                    {
                                        targetFetchDoc.SelectSingleNode("fetch/entity").RemoveChild(linkentityNode);
                                    }
                                }

                                targetView["fetchxml"] = targetFetchDoc.OuterXml;
                            }
                        }

                        try
                        {
                            var viewToUpdate = new Entity(targetView.LogicalName)
                            {
                                Id = targetView.Id
                            };
                            viewToUpdate["fetchxml"] = targetView["fetchxml"];
                            viewToUpdate["layoutxml"] = targetView["layoutxml"];

                            service.Update(viewToUpdate);
                        }
                        catch (Exception error)
                        {
                            errors.Add(new Tuple<string, string>(targetView["name"].ToString(), error.Message));
                        }
                    }
                }

                return errors;
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);

                throw new Exception("Error while copying layout to target views: " + errorMessage);
            }
        }
    }
}
