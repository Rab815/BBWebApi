using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using Bloomberglp.Blpapi;
using BloombergWebAPICore.Dto;
using BloombergWebAPICore.IWebApi;
using Newtonsoft.Json;

namespace BloombergWebAPICore.Operations
{
    public interface IRequestProcessor
    {
        void HandleResponseEvent(Event objEvent);

        void ProcessRequest(Session s, IRequestContract requestContract);

        void ProcessEvents(Session s, Request req);

        string GetResponse();
    }
    public abstract class RequestProcessor : IRequestProcessor
    {
        protected string connString = "";
        protected IResponseContract response = new OResponseContract();
        protected Request request = null;

        protected RequestProcessor()
        {
                
        }

        protected RequestProcessor(IRequestContract requestContract)
        {
            string s = "";
        }

        protected RequestProcessor(Session session,IRequestContract requestContract)
        {
            // get the configuration to process bloomberg request
            BloombergApiSessionConfig config = new BloombergApiSessionConfig();

            // TODO MOVE THIS CODE TO THE CONSTRUCTOR FOR PREPOSTING ITEMS OT THE REQUEST FIELDS
            // create the service
            Service refDataSvc = session.GetService(config.BbService);
            // get Request type
            var requestName = Enum.GetName(typeof(BloombergRequestServiceTypes), requestContract.RequestType);
            // create the request
            request = refDataSvc.CreateRequest(requestName);
        }

        public virtual void ProcessRequest(Session s, IRequestContract requestContract)
        {
            // clone the original request so it can be reused by client and return with response
            response.OrigRequest = requestContract;

            //// get the configuration to process bloomberg request
            //BloombergApiSessionConfig config = new BloombergApiSessionConfig();

            //// TODO MOVE THIS CODE TO THE CONSTRUCTOR FOR PREPOSTING ITEMS OT THE REQUEST FIELDS
            //// create the service
            //Service refDataSvc = s.GetService(config.BbService);
            //// get Request type
            //var requestName = Enum.GetName(typeof(BloombergRequestServiceTypes), requestContract.RequestType);
            //// create the request
            //request = refDataSvc.CreateRequest(requestName);
            
            // Loop through the list of identifiers and append them to the request
            foreach (var sec in requestContract.SecurityList)
            {
                string secfield = "";
                // identifier security request
                if (sec.IdentifierType != IdentifierType.NONE)
                    secfield = String.Format("/{0}/{1}",EnumLookup.IdentifierTypeName[sec.IdentifierType], sec.Identifier) ;
                
                //goldkey security request
                if (sec.GoldKey != GoldKey.NONE)
                    secfield = String.Format("{0} {1}",sec.Identifier , EnumLookup.GoldKeyName[sec.GoldKey]);

                // Unique request error: if neither is filled for the security put in an error response
                // DO NOT SEND TO BLOOMBERG IF THIS HAPPENS
                if (sec.IdentifierType == IdentifierType.NONE && sec.GoldKey == GoldKey.NONE)
                {
                    response.RequestError = new ErrorInfo()
                    {
                        Message = sec.Identifier + " not given a goldkey or indentifier type",
                        Category = "none",
                        Code = 0,
                        Source = sec.Identifier,
                        Subcategory = ""
                    };
                }
                else
                    request.GetElement("securities").AppendValue(secfield);
            }

            // allow bloomberg to process bad request if user forgets fields list
            if (requestContract.FieldsList == null)
                request.GetElement("fields").AppendValue("");
            else
                // Indicate which fields to retrieve for each identifier
                foreach(var field in requestContract.FieldsList)
                    request.GetElement("fields").AppendValue(field);

            ProcessEvents(s,request);
        }

