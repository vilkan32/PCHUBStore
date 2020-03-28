using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class mainSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainSliderId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainSliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainSliders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_MainSliderId",
                table: "Pictures",
                column: "MainSliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_MainSliders_MainSliderId",
                table: "Pictures",
                column: "MainSliderId",
                principalTable: "MainSliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_MainSliders_MainSliderId",
                table: "Pictures");

            migrationBuilder.DropTable(
                name: "MainSliders");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_MainSliderId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "MainSliderId",
                table: "Pictures");
        }
    }
}
