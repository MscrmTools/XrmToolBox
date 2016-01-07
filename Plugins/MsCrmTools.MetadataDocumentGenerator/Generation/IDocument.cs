using Microsoft.Xrm.Sdk;
using System.ComponentModel;

namespace MsCrmTools.MetadataDocumentGenerator.Generation
{
    internal interface IDocument
    {
        GenerationSettings Settings { get; set; }
        BackgroundWorker Worker { set; }

        void Generate(IOrganizationService service);
    }
}