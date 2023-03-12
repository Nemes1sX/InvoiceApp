using AutoMapper.Configuration;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.FormRequests.Rules
{
    public class InvoiceItemQty : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var invoiceRequest = (InvoiceRequest)validationContext.ObjectInstance;

            return invoiceRequest.InvoiceItems.Count < 1
                ? new ValidationResult("Invoice must have its items")
                : ValidationResult.Success;
        }

 
    }
}
