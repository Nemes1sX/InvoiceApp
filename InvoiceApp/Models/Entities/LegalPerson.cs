namespace InvoiceApp.Models.Entities
{
    public class LegalPerson : Individual
    {
        public string? Name { get; set; }
        public bool VATPayer { get; set; }
    }
}
