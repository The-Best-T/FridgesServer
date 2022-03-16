using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FridgeModels",
                columns: new[] { "FridgeModelId", "Name", "Year" },
                values: new object[,]
                {
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Beko RCSK 310M20", 2018 },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Tesler RC-55 White", 2019 },
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Pozis RK-139 W", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DefaulQuantity", "Name" },
                values: new object[,]
                {
                    { new Guid("5ec6bd0e-950c-41ed-a1cb-b8ae721b9bbf"), 2, "Tomato" },
                    { new Guid("dd6b036b-0f61-4731-b767-cefafd53b1b1"), 1, "Lemon" },
                    { new Guid("31d9de02-cf0e-4f33-8c53-155682504b50"), 1, "Milk" },
                    { new Guid("8a9b4c3a-f358-4166-8f95-72205c8cf598"), 5, "Potato" },
                    { new Guid("37652645-2c6e-4b39-9ebc-fdf67aebbfbf"), 2, "Onion" }
                });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("d3f4ea2b-6110-4470-9edd-cb8b3d9413db"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Boston Griffin" },
                    { new Guid("ff6d827b-b8a8-447d-b072-b21c7647ba16"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Silas Evans" },
                    { new Guid("83309c7f-7bc3-4f37-aa58-52ec0174e05d"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Gary Bryant" },
                    { new Guid("0eb7d17d-14d6-4bda-b75f-b91649f58299"), new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Seth Hughes" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("0eb7d17d-14d6-4bda-b75f-b91649f58299"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("83309c7f-7bc3-4f37-aa58-52ec0174e05d"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("d3f4ea2b-6110-4470-9edd-cb8b3d9413db"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("ff6d827b-b8a8-447d-b072-b21c7647ba16"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("31d9de02-cf0e-4f33-8c53-155682504b50"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("37652645-2c6e-4b39-9ebc-fdf67aebbfbf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5ec6bd0e-950c-41ed-a1cb-b8ae721b9bbf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("8a9b4c3a-f358-4166-8f95-72205c8cf598"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("dd6b036b-0f61-4731-b767-cefafd53b1b1"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "FridgeModelId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "FridgeModelId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "FridgeModelId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));
        }
    }
}
