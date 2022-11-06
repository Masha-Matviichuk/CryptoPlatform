using Api.Models;
using AutoMapper;
using BLL.Dtos;

namespace Api.Configuration
{
    public class APIAutoMapperProfile : Profile
    {
        public APIAutoMapperProfile()
        {
            CreateMap<SignUpDto, SignUpModel>()
                .ForMember(d => d.FirstName, m => m.MapFrom(user => user.FirstName))
                .ForMember(d => d.Email, m => m.MapFrom(user => user.Email))
                .ForMember(d => d.LastName, m => m.MapFrom(user => user.LastName))
                .ForMember(d => d.PhoneNumber, m => m.MapFrom(user => user.PhoneNumber))
                .ForMember(d => d.Password, m => m.MapFrom(user => user.Password))
                .ReverseMap();


            CreateMap<SignInDto, SignInModel>().ReverseMap();
        }
    }
}