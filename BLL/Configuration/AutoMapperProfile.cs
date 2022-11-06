using AutoMapper;
using BLL.Dtos;
using DAL.Entities;
using DAL.Pagination;

namespace BLL.Configuration
{
    public class AutoMapperProfile  : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(d => d.Id, m => m.MapFrom(a => a.Id))
                .ForMember(d => d.Title, m => m.MapFrom(a => a.Title))
                .ForMember(d => d.URL, m => m.MapFrom(a => a.URL))
                .ForMember(d => d.AuthorName, m => m.MapFrom(a => a.AuthorName))
                .ForMember(d => d.DateCreated, m => m.MapFrom(a => a.DateCreated))
                .ForMember(d => d.DateUploaded, m => m.MapFrom(a => a.DateUploaded))
                .ForMember(d => d.Text, m => m.MapFrom(a => a.Text))
                .ForMember(d => d.LikesCount, m => m.MapFrom(a => a.LikesCount))
                .ForMember(d => d.DislikesCount, m => m.MapFrom(a => a.DislikesCount))
                .ForMember(d => d.Picture, m => m.MapFrom(a => a.Picture.Data))
                .ForMember(d => d.CategoryName, m => m.MapFrom(a => a.Category.Name))
                .ForMember(d => d.SourceName, m => m.MapFrom(a => a.Source.Name))
                .ReverseMap();
            
            CreateMap<PagedList<Article>, PagedList<ArticleDto>>()
                .ForMember(d => d.Items, m => m.MapFrom(a => a.Items)).ReverseMap();
            
            CreateMap<User, SignUpDto>()
                .ForMember(d=> d.Email, m => m.MapFrom(user=>user.Email))
                .ForMember(d=> d.Email, m => m.MapFrom(user=>user.UserName))
                .ForMember(d=> d.PhoneNumber, m => m.MapFrom(user=>user.PhoneNumber))
                .ForMember(d=> d.Password, m => m.MapFrom(user=>user.PasswordHash))
                .ReverseMap();
            
            CreateMap<User, UserDto>()
                .ForMember(d=> d.Email, m => m.MapFrom(user=>user.Email))
                .ForMember(d=> d.Email, m => m.MapFrom(user=>user.UserName))
                .ForMember(d=> d.PhoneNumber, m => m.MapFrom(user=>user.PhoneNumber))
                .ForMember(d=> d.Password, m => m.MapFrom(user=>user.PasswordHash))
                .ForMember(d=> d.FirstName, m => m.MapFrom(user=>user.FirstName))
                .ForMember(d=> d.LastName, m => m.MapFrom(user=>user.LastName))
                .ReverseMap();
        }
    }
}