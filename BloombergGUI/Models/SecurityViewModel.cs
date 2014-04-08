using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BloombergWebAPICore;
using BloombergWebAPICore.IWebApi;
using WebGrease;

namespace BloombergGUI.Models
{

    public class SecurityViewModel //: IValidatableObject
    {
        [Required]
        public string Identifier { get; set; }
        //public IdentifierType IdentifierType { get; set; }
        [Required(ErrorMessage = "Cannot be none")]
        [Range((int)GoldKey.LAW, (int)GoldKey.ALPHA)]
        public GoldKey GoldKey { get; set; }
        //public string CrdId { get; set; }
        public string FieldsList { get; set; } 
        public ResponseContract ResponseContract { get; set; }
        public List<SecurityRequest> SecurityRequests{ get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (String.IsNullOrEmpty(Identifier))
        //        yield return new ValidationResult("Identifier is required");
        //    if (GoldKey == GoldKey.NONE)
        //        yield return new ValidationResult("Gold Key must be supplied");
        //}
    }
}