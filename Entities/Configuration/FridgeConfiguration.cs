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
                    Id = new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"),
                    Name = "Fridge1",
                    OwnerName = "Boston Griffin",
                    ModelId = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                },
                new Fridge
                {
                    Id = new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"),
                    Name = "Fridge2",
                    OwnerName = "Silas Evans",
                    ModelId = new Guid("c15120c7-8e1f-4e85-b7a1-93a1b6bee619")
                },
                new Fridge
                {
                    Id = new Guid("a9e539af-02f4-40b3-a196-bdf070d850f4"),
                    Name = "Fridge3",
                    OwnerName = "Seth Hughes",
                    ModelId = new Guid("3aca17c8-2554-49f0-b43f-cfaceb030d7f")
                },
                new Fridge
                {
                    Id = new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"),
                    Name = "Fridge4",
                    OwnerName = "Gary Bryant",
                    ModelId = new Guid("c15120c7-8e1f-4e85-b7a1-93a1b6bee619")
                });
        }
    }
}
