using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class AddFridgeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerName",
                table: "Fridges",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Fridges",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FridgeModels",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Fridges");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerName",
                table: "Fridges",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FridgeModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.InsertData(
                table: "Fridges",
                columns: new[] { "FridgeId", "ModelId", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("d3f4ea2b-6110-4470-9edd-cb8b3d9413db"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Boston Griffin" },
                    { new Guid("ff6d827b-b8a8-447d-b072-b21c7647ba16"), new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), "Silas Evans" },
                    { new Guid("0eb7d17d-14d6-4bda-b75f-b91649f58299"), new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), "Seth Hughes" },
                    { new Guid("83309c7f-7bc3-4f37-aa58-52ec0174e05d"), new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), "Gary Bryant" }
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
        }
    }
}
