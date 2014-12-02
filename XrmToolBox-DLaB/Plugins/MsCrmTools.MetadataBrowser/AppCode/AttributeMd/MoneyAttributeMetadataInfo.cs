﻿using Microsoft.Xrm.Sdk.Metadata;
using MsCrmTools.MetadataBrowser.AppCode.AttributeMd;

namespace MsCrmTools.MetadataBrowser.AppCode.AttributeMd
{
    // [TypeConverter(typeof(AttributeMetadataInfoConverter))]
    public class MoneyAttributeMetadataInfo : AttributeMetadataInfo
    {
        private readonly MoneyAttributeMetadata amd;

        public MoneyAttributeMetadataInfo(MoneyAttributeMetadata amd)
            : base(amd)
        {
            this.amd = amd;
        }

        public ImeMode ImeMode
        {
            get { return amd.ImeMode.Value; }
        }

        public string CalculationOf
        {
            get { return amd.CalculationOf; }
        }

        public double MaxValue
        {
            get { return amd.MaxValue.Value; }
        }

        public double MinValue
        {
            get { return amd.MinValue.Value; }
        }

        public decimal Precision
        {
            get { return amd.Precision.Value; }
        }
    }
}