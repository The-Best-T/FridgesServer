using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class FridgeModelConfiguration : IEntityTypeConfiguration<FridgeModel>
    {
        public void Configure(EntityTypeBuilder<FridgeModel> builder)
        {
            builder.HasData
            (
                new FridgeModel
                {
                    Id = new Guid("80abbca8 - 664d - 4b20 - b5de - 024705497d4a"),
                    Name = "Beko RCSK 310M20",
                    Year = 2018
                },
                new FridgeModel
                {
                    Id = new Guid("86dba8c0 - d178 - 41e7 - 938c - ed49778fb52a"),
                    Name = "Tesler RC-55 White",
                    Year=2019
                },
                new FridgeModel()
                {
                    Id= new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                    Name= "Pozis RK-139 W",
                    Year=2020
                }) ; 
        }
    }
}
