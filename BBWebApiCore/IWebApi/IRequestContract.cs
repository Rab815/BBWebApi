using System.Collections.Generic;
using Newtonsoft.Json;

namespace BloombergWebAPICore.IWebApi
{
    [JsonConverter(typeof(RequestConverter))]
    public interface IRequestContract
    {
        string Type { get; }
        BloombergRequestServiceTypes RequestType { get; }
        IEnumerable<SecurityRequest> SecurityList { get; set; }
        IEnumerable<string> FieldsList { get; set; }
    }

    public abstract class RequestContract : IRequestContract
    {
        [JsonProperty(PropertyName = "$type")]
        public string Type
        {
            get
            {
                return this.GetType().FullName;
            }
        }
        public abstract BloombergRequestServiceTypes RequestType { get; }

        public virtual IEnumerable<SecurityRequest> SecurityList { get; set; }

        public virtual IEnumerable<string> FieldsList { get; set; }
    }

    public class SecurityRequest
    {
        public SecurityRequest()
        {
            // default these to none to force them to set at least one in the request
            IdentifierType = IdentifierType.NONE;
            GoldKey = GoldKey.NONE;
        }

        public string Identifier;
        public IdentifierType IdentifierType;
        public GoldKey GoldKey;
        public string GoldkeyText;
        public string CrdId;
    }

}
