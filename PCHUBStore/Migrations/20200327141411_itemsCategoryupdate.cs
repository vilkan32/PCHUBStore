using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class itemsCategoryupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_PageCategories_CategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemsCategory",
                table: "PageCategories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ItemsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    PageCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsCategories_PageCategories_PageCategoryId",
                        column: x => x.PageCategoryId,
                        principalTable: "PageCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCategories_PageCategoryId",
                table: "ItemsCategories",
                column: "PageCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemsCategories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "ItemsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemsCategories_CategoryId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemsCategories");

            migrationBuilder.AddColumn<string>(
                name: "ItemsCategory",
                table: "PageCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_PageCategories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "PageCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
