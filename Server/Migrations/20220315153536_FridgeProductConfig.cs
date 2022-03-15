using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class FridgeProductConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "FridgeModelId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "FridgeModelId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.InsertData(
                table: "FridgeModels",
                columns: new[] { "FridgeModelId", "Name", "Year" },
                values: new object[,]
                {
                    { new Guid("c15120c7-8e1f-4e85-b7a1-93a1b6bee619"), "Tesler RC-55 White", 2019 },
                    { new Guid("3aca17c8-2554-49f0-b43f-cfaceb030d7f"), "Pozis RK-139 W", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "Name", "OwnerName" },
                values: new object[] { new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge1", "Boston Griffin" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DefaulQuantity", "Name" },
                values: new object[,]
                {
                    { new Guid("f480ecb3-2f19-4d0a-b1b5-9be38ff31a79"), 2, "Tomato" },
                    { new Guid("f66b7398-4bf7-48e5-843a-4a5e956fbaef"), 1, "Lemon" },
                    { new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"), 1, "Milk" },
                    { new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"), 5, "Potato" },
                    { new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"), 2, "Onion" }
                });

            migrationBuilder.InsertData(
                table: "FridgeProduct",
                columns: new[] { "FridgeId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"), new Guid("f66b7398-4bf7-48e5-843a-4a5e956fbaef"), 2 },
                    { new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"), new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"), 1 },
                    { new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"), new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"), 6 }
                });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "Name", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"), new Guid("c15120c7-8e1f-4e85-b7a1-93a1b6bee619"), "Fridge2", "Silas Evans" },
                    { new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"), new Guid("c15120c7-8e1f-4e85-b7a1-93a1b6bee619"), "Fridge4", "Gary Bryant" },
                    { new Guid("a9e539af-02f4-40b3-a196-bdf070d850f4"), new Guid("3aca17c8-2554-49f0-b43f-cfaceb030d7f"), "Fridge3", "Seth Hughes" }
                });

            migrationBuilder.InsertData(
                table: "FridgeProduct",
                columns: new[] { "FridgeId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"), new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"), 3 },
                    { new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"), new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"), 1 },
                    { new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"), new Guid("f480ecb3-2f19-4d0a-b1b5-9be38ff31a79"), 3 },
                    { new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"), new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"), 1 },
                    { new Guid("a9e539af-02f4-40b3-a196-bdf070d850f4"), new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"), 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FridgeProduct",
                keyColumns: new[] { "FridgeId", "ProductId" },
                keyValues: new object[] { new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"), new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac") });

            migrationBuilder.DeleteData(
                table: "FridgeProduct",
                keyColumns: new[] { "FridgeId", "ProductId" },
                keyValues: new object[] { new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"), new Guid("f480ecb3-2f19-4d0a-b1b5-9be38ff31a79") });

            migrationBuilder.DeleteData(
                table: "FridgeProduct",
                keyColumns: new[] { "FridgeId", "ProductId" },
                keyValues: new object[] { new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"), new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520") });

            migrationBuilder.DeleteData(
                table: "FridgeProduct",
                keyColumns: new[] { "FridgeId", "ProductId" },
                keyValues: new object[] { new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"), new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b") });

            migrationBuilder.DeleteData(
                table: "FridgeProduct",
                keyColumns: new[] { "FridgeId", "ProductId" },
                keyValues: new object[] { new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"), new Guid("f66b7398-4bf7-48e5-843a-4a5e956fbaef") });

            migrationBuilder.DeleteData(
                table: "FridgeProduct",
                keyColumns: new[] { "FridgeId", "ProductId" },
                keyValues: new object[] { new Guid("a9e539af-02f4-40b3-a196-bdf070d850f4"), new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520") });

            migrationBuilder.DeleteData(
                table: "FridgeProduct",
                keyColumns: new[] { "FridgeId", "ProductId" },
                keyValues: new object[] { new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"), new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b") });

            migrationBuilder.DeleteData(
                table: "FridgeProduct",
                keyColumns: new[] { "FridgeId", "ProductId" },
                keyValues: new object[] { new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"), new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac") });

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("36d2927d-c93f-4da5-b64e-5130b974f523"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("37f4a1cc-568c-4932-96fe-49f5b001664a"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("a9e539af-02f4-40b3-a196-bdf070d850f4"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("bfcadead-4c5d-4b90-86b6-68e76e65d039"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("08e41d72-fe05-409e-ad21-9fa68f0ba520"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ac80af88-38af-46de-8cdf-207f139e9e9b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("acf54693-0041-4d12-9e18-ede3c28d62ac"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f480ecb3-2f19-4d0a-b1b5-9be38ff31a79"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f66b7398-4bf7-48e5-843a-4a5e956fbaef"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "FridgeModelId",
                keyValue: new Guid("3aca17c8-2554-49f0-b43f-cfaceb030d7f"));

            migrationBuilder.DeleteData(
                table: "FridgeModels",
                keyColumn: "FridgeModelId",
                keyValue: new Guid("c15120c7-8e1f-4e85-b7a1-93a1b6bee619"));

            migrationBuilder.InsertData(
                table: "FridgeModels",
                columns: new[] { "FridgeModelId", "Name", "Year" },
                values: new object[,]
                {
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Tesler RC-55 White", 2019 },
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Pozis RK-139 W", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "Name", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("2b498cb6-399b-4852-ac7c-1c4afb1d1293"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge1", "Boston Griffin" },
                    { new Guid("fc4c2c76-f620-4f59-9380-5c2d8e083e0d"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge2", "Silas Evans" }
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

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "Name", "OwnerName" },
                values: new object[] { new Guid("12be9e00-5e71-455c-a001-a5398c625c57"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Fridge4", "Gary Bryant" });

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "Name", "OwnerName" },
                values: new object[] { new Guid("0e471e9b-ace9-4c4e-a6b2-e4c33caac5ae"), new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Fridge3", "Seth Hughes" });
        }
    }
}
