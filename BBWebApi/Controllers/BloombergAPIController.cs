using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Providers.Entities;
using BBWebApi;
using Bloomberglp.Blpapi;
using BloombergWebAPICore;
using BloombergWebAPICore.IWebApi;
using BloombergWebAPICore.Operations;
using Session = Bloomberglp.Blpapi.Session;

namespace BBWebApi2.Controllers
{
    [RoutePrefix("api/v1/bbapi")] // new versions follow this route approach
    public class BloombergApiController : ApiController
    {
        private BloombergApiSessionConfig config = null;

        [AcceptVerbs("POST")]
        [Route("ProcessBloombergRequest")]
        public HttpResponseMessage ProcessBloombergRequest(IRequestContract marshalContract)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, marshalContract);
            // TODO: Test for null marshal contract here

            //Create a bloomberg session to handle request also populates local config
            Session session = CreateBloombergSession();

            /////////////////////////////////////////////////////////////////////////////
            // Factory impl
            // get the processor for the Request supports expansion of new processors
            //IRequestProcessor rpRequestProcessor = MvcApplication.factory.FactoryToRequestProcessor(marshalContract);
            IRequestProcessor rpRequestProcessor = WebApiApplication.factory.FactoryToRequestProcessor(session, marshalContract);

            /////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////

            // Concrete impl
            //IRequestProcessor rpRequestProcessor = new ORequestProcessor();

            ////////////////////////////////////////////////////////////////////////////


            // polymorphic, let the object determine how to process itself based on concrete implementation of type
            rpRequestProcessor.ProcessRequest(session, marshalContract);

            // get the reponse from the object
            var json = rpRequestProcessor.GetResponse();

            // if we get to this point return a status of success
            var responsemsg = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };

            return responsemsg;
        }

        /// <summary>
        /// Sets config and other session data to create the session
        /// </summary>
        /// <returns></returns>
        private Session CreateBloombergSession()
        {
            // retrieves the appsettings and fills the config object
            config = new BloombergApiSessionConfig();

            var sessionOptions = new SessionOptions { ServerHost = config.BbHost, ServerPort = config.BbPort };
            var session = new Session(sessionOptions);

            if (!session.Start())
            {
                //_Logging.Error("Unable to initiate Bloomberg session.");
                return null;
            }

            if (!session.OpenService(config.BbService))
            {
                //_Logging.Error("Unable to open Bloomberg service.");
                return null;
            }
            return session;

        }



    }
}