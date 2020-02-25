using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class FixIdTypeForBasicCharacteristics : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "BasicCharacteristics",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_BasicCharacteristics_ProductId",
                table: "BasicCharacteristics",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasicCharacteristics_Products_ProductId",
                table: "BasicCharacteristics",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasicCharacteristics_Products_ProductId",
                table: "BasicCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_BasicCharacteristics_ProductId",
                table: "BasicCharacteristics");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "BasicCharacteristics",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
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
