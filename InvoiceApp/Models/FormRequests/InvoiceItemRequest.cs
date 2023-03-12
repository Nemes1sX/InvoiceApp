using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.Models.FormRequests
{
    public class InvoiceItemRequest
    {
        [Required, Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }
        [Required, Range(0, Double.MaxValue)]
        public double ItemPrice { get; set; }
    }
}
