//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Messages;
//using Microsoft.Xrm.Sdk.Metadata;
//using MsCrmTools.MetadataDocumentGenerator.Helper;
//using Novacode;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Globalization;
//using System.Linq;
//using System.Windows.Forms;
//using System.Xml;
//using Table = Novacode.Table;
//using XmlDocument = System.Xml.XmlDocument;

//namespace MsCrmTools.MetadataDocumentGenerator.Generation
//{
//    public class WordDocumentDocX : IDocument
//    {
//        #region Variables

//        private readonly List<EntityMetadata> emdCache;

//        //private ParagraphStyle _cellContent;

//        //private ParagraphStyle _cellHeader;

//        /// <summary>
//        /// Word document
//        /// </summary>
//        private DocX _innerDocument;

//        //private ListStyle _numberList;

//        /// <summary>
//        /// Generation Settings
//        /// </summary>
//        private GenerationSettings _settings;

//        //private TableCellFormat _tcfContent;
//        //private TableCellFormat _tcfHeader;
//        //private ParagraphStyle _title1;
//        //private ParagraphStyle _title2;
//        private IEnumerable<Entity> currentEntityForms;

//        private BackgroundWorker worker;

//        #endregion Variables

//        #region Constructor

//        /// <summary>
//        /// Initializes a new instance of class WordDocument
//        /// </summary>
//        public WordDocumentDocX()
//        {
//            try
//            {
//                emdCache = new List<EntityMetadata>();
//                //InitializesStyles();
//            }
//            catch (Exception error)
//            {
//                MessageBox.Show(error.ToString());
//            }
//        }

//        #endregion Constructor

//        public GenerationSettings Settings
//        {
//            get { return _settings; }
//            set { _settings = value; }
//        }

//        public BackgroundWorker Worker
//        {
//            set { worker = value; }
//        }

//        #region Methods

//        /// <summary>
//        /// Add an attribute metadata
//        /// </summary>
//        /// <param name="attributeMetadataList">List of Attribute metadata</param>
//        public void AddAttribute(IEnumerable<AttributeMetadata> attributeMetadataList)
//        {
//            var p = _innerDocument.InsertParagraph();
//            p.Append("Attributes");
//            p.StyleName = "Heading2";

//            //var section = new Section(_innerDocument) { PageSetup = { Orientation = Orientation.Landscape } };

//            //        var table = new Table(_innerDocument);

//            var header = new List<string>
//                             {
//                                 "Logical Name",
//                                 "Schema Name",
//                                 "Display Name",
//                                 "Type",
//                                 "Description",
//                                 "Is Custom"
//                             };

//            var amds = attributeMetadataList.OrderBy(attr => attr.SchemaName).Distinct(new AttributeMetadataComparer());

//            var table = _innerDocument.AddTable(amds.Count(), 7);
//            int rowIndex = 0;

//            foreach (var amd in amds)
//            {
//                var displayNameLabel = amd.DisplayName.LocalizedLabels.Count == 0
//                                           ? null
//                                           : amd.DisplayName.LocalizedLabels.FirstOrDefault(
//                                               l => l.LanguageCode == _settings.DisplayNamesLangugageCode);
//                var descriptionLabel = amd.Description.LocalizedLabels.Count == 0
//                                           ? null
//                                           : amd.Description.LocalizedLabels.FirstOrDefault(
//                                               l => l.LanguageCode == _settings.DisplayNamesLangugageCode);

//                var displayName = displayNameLabel != null ? displayNameLabel.Label : "Not Translated";
//                var description = descriptionLabel != null ? descriptionLabel.Label : "Not Translated";

//                var metadata = new List<string>
//                                   {
//                                       amd.LogicalName,
//                                       amd.SchemaName,
//                                       displayName,
//                                       amd.AttributeType != null ? amd.AttributeType.Value.ToString() : string.Empty,
//                                       description,
//                                       amd.IsCustomAttribute != null
//                                           ? amd.IsCustomAttribute.Value.ToString(CultureInfo.InvariantCulture)
//                                           : string.Empty
//                                   };

//                if (_settings.AddRequiredLevelInformation)
//                {
//                    table.InsertColumn(0);
//                    metadata.Add(amd.RequiredLevel.Value.ToString());
//                    if (!header.Contains("Required Level")) header.Add("Required Level");
//                }

