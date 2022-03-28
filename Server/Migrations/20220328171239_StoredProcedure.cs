using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class StoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var storedProceduere = "CREATE PROCEDURE [dbo].[FillFridges] AS " +
                "UPDATE FridgeProduct SET Quantity= " +
                "(SELECT Products.DefaultQuantity FROM Products WHERE Products.ProductId=FridgeProduct.ProductId) " +
                "WHERE Quantity=0";
            migrationBuilder.Sql(storedProceduere);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcedure = "DROP PROCEDURE [dbo].[FillFridges]";
            migrationBuilder.Sql(dropProcedure);
        }
    }
}
