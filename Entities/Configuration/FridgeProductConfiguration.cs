using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class FridgeProductConfiguration : IEntityTypeConfiguration<FridgeProduct>
    {
        public void Configure(EntityTypeBuilder<FridgeProduct> builder)
        {
            builder.HasData
            (
                new FridgeProduct 
                {
                    FridgeId=new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"),
                    ProductId=new Guid("f66b7398-4bf7-48e5-843a-4a5e956fbaef"),
                    Quantity=2
                },
                new FridgeProduct
                {
                    FridgeId = new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"),
                    ProductId = new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"),
                    Quantity = 1
                },
                new FridgeProduct
                {
                    FridgeId = new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"),
                    ProductId = new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"),
                    Quantity = 6
                },
                new FridgeProduct
                {
                    FridgeId = new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"),
                    ProductId = new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"),
                    Quantity = 3
                },
                new FridgeProduct
                {
                    FridgeId = new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"),
                    ProductId = new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"),
                    Quantity = 1
                },
                new FridgeProduct
                {
                    FridgeId = new Guid("a9e539af-02f4-40b3-a196-bdf070d850f4"),
                    ProductId = new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"),
                    Quantity = 2
                },
                new FridgeProduct
                {
                    FridgeId = new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"),
                    ProductId = new Guid("f480ecb3-2f19-4d0a-b1b5-9be38ff31a79"),
                    Quantity = 3
                },
                new FridgeProduct
                {
                    FridgeId = new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"),
                    ProductId = new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"),
                    Quantity = 1
                }
            );  
        }
    }
}
