using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Model;
using DatingApp.API.Helper;

namespace DatingApp.API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles (){
        CreateMap<User, UserForDetaildDTO>()
        .ForMember(dest =>dest.photoUrl,opt => {
         opt.MapFrom(src => src.Photo.FirstOrDefault(p =>p.IsMain).Url);   
        })
        .ForMember(dest => dest.Age , opt => {
            opt.ResolveUsing(d=>d.DateOfBirth.calculateAge());
        })
        ;
        CreateMap<User, UserForListDTO>().ForMember(dest =>dest.photoUrl,opt => {
         opt.MapFrom(src => src.Photo.FirstOrDefault(p =>p.IsMain).Url);   
        })
        .ForMember(dest => dest.Age , opt => {
            opt.ResolveUsing(d=>d.DateOfBirth.calculateAge());
        })
        ;
        CreateMap<Photo, PhotoForDetailedDto>();
        }
    }
}