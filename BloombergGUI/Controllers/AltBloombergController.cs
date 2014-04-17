using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BloombergGUI.Models;
using BloombergWebAPICore;
using BloombergWebAPICore.IWebApi;
using Core.MVC;
using CsvHelper;
using System.Web.Http;
using Microsoft.Ajax.Utilities;

namespace BloombergGUI.Controllers
{
    public class AltBloombergController : Controller
    {

        public ActionResult Index()
        {
            SecurityViewAltModel securityViewModel = GetViewModel();
            if (securityViewModel == null || securityViewModel.SecurityRequests == null)
                securityViewModel = new SecurityViewAltModel
                {
                    SecurityRequests = new List<SecurityRequest>(),
                    FieldsList = WebConfigurationManager.AppSettings["SecurityFields"]
                };
                        
            return View("Index",securityViewModel);
        }

        [System.Web.Http.HttpPost]
        public ActionResult AddIdentifier(AddIdentifierInput postdata)
        {

            //SecurityViewAltModel securityViewModel = new SecurityViewAltModel { FieldsList = WebConfigurationManager.AppSettings["SecurityFields"] };
            SecurityViewAltModel securityViewModel = GetViewModel();
            securityViewModel = securityViewModel ?? new SecurityViewAltModel { FieldsList = WebConfigurationManager.AppSettings["SecurityFields"] };
            securityViewModel.SecurityRequests = securityViewModel.SecurityRequests ?? new List<SecurityRequest>();
            SecurityRequest newone = new SecurityRequest()
            {
                Identifier = postdata.Identifier,
                GoldKey = postdata.GoldKey,
                GoldkeyText = EnumLookup.GoldKeyName[postdata.GoldKey]
            };
            securityViewModel.SecurityRequests.Add(newone);
            SaveViewModel(securityViewModel);
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AltBloomberg");
            return Json(new { Url = redirectUrl, Requests = newone });

        }

        [System.Web.Mvc.HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Clear Identifiers")]
        public ActionResult ClearIdentifiers(SecurityViewAltModel securityViewModel)
        {
            // clear object and reset fields list
            securityViewModel.FieldsList = WebConfigurationManager.AppSettings["SecurityFields"];
            securityViewModel.SecurityRequests = new List<SecurityRequest>();
            SaveViewModel(securityViewModel);

            return View("Index", GetViewModel());
        }

        [System.Web.Mvc.HttpPost]
        [MultiButton(MatchFormKey = "action", MatchFormValue = "Delete Identifier")]
        public ActionResult DeleteIdentifer(FormCollection collection)
        {
            SecurityViewAltModel securityViewModel = GetViewModel();
            List<SecurityRequest> reqs = null;
            if (collection["SecurityRequests"] != null)
            {
                var ids = (collection["SecurityRequests"].Count() > 1)
                    ? collection["SecurityRequests"].Split(',')
                    : new string[] { collection["SecurityRequests"][0].ToString() };
                reqs = securityViewModel.SecurityRequests;
                //if (ids.Count() > 1)
                    foreach (var id in ids)
                    {
                        var identifier = id.Split(' ')[0];
                        var key = EnumLookup.GoldKeyName.FirstOrDefault(x => String.Equals(x.Value, id.Split(' ')[1], StringComparison.CurrentCultureIgnoreCase)).Key;
                        reqs.RemoveAt(reqs.FindIndex(m => m.Identifier == identifier && m.GoldKey == (GoldKey)key));
                    }
                //else
                //{
                //    var identifier = ids[0].Split(' ')[0];
                //    var key = EnumLookup.GoldKeyName.FirstOrDefault(x => String.Equals(x.Value, ids[0].Split(' ')[1], StringComparison.CurrentCultureIgnoreCase)).Key;

                //    reqs.RemoveAt(reqs.FindIndex(m => m.Identifier == identifier && m.GoldKey == (GoldKey)key));
                //}

                securityViewModel = new SecurityViewAltModel
                {
                    FieldsList = String.Join(",", securityViewModel.FieldsList),
                    SecurityRequests = reqs
                };
                SaveViewModel(securityViewModel);
            }
            return View("Index", GetViewModel());
        }

        [System.Web.Http.HttpPost]
        public ActionResult Import(HttpPostedFileBase csvIdentifierFileBase)
        {
            SecurityViewAltModel securityViewModel = GetViewModel() ?? new SecurityViewAltModel();
            string fieldList = "";
            HttpResponseMessage result = null;
            //HttpPostedFileBase file1 = Request.Files[0]; //Uploaded file

            ICsvParser csvParser = new CsvParser(new StreamReader(csvIdentifierFileBase.InputStream));
            var csvReader = new CsvReader(csvParser);
            List<SecurityRequest> importlist = new List<SecurityRequest>();

            while (csvReader.Read())
            {
                var identifier = csvReader.GetField<string>(0);
                var goldkey = csvReader.GetField<string>(1);
                var val = EnumLookup.GoldKeyName.FirstOrDefault(x => x.Value.ToLower().Contains(goldkey.ToLower())).Key;
                importlist.Add(new SecurityRequest() { Identifier = identifier, GoldKey = (GoldKey)val, GoldkeyText = goldkey });
            }

            if(securityViewModel.SecurityRequests == null)
                securityViewModel.SecurityRequests = new List<SecurityRequest>();

            securityViewModel.SecurityRequests.AddRange(importlist);
            securityViewModel.FieldsList = String.Join(",", fieldList);
            SaveViewModel(securityViewModel);
            return RedirectToAction("Index");
        }

        private SecurityViewAltModel GetViewModel()
        {
            return Session["model"] as SecurityViewAltModel;
        }

        private void SaveViewModel(SecurityViewAltModel model)
        {
            Session["Model"] = model;
        }
    }
}
