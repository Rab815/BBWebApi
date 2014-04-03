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
        private List<string> fields = WebConfigurationManager.AppSettings["SecurityFields"].Split(',').ToList();

        public ActionResult Index()
        {
            //Response.Write("IS AUTH: "+HttpContext.User.Identity.IsAuthenticated + " Username: " + HttpContext.User.Identity.Name);
            //Response.Write("AUTH TYPE: "+HttpContext.User.Identity.AuthenticationType);
            //fields = WebConfigurationManager.AppSettings["SecurityFields"].Split(',').ToList();
            // need an empty object to fillin enumerated types
            SecurityViewModel emptySecurityViewModel = new SecurityViewModel {FieldsList = String.Join(",",fields)};
            return View(emptySecurityViewModel);
            //return View();
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Add Identifier")]
        public ActionResult AddIdentifier(SecurityViewModel securityViewModel)
        {

            if (ModelState.IsValid)
            {
                if (Session["reqs"] != null)
                    reqs = (List<SecurityRequest>) Session["reqs"];
                else
                    reqs = new List<SecurityRequest>();

                // add to list box and global var
                reqs.Add(new SecurityRequest()
                {
                    Identifier = securityViewModel.Identifier,
                    CrdId = securityViewModel.CrdId,
                    GoldKey = securityViewModel.GoldKey,
                    IdentifierType = securityViewModel.IdentifierType
                });

                Session["reqs"] = reqs;
                // clears the form values before returning
                this.ModelState.Clear();
                securityViewModel = new SecurityViewModel { FieldsList = String.Join(",", fields) };
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
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Delete Identifier")]
        public ActionResult DeleteIdentifer()
        {
            string id = Request["Identifiers"];
            reqs = Session["reqs"] as List<SecurityRequest>;

            reqs.RemoveAt(reqs.FindIndex(m => m.Identifier == id));

            Session["reqs"] = reqs;

            SecurityViewModel viewModel = new SecurityViewModel();
            viewModel.FieldsList = String.Join(",", fields);
            return View("Index", viewModel);
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Submit Identifiers")]
        public ActionResult SubmitIdentifiers()
        {
            //fields = WebConfigurationManager.AppSettings["SecurityFields"].Split(',').ToList();
            List<string> fieldList = Request["FieldsList"].Split(',').ToList();
            reqs = Session["reqs"] as List<SecurityRequest>;

            var oRefContract = new ReferenceDataRequestContract()
            {
                FieldsList = fieldList,
                SecurityList = reqs,
                ReturnEids = true,
                ReturnFormattedValue = false,
                UseUTCTime = false,
                ForceDelay = false
            };

            var apiurl = WebConfigurationManager.AppSettings["BBWebApiLocal"];
            
            // this allows access during debuging with calls from local machine
            var client = new HttpClient(new HttpClientHandler {UseDefaultCredentials = true})
            {
                BaseAddress = new Uri(apiurl)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync("processbloombergrequest", oRefContract).Result;
            ResponseContract contract = null;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                contract = JsonConvert.DeserializeObject<OResponseContract>(content);

                //Response.Write("IS AUTH: " + HttpContext.User.Identity.IsAuthenticated + " Username: " + HttpContext.User.Identity.Name + "<br/>");
                //Response.Write("AUTH TYPE: " + HttpContext.User.Identity.AuthenticationType + "<br/>");

                //Response.Write("ApiURL: "+ apiurl+"<br/>");
                //Response.Write(response.ReasonPhrase + "<br/>");
                //foreach (var header in response.Headers)
                //    Response.Write(String.Format("{0}:{1}<br/>", header.Key, header.Value.FirstOrDefault()));

            }
            //else
            //{
            //    Response.Write("IS AUTH: " + HttpContext.User.Identity.IsAuthenticated + " Username: " + HttpContext.User.Identity.Name + "<br/>");
            //    Response.Write("AUTH TYPE: " + HttpContext.User.Identity.AuthenticationType + "<br/>");

            //    Response.Write("ApiURL: " + apiurl + "<br/>");
            //    Response.Write(response.ReasonPhrase+"<br/>");
            //    foreach (var header in response.Headers)
            //        Response.Write(String.Format("{0}:{1}<br/>", header.Key, header.Value.FirstOrDefault()));
            //}

            SecurityViewModel viewModel = new SecurityViewModel {ResponseContract = contract};
            viewModel.FieldsList = String.Join(",", fieldList);
            return View("Index",viewModel);
        }
    }
}