using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;

namespace Console.Test
{
    internal class Program
    {
        private static void Export()
        {
            var service = new OrganizationService(CrmConnection.Parse("Url=http://dev-crm/SCAMPROD;"));

            //var doc = new WordDocumentDocX();
            //doc.Settings = new GenerationSettings
            //{
            //    FilePath = @"c:\temp\doc.docx",
            //    AddAuditInformation = true,
            //    AddEntitiesSummary = true,
            //    AddFieldSecureInformation = true,
            //    AddFormLocation = true,
            //    AddRequiredLevelInformation = true,
            //    AddValidForAdvancedFind = true,
            //    AttributesSelection = AttributeSelectionOption.AllAttributes,
            //    DisplayNamesLangugageCode = 1033,
            //    EntitiesToProceed = new List<EntityItem>{new EntityItem
            //    {
            //        Name = "scam_repartition"
            //    }},
            //    IncludeOnlyAttributesOnForms = false,
            //    OutputDocumentType = Output.Word,
            //};
            //doc.Generate(service);
        }

        private static void Import()
        {
            var service = new OrganizationService(CrmConnection.Parse("Url=http://10.0.0.4/TEST;Domain=ENTREPRISE;Username=administrateur;Password=pass@word1"));
        }

        private static void Main(string[] args)
        {
            Export();
        }
    }
}