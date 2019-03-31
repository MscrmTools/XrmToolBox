using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;

namespace XrmToolBox.CustomControls
{
    public enum SerializationStyle
    {
        Explicit = 0,
        Basic = 1
    }
    public class EntitySerializer
    {
        private static string guidtemplate = "FFFFEEEEDDDDCCCCBBBBAAAA99998888";

        public static XmlDocument Serialize(Entity entity, XmlNode parent, SerializationStyle style)
        {
            XmlDocument result;
            if (parent != null)
            {
                result = parent.OwnerDocument;
            }
            else
            {
                result = new XmlDocument();
                parent = result.CreateElement("Entities");
                result.AppendChild(parent);
            }
            XmlNode xEntity = GetEntityNode(entity, result, style);
            foreach (KeyValuePair<string, object> attribute in entity.Attributes)
            {
                if (attribute.Key == entity.LogicalName + "id")
                {   // Don't include PK
                    continue;
                }
                XmlNode xAttribute = GetAttributeNode(result, attribute, style);
                object value = attribute.Value;
                if (value is AliasedValue)
                {
                    if (!string.IsNullOrEmpty(((AliasedValue)value).EntityLogicalName))
                    {
                        XmlAttribute xAliasedEntity = result.CreateAttribute("entitylogicalname");
                        xAliasedEntity.Value = ((AliasedValue)value).EntityLogicalName;
                        xAttribute.Attributes.Append(xAliasedEntity);
                    }
                    if (!string.IsNullOrEmpty(((AliasedValue)value).AttributeLogicalName))
                    {
                        XmlAttribute xAliasedAttribute = result.CreateAttribute("attributelogicalname");
                        xAliasedAttribute.Value = ((AliasedValue)value).AttributeLogicalName;
                        xAttribute.Attributes.Append(xAliasedAttribute);
                    }
                    value = ((AliasedValue)value).Value;
                }
                XmlAttribute xType = result.CreateAttribute("type");
                xType.Value = LastClassName(value);
                xAttribute.Attributes.Append(xType);
                if (value is EntityReference)
                {
                    XmlAttribute xRefEntity = result.CreateAttribute("entity");
                    xRefEntity.Value = ((EntityReference)value).LogicalName;
                    xAttribute.Attributes.Append(xRefEntity);
                    if (!string.IsNullOrEmpty(((EntityReference)value).Name))
                    {
                        XmlAttribute xRefValue = result.CreateAttribute("value");
                        xRefValue.Value = ((EntityReference)value).Name;
                        xAttribute.Attributes.Append(xRefValue);
                    }
                }
                object basetypevalue = AttributeToBaseType(value);
                if (basetypevalue != null)
                {
                    XmlText xValue = result.CreateTextNode(basetypevalue.ToString());
                    xAttribute.AppendChild(xValue);
                }
                xEntity.AppendChild(xAttribute);
            }
            parent.AppendChild(xEntity);
            return result;
        }

        public static Entity Deserialize(XmlNode xEntity)
        {
            Entity result;
            string name = xEntity.Name == "Entity" ? GetXmlAttribute(xEntity, "name") : xEntity.Name;
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new XmlException("Cannot deserialize entity, missing entity name");
            }
            string strId = GetXmlAttribute(xEntity, "id");
            Guid id = StringToGuidish(strId);
            if (!id.Equals(Guid.Empty))
            {
                result = new Entity(name, id);
            }
            else
            {
                result = new Entity(name);
            }
            foreach (XmlNode xAttribute in xEntity.ChildNodes)
            {
                if (xAttribute.NodeType == XmlNodeType.Element)
                {
                    string attribute = xAttribute.Name == "Attribute" ? GetXmlAttribute(xAttribute, "name") : xAttribute.Name;
                    string type = GetXmlAttribute(xAttribute, "type");
                    string value = xAttribute.ChildNodes.Count > 0 ? xAttribute.ChildNodes[0].InnerText : "";
                    if (type == "EntityReference")
                    {
                        string entity = GetXmlAttribute(xAttribute, "entity");
                        value = entity + ":" + value;
                        var entrefname = GetXmlAttribute(xAttribute, "value");
                        if (!string.IsNullOrEmpty(entrefname))
                        {
                            value += ":" + entrefname;
                        }
                    }
                    result[attribute] = GetProperty(type, value);
                }
            }
            return result;
        }

