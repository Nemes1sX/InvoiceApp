using AutoMapper;
using InvoiceApp.Models.Entities;
using InvoiceApp.Models.Responses;
using InvoiceApp.Models.Dtos;

namespace InvoiceApp.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CountryResponse, Country>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Code, act => act.MapFrom(src => src.Alpha2Code))
                .ForMember(dest => dest.Individuals, opt => opt.Ignore())
                .ForMember(dest => dest.LegalPersons, opt => opt.Ignore())
                .ForMember(dest => dest.VATPrecent, opt => opt.Ignore());
            CreateMap<Country, CountryDto>();
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(dest => dest.IssueDate, act => act.MapFrom(src => src.IssueDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.TotalPrice, act => act.MapFrom(src => src.TotalPrice / 100m));
            CreateMap<InvoiceItem, InvoiceItemDto>()
                .ForMember(dest => dest.TotalItemPrice, act => act.MapFrom(src => src.TotalItemPrice / 100m))
                .ForMember(dest => dest.BasePrice, act => act.MapFrom(src => src.BasePrice / 100m))                 
                .ForMember(dest => dest.PriceWithVAT, act => act.MapFrom(src => src.PriceWithVAT / 100m)); 
            CreateMap<Individual, IndividualDto>(); 
            CreateMap<LegalPerson, LegalPersonDto>();
        }
    }
}
