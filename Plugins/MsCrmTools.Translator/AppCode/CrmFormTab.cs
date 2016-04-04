using System;
using System.Collections.Generic;

namespace MsCrmTools.Translator.AppCode
{
    public class CrmFormTab
    {
        public string Entity { get; set; }
        public string Form { get; set; }
        public Guid FormId { get; set; }
        public Guid FormUniqueId { get; set; }
        public Guid Id { get; set; }
        public Dictionary<int, string> Names { get; set; }
    }
}