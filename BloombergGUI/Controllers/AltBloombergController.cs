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
                        
            return View(securityViewModel);
        }

        [System.Web.Http.HttpPost]
        public ActionResult AddIdentifier(dynamic postdata)
        {

            SecurityViewAltModel securityViewModel = new SecurityViewAltModel { FieldsList = WebConfigurationManager.AppSettings["SecurityFields"] };
            securityViewModel.SecurityRequests = securityViewModel.SecurityRequests ?? new List<SecurityRequest>();
            securityViewModel.SecurityRequests.Add(new SecurityRequest(){ Identifier = postdata.Identifier, GoldKey = postdata.GoldKey });
            SaveViewModel(securityViewModel);
            return RedirectToAction("Index");

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

        [System.Web.Http.HttpPost]
        public ActionResult Import(HttpPostedFileBase csvIdentifierFileBase)
        {
            SecurityViewAltModel securityViewModel = new SecurityViewAltModel();
            string fieldList = "";
            HttpResponseMessage result = null;
            HttpPostedFileBase file1 = Request.Files[0]; //Uploaded file
            //Use the following properties to get file's name, size and MIMEType
            //int fileSize = file.ContentLength;
            //string fileName = file.FileName;
            //string mimeType = file.ContentType;
            //System.IO.Stream fileContent = file.InputStream;

            //var httpRequest = HttpContext.Current.Request;
            //if (csvIdentifierFileBase != null)
            //{
            ICsvParser csvParser = new CsvParser(new StreamReader(file1.InputStream));
            var csvReader = new CsvReader(csvParser);
            List<SecurityRequest> importlist = new List<SecurityRequest>();

            while (csvReader.Read())
            {
                var identifier = csvReader.GetField<string>(0);
                var goldkey = csvReader.GetField<string>(1);
                var val = EnumLookup.GoldKeyName.FirstOrDefault(x => x.Value.ToLower().Contains(goldkey.ToLower())).Key;
                importlist.Add(new SecurityRequest() { Identifier = identifier, GoldKey = (GoldKey)val });
            }


            securityViewModel.SecurityRequests = importlist;
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
