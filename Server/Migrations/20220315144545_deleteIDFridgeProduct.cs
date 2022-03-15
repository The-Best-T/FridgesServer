using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class deleteIDFridgeProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FridgeProduct");

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "Name", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("2b498cb6-399b-4852-ac7c-1c4afb1d1293"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge1", "Boston Griffin" },
                    { new Guid("fc4c2c76-f620-4f59-9380-5c2d8e083e0d"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge2", "Silas Evans" },
                    { new Guid("0e471e9b-ace9-4c4e-a6b2-e4c33caac5ae"), new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Fridge3", "Seth Hughes" },
                    { new Guid("12be9e00-5e71-455c-a001-a5398c625c57"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Fridge4", "Gary Bryant" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DefaulQuantity", "Name" },
                values: new object[,]
                {
                    { new Guid("bc51834c-bb58-436a-9b5c-a9aa9e7b2fc6"), 2, "Tomato" },
                    { new Guid("85ce794a-87d3-4d73-a4d2-fe79c27ec4ea"), 1, "Lemon" },
                    { new Guid("edd3518e-07f5-45b7-b817-b36ebaa17e78"), 1, "Milk" },
                    { new Guid("62bdc683-0419-4358-a225-1f5d09068269"), 5, "Potato" },
                    { new Guid("464b7c37-bffc-4b6f-a405-99cf0f8f389f"), 2, "Onion" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("0e471e9b-ace9-4c4e-a6b2-e4c33caac5ae"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("12be9e00-5e71-455c-a001-a5398c625c57"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("2b498cb6-399b-4852-ac7c-1c4afb1d1293"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("fc4c2c76-f620-4f59-9380-5c2d8e083e0d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("464b7c37-bffc-4b6f-a405-99cf0f8f389f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("62bdc683-0419-4358-a225-1f5d09068269"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("85ce794a-87d3-4d73-a4d2-fe79c27ec4ea"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("bc51834c-bb58-436a-9b5c-a9aa9e7b2fc6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("edd3518e-07f5-45b7-b817-b36ebaa17e78"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "FridgeProduct",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
