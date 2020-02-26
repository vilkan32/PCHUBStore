using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class MainPictureProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainPictureId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Pictures_MainPictureId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_MainPictureId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MainPictureId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Pictures",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