//                if (_settings.AddValidForAdvancedFind)
//                {
//                    table.InsertColumn(0);
//                    metadata.Add(amd.IsValidForAdvancedFind.Value.ToString(CultureInfo.InvariantCulture));
//                    if (!header.Contains("Valid for AF")) header.Add("Valid for AF");
//                }

//                if (_settings.AddAuditInformation)
//                {
//                    table.InsertColumn(0);
//                    metadata.Add(amd.IsAuditEnabled.Value.ToString(CultureInfo.InvariantCulture));
//                    if (!header.Contains("Audit Enabled")) header.Add("Audit Enabled");
//                }

//                if (_settings.AddFieldSecureInformation)
//                {
//                    table.InsertColumn(0);
//                    metadata.Add(amd.IsSecured.Value.ToString(CultureInfo.InvariantCulture));
//                    if (!header.Contains("Is Secured")) header.Add("Is Secured");
//                }

//                if (_settings.AddFormLocation)
//                {
//                    table.InsertColumn(0);
//                    string data = string.Empty;

//                    var entity = _settings.EntitiesToProceed.First(e => e.Name == amd.EntityLogicalName);

//                    foreach (var form in entity.FormsDefinitions.Where(fd => entity.Forms.Contains(fd.Id) || entity.Forms.Count == 0))
//                    {
//                        var formName = form.GetAttributeValue<string>("name");
//                        var xmlDocument = new XmlDocument();
//                        xmlDocument.LoadXml(form["formxml"].ToString());

//                        var controlNode = xmlDocument.SelectSingleNode("//control[@datafieldname='" + amd.LogicalName + "']");
//                        if (controlNode != null)
//                        {
//                            XmlNodeList sectionNodes = controlNode.SelectNodes("ancestor::section");
//                            XmlNodeList headerNodes = controlNode.SelectNodes("ancestor::header");
//                            XmlNodeList footerNodes = controlNode.SelectNodes("ancestor::footer");

//                            if (sectionNodes.Count > 0)
//                            {
//                                var sectionName = sectionNodes[0].SelectSingleNode("labels/label[@languagecode='" + _settings.DisplayNamesLangugageCode + "']").Attributes["description"].Value;

//                                XmlNode tabNode = sectionNodes[0].SelectNodes("ancestor::tab")[0];
//                                var tabName = tabNode.SelectSingleNode("labels/label[@languagecode='" + _settings.DisplayNamesLangugageCode + "']").Attributes["description"].Value;

//                                if (data.Length > 0)
//                                {
//                                    data += "\n" + string.Format("{0}/{1}/{2}", formName, tabName, sectionName);
//                                }
//                                else
//                                {
//                                    data = string.Format("{0}/{1}/{2}", formName, tabName, sectionName);
//                                }
//                            }
//                            else if (headerNodes.Count > 0)
//                            {
//                                if (data.Length > 0)
//                                {
//                                    data += "\n" + string.Format("{0}/Header", formName);
//                                }
//                                else
//                                {
//                                    data = string.Format("{0}/Header", formName);
//                                }
//                            }
//                            else if (footerNodes.Count > 0)
//                            {
//                                if (data.Length > 0)
//                                {
//                                    data += "\n" + string.Format("{0}/Footer", formName);
//                                }
//                                else
//                                {
//                                    data = string.Format("{0}/Footer", formName);
//                                }
//                            }
//                        }
//                    }

//                    metadata.Add(data);
//                    if (!header.Contains("Form Location")) header.Add("Form Location");
//                }

//                metadata.Add(GetAddAdditionalData(amd));

//                CreateAttributeContentRow(metadata.ToArray<string>(), table, rowIndex++);
//            }

//            header.Add("Additional data");
//            CreateHeaderRow(header.ToArray<string>(), table, true);

//            //table.TableFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Single,
//            //                                     new Color(165, 172, 181), 1);

//            //section.Blocks.Add(table);

//            p.InsertTableAfterSelf(table);
//        }

