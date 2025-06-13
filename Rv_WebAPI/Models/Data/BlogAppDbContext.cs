using Microsoft.EntityFrameworkCore;
using Rv_WebAPI.Models.Entity;

namespace Rv_WebAPI.Models.Data
{
    public class BlogAppDbContext : DbContext
    {
        public BlogAppDbContext(DbContextOptions<BlogAppDbContext> options) : base(options) { }
        public DbSet<BlogAppModel> BlogAppModel { get; set; }
        public DbSet<BlogAppCatagotyModel> BlogAppCatagotyModels { get; set; }
    }
}
