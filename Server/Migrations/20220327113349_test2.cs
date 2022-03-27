using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61102998-c342-401d-a55c-32581d2ef0b7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1faba8ad-fe00-47b6-81de-09228018766c", "44c7cf07-e8d0-4caa-9e03-4fef7fb11350", "Client", "CLIENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1faba8ad-fe00-47b6-81de-09228018766c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "61102998-c342-401d-a55c-32581d2ef0b7", "64a3300a-9bdc-4689-aedd-e16ed56823f5", "Client", "CLIENT" });
        }
    }
}
