using InvoiceApp.DataContext;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.FormRequests.Rules
{
    public class PayedIndividualOrLegalPerson : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var invoiceRequest = (InvoiceRequest)validationContext.ObjectInstance;
            var _db = (InvoiceDataContext)validationContext.GetService(typeof(InvoiceDataContext));

            var payedLegalPerson = _db.LegalPersons.Find(invoiceRequest.PayedByLegalPersonId);
            var payedIndividual = _db.Individuals.Find(invoiceRequest.PayedByIndividualId);

            if (payedIndividual == null && payedLegalPerson == null)
            {
                return new ValidationResult("Payed individual or legal person isn't existed");
            }

            if (payedIndividual != null && payedLegalPerson != null)
            {
                return new ValidationResult("There can only be payed individual or legal person");
            }

            return ValidationResult.Success;
        }
    }
}
