using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class FridgeConfiguration : IEntityTypeConfiguration<Fridge>
    {
        public void Configure(EntityTypeBuilder<Fridge> builder)
        {
            builder.HasData
            (
                new Fridge
                {
                    Id = Guid.NewGuid(),
                    Name="Fridge1",
                    OwnerName = "Boston Griffin",
                    ModelId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a")
                },
                new Fridge
                {
                    Id = Guid.NewGuid(),
                    Name = "Fridge2",
                    OwnerName = "Silas Evans",
                    ModelId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a")
                },
                new Fridge
                {
                    Id = Guid.NewGuid(),
                    Name = "Fridge3",
                    OwnerName = "Seth Hughes",
                    ModelId = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811")
                },
                new Fridge
                {
                    Id = Guid.NewGuid(),
                    Name = "Fridge4",
                    OwnerName = "Gary Bryant",
                    ModelId = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a")
                });
        }
    }
}
