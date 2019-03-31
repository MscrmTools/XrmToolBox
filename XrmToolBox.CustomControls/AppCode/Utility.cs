using System;
using System.Reflection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Linq;
using System.Xml;

namespace XrmToolBox.CustomControls
{
    internal class Utility
    {
        /// <summary>
        /// Helper method to retrieve a property value for a generic object using reflection, PropertyInfo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="p"></param>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(object data, PropertyInfo p, int languageCode = 1033)
        {
            T propValue = default(T);
            object dataValue = p.GetValue(data);

            if (dataValue != null)
            {
                if (dataValue is Guid)
                {
                    dataValue = ((Guid)dataValue).ToString("b");
                }
                else if (dataValue is AttributeTypeDisplayName)
                {
                    dataValue = ((AttributeTypeDisplayName)dataValue).Value;
                }
                else if (dataValue is BooleanManagedProperty)
                {
                    var boolean = (BooleanManagedProperty)dataValue;
                    dataValue = boolean.Value;

                }
                else if (dataValue is AttributeRequiredLevelManagedProperty)
                {
                    var reqLevel = (AttributeRequiredLevelManagedProperty)dataValue;
                    dataValue = reqLevel.Value;
                }
                else if (dataValue is AttributeTypeCode)
                {
                    var val = (AttributeTypeCode)dataValue;
                    dataValue = val.ToString();

                }
                else if (dataValue is String[])
                {
                    var val = (String[])dataValue;
                    if (val.Length > 0)
                    {
                        dataValue = val[0];
                    }
                }

                else if (dataValue is Microsoft.Xrm.Sdk.Label)
                {
                    var label = (Microsoft.Xrm.Sdk.Label)dataValue;
                    if (label.LocalizedLabels.Count > 0)
                    {
                        var localLabel = label.LocalizedLabels.Where(l => l.LanguageCode == languageCode).First();
                        if (localLabel != null)
                        {
                            dataValue = localLabel.Label;
                        }
                    }
                }
            }
            if (dataValue is IConvertible)
            {
                propValue = (T)Convert.ChangeType(dataValue, typeof(T));
            }

            return propValue;
        }
        

        /// <summary>
        /// Helper method to return the Type of a value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="showFriendlyNames"></param>
        /// <returns></returns>
        public static Type GetValueType(object value, bool showFriendlyNames)
        {
            if (value == null)
            {
                return typeof(string);
            }
            if (showFriendlyNames && !ValueTypeIsFriendly(value))
            {
                return typeof(string);
            }
            var basevalue = EntitySerializer.AttributeToBaseType(value);
            if (basevalue == null)
            {
                return typeof(string);
            }
            return basevalue.GetType();
        }


        internal static bool ValueTypeIsFriendly(object value)
        {
            return value is Int32 || value is decimal || value is double || value is string || value is Money;
        }
        /// <summary>
        /// Get the property type for the current property
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Type GetPropertyType(PropertyInfo p)
        {
            Type propType = typeof(string);
            object t = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;

            if (t is BooleanManagedProperty)
            {
                propType = typeof(bool);
            }
            return propType;
        }

        /// <summary>
        /// Helper method to validate Xml as well formed 
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static bool IsValidXml(string xmlString)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlString);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }
    }
}
