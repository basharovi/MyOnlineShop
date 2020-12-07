using Microsoft.EntityFrameworkCore;
using my_online_shop.Models;

namespace my_online_shop.Migrations.Data
{
    public class PostgresDbContext : DbContext
    {
        private string _connectionString;
        private string _migrationAssemblyName;

        public PostgresDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseNpgsql(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<SpecialTag> SpecialTags { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
