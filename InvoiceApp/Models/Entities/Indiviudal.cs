namespace InvoiceApp.Models.Entities
{
    public abstract class Person : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Country? Country { get; set; }
    }
}
