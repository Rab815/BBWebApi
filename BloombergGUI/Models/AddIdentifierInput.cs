using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BloombergWebAPICore;

namespace BloombergGUI.Models
{
    public class AddIdentifierInput
    {
        //[Required]
        public string Identifier { get; set; }
        //public IdentifierType IdentifierType { get; set; }
        //[Required(ErrorMessage = "Cannot be none")]
        //[Range((int)GoldKey.LAW, (int)GoldKey.ALPHA)]
        public GoldKey GoldKey { get; set; }
    }
}