using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class characteristicsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FullCharacteristics_Products_ProductId1",
                table: "FullCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_FullCharacteristics_ProductId1",
                table: "FullCharacteristics");

            migrationBuilder.DropColumn(
                name: "BasicCharacteristicsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "FullCharacteristics");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "FullCharacteristics",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_FullCharacteristics_ProductId",
                table: "FullCharacteristics",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_FullCharacteristics_Products_ProductId",
                table: "FullCharacteristics",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FullCharacteristics_Products_ProductId",
                table: "FullCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_FullCharacteristics_ProductId",
                table: "FullCharacteristics");

            migrationBuilder.AddColumn<int>(
                name: "BasicCharacteristicsId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "FullCharacteristics",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "FullCharacteristics",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FullCharacteristics_ProductId1",
                table: "FullCharacteristics",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FullCharacteristics_Products_ProductId1",
                table: "FullCharacteristics",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
