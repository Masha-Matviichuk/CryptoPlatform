using System;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.UoW
{
    public class UnitOfWork : IUnitOfWork
        {
            private readonly PlatformDbContext _context;


            public IArticleRepository ArticleRepository { get; }
            public IRepository<Category> CategoryRepository { get; }
            public IRepository<Picture> PictureRepository { get; }
            public IRepository<Source> SourceRepository { get; }

            public UnitOfWork(PlatformDbContext context, IArticleRepository articleRepository,
                IRepository<Category> categoryRepository, IRepository<Picture> pictureRepository,
                IRepository<Source> sourceRepository)
            {
                _context = context;
                ArticleRepository = articleRepository;
                CategoryRepository = categoryRepository;
                PictureRepository = pictureRepository;
                SourceRepository = sourceRepository;
            }


            public async Task SaveChangesAsync()
            {
                await _context.SaveChangesAsync();
            }

            public int ExecuteCustomQuery<T>(FormattableString command)
            {
                return _context.Database.ExecuteSqlInterpolated(command);
            }
        }
    }
