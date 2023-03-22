namespace InvoiceApp.Models.Responses
{
    public class CountryResponse
    {
        public string Name { get; set; }
        public List<string> TopLevelDomain { get; set; }
        public string Alpha2Code { get; set; }
        public List<string> CallingCodes { get; set; }
        public string Capital { get; set; }
        public List<string> AltSpellings { get; set; }
        public string Subregion { get; set; }
        public string Region { get; set; }
        public int Population { get; set; }
        public List<double> Latlng { get; set; }
        public string Demonym { get; set; }
        public double Area { get; set; }
        public List<string> Timezones { get; set; }
        public string NativeName { get; set; }
        public string NumericCode { get; set; }
        public string Flag { get; set; }
        public bool Independent { get; set; }
    }
}
