using InvoiceApp.Models.FormRequests.Rules;
using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.FormRequests
{
    /// <summary>
    /// Invoice request
    /// </summary>
    public class InvoiceRequest
    {
        /// <summary>
        /// Billed legal person id
        /// </summary>
        [Required, LegalPersonExists, EULegalPerson]
        public int BilledByLegalPersonId { get; set; }
        /// <summary>
        /// Payed by legal person id
        /// </summary>
        [PayedIndividualOrLegalPerson]
        public int? PayedByLegalPersonId { get; set; }
        /// <summary>
        /// Payed by legal person id
        /// </summary>
        [PayedIndividualOrLegalPerson]
        public int? PayedByIndividualId { get; set; }
        /// <summary>
        /// Invoice item request list
        /// </summary>
        [Required, InvoiceItemQty]
        public List<InvoiceItemRequest> InvoiceItems { get; set; }
    }
}