//        /// <summary>
//        /// Adds metadata of an entity
//        /// </summary>
//        /// <param name="emd">Entity metadata</param>
//        public void AddEntityMetadata(EntityMetadata emd)
//        {
//            var displayNameLabel = emd.DisplayName.LocalizedLabels.Count == 0
//                                       ? null
//                                       : emd.DisplayName.LocalizedLabels.FirstOrDefault(
//                                           l => l.LanguageCode == _settings.DisplayNamesLangugageCode);
//            var pluralDisplayNameLabel = emd.DisplayCollectionName.LocalizedLabels.Count == 0
//                                             ? null
//                                             : emd.DisplayCollectionName.LocalizedLabels.FirstOrDefault(
//                                                 l => l.LanguageCode == _settings.DisplayNamesLangugageCode);
//            var descriptionLabel = emd.Description.LocalizedLabels.Count == 0
//                                       ? null
//                                       : emd.Description.LocalizedLabels.FirstOrDefault(
//                                           l => l.LanguageCode == _settings.DisplayNamesLangugageCode);

//            var displayName = displayNameLabel != null ? displayNameLabel.Label : "Not Translated";
//            var pluralDisplayName = pluralDisplayNameLabel != null ? pluralDisplayNameLabel.Label : "Not Translated";
//            var description = descriptionLabel != null ? descriptionLabel.Label : "Not Translated";

//            _innerDocument.InsertParagraph()
//            .Append("Entity: " + displayName)
//            .StyleName = "Heading1";

//            _innerDocument.InsertParagraph()
//           .Append("Metadata")
//           .StyleName = "Heading2";

//            var table = _innerDocument.AddTable(9, 2);

//            CreateHeaderRow(new[] { "Property", "Value" }, table);

//            CreateContentRow(new[] { "Display Name", displayName }, table, 1);
//            CreateContentRow(new[] { "Plural Display Name", pluralDisplayName }, table, 2);
//            CreateContentRow(new[] { "Description", description }, table, 3);
//            CreateContentRow(new[] { "Schema Name", emd.SchemaName }, table, 4);
//            CreateContentRow(new[] { "Logical Name", emd.LogicalName }, table, 5);
//            CreateContentRow(new[]
//                                     {
//                                         "Object Type Code",
//                                         emd.ObjectTypeCode != null
//                                             ? emd.ObjectTypeCode.Value.ToString(CultureInfo.InvariantCulture)
//                                             : string.Empty
//                                     }, table, 6);
//            CreateContentRow(new[]
//                                     {
//                                         "Is Custom Entity",
//                                         (emd.IsCustomEntity != null && emd.IsCustomEntity.Value).ToString(
//                                             CultureInfo.InvariantCulture)
//                                     }, table, 7);
//            CreateContentRow(new[]
//                                     {
//                                         "Ownership Type",
//                                         emd.OwnershipType != null ? emd.OwnershipType.Value.ToString() : string.Empty
//                                     }, table, 8);

//            _innerDocument.Paragraphs.Last().InsertTableAfterSelf(table);
//        }

//        public void Generate(IOrganizationService service)
//        {
//            _innerDocument = DocX.Create(Settings.FilePath);

//            int totalEntities = _settings.EntitiesToProceed.Count;
//            int processed = 0;

//            foreach (var entity in _settings.EntitiesToProceed)
//            {
//                ReportProgress(processed * 100 / totalEntities, string.Format("Processing entity '{0}'...", entity.Name));

//                var emd = emdCache.FirstOrDefault(x => x.LogicalName == entity.Name);
//                if (emd == null)
//                {
//                    var reRequest = new RetrieveEntityRequest
//                                        {
//                                            LogicalName = entity.Name,
//                                            EntityFilters = EntityFilters.Entity | EntityFilters.Attributes
//                                        };
//                    var reResponse = (RetrieveEntityResponse)service.Execute(reRequest);

//                    emdCache.Add(reResponse.EntityMetadata);
//                    emd = reResponse.EntityMetadata;
//                }

//                AddEntityMetadata(emd);

//                List<AttributeMetadata> amds = new List<AttributeMetadata>();

//                if (_settings.AddFormLocation)
//                {
//                    currentEntityForms = MetadataHelper.RetrieveEntityFormList(emd.LogicalName, service);
//                }

//                switch (_settings.AttributesSelection)
//                {
//                    case AttributeSelectionOption.AllAttributes:
//                        amds = emd.Attributes.ToList();
//                        break;

//                    case AttributeSelectionOption.AttributesOptionSet:
//                        amds =
//                            emd.Attributes.Where(
//                                x => x.AttributeType != null && (x.AttributeType.Value == AttributeTypeCode.Boolean
//                                                                 || x.AttributeType.Value == AttributeTypeCode.Picklist
//                                                                 || x.AttributeType.Value == AttributeTypeCode.State
//                                                                 || x.AttributeType.Value == AttributeTypeCode.Status)).ToList();
//                        break;

