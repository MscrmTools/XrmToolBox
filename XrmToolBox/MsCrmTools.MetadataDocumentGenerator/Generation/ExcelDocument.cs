using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Xml;
using GemBox.Spreadsheet;
using LicenseKey;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataDocumentGenerator.Helper;

namespace MsCrmTools.MetadataDocumentGenerator.Generation
{
    internal class ExcelDocument : IDocument
    {
        #region Variables

        /// <summary>
        /// Excel workbook
        /// </summary>
        readonly ExcelFile innerWorkBook;

        /// <summary>
        /// Generation Settings
        /// </summary>
        GenerationSettings settings;

        /// <summary>
        /// Line number where to write
        /// </summary>
        int lineNumber;

        private int summaryLineNumber;

        /// <summary>
        /// Indicates if the header row of attributes for the current entity
        /// is already added
        /// </summary>
        bool attributesHeaderAdded;

        bool entitiesHeaderAdded;

        private readonly List<EntityMetadata> emdCache;

        private BackgroundWorker worker;

        private IEnumerable<Entity> currentEntityForms; 

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class ExcelDocument
        /// </summary>
        public ExcelDocument()
        {
            var key = new Key();
            SpreadsheetInfo.SetLicense(key.Excel3Dot7LicenseKey);
            emdCache = new List<EntityMetadata>();
            innerWorkBook = new ExcelFile();
            lineNumber = 0;
            summaryLineNumber = 0;
        }

        #endregion Constructor

        #region Properties

        public GenerationSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public BackgroundWorker Worker
        {
            set { worker = value; }
        }

        #endregion Properties

        #region Methods

        private void ReportProgress(int percentage, string message)
        {
            if (worker.WorkerReportsProgress)
                worker.ReportProgress(percentage, message);
        }

