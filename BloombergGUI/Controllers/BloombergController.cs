using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.IO;
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
using CsvHelper;
using Newtonsoft.Json;

namespace BloombergGUI.Controllers
{
    public class BloombergController : Controller
    {
        private List<SecurityRequest> reqs = null;
        private List<string> fields = null;//WebConfigurationManager.AppSettings["SecurityFields"].Split(',').ToList();


        public ActionResult Index(SecurityViewModel securityViewModel)
        {
            if (GetViewModel() == null)
                SaveViewModel(securityViewModel);
            else
                securityViewModel = GetViewModel();

            if (String.IsNullOrWhiteSpace(securityViewModel.FieldsList))
                securityViewModel.FieldsList = WebConfigurationManager.AppSettings["SecurityFields"];

            SaveViewModel(securityViewModel);
            return View(securityViewModel);
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Add Identifier")]
        public ActionResult AddIdentifier(SecurityViewModel securityViewModel)
        {
            string fieldList = "";
            if (ModelState.IsValid)
            {
                if (GetViewModel() != null)
                {
                    reqs = GetViewModel().SecurityRequests ?? new List<SecurityRequest>();
                }

                if (String.IsNullOrWhiteSpace(GetViewModel().FieldsList))
                    fieldList = WebConfigurationManager.AppSettings["SecurityFields"];
                else
                    fieldList = GetViewModel().FieldsList;

                // add to list box and global var
                reqs.Add(new SecurityRequest()
                {
                    Identifier = securityViewModel.Identifier,
                    //CrdId = securityViewModel.CrdId,
                    GoldKey = securityViewModel.GoldKey,
                    //IdentifierType = securityViewModel.IdentifierType
                });

                // clears the form values before returning
                this.ModelState.Clear();
                securityViewModel = new SecurityViewModel { FieldsList = fieldList, SecurityRequests = reqs };
                // set the view model to storage
                SaveViewModel(securityViewModel);
                return View("Index", GetViewModel());
            }
            return RedirectToAction("Index", GetViewModel());

        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Clear Identifiers")]
        public ActionResult ClearIdentifiers(SecurityViewModel securityViewModel)
        {
            // clear object and reset fields list
            securityViewModel.FieldsList = WebConfigurationManager.AppSettings["SecurityFields"];
            SaveViewModel(securityViewModel);

            return View("Index", GetViewModel());
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Delete Identifier")]
        public ActionResult DeleteIdentifer(FormCollection collection, SecurityViewModel securityViewModel)
        {
            if (collection["SecurityRequests"] != null)
            {
                var ids = (collection["SecurityRequests"].Count() > 1)
                    ? collection["SecurityRequests"].Split(',')
                    : new string[] {collection["SecurityRequests"][0].ToString()};
                reqs = GetViewModel().SecurityRequests;
                if (ids.Count() > 1)
                    foreach (var id in ids)
                    {
                        var identifier = id.Split(' ')[0];
                        var key = EnumLookup.GoldKeyName.FirstOrDefault(x => x.Value.ToUpper() == id.Split(' ')[1]).Key;
                        reqs.RemoveAt(reqs.FindIndex(m => m.Identifier == identifier && m.GoldKey == (GoldKey) key));
                    }
                else
                {
                    var identifier = ids[0].Split(' ')[0];
                    var key = EnumLookup.GoldKeyName.FirstOrDefault(x => x.Value.ToUpper() == ids[0].Split(' ')[1]).Key;

                    reqs.RemoveAt(reqs.FindIndex(m => m.Identifier == identifier && m.GoldKey == (GoldKey) key));
                }

                securityViewModel = new SecurityViewModel
                {
                    FieldsList = String.Join(",", securityViewModel.FieldsList),
                    SecurityRequests = reqs
                };
                SaveViewModel(securityViewModel);
            }
            return View("Index", GetViewModel());
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Submit Identifiers")]
        public ActionResult SubmitIdentifiers(SecurityViewModel securityViewModel)
        {
            List<string> fieldList = Request["FieldsList"].Split(',').ToList();

            reqs = GetViewModel().SecurityRequests;

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

            //SecurityViewModel viewModel = new SecurityViewModel {ResponseContract = contract};
            securityViewModel.ResponseContract = contract;
            securityViewModel.SecurityRequests = reqs;
            securityViewModel.FieldsList = String.Join(",", fieldList);
            SaveViewModel(securityViewModel);
            return View("Index", GetViewModel());
        }

        [HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Import")]
        public ActionResult Import(HttpPostedFileBase csvIdentifierFileBase, SecurityViewModel securityViewModel)
        {
            string fieldList = "";
            if (csvIdentifierFileBase != null)
            {
                ICsvParser csvParser = new CsvParser(new StreamReader(csvIdentifierFileBase.InputStream));
                var csvReader = new CsvReader(csvParser);
                List<SecurityRequest> importlist = new List<SecurityRequest>();
                if (GetViewModel() != null)
                {
                    reqs = GetViewModel().SecurityRequests ?? new List<SecurityRequest>();
                }
                if (String.IsNullOrWhiteSpace(GetViewModel().FieldsList))
                    fieldList = WebConfigurationManager.AppSettings["SecurityFields"];
                else
                    fieldList = GetViewModel().FieldsList;

                while (csvReader.Read())
                {
                    var identifier = csvReader.GetField<string>(0);
                    var goldkey = csvReader.GetField<string>(1);
                    var val =
                        EnumLookup.GoldKeyName.FirstOrDefault(x => x.Value.ToLower().Contains(goldkey.ToLower())).Key;
                    importlist.Add(new SecurityRequest() {Identifier = identifier, GoldKey = (GoldKey) val});
                }

                foreach (var item in importlist)
                    reqs.Add(item);

                securityViewModel.SecurityRequests = reqs;
                securityViewModel.FieldsList = String.Join(",", fieldList);
                //store the model
                SaveViewModel(securityViewModel);
            }
            return View("Index", GetViewModel());
        }

        // mimic persistant storage
        private SecurityViewModel GetViewModel()
        {
            return Session["model"] as SecurityViewModel;
        }

        private void SaveViewModel(SecurityViewModel model)
        {
            Session["Model"] = model;
        }
    }
}