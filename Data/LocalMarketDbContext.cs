using LocalMarket.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LocalMarket.Data
{
    public class LocalMarketDbContext : IdentityDbContext<User>
    {
        public LocalMarketDbContext(DbContextOptions<LocalMarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Unit> Units { get; init; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<Town> Towns { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            builder.Entity<Product>()
                   .Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Entity<Producer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Producer>(p => p.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(p => p.Producer)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProducerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Product>()
              .HasOne(p => p.Category)
              .WithMany(c => c.Products)
              .HasForeignKey(p => p.CategoryId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
             .Entity<Product>()
             .HasOne(p => p.Unit)
             .WithMany(c => c.Products)
             .HasForeignKey(p => p.UnitId)
             .OnDelete(DeleteBehavior.Restrict);


            builder
             .Entity<Product>()
             .HasOne(p => p.Producer)
             .WithMany(c => c.Products)
             .HasForeignKey(p => p.ProducerId)
             .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);

        }
    }
}
