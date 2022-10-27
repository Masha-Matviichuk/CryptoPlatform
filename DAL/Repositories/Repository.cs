using System;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Pagination;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly PlatformDbContext _context;
        private readonly ILogger<Repository<TEntity>> _logger;

        public Repository(PlatformDbContext context, ILogger<Repository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PagedList<TEntity>> GetAllAsync(int pageIndex, int pageSize)
        {
            PagedList<TEntity> allList = default;
            try
            {
                allList = await _context.Set<TEntity>().ToPagedListAsync(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return allList;
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            TEntity entity = null;
            try
            {
                entity = await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            EntityEntry newEntity = null;
            try
            {
                newEntity = await _context.Set<TEntity>().AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return (TEntity)newEntity.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            EntityEntry newEntity = null;
            try
            {
                newEntity = _context.Set<TEntity>().Attach(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return (TEntity)newEntity.Entity;
        }

        public async Task<TEntity> SoftDeleteByIdAsync(string id)
        {
            TEntity entity = null;
            try
            {
                entity = await _context.Set<TEntity>().FindAsync(id);
                if (entity == null) throw new NullReferenceException();
                entity.IsDeleted = true; 
                entity = _context.Set<TEntity>().Attach(entity).Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return entity;
        }
    }
}