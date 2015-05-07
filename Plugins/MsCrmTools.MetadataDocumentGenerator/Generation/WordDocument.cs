﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using GemBox.Document;
using GemBox.Document.Tables;
using GemBox.LicenseKey;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataDocumentGenerator.Helper;
using BorderStyle = GemBox.Document.BorderStyle;
using Orientation = GemBox.Document.Orientation;

namespace MsCrmTools.MetadataDocumentGenerator.Generation
{
    internal class WordDocument : IDocument
    {
        #region Variables

        /// <summary>
        /// Word document
        /// </summary>
        private readonly DocumentModel _innerDocument;

        /// <summary>
        /// Generation Settings
        /// </summary>
        private GenerationSettings _settings;
        private BackgroundWorker worker;

        private readonly List<EntityMetadata> emdCache;
        private IEnumerable<Entity> currentEntityForms; 

        private ParagraphStyle _title1;
        private ParagraphStyle _title2;
        private ParagraphStyle _cellHeader;
        private ParagraphStyle _cellContent;
        private ListStyle _numberList;
        private TableCellFormat _tcfHeader;
        private TableCellFormat _tcfContent;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of class WordDocument
        /// </summary>
        public WordDocument()
        {
            try
            {
                var k = new Key();
                ComponentInfo.SetLicense(k.WordKey);

                _innerDocument = new DocumentModel();
                emdCache = new List<EntityMetadata>();
                InitializesStyles();
            }
            catch (Exception error)
            {

                MessageBox.Show(error.ToString());
            }
        }

        #endregion Constructor

        public GenerationSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        public BackgroundWorker Worker
        {
            set { worker = value; }
        }

        #region Methods

        private void ReportProgress(int percentage, string message)
        {
            if (worker.WorkerReportsProgress)
                worker.ReportProgress(percentage, message);
        }

        public void Generate(IOrganizationService service)
        {

            int totalEntities = _settings.EntitiesToProceed.Count;
            int processed = 0;

            foreach (var entity in _settings.EntitiesToProceed)
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

                AddEntityMetadata(emd);

                var docs = MetadataHelper.RetrieveEntityForms(emd.LogicalName, entity.Forms, service);

                List<AttributeMetadata> amds = new List<AttributeMetadata>();

                if (_settings.AddFormLocation)
                {
                    currentEntityForms = MetadataHelper.RetrieveEntityFormList(emd.LogicalName, service);
                }

                switch (_settings.AttributesSelection)
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
                                _settings.EntitiesToProceed.First(y => y.Name == emd.LogicalName).Attributes.Contains(
                                    x.LogicalName)).ToList();
                        break;
                    case AttributeSelectionOption.AttributesOnForm:

