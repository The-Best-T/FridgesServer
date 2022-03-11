using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Tomato",
                    DefaulQuantity = 2
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Lemon",
                    DefaulQuantity = 1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Milk",
                    DefaulQuantity = 1
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Potato",
                    DefaulQuantity = 5
                },
                new Product
                {
                    Id= Guid.NewGuid(),
                    Name="Onion",
                    DefaulQuantity=2
                }
            );
        }
    }
}
