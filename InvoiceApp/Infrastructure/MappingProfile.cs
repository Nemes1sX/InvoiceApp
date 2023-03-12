using AutoMapper;
using InvoiceApp.Models.Entities;
using InvoiceApp.Models.Responses;

namespace InvoiceApp.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CountryResponse, Country>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.name))
                .ForMember(dest => dest.Code, act => act.MapFrom(src => src.alpha2Code))
                .ForMember(dest => dest.Individuals, opt => opt.Ignore())
                .ForMember(dest => dest.LegalPersons, opt => opt.Ignore())
                .ForMember(dest => dest.VATPrecent, opt => opt.Ignore());
        }
    }
}
