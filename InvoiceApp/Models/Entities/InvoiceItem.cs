using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.Entities
{
    public class InvoiceItem : Entity
    { 
        public int BasePrice { get; set; }
        public int TotalItemPrice { get; set; }
        public int Quantity { get; set; }
        public int PriceWithVAT { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
