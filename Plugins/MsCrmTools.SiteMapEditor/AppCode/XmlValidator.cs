// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace MsCrmTools.SiteMapEditor.AppCode
{
    /// <summary>
    /// Class that manages Xml validation against xsd file
    /// </summary>
    public class XmlValidator
    {
        private readonly List<string> messages = new List<string>();

        public void ShowCompileErrors(object sender, ValidationEventArgs args)
        {
            messages.Add(args.Message);
        }

        /// <summary>
        /// Validates the specified xml content
        /// </summary>
        /// <param name="sImportDocument">Xml content to validate</param>
        /// <returns>Error messages if any, else "true"</returns>
        public List<string> ValidateCrmImportDocument(string sImportDocument)
        {
            ValidationEventHandler eventHandler = ShowCompileErrors;
            XmlSchemaCollection myschemacoll = new XmlSchemaCollection();
            XmlValidatingReader vr;

            try
            {
                using (StreamReader schemaReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("MsCrmTools.SiteMapEditor.Resources.sitemap.xsd")))
                {
                    //Load the XmlValidatingReader.
                    vr = new XmlValidatingReader(schemaReader.BaseStream, XmlNodeType.Element, null);

                    vr.Schemas.Add(myschemacoll);
                    vr.ValidationType = ValidationType.Schema;

                    while (vr.Read())
                    {
                    }
                }

                return messages;
            }
            //This code catches any XML exceptions.
            catch (XmlException XmlExp)
            {
                messages.Add(XmlExp.Message);
                return messages;
            }
            //This code catches any XML schema exceptions.
            catch (XmlSchemaException XmlSchemaExp)
            {
                messages.Add(XmlSchemaExp.Message);
                return messages;
            }
            //This code catches any standard exceptions.
            catch (Exception GeneralExp)
            {
                messages.Add(GeneralExp.Message);
                return messages;
            }
            finally
            {
                vr = null;
                myschemacoll = null;
            }
        }
    }
}