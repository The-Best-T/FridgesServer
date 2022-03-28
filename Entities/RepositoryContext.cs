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

            builder.ApplyConfiguration(new FridgeModelConfiguration());
            builder.ApplyConfiguration(new FridgeConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new FridgeProductConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
