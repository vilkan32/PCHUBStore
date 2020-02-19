using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class fixBasicCharacteristics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicCharacteristics_Products_ProductId1",
                table: "BasicCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_BasicCharacteristics_ProductId1",
                table: "BasicCharacteristics");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "BasicCharacteristics");

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousPrice",
                table: "Products",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Products",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "BasicCharacteristicsId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_BasicCharacteristics_BasicCharacteristicsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BasicCharacteristicsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BasicCharacteristicsId",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId1",
                table: "BasicCharacteristics",
                type: "nvarchar(450)",
                nullable: true);

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
    }
}
