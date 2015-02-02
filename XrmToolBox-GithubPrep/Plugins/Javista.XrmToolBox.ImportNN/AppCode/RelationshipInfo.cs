using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Metadata;

namespace Javista.XrmToolBox.ImportNN.AppCode
{
    internal class RelationshipInfo
    {
        public ManyToManyRelationshipMetadata Metadata { get; set; }
        public override string ToString()
        {
            return string.Format("{0} ({1} - {2})",
                Metadata.SchemaName,
                Metadata.Entity1LogicalName,
                Metadata.Entity2LogicalName);
        }
    }
}
