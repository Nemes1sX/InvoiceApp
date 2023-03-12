using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models.Entities
{
    public class Order : Entity
    {
        public int TotalPrice { get; set; }
        public int LegalPersonId { get; set; }
        public int? PayerLegalLegalPersonId { get; set; }
        public int? PayerIndividualId { get; set; }
        [ForeignKey("LegalPersonId")]
        public LegalPerson LegalPerson { get; set; }
        [ForeignKey("PayerLegalPersonId")]
        public LegalPerson? PayerLegalPerson { get; set; }
        [ForeignKey("PayerIndividualId")]
        public Individual?  PayerIndividual { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
