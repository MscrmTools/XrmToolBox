using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataDocumentGenerator.Helper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace MsCrmTools.MetadataDocumentGenerator.Generation
{
    internal class ExcelDocument : IDocument
    {
        #region Variables

        private readonly List<EntityMetadata> emdCache;

        /// <summary>
        /// Excel workbook
        /// </summary>
        private readonly ExcelPackage innerWorkBook;

        /// <summary>
        /// Indicates if the header row of attributes for the current entity
        /// is already added
        /// </summary>
        private bool attributesHeaderAdded;

        private IEnumerable<Entity> currentEntityForms;

        private bool entitiesHeaderAdded;

        /// <summary>
        /// Line number where to write
        /// </summary>
        private int lineNumber;

        /// <summary>
        /// Generation Settings
        /// </summary>
        private GenerationSettings settings;

        private int summaryLineNumber;
        private BackgroundWorker worker;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class ExcelDocument
        /// </summary>
        public ExcelDocument()
        {
            emdCache = new List<EntityMetadata>();
            innerWorkBook = new ExcelPackage();
            lineNumber = 1;
            summaryLineNumber = 1;
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

        /// <summary>
        /// Add an attribute metadata
        /// </summary>
        /// <param name="amd">Attribute metadata</param>
        /// <param name="sheet">Worksheet where to write</param>
        public void AddAttribute(AttributeMetadata amd, ExcelWorksheet sheet)
        {
            var y = 1;

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

            var amdDisplayName = amd.DisplayName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
            sheet.Cells[lineNumber, y].Value = (amd.DisplayName.LocalizedLabels.Count == 0 ? "N/A" : amdDisplayName != null ? amdDisplayName.Label : "");
            y++;

            if (amd.AttributeType != null) sheet.Cells[lineNumber, y].Value = (amd.AttributeType.Value.ToString());
            y++;

            var amdDescription = amd.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
            sheet.Cells[lineNumber, y].Value = (amd.Description.LocalizedLabels.Count == 0 ? "N/A" : amdDescription != null ? amdDescription.Label : "");
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
                var entity = settings.EntitiesToProceed.FirstOrDefault(e => e.Name == amd.EntityLogicalName);
                if (entity != null)
                {
                    foreach (var form in entity.FormsDefinitions.Where(fd => entity.Forms.Contains(fd.Id) || entity.Forms.Count == 0))
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

                            if (sectionNodes.Count > 0)
                            {
                                if (sectionNodes[0].SelectSingleNode("labels/label[@languagecode='" + settings.DisplayNamesLangugageCode + "']") != null)
                                {
                                    var sectionName = sectionNodes[0].SelectSingleNode("labels/label[@languagecode='" + settings.DisplayNamesLangugageCode + "']").Attributes["description"].Value;

                                    XmlNode tabNode = sectionNodes[0].SelectNodes("ancestor::tab")[0];
                                    if (tabNode != null && tabNode.SelectSingleNode("labels/label[@languagecode='" + settings.DisplayNamesLangugageCode + "']") != null)
                                    {

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
                }

                sheet.Column(y).PageBreak = true;

                y++;
            }

            sheet.Column(y).PageBreak = true;

            AddAdditionalData(lineNumber, y, amd, sheet);
        }

        /// <summary>
        /// Adds metadata of an entity
        /// </summary>
        /// <param name="emd">Entity metadata</param>
        /// <param name="sheet">Worksheet where to write</param>
        public void AddEntityMetadata(EntityMetadata emd, ExcelWorksheet sheet)
        {
            attributesHeaderAdded = false;
            lineNumber = 1;

            sheet.Cells[lineNumber, 1].Value = "Entity";
            sheet.Cells[lineNumber, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[lineNumber, 1].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
            sheet.Cells[lineNumber, 1].Style.Font.Bold = true;
            var emdDisplayName = emd.DisplayName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
            sheet.Cells[lineNumber, 2].Value = emd.DisplayName.LocalizedLabels.Count == 0 ? emd.SchemaName : emdDisplayName != null ? emdDisplayName.Label : null;
            lineNumber++;

            sheet.Cells[lineNumber, 1].Value = ("Plural Display Name");
            sheet.Cells[lineNumber, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[lineNumber, 1].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
            sheet.Cells[lineNumber, 1].Style.Font.Bold = true;
            var emdDisplayCollectionName = emd.DisplayCollectionName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
            sheet.Cells[lineNumber, 2].Value = (emd.DisplayCollectionName.LocalizedLabels.Count == 0 ? "N/A" : emdDisplayCollectionName != null ? emdDisplayCollectionName.Label : null);
            lineNumber++;

            sheet.Cells[lineNumber, 1].Value = ("Description");
            sheet.Cells[lineNumber, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[lineNumber, 1].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
            sheet.Cells[lineNumber, 1].Style.Font.Bold = true;
            var emdDescription = emd.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
            sheet.Cells[lineNumber, 2].Value = (emd.Description.LocalizedLabels.Count == 0 ? "N/A" : emdDescription != null ? emdDescription.Label : null);
            lineNumber++;

            sheet.Cells[lineNumber, 1].Value = ("Schema Name");
            sheet.Cells[lineNumber, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[lineNumber, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[lineNumber, 1].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
            sheet.Cells[lineNumber, 1].Style.Font.Bold = true;
            sheet.Cells[lineNumber, 2].Value = (emd.SchemaName);
            lineNumber++;

            sheet.Cells[lineNumber, 1].Value = ("Logical Name");
            sheet.Cells[lineNumber, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[lineNumber, 1].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
            sheet.Cells[lineNumber, 1].Style.Font.Bold = true;
            sheet.Cells[lineNumber, 2].Value = (emd.LogicalName);
            lineNumber++;

            sheet.Cells[lineNumber, 1].Value = ("Object Type Code");
            sheet.Cells[lineNumber, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[lineNumber, 1].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
            sheet.Cells[lineNumber, 1].Style.Font.Bold = true;
            sheet.Cells[lineNumber, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            if (emd.ObjectTypeCode != null) sheet.Cells[lineNumber, 1].Value = (emd.ObjectTypeCode.Value);
            lineNumber++;

            sheet.Cells[lineNumber, 1].Value = ("Is Custom Entity");
            sheet.Cells[lineNumber, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[lineNumber, 1].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
            sheet.Cells[lineNumber, 1].Style.Font.Bold = true;
            sheet.Cells[lineNumber, 2].Value = (emd.IsCustomEntity != null && emd.IsCustomEntity.Value);
            sheet.Cells[lineNumber, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            lineNumber++;

            sheet.Cells[lineNumber, 1].Value = ("Ownership Type");
            sheet.Cells[lineNumber, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[lineNumber, 1].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
            sheet.Cells[lineNumber, 1].Style.Font.Bold = true;
            if (emd.OwnershipType != null) sheet.Cells[lineNumber, 2].Value = (emd.OwnershipType.Value);
            lineNumber++;
            lineNumber++;
        }

        public void AddEntityMetadataInLine(EntityMetadata emd, ExcelWorksheet sheet)
        {
            var y = 1;

            if (!entitiesHeaderAdded)
            {
                summaryLineNumber += 2;
                InsertEntityHeader(sheet, summaryLineNumber, y);
                entitiesHeaderAdded = true;
            }
            summaryLineNumber++;

            var emdDisplayName = emd.DisplayName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
            sheet.Cells[summaryLineNumber, y].Value = (emd.DisplayName.LocalizedLabels.Count == 0 ? emd.SchemaName : emdDisplayName != null ? emdDisplayName.Label : null);
            y++;

            var emdDisplayCollectionName = emd.DisplayCollectionName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
            sheet.Cells[summaryLineNumber, y].Value = (emd.DisplayCollectionName.LocalizedLabels.Count == 0 ? "N/A" : emdDisplayCollectionName != null ? emdDisplayCollectionName.Label : null);
            y++;

            var emdDescription = emd.Description.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
            sheet.Cells[summaryLineNumber, y].Value = (emd.Description.LocalizedLabels.Count == 0 ? "N/A" : emdDescription != null ? emdDescription.Label : null);
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

        /// <summary>
        /// Add a new worksheet
        /// </summary>
        /// <param name="displayName">Name of the worksheet</param>
        /// <param name="logicalName">Logical name of the entity</param>
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

            ExcelWorksheet sheet = null;
            int i = 1;
            do
            {
                try
                {
                    sheet = innerWorkBook.Workbook.Worksheets.Add(name);
                }
                catch (Exception)
                {
                    name = name.Substring(0, name.Length - 2) + "_" + i;
                    i++;
                }
            } while (sheet == null);

            return sheet;
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

            foreach (var entity in settings.EntitiesToProceed.OrderBy(e => e.Name))
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
                    var reResponse = (RetrieveEntityResponse)service.Execute(reRequest);

                    emdCache.Add(reResponse.EntityMetadata);
                    emd = reResponse.EntityMetadata;
                }

                if (settings.AddEntitiesSummary)
                {
                    AddEntityMetadataInLine(emd, summarySheet);
                }

                lineNumber = 1;

                var emdDisplayNameLabel = emd.DisplayName.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);

                var sheet = AddWorkSheet(emdDisplayNameLabel == null ? "N/A" : emdDisplayNameLabel.Label, emd.SchemaName);

                if (!settings.AddEntitiesSummary)
                {
                    AddEntityMetadata(emd, sheet);
                }

                if (settings.AddFormLocation)
                {
                    currentEntityForms = MetadataHelper.RetrieveEntityFormList(emd.LogicalName, service);
                }

                List<AttributeMetadata> amds = new List<AttributeMetadata>();

                switch (settings.AttributesSelection)
                {
                    case AttributeSelectionOption.AllAttributes:
                        amds = emd.Attributes.ToList();
                        break;

                    case AttributeSelectionOption.AttributesOptionSet:
                        amds =
                            emd.Attributes.Where(
                                x => x.AttributeType != null && (x.AttributeType.Value == AttributeTypeCode.Boolean
                                                                 || x.AttributeType.Value == AttributeTypeCode.Picklist
                                                                 || x.AttributeType.Value == AttributeTypeCode.State
                                                                 || x.AttributeType.Value == AttributeTypeCode.Status)).ToList();
                        break;

                    case AttributeSelectionOption.AttributeManualySelected:

                        amds =
                            emd.Attributes.Where(
                                x =>
                                settings.EntitiesToProceed.FirstOrDefault(y => y.Name == emd.LogicalName).Attributes.Contains(
                                    x.LogicalName)).ToList();
                        break;

                    case AttributeSelectionOption.AttributesOnForm:

                        // If no forms selected, we search attributes in all forms
                        if (entity.Forms.Count == 0)
                        {
                            foreach (var form in entity.FormsDefinitions)
                            {
                                var tempStringDoc = form.GetAttributeValue<string>("formxml");
                                var tempDoc = new XmlDocument();
                                tempDoc.LoadXml(tempStringDoc);

                                amds.AddRange(emd.Attributes.Where(x =>
                                    tempDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") !=
                                    null));
                            }
                        }
                        else
                        {
                            // else we parse selected forms
                            foreach (var formId in entity.Forms)
                            {
                                var form = entity.FormsDefinitions.FirstOrDefault(f => f.Id == formId);
                                var tempStringDoc = form.GetAttributeValue<string>("formxml");
                                var tempDoc = new XmlDocument();
                                tempDoc.LoadXml(tempStringDoc);

                                amds.AddRange(emd.Attributes.Where(x =>
                                    tempDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") !=
                                    null));
                            }
                        }

                        break;

                    case AttributeSelectionOption.AttributesNotOnForm:
                        // If no forms selected, we search attributes in all forms
                        if (entity.Forms.Count == 0)
                        {
                            foreach (var form in entity.FormsDefinitions)
                            {
                                var tempStringDoc = form.GetAttributeValue<string>("formxml");
                                var tempDoc = new XmlDocument();
                                tempDoc.LoadXml(tempStringDoc);

                                amds.AddRange(emd.Attributes.Where(x =>
                                    tempDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") ==
                                    null));
                            }
                        }
                        else
                        {
                            // else we parse selected forms
                            foreach (var formId in entity.Forms)
                            {
                                var form = entity.FormsDefinitions.FirstOrDefault(f => f.Id == formId);
                                var tempStringDoc = form.GetAttributeValue<string>("formxml");
                                var tempDoc = new XmlDocument();
                                tempDoc.LoadXml(tempStringDoc);

                                amds.AddRange(emd.Attributes.Where(x =>
                                    tempDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") ==
                                    null));
                            }
                        }

                        break;
                }


                if (settings.Prefixes != null && settings.Prefixes.Count > 0)
                {
                    var filteredAmds = new List<AttributeMetadata>();

                    foreach (var prefix in settings.Prefixes)
                    {
                        filteredAmds.AddRange(amds.Where(a => a.LogicalName.StartsWith(prefix) /*|| a.IsCustomAttribute.Value == false*/));
                    }

                    amds = filteredAmds;
                }

                if (amds.Any())
                {
                    foreach (var amd in amds.Distinct(new AttributeMetadataComparer()).OrderBy(a => a.LogicalName))
                    {
                        AddAttribute(emd.Attributes.FirstOrDefault(x => x.LogicalName == amd.LogicalName), sheet);
                    }
                }
                else
                {
                    Write("no attributes to display", sheet, 1, !settings.AddEntitiesSummary ? 10 : 1);
                }

                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                processed++;
            }

            if (settings.AddEntitiesSummary)
            {
                summarySheet.Cells[summarySheet.Dimension.Address].AutoFitColumns();
            }

            SaveDocument(settings.FilePath);
        }

        /// <summary>
        /// Saves the current workbook
        /// </summary>
        /// <param name="path">Path where to save the document</param>
        public void SaveDocument(string path)
        {
            innerWorkBook.File = new FileInfo(path);
            innerWorkBook.Save();
        }

        internal void Write(string message, ExcelWorksheet sheet, int x, int y)
        {
            sheet.Cells[x, y].Value = message;
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

                            sheet.Cells[x, y].Value = (string.Format(
                                "Minimum value: {0}\r\nMaximum value: {1}",
                                bamd.MinValue.HasValue ? bamd.MinValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A",
                                bamd.MaxValue.HasValue ? bamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A"));
                        }
                        break;

                    case AttributeTypeCode.Boolean:
                        {
                            var bamd = (BooleanAttributeMetadata)amd;

                            var bamdOptionSetTrue = bamd.OptionSet.TrueOption.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
                            var bamdOptionSetFalse = bamd.OptionSet.FalseOption.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);

                            sheet.Cells[x, y].Value = (string.Format(
                                "True: {0}\r\nFalse: {1}\r\nDefault Value: {2}",
                                bamdOptionSetTrue != null ? bamdOptionSetTrue.Label : "",
                                bamdOptionSetFalse != null ? bamdOptionSetFalse.Label : "",
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
                                var omdLocLabel = omd.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
                                if (omdLocLabel != null)
                                {
                                    var label = omdLocLabel.Label;

                                    format += string.Format("\r\n{0}: {1}",
                                                            omd.Value,
                                                            label);
                                }
                            }

                            format += string.Format("\r\nDefault: {0}", pamd.DefaultFormValue.HasValue && pamd.DefaultFormValue.Value != -1 ? pamd.DefaultFormValue.Value.ToString(CultureInfo.InvariantCulture) : "N/A");

                            sheet.Cells[x, y].Value = (format);
                        }
                        break;

                    case AttributeTypeCode.State:
                        {
                            var samd = (StateAttributeMetadata)amd;

                            string format = "States:";

                            foreach (var omd in samd.OptionSet.Options)
                            {
                                var omdLocLabel = omd.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
                                format += string.Format("\r\n{0}: {1}",
                                                        omd.Value,
                                                        omdLocLabel != null ? omdLocLabel.Label : "");
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
                                var omdLocLabel = omd.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == settings.DisplayNamesLangugageCode);
                                format += string.Format("\r\n{0}: {1}",
                                                        omd.Value,
                                                        omdLocLabel != null ? omdLocLabel.Label : "");
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

            for (int i = 1; i <= y + 1; i++)
            {
                sheet.Cells[x, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[x, i].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
                sheet.Cells[x, i].Style.Font.Bold = true;

                // TODO Voir si ca sert vraiment
                //sheet.Columns[i].AutoFit();
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
                sheet.Cells[x, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[x, i].Style.Fill.BackgroundColor.SetColor(Color.PowderBlue);
                sheet.Cells[x, i].Style.Font.Bold = true;
            }
        }

        private void ReportProgress(int percentage, string message)
        {
            if (worker.WorkerReportsProgress)
                worker.ReportProgress(percentage, message);
        }

        #endregion Methods
    }
}
