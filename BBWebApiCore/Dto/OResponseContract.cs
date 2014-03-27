using System.Collections.Generic;
using System.Linq;
using BloombergWebAPICore.IWebApi;

namespace BloombergWebAPICore.Dto
{
    public sealed class OResponseContract : ResponseContract
    {
        private List<Security> securityData = new List<Security>();

        public OResponseContract()
        {
            ResponseError = null;
            SecurityData = new List<Security>();
        }

        // ensure order of sequence returned is by sequence number
        public override List<Security> SecurityData
        {
            get
            {
                if (securityData.Count > 0)
                   securityData =  securityData.OrderBy(prop => prop.SequenceNumber).ToList();
                return securityData; 
            }
            set { securityData = value; }
        }
    }

    // Classes used by response contract to serialize the data back from bloomberg 
    // in a consistant fashion
    public class ErrorInfo
    {
        public string Source;
        public int Code;
        public string Category;
        public string Message;
        public string Subcategory;
    }

    public class Field
    {
        public string FieldId;
        public string FieldValue;
    }

    public class FieldException
    {
        public string FieldId;
        public ErrorInfo ErrorInfo;
    }

    public class Security
    {
        public Security()
        {
            FieldList = null;
            FieldExceptionList = null;
            SecurityError = null;
        }
        public string Identifier;
        public int SequenceNumber;
        public string CrdId;
        public List<Field> FieldList;
        public List<FieldException> FieldExceptionList;
        //public List<EidData> EIDData;
        public ErrorInfo SecurityError;

    }
}
