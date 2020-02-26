using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class idFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Products_ProductId1",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ProductId1",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Pictures");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainPicForProductId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ProductId",
                table: "Pictures",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Products_ProductId",
                table: "Pictures",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Products_ProductId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ProductId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "MainPicForProductId",
                table: "Pictures");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Pictures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "Pictures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ProductId1",
                table: "Pictures",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Products_ProductId1",
                table: "Pictures",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
