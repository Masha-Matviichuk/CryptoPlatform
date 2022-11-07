using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Services.Strategy;
using DAL.Entities;
using DAL.Pagination;
using DAL.UoW;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class ArticleService :IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ArticleService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
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

        public async Task<ArticleDto> AddLikeAsync(string articleId, string userId)
        {
            if (articleId == null)
                throw new ArgumentNullException(nameof(articleId),"This id is null");
            if (userId == null)
                throw new ArgumentNullException(nameof(userId),"This id is null");

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var article = await _unitOfWork.ArticleRepository.GetByIdAsync(articleId);
            if (user.UserLikes.Contains(articleId))
            {
                article.LikesCount -= 1;
            } else if (!user.UserLikes.Contains(articleId) && !user.UserDislikes.Contains(articleId))
            {
                article.LikesCount += 1;
            }else if (!user.UserLikes.Contains(articleId) && user.UserDislikes.Contains(articleId))
            {
                article.LikesCount += 1;
                article.DislikesCount -= 1;
            }
            
            var updated = await _unitOfWork.ArticleRepository.UpdateAsync(article);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ArticleDto>(updated);
        }

        public async Task<ArticleDto> AddDislikeAsync(string articleId, string userId)
        {
            if (articleId == null)
                throw new ArgumentNullException(nameof(articleId),"This id is null");
            if (userId == null)
                throw new ArgumentNullException(nameof(userId),"This id is null");

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var article = await _unitOfWork.ArticleRepository.GetByIdAsync(articleId);
            if (user.UserDislikes.Contains(articleId))
            {
                article.DislikesCount -= 1;
            } else if (!user.UserDislikes.Contains(articleId) && !user.UserLikes.Contains(articleId))
            {
                article.DislikesCount += 1;
            }else if (!user.UserDislikes.Contains(articleId) && user.UserLikes.Contains(articleId))
            {
                article.DislikesCount += 1;
                article.LikesCount -= 1;
            }
            
            var updated = await _unitOfWork.ArticleRepository.UpdateAsync(article);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ArticleDto>(updated);
        }
    }
}