using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;
using MsCrmTools.MetadataBrowser.AppCode.OptionMd;
using System;
using System.ComponentModel;

namespace MsCrmTools.MetadataBrowser.AppCode.BooleanOptionSetMd
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BooleanOptionSetMetadataInfo
    {
        private readonly BooleanOptionSetMetadata amd;

        public BooleanOptionSetMetadataInfo(BooleanOptionSetMetadata amd)
        {
            this.amd = amd;
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LabelInfo Description
        {
            get { return new LabelInfo(amd.Description); }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public LabelInfo DisplayName
        {
            get { return new LabelInfo(amd.DisplayName); }
        }

        public string ExtensionData
        {
            get { return amd.ExtensionData != null ? amd.ExtensionData.ToString() : ""; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public OptionMetadataInfo FalseOption
        {
            get { return new OptionMetadataInfo(amd.FalseOption); }
        }

        public string IntroducedVersion
        {
            get { return amd.IntroducedVersion; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public BooleanManagedPropertyInfo IsCustomizable
        {
            get { return new BooleanManagedPropertyInfo(amd.IsCustomizable); }
        }

        public bool IsCustomOptionSet
        {
            get { return amd.IsCustomOptionSet.HasValue && amd.IsCustomOptionSet.Value; }
        }

        public bool IsGlobal
        {
            get { return amd.IsGlobal.HasValue && amd.IsGlobal.Value; }
        }

        public bool IsManaged
        {
            get { return amd.IsManaged.HasValue && amd.IsManaged.Value; }
        }

        public Guid MetadataId
        {
            get { return amd.MetadataId.Value; }
        }

        public string Name
        {
            get { return amd.Name; }
        }

        public OptionSetType OptionSetType
        {
            get { return amd.OptionSetType.Value; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public OptionMetadataInfo TrueOption
        {
            get { return new OptionMetadataInfo(amd.TrueOption); }
        }

        public override string ToString()
        {
            return amd.Name;
        }
    }
}