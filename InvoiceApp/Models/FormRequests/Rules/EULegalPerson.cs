using InvoiceApp.DataContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.FormRequests.Rules
{
    public class EULegalPerson : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var invoiceRequest = (InvoiceRequest)validationContext.ObjectInstance;

            var _db = (InvoiceDataContext)validationContext.GetService(typeof(InvoiceDataContext));
            var legalPerson = _db.LegalPersons.Where(x => x.Id == invoiceRequest.BilledByLegalPersonId).Include(x => x.Country).SingleOrDefault();

            if (legalPerson == null) 
            {
               return new ValidationResult("Legal person isn't from EU");
            }

            return !legalPerson.Country.EuropeanUnion && legalPerson == null
                ? new ValidationResult("Legal person isn't from EU")
                : ValidationResult.Success;
        }
    }
}