                        var docsToProcess = docs.Where(d => entity.Forms.Contains(new Guid(d.SelectSingleNode("//formid").InnerText)));
                        foreach (var doc in docsToProcess)
                        {
                            amds.AddRange(emd.Attributes.Where(x =>
                                doc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") != null));
                        }
                        
                        break;
                    case AttributeSelectionOption.AttributesNotOnForm:
                       var docsToProcess2 = docs.Where(d => entity.Forms.Contains(new Guid(d.SelectSingleNode("//formid").InnerText)));
                       foreach (var doc in docsToProcess2)
                        {
                            amds.AddRange(emd.Attributes.Where(x =>
                                doc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") == null));
                        }break;
                }

                if (Settings.Prefixes != null && Settings.Prefixes.Count > 0)
                {
                    var filteredAmds = new List<AttributeMetadata>();

                    foreach (var prefix in Settings.Prefixes)
                    {
                        filteredAmds.AddRange(amds.Where(a => a.LogicalName.StartsWith(prefix) /*|| a.IsCustomAttribute.Value == false*/));
                    }

                    amds = filteredAmds;
                }


                AddAttribute(amds);
                processed++;
            }
            SaveDocument(_settings.FilePath);
        }

        private void InitializesStyles()
        {
            _title1 = (ParagraphStyle) Style.CreateStyle(StyleTemplateType.Heading1, _innerDocument);
            _title1.CharacterFormat.FontName = "Segoe UI";
            _title1.CharacterFormat.FontColor = new Color(18, 97, 225);

            _title2 = (ParagraphStyle) Style.CreateStyle(StyleTemplateType.Heading2, _innerDocument);
            _title2.CharacterFormat.FontName = "Segoe UI";
            _title2.CharacterFormat.FontColor = new Color(59, 59, 59);

            _innerDocument.Styles.Add(_title1);
            _innerDocument.Styles.Add(_title2);

            _cellHeader = (ParagraphStyle) Style.CreateStyle(StyleTemplateType.Normal, _innerDocument);
            _cellHeader.Name = "MyHeaderCells";
            _cellHeader.CharacterFormat.Bold = true;
            _cellHeader.CharacterFormat.FontName = "Segoe UI";
            _innerDocument.Styles.Add(_cellHeader);

            _cellContent = (ParagraphStyle) Style.CreateStyle(StyleTemplateType.Normal, _innerDocument);
            _cellContent.Name = "MyContentCells";
            _cellContent.CharacterFormat.FontName = "Segoe UI";
            _innerDocument.Styles.Add(_cellContent);

            _tcfHeader = new TableCellFormat();
            _tcfHeader.Borders.SetBorders(MultipleBorderTypes.Bottom, BorderStyle.Single, new Color(165, 172, 181), 1);
            _tcfHeader.Borders.SetBorders(MultipleBorderTypes.Left | MultipleBorderTypes.Right, BorderStyle.None,
                                          Color.White, 0);
            _tcfHeader.BackgroundColor = new Color(245, 247, 249);

            _tcfContent = new TableCellFormat();
            _tcfContent.Borders.SetBorders(MultipleBorderTypes.All, BorderStyle.None, new Color(255, 255, 255), 0);

            _numberList = new ListStyle("NumberWithDot", ListTemplateType.NumberWithDot);

            _innerDocument.Styles.Add(_numberList);
        }

        private TableRow CreateHeaderRow(IEnumerable<string> columns)
        {
            var row = new TableRow(_innerDocument);

            foreach (var column in columns)
            {
                row.Cells.Add(new TableCell(_innerDocument, new Paragraph(_innerDocument, column)
                                                                {
                                                                    ParagraphFormat =
                                                                        new ParagraphFormat {Style = _cellHeader}
                                                                })
                                  {
                                      CellFormat = _tcfHeader.Clone()
                                  });
            }

            return row;
        }

        private TableRow CreateContentRow(IEnumerable<string> values)
        {
            var row = new TableRow(_innerDocument);

            foreach (var value in values)
            {
                var transformedValue = value.Split('\n');

                var p = new Paragraph(_innerDocument)
                            {
                                ParagraphFormat = new ParagraphFormat {Style = _cellContent}
                            };

                foreach (var s in transformedValue)
                {
                    p.Inlines.Add(new Run(_innerDocument, s));
                    p.Inlines.Add(new SpecialCharacter(_innerDocument, SpecialCharacterType.LineBreak));
                }

                row.Cells.Add(new TableCell(_innerDocument, p)
                                  {
                                      CellFormat = _tcfContent.Clone()
                                  });
            }

            return row;
        }

        private Section AddHeading1(string text)
        {
            var p = new Paragraph(_innerDocument, text)
                        {
                            ParagraphFormat = new ParagraphFormat {Style = _title1},
                            ListFormat = {Style = _numberList}
                        };
            p.ParagraphFormat.NoSpaceBetweenParagraphsOfSameStyle = true;

            var section = new Section(_innerDocument, p);

            _innerDocument.Sections.Add(section);

            return section;
        }

        private void AddHeading2(Section section, string text)
        {
            var p = new Paragraph(_innerDocument, text)
                        {
                            ParagraphFormat = new ParagraphFormat {Style = _title2},
                            ListFormat = {Style = _numberList}
                        };

            p.ParagraphFormat.NoSpaceBetweenParagraphsOfSameStyle = true;
            p.ListFormat.ListLevelNumber++;

            section.Blocks.Add(p);
        }

        /// <summary>
        /// Adds metadata of an entity
        /// </summary>
        /// <param name="emd">Entity metadata</param>
        public void AddEntityMetadata(EntityMetadata emd)
        {
            var displayNameLabel = emd.DisplayName.LocalizedLabels.Count == 0
                                       ? null
                                       : emd.DisplayName.LocalizedLabels.FirstOrDefault(
                                           l => l.LanguageCode == _settings.DisplayNamesLangugageCode);
            var pluralDisplayNameLabel = emd.DisplayCollectionName.LocalizedLabels.Count == 0
                                             ? null
                                             : emd.DisplayCollectionName.LocalizedLabels.FirstOrDefault(
                                                 l => l.LanguageCode == _settings.DisplayNamesLangugageCode);
            var descriptionLabel = emd.Description.LocalizedLabels.Count == 0
                                       ? null
                                       : emd.Description.LocalizedLabels.FirstOrDefault(
                                           l => l.LanguageCode == _settings.DisplayNamesLangugageCode);

            var displayName = displayNameLabel != null ? displayNameLabel.Label : "Not Translated";
            var pluralDisplayName = pluralDisplayNameLabel != null ? pluralDisplayNameLabel.Label : "Not Translated";
            var description = descriptionLabel != null ? descriptionLabel.Label : "Not Translated";

            var section = AddHeading1("Entity: " + displayName);
            AddHeading2(section, "Metadata");

            var table = new Table(_innerDocument);
            table.TableFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single,
                                                 new Color(165, 172, 181), 1);

            table.Rows.Add(CreateHeaderRow(new[] {"Property", "Value"}));
            table.Rows.Add(CreateContentRow(new[] {"Display Name", displayName}));
            table.Rows.Add(CreateContentRow(new[] {"Plural Display Name", pluralDisplayName}));
            table.Rows.Add(CreateContentRow(new[] {"Description", description}));
            table.Rows.Add(CreateContentRow(new[] {"Schema Name", emd.SchemaName}));
            table.Rows.Add(CreateContentRow(new[] {"Logical Name", emd.LogicalName}));
            table.Rows.Add(
                CreateContentRow(new[]
                                     {
                                         "Object Type Code",
                                         emd.ObjectTypeCode != null
                                             ? emd.ObjectTypeCode.Value.ToString(CultureInfo.InvariantCulture)
                                             : string.Empty
                                     }));
            table.Rows.Add(
                CreateContentRow(new[]
                                     {
                                         "Is Custom Entity",
                                         (emd.IsCustomEntity != null && emd.IsCustomEntity.Value).ToString(
                                             CultureInfo.InvariantCulture)
                                     }));
            table.Rows.Add(
                CreateContentRow(new[]
                                     {
                                         "Ownership Type",
                                         emd.OwnershipType != null ? emd.OwnershipType.Value.ToString() : string.Empty
                                     }));

            table.TableFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single,
                                                 new Color(165, 172, 181), 1);

