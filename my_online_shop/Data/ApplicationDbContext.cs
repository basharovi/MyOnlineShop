using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyOnlineShop.Models;

namespace MyOnlineShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<SpecialTag> SpecialTags { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; } 

        public new DbSet<ApplicationUser> Users { get; set; }

    }

    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<SpecialTag> SpecialTags { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
