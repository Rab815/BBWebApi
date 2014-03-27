using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading;
using Core.Extensions;
using BloombergWebAPICore;
using BloombergWebAPICore.Dto;
using BloombergWebAPICore.IWebApi;
using Newtonsoft.Json;

namespace PrototypeBloombergProcess
{
 class Program
    {
        static void Main(string[] args)
        {
            RunAsync();
        }

        static void RunAsync()
        {

            //Simulates the GetCDSList call
           var cds = new List<SecurityRequest>()
            {
                // Randoms
                new SecurityRequest() {CrdId = "CRD_US29843LAC72", Identifier = "US29843LAC72", IdentifierType = IdentifierType.ISIN}
                ,new SecurityRequest() {CrdId = "CRD_XS0939678792", Identifier = "XS0939678792", GoldKey = GoldKey.CORP}
                ,new SecurityRequest() {CrdId = "CRD_EDM6", Identifier = "EDM6", GoldKey = GoldKey.INDEX}
                ,new SecurityRequest() {CrdId = "CRD_SL3332PH", Identifier = "SL3332PH", GoldKey = GoldKey.CORP}
                ,new SecurityRequest() {CrdId = "CRD_SL3332PH", Identifier = "SL3332PH", IdentifierType = IdentifierType.CUSIP}
                ,new SecurityRequest() {CrdId = "CRD_912828TZ3", Identifier = "912828TZ3", IdentifierType = IdentifierType.CUSIP}
                ,new SecurityRequest() {CrdId = "CRD_912828TZ3", Identifier = "912828TZ3", GoldKey = GoldKey.GOVT}
                // CDS
                ,new SecurityRequest() {CrdId = "CRD_SP8A1ETA", Identifier = "SP8A1ETA", IdentifierType = IdentifierType.CUSIP}
                ,new SecurityRequest() {CrdId = "CRD_SP3A0TU1", Identifier = "SP3A0TU1", IdentifierType = IdentifierType.CUSIP}
                ,new SecurityRequest() {CrdId = "CRD_SP4A102V", Identifier = "SP4A102V", IdentifierType = IdentifierType.CUSIP}
                // IRS
                ,new SecurityRequest() {CrdId = "CRD_SL7B04GS", Identifier = "SL7B04GS", IdentifierType = IdentifierType.CUSIP}
                ,new SecurityRequest() {CrdId = "CRD_SLQF01NK", Identifier = "SLQF01NK", IdentifierType = IdentifierType.CUSIP}
                ,new SecurityRequest() {CrdId = "CRD_SL6A2LTR", Identifier = "SL6A2LTR", IdentifierType = IdentifierType.CUSIP}
                ,new SecurityRequest() {CrdId = "CRD_SL602VFM", Identifier = "SL602VFM", IdentifierType = IdentifierType.CUSIP}
                ,new SecurityRequest() {CrdId = "CRD_SL602VFS", Identifier = "SL602VFS", IdentifierType = IdentifierType.CUSIP}
                
                // known invalids
                // invalid identifier
                ,new SecurityRequest() {CrdId = "CRD_SL602", Identifier = "SL602", IdentifierType = IdentifierType.CUSIP}
                // missing identifier type and gold key
                ,new SecurityRequest() {CrdId = "CRD_US29843LAC72", Identifier = "US29843LAC72"}
                // invalid identifier
                ,new SecurityRequest() {CrdId = "CRD_XS0939678792", Identifier = "XS0939678", GoldKey = GoldKey.CORP}
                
                //,new SecurityRequest() {CrdId = "CRD_EDM6", Identifier = "EDM6", GoldKey = GoldKey.INDEX}
                //,new SecurityRequest() {CrdId = "CRD_SL3332PH", Identifier = "SL3332PH", GoldKey = GoldKey.CORP}
                //,new SecurityRequest() {CrdId = "CRD_SL3332PH", Identifier = "SL3332PH", IdentifierType = IdentifierType.CUSIP}
                //,new SecurityRequest() {CrdId = "CRD_912828TZ3", Identifier = "912828TZ3", IdentifierType = IdentifierType.CUSIP}
                //,new SecurityRequest() {CrdId = "CRD_912828TZ3", Identifier = "912828TZ3", GoldKey = GoldKey.GOVT}

            };

           List<string> fieldlist = null;
           fieldlist = new List<string>
            {
                "SW_VAL_PREMIUM","SW_PAY_NOTL_AMT","SW_REC_NOTL_AMT","FUT_PX_VAL_BP","SW_CNV_BPV"
            };


            // set up request object
            var oRefContract = new ReferenceDataRequestContract()
            {
                FieldsList = fieldlist,
                SecurityList = cds,
                ReturnEids = true,
                ReturnFormattedValue = false,
                UseUTCTime = false,
                ForceDelay = false
            };

            var oHistContract = new HistoricalDataRequestContract()
            {
                FieldsList = fieldlist,
                SecurityList = cds,
                EndDate = DateTime.Now.ToString(),
                StartDate = DateTime.Now.ToString(),
            };
            oHistContract.SetPeriodicityAdjustment(HistoricalDataRequestContract.Periodicity.Calendar);


            Console.WriteLine("Requesting Data");

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // auto serializes $type with all objects, in lieu of using a base object to interpret
            // type by including a type property
            // Let the system do the work adjust config in webapiconfig.cs to use typename handling
            // Advantage, sub complox objects are typed
            //var json = new JsonMediaTypeFormatter { SerializerSettings = { TypeNameHandling = TypeNameHandling.Objects } };
            //var response1 = client.PostAsync("bloombergapi/processbloombergrequest", oContract, json).Result;//handle response
            ///////////////////////////////////////////////////////////////////////////////////////////////////
             
            // works with converter in place, we do the work
            //var response = client.PostAsJsonAsync("bloombergapi/processbloombergrequest", oContract).Result;

            var client = new HttpClient { BaseAddress = new Uri("http://d151stn054219/bbwebapi2/api/v1/bbapi/") };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // uses my entension method to handle type naming in json request
            //var response = client.PostAsJsonAsync("processbloombergrequest", oContract, TypeNameHandling.Objects).Result;
            var response = client.PostAsJsonAsync("processbloombergrequest", oRefContract).Result;

            if (response.IsSuccessStatusCode)
            {
                //var content = response.Content.ReadAsAsync<string>().Result;
                var content = response.Content.ReadAsStringAsync().Result;
                
                Console.Write(content);
            }

            //var resultTask = client.PostAsJsonAsync<CreditDefaultSwapRequest>("bloombergapi/processbloombergrequest", oContract, TypeNameHandling.Objects)
            //    .ContinueWith<HttpResponseMessage>(t =>
            //{
            //    var response = t.Result;
            //    //var objectTask = response.Content.ReadAsAsync<CreditDefaultSwapRequest>().ContinueWith<Url>(u =>
            //    //{
            //    //    var myobject = u.Result;
            //    //    //do stuff 
            //    //    //return myobject;
            //    //});
            //    return response;
            //});

            //Console.ReadLine();

            //if (resultTask.IsCompleted)
            //{
            //    var content = resultTask.Result.Content.ReadAsAsync<string>().Result;
            //    Console.Write(content);

            //}
            Console.ReadLine();
            Environment.Exit(1);
            //if (response.IsSuccessStatusCode)
            //{
            //    //Uri gizmoUrl = response.Headers.Location;

                //// HTTP PUT
                //gizmo.Price = 80;   // Update price
                //response = await client.PutAsJsonAsync(gizmoUrl, gizmo);

                //// HTTP DELETE
                //response = await client.DeleteAsync(gizmoUrl);
            //}
             
        }
    }
}
