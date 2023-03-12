namespace InvoiceApp.Models.Entities
{
    public class Country : Entity
    {
        public string? Name { get; set; } 
        public string? Code { get; set; }
        public int VATPrecent { get; set; } = 0;
        /*public int VATPrecentNonEU { 
            get 
            { 
                return _VATPrecent;
            }
            set 
            { 
                _VATPrecent = 0;
            }
        } 
        public int VATPrecentEU
        {
            get
            {
                return _VATPrecent;
            }
            set
            {
                if (_VATPrecent > 0)
                {
                    _VATPrecent = value;
                }
                else
                {
                    _VATPrecent = 0;
                }
            }
        }*/
        public bool EuropeanUnion { get; set; } = false;
        public ICollection<LegalPerson>? LegalPersons { get; set;}
        public ICollection<Individual>? Individuals { get; set;}
    }
}
