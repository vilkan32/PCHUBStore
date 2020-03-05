using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class changeFilters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "FilterCategories");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "FilterCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViewSubCategoryName",
                table: "FilterCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "FilterCategories");

            migrationBuilder.DropColumn(
                name: "ViewSubCategoryName",
                table: "FilterCategories");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FilterCategories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
