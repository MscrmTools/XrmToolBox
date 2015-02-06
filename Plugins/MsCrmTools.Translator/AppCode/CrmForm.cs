using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsCrmTools.Translator.AppCode
{
    public class CrmForm
    {
        public Guid FormUniqueId { get; set; }
        public Guid Id { get; set; }
        public string Entity { get; set; }
        public Dictionary<int, string> Names { get; set; }
        public Dictionary<int, string> Descriptions { get; set; }  
    }
}
