using System;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.UoW
{
    public interface IUnitOfWork
    {
        IArticleRepository ArticleRepository { get;  }
        IRepository<Category> CategoryRepository { get;  }
        IRepository<Picture> PictureRepository { get;  }
        IRepository<Source> SourceRepository { get;  }
        Task SaveChangesAsync();
        int ExecuteCustomQuery<T>(FormattableString command);
    }
}