//                    case AttributeSelectionOption.AttributeManualySelected:
//                        amds =
//                            emd.Attributes.Where(
//                                x =>
//                                _settings.EntitiesToProceed.First(y => y.Name == emd.LogicalName).Attributes.Contains(
//                                    x.LogicalName)).ToList();
//                        break;

//                    case AttributeSelectionOption.AttributesOnForm:

//                        // If no forms selected, we search attributes in all forms
//                        if (entity.Forms.Count == 0)
//                        {
//                            foreach (var form in entity.FormsDefinitions)
//                            {
//                                var tempStringDoc = form.GetAttributeValue<string>("formxml");
//                                var tempDoc = new XmlDocument();
//                                tempDoc.LoadXml(tempStringDoc);

//                                amds.AddRange(emd.Attributes.Where(x =>
//                                    tempDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") !=
//                                    null));
//                            }
//                        }
//                        else
//                        {
//                            // else we parse selected forms
//                            foreach (var formId in entity.Forms)
//                            {
//                                var form = entity.FormsDefinitions.First(f => f.Id == formId);
//                                var tempStringDoc = form.GetAttributeValue<string>("formxml");
//                                var tempDoc = new XmlDocument();
//                                tempDoc.LoadXml(tempStringDoc);

//                                amds.AddRange(emd.Attributes.Where(x =>
//                                    tempDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") !=
//                                    null));
//                            }
//                        }

//                        break;

//                    case AttributeSelectionOption.AttributesNotOnForm:
//                        // If no forms selected, we search attributes in all forms
//                        if (entity.Forms.Count == 0)
//                        {
//                            foreach (var form in entity.FormsDefinitions)
//                            {
//                                var tempStringDoc = form.GetAttributeValue<string>("formxml");
//                                var tempDoc = new XmlDocument();
//                                tempDoc.LoadXml(tempStringDoc);

//                                amds.AddRange(emd.Attributes.Where(x =>
//                                    tempDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") ==
//                                    null));
//                            }
//                        }
//                        else
//                        {
//                            // else we parse selected forms
//                            foreach (var formId in entity.Forms)
//                            {
//                                var form = entity.FormsDefinitions.First(f => f.Id == formId);
//                                var tempStringDoc = form.GetAttributeValue<string>("formxml");
//                                var tempDoc = new XmlDocument();
//                                tempDoc.LoadXml(tempStringDoc);

//                                amds.AddRange(emd.Attributes.Where(x =>
//                                    tempDoc.SelectSingleNode("//control[@datafieldname='" + x.LogicalName + "']") ==
//                                    null));
//                            }
//                        }

//                        break;
//                }

//                if (Settings.Prefixes != null && Settings.Prefixes.Count > 0)
//                {
//                    var filteredAmds = new List<AttributeMetadata>();

//                    foreach (var prefix in Settings.Prefixes)
//                    {
//                        filteredAmds.AddRange(amds.Where(a => a.LogicalName.StartsWith(prefix) /*|| a.IsCustomAttribute.Value == false*/));
//                    }

//                    amds = filteredAmds;
//                }

//                AddAttribute(amds);
//                processed++;
//            }
//            SaveDocument(_settings.FilePath);
//        }

//        /// <summary>
//        /// Saves the current workbook
//        /// </summary>
//        /// <param name="path">Path where to save the document</param>
//        public void SaveDocument(string path)
//        {
//            _innerDocument.Save();
//        }

//        private void CreateAttributeContentRow(IEnumerable<string> values, Table table, int rowIndex)
//        {
//            for (var i = 0; i < values.Count(); i++)
//            {
//                table.Rows[rowIndex].Cells[i].Paragraphs[0].Append(values.ElementAt(i));
//            }
//        }

//        private void CreateContentRow(IEnumerable<string> values, Table table, int rowIndex)
//        {
//            table.Rows[rowIndex].Cells[0].Paragraphs[0].Append(values.ElementAt(0));
//            table.Rows[rowIndex].Cells[1].Paragraphs[0].Append(values.ElementAt(1));
//        }

//        private void CreateHeaderRow(IEnumerable<string> columns, Table table, bool insertAtBeginning = false)
//        {
//            if (insertAtBeginning)
//            {
//                var row = table.InsertRow(0);

