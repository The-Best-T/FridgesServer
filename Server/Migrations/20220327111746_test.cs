using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00cdb753-6a3f-4fa0-b5c7-973ec535935a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f8923b2-b13d-4f02-8f28-57e02c8644d5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "61102998-c342-401d-a55c-32581d2ef0b7", "64a3300a-9bdc-4689-aedd-e16ed56823f5", "Client", "CLIENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61102998-c342-401d-a55c-32581d2ef0b7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f8923b2-b13d-4f02-8f28-57e02c8644d5", "d55b2e4b-dc64-4649-9b4e-245dd6c37c61", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "00cdb753-6a3f-4fa0-b5c7-973ec535935a", "4c6d5537-b583-485f-b504-ed82662e7319", "Client", "CLIENT" });
        }
    }
}
