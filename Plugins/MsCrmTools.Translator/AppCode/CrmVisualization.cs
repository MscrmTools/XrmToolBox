using System;
using System.Collections.Generic;

namespace MsCrmTools.Translator.AppCode
{
    public class CrmVisualization
    {
        public Dictionary<int, string> Descriptions { get; set; }
        public string Entity { get; set; }
        public Guid Id { get; set; }
        public Dictionary<int, string> Names { get; set; }
    }
}