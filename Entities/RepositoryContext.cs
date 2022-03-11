using Microsoft.EntityFrameworkCore;
using Entities.Models;
namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FridgeModel> FridgeModels { get; set; }
        public RepositoryContext(DbContextOptions options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Fridge>()
                .HasMany(f => f.Products)
                .WithMany(p => p.Fridges)
                .UsingEntity<FridgeProduct>(
                    j => j
                    .HasOne(fp => fp.Product)
                    .WithMany(p => p.FridgeProducts)
                    .HasForeignKey(fp => fp.ProductId),
                    j => j
                    .HasOne(fp => fp.Fridge)
                    .WithMany(f => f.FridgeProducts)
                    .HasForeignKey(fp => fp.FridgeId),
                    j =>
                    {
                        j.Property(fp => fp.Quantity).HasDefaultValue(0);
                        j.HasKey(fp => new { fp.FridgeId, fp.ProductId });
                        j.ToTable("FridgeProduct");
                    });
            base.OnModelCreating(builder);
        }
    }
}
