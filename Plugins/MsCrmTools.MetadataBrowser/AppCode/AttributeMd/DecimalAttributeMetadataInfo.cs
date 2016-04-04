﻿using Microsoft.Xrm.Sdk.Metadata;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    public class DecimalAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly DecimalAttributeMetadata amd;

        public DecimalAttributeMetadataInfo(DecimalAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public ImeMode ImeMode
        {
            get { return amd.ImeMode.Value; }
        }

        public decimal MaxValue
        {
            get { return amd.MaxValue.Value; }
        }

        public decimal MinValue
        {
            get { return amd.MinValue.Value; }
        }

        public decimal Precision
        {
            get { return amd.Precision.Value; }
        }
    }
}