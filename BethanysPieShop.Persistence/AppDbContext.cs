using BethanysPieShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
