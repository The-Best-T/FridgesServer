using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class lazy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("9a0d5d8c-5fdf-4d5a-8c29-cc5cea0ed972"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("ca0276e6-e7f7-43f4-8d96-c40e8387fb90"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("d128b600-b7d0-4732-9fbd-bc9be3098f2d"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("e34e76a7-62ea-4333-b158-bb90faff3789"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("279a3151-c742-4e21-af9e-517e687afed2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5494c80a-8a16-45a8-812a-3e5e5db514ad"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("80344d69-8951-4e2d-abf5-fd65d5801342"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a640c941-9723-430c-88ce-de6f4f1c1185"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("eeb0a8e9-5334-422c-8356-4861d172c77e"));

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "Name", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("94469ef2-fed9-4b57-8140-29fa379d6f20"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge1", "Boston Griffin" },
                    { new Guid("0462a078-2e93-473c-b202-569fd7256e13"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge2", "Silas Evans" },
                    { new Guid("68cf9bd6-4b1d-4a6d-b154-e4c5219aa1d7"), new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Fridge3", "Seth Hughes" },
                    { new Guid("136510d1-aa22-4f64-9a4e-e765340be40d"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Fridge4", "Gary Bryant" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DefaulQuantity", "Name" },
                values: new object[,]
                {
                    { new Guid("a4f69b25-b701-436e-b705-2dd03060e188"), 2, "Tomato" },
                    { new Guid("6091c783-91f8-4b0c-b66f-4580fb014154"), 1, "Lemon" },
                    { new Guid("025c7e83-1950-456c-a65a-c1ad8b036287"), 1, "Milk" },
                    { new Guid("e66fce94-ef42-429b-94c2-460a14ef5972"), 5, "Potato" },
                    { new Guid("97dbe6ad-b53e-4ea6-9472-aa331a4e04a4"), 2, "Onion" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("0462a078-2e93-473c-b202-569fd7256e13"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("136510d1-aa22-4f64-9a4e-e765340be40d"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("68cf9bd6-4b1d-4a6d-b154-e4c5219aa1d7"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("94469ef2-fed9-4b57-8140-29fa379d6f20"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("025c7e83-1950-456c-a65a-c1ad8b036287"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("6091c783-91f8-4b0c-b66f-4580fb014154"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("97dbe6ad-b53e-4ea6-9472-aa331a4e04a4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a4f69b25-b701-436e-b705-2dd03060e188"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("e66fce94-ef42-429b-94c2-460a14ef5972"));

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "Name", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("e34e76a7-62ea-4333-b158-bb90faff3789"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge1", "Boston Griffin" },
                    { new Guid("9a0d5d8c-5fdf-4d5a-8c29-cc5cea0ed972"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge2", "Silas Evans" },
                    { new Guid("d128b600-b7d0-4732-9fbd-bc9be3098f2d"), new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Fridge3", "Seth Hughes" },
                    { new Guid("ca0276e6-e7f7-43f4-8d96-c40e8387fb90"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Fridge4", "Gary Bryant" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DefaulQuantity", "Name" },
                values: new object[,]
                {
                    { new Guid("80344d69-8951-4e2d-abf5-fd65d5801342"), 2, "Tomato" },
                    { new Guid("5494c80a-8a16-45a8-812a-3e5e5db514ad"), 1, "Lemon" },
                    { new Guid("a640c941-9723-430c-88ce-de6f4f1c1185"), 1, "Milk" },
                    { new Guid("eeb0a8e9-5334-422c-8356-4861d172c77e"), 5, "Potato" },
                    { new Guid("279a3151-c742-4e21-af9e-517e687afed2"), 2, "Onion" }
                });
        }
    }
}
