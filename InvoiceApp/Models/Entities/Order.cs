namespace InvoiceApp.Models.Entities
{
    public class Order : Entity
    {
        public int TotalPrice { get; set; }
        public LegalPerson LegalPerson { get; set; }
        public LegalPerson? PayerLegalPerson { get; set; }
        public Individual?  PayerIndividual { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
