using System;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Pagination;

namespace DAL.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<PagedList<Article>> GetAllByCategoryAsync(int pageIndex, int pageSize, string categoryId);
        Task<PagedList<Article>> GetAllByDateAsync(int pageIndex, int pageSize, DateTime dateFrom, DateTime dateTo);
        Task<PagedList<Article>> GetAllByCategoryAndDateAsync(int pageIndex, int pageSize, DateTime dateFrom, DateTime dateTo, string categoryId);
        Task<Article> GetArticleByIdAsync(string articleId);
    }
}