        private static XmlNode GetEntityNode(Entity entity, XmlDocument result, SerializationStyle style)
        {
            switch (style)
            {
                case SerializationStyle.Basic:
                    {
                        XmlNode xEntity = result.CreateElement("Entity");
                        XmlAttribute xEntityName = result.CreateAttribute("name");
                        xEntityName.Value = entity.LogicalName;
                        xEntity.Attributes.Append(xEntityName);
                        XmlAttribute xEntityId = result.CreateAttribute("id");
                        xEntityId.Value = entity.Id.ToString();
                        xEntity.Attributes.Append(xEntityId);
                        return xEntity;
                    }
                case SerializationStyle.Explicit:
                    {
                        XmlNode xEntity = result.CreateElement(entity.LogicalName);
                        XmlAttribute xEntityId = result.CreateAttribute("id");
                        xEntityId.Value = entity.Id.ToString();
                        xEntity.Attributes.Append(xEntityId);
                        return xEntity;
                    }
                default:
                    return null;
            }
        }

        private static XmlNode GetAttributeNode(XmlDocument result, KeyValuePair<string, object> attribute, SerializationStyle style)
        {
            switch (style)
            {
                case SerializationStyle.Basic:
                    XmlNode xAttribute = result.CreateNode(XmlNodeType.Element, "Attribute", "");
                    XmlAttribute xName = result.CreateAttribute("name");
                    xName.Value = attribute.Key;
                    xAttribute.Attributes.Append(xName);
                    return xAttribute;
                case SerializationStyle.Explicit:
                    return result.CreateNode(XmlNodeType.Element, attribute.Key, "");
                default:
                    return null;
            }
        }

        public static string ToJSON(Entity entity, Formatting format, int indent)
        {
            StringBuilder sb = new StringBuilder();
            var space = format == Formatting.Indented ? " " : "";
            sb.Append(
                Sep(format, indent + 0) + "{" + space +
                Sep(format, indent + 1) + "\"entity\":" + space + "\"" + entity.LogicalName + "\"," +
                Sep(format, indent + 1) + "\"id\":" + space + "\"{" + entity.Id.ToString() + "}\"," +
                Sep(format, indent + 1) + "\"attributes\":" + space + "[");

            bool first = true;
            foreach (KeyValuePair<string, object> attribute in entity.Attributes)
            {
                Object value = attribute.Value;
                if (attribute.Key == entity.LogicalName + "id")
                {
                    continue;
                }
                if (attribute.Key.EndsWith("_base") && entity.Contains(attribute.Key.Substring(0, attribute.Key.Length - 5)))
                {
                    continue;
                }

                if (first)
                {
                    sb.Append(Sep(format, indent + 2) + "{");
                    first = false;
                }
                else
                    sb.Append("," + Sep(format, indent + 2) + "{");

                if (value is AliasedValue)
                {
                    if (!string.IsNullOrEmpty(((AliasedValue)value).AttributeLogicalName))
                    {
                        sb.Append(Sep(format, indent + 3) + "\"attributelogicalname\":" + space + "\"" + (((AliasedValue)value).AttributeLogicalName) + "\",");
                    }
                    if (!string.IsNullOrEmpty(((AliasedValue)value).EntityLogicalName))
                    {
                        sb.Append(Sep(format, indent + 3) + "\"entitylogicalname\":" + space + "\"" + (((AliasedValue)value).EntityLogicalName) + "\",");
                    }
                    value = (((AliasedValue)value).Value);
                }

                sb.Append(Sep(format, indent + 3) + "\"name\":" + space + "\"" + attribute.Key + "\",");
                sb.Append(Sep(format, indent + 3) + "\"type\":" + space + "\"" + LastClassName(value) + "\",");

                if (value is EntityReference)
                {
                    sb.Append(Sep(format, indent + 3) + "\"entity\":" + space + "\"" + ((EntityReference)value).LogicalName + "\",");
                    if (!string.IsNullOrEmpty(((EntityReference)value).Name))
                    {
                        sb.Append(Sep(format, indent + 3) + "\"namevalue\":" + space + "\"" + ((EntityReference)value).Name + "\",");
                    }
                    value = ((EntityReference)value).Id;

                }

                if (value != null)
                {
                    sb.Append(string.Format(Sep(format, indent + 3) + "\"value\":" + space + "\"{0}\"", AttributeToBaseType(value)));
                }

                sb.Append(Sep(format, indent + 2) + "}");
            }
            sb.Append(Sep(format, indent + 1) + "]");
            sb.Append(Sep(format, indent + 0) + "}");
            return sb.ToString();
        }

