namespace InvoiceApp.Models.Entities
{
    public class LegalPerson : Person
    {
        public string? Name { get; set; }
        public bool VATPayer { get; set; }
    }
}
