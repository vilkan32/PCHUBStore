using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class indexPageAdministration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndexPages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexPages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ColorfulBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Href = table.Column<string>(nullable: true),
                    IndexPageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorfulBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorfulBoxes_IndexPages_IndexPageId",
                        column: x => x.IndexPageId,
                        principalTable: "IndexPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndexCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    CategoryName = table.Column<string>(nullable: false),
                    AllName = table.Column<string>(nullable: false),
                    AllHref = table.Column<string>(nullable: false),
                    IndexPageId = table.Column<int>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndexCategories_IndexPages_IndexPageId",
                        column: x => x.IndexPageId,
                        principalTable: "IndexPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Href = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_IndexCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "IndexCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorfulBoxes_IndexPageId",
                table: "ColorfulBoxes",
                column: "IndexPageId");

            migrationBuilder.CreateIndex(
                name: "IX_IndexCategories_IndexPageId",
                table: "IndexCategories",
                column: "IndexPageId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorfulBoxes");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "IndexCategories");

            migrationBuilder.DropTable(
                name: "IndexPages");
        }
    }
}
