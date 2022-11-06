using System.Threading.Tasks;
using Api.Models;
using BLL.Dtos;
using DAL.Pagination;

namespace BLL.Interfaces
{
    public interface IArticleService
    {
        Task<PagedList<ArticleDto>> GetArticlesByFilterAsync(int pageIndex, int pageSize, FilterOptions filterOptions);
        Task<PagedList<ArticleDto>> GetAllArticlesAsync(int pageIndex, int pageSize);
        Task<ArticleDto> GetArticleByIdAsync(string articleId);
        Task<ArticleDto> AddLikeAsync(string articleId);
        Task<ArticleDto> AddDislikeAsync(string articleId);
        //Task<PagedList<ArticleDto>> DeleteArticleAsync(int pageIndex, int pageSize, string categoryId);
    }
}