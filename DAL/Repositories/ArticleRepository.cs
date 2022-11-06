using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly PlatformDbContext _context;
        private readonly ILogger<Repository<Article>> _logger;
        
        public ArticleRepository(PlatformDbContext context, ILogger<Repository<Article>> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PagedList<Article>> GetAllByCategoryAsync(int pageIndex, int pageSize, string categoryId)
        {
            PagedList<Article> allList = default;
            try
            {
                allList = await _context.Set<Article>().Where(x=> x.CategoryId == categoryId)
                    .Include(x=>x.Picture)
                    .Include(x=>x.Category)
                    .Include(x=>x.Source)
                    .ToPagedListAsync(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return allList;
        }

        public async Task<PagedList<Article>> GetAllByDateAsync(int pageIndex, int pageSize, DateTime dateFrom, DateTime dateTo)
        {
            PagedList<Article> allList = default;
            try
            {
                if (dateFrom==null && dateTo !=null)
                {
                    allList = await _context.Set<Article>().Where(x=> x.DateCreated <= dateTo)
                        .Include(x=>x.Picture)
                        .Include(x=>x.Category)
                        .Include(x=>x.Source)
                        .ToPagedListAsync(pageIndex, pageSize);   
                } else if (dateFrom != null && dateTo == null)
                {
                    allList = await _context.Set<Article>().Where(x=> x.DateCreated >= dateFrom)
                        .Include(x=>x.Picture)
                        .Include(x=>x.Category)
                        .Include(x=>x.Source)
                        .ToPagedListAsync(pageIndex, pageSize);   
                }else if (dateFrom != null && dateTo != null)
                {
                    allList = await _context.Set<Article>().Where(x=> x.DateCreated >= dateFrom && x.DateCreated <= dateTo)
                        .Include(x=>x.Picture)
                        .Include(x=>x.Category)
                        .Include(x=>x.Source)
                        .ToPagedListAsync(pageIndex, pageSize);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return allList;
        }

        public async Task<PagedList<Article>> GetAllByCategoryAndDateAsync(int pageIndex, int pageSize, DateTime dateFrom, DateTime dateTo, string categoryId)
        {
            PagedList<Article> allList = default;
            try
            {
                var filtered = await _context.Set<Article>().Where(x => x.CategoryId == categoryId)
                    .Include(x => x.Picture)
                    .Include(x => x.Category)
                    .Include(x => x.Source).ToListAsync();
                
                if (dateFrom==null && dateTo !=null)
                {
                    allList = await filtered.Where(x=> x.DateCreated <= dateTo).AsQueryable()
                        .ToPagedListAsync(pageIndex, pageSize);   
                } else if (dateFrom != null && dateTo == null)
                {
                    allList = await filtered.Where(x=> x.DateCreated >= dateFrom).AsQueryable()
                        .ToPagedListAsync(pageIndex, pageSize);   
                }else if (dateFrom != null && dateTo != null)
                {
                    allList = await _context.Set<Article>().Where(x=> x.DateCreated >= dateFrom && x.DateCreated <= dateTo).AsQueryable()
                        .ToPagedListAsync(pageIndex, pageSize);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return allList;
        }

        public async Task<Article> GetArticleByIdAsync(string articleId)
        {
            Article entity = null;
            try
            {
                entity = await _context.Set<Article>().Where(x=>x.Id == articleId)
                    .Include(x=>x.Picture)
                    .Include(x=>x.Category)
                    .Include(x=>x.Source).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return entity;
        }
    }
}