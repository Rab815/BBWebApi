using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BloombergWebAPICore.Operations;

namespace BBWebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        public static RequestProcessorFactory factory = null;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            // webapi2 configuration
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // load factories
            factory = new RequestProcessorFactory();
            factory.LoadFactories();
        }
    }
}