        public void ProcessEvents(Session s, Request req)
        {
            s.SendRequest(req, new CorrelationID(1));

            bool continueToLoop = true;
            while (continueToLoop)
            {
                Event eventObj = s.NextEvent();
                switch (eventObj.Type)
                {
                    case Event.EventType.RESPONSE: // final response
                        continueToLoop = false;
                        HandleResponseEvent(eventObj);
                        break;
                    case Event.EventType.PARTIAL_RESPONSE:
                        HandleResponseEvent(eventObj);
                        break;
                    default:
                        //if (Debug)
                        HandleOtherEvent(eventObj);
                        break;
                }
            }
        }

        public virtual void HandleResponseEvent(Event objEvent)
        {
            try
            {
                // check for response vs partial response
                Event.EventType t = objEvent.Type;
                // if a partial response the message goes at the end of the queue

                foreach (Message message in objEvent.GetMessages())
                {
                    Element referenceDataResponse = message.AsElement;
                    if (referenceDataResponse.HasElement("responseError"))
                    {
                        Element error = referenceDataResponse.GetElement("responseError");
                        response.ResponseError = new ErrorInfo()
                        {
                            Message = error.GetElementValueByDataType("source"),
                            Category = error.GetElementValueByDataType("category"),
                            Code = error.GetElementValueByDataType("code"),
                            Source = error.GetElementValueByDataType("source"),
                            Subcategory = error.GetElementValueByDataType("subcategory")
                        };
                    }

                    Element securityDataArray = referenceDataResponse.GetElement("securityData");
                    int numItems = securityDataArray.NumValues;

                    for (int i = 0; i < numItems; ++i)
                    {
                        string secsearch = "";
                        Element securityData = securityDataArray.GetValueAsElement(i);
                        String security = securityData.GetElementAsString("security");
                        int sequenceNumber = securityData.GetElementAsInt32("sequenceNumber");

                        // Defined regex to find identifiers in indentifier field coming back form bb
                        RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.RightToLeft;
                        // ticker gold match "^\w+(\s)(\w+)|\w+\s(.)-(\w+)$" matches  "xxxxxxx govt", "xxxx M-Mkt"
                        Regex goldtickerRegex = new Regex(@"^(?<id>\w+)\s\w+$", options);
                        Regex identifierticker = new Regex(@"^/\w+/(?<id>\w+)$", options); // identifier ticker 
                        MatchCollection col = null;

                        // try to find the identifier from a goldkey type then try to find the identifier from an identifier type
                        col = goldtickerRegex.Matches(security);
                        if(col.Count == 0)
                            col = identifierticker.Matches(security);
                        // pull out just the identifier value
                        if (col.Count > 0)
                            secsearch = col[0].Groups["id"].Value;

                        // search the security list that was submitted in the request for the matching identifier and get the crid
                        var firstOrDefault = response.OrigRequest.SecurityList.FirstOrDefault(sec => sec.Identifier == secsearch);
                        string crdid = "";
                        // set the crid for the response value
                        if (firstOrDefault != null)
                               crdid = firstOrDefault.CrdId;
                        // create a security object with the information coming back
                        Security newSecurity = new Security
                        {
                            Identifier = security,
                            CrdId = crdid,
                            SequenceNumber = sequenceNumber
                        };

                        // start appending objects to the security as appropriate
                        if (securityData.HasElement("securityError"))
                        {
                            // log error
                            var securityError = securityData.GetElement("securityError");

                            newSecurity.SecurityError = new ErrorInfo()
                            {
                                Message = securityError.GetElementValueByDataType("message"),
                                Category = securityError.GetElementValueByDataType("category"),
                                Code = securityError.GetElementValueByDataType("code"),
                                Source = securityError.GetElementValueByDataType("source"),
                                Subcategory = securityError.GetElementValueByDataType("subcategory")
                            };
                        }
                        if (securityData.HasElement("fieldExceptions"))
                        {
                            var fieldExceptionArray = securityData.GetElement("fieldExceptions");
                            var count =  fieldExceptionArray.NumValues;
                            for (int j = 0; j < count; ++j)
                            {
                                var fieldError = fieldExceptionArray.GetValueAsElement(j);
                                var errorinfo = fieldError.GetElement("errorInfo");

                                // if this particular securitydata response element doesn't have it's list initialized
                                if (newSecurity.FieldExceptionList == null)
                                    newSecurity.FieldExceptionList =
                                        new List<FieldException>();

                                newSecurity.FieldExceptionList.Add(
                                    new FieldException
                                    {
                                        FieldId = fieldError.GetElementValueByDataType("fieldId"),
                                        ErrorInfo = new ErrorInfo()
                                        {
                                            Message = errorinfo.GetElementValueByDataType("message"),
                                            Category = errorinfo.GetElementValueByDataType("category"),
                                            Code = errorinfo.GetElementValueByDataType("code"),
                                            Source = errorinfo.GetElementValueByDataType("source"),
                                            Subcategory = errorinfo.GetElementValueByDataType("subcategory")
                                        }
                                    });

                            }
                        }
                        if (securityData.HasElement("fieldData"))
                        {
                            Element fieldData = securityData.GetElement("fieldData");
                            var count = fieldData.NumElements;
                            for (int j = 0; j < count; ++j)
                            {
                                var data = fieldData.GetElement(j);

                                if (newSecurity.FieldList == null)
                                    newSecurity.FieldList = new List<Field>();

                                newSecurity.FieldList.Add(
                                    new Field()
                                    {
                                        FieldId = data.Name.ToString(),
                                        FieldValue = data.GetValueAsString()
                                    });
                            }
                        }

                        // finally after processing at it to the response
                        response.SecurityData.Add(newSecurity);
                    }
                }
            }
            catch (Exception ex)
            {
                var str = ex.Message;

            }
        }

