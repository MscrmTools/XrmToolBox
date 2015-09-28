// PROJECT : MsCrmTools.ScriptsFinder
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Collections.Generic;

namespace MsCrmTools.ScriptsFinder
{
    public class Script
    {
        public string Attribute { get; set; }
        public string AttributeLogicalName { get; set; }
        public List<string> Dependencies { get; set; }
        public string EntityLogicalName { get; set; }
        public string EntityName { get; set; }
        public string Event { get; set; }
        public bool HasProblem { get; set; }
        public bool? IsActive { get; set; }
        public string MethodCalled { get; set; }
        public string Name { get; set; }
        public string ScriptLocation { get; set; }
        public string Type { get; set; }
    }
}