using AutoMapper;
using P229FirstApi.DTOs.AuthDTOs;
using P229FirstApi.DTOs.CategroryDTOs;
using P229FirstApi.Entities;

namespace P229FirstApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryListDto>();

            CreateMap<RegisterDto, AppUser>();
                //.ForMember(des=>des.Count,src=>src.MapFrom(c=>c.Products.Count()));

            //CreateMap<CategoryPutDto, Category>();
            //CreateMap<Category, CategoryListDto>().ReverseMap();

            //CreateMap<Category, CategoryListDto>()
            //    .ForMember(des => des.test, src => src.MapFrom(c => c.Id))
            //    .ForMember(des => des.ad, src => src.MapFrom(c => c.Name))
            //    .ForMember(des => des.elebel, src => src.MapFrom(c =>"Ozum Yazdim"));
        }
    }
}
