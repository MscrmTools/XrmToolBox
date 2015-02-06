using System.Collections.Generic;
using System.Net;
using GemBox.Spreadsheet;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MsCrmTools.Translator;
using MsCrmTools.Translator.AppCode;
using Microsoft.Xrm.Client;

namespace Console.Test
{
    class Program
    {
       static void Main(string[] args)
        {
           
        }

        private static void Import()
        {
            var service = new OrganizationService(CrmConnection.Parse("Url=http://10.0.0.4/TEST;Domain=ENTREPRISE;Username=administrateur;Password=pass@word1"));
        }

        private static void Export()
        {
             var service = new OrganizationService(CrmConnection.Parse("Url=http://10.0.0.4/TEST;Domain=ENTREPRISE;Username=administrateur;Password=pass@word1"));
        }
    }
}
