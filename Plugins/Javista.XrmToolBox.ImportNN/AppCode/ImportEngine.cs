using Microsoft.Crm.Sdk.Messages;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Javista.XrmToolBox.ImportNN.AppCode
{
    public class ResultEventArgs : EventArgs
    {
        public int LineNumber;
        public string Message;
    }

    internal class ImportEngine
    {
        private readonly string filePath;
        private readonly IOrganizationService service;
        private readonly ImportFileSettings settings;

        public ImportEngine(string filePath, IOrganizationService service, ImportFileSettings settings)
        {
            this.filePath = filePath;
            this.service = service;
            this.settings = settings;
        }

        public event EventHandler<ResultEventArgs> RaiseError;

        public event EventHandler<ResultEventArgs> RaiseSuccess;

        public void Import()
        {
            using (var reader = new StreamReader(filePath, Encoding.Default))
            {
                string line;
                int lineNumber = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    try
                    {
                        string[] data = new string[2];

                        using (TextFieldParser parser = new TextFieldParser(new StringReader(line))
                        {
                            HasFieldsEnclosedInQuotes = true
                        })
                        {
                            parser.SetDelimiters(",");

                            while (!parser.EndOfData)
                            {
                                data = parser.ReadFields();
                            }
                        }

                        Guid firstGuid = Guid.Empty;
                        Guid secondGuid = Guid.Empty;

                        if (settings.FirstAttributeIsGuid)
                        {
                            firstGuid = new Guid(data[0]);
                        }
                        else
                        {
                            var records = service.RetrieveMultiple(new QueryExpression(settings.FirstEntity)
                            {
                                Criteria =
                                {
                                    Conditions =
                                    {
                                        new ConditionExpression(settings.FirstAttributeName, ConditionOperator.Equal,
                                            data[0])
                                    }
                                }
                            });

                            if (records.Entities.Count == 1)
                            {
                                firstGuid = records.Entities.First().Id;
                            }
                            else if (records.Entities.Count > 1)
                            {
                                RaiseError(this,
                                    new ResultEventArgs
                                    {
                                        LineNumber = lineNumber,
                                        Message = string.Format("More than one record ({0}) were found with the value specified", settings.FirstEntity)
                                    });

                                continue;
                            }
                            else
                            {
                                RaiseError(this,
                                    new ResultEventArgs
                                    {
                                        LineNumber = lineNumber,
                                        Message = string.Format("No record ({0}) was found with the value specified", settings.FirstEntity)
                                    });

                                continue;
                            }
                        }

                        if (settings.SecondAttributeIsGuid)
                        {
                            secondGuid = new Guid(data[1]);
                        }
                        else
                        {
                            var records = service.RetrieveMultiple(new QueryExpression(settings.SecondEntity)
                            {
                                Criteria =
                                {
                                    Conditions =
                                    {
                                        new ConditionExpression(settings.SecondAttributeName, ConditionOperator.Equal,
                                            data[1])
                                    }
                                }
                            });

                            if (records.Entities.Count == 1)
                            {
                                secondGuid = records.Entities.First().Id;
                            }
                            else if (records.Entities.Count > 1)
                            {
                                RaiseError(this,
                                    new ResultEventArgs
                                    {
                                        LineNumber = lineNumber,
                                        Message = string.Format("More than one record ({0}) were found with the value specified", settings.SecondEntity)
                                    });

                                continue;
                            }
                            else
                            {
                                RaiseError(this,
                                    new ResultEventArgs
                                    {
                                        LineNumber = lineNumber,
                                        Message = string.Format("No record ({0}) was found with the value specified", settings.SecondEntity)
                                    });

                                continue;
                            }
                        }

                        if (settings.Relationship == "listcontact_association"
                            || settings.Relationship == "listaccount_association"
                            || settings.Relationship == "listlead_association")
                        {
                            var request = new AddListMembersListRequest
                            {
                                ListId = settings.FirstEntity == "list" ? firstGuid : secondGuid,
                                MemberIds = new[] { settings.FirstEntity == "list" ? secondGuid : firstGuid }
                            };

                            service.Execute(request);
                        }
                        else
                        {
                            var request = new AssociateRequest
                            {
                                Target = new EntityReference(settings.FirstEntity, firstGuid),
                                Relationship = new Relationship(settings.Relationship),
                                RelatedEntities = new EntityReferenceCollection
                                {
                                    new EntityReference(settings.SecondEntity, secondGuid)
                                }
                            };

                            if (request.Target.LogicalName == request.RelatedEntities.First().LogicalName)
                            {
                                request.Relationship.PrimaryEntityRole = EntityRole.Referenced;
                            }

                            service.Execute(request);
                        }

                        OnRaiseSuccess(new ResultEventArgs { LineNumber = lineNumber });
                    }
                    catch (FaultException<OrganizationServiceFault> error)
                    {
                        if (error.Detail.ErrorCode.ToString("X") == "80040237")
                        {
                            OnRaiseError(new ResultEventArgs { LineNumber = lineNumber, Message = "Relationship was not created because it already exists" });
                        }
                        else
                        {
                            OnRaiseError(new ResultEventArgs { LineNumber = lineNumber, Message = error.Message });
                        }
                    }
                }
            }
        }

        protected virtual void OnRaiseError(ResultEventArgs e)
        {
            EventHandler<ResultEventArgs> handler = RaiseError;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnRaiseSuccess(ResultEventArgs e)
        {
            EventHandler<ResultEventArgs> handler = RaiseSuccess;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}