//                for (var i = 0; i < columns.Count(); i++)
//                {
//                    row.Cells[i].Paragraphs[0].Append(columns.ElementAt(i));
//                }
//            }
//            else
//            {
//                for (var i = 0; i < columns.Count(); i++)
//                {
//                    table.Rows[0].Cells[i].Paragraphs[0].Append(columns.ElementAt(i));
//                }
//            }
//        }

//        private string GetAddAdditionalData(AttributeMetadata amd)
//        {
//            if (amd.AttributeType != null)
//                switch (amd.AttributeType.Value)
//                {
//                    case AttributeTypeCode.BigInt:
//                        {
//                            var bamd = (BigIntAttributeMetadata)amd;

//                            return string.Format(
//                                "Minimum value: {0}\nMaximum value: {1}",
//                                bamd.MinValue.HasValue
//                                    ? bamd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A",
//                                bamd.MaxValue.HasValue
//                                    ? bamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A");
//                        }
//                    case AttributeTypeCode.Boolean:
//                        {
//                            var bamd = (BooleanAttributeMetadata)amd;

//                            var trueLabel = bamd.OptionSet.TrueOption.Label.LocalizedLabels.Count == 0
//                                                ? null
//                                                : bamd.OptionSet.TrueOption.Label.LocalizedLabels.FirstOrDefault(
//                                                    l => l.LanguageCode == _settings.DisplayNamesLangugageCode);
//                            var falseLabel = bamd.OptionSet.FalseOption.Label.LocalizedLabels.Count == 0
//                                                 ? null
//                                                 : bamd.OptionSet.FalseOption.Label.LocalizedLabels.
//                                                        FirstOrDefault(
//                                                            l =>
//                                                            l.LanguageCode == _settings.DisplayNamesLangugageCode);

//                            return string.Format(
//                                "True: {0}\nFalse: {1}\nDefault Value: {2}",
//                                bamd.OptionSet.TrueOption == null
//                                    ? "N/A"
//                                    : trueLabel != null ? trueLabel.Label : "Not Translated",
//                                bamd.OptionSet.FalseOption == null
//                                    ? "N/A"
//                                    : falseLabel != null ? falseLabel.Label : "Not Translated",
//                                (bamd.DefaultValue != null && bamd.DefaultValue.Value).ToString(
//                                    CultureInfo.InvariantCulture));
//                        }
//                    case AttributeTypeCode.Customer:
//                        {
//                            // Do Nothing
//                        }
//                        break;

//                    case AttributeTypeCode.DateTime:
//                        {
//                            var damd = (DateTimeAttributeMetadata)amd;

//                            return string.Format(
//                                "Format: {0}",
//                                damd.Format.HasValue ? damd.Format.Value.ToString() : "N/A");
//                        }
//                    case AttributeTypeCode.Decimal:
//                        {
//                            var damd = (DecimalAttributeMetadata)amd;

//                            return string.Format(
//                                "Minimum value: {0}\nMaximum value: {1}\nPrecision: {2}",
//                                damd.MinValue.HasValue
//                                    ? damd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A",
//                                damd.MaxValue.HasValue
//                                    ? damd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A",
//                                damd.Precision.HasValue
//                                    ? damd.Precision.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A");
//                        }
//                    case AttributeTypeCode.Double:
//                        {
//                            var damd = (DoubleAttributeMetadata)amd;

//                            return string.Format(
//                                "Minimum value: {0}\nMaximum value: {1}\nPrecision: {2}",
//                                damd.MinValue.HasValue
//                                    ? damd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A",
//                                damd.MaxValue.HasValue
//                                    ? damd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A",
//                                damd.Precision.HasValue
//                                    ? damd.Precision.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A");
//                        }
//                    case AttributeTypeCode.EntityName:
//                        {
//                            // Do nothing
//                        }
//                        break;

//                    case AttributeTypeCode.Integer:
//                        {
//                            var iamd = (IntegerAttributeMetadata)amd;

//                            return string.Format(
//                                "Minimum value: {0}\nMaximum value: {1}",
//                                iamd.MinValue.HasValue
//                                    ? iamd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A",
//                                iamd.MaxValue.HasValue
//                                    ? iamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A");
//                        }
//                    case AttributeTypeCode.Lookup:
//                        {
//                            var lamd = (LookupAttributeMetadata)amd;

//                            return lamd.Targets.Aggregate("Targets:", (current, entity) => current + ("\n" + entity));
//                        }
//                    case AttributeTypeCode.Memo:
//                        {
//                            var mamd = (MemoAttributeMetadata)amd;

