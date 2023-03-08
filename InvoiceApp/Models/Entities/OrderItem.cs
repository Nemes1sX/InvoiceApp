using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.Entities
{
    public class OrderItem : Entity
    { 
        public int BasePrice { get; set; }
        public int Quantity { get; set; }
        public int PriceWithVAT { get; set; }
        public Order? Order { get; set; }
    }
}