        public void Generate(IOrganizationService service)
        {
            ExcelWorksheet summarySheet = null;
            if (settings.AddEntitiesSummary)
            {
                summaryLineNumber = 1;
                summarySheet = AddWorkSheet("Entities list");
            }
            int totalEntities = settings.EntitiesToProceed.Count;
            int processed = 0;

            foreach (var entity in settings.EntitiesToProceed)
            {
                ReportProgress(processed * 100 / totalEntities, string.Format("Processing entity '{0}'...", entity.Name));
             
                var emd = emdCache.FirstOrDefault(x => x.LogicalName == entity.Name);
                if (emd == null)
                {
                    var reRequest = new RetrieveEntityRequest
                                        {
                                            LogicalName = entity.Name,
                                            EntityFilters = EntityFilters.Entity | EntityFilters.Attributes
                                        };
                    var reResponse = (RetrieveEntityResponse) service.Execute(reRequest);

                    emdCache.Add(reResponse.EntityMetadata);
                    emd = reResponse.EntityMetadata;
                }

                if (settings.AddEntitiesSummary)
                {
                    AddEntityMetadataInLine(emd, summarySheet);
                } 

                lineNumber= 1;

                var displayNameLabel = emd.DisplayName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);

                var sheet = AddWorkSheet(displayNameLabel == null ? "N/A" : displayNameLabel.Label, emd.SchemaName);

                if (!settings.AddEntitiesSummary)
                {
                    AddEntityMetadata(emd, sheet);
                }

                var formsDoc = MetadataHelper.RetrieveEntityForms(emd.LogicalName, service);

                if (settings.AddFormLocation)
                {
                    currentEntityForms  = MetadataHelper.RetrieveEntityFormList(emd.LogicalName, service);
                }

                IEnumerable<AttributeMetadata> amds = new List<AttributeMetadata>();

                switch (settings.AttributesSelection)
                {
                    case AttributeSelectionOption.AllAttributes:
                        amds = emd.Attributes;
                        break;
                    case AttributeSelectionOption.AttributesOptionSet:
                        amds = emd.Attributes.Where(x => x.AttributeType != null && (x.AttributeType.Value == AttributeTypeCode.Boolean
                                                                                     || x.AttributeType.Value == AttributeTypeCode.Picklist
                                                                                     || x.AttributeType.Value == AttributeTypeCode.State
                                                                                     || x.AttributeType.Value == AttributeTypeCode.Status));
                        break;
                    case AttributeSelectionOption.AttributeManualySelected:
                        amds =
                            emd.Attributes.Where(
                                x =>
                                settings.EntitiesToProceed.First(y => y.Name == emd.LogicalName).Attributes.Contains(
                                    x.LogicalName));
                        break;
                    case AttributeSelectionOption.AttributesOnForm:
                        amds =
                            emd.Attributes.Where(
                                x =>
                                formsDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") != null);
                        break;
                    case AttributeSelectionOption.AttributesNotOnForm:
                        amds =
                            emd.Attributes.Where(
                                x =>
                                formsDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") == null);
                        break;
                }

                if (amds.Any())
                {
                    foreach (var amd in amds)
                    {
                        AddAttribute(emd.Attributes.First(x => x.LogicalName == amd.LogicalName), sheet);
                    }
                }
                else
                {
                    Write("no attributes to display", sheet, 1, !settings.AddEntitiesSummary ? 10 : 1);
                }

                processed++;
            }

            SaveDocument(settings.FilePath);
        }
       
        /// <summary>
        /// Add a new worksheet
        /// </summary>
        /// <param name="sheetName">Name of the worksheet</param>
        /// <returns></returns>
        public ExcelWorksheet AddWorkSheet(string displayName, string logicalName = null)
        {
            string name;

            if (logicalName != null)
            {
                if (logicalName.Length >= 26)
                {
                    name = logicalName;
                }
                else
                {
                            var remainingLength = 31 - 3 - 3 - logicalName.Length;
                name = string.Format("{0} ({1})",
                    remainingLength == 0
                        ? "..."
                        : (displayName.Length > remainingLength
                            ? displayName.Substring(0, remainingLength)
                            : displayName),
                    logicalName);
                }
            }
            else
                name = displayName;
            name = name
                .Replace(":", " ")
                .Replace("\\", " ")
                .Replace("/", " ")
                .Replace("?", " ")
                .Replace("*", " ")
                .Replace("[", " ")
                .Replace("]", " ");

            if (name.Length > 31)
                name = name.Substring(0, 31);

            attributesHeaderAdded = false;

            ExcelWorksheet sheet=null;
            int i = 1;
            do
            {
                try
                {
                    sheet = innerWorkBook.Worksheets.Add(name);
                }
                catch (Exception)
                {
                    name = name.Substring(0, name.Length - 2) + "_" + i;
                    i++;
                }
            } while (sheet == null);

            return sheet;
        }

        /// <summary>
        /// Adds metadata of an entity
        /// </summary>
        /// <param name="emd">Entity metadata</param>
        /// <param name="sheet">Worksheet where to write</param>
        public void AddEntityMetadata(EntityMetadata emd, ExcelWorksheet sheet)
        {
            attributesHeaderAdded = false;
            lineNumber = 0;

            sheet.Cells[lineNumber, 0].Value = "Entity";
            sheet.Cells[lineNumber, 0].Style.FillPattern.SetSolid(Color.PowderBlue);
            sheet.Cells[lineNumber, 0].Style.Font.Weight = ExcelFont.BoldWeight;
            sheet.Cells[lineNumber, 1].Value = emd.DisplayName.LocalizedLabels.Count == 0 ? emd.SchemaName : emd.DisplayName.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label;
            lineNumber++;

            sheet.Cells[lineNumber, 0].Value = ("Plural Display Name");
            sheet.Cells[lineNumber, 0].Style.FillPattern.SetSolid(Color.PowderBlue);
            sheet.Cells[lineNumber, 0].Style.Font.Weight = ExcelFont.BoldWeight;
            sheet.Cells[lineNumber, 1].Value = (emd.DisplayCollectionName.LocalizedLabels.Count == 0 ? "N/A" : emd.DisplayCollectionName.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
            lineNumber++;

            sheet.Cells[lineNumber, 0].Value = ("Description");
            sheet.Cells[lineNumber, 0].Style.FillPattern.SetSolid(Color.PowderBlue);
            sheet.Cells[lineNumber, 0].Style.Font.Weight = ExcelFont.BoldWeight;
            sheet.Cells[lineNumber, 1].Value = (emd.Description.LocalizedLabels.Count == 0 ? "N/A" : emd.Description.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
            lineNumber++;

            sheet.Cells[lineNumber, 0].Value = ("Schema Name");
            sheet.Cells[lineNumber, 0].Style.FillPattern.SetSolid(Color.PowderBlue);
            sheet.Cells[lineNumber, 0].Style.Font.Weight = ExcelFont.BoldWeight;
            sheet.Cells[lineNumber, 1].Value = (emd.SchemaName);
            lineNumber++;

            sheet.Cells[lineNumber, 0].Value = ("Logical Name");
            sheet.Cells[lineNumber, 0].Style.FillPattern.SetSolid(Color.PowderBlue);
            sheet.Cells[lineNumber, 0].Style.Font.Weight = ExcelFont.BoldWeight;
            sheet.Cells[lineNumber, 1].Value = (emd.LogicalName);
            lineNumber++;

            sheet.Cells[lineNumber, 0].Value = ("Object Type Code");
            sheet.Cells[lineNumber, 0].Style.FillPattern.SetSolid(Color.PowderBlue);
            sheet.Cells[lineNumber, 0].Style.Font.Weight = ExcelFont.BoldWeight;
            sheet.Cells[lineNumber, 1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
            if (emd.ObjectTypeCode != null) sheet.Cells[lineNumber, 1].Value = (emd.ObjectTypeCode.Value);
            lineNumber++;

            sheet.Cells[lineNumber, 0].Value = ("Is Custom Entity");
            sheet.Cells[lineNumber, 0].Style.FillPattern.SetSolid(Color.PowderBlue);
            sheet.Cells[lineNumber, 0].Style.Font.Weight = ExcelFont.BoldWeight;
            sheet.Cells[lineNumber, 1].Value = (emd.IsCustomEntity != null && emd.IsCustomEntity.Value);
            sheet.Cells[lineNumber, 1].Style.HorizontalAlignment = HorizontalAlignmentStyle.Left;
            lineNumber++;

            sheet.Cells[lineNumber, 0].Value = ("Ownership Type");
            sheet.Cells[lineNumber, 0].Style.FillPattern.SetSolid(Color.PowderBlue);
            sheet.Cells[lineNumber, 0].Style.Font.Weight = ExcelFont.BoldWeight;
            if (emd.OwnershipType != null) sheet.Cells[lineNumber, 1].Value = (emd.OwnershipType.Value);
            lineNumber++;
            lineNumber++;
        }

        /// <summary>
        /// Add an attribute metadata
        /// </summary>
        /// <param name="amd">Attribute metadata</param>
        /// <param name="sheet">Worksheet where to write</param>
        public void AddAttribute(AttributeMetadata amd, ExcelWorksheet sheet)
        {
            var y = 0;

            if (!attributesHeaderAdded)
            {
                InsertAttributeHeader(sheet, lineNumber, y);
                attributesHeaderAdded = true;
            }
            lineNumber++;

            sheet.Cells[lineNumber, y].Value = (amd.LogicalName);
            y++;

            sheet.Cells[lineNumber, y].Value = (amd.SchemaName);
            y++;

            sheet.Cells[lineNumber, y].Value = (amd.DisplayName.LocalizedLabels.Count == 0 ? "N/A" : amd.DisplayName.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
            y++;

            if (amd.AttributeType != null) sheet.Cells[lineNumber, y].Value = (amd.AttributeType.Value.ToString());
            y++;

            sheet.Cells[lineNumber, y].Value = (amd.Description.LocalizedLabels.Count == 0 ? "N/A" : amd.Description.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
            y++;

            sheet.Cells[lineNumber, y].Value = ((amd.IsCustomAttribute != null && amd.IsCustomAttribute.Value).ToString(CultureInfo.InvariantCulture));
            y++;

            if (settings.AddRequiredLevelInformation)
            {
                sheet.Cells[lineNumber, y].Value = (amd.RequiredLevel.Value.ToString());
                y++;
            }

            if (settings.AddValidForAdvancedFind)
            {
                sheet.Cells[lineNumber, y].Value = (amd.IsValidForAdvancedFind.Value.ToString(CultureInfo.InvariantCulture));
                y++;
            }

            if (settings.AddAuditInformation)
            {
                sheet.Cells[lineNumber, y].Value = (amd.IsAuditEnabled.Value.ToString(CultureInfo.InvariantCulture));
                y++;
            }

            if (settings.AddFieldSecureInformation)
            {
                sheet.Cells[lineNumber, y].Value = ((amd.IsSecured != null && amd.IsSecured.Value).ToString(CultureInfo.InvariantCulture));
                y++;
            }

            if (settings.AddFormLocation)
            {
                foreach (var form in currentEntityForms)
                {
                    var formName = form.GetAttributeValue<string>("name");
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(form["formxml"].ToString());

                    var controlNode = xmlDocument.SelectSingleNode("//control[@datafieldname='" + amd.LogicalName + "']");
                    if (controlNode != null)
                    {
                        XmlNodeList sectionNodes = controlNode.SelectNodes("ancestor::section");
                        XmlNodeList headerNodes = controlNode.SelectNodes("ancestor::header");
                        XmlNodeList footerNodes = controlNode.SelectNodes("ancestor::footer");

                        if(sectionNodes.Count > 0)
                        {
                            var sectionName = sectionNodes[0].SelectSingleNode("labels/label[@languagecode='" + settings.DisplayNamesLangugageCode + "']").Attributes["description"].Value;

                            XmlNode tabNode = sectionNodes[0].SelectNodes("ancestor::tab")[0];
                            var tabName = tabNode.SelectSingleNode("labels/label[@languagecode='" + settings.DisplayNamesLangugageCode + "']").Attributes["description"].Value;

                            if (sheet.Cells[lineNumber, y].Value != null)
                            {
                                sheet.Cells[lineNumber, y].Value = sheet.Cells[lineNumber, y].Value + "\r\n" + string.Format("{0}/{1}/{2}", formName, tabName, sectionName);
                            }
                            else
                            {
                                sheet.Cells[lineNumber, y].Value = string.Format("{0}/{1}/{2}", formName, tabName, sectionName);
                            }
                        }
                        else if (headerNodes.Count > 0)
                        {
                            if (sheet.Cells[lineNumber, y].Value != null)
                            {
                                sheet.Cells[lineNumber, y].Value = sheet.Cells[lineNumber, y].Value + "\r\n" + string.Format("{0}/Header", formName);
                            }
                            else
                            {
                                sheet.Cells[lineNumber, y].Value = string.Format("{0}/Header", formName);
                            }
                        }
                        else if (footerNodes.Count > 0)
                        {
                            if (sheet.Cells[lineNumber, y].Value != null)
                            {
                                sheet.Cells[lineNumber, y].Value = sheet.Cells[lineNumber, y].Value + "\r\n" + string.Format("{0}/Footer", formName);
                            }
                            else
                            {
                                sheet.Cells[lineNumber, y].Value = string.Format("{0}/Footer", formName);
                            }
                        }
                    }
                }

                y++;
            }

            AddAdditionalData(lineNumber, y, amd, sheet);
        }

        /// <summary>
        /// Saves the current workbook
        /// </summary>
        /// <param name="path">Path where to save the document</param>
        public void SaveDocument(string path)
        {
            innerWorkBook.Save(path, SaveOptions.XlsxDefault);
        }

        /// <summary>
        /// Adds attribute type specific metadata information
        /// </summary>
        /// <param name="x">Row number</param>
        /// <param name="y">Cell number</param>
        /// <param name="amd">Attribute metadata</param>
        /// <param name="sheet">Worksheet where to write</param>
        private void AddAdditionalData(int x, int y, AttributeMetadata amd, ExcelWorksheet sheet)
        {
            if (amd.AttributeType != null)
                switch (amd.AttributeType.Value)
                {
                    case AttributeTypeCode.BigInt:
                        {
                            var bamd = (BigIntAttributeMetadata)amd;

                            sheet.Cells[x,y].Value = (string.Format(
                                "Minimum value: {0}\r\nMaximum value: {1}",
                                bamd.MinValue.HasValue ? bamd.MinValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A",
                                bamd.MaxValue.HasValue ? bamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A"));
                        }
                        break;
                    case AttributeTypeCode.Boolean:
                        {
                            var bamd = (BooleanAttributeMetadata)amd;

                            sheet.Cells[x, y].Value = (string.Format(
                                "True: {0}\r\nFalse: {1}\r\nDefault Value: {2}",
                                bamd.OptionSet.TrueOption.Label.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label,
                                bamd.OptionSet.FalseOption.Label.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label,
                                bamd.DefaultValue.HasValue ? bamd.DefaultValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A"));
                        }
                        break;
                    case AttributeTypeCode.Customer:
                        {
                            // Do Nothing
                        }
                        break;
                    case AttributeTypeCode.DateTime:
                        {
                            var damd = (DateTimeAttributeMetadata)amd;

                            sheet.Cells[x, y].Value = (string.Format(
                                "Format: {0}",
                                damd.Format.HasValue ? damd.Format.Value.ToString() : "N/A"));
                        }
                        break;
                    case AttributeTypeCode.Decimal:
                        {
                            var damd = (DecimalAttributeMetadata)amd;

                            sheet.Cells[x, y].Value = (string.Format(
                                "Minimum value: {0}\r\nMaximum value: {1}\r\nPrecision: {2}",
                                damd.MinValue.HasValue ? damd.MinValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A",
                                damd.MaxValue.HasValue ? damd.MaxValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A",
                                damd.Precision.HasValue ? damd.Precision.Value.ToString(CultureInfo.InvariantCulture) : "N/A"));
                        }
                        break;
                    case AttributeTypeCode.Double:
                        {
                            var damd = (DoubleAttributeMetadata)amd;

                            sheet.Cells[x, y].Value = (string.Format(
                                "Minimum value: {0}\r\nMaximum value: {1}\r\nPrecision: {2}",
                                damd.MinValue.HasValue ? damd.MinValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A",
                                damd.MaxValue.HasValue ? damd.MaxValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A",
                                damd.Precision.HasValue ? damd.Precision.Value.ToString(CultureInfo.InvariantCulture) : "N/A"));
                        }
                        break;
                    case AttributeTypeCode.EntityName:
                        {
                            // Do nothing
                        }
                        break;
                    case AttributeTypeCode.Integer:
                        {
                            var iamd = (IntegerAttributeMetadata)amd;

                            sheet.Cells[x, y].Value = (string.Format(
                                "Minimum value: {0}\r\nMaximum value: {1}",
                                iamd.MinValue.HasValue ? iamd.MinValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A",
                                iamd.MaxValue.HasValue ? iamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A"));
                        }
                        break;
                    case AttributeTypeCode.Lookup:
                        {
                            var lamd = (LookupAttributeMetadata)amd;

                            var format = lamd.Targets.Aggregate("Targets:", (current, entity) => current + ("\r\n" + entity));

                            sheet.Cells[x, y].Value = (format);
                        }
                        break;
                    case AttributeTypeCode.Memo:
                        {
                            var mamd = (MemoAttributeMetadata)amd;

                            sheet.Cells[x, y].Value = (string.Format(
                                "Format: {0}\r\nMax length: {1}",
                                mamd.Format.HasValue ? mamd.Format.Value.ToString() : "N/A",
                                mamd.MaxLength.HasValue ? mamd.MaxLength.Value.ToString(CultureInfo.InvariantCulture) : "N/A"));
                        }
                        break;
                    case AttributeTypeCode.Money:
                        {
                            var mamd = (MoneyAttributeMetadata)amd;

                            sheet.Cells[x, y].Value = (string.Format(
                                "Minimum value: {0}\r\nMaximum value: {1}\r\nPrecision: {2}",
                                mamd.MinValue.HasValue ? mamd.MinValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A",
                                mamd.MaxValue.HasValue ? mamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A",
                                mamd.Precision.HasValue ? mamd.Precision.Value.ToString(CultureInfo.InvariantCulture) : "N/A"));
                        }
                        break;
                    case AttributeTypeCode.Owner:
                        {
                            // Do nothing
                        }
                        break;
                    case AttributeTypeCode.PartyList:
                        {
                            // Do nothing
                        }
                        break;
                    case AttributeTypeCode.Picklist:
                        {
                            var pamd = (PicklistAttributeMetadata)amd;

                            string format = "Options:";

                            foreach (var omd in pamd.OptionSet.Options)
                            {
                                format += string.Format("\r\n{0}: {1}",
                                                        omd.Value,
                                                        omd.Label.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
                            }

                            format += string.Format("\r\nDefault: {0}", pamd.DefaultFormValue.HasValue ? pamd.DefaultFormValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A");

                            sheet.Cells[x, y].Value = (format);
                        }
                        break;
                    case AttributeTypeCode.State:
                        {
                            var samd = (StateAttributeMetadata)amd;

                            string format = "States:";

                            foreach (var omd in samd.OptionSet.Options)
                            {
                                format += string.Format("\r\n{0}: {1}",
                                                        omd.Value,
                                                        omd.Label.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
                            }

                            sheet.Cells[x, y].Value = (format);
                        }
                        break;
                    case AttributeTypeCode.Status:
                        {
                            var samd = (StatusAttributeMetadata)amd;

                            string format = "States:";

                            foreach (OptionMetadata omd in samd.OptionSet.Options)
                            {
                                format += string.Format("\r\n{0}: {1}",
                                                        omd.Value,
                                                        omd.Label.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
                            }

                            sheet.Cells[x, y].Value = (format);
                        }
                        break;
                    case AttributeTypeCode.String:
                        {
                            var samd = amd as StringAttributeMetadata;
                            if (samd != null)
                            {
                                sheet.Cells[x, y].Value = (string.Format(
                                    "Format: {0}\r\nMax length: {1}",
                                    samd.Format.HasValue ? samd.Format.Value.ToString() : "N/A",
                                    samd.MaxLength.HasValue
                                        ? samd.MaxLength.Value.ToString(CultureInfo.InvariantCulture)
                                        : "N/A"));
                            }

                            var mamd = amd as MemoAttributeMetadata;
                            if (mamd != null)
                            {
                                sheet.Cells[x, y].Value = (string.Format(
                                    "Format: {0}\r\nMax length: {1}",
                                    mamd.Format.HasValue ? mamd.Format.Value.ToString() : "N/A",
                                    mamd.MaxLength.HasValue
                                        ? mamd.MaxLength.Value.ToString(CultureInfo.InvariantCulture)
                                        : "N/A"));
                            }
                        }
                        break;
                    case AttributeTypeCode.Uniqueidentifier:
                        {
                            // Do Nothing
                        }
                        break;
                }
        }

        /// <summary>
        /// Adds row header for attribute list
        /// </summary>
        /// <param name="sheet">Worksheet where to write</param>
        /// <param name="x">Row number</param>
        /// <param name="y">Cell number</param>
        private void InsertAttributeHeader(ExcelWorksheet sheet, int x, int y)
        {
            // Write the header
            sheet.Cells[x, y].Value = ("Logical Name");
            y++;

            sheet.Cells[x, y].Value = ("Schema Name");
            y++;

            sheet.Cells[x, y].Value = ("Display Name");
            y++;

            sheet.Cells[x, y].Value = ("Type");
            y++;

            sheet.Cells[x, y].Value = ("Description");
            y++;

            sheet.Cells[x, y].Value = ("Custom Attribute");
            y++;

            if (settings.AddRequiredLevelInformation)
            {
                sheet.Cells[x, y].Value = ("Required Level");
                y++;
            }

            if (settings.AddValidForAdvancedFind)
            {
                sheet.Cells[x, y].Value = ("ValidFor AdvancedFind");
                y++;
            }

            if (settings.AddAuditInformation)
            {
                sheet.Cells[x, y].Value = ("Audit Enabled");
                y++;
            }

            if (settings.AddFieldSecureInformation)
            {
                sheet.Cells[x, y].Value = ("Secured");
                y++;
            }

            if (settings.AddFormLocation)
            {
                sheet.Cells[x, y].Value = ("Form location");
                y++;
            }

            sheet.Cells[x, y].Value = ("Additional data");
            y++;

            for (int i = 0; i <= y; i++)
            {
                sheet.Cells[x, i].Style.FillPattern.SetSolid(Color.PowderBlue);
                sheet.Cells[x, i].Style.Font.Weight = ExcelFont.BoldWeight;
                sheet.Columns[i].AutoFit();
            }
        }

        /// <summary>
        /// Adds row header for attribute list
        /// </summary>
        /// <param name="sheet">Worksheet where to write</param>
        /// <param name="x">Row number</param>
        /// <param name="y">Cell number</param>
        private void InsertEntityHeader(ExcelWorksheet sheet, int x, int y)
        {
            // Write the header
            sheet.Cells[x, y].Value = ("Entity");
            y++;

            sheet.Cells[x, y].Value = ("Plural Display Name");
            y++;

            sheet.Cells[x, y].Value = ("Description");
            y++;

            sheet.Cells[x, y].Value = ("Schema Name");
            y++;

            sheet.Cells[x, y].Value = ("Logical Name");
            y++;

            sheet.Cells[x, y].Value = ("Object Type Code");
            y++;

            sheet.Cells[x, y].Value = ("Is Custom Entity");
            y++;

            sheet.Cells[x, y].Value = ("Ownership Type");
            y++;

            for (int i = 1; i < y; i++)
            {
                sheet.Cells[x, i].Style.FillPattern.SetSolid(Color.PowderBlue);
                sheet.Cells[x, i].Style.Font.Weight = ExcelFont.BoldWeight;
            }
        }

        public void AddEntityMetadataInLine(EntityMetadata emd, ExcelWorksheet sheet)
        {
            var y = 0;

            if (!entitiesHeaderAdded)
            {
                summaryLineNumber += 2;
                InsertEntityHeader(sheet, summaryLineNumber, y);
                entitiesHeaderAdded = true;
            }
            summaryLineNumber++;

            sheet.Cells[summaryLineNumber, y].Value = (emd.DisplayName.LocalizedLabels.Count == 0 ? emd.SchemaName : emd.DisplayName.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
            y++;

            sheet.Cells[summaryLineNumber, y].Value = (emd.DisplayCollectionName.LocalizedLabels.Count == 0 ? "N/A" : emd.DisplayCollectionName.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
            y++;

            sheet.Cells[summaryLineNumber, y].Value = (emd.Description.LocalizedLabels.Count == 0 ? "N/A" : emd.Description.LocalizedLabels.First(l => l.LanguageCode == settings.DisplayNamesLangugageCode).Label);
            y++;

            sheet.Cells[summaryLineNumber, y].Value = (emd.SchemaName);
            y++;

            sheet.Cells[summaryLineNumber, y].Value = (emd.LogicalName);
            y++;

            if (emd.ObjectTypeCode != null) sheet.Cells[summaryLineNumber, y].Value = (emd.ObjectTypeCode.Value);
            y++;

            sheet.Cells[summaryLineNumber, y].Value = (emd.IsCustomEntity != null && emd.IsCustomEntity.Value);
            y++;

            if (emd.OwnershipType != null) sheet.Cells[summaryLineNumber, y].Value = (emd.OwnershipType.Value);
        }

        internal void Write(string message, ExcelWorksheet sheet, int x, int y)
        {
            sheet.Cells[x, y].Value = message;
        }

        #endregion Methods
    }
}