//                            return string.Format(
//                                "Format: {0}\nMax length: {1}",
//                                mamd.Format.HasValue ? mamd.Format.Value.ToString() : "N/A",
//                                mamd.MaxLength.HasValue
//                                    ? mamd.MaxLength.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A");
//                        }
//                    case AttributeTypeCode.Money:
//                        {
//                            var mamd = (MoneyAttributeMetadata)amd;

//                            return string.Format(
//                                "Minimum value: {0}\nMaximum value: {1}\nPrecision: {2}",
//                                mamd.MinValue.HasValue
//                                    ? mamd.MinValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A",
//                                mamd.MaxValue.HasValue
//                                    ? mamd.MaxValue.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A",
//                                mamd.Precision.HasValue
//                                    ? mamd.Precision.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A");
//                        }
//                    case AttributeTypeCode.Owner:
//                        {
//                            // Do nothing
//                        }
//                        break;

//                    case AttributeTypeCode.PartyList:
//                        {
//                            // Do nothing
//                        }
//                        break;

//                    case AttributeTypeCode.Picklist:
//                        {
//                            var pamd = (PicklistAttributeMetadata)amd;

//                            var format = "Options:";

//                            foreach (var omd in pamd.OptionSet.Options)
//                            {
//                                var optionLabel = omd.Label.LocalizedLabels.Count == 0
//                                                      ? null
//                                                      : omd.Label.LocalizedLabels.FirstOrDefault(
//                                                          l =>
//                                                          l.LanguageCode == _settings.DisplayNamesLangugageCode);

//                                format += string.Format("\n{0}: {1}",
//                                                        omd.Value,
//                                                        optionLabel != null ? optionLabel.Label : "Not Translated");
//                            }

//                            format += string.Format("\nDefault: {0}",
//                                                    pamd.DefaultFormValue.HasValue
//                                                        ? pamd.DefaultFormValue.Value.ToString(
//                                                            CultureInfo.InvariantCulture)
//                                                        : "N/A");

//                            return format;
//                        }
//                    case AttributeTypeCode.State:
//                        {
//                            var samd = (StateAttributeMetadata)amd;

//                            var format = "States:";

//                            foreach (var omd in samd.OptionSet.Options)
//                            {
//                                var optionLabel = omd.Label.LocalizedLabels.Count == 0
//                                                      ? null
//                                                      : omd.Label.LocalizedLabels.FirstOrDefault(
//                                                          l =>
//                                                          l.LanguageCode == _settings.DisplayNamesLangugageCode);

//                                format += string.Format("\n{0}: {1}",
//                                                        omd.Value,
//                                                        optionLabel != null ? optionLabel.Label : "Not Translated");
//                            }

//                            return format;
//                        }
//                    case AttributeTypeCode.Status:
//                        {
//                            var samd = (StatusAttributeMetadata)amd;

//                            string format = "States:";

//                            foreach (var omd in samd.OptionSet.Options)
//                            {
//                                var optionLabel = omd.Label.LocalizedLabels.Count == 0
//                                                      ? null
//                                                      : omd.Label.LocalizedLabels.FirstOrDefault(
//                                                          l =>
//                                                          l.LanguageCode == _settings.DisplayNamesLangugageCode);

//                                format += string.Format("\n{0}: {1}",
//                                                        omd.Value,
//                                                        optionLabel != null ? optionLabel.Label : "Not Translated");
//                            }

//                            return format;
//                        }
//                    case AttributeTypeCode.String:
//                        {
//                            var samd = (StringAttributeMetadata)amd;

//                            return string.Format(
//                                "Format: {0}\nMax length: {1}",
//                                samd.Format.HasValue ? samd.Format.Value.ToString() : "N/A",
//                                samd.MaxLength.HasValue
//                                    ? samd.MaxLength.Value.ToString(CultureInfo.InvariantCulture)
//                                    : "N/A");
//                        }
//                    case AttributeTypeCode.Uniqueidentifier:
//                        {
//                            // Do Nothing
//                        }
//                        break;
//                }

//            return string.Empty;
//        }

//        private void ReportProgress(int percentage, string message)
//        {
//            if (worker != null && worker.WorkerReportsProgress)
//                worker.ReportProgress(percentage, message);
//        }

//        #endregion Methods
//    }
//}