        private static string LastClassName(object obj)
        {
            string result = obj == null ? "null" : obj.GetType().ToString();
            result = result.Split('.')[result.Split('.').Length - 1];
            return result;
        }

        public static object AttributeToBaseType(object attribute)
        {
            if (attribute is AliasedValue)
                return AttributeToBaseType(((AliasedValue)attribute).Value);
            else if (attribute is EntityReference)
                return ((EntityReference)attribute).Id;
            else if (attribute is EntityReferenceCollection)
            {
                var referencedEntity = "";
                foreach (var er in (EntityReferenceCollection)attribute)
                {
                    if (referencedEntity == "")
                    {
                        referencedEntity = er.LogicalName;
                    }
                    else if (referencedEntity != er.LogicalName)
                    {
                        referencedEntity = "";
                        break;
                    }
                }
                var result = "";
                foreach (var er in (EntityReferenceCollection)attribute)
                {
                    if (result != "")
                    {
                        result += ",";
                    }
                    if (referencedEntity != "")
                    {
                        result += er.Id.ToString();
                    }
                    else
                    {
                        result += er.LogicalName + ":" + er.Id.ToString();
                    }
                }
                return result;
            }
            else if (attribute is EntityCollection)
            {
                var result = "";
                if (((EntityCollection)attribute).Entities.Count > 0)
                {
                    foreach (var entity in ((EntityCollection)attribute).Entities)
                    {
                        if (result != "")
                        {
                            result += ",";
                        }
                        result += entity.Id.ToString();
                    }
                    result = ((EntityCollection)attribute).EntityName + ":" + result;
                }
                return result;
            }
            else if (attribute is OptionSetValue)
                return ((OptionSetValue)attribute).Value;
            else if (attribute is OptionSetValueCollection)
                return "[" + string.Join(",", ((OptionSetValueCollection)attribute).Select(v => v.Value.ToString())) + "]";
            else if (attribute is Money)
                return ((Money)attribute).Value;
            else if (attribute is BooleanManagedProperty)
                return ((BooleanManagedProperty)attribute).Value;
            else
                return attribute;
        }

        public static string AttributeToString(object attribute, AttributeMetadata meta)
        {
            if (attribute == null)
            {
                return "";
            }
            if (attribute is AliasedValue aliasedValue)
            {
                return AttributeToString(aliasedValue.Value, meta);
            }
            else if (attribute is EntityReference entityReference)
            {
                if (!string.IsNullOrEmpty(entityReference.Name))
                {
                    return entityReference.Name;
                }
                return entityReference.Id.ToString();
            }
            else if (attribute is EntityCollection entityCollection && entityCollection.EntityName == "activityparty")
            {
                var result = "";
                if (entityCollection.Entities.Count > 0)
                {
                    foreach (var entity in entityCollection.Entities)
                    {
                        var party = "";
                        if (entity.Contains("partyid") && entity["partyid"] is EntityReference)
                        {
                            party = ((EntityReference)entity["partyid"]).Name;
                        }
                        if (string.IsNullOrEmpty(party) && entity.Contains("addressused"))
                        {
                            party = entity["addressused"].ToString();
                        }
                        if (string.IsNullOrEmpty(party))
                        {
                            party = entity.Id.ToString();
                        }
                        if (!string.IsNullOrEmpty(result))
                        {
                            result += ", ";
                        }
                        result += party;
                    }
                }
                return result;
            }
            else if (attribute is OptionSetValue optionSetValue)
            {
                return GetOptionSetLabel(meta, optionSetValue.Value);
            }
            else if (attribute is OptionSetValueCollection optionSetValues)
            {
                return string.Join("; ", optionSetValues.Select(v => GetOptionSetLabel(meta, v.Value)));
            }
            else if (attribute is Money money)
            {
                return money.Value.ToString();
            }
            else if (attribute is BooleanManagedProperty booleanManagedProperty)
            {
                return booleanManagedProperty.Value.ToString();
            }
            else if (attribute is bool boolValue)
            {
                return (GetBooleanLabel(meta, boolValue));
            }
            return attribute.ToString();
        }

