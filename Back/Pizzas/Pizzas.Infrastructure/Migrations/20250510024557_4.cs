using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_CategoryEntity_CategoryId",
                table: "Pizzas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEntity",
                table: "CategoryEntity");

            migrationBuilder.RenameTable(
                name: "CategoryEntity",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryEntity_Name",
                table: "Categories",
                newName: "IX_Categories_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Categories_CategoryId",
                table: "Pizzas",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Categories_CategoryId",
                table: "Pizzas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "CategoryEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Name",
                table: "CategoryEntity",
                newName: "IX_CategoryEntity_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEntity",
                table: "CategoryEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_CategoryEntity_CategoryId",
                table: "Pizzas",
                column: "CategoryId",
                principalTable: "CategoryEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
