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
                    Id = new Guid("f480ecb3-2f19-4d0a-b1b5-9be38ff31a79"),
                    Name = "Tomato",
                    DefaultQuantity = 2
                },
                new Product
                {
                    Id = new Guid("f66b7398-4bf7-48e5-843a-4a5e956fbaef"),
                    Name = "Lemon",
                    DefaultQuantity = 1
                },
                new Product
                {
                    Id = new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"),
                    Name = "Milk",
                    DefaultQuantity = 1
                },
                new Product
                {
                    Id = new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"),
                    Name = "Potato",
                    DefaultQuantity = 5
                },
                new Product
                {
                    Id = new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"),
                    Name = "Onion",
                    DefaultQuantity = 2
                });
        }
    }
}
