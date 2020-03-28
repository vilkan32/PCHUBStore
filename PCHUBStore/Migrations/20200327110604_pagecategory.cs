using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class pagecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryViewName",
                table: "PageCategories");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "PageCategories");

            migrationBuilder.AddColumn<int>(
                name: "PageCategoryId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_PageCategoryId",
                table: "Pictures",
                column: "PageCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_PageCategories_PageCategoryId",
                table: "Pictures",
                column: "PageCategoryId",
                principalTable: "PageCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_PageCategories_PageCategoryId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_PageCategoryId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PageCategoryId",
                table: "Pictures");

            migrationBuilder.AddColumn<string>(
                name: "CategoryViewName",
                table: "PageCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "PageCategories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
