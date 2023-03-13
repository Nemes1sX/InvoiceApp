namespace InvoiceApp.Models.Dtos
{
    public class InvoiceItemDto : BaseDto
    {
        public decimal BasePrice { get; set; }
        public decimal TotalItemPrice { get; set; }
        public int Quantity { get; set; }
        public decimal PriceWithVAT { get; set; }
    }
}
