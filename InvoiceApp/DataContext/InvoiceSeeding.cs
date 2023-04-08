using InvoiceApp.Models.Responses;
using InvoiceApp.Models.Entities;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using InvoiceApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.DataContext
{
    public class InvoiceSeeding
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly InvoiceDataContext _context;

        public InvoiceSeeding(IHttpClientFactory httpClientFactory, InvoiceDataContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        public static List<EuVATCountry> EuVATCountries = new List<EuVATCountry>
        {
            new EuVATCountry{ CountryCode = "LT", VATRate = 21},
            new EuVATCountry{ CountryCode = "LV", VATRate = 21},
            new EuVATCountry{ CountryCode = "EE", VATRate = 20},
            new EuVATCountry{ CountryCode = "FI", VATRate = 24},
            new EuVATCountry{ CountryCode = "SE", VATRate = 25},
            new EuVATCountry{ CountryCode = "PL", VATRate = 23},
            new EuVATCountry{ CountryCode = "DE", VATRate = 19},
            new EuVATCountry{ CountryCode = "FR", VATRate = 20},
            new EuVATCountry{ CountryCode = "ES", VATRate = 21},
            new EuVATCountry{ CountryCode = "PT", VATRate = 23},
            new EuVATCountry{ CountryCode = "BE", VATRate = 21},
            new EuVATCountry{ CountryCode = "LU", VATRate = 17},
            new EuVATCountry{ CountryCode = "DK", VATRate = 25},
            new EuVATCountry{ CountryCode = "NL", VATRate = 21},
            new EuVATCountry{ CountryCode = "IE", VATRate = 23},
            new EuVATCountry{ CountryCode = "IT", VATRate = 22},
            new EuVATCountry{ CountryCode = "GR", VATRate = 24},
            new EuVATCountry{ CountryCode = "CZ", VATRate = 21},
            new EuVATCountry{ CountryCode = "SK", VATRate = 20},
            new EuVATCountry{ CountryCode = "AT", VATRate = 20},
            new EuVATCountry{ CountryCode = "HU", VATRate = 27},
            new EuVATCountry{ CountryCode = "BG", VATRate = 20},
            new EuVATCountry{ CountryCode = "RO", VATRate = 19},
            new EuVATCountry{ CountryCode = "CY", VATRate = 19},
            new EuVATCountry{ CountryCode = "HR", VATRate = 25},
            new EuVATCountry{ CountryCode = "SI", VATRate = 22},
            new EuVATCountry{ CountryCode = "MT", VATRate = 21},
        };

        public void Seed()
        {
            SeedCountries();
            SeedLegalPeopleAndIndividuals();

            _context.SaveChanges();
        }

        private void SeedLegalPeopleAndIndividuals()
        {
            if (_context.Individuals.Any())
            {
                return;
            }

            var individuals = new List<Individual>
            {
                new Individual { FirstName = "Testas", LastName = "Testesnis", CountryId = 50},
                new Individual { FirstName = "Petras", LastName = "Petraitis", CountryId = 17},
                new Individual { FirstName = "Jonas", LastName = "Jonaitis", CountryId = 6},
            };

            if (_context.LegalPersons.Any())
            {
                return;
            }

            var legalPersons = new List<LegalPerson>
            {
                new LegalPerson {Name = "Testas", FirstName = "Testas", LastName = "Testenis", VATPayer = true, CountryId = 17 },
                new LegalPerson {Name = "Silke", FirstName = "Jonas", LastName = "Jonaitis", VATPayer = false, CountryId = 5},
                new LegalPerson {Name = "Ausis", FirstName = "Petras", LastName = "Petraite", VATPayer = true, CountryId = 5},
            };

            _context.Individuals.AddRange(individuals);
            _context.LegalPersons.AddRange(legalPersons);
        }

        private void SeedCountries()
        {
            if (_context.Countries.Any())
            {
                return;
            }

            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();
            var url = config.GetValue<string>("UrlSettings:CountriesApiUrl");
            var client = _httpClientFactory.CreateClient("InvoiceSeeding");
            var mapper = serviceProvider.GetService<IMapper>();
            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                mapper = mappingConfig.CreateMapper();
            }

            var EUCountriesResponse = SeedEUCountries(client, url);
            var africaCountries = SeedAfricaCountries(client, mapper, url);
            var asiaCountries = SeedAsiaCountries(client, mapper, url);
            var americasCountries = SeedAmericasCountries(client, mapper, url);
            var australiaCountries = SeedAustraliaCountries(client, mapper, url);
            var EUCountries = MapEUCountries(EUCountriesResponse);

            _context.Countries.AddRange(EUCountries);
            _context.Countries.AddRange(asiaCountries);
            _context.Countries.AddRange(africaCountries);
            _context.Countries.AddRange(americasCountries);
            _context.Countries.AddRange(australiaCountries);
        }

        private List<CountryResponse> SeedEUCountries(HttpClient client, string url)
        {
            var response = client.GetFromJsonAsync<List<CountryResponse>>(url + "albloc/EU").GetAwaiter().GetResult();
            var result = response.Where(x => x.Independent).ToList();
            return result;
        }

        private List<Country> SeedAmericasCountries(HttpClient client, IMapper mapper, string url)
        {
            var response = client.GetFromJsonAsync<List<CountryResponse>>(url + "/americas").GetAwaiter().GetResult();
            var americaCountries = mapper.Map<List<Country>>(response);
            return americaCountries;
        }
        private List<Country> SeedAsiaCountries(HttpClient client, IMapper mapper, string url)
        {
            var response = client.GetFromJsonAsync<List<CountryResponse>>(url + "/asia").GetAwaiter().GetResult();
            var asiaCountries = mapper.Map<List<Country>>(response);
            return asiaCountries;
        }
        private List<Country> SeedAfricaCountries(HttpClient client, IMapper mapper, string url)
        {
            var response = client.GetFromJsonAsync<List<CountryResponse>>(url + "/africa").GetAwaiter().GetResult();
            var africaCountries = mapper.Map<List<Country>>(response);
            return africaCountries;
        }
        private List<Country> SeedAustraliaCountries(HttpClient client, IMapper mapper, string url)
        {
            var response = client.GetFromJsonAsync<List<CountryResponse>>(url + "/oceania").GetAwaiter().GetResult();
            var australiaCountries = mapper.Map<List<Country>>(response);
            return australiaCountries;
        }

        private List<Country> MapEUCountries(List<CountryResponse> countryResponses)
        {
            var EUcountryList = new List<Country>();

            foreach (var result in countryResponses)
            {
                var EUcountry = new Country();
                EUcountry.Code = result.Alpha2Code;
                EUcountry.Name = result.Name;
                EUcountry.EuropeanUnion = true;
                var EuVATCountry = EuVATCountries.SingleOrDefault(x => x.CountryCode == result.Alpha2Code);
                if (EuVATCountry != null)
                {
                    EUcountry.VATPrecent = EuVATCountry.VATRate;
                }
                EUcountryList.Add(EUcountry);
            }

            return EUcountryList;
        }
    }
}
