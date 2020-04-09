using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class chatRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    IsAuthenticated = table.Column<bool>(nullable: false),
                    ConnetctionId = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRequests");
        }
    }
}
