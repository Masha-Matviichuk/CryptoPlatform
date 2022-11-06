using System.Threading.Tasks;
using Api.Models;
using BLL.Dtos;
using DAL.Entities;
using DAL.Pagination;

namespace BLL.Interfaces.Strategy
{
    public interface IFilter
    {
        Task<PagedList<ArticleDto>> GetFilteredArticlesAsync(int pageIndex, int pageSize, FilterOptions filterOptions);
    }
}