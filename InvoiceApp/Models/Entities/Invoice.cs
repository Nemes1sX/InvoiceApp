using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models.Entities
{
    public class Invoice : Entity
    {
        public int TotalPrice { get; set; }
        public int BilledLegalPersonId { get; set; }
        public int? PayedLegalPersonId { get; set; }
        public int? PayedIndividualId { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.Now;
        [ForeignKey("BilledLegalPersonId")]
        public LegalPerson BilledLegalPerson { get; set; }
        [ForeignKey("PayedLegalPersonId")]
        public LegalPerson? PayedLegalPerson { get; set; }
        [ForeignKey("PayedIndividualId")]
        public Individual?  PayedIndividual { get; set; } 
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
