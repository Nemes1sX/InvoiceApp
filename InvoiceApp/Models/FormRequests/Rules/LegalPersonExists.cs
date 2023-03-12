using InvoiceApp.DataContext;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.FormRequests.Rules
{
    public class LegalPersonExists : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var invoiceRequest = (InvoiceRequest) validationContext.ObjectInstance;
            var _db = (InvoiceDataContext)validationContext.GetService(typeof(InvoiceDataContext));

            var legalPerson = _db.LegalPersons.Find(invoiceRequest.BilledByLegalPersonId);

            return legalPerson == null 
                ? new ValidationResult("Legal person doesn't exist")
                : ValidationResult.Success;
        }
    }
}