            section.Blocks.Add(table);
        }

        /// <summary>
        /// Add an attribute metadata
        /// </summary>
        /// <param name="attributeMetadataList">List of Attribute metadata</param>
        public void AddAttribute(IEnumerable<AttributeMetadata> attributeMetadataList)
        {
            var section = new Section(_innerDocument) {PageSetup = {Orientation = Orientation.Landscape}};
            _innerDocument.Sections.Add(section);
            AddHeading2(section, "Attributes");

            var table = new Table(_innerDocument);

            var header = new List<string>
                             {
                                 "Logical Name",
                                 "Schema Name",
                                 "Display Name",
                                 "Type",
                                 "Description",
                                 "Is Custom"
                             };

            foreach (var amd in attributeMetadataList.OrderBy(attr => attr.SchemaName))
            {
                var displayNameLabel = amd.DisplayName.LocalizedLabels.Count == 0
                                           ? null
                                           : amd.DisplayName.LocalizedLabels.FirstOrDefault(
                                               l => l.LanguageCode == _settings.DisplayNamesLangugageCode);
                var descriptionLabel = amd.Description.LocalizedLabels.Count == 0
                                           ? null
                                           : amd.Description.LocalizedLabels.FirstOrDefault(
                                               l => l.LanguageCode == _settings.DisplayNamesLangugageCode);


                var displayName = displayNameLabel != null ? displayNameLabel.Label : "Not Translated";
                var description = descriptionLabel != null ? descriptionLabel.Label : "Not Translated";

                var metadata = new List<string>
                                   {
                                       amd.LogicalName,
                                       amd.SchemaName,
                                       displayName,
                                       amd.AttributeType != null ? amd.AttributeType.Value.ToString() : string.Empty,
                                       description,
                                       amd.IsCustomAttribute != null
                                           ? amd.IsCustomAttribute.Value.ToString(CultureInfo.InvariantCulture)
                                           : string.Empty
                                   };

                if (_settings.AddRequiredLevelInformation)
                {
                    metadata.Add(amd.RequiredLevel.Value.ToString());
                    if (!header.Contains("Required Level")) header.Add("Required Level");
                }

                if (_settings.AddValidForAdvancedFind)
                {
                    metadata.Add(amd.IsValidForAdvancedFind.Value.ToString(CultureInfo.InvariantCulture));
                    if (!header.Contains("Valid for AF")) header.Add("Valid for AF");
                }

                if (_settings.AddAuditInformation)
                {
                    metadata.Add(amd.IsAuditEnabled.Value.ToString(CultureInfo.InvariantCulture));
                    if (!header.Contains("Audit Enabled")) header.Add("Audit Enabled");
                }

                if (_settings.AddFieldSecureInformation)
                {
                    metadata.Add(amd.IsSecured.Value.ToString(CultureInfo.InvariantCulture));
                    if (!header.Contains("Is Secured")) header.Add("Is Secured");
                }

                if (_settings.AddFormLocation)
                {
                    string data = string.Empty;

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

                            if (sectionNodes.Count > 0)
                            {
                                var sectionName = sectionNodes[0].SelectSingleNode("labels/label[@languagecode='" + _settings.DisplayNamesLangugageCode + "']").Attributes["description"].Value;

                                XmlNode tabNode = sectionNodes[0].SelectNodes("ancestor::tab")[0];
                                var tabName = tabNode.SelectSingleNode("labels/label[@languagecode='" + _settings.DisplayNamesLangugageCode + "']").Attributes["description"].Value;

                                if (data.Length > 0)
                                {
                                    data += "\n" + string.Format("{0}/{1}/{2}", formName, tabName, sectionName);
                                }
                                else
                                {
                                    data = string.Format("{0}/{1}/{2}", formName, tabName, sectionName);
                                }
                            }
                            else if (headerNodes.Count > 0)
                            {
                                if (data.Length > 0)
                                {
                                    data += "\n" + string.Format("{0}/Header", formName);
                                }
                                else
                                {
                                    data = string.Format("{0}/Header", formName);
                                }
                            }
                            else if (footerNodes.Count > 0)
                            {
                                if (data.Length > 0)
                                {
                                    data += "\n" + string.Format("{0}/Footer", formName);
                                }
                                else
                                {
                                    data = string.Format("{0}/Footer", formName);
                                }
                            }
                        }
                    }

                    metadata.Add(data);
                    if (!header.Contains("Form Location")) header.Add("Form Location");
                }

