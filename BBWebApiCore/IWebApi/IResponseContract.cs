using System.Collections.Generic;
using BloombergWebAPICore.Dto;

namespace BloombergWebAPICore.IWebApi
{
    public interface IResponseContract
    {
        IRequestContract OrigRequest { get; set; }

        ErrorInfo RequestError { get; set; }

        ErrorInfo ResponseError { get; set; }
        List<Security> SecurityData { get; set; }
    }

    public abstract class ResponseContract : IResponseContract
    {
        public ResponseContract()
        {

        }
        public virtual IRequestContract OrigRequest { get; set; }
        public virtual ErrorInfo RequestError { get; set; }

        public virtual ErrorInfo ResponseError { get; set; }

        public virtual List<Security> SecurityData { get; set; }


    }
}
