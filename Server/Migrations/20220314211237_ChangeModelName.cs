using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class ChangeModelName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("13471130-db56-49ad-81f3-b4e713f8f3a8"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("688ca6d9-cd98-4e91-993c-aa28f696b0a4"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("86bc42b0-3e71-45cc-86a2-d21bb0f05a13"));

            migrationBuilder.DeleteData(
                table: "Fridges",
                keyColumn: "FridgeId",
                keyValue: new Guid("96e78e54-3713-4660-b043-f32c0834a7e0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0bd3fbf2-cdef-444d-93ce-b6d422205022"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1c34ecf2-f332-4363-b715-d41c3877748d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4640326a-d8d4-47d7-ba6c-b53ac8670e6b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5206e205-1869-4e47-9278-1e49f5bfe33d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("669334fb-08cf-401e-b78e-05ccb636c185"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("688ca6d9-cd98-4e91-993c-aa28f696b0a4"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge1", "Boston Griffin" },
                    { new Guid("96e78e54-3713-4660-b043-f32c0834a7e0"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Fridge2", "Silas Evans" },
                    { new Guid("13471130-db56-49ad-81f3-b4e713f8f3a8"), new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Fridge3", "Seth Hughes" },
                    { new Guid("86bc42b0-3e71-45cc-86a2-d21bb0f05a13"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Fridge4", "Gary Bryant" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DefaulQuantity", "Name" },
                values: new object[,]
                {
                    { new Guid("4640326a-d8d4-47d7-ba6c-b53ac8670e6b"), 2, "Tomato" },
                    { new Guid("1c34ecf2-f332-4363-b715-d41c3877748d"), 1, "Lemon" },
                    { new Guid("669334fb-08cf-401e-b78e-05ccb636c185"), 1, "Milk" },
                    { new Guid("0bd3fbf2-cdef-444d-93ce-b6d422205022"), 5, "Potato" },
                    { new Guid("5206e205-1869-4e47-9278-1e49f5bfe33d"), 2, "Onion" }
                });
        }
    }
}
