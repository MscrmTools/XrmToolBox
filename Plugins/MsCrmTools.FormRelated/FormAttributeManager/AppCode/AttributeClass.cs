using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace MsCrmTools.FormAttributeManager.AppCode
{
    internal class AttributeClass
    {
        public static Guid GetClassId(AttributeMetadata amd)
        {
            if (amd.AttributeType == null)
            {
                throw new ArgumentNullException(typeof(AttributeMetadata).ToString());
            }

            switch (amd.AttributeType.Value)
            {
                case AttributeTypeCode.Boolean:
                    return new Guid("{67FAC785-CD58-4f9f-ABB3-4B7DDC6ED5ED}");

                case AttributeTypeCode.DateTime:
                    return new Guid("{5B773807-9FB2-42db-97C3-7A91EFF8ADFF}");

                case AttributeTypeCode.Decimal:
                    return new Guid("{C3EFE0C3-0EC6-42be-8349-CBD9079DFD8E}");

                case AttributeTypeCode.Money:
                    return new Guid("{533B9E00-756B-4312-95A0-DC888637AC78}");

                case AttributeTypeCode.Integer:
                    {
                        switch (((IntegerAttributeMetadata)amd).Format)
                        {
                            case IntegerFormat.Duration:
                                return new Guid("{AA987274-CE4E-4271-A803-66164311A958}");

                            case IntegerFormat.Language:
                                return new Guid("{671A9387-CA5A-4d1e-8AB7-06E39DDCF6B5}");

                            case IntegerFormat.TimeZone:
                                return new Guid("{7C624A0B-F59E-493d-9583-638D34759266}");

                            case IntegerFormat.None:
                                return new Guid("{C6D124CA-7EDA-4a60-AEA9-7FB8D318B68F}");

                            default:
                                throw new Exception("Unsupported format " + ((IntegerAttributeMetadata)amd).Format + " for type " + amd.AttributeType.Value);
                        }
                    }
                case AttributeTypeCode.String:
                    {
                        switch (((StringAttributeMetadata)amd).Format)
                        {
                            case StringFormat.Email:
                                return new Guid("{ADA2203E-B4CD-49be-9DDF-234642B43B52}");

                            case StringFormat.TextArea:
                                return new Guid("{6F3FB987-393B-4d2d-859F-9D0F0349B6AD}");

                            case StringFormat.Text:
                                return new Guid("{4273EDBD-AC1D-40d3-9FB2-095C621B552D}");

                            case StringFormat.Url:
                                return new Guid("{71716B6C-711E-476c-8AB8-5D11542BFB47}");

                            default:
                                throw new Exception("Unsupported format " + ((StringAttributeMetadata)amd).Format + " for type " + amd.AttributeType.Value);
                        }
                    }
                case AttributeTypeCode.Lookup:
                    return new Guid("{270BD3DB-D9AF-4782-9025-509E298DEC0A}");

                case AttributeTypeCode.Memo:
                    return new Guid("{E0DECE4B-6FC8-4a8f-A065-082708572369}");

                case AttributeTypeCode.Picklist:
                    return new Guid("{3EF39988-22BB-4f0b-BBBE-64B5A3748AEE}");

                default:
                    throw new Exception("Unsupported type " + amd.AttributeType.Value);
            }
        }
    }
}