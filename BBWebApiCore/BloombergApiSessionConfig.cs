using System;
using System.Collections.Specialized;
using System.Configuration;

namespace BloombergWebAPICore
{
    public class BloombergApiSessionConfig
    {
        public string BbHost { get; set; }
        public string BbService { get; set; }
        public int BbPort { get; set; }

        public BloombergApiSessionConfig()
        {
            RetrieveAppSettings();
        }

        public BloombergApiSessionConfig RetrieveAppSettings()
        {
            //var config = new BloombergApiSessionConfig();
            try
            {                
                // Get the AppSettings section.
                NameValueCollection appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    //_Logging.Info("0 AppSettings were retrieved from the app.config file.");
                    throw new Exception("0 AppSettings were retrieved from the app.config file.");
                }

                BbService = appSettings["BloombergService"];
                BbHost = appSettings["BloombergHost"];
                BbPort = int.Parse(appSettings["BloombergPort"]);
            }
            catch (ConfigurationErrorsException e)
            {
               // _Logging.Error("An error occurred when trying to retrieve AppSettings from the app.config file. " + e.BareMessage);

            }
            return this;
        }
    }
}
