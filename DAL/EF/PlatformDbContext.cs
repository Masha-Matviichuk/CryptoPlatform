using System.Net.Mime;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class PlatformDbContext : DbContext
    {
        public PlatformDbContext() : base()
        {

        }
        public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options)
        {
           
        }
        
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=CryptoPlatformDb;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
