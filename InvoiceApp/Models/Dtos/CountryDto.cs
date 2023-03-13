namespace InvoiceApp.Models.Dtos
{
    public class CountryDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int VATPrecent { get; set; } = 0;
        public bool EuropeanUnion { get; set; } = false;
    }
}
