using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceApp.Models.Entities
{
    public abstract class Person : Entity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country? Country { get; set; }
    }
}
