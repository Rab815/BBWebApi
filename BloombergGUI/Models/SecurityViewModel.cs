using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BloombergWebAPICore;
using BloombergWebAPICore.IWebApi;

namespace BloombergGUI.Models
{
    
    public class SecurityViewModel: IValidatableObject
    {
        public string Identifier { get; set; }
        public IdentifierType IdentifierType { get; set; }
        public GoldKey GoldKey { get; set; }
        //public bool Validated;
        public string CrdId { get; set; }
        public string FieldsList { get; set; } 
        public ResponseContract ResponseContract { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(Identifier))
                yield return new ValidationResult("Identifier is required");
            if (IdentifierType == IdentifierType.NONE && GoldKey == GoldKey.NONE)
                yield return new ValidationResult("Either Identifier or Gold Key must be supplied");
            if (IdentifierType != IdentifierType.NONE && GoldKey != GoldKey.NONE)
                yield return new ValidationResult("Either Identifier or Gold Key must be supplied, but not both.  Please choose one.");
        }
    }
}