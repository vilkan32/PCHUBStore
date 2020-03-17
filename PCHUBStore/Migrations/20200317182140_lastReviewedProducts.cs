using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class lastReviewedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserLastReviewedId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserLastReviewedId",
                table: "Products",
                column: "UserLastReviewedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserLastReviewedId",
                table: "Products",
                column: "UserLastReviewedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserLastReviewedId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserLastReviewedId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserLastReviewedId",
                table: "Products");
        }
    }
}
