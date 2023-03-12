using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models.Entities
{
    public class Invoice : Entity
    {
        public int TotalPrice { get; set; }
        public int BilledLegalPersonId { get; set; }
        public int? PayerLegalLegalPersonId { get; set; }
        public int? PayedIndividualId { get; set; }
        [ForeignKey("BilledLegalPersonId")]
        public LegalPerson BilledLegalPerson { get; set; }
        [ForeignKey("PayedLegalPersonId")]
        public LegalPerson? PayedLegalPerson { get; set; }
        [ForeignKey("PayedIndividualId")]
        public Individual?  PayedIndividual { get; set; } 
        public ICollection<InvoiceItem> OrderItems { get; set; }
    }
}
