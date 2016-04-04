using System;
using System.Text.RegularExpressions;

namespace MsCrmTools.FormParameterManager.AppCode
{
    public enum FormParameterType
    {
        Boolean,
        SafeString,
        DateTime,
        Double,
        Integer,
        Long,
        UnsignedInt,
        PositiveInteger,
        UniqueId,
        EntityType
    }

    public class FormParameter : ICloneable
    {
        private const string ParameterNameRegExp = "[a-zA-Z0-9]*_[a-zA-Z0-9]*";

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                var re = new Regex(ParameterNameRegExp);
                if (!re.Match(value).Success)
                {
                    throw new NotSupportedException("The name cannot start with underscore (_) or crm_. It must start with alphanumeric characters followed by underscore (_). For example, parameter_1 or 1_parameter. Name cannot contains hyphen (-), colon (:), semicolon (;), comma (,) or point(.).");
                }

                name = value;
            }
        }

        public CrmForm ParentForm { get; set; }
        public FormParameterType Type { get; set; }

        public object Clone()
        {
            return new FormParameter
            {
                Name = Name,
                Type = Type
            };
        }
    }
}