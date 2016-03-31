using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace MsCrmTools.MetadataDocumentGenerator
{
    [Flags]
    public enum AttributeSelectionOption
    {
        AllAttributes = 0,
        AttributesOnForm = 1,
        AttributesNotOnForm = 2,
        AttributesOptionSet = 3,
        AttributeManualySelected = 4,
    }

    [Flags]
    public enum Output
    {
        Excel,
        Word
    }

    /// <summary>
    /// Class to store list of entities
    /// </summary>
    public class EntityItem
    {
        public EntityItem()
        {
            Forms = new List<Guid>();
            FormsDefinitions = new List<Entity>();
        }

        public List<string> Attributes { get; set; }
        public List<Guid> Forms { get; set; }

        [XmlIgnore]
        public List<Entity> FormsDefinitions { get; set; }

        public string Name { get; set; }
    }

    /// <summary>
    /// Settings for document generation
    /// </summary>
    public class GenerationSettings : ICloneable
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of class GenerationSettings
        /// </summary>
        public GenerationSettings()
        {
            EntitiesToProceed = new List<EntityItem>();
        }

        #endregion Constructor

        #region Properties

        public bool AddAuditInformation { get; set; }
        public bool AddEntitiesSummary { get; set; }
        public bool AddFieldSecureInformation { get; set; }
        public bool AddFormLocation { get; set; }
        public bool AddRequiredLevelInformation { get; set; }
        public bool AddValidForAdvancedFind { get; set; }
        public AttributeSelectionOption AttributesSelection { get; set; }
        public int DisplayNamesLangugageCode { get; set; }
        public List<EntityItem> EntitiesToProceed { get; set; }

        public string FilePath { get; set; }

        public bool IncludeOnlyAttributesOnForms { get; set; }
        public Output OutputDocumentType { get; set; }

        public List<string> Prefixes { get; set; }

        #endregion Properties

        #region Methods

        public static GenerationSettings CreateFromFile()
        {
            var ofDialog = new OpenFileDialog
            {
                Title = "Select a settings file",
                Filter = "Metadata Document Generator settings file|*.msettings"
            };

            if (ofDialog.ShowDialog() == DialogResult.OK)
            {
                using (var reader = new StreamReader(ofDialog.FileName))
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(reader.ReadToEnd());

                    return
                        (GenerationSettings)XmlSerializerHelper.Deserialize(doc.OuterXml, typeof(GenerationSettings));
                }
            }

            return new GenerationSettings();
        }

        public object Clone()
        {
            return new GenerationSettings
            {
                AddAuditInformation = AddAuditInformation,
                AddEntitiesSummary = AddEntitiesSummary,
                AddFieldSecureInformation = AddFieldSecureInformation,
                AddRequiredLevelInformation = AddRequiredLevelInformation,
                AddValidForAdvancedFind = AddValidForAdvancedFind,
                DisplayNamesLangugageCode = DisplayNamesLangugageCode,
                EntitiesToProceed = EntitiesToProceed,
                FilePath = FilePath,
                IncludeOnlyAttributesOnForms = IncludeOnlyAttributesOnForms,
                AttributesSelection = AttributesSelection,
                Prefixes = Prefixes
            };
        }

        public void SaveToFile()
        {
            var sfDialog = new SaveFileDialog
            {
                Title = "Select location to save the settings",
                Filter = "Metadata Document Generator settings file|*.msettings"
            };

            if (sfDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializerHelper.SerializeToFile(this, sfDialog.FileName);
            }
        }

        #endregion Methods
    }
}