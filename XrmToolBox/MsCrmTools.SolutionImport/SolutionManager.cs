// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.IO;
using System.ServiceModel;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace MsCrmTools.SolutionImport
{
    internal class SolutionManager
    {
        private readonly IOrganizationService innerService;
        private Guid importJob;
        private string importPath; //Added to allow for import log download
        private string zipFile; //moved to global so it can be reused int he import log download

        public SolutionManager(IOrganizationService service)
        {
            innerService = service;
        }

        public void ImportSolutionArchive(string archivePath, ImportSettings settings)
        {
            try
            {
                importPath = archivePath; //sets the global variable for the import path

                var request = new ImportSolutionRequest
                                  {
                                      ConvertToManaged = settings.ConvertToManaged,
                                      CustomizationFile = File.ReadAllBytes(archivePath),
                                      OverwriteUnmanagedCustomizations = settings.OverwriteUnmanagedCustomizations,
                                      PublishWorkflows = settings.Activate,
                                      ImportJobId = settings.ImportId
                                  };

                innerService.Execute(request);
            }
            catch (FaultException<OrganizationServiceFault> error)
            {
                throw new Exception("An error while importing archive: " + error.Message);
            }
            finally
            {
                if (settings.DownloadLog)
                {
                    DownloadLogFile(importPath, settings);
                } //Download the log file
            }
        }

        public void ImportSolutionFolder(ImportSettings settings)
        {
            try
            {
                var di = new DirectoryInfo(settings.Path);
                string folderToZip = di.FullName;
                zipFile = di.Name + ".zip";

                // Zip folder content
                ZipManager.ZipFiles(settings.Path, zipFile);

                ImportSolutionArchive(zipFile, settings);

                File.Delete(zipFile);
            }
            catch (FaultException<OrganizationServiceFault> error)
            {
                throw new Exception("An error while importing archive: " + error.Message);
            }
            catch (Exception error)
            {
                throw new Exception("An error while importing archive: " + error.Message);
            }
            finally
            {
                if (settings.DownloadLog)
                {
                    DownloadLogFile(zipFile, settings);
                } //Download the log file
            }
        }

        public void PublishAll()
        {
            try
            {
                var request = new PublishAllXmlRequest();
                innerService.Execute(request);
            }
            catch (FaultException<OrganizationServiceFault> error)
            {
                throw new Exception("An error while publishing archive: " + error.Message);
            }
        }

        //Downloads the import log file
        public void DownloadLogFile(string path, ImportSettings settings)
        {
            try
            {
                var importLogRequest = new RetrieveFormattedImportJobResultsRequest
                                           {
                                               ImportJobId = settings.ImportId
                                           };
                var importLogResponse =
                    (RetrieveFormattedImportJobResultsResponse) innerService.Execute(importLogRequest);
                DateTime time = DateTime.Now;
                string format = "yyyy_MM_dd__HH_mm";
                File.WriteAllText(path.Replace(".zip", " ") + time.ToString(format) + ".xml",
                                  importLogResponse.FormattedResults);
            }
            catch (Exception)
            {
                // Do nothing
            }
        }

        public int IsFinished(Guid importId)
        {
            try
            {
                var importJob = innerService.Retrieve("importjob", importId, new ColumnSet(true));
                if (importJob.Contains("completedon"))
                    return 100;

                if (importJob.Contains("progress"))
                    return Convert.ToInt32(importJob.GetAttributeValue<double>("progress"));

                return 0;
            }
            catch (Exception error)
            {
                System.Windows.Forms.MessageBox.Show(error.ToString());
                throw;
            }
        }
    }
}