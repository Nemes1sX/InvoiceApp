namespace InvoiceApp.Models.Entities
{
    public class LegalPerson : Indiviudal
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool VATPayer { get; set; }
    }
}
