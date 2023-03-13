namespace InvoiceApp.Models.Dtos
{
    public class LegalPersonDto : PersonDto
    {
        public string? Name { get; set; }
        public bool VATPayer { get; set; }
    }
}
