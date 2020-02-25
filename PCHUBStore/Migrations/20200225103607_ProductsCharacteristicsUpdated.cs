using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class ProductsCharacteristicsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_BasicCharacteristics_BasicCharacteristicsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BasicCharacteristicsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BasicCharacteristics");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "BasicCharacteristics",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "BasicCharacteristics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "BasicCharacteristics",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BasicCharacteristics_ProductId1",
                table: "BasicCharacteristics",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BasicCharacteristics_Products_ProductId1",
                table: "BasicCharacteristics",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicCharacteristics_Products_ProductId1",
                table: "BasicCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_BasicCharacteristics_ProductId1",
                table: "BasicCharacteristics");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "BasicCharacteristics");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "BasicCharacteristics");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "BasicCharacteristics");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BasicCharacteristics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BasicCharacteristicsId",
                table: "Products",
                column: "BasicCharacteristicsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BasicCharacteristics_BasicCharacteristicsId",
                table: "Products",
                column: "BasicCharacteristicsId",
                principalTable: "BasicCharacteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
