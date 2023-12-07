using AutoMapper;
using ChauffeurApp.Application.DTOs;
using ChauffeurApp.Core.Entities;

namespace ChauffeurApp.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDTO>().ReverseMap();
            // SOURCE,DESTINATION
            CreateMap<BrandCreateDTO, Brand>().ReverseMap();

           CreateMap<Company,CreateCompanyDTO>().ReverseMap();
        }
    }
}
