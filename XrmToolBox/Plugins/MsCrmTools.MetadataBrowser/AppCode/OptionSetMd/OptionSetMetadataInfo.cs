﻿using System;
using System.ComponentModel;
using System.Drawing.Design;

using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.LabelMd;
using MsCrmTools.MetadataBrowser.AppCode.OptionMd;
using OptionMetadataCollection = MsCrmTools.MetadataBrowser.AppCode.OptionMd.OptionMetadataCollection;

namespace MsCrmTools.MetadataBrowser.AppCode.OptionSetMd
{
    [TypeConverter(typeof (OptionSetAttributeMetadataInfoConverter))]
    public class OptionSetMetadataInfo
    {
        private readonly OptionSetMetadata amd;

        public OptionSetMetadataInfo(OptionSetMetadata amd)
        {
            this.amd = amd;
        }

        [TypeConverter(typeof (LabelInfoConverter))]
        public LabelInfo Description
        {
            get { return new LabelInfo(amd.Description); }
        }

        [TypeConverter(typeof (LabelInfoConverter))]
        public LabelInfo DisplayName
        {
            get { return new LabelInfo(amd.DisplayName); }
        }

        public string ExtensionData
        {
            get { return amd.ExtensionData != null ? amd.ExtensionData.ToString() : ""; }
        }

        public string Name
        {
            get { return amd.Name; }
        }

        public string IntroducedVersion
        {
            get { return amd.IntroducedVersion; }
        }

        public bool IsCustomOptionSet
        {
            get { return amd.IsCustomOptionSet.HasValue && amd.IsCustomOptionSet.Value; }
        }

        [TypeConverter(typeof(BooleanManagedPropertyInfoConverter))]
        public BooleanManagedPropertyInfo IsCustomizable
        {
            get { return new BooleanManagedPropertyInfo(amd.IsCustomizable); }
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

        public OptionSetType OptionSetType
        {
            get { return amd.OptionSetType.Value; }
        }

        [Editor(typeof (CustomCollectionEditor), typeof (UITypeEditor))]
        [TypeConverter(typeof (OptionMetadataCollectionConverter))]
        public OptionMetadataCollection Options
        {
            get
            {
                var collec = new OptionMetadataCollection();
                foreach (OptionMetadata omd in amd.Options)
                {
                    collec.Add(new OptionMetadataInfo(omd));
                }

                return collec;
            }
        }
    }
}