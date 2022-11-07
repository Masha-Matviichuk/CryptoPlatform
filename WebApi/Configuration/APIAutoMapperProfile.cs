using AutoMapper;
using BLL.Dtos;
using WebApi.Models;

namespace WebApi.Configuration
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

            CreateMap<CustomerMailRequest, MessageModel>()
                .ForMember(d => d.EmailFrom, m => m.MapFrom(m => m.FromAddress))
                .ForMember(d => d.EmailTo, m => m.MapFrom(m => m.ToAddress))
                .ForMember(d => d.Subject, m => m.MapFrom(m => m.Subject))
                .ForMember(d => d.Body, m => m.MapFrom(m => m.Body))
                .ReverseMap();
        }
    }
}