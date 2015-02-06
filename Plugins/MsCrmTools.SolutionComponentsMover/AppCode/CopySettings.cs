using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.SolutionComponentsMover.AppCode
{
    internal class CopySettings
    {
        public List<Entity> SourceSolutions { get; set; }
        public List<Entity> TargetSolutions { get; set; }

        public List<int> ComponentsTypes { get; set; }
    }
}
