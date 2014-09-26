using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Javista.XrmToolBox.ImportNN.AppCode
{
    class ImportFileSettings
    {
        public string FirstEntity { get; set; }

        public string SecondEntity { get; set; }

        public string Relationship { get; set; }

        public bool FirstAttributeIsGuid { get; set; }

        public bool SecondAttributeIsGuid { get; set; }

        public string FirstAttributeName { get; set; }

        public string SecondAttributeName { get; set; }
    }
}
