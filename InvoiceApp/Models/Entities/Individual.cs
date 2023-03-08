namespace InvoiceApp.Models.Entities
{
    public class Individual : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Country? Country { get; set; }
    }
}
