using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Pagination;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedList<Category>> GetAllCategoriesAsync(int pageIndex, int pageSize);
    }
}