        private static string GetOptionSetLabel(AttributeMetadata meta, int value)
        {
            if (meta != null && meta is EnumAttributeMetadata)
            {
                foreach (var osv in ((EnumAttributeMetadata)meta).OptionSet.Options)
                {
                    if (osv.Value == value)
                    {
                        return osv.Label.UserLocalizedLabel.Label;
                    }
                }
            }
            return value.ToString();
        }

        private static string GetBooleanLabel(AttributeMetadata meta, bool value)
        {
            if (meta is BooleanAttributeMetadata bmeta)
            {
                if (value)
                {
                    return bmeta.OptionSet.TrueOption.Label.UserLocalizedLabel.Label;
                }
                else
                {
                    return bmeta.OptionSet.FalseOption.Label.UserLocalizedLabel.Label;
                }
            }
            return value.ToString();
        }

        internal static string Sep(Formatting format, int indent)
        {
            if (format == Formatting.None)
            {
                return "";
            }
            return "\n" + new string(' ', indent * 4);
        }

        private static object GetProperty(string type, string value)
        {
            switch (type)
            {
                case "String":
                case "Memo":
                    return value;
                case "Int32":
                case "Integer":
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return Int32.Parse(value);
                    }
                    break;
                case "Int64":
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return Int64.Parse(value);
                    }
                    break;
                case "OptionSetValue":
                case "Picklist":
                case "State":
                case "Status":
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return new OptionSetValue(int.Parse(value));
                    }
                    break;
                case "EntityReference":
                case "Lookup":
                case "Customer":
                case "Owner":
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var valueparts = value.Split(':');
                        string entity = valueparts[0];
                        value = valueparts[1];
                        Guid refId = StringToGuidish(value);
                        var entref = new EntityReference(entity, refId);
                        if (valueparts.Length > 2)
                        {
                            entref.Name = valueparts[2];
                        }
                        return entref;
                    }
                    break;
                case "DateTime":
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                    }
                    break;
                case "Boolean":
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return StringToBool(value);
                    }
                    break;
                case "Guid":
                case "Uniqueidentifier":
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        Guid uId = StringToGuidish(value);
                        return uId;
                    }
                    break;
                case "Decimal":
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return decimal.Parse(value);
                    }
                    break;
                case "Money":
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return new Money(decimal.Parse(value));
                    }
                    break;
                case "null":
                    return null;
                default:
                    throw new ArgumentOutOfRangeException("Type", type, "Cannot parse attibute type");
            }

            return null;
        }

        private static Guid StringToGuidish(string strId)
        {
            Guid id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(strId) &&
                !Guid.TryParse(strId, out id))
            {
                string template = guidtemplate;
                Guid.TryParse(template.Substring(0, 32 - strId.Length) + strId, out id);
            }
            return id;
        }

        private static bool StringToBool(string value)
        {
            if (value == "0")
            {
                return false;
            }
            else if (value == "1")
            {
                return true;
            }
            else
            {
                return bool.Parse(value);
            }
        }

        public static string GetXmlAttribute(XmlNode node, string attribute)
        {
            XmlAttribute xAtt = node.Attributes[attribute];
            if (xAtt != null)
            {
                return xAtt.Value;
            }
            else
            {
                return "";
            }
        }
    }
}
