namespace InvoiceApp.Models.Responses
{
    public class CountryResponse
    {
        public string name { get; set; }
        public List<string> topLevelDomain { get; set; }
        public string alpha2Code { get; set; }
        public List<string> callingCodes { get; set; }
        public string capital { get; set; }
        public List<string> altSpellings { get; set; }
        public string subregion { get; set; }
        public string region { get; set; }
        public int population { get; set; }
        public List<double> latlng { get; set; }
        public string demonym { get; set; }
        public double area { get; set; }
        public List<string> timezones { get; set; }
        public string nativeName { get; set; }
        public string numericCode { get; set; }
        public string flag { get; set; }
        public bool independent { get; set; }
    }
}