        public virtual void HandleOtherEvent(Event objEvent)
        {
            //_Logging.Debug("EventType=" + eventObj.Type);
            foreach (Message message in objEvent.GetMessages())
            {
                //_Logging.Debug("correlationID=" + message.CorrelationID);
                //_Logging.Debug("messageType=" + message.MessageType);
                //message.Print(Console.Out);
                if (Event.EventType.SESSION_STATUS == objEvent.Type && message.MessageType.Equals("SessionTerminated"))
                {
                    //_Logging.Error("Terminating: " + message.MessageType);
                }
            }
        }

        public virtual void RetrieveConnectionString()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["external_pricing"];

            if (settings != null)
                connString = settings.ConnectionString;
            else
            {
                //_Logging.Error("Failed to retrieve connection string from the app.config file.");
            }
        }

        public string GetResponse()
        {
            try
            {
                return JsonConvert.SerializeObject(response);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }
    }



    public class ReferenceDataRequestProcessor : RequestProcessor
    {
        public ReferenceDataRequestProcessor()
        {
            
        }
        //// empty class for any possible preprocessing
        public ReferenceDataRequestProcessor(IRequestContract requestContract)
            : base(requestContract)
        {
            string s = "";
        }

        public ReferenceDataRequestProcessor(Session session, IRequestContract requestContract)
            : base(session, requestContract)
        {
            string s = "";
        }

        public override void ProcessRequest(Session s, IRequestContract requestContract)
        {
            if (((ReferenceDataRequestContract)requestContract).ReturnEids)
                request.GetElement("fields").AppendValue("");
            base.ProcessRequest(s, requestContract);
        }
    }

    public class HistoricalDataRequestProcessor : RequestProcessor
    {
        public HistoricalDataRequestProcessor()
        {
            
        }
        //// empty class for any possible preprocessing
        public HistoricalDataRequestProcessor(IRequestContract requestContract)
            : base(requestContract)
        {
            string s = "";
        }

        public HistoricalDataRequestProcessor(Session session, IRequestContract requestContract)
            : base(session, requestContract)
        {
            string s = "";
        }
    }
}
