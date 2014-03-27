using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BloombergGUI.Models;
using BloombergWebAPICore;
using BloombergWebAPICore.Dto;
using BloombergWebAPICore.IWebApi;
using Core.MVC;
using Newtonsoft.Json;

namespace BloombergGUI.Controllers
{
    public class BloombergController : Controller
    {
        private List<SecurityRequest> reqs = null; 
        private List<string> fields = new List<string>();

        public ActionResult Index()
        {
            // need an empty object to fillin enumerated types
            SecurityViewModel emptySecurityViewModel = new SecurityViewModel();
            return View(emptySecurityViewModel);
            //return View();
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Add Identifier")]
        public ActionResult AddIdentifier(SecurityViewModel securityViewModel)
        {
            string s = "";
            //if (!ModelState.IsValid)
            //{
            //    if (!model.Validated)
            //    {
            //        var validationResults = model.Validate(new ValidationContext(model, null, null));
            //        foreach (var error in validationResults)
            //            foreach (var memberName in error.MemberNames)
            //                ModelState.AddModelError(memberName, error.ErrorMessage);
            //    }

            //    //return View("Index");
            //}

            if (ModelState.IsValid)
            {
                if (Session["reqs"] != null)
                    reqs = (List<SecurityRequest>) Session["reqs"];
                else
                    reqs = new List<SecurityRequest>();

                string id = Request["Identifier"];
                string crdid = Request["CrdId"];
                int identifierType = Convert.ToInt32(Request["IdentifierType"]);
                int goldkey = Convert.ToInt32(Request["GoldKey"]);

                // add to list box and global var
                reqs.Add(new SecurityRequest()
                {
                    Identifier = id,
                    CrdId = crdid,
                    GoldKey = (GoldKey) goldkey,
                    IdentifierType = (IdentifierType) identifierType
                });

                Session["reqs"] = reqs;
            }
            return View("Index",securityViewModel);

        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Clear Identifiers")]
        public ActionResult ClearIdentifiers()
        {
            Session["reqs"] = null;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Submit Identifiers")]
        public ActionResult SubmitIdentifiers()
        {
            string s = "";
            fields = WebConfigurationManager.AppSettings["SecurityFields"].Split(',').ToList();

            reqs = Session["reqs"] as List<SecurityRequest>;

            var oRefContract = new ReferenceDataRequestContract()
            {
                FieldsList = fields,
                SecurityList = reqs,
                ReturnEids = true,
                ReturnFormattedValue = false,
                UseUTCTime = false,
                ForceDelay = false
            };

            var apiurl = WebConfigurationManager.AppSettings["BBWebApiLocal"];

            var client = new HttpClient { BaseAddress = new Uri(apiurl) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("processbloombergrequest", oRefContract).Result;
            ResponseContract contract = null;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;


                //JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
                contract = JsonConvert.DeserializeObject<OResponseContract>(content);
                //Response.Write(content);
            }

            SecurityViewModel viewModel = new SecurityViewModel {ResponseContract = contract};
            return View("Index",viewModel);
        }
    }
}