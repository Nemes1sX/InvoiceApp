namespace InvoiceApp.Models.Dtos
{
    public abstract class PersonDto : BaseDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int CountryId { get; set; }
    }
}
