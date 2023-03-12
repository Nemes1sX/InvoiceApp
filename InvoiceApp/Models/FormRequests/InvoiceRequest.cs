using InvoiceApp.Models.FormRequests.Rules;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.FormRequests
{
    public class InvoiceRequest
    {
        [Required, LegalPersonExists, EULegalPerson]
        public int BilledByLegalPersonId { get; set; }
        [PayedIndividualOrLegalPerson]
        public int? PayedByLegalPersonId { get; set; }
        [PayedIndividualOrLegalPerson]
        public int? PayedByIndividualId { get; set; }
        [Required, InvoiceItemQty]
        public List<InvoiceItemRequest> InvoiceItems { get; set; }
    }
}
