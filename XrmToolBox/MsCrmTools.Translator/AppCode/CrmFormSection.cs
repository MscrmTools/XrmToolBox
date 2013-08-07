﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsCrmTools.Translator.AppCode
{
    public class CrmFormSection
    {
        public Guid Id { get; set; }
        public string Entity { get; set; }
        public string Form { get; set; }
        public string Tab { get; set; }
        public Dictionary<int, string> Names { get; set; }
        public Dictionary<int, string> Descriptions { get; set; }
        public Guid FormId { get; set; }
    }
}
