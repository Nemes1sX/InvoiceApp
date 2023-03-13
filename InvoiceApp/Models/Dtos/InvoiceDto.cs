namespace InvoiceApp.Models.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceDto : BaseDto
    {
        public decimal TotalPrice { get; set; }
        public string? IssueDate { get; set; }
        public int BilledLegalPersonId { get; set; }
        public int? PayedLegalPersonId { get; set; }
        public int? PayedIndividualId { get; set; }
        public ICollection<InvoiceItemDto> InvoiceItems { get; set; }
    }
}
