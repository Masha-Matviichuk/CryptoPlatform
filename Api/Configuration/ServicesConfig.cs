using DAL.EF;
using DAL.EF.Auth;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.UoW;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class ServicesConfig
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            var platformDbConnectionString = builder.Configuration.GetConnectionString("PlatformDB");

            builder.Services.AddDbContext<PlatformDbContext>(options => options.UseSqlServer(platformDbConnectionString));

            var identityDbConnectionString = builder.Configuration.GetConnectionString("IdentityDB");

            builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(identityDbConnectionString));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IRepository<Article>, Repository<Article>>();
            builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
            builder.Services.AddScoped<IRepository<Source>, Repository<Source>>();
            builder.Services.AddScoped<IRepository<Picture>, Repository<Picture>>();

            
            return builder;
        }
    }
}