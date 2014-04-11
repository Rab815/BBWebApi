using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BloombergWebAPICore.IWebApi;

namespace BloombergGUI.Models
{
    public class SecurityViewAltModel
    {
        public string FieldsList { get; set; }
        public ResponseContract ResponseContract { get; set; }
        public List<SecurityRequest> SecurityRequests { get; set; }
    }
}