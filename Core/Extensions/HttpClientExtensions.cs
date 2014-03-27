using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;


namespace Core.Extensions
{
    /// <summary>
    /// Additional extension methods for posting json objects to webapi to include type name handling
    /// Also:  This must be set in the WebApiConfig.cs file for deserialization of polymorphic objects by web api methods
    ///     config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
    /// 
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// For use with polymorphic webapi calls, ie... calls which accept abstract and interface based method params
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="typeNameHandling">Defaults to Object based type handling</param>
        /// <returns></returns>
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value, TypeNameHandling typeNameHandling)
        {

            return client.PostAsJsonAsync<T>(requestUri, value, CancellationToken.None, typeNameHandling);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken, TypeNameHandling typeNameHandling)
        {
            var formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = typeNameHandling
                }
            };

            //{
            //    SerializerSettings =  { TypeNameHandling = typeNameHandling }
            //};
            return client.PostAsync<T>(requestUri, value, formatter, cancellationToken);
        }

    }
}
