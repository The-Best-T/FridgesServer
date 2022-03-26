using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FridgeModel> FridgeModels { get; set; }
        public DbSet<FridgeProduct> FridgeProducts { get; set; }
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Fridge>()
                .HasMany(f => f.Products)
                .WithMany(p => p.Fridges)
                .UsingEntity<FridgeProduct>(
                    j => j
                    .HasOne(fp => fp.Product)
                    .WithMany(p => p.FridgeProducts)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(fp => fp.ProductId),
                    j => j
                    .HasOne(fp => fp.Fridge)
                    .WithMany(f => f.FridgeProducts)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(fp => fp.FridgeId),
                    j =>
                    {
                        j.Property(fp => fp.Quantity).HasDefaultValue(0);
                        j.HasKey(fp => new { fp.FridgeId, fp.ProductId });
                        j.ToTable("FridgeProduct");
                    });

            builder.Entity<Fridge>()
                .HasOne(f => f.Model)
                .WithMany(m => m.Fridges)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ApplyConfiguration(new FridgeModelConfiguration());
            builder.ApplyConfiguration(new FridgeConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new FridgeProductConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
