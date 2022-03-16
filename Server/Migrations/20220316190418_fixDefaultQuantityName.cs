using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class fixDefaultQuantityName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaulQuantity",
                table: "Products",
                newName: "DefaultQuantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultQuantity",
                table: "Products",
                newName: "DefaulQuantity");
        }
    }
}
