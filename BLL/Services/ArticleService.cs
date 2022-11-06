using System;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Services.Strategy;
using DAL.Entities;
using DAL.Pagination;
using DAL.UoW;

namespace BLL.Services
{
    public class ArticleService :IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<ArticleDto>> GetArticlesByFilterAsync(int pageIndex, int pageSize, FilterOptions filterOptions)
        {
            if (pageIndex < 0)
                throw new ArgumentNullException();
            if (pageSize < 1)
                throw new ArgumentNullException();
            PagedList<ArticleDto> result;

            if (filterOptions.CategoryId!=null && filterOptions.DateFrom==null && filterOptions.DateTo==null)
            {
                var context = new FilterContext(new CategoryFilter(_unitOfWork, _mapper));
                result = await context.FindArticles(pageIndex, pageSize, filterOptions);
            }else if (filterOptions.CategoryId == null && (filterOptions.DateFrom != null ||
                      filterOptions.DateTo != null))
            {
                var context = new FilterContext(new DateFilter(_unitOfWork, _mapper));
                result = await context.FindArticles(pageIndex, pageSize, filterOptions);
            }
            else
            {
                var context = new FilterContext(new CategoryAndDateFilter(_unitOfWork, _mapper));
                result = await context.FindArticles(pageIndex, pageSize, filterOptions);
            }

            return result;
        }

        public async Task<PagedList<ArticleDto>> GetAllArticlesAsync(int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
                throw new ArgumentNullException();
            if (pageSize < 1)
                throw new ArgumentNullException();
            var articles = await _unitOfWork.ArticleRepository.GetAllAsync(pageIndex, pageSize);
            var articlesDto =  _mapper.Map<PagedList<ArticleDto>>(articles);
            return articlesDto;
        }

        public async Task<ArticleDto> GetArticleByIdAsync(string articleId)
        {
            if (string.IsNullOrEmpty(articleId))
                throw new ArgumentNullException(articleId);
            var article = await _unitOfWork.ArticleRepository.GetArticleByIdAsync(articleId);
            return _mapper.Map<ArticleDto>(article);
        }

        public Task<ArticleDto> AddLikeAsync(string articleId)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleDto> AddDislikeAsync(string articleId)
        {
            throw new NotImplementedException();
        }
    }
}