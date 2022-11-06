using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Pagination;
using DAL.UoW;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<PagedList<Category>> GetAllCategoriesAsync(int pageIndex, int pageSize)
        {
            return await _uow.CategoryRepository.GetAllAsync(pageIndex, pageSize);
        }
    }
}