                metadata.Add(GetAddAdditionalData(amd));

                table.Rows.Add(CreateContentRow(metadata.ToArray<string>()));
            }

            header.Add("Additional data");
            table.Rows.Insert(0, CreateHeaderRow(header.ToArray<string>()));

            table.TableFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single,
                                                 new Color(165, 172, 181), 1);

            section.Blocks.Add(table);
        }

        /// <summary>
        /// Saves the current workbook
        /// </summary>
        /// <param name="path">Path where to save the document</param>
        public void SaveDocument(string path)
        {
            _innerDocument.Save(path, SaveOptions.DocxDefault);
        }

        private string GetAddAdditionalData(AttributeMetadata amd)
        {
            if (amd.AttributeType != null)
                switch (amd.AttributeType.Value)
                {
                    case AttributeTypeCode.BigInt:
                        {
                            var bamd = (BigIntAttributeMetadata) amd;

                            return string.Format(
                                "Minimum value: {0}\nMaximum value: {1}",
                                bamd.MinValue.HasValue
                                    ? bamd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A",
                                bamd.MaxValue.HasValue
                                    ? bamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A");
                        }
                    case AttributeTypeCode.Boolean:
                        {
                            var bamd = (BooleanAttributeMetadata) amd;

                            var trueLabel = bamd.OptionSet.TrueOption.Label.LocalizedLabels.Count == 0
                                                ? null
                                                : bamd.OptionSet.TrueOption.Label.LocalizedLabels.FirstOrDefault(
                                                    l => l.LanguageCode == _settings.DisplayNamesLangugageCode);
                            var falseLabel = bamd.OptionSet.FalseOption.Label.LocalizedLabels.Count == 0
                                                 ? null
                                                 : bamd.OptionSet.FalseOption.Label.LocalizedLabels.
                                                        FirstOrDefault(
                                                            l =>
                                                            l.LanguageCode == _settings.DisplayNamesLangugageCode);

                            return string.Format(
                                "True: {0}\nFalse: {1}\nDefault Value: {2}",
                                bamd.OptionSet.TrueOption == null
                                    ? "N/A"
                                    : trueLabel != null ? trueLabel.Label : "Not Translated",
                                bamd.OptionSet.FalseOption == null
                                    ? "N/A"
                                    : falseLabel != null ? falseLabel.Label : "Not Translated",
                                (bamd.DefaultValue != null && bamd.DefaultValue.Value).ToString(
                                    CultureInfo.InvariantCulture));
                        }
                    case AttributeTypeCode.Customer:
                        {
                            // Do Nothing
                        }
                        break;
                    case AttributeTypeCode.DateTime:
                        {
                            var damd = (DateTimeAttributeMetadata) amd;

                            return string.Format(
                                "Format: {0}",
                                damd.Format.HasValue ? damd.Format.Value.ToString() : "N/A");
                        }
                    case AttributeTypeCode.Decimal:
                        {
                            var damd = (DecimalAttributeMetadata) amd;

                            return string.Format(
                                "Minimum value: {0}\nMaximum value: {1}\nPrecision: {2}",
                                damd.MinValue.HasValue
                                    ? damd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A",
                                damd.MaxValue.HasValue
                                    ? damd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A",
                                damd.Precision.HasValue
                                    ? damd.Precision.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A");
                        }
                    case AttributeTypeCode.Double:
                        {
                            var damd = (DoubleAttributeMetadata) amd;

                            return string.Format(
                                "Minimum value: {0}\nMaximum value: {1}\nPrecision: {2}",
                                damd.MinValue.HasValue
                                    ? damd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A",
                                damd.MaxValue.HasValue
                                    ? damd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A",
                                damd.Precision.HasValue
                                    ? damd.Precision.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A");
                        }
                    case AttributeTypeCode.EntityName:
                        {
                            // Do nothing
                        }
                        break;
                    case AttributeTypeCode.Integer:
                        {
                            var iamd = (IntegerAttributeMetadata) amd;

                            return string.Format(
                                "Minimum value: {0}\nMaximum value: {1}",
                                iamd.MinValue.HasValue
                                    ? iamd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A",
                                iamd.MaxValue.HasValue
                                    ? iamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A");
                        }
                    case AttributeTypeCode.Lookup:
                        {
                            var lamd = (LookupAttributeMetadata) amd;

                            return lamd.Targets.Aggregate("Targets:", (current, entity) => current + ("\n" + entity));
                        }
                    case AttributeTypeCode.Memo:
                        {
                            var mamd = (MemoAttributeMetadata) amd;

                            return string.Format(
                                "Format: {0}\nMax length: {1}",
                                mamd.Format.HasValue ? mamd.Format.Value.ToString() : "N/A",
                                mamd.MaxLength.HasValue
                                    ? mamd.MaxLength.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A");
                        }
                    case AttributeTypeCode.Money:
                        {
                            var mamd = (MoneyAttributeMetadata) amd;

                            return string.Format(
                                "Minimum value: {0}\nMaximum value: {1}\nPrecision: {2}",
                                mamd.MinValue.HasValue
                                    ? mamd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A",
                                mamd.MaxValue.HasValue
                                    ? mamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A",
                                mamd.Precision.HasValue
                                    ? mamd.Precision.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A");
                        }
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
                            var pamd = (PicklistAttributeMetadata) amd;

                            var format = "Options:";

                            foreach (var omd in pamd.OptionSet.Options)
                            {
                                var optionLabel = omd.Label.LocalizedLabels.Count == 0
                                                      ? null
                                                      : omd.Label.LocalizedLabels.FirstOrDefault(
                                                          l =>
                                                          l.LanguageCode == _settings.DisplayNamesLangugageCode);

                                format += string.Format("\n{0}: {1}",
                                                        omd.Value,
                                                        optionLabel != null ? optionLabel.Label : "Not Translated");
                            }

                            format += string.Format("\nDefault: {0}",
                                                    pamd.DefaultFormValue.HasValue
                                                        ? pamd.DefaultFormValue.Value.ToString(
                                                            CultureInfo.InvariantCulture)
                                                        : "N/A");

                            return format;
                        }
                    case AttributeTypeCode.State:
                        {
                            var samd = (StateAttributeMetadata) amd;

                            var format = "States:";

                            foreach (var omd in samd.OptionSet.Options)
                            {
                                var optionLabel = omd.Label.LocalizedLabels.Count == 0
                                                      ? null
                                                      : omd.Label.LocalizedLabels.FirstOrDefault(
                                                          l =>
                                                          l.LanguageCode == _settings.DisplayNamesLangugageCode);

                                format += string.Format("\n{0}: {1}",
                                                        omd.Value,
                                                        optionLabel != null ? optionLabel.Label : "Not Translated");
                            }

                            return format;
                        }
                    case AttributeTypeCode.Status:
                        {
                            var samd = (StatusAttributeMetadata) amd;

                            string format = "States:";

                            foreach (var omd in samd.OptionSet.Options)
                            {
                                var optionLabel = omd.Label.LocalizedLabels.Count == 0
                                                      ? null
                                                      : omd.Label.LocalizedLabels.FirstOrDefault(
                                                          l =>
                                                          l.LanguageCode == _settings.DisplayNamesLangugageCode);

                                format += string.Format("\n{0}: {1}",
                                                        omd.Value,
                                                        optionLabel != null ? optionLabel.Label : "Not Translated");
                            }

                            return format;
                        }
                    case AttributeTypeCode.String:
                        {
                            var samd = (StringAttributeMetadata) amd;

                            return string.Format(
                                "Format: {0}\nMax length: {1}",
                                samd.Format.HasValue ? samd.Format.Value.ToString() : "N/A",
                                samd.MaxLength.HasValue
                                    ? samd.MaxLength.Value.ToString(CultureInfo.InvariantCulture)
                                    : "N/A");
                        }
                    case AttributeTypeCode.Uniqueidentifier:
                        {
                            // Do Nothing
                        }
                        break;
                }

            return string.Empty;
        }

        #endregion Methods
    }
}
