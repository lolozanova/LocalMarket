using LocalMarket.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LocalMarket.Data
{
    public class LocalMarketDbContext : IdentityDbContext
    {
        public LocalMarketDbContext(DbContextOptions<LocalMarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Unit> Units { get; init; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            builder.Entity<Product>()
                   .Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            base.OnModelCreating(builder);

        }
    }
}
