using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.MetadataDocumentGenerator.Generation
{
    interface IDocument
    {
        GenerationSettings Settings { get; set; }
        BackgroundWorker Worker { set; }
        void Generate(IOrganizationService service);
    }
}
