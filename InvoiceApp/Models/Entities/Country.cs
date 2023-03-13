namespace InvoiceApp.Models.Entities
{
    public class Country : Entity
    {
        public string? Name { get; set; } 
        public string? Code { get; set; }
        public int VATPrecent { get; set; } = 0;
        public bool EuropeanUnion { get; set; } = false;
        public ICollection<LegalPerson>? LegalPersons { get; set;}
        public ICollection<Individual>? Individuals { get; set;}
    }
}
