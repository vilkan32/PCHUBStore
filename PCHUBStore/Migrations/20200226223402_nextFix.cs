using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class nextFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Pictures_MainPictureId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MainPictureId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "MainPictureId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MainPictureId",
                table: "Products",
                column: "MainPictureId",
                unique: true,
                filter: "[MainPictureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Pictures_MainPictureId",
                table: "Products",
                column: "MainPictureId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Pictures_MainPictureId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MainPictureId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "MainPictureId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_MainPictureId",
                table: "Products",
                column: "MainPictureId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Pictures_MainPictureId",
                table: "Products",
                column: "MainPictureId",
                principalTable: "Pictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
