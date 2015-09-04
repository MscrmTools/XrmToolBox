using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.IO;
using System.Text;

namespace Javista.XrmToolBox.ImportNN.AppCode
{
    public class ExportResultEventArgs : EventArgs
    {
        public string Message;
    }

    internal class ExportEngine
    {
        private readonly string filePath;
        private readonly IOrganizationService service;
        private readonly ImportFileSettings settings;

        public ExportEngine(string filePath, IOrganizationService service, ImportFileSettings settings)
        {
            this.filePath = filePath;
            this.service = service;
            this.settings = settings;
        }

        public event EventHandler<ExportResultEventArgs> RaiseError;

        public event EventHandler<ExportResultEventArgs> RaiseSuccess;

        public void Export()
        {
            var qe = new QueryExpression(settings.Relationship)
            {
                ColumnSet = new ColumnSet(true),
                PageInfo = new PagingInfo { Count = 250, PageNumber = 1 }
            };

            EntityCollection results;

            do
            {
                results = service.RetrieveMultiple(qe);
                using (var writer = new StreamWriter(filePath, true, Encoding.Default))
                {
                    foreach (var result in results.Entities)
                    {
                        try
                        {
                            string dataFirst;
                            string dataSecond;
                            var guidFirst = result.GetAttributeValue<Guid>(settings.FirstEntity + "id");
                            var guidSecond = result.GetAttributeValue<Guid>(settings.SecondEntity + "id");

                            if (!settings.FirstAttributeIsGuid)
                            {
                                var record = service.Retrieve(settings.FirstEntity, guidFirst,
                                    new ColumnSet(settings.FirstAttributeName));

                                if (!record.Contains(settings.FirstAttributeName))
                                {
                                    OnRaiseError(new ExportResultEventArgs
                                    {
                                        Message =
                                            string.Format("The record '{0}' ({1}) does not contain value for attribute '{2}' and so the NN relationship cannot be exported",
                                            record.Id.ToString("B"),
                                            settings.FirstEntity,
                                            settings.FirstAttributeName)
                                    });
                                    continue;
                                }
                                dataFirst = record[settings.FirstAttributeName].ToString();
                            }
                            else
                            {
                                dataFirst = guidFirst.ToString("B");
                            }

                            if (!settings.SecondAttributeIsGuid)
                            {
                                var record = service.Retrieve(settings.SecondEntity, guidSecond,
                                    new ColumnSet(settings.SecondAttributeName));

                                if (!record.Contains(settings.SecondAttributeName))
                                {
                                    OnRaiseError(new ExportResultEventArgs
                                    {
                                        Message =
                                            string.Format("The record '{0}' ({1}) does not contain value for attribute '{2}' and so the NN relationship cannot be exported",
                                            record.Id.ToString("B"),
                                            settings.SecondEntity,
                                            settings.SecondAttributeName)
                                    });
                                    continue;
                                }
                                dataSecond = record[settings.SecondAttributeName].ToString();
                            }
                            else
                            {
                                dataSecond = guidSecond.ToString("B");
                            }

                            writer.WriteLine("{0},{1}", dataFirst, dataSecond);

                            OnRaiseSuccess(new ExportResultEventArgs());
                        }
                        catch (Exception error)
                        {
                            OnRaiseError(new ExportResultEventArgs
                            {
                                Message = error.Message
                            });
                        }
                    }
                }

                qe.PageInfo.PageNumber++;
            } while (results.MoreRecords);
        }

        protected virtual void OnRaiseError(ExportResultEventArgs e)
        {
            EventHandler<ExportResultEventArgs> handler = RaiseError;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRaiseSuccess(ExportResultEventArgs e)
        {
            EventHandler<ExportResultEventArgs> handler = RaiseSuccess;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}