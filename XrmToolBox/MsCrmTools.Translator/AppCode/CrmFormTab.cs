using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsCrmTools.Translator.AppCode
{
    public class CrmFormTab
    {
        public Guid Id { get; set; }
        public string Entity { get; set; }
        public string Form { get; set; }
        public Dictionary<int, string> Names { get; set; }

        public Guid FormUniqueId { get; set; }
        public Guid FormId { get; set; }
    }
}
