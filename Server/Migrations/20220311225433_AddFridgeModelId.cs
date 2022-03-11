using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class AddFridgeModelId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_FridgeModels_FridgeModelId",
                table: "Fridges");

            migrationBuilder.RenameColumn(
                name: "FridgeModelId",
                table: "Fridges",
                newName: "ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Fridges_FridgeModelId",
                table: "Fridges",
                newName: "IX_Fridges_ModelId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModelId",
                table: "Fridges",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_FridgeModels_ModelId",
                table: "Fridges",
                column: "ModelId",
                principalTable: "FridgeModels",
                principalColumn: "FridgeModelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fridges_FridgeModels_ModelId",
                table: "Fridges");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "Fridges",
                newName: "FridgeModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Fridges_ModelId",
                table: "Fridges",
                newName: "IX_Fridges_FridgeModelId");

            migrationBuilder.AlterColumn<Guid>(
                name: "FridgeModelId",
                table: "Fridges",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Fridges_FridgeModels_FridgeModelId",
                table: "Fridges",
                column: "FridgeModelId",
                principalTable: "FridgeModels",
                principalColumn: "FridgeModelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
