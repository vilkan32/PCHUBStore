using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class newPages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorfulBoxes_IndexPages_IndexPageId",
                table: "ColorfulBoxes");

            migrationBuilder.DropForeignKey(
                name: "FK_IndexCategories_IndexPages_IndexPageId",
                table: "IndexCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_IndexCategories_CategoryId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndexPages",
                table: "IndexPages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndexCategories",
                table: "IndexCategories");

            migrationBuilder.RenameTable(
                name: "IndexPages",
                newName: "Pages");

            migrationBuilder.RenameTable(
                name: "IndexCategories",
                newName: "PageCategories");

            migrationBuilder.RenameIndex(
                name: "IX_IndexCategories_IndexPageId",
                table: "PageCategories",
                newName: "IX_PageCategories_IndexPageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pages",
                table: "Pages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageCategories",
                table: "PageCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorfulBoxes_Pages_IndexPageId",
                table: "ColorfulBoxes",
                column: "IndexPageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_PageCategories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "PageCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageCategories_Pages_IndexPageId",
                table: "PageCategories",
                column: "IndexPageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorfulBoxes_Pages_IndexPageId",
                table: "ColorfulBoxes");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_PageCategories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_PageCategories_Pages_IndexPageId",
                table: "PageCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pages",
                table: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageCategories",
                table: "PageCategories");

            migrationBuilder.RenameTable(
                name: "Pages",
                newName: "IndexPages");

            migrationBuilder.RenameTable(
                name: "PageCategories",
                newName: "IndexCategories");

            migrationBuilder.RenameIndex(
                name: "IX_PageCategories_IndexPageId",
                table: "IndexCategories",
                newName: "IX_IndexCategories_IndexPageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndexPages",
                table: "IndexPages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndexCategories",
                table: "IndexCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorfulBoxes_IndexPages_IndexPageId",
                table: "ColorfulBoxes",
                column: "IndexPageId",
                principalTable: "IndexPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndexCategories_IndexPages_IndexPageId",
                table: "IndexCategories",
                column: "IndexPageId",
                principalTable: "IndexPages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_IndexCategories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "IndexCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
