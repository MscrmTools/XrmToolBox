// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;

namespace MsCrmTools.SolutionImport
{
    internal class SolutionManager
    {
        private readonly IOrganizationService innerService;
        private string importPath; //Added to allow for import log download
        private string zipFile; //moved to global so it can be reused int he import log download

        public SolutionManager(IOrganizationService service)
        {
            innerService = service;
        }

        //Downloads the import log file
        public string DownloadLogFile(string path, ImportSettings settings)
        {
            try
            {
                var importLogRequest = new RetrieveFormattedImportJobResultsRequest
                                           {
                                               ImportJobId = settings.ImportId
                                           };
                var importLogResponse =
                    (RetrieveFormattedImportJobResultsResponse)innerService.Execute(importLogRequest);
                DateTime time = DateTime.Now;
                string format = "yyyy_MM_dd__HH_mm";
                string filePath = path.Replace(".zip", "-") + time.ToString(format) + ".xml";
                File.WriteAllText(filePath, importLogResponse.FormattedResults);

                return filePath;
            }
            catch (Exception)
            {
                // Do nothing
                return string.Empty;
            }
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

                if (settings.MajorVersion >= 6)
                {
                    var requestAsync = new ExecuteAsyncRequest
                    {
                        Request = request
                    };

                    innerService.Execute(requestAsync);

                    bool isfinished = false;
                    do
                    {
                        try
                        {
                            var job = innerService.Retrieve("importjob", settings.ImportId, new ColumnSet(true));

                            if (job.Contains("completedon"))
                            {
                                isfinished = true;
                            }
                        }
                        catch
                        {
                        }

                        Thread.Sleep(2000);
                    } while (isfinished == false);
                }
                else
                {
                    innerService.Execute(request);
                }
            }
            catch (FaultException<OrganizationServiceFault> error)
            {
                throw new Exception("An error while importing archive: " + error.Message);
            }
            finally
            {
                if (settings.DownloadLog)
                {
                    string filePath = DownloadLogFile(importPath, settings);

                    if (MessageBox.Show("Do you want to open the log file now?\r\n\r\nThe file will also be available on disk: " + filePath, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start("Excel.exe", "\"" + filePath + "\"");
                    }
                }
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
                    string filePath = DownloadLogFile(importPath, settings);

                    if (MessageBox.Show("Do you want to open the log file now?\r\n\r\nThe file will also be available on disk: " + filePath, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start("Excel.exe", "\"" + filePath + "\"");
                    }
                }
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
    }
}