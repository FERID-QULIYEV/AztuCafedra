using AztuKafedra.Models;
using Microsoft.EntityFrameworkCore;

namespace AztuKafedra.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ParentCategory> Parentcategory { get; set; }
        public DbSet<ChildCategory> ChildCategory { get; set; }
        public DbSet<BigParentsCategory> BigParentsCategory { get; set; }
        public DbSet<User>User{ get; set; }
        public DbSet<Pasition> Pasitions { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<News> News { get; set; } 
    }
}
