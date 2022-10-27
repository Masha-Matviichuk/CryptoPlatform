using System.Threading.Tasks;
using DAL.Pagination;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> 
    {
        Task<PagedList<TEntity>> GetAllAsync(int pageIndex, int pageSize);
       
        Task<TEntity> GetByIdAsync(string id);
      
        Task<TEntity> CreateAsync(TEntity entity);
      
        Task<TEntity> UpdateAsync(TEntity entity);
      
        Task<TEntity> SoftDeleteByIdAsync(string id);
    }
}