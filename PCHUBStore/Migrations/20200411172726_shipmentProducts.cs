using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class shipmentProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shipments_ShipmentId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShipmentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ShipmentProducts",
                columns: table => new
                {
                    ShipmentId = table.Column<int>(nullable: false),
                    ProductId = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentProducts", x => new { x.ProductId, x.ShipmentId });
                    table.ForeignKey(
                        name: "FK_ShipmentProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentProducts_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentProducts_ShipmentId",
                table: "ShipmentProducts",
                column: "ShipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentProducts");

            migrationBuilder.AddColumn<int>(
                name: "ShipmentId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShipmentId",
                table: "Products",
                column: "ShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shipments_ShipmentId",
                table: "Products",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
