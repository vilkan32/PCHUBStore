using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCHUBStore.Migrations
{
    public partial class chats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    ConnectionId = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    TechnicianName = table.Column<string>(nullable: true),
                    Messages = table.Column<string>(nullable: true),
                    ConnectionTerminated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");
        